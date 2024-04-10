using System.Collections.Generic;

namespace RemoteFile.Delegates
{
    /// <summary>
    /// Get Remote Directory File Listing
    /// </summary>
    /// <param name="remoteFolder">Directory</param>
    /// <returns>File Listing</returns>
    public delegate IEnumerable<Files.RemoteFile> FileEnumeratorEventHandler(Files.RemoteFolder remoteFolder);
}
