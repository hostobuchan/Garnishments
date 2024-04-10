using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public class BarcodesFile : BaseFile
    {
        private List<Records.BarcodesRecord> _BarcodeRecords { get; set; }

        /// <summary>
        /// ID of Batch
        /// </summary>
        public override int? BatchID
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_[\d]{4}_AL_[\d]{14}ID)[\d]{4}(?=_3m_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                    return Convert.ToInt32(match.Value);
                else
                    return null;
            }
        }
        /// <summary>
        /// ID of Company
        /// </summary>
        public override short? CompanyID
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_)[\d]{4}(?=_AL_[\d]{14}ID[\d]{4}_3m_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                    return Convert.ToInt16(match.Value);
                else
                    return null;
            }
        }
        /// <summary>
        /// Date Batch Processed
        /// </summary>
        public override DateTime? BatchDate
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_[\d]{4}_AL_)[\d]{14}(?=ID[\d]{4}_3m_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                    return DateTime.ParseExact(match.Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                else
                    return null;
            }
        }
        /// <summary>
        /// File Version used for upload
        /// </summary>
        public Enums.FileVersion FileVersion
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_[\d]{4}_AL_([\d]{14}?)ID[\d]{4}_)[a-zA-Z0-9]{1,3}(?=_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    try
                    {
                        return Dictionaries.FileVersionStrings.FirstOrDefault(el => el.Value == match.Value).Key;
                    }
                    catch { return Enums.FileVersion.Unknown; }
                }
                else
                    return Enums.FileVersion.Unknown;
            }
        }
        /// <summary>
        /// File Containing Listing of Barcode Images
        /// </summary>
        public Zip.ZipArchive.ZipFileInfo RecordFile
        {
            get
            {
                return this.Files.FirstOrDefault(el => System.Text.RegularExpressions.Regex.IsMatch(el.Name, @"WALZ_[\d]{4}_AL_[\d]{14}ID[\d]{4}_[a-zA-Z0-9]{1,3}_[\d]+[\.]txt", System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            }
        }
        /// <summary>
        /// Listing of Barcode Images
        /// </summary>
        public List<Records.BarcodesRecord> BarcodeRecords
        {
            get
            {
                if (this._BarcodeRecords == null)
                {
                    this._BarcodeRecords = new List<Records.BarcodesRecord>();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(this.RecordFile.GetStream(System.IO.FileMode.Open, System.IO.FileAccess.Read)))
                    {
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            this._BarcodeRecords.Add(new Records.BarcodesRecord(reader.ReadLine(), this.FileVersion));
                        }
                    }
                }
                return this._BarcodeRecords;
            }
        }

        public BarcodesFile(FileType FileType) : base(FileType) { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
