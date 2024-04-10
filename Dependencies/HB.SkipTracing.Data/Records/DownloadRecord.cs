using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Records
{
    public abstract class DownloadRecord
    {        
        public abstract string Vendor { get; }
        public abstract Enums.ImportSettings ImportSettings { get; }
        public string FileNo { get; private set; }
        public byte DebtorNo { get; private set; }
        public string UploadAccountNo { get { return $"{this.FileNo}-{this.DebtorNo}"; } set { var acct = value.Split('-'); this.FileNo = acct[0]; this.DebtorNo = acct.GetUpperBound(0) > 0 ? Convert.ToByte(acct[acct.GetUpperBound(0)]) : (byte)1; } }

        public Enums.AssetType AssetType { get; protected set; }
        public string Name { get; protected set; }
        public string Contact { get; protected set; }
        public string Address1 { get; protected set; }
        public string Address2 { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string Zip { get; protected set; }
        public string Phone { get; protected set; }
        public string AsPhone { get; protected set; }
        public string AdditionalInfo { get; protected set; }


        #region Internal DB IDs
        public int AID { get; set; }
        public int ASID { get; set; }
        #endregion
    }
}
