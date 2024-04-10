using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.SkipTracing.Data.Accounts;
using HB.SkipTracing.Data.Interfaces;
using HB.SkipTracing.Data.Records;
using HB.SkipTracing.Data.RNN.Records;

namespace HB.SkipTracing.Data.RNN.Batches
{
    public class BatchHandler : Data.Batches.BatchHandler<Data.Records.UploadRecord, Data.Records.DownloadRecord>
    {
        private string _UploadFileName;
        public override string UploadFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_UploadFileName)) { _UploadFileName = $"RNN_{DateTime.Today:yyyy-MM-dd}"; }
                return _UploadFileName;
            }

            protected set
            {
                _UploadFileName = value;
            }
        }

        public BatchHandler(Enums.AssetType searchType) : base(2, searchType) { }

        public override IEnumerable<Data.Records.UploadRecord> GenerateBatchUploadRecords(IEnumerable<EvaluateeAccount> accounts)
        {
            return accounts.SelectMany(acct => acct.Debtors.Select(deb => new Records.UploadRecord(this, acct, deb)));
        }
        public override async Task<IEnumerable<Data.Records.UploadRecord>> GenerateBatchUploadRecordsAsync(IEnumerable<EvaluateeAccount> accounts)
        {
            return await Task.Run(() => GenerateBatchUploadRecords(accounts));
        }
    }
}
