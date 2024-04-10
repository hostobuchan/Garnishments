using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public class ImagesFile : BaseFile
    {
        private List<Records.ImagesRecord> _ImageRecords { get; set; }

        /// <summary>
        /// ID of Batch
        /// <para>Not Used for Images File</para>
        /// </summary>
        public override int? BatchID { get { return null; } }
        /// <summary>
        /// ID of Company
        /// <para>Not Used for Images File</para>
        /// </summary>
        public override short? CompanyID { get { return null; } }
        /// <summary>
        /// Date Batch Processed
        /// </summary>
        public override DateTime? BatchDate
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=Extract)[\d]{8}(?=[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                    return DateTime.ParseExact(match.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                else
                    return null;
            }
        }
        /// <summary>
        /// File Containing Listing of Signature Images
        /// </summary>
        public Zip.ZipArchive.ZipFileInfo RecordFile
        {
            get
            {
                return this.Files.FirstOrDefault(el => System.Text.RegularExpressions.Regex.IsMatch(el.Name, @"Extract[\d]{8}[\.]txt", System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            }
        }
        /// <summary>
        /// Listing of Signature Images
        /// </summary>
        public List<Records.ImagesRecord> ImageRecords
        {
            get
            {
                if (this._ImageRecords == null)
                {
                    this._ImageRecords = new List<Records.ImagesRecord>();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(this.RecordFile.GetStream(System.IO.FileMode.Open, System.IO.FileAccess.Read)))
                    {
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            this._ImageRecords.Add(new Records.ImagesRecord(reader.ReadLine()));
                        }
                    }
                }
                return this._ImageRecords;
            }
        }

        public ImagesFile(FileType FileType) : base(FileType) { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
