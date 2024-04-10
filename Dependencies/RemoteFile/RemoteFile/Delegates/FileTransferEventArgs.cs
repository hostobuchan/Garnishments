using System;

namespace RemoteFile.Delegates
{
    public class FileTransferEventArgs : EventArgs
    {
        public Interfaces.SiteConnectionInterface Connection { get; private set; }
        public string Source { get; private set; }
        public string Destination { get; private set; }
        public UInt64 Transferred { get; private set; }
        public UInt64 Total { get; private set; }
        public int? PercentComplete { get { return this.Total == 0 ? (int?)null : (int?)(this.Transferred / this.Total); } }

        public FileTransferEventArgs(Interfaces.SiteConnectionInterface connection, string source = "", string destination = "", UInt64 transferred = 0, UInt64 total = 0)
        {
            this.Connection = connection;
            this.Source = source;
            this.Destination = destination;
            this.Transferred = transferred;
            this.Total = total;
        }
    }
}
