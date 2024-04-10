using System;

namespace RemoteFile.Connections.Methods.FTP
{
    struct FtpLineResult
    {
        public Enums.FtpListStyle Style;
        public string Name;
        public DateTime DateTime;
        public bool IsDirectory;
        public long Size;

        public static explicit operator Files.RemoteFile(FtpLineResult result)
        {
            return new Files.RemoteFile(result.Name,
                result.Name.IndexOf(System.IO.Path.DirectorySeparatorChar) > 0 ?
                    result.Name.Substring(result.Name.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1)
                    : result.Name.IndexOf(System.IO.Path.AltDirectorySeparatorChar) > 0 ?
                        result.Name.Substring(result.Name.LastIndexOf(System.IO.Path.AltDirectorySeparatorChar) + 1)
                        : result.Name,
                (ulong)result.Size,
                result.DateTime);
        }
    }
}
