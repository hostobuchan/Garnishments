using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public abstract class BaseFile : FileType, IDisposable
    {
        protected Zip.ZipArchive Archive { get; set; }

        public abstract int? BatchID { get; }
        public abstract short? CompanyID { get; }
        public abstract DateTime? BatchDate { get; }
        /// <summary>
        /// Enumeration of Files Contained
        /// </summary>
        public IEnumerable<Zip.ZipArchive.ZipFileInfo> Files
        {
            get
            {
                if (this.Archive == null) this.Archive = Zip.ZipArchive.OpenOnFile(this.FileName);
                foreach (Zip.ZipArchive.ZipFileInfo zf in this.Archive.Files)
                {
                    yield return zf;
                }
            }
        }
        /// <summary>
        /// Enumeration of PDF Files Contained
        /// </summary>
        public IEnumerable<Zip.ZipArchive.ZipFileInfo> ImageFiles
        {
            get
            {
                return this.Files.Where(el => el.Name.EndsWith(".PDF", StringComparison.OrdinalIgnoreCase));
            }
        }

        public BaseFile(FileType FileType) : base(FileType) { }

        public void OpenArchiveOnStream(System.IO.Stream stream)
        {
            this.Archive = Zip.ZipArchive.OpenOnStream(stream, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Archive.Dispose();
        }

        #endregion
    }
}
