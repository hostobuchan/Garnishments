using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public class ReceiptFile : BaseFile
    {
        /// <summary>
        /// ID of Batch
        /// </summary>
        public override int? BatchID
        {
            get
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_[\d]{4}_[\d]{14}_Mailed_ID)[\d]{4}(?=_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
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
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_)[\d]{4}(?=_[\d]{14}_Mailed_ID[\d]{4}_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
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
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(this.FileName, @"(?<=WALZ_[\d]{4}_)[\d]{14}(?=_Mailed_ID[\d]{4}_[\d]+[\.]zip)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                    return DateTime.ParseExact(match.Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                else
                    return null;
            }
        }

        public ReceiptFile(FileType FileType) : base(FileType) { }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
