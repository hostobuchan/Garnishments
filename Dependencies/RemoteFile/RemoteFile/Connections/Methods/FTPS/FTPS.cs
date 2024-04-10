using RemoteFile.Delegates;
using RemoteFile.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RemoteFile.Connections.Methods.FTPS
{
    public class FTPS : Interfaces.SiteConnectionInterface
    {
        private ftps Connection { get; set; }
        public string Host { get { return this.Connection?.Host; } }
        public int? Port { get { return this.Connection?.Port; } }
        public string UserName { get { return this.Connection?.UserName; } }
        public string Password { get { return this.Connection?.Password; } }
        public string Directory { get { return this.Connection?.Directory; } }
        public bool UsePassive { get { return this.Connection?.UsePassive ?? true; } }
        public bool Explicit_TLS { get { return this.Connection?.Explicit_TLS ?? true; } }
        public WebProxy Proxy { get { return this.Connection?.Proxy; } }
        public ICredentials Credentials { get { return this.Connection?.Credentials; } }

        public FTPS(string site, int? port, string userName, string password, string directory, bool usePassive = true, bool explicit_TLS = false)
        {
            this.Connection = new ftps(site, port, userName, password, directory, usePassive, explicit_TLS);
        }
        public FTPS(Connections.SiteLogin login) : this(login.FtpSite, login.FtpPort, login.FtpUserName, login.FtpPassword, login.FtpDirectory, true, login.ProtocolUsed == Enums.ConnectionMethod.FTPS_Explicit) { }

        public void Close()
        {
            this.Connection.Close();
        }

        public void Connect(int? port = null)
        {
            this.Connection.SSLConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls;
            this.Connection.Connect(this.Connection.Host, port ?? this.Connection.Port ?? 990);
            if (this.Connection.Explicit_TLS) this.Connection.AuthTLS();
            this.Connection.Login(this.Connection.UserName, this.Password);
        }

        public void ChangeDirectory(string remoteDirectory)
        {
            this.Connection.ChangeFolder(remoteDirectory);
        }

        public IEnumerable<Files.RemoteFile> FileListing(RemoteFolder remoteFolder)
        {
            return this.FileListing(remoteFolder.FullPath);
        }

        public IEnumerable<Files.RemoteFile> FileListing(string remoteFolder)
        {
            foreach (var file in this.Connection.List(remoteFolder))
            {
                if (file.IsFile)
                {
                    yield return new Files.RemoteFile(file.Name, file.Name, (ulong?)file.Size, file.ModifyDate);
                }
            }
        }

        public IEnumerable<RemoteFolder> FolderListing(RemoteFolder remoteFolder)
        {
            return this.FolderListing(remoteFolder.FullPath);
        }

        public IEnumerable<RemoteFolder> FolderListing(string remoteFolder)
        {
            foreach (var folder in this.Connection.List(remoteFolder))
            {
                if (folder.IsFolder)
                {
                    yield return new RemoteFolder(this, folder.Name, folder.Name, (ulong?)folder.Size, folder.ModifyDate);
                }
            }
        }

        public void GetFile(Files.RemoteFile source, string destination, FileTransferEventHandler progress = null)
        {
            GetFile(source.FullPath, destination, progress);
        }

        public void GetFile(Files.RemoteFile source, Stream destination, FileTransferEventHandler progress = null)
        {
            var progressUpdate = CreateProgressHandler(source.FullPath, "", progress);
            this.Connection.Progress += progressUpdate;
            this.Connection.Download(source.FullPath, destination);
            this.Connection.Progress -= progressUpdate;
        }

        public void GetFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            var progressUpdate = CreateProgressHandler(source, destination, progress);
            this.Connection.Progress += progressUpdate;
            this.Connection.Download(source, destination);
            this.Connection.Progress -= progressUpdate;
        }

        public void GetFile(string source, Stream destination, FileTransferEventHandler progress = null)
        {
            var progressUpdate = CreateProgressHandler(source, "", progress);
            this.Connection.Progress += progressUpdate;
            this.Connection.Download(source, destination);
            this.Connection.Progress -= progressUpdate;
        }

        public void PutFile(Stream source, string destination, FileTransferEventHandler progress = null)
        {
            var progressUpdate = CreateProgressHandler("", destination, progress);
            this.Connection.Progress += progressUpdate;
            this.Connection.Upload(destination, source);
            this.Connection.Progress -= progressUpdate;
        }

        public void PutFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            var progressUpdate = CreateProgressHandler(source, destination, progress);
            this.Connection.Progress += progressUpdate;
            this.Connection.Upload(destination, source);
            this.Connection.Progress -= progressUpdate;
        }

        public void Delete(Files.RemoteFile remoteFile)
        {
            this.Delete(remoteFile.FullPath);
        }

        public void Delete(string remoteFile)
        {
            this.Connection.DeleteFile(remoteFile);
        }

        public void Dispose()
        {
            this.Connection.Dispose();
            this.Connection = null;
        }

        EventHandler<Limilabs.FTP.Client.ProgressEventArgs> CreateProgressHandler(string source, string destination, FileTransferEventHandler handler)
        {
            return (o, e) => { handler?.Invoke(this, new FileTransferEventArgs(this, source, destination, (ulong)e.TotalBytesTransferred, (ulong)e.TotalBytesToTransfer)); };
        }
    }

    class ftps : Limilabs.FTP.Client.Ftp, IDisposable
    {
        public string Host { get; private set; }
        public int? Port { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Directory { get; set; }
        public bool UsePassive { get; set; }
        public bool Explicit_TLS { get; set; }
        public WebProxy Proxy { get; set; }
        public ICredentials Credentials { get { return new NetworkCredential(this.UserName, this.Password); } }

        public ftps(string Site, int? Port, string UserName, string Password, string directory, bool UsePassive = true, bool Explicit_TLS = false)
        {
            this.Host = Site;
            this.Port = Port;
            this.UserName = UserName;
            this.Password = Password;
            this.UsePassive = UsePassive;
            this.Explicit_TLS = Explicit_TLS;
            this.ServerCertificateValidate += FTPS_ServerCertificateValidate;
        }
        public ftps(Connections.SiteLogin login) : this(login.FtpSite, login.FtpPort, login.FtpUserName, login.FtpPassword, login.FtpDirectory, true, login.ProtocolUsed == Enums.ConnectionMethod.FTPS_Explicit) { }

        void FTPS_ServerCertificateValidate(object sender, Limilabs.FTP.Client.ServerCertificateValidateEventArgs e)
        {
            e.IsValid = true; //Ignore Validation and Accept Certificate
        }

        public void SetFolder(string Folder)
        {
            ChangeFolder(Folder);
        }

        void IDisposable.Dispose()
        {
            if (this.Connected)
            {
                try
                {
                    this.Close();
                }
                finally { }
            }
        }
    }
}
