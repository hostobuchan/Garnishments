using System;
using System.Collections.Generic;

namespace RemoteFile.Files
{
    public class RemoteFolder : RemoteFile
    {
        event Delegates.FileEnumeratorEventHandler FileListingRequested;
        event Delegates.FolderEnumeratorEventHandler FolderListingRequested;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Files.RemoteFile> FileListing()
        {
            return this.FileListingRequested?.Invoke(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Files.RemoteFolder> FolderListing()
        {
            return this.FolderListingRequested?.Invoke(this);
        }

        protected internal RemoteFolder(Interfaces.SiteConnectionInterface site, string fullPath, string name = "", ulong? size = null, DateTime? creationDate = null) : base(fullPath, name, size, creationDate)
        {
            this.FileListingRequested = site.FileListing;
            this.FolderListingRequested = site.FolderListing;
        }
    }
}
