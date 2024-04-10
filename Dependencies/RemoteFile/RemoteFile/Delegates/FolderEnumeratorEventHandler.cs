using System.Collections.Generic;

namespace RemoteFile.Delegates
{
    /// <summary>
    /// Get Remote Directory Folder Listing
    /// </summary>
    /// <param name="remoteFolder">Directory</param>
    /// <returns>Folder Listing</returns>
    public delegate IEnumerable<Files.RemoteFolder> FolderEnumeratorEventHandler(Files.RemoteFolder remoteFolder);
}
