using System;

namespace RemoteFile.Files
{
    public class RemoteFile
    {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public UInt64? Size { get; private set; }
        public DateTime? CreationDate { get; private set; }

        protected internal RemoteFile(string fullPath, string name = "", ulong? size = null, DateTime? creationDate = null)
        {
            this.FullPath = fullPath;
            this.Name = name;
            this.Size = size;
            this.CreationDate = creationDate;
        }

        public override string ToString()
        {
            return $"{this.FullPath}";
        }
    }
}
