using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Interfaces
{
    public interface IBatchHandler<T, K> where T : Records.UploadRecord where K : Records.DownloadRecord
    {
        int GetBatchID();
        Task<int> GetBatchIDAsync();
        IEnumerable<T> GenerateBatchUploadRecords(IEnumerable<Accounts.EvaluateeAccount> accounts);
        Task<IEnumerable<T>> GenerateBatchUploadRecordsAsync(IEnumerable<Accounts.EvaluateeAccount> accounts);

        bool LoadBatchInfo(System.IO.FileInfo file);
        Task<bool> LoadBatchInfoAsync(System.IO.FileInfo file);
    }

    public static partial class Extensions
    {
        public static bool LoadBatchInfo<T, K>(this IBatchHandler<T, K> handler, string fileName) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            return handler.LoadBatchInfo(new System.IO.FileInfo(fileName));
        }
        public static async Task<bool> LoadBatchInfoAsync<T, K>(this IBatchHandler<T, K> handler, string fileName) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            return await handler.LoadBatchInfoAsync(new System.IO.FileInfo(fileName));
        }
    }
}
