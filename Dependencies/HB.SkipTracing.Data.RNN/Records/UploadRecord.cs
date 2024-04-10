using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.RNN.Records
{
    public class UploadRecord : Data.Records.UploadRecord
    {
        private Func<int> BatchNumber { get; set; }

        public string SSN { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        /// <summary>
        /// Account Number
        /// </summary>
        public string ClientField1 { get { return this.UploadAccountNo; } }
        public string ClientField2 { get; set; }
        public string ClientField3 { get; set; }
        public string ClientField4 { get; set; }
        public string ClientField5 { get; set; }
        public DateTime? OpenDate { get; set; }
        public string RNN_ID { get; set; }

        public UploadRecord(Data.Interfaces.IBatchHandler<Data.Records.UploadRecord, Data.Records.DownloadRecord> batchManager, Accounts.EvaluateeAccount account, Accounts.EvaluateeDebtor debtor) : base(account, debtor)
        {
            this.BatchNumber = batchManager.GetBatchID;
        }
        protected UploadRecord(
            string fileNo,
            byte debtor,
            string ssn,
            string fName,
            string lName,
            string add1,
            string add2,
            string city,
            string state,
            string zip,
            string p1,
            string p2,
            string p3,
            string cf2 = null,
            string cf3 = null,
            string cf4 = null,
            string cf5 = null,
            DateTime? openDate = null,
            string rnnId = null
            ) : base(fileNo, debtor)
        {
            this.SSN = ssn;
            this.First_Name = fName;
            this.Last_Name = lName;
            this.Address1 = add1;
            this.Address2 = add2;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Phone1 = p1.NumbersOnly();
            this.Phone2 = p2.NumbersOnly();
            this.Phone3 = p3.NumbersOnly();
            this.ClientField2 = cf2;
            this.ClientField3 = cf3;
            this.ClientField4 = cf4;
            this.ClientField5 = cf5;
            this.OpenDate = openDate;
            this.RNN_ID = rnnId;
        }

        protected override void Initialize(Accounts.EvaluateeAccount account, Accounts.EvaluateeDebtor debtor)
        {
            this.SSN = debtor.SSN;
            this.First_Name = debtor.NameFirst;
            this.Last_Name = debtor.NameLast;
            this.Address1 = debtor.Address1;
            this.Address2 = debtor.Address2;
            this.City = debtor.City;
            this.State = debtor.State;
            this.Zip = debtor.Zip;
            this.Date_Of_Birth = debtor.DOB;
            this.Phone1 = debtor.Phone.NumbersOnly();
            this.Phone2 = debtor.Phone2.NumbersOnly();
            this.Phone3 = debtor.Fax.NumbersOnly();
            this.ClientField2 = account.Balance.ToString("N2");
            // Client Field 3
            // Client Field 4
            // Client Field 5
            this.OpenDate = account.OpenedDate;
            // RNN ID      
        }
    }
}
