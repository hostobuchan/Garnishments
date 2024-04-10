using RecordTypes.CLS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes.RMS.Base
{
    public abstract class Record : EDI.EDIRecords.RecordType<Record>
    {
        #region EDI.EDIRecords.RecordType<Record>
        public abstract void AddHeaders(List<Record> BaseList, List<Record> AddList);
        public void BasicFileMaintenance(List<Record> Records)
        {
            return;
        }
        public List<Record> GetAccountRecords(Account Account, List<Record> Records)
        {
            return Records.OfType<AccountInfo>().Where(el => el.AccountNumber.Value == Account.Forw_FileNo).ToList<Record>();
        }
        public abstract Record GetRecordType(string Record);
        public int NewAccounts(List<Record> Records)
        {
            return Records.OfType<AccountInfo>().GroupBy(el => el.AccountNumber.Value).Count();
        }
        public abstract Record PlacementRecord(List<Record> Records, string AccountNumber);
        public IEnumerable<List<Record>> UniqueAccountListing(List<Record> Records)
        {
            foreach (AccountInfo R in Records.OfType<AccountInfo>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
            {
                yield return Records.OfType<AccountInfo>().Where(el => el.AccountNumber.Value == R.AccountNumber.Value).ToList<Record>();
            }
        }
        #endregion

        public class RMSEnumerator : IEnumerator
        {
            public List<Record> RecordList;
            private int position = -1;

            public RMSEnumerator(List<Record> RecordList)
            {
                this.RecordList = RecordList;
            }

            private IEnumerator getEnumerator()
            {
                return (IEnumerator)this;
            }

            public bool MoveNext()
            {
                this.position++;
                return (this.position < this.RecordList.Count);
            }

            public void Reset() { this.position = -1; }

            public object Current { get { return this.RecordList[position]; } }
        }
    }
}
