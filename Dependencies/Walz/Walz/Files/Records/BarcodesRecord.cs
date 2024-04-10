using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Records
{
    public class BarcodesRecord : UploadRecord, IFormattable
    {
        #region Private Properties
        private int _TrackingNumber { get; set; }
        private int _StartPage { get; set; }
        private int _PageCount { get; set; }
        #endregion

        #region Public Properties
        public string TrackingNumber { get { return this.Segments[_TrackingNumber]; } set { this.Segments[_TrackingNumber] = value; } }
        public int? StartPage { get { try { return Convert.ToInt32(this.Segments[_StartPage]); } catch { return null; } } set { this.Segments[_StartPage] = value.HasValue ? value.Value.ToString() : ""; } }
        public int? PageCount { get { try { return Convert.ToInt32(this.Segments[_PageCount]); } catch { return null; } } set { this.Segments[_PageCount] = value.HasValue ? value.Value.ToString() : ""; } }
        #endregion

        public BarcodesRecord(string Record, Enums.FileVersion fileVersion) : base(Record, fileVersion)
        {
            switch (fileVersion)
            {
                case Enums.FileVersion.FileVersion3:
                case Enums.FileVersion.FileVersion3m:
                    this._TrackingNumber = 17;
                    this._StartPage = 18;
                    this._PageCount = 19;
                    break;
                case Enums.FileVersion.FileVersion3r:
                case Enums.FileVersion.FileVersion3rm:
                    this._TrackingNumber = 23;
                    this._StartPage = 24;
                    this._PageCount = 25;
                    break;
                case Enums.FileVersion.FileVersion1:
                case Enums.FileVersion.FileVersion2:
                case Enums.FileVersion.FileVersion2m:
                case Enums.FileVersion.FileVersion2r:
                case Enums.FileVersion.FileVersion2rm:
                default:
                    throw new NotImplementedException("The Selected Format is Not Implemented");
            }
        }

        #region IFormattable Members

        public new string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "3":
                case "3r":
                case "3m":
                case "3rm":
                    return string.Format("{0}\t{1}\t{2}\t{3}",
                        base.ToString(format),
                        this.TrackingNumber,
                        this.StartPage.HasValue ? this.StartPage.Value.ToString() : "",
                        this.PageCount.HasValue ? this.PageCount.Value.ToString() : ""
                        );
                case "1":
                case "2":
                case "2r":
                case "2m":
                case "2rm":
                default:
                    throw new NotImplementedException(string.Format("Selected Format Not Supported \"{0}\"", format));
            }
        }

        public new string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public new string ToString(Enums.FileVersion fileVersion)
        {
            return ToString(Dictionaries.FileVersionStrings[fileVersion]);
        }

        public override string ToString()
        {
            return ToString("3m");
        }

        #endregion
    }
}
