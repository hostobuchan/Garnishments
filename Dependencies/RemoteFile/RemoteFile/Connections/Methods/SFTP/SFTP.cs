using RemoteFile.Delegates;
using RemoteFile.Files;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoteFile.Connections.Methods.SFTP
{
    public class SFTP : Interfaces.SiteConnectionInterface
    {
        private Sftp2 Connection;
        private SiteLogin Login;

        public SFTP(Connections.SiteLogin login)
        {
            this.Connection = new Sftp2(login);
            this.Login = login;
        }
        public SFTP(string site, int port, string user, string pass)
        {
            this.Connection = new Sftp2(site, port, user, pass);
        }

        public void Close()
        {
            this.Connection.Disconnect();
        }

        public void Connect(int? port = null)
        {
            this.Connection.Connect();
            if (this.Login != null)
                this.Connection.ChangeDirectory(this.Login.FtpDirectory);
        }

        public void ChangeDirectory(string remoteDirectory)
        {
            this.Connection.ChangeDirectory(remoteDirectory);
        }

        public IEnumerable<Files.RemoteFile> FileListing(RemoteFolder remoteFolder)
        {
            return FileListing(remoteFolder.FullPath);
        }

        public IEnumerable<Files.RemoteFile> FileListing(string remoteFolder)
        {
            foreach (var file in this.Connection.FileListing(remoteFolder))
            {
                yield return new Files.RemoteFile(file.FullName, file.Name, (ulong)file.Length, file.LastWriteTime);
            }
        }

        public IEnumerable<RemoteFolder> FolderListing(RemoteFolder remoteFolder)
        {
            return FolderListing(remoteFolder.FullPath);
        }

        public IEnumerable<RemoteFolder> FolderListing(string remoteFolder)
        {
            foreach (var file in this.Connection.DirectoryListing(remoteFolder))
            {
                yield return new Files.RemoteFolder(this, file.FullName, file.Name, (ulong)file.Length, file.LastWriteTime);
            }
        }

        public void GetFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(destination, System.IO.FileMode.Create))
            {
                this.Connection.DownloadFile(source, fs, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, source, destination, trans, 0)); });
            }
        }

        public void GetFile(string source, System.IO.Stream destination, FileTransferEventHandler progress = null)
        {
            this.Connection.DownloadFile(source, destination, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, source, "", trans, 0)); });
        }

        public void GetFile(Files.RemoteFile source, string destination, FileTransferEventHandler progress = null)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(destination, System.IO.FileMode.Create))
            {
                this.Connection.DownloadFile(source.FullPath, fs, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, source.FullPath, destination, trans, source.Size ?? 0)); });
            }
        }

        public void GetFile(Files.RemoteFile source, System.IO.Stream destination, FileTransferEventHandler progress = null)
        {
            this.Connection.DownloadFile(source.FullPath, destination, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, source.FullPath, "", trans, 0)); });
        }

        public void PutFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(source, System.IO.FileMode.Open))
            {
                this.Connection.UploadFile(fs, destination, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, source, destination, trans, (ulong)fs.Length)); });
            }
        }

        public void PutFile(System.IO.Stream source, string destination, Delegates.FileTransferEventHandler progress = null)
        {
            this.Connection.UploadFile(source, destination, (trans) => { progress?.Invoke(this, new FileTransferEventArgs(this, destination: destination, transferred: trans, total: (ulong)source.Length)); });
        }

        public void Delete(Files.RemoteFile remoteFile)
        {
            this.Delete(remoteFile.FullPath);
        }

        public void Delete(string remoteFile)
        {
            this.Connection.DeleteFile(remoteFile);
        }

        #region IDisposable

        public void Dispose()
        {
            try
            {
                this.Connection.Dispose();
                this.Connection = null;
            }
            catch { }
        }

        #endregion
    }

    class Sftp2 : Renci.SshNet.SftpClient
    {
        public Sftp2(Connections.SiteLogin login) : this(login.FtpSite, login.FtpPort ?? 22, login.FtpUserName, login.FtpPassword) { }
        public Sftp2(string site, int port, string user, string pass) : base(CreateConnInfo(site, port, user, pass))
        {
            this.HostKeyReceived += (sender, e) =>
            {
                e.CanTrust = true;
            };
        }

        private static Renci.SshNet.ConnectionInfo CreateConnInfo(string site, int port, string user, string pass)
        {
            Renci.SshNet.KeyboardInteractiveAuthenticationMethod keyAuth = new Renci.SshNet.KeyboardInteractiveAuthenticationMethod(user);
            keyAuth.AuthenticationPrompt += new EventHandler<Renci.SshNet.Common.AuthenticationPromptEventArgs>((sender, e) =>
            {
                foreach (var prompt in e.Prompts)
                {
                    if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        prompt.Response = pass;
                    }
                }
            });
            return new Renci.SshNet.ConnectionInfo(site, port, user, new Renci.SshNet.AuthenticationMethod[]
            {
                keyAuth,
                new Renci.SshNet.PasswordAuthenticationMethod(user, pass)
            });
        }

        public IEnumerable<Renci.SshNet.Sftp.SftpFile> FileListing(string remoteLocation)
        {
            return this.ListDirectory(remoteLocation).Where(el => !el.IsDirectory);
        }
        public IEnumerable<Renci.SshNet.Sftp.SftpFile> DirectoryListing(string remoteLocation)
        {
            return this.ListDirectory(remoteLocation).Where(el => el.IsDirectory);
        }
    }
}
