using System;
using System.Collections.Generic;

namespace RemoteFile.Interfaces
{
    public interface SiteConnectionInterface : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        void Connect(int? port = null);
        /// <summary>
        /// 
        /// </summary>
        void Close();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteDirectory"></param>
        void ChangeDirectory(string remoteDirectory);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFolder"></param>
        /// <returns></returns>
        IEnumerable<Files.RemoteFile> FileListing(string remoteFolder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFolder"></param>
        /// <returns></returns>
        IEnumerable<Files.RemoteFile> FileListing(Files.RemoteFolder remoteFolder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFolder"></param>
        /// <returns></returns>
        IEnumerable<Files.RemoteFolder> FolderListing(string remoteFolder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFolder"></param>
        /// <returns></returns>
        IEnumerable<Files.RemoteFolder> FolderListing(Files.RemoteFolder remoteFolder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void PutFile(string source, string destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void PutFile(System.IO.Stream source, string destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void GetFile(string source, string destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void GetFile(string source, System.IO.Stream destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void GetFile(Files.RemoteFile source, string destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="progress"></param>
        void GetFile(Files.RemoteFile source, System.IO.Stream destination, Delegates.FileTransferEventHandler progress = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFile"></param>
        void Delete(Files.RemoteFile remoteFile);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFile"></param>
        void Delete(string remoteFile);
    }
}
