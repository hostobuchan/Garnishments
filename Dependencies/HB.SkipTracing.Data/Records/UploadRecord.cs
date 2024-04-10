using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HB.SkipTracing.Data.Records
{
    public abstract class UploadRecord
    {
        public string FileNo { get; set; }
        public byte DebtorNo { get; set; }
        public string UploadAccountNo { get { return $"{this.FileNo}-{this.DebtorNo}"; } }

        internal UploadRecord() { }
        protected UploadRecord(Accounts.EvaluateeAccount account, Accounts.EvaluateeDebtor debtor)
        {
            this.FileNo = account.FileNo;
            this.DebtorNo = (byte)debtor.Debtor;
            Initialize(account, debtor);
        }
        protected UploadRecord(string fileNo, byte debtor)
        {
            this.FileNo = fileNo;
            this.DebtorNo = debtor;
        }

        protected abstract void Initialize(Accounts.EvaluateeAccount account, Accounts.EvaluateeDebtor debtor);
    }
}
