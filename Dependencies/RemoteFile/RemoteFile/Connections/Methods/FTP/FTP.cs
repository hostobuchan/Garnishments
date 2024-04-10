using RemoteFile.Delegates;
using RemoteFile.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RemoteFile.Connections.Methods.FTP
{
    public class FTP : Interfaces.SiteConnectionInterface
    {
        public string Site { get; private set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private int? Port { get; set; }
        public string RemoteDirectory { get; private set; }
        public bool UsePassive { get; set; }
        public WebProxy Proxy { get; set; }
        private ICredentials Credentials { get { return new NetworkCredential(this.UserName, this.Password); } }

        public FTP(string site, string userName, string password, int? port = 21, string directory = "", bool usePassive = true)
        {
            this.Site = site;
            this.UserName = userName;
            this.Password = password;
            this.Port = port;
            this.RemoteDirectory = directory;
            this.UsePassive = usePassive;
        }
        public FTP(Connections.SiteLogin login) : this(login.FtpName, login.FtpUserName, login.FtpPassword, login.FtpPort, login.FtpDirectory) { }

        public List<string> ListFiles(string FTPDirectory, int Timeout = 60000)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + this.Site + "/" + FTPDirectory + "/");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.UseBinary = true;
            request.Proxy = this.Proxy;
            request.UsePassive = this.UsePassive;
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.Credentials = this.Credentials;

            WebResponse response = request.GetResponse();
            string file;
            List<string> Files = new List<string>();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            while ((file = reader.ReadLine()) != null)
            {
                Console.WriteLine(file);
                Files.Add(file);
            }
            reader.Close();

            return Files;
        }

        public string UploadFile(string LocalFileLocation, string FTPFileLocation, int Timeout = 60000)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + this.Site + "/" + FTPFileLocation);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.Proxy = this.Proxy;
            request.UsePassive = this.UsePassive;
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.Credentials = this.Credentials;

            // Create Filestream To Upload
            StreamReader UploadStream = new StreamReader(LocalFileLocation);

            // Up Stream
            request.ContentLength = UploadStream.BaseStream.Length;
            Stream requestStream = request.GetRequestStream();
            UploadStream.BaseStream.CopyTo(requestStream);
            requestStream.Close();

            // Read Response From Server
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }

        public void DownloadFile(string FTPFileLocation, string LocalFileLocation, int Timeout = 60000)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + this.Site + "/" + FTPFileLocation);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.Proxy = this.Proxy;
            request.UsePassive = this.UsePassive;
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.Credentials = this.Credentials;

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            FileStream fs = new FileStream(LocalFileLocation, FileMode.Create);
            responseStream.CopyTo(fs);
            responseStream.Close();
            fs.Flush();
            fs.Close();
        }

        public string DeleteFile(string FTPFileLocation, int Timeout = 60000)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + this.Site + "/" + FTPFileLocation);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.UseBinary = true;
            request.Proxy = this.Proxy;
            request.UsePassive = this.UsePassive;
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            request.Credentials = this.Credentials;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            return reader.ReadToEnd();
        }

        public void Connect(int? port = null)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Files.RemoteFile> FileListing(string remoteFolder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Files.RemoteFile> FileListing(RemoteFolder remoteFolder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteFolder> FolderListing(string remoteFolder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteFolder> FolderListing(RemoteFolder remoteFolder)
        {
            throw new NotImplementedException();
        }

        public void PutFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void PutFile(Stream source, string destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void GetFile(string source, string destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void GetFile(string source, Stream destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void GetFile(Files.RemoteFile source, string destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void GetFile(Files.RemoteFile source, Stream destination, FileTransferEventHandler progress = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ChangeDirectory(string remoteDirectory)
        {
            throw new NotImplementedException();
        }

        public void Delete(Files.RemoteFile remoteFile)
        {
            throw new NotImplementedException();
        }

        public void Delete(string remoteFile)
        {
            throw new NotImplementedException();
        }
    }
}
