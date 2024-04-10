using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Interfaces
{
    public interface IDataHandler<in T, K> where T : Records.UploadRecord where K : Records.DownloadRecord
    {
        string Vendor { get; }
        // Generate Notes for CLS
        IEnumerable<RecordTypes.YGC.Base.YGCBase> GenerateUploadNotesForBatch(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched);
        Task<IEnumerable<RecordTypes.YGC.Base.YGCBase>> GenerateUploadNotesForBatchAsync(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched);
        // Generate Update Codes for Clients
        IEnumerable<RecordTypes.MergePops.MergePop> GenerateUploadMergesForBatch(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched);
        Task<IEnumerable<RecordTypes.MergePops.MergePop>> GenerateUploadMergesForBatchAsync(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched);
        // Save Upload Info to Database
        void SaveUploadRecords(IEnumerable<T> uploads, int batchId);
        Task SaveUploadRecordsAsync(IEnumerable<T> uploads, int batchId);
        // Save Download Info to Database
        void SaveDownloadRecords(IEnumerable<K> downloads, int batchId);
        Task SaveDownloadRecordsAsync(IEnumerable<K> downloads, int batchId);
        // Save Garn ID to Download Info in Database
        void UpdateGarnID(List<Reporting.AssetResult<K>> garnResults, int batchId);
        Task UpdateGarnIDAsync(List<Reporting.AssetResult<K>> garnResults, int batchId);

        // Generate Merge Records for Intake
        IEnumerable<RecordTypes.MergePops.MergePop> GenerateMergesForBatch(IEnumerable<K> downloads);
        Task<IEnumerable<RecordTypes.MergePops.MergePop>> GenerateMergesForBatchAsync(IEnumerable<K> downloads);
        // Generate YGC Update Records for Intake
        IEnumerable<RecordTypes.YGC.Base.YGCBase> GenerateYGCRecordsForBatch(IEnumerable<K> downloads);
        Task<IEnumerable<RecordTypes.YGC.Base.YGCBase>> GenerateYGCRecordsForBatchAsync(IEnumerable<K> downloads);

        // Save Into Asset & Garnishment System
        List<Reporting.AssetResult<K>> RequestGarnishmentsForBatch(IEnumerable<K> downloads, Func<Assets.AccountAssetInfo<K>, Enums.AssetResultReason, bool> funcAddAsset = null, Func<Assets.AccountAssetInfo<K>, Enums.GarnishmentResultReason, bool> funcRequestGarn = null);
        Task<List<Reporting.AssetResult<K>>> RequestGarnishmentsForBatchAsync(IEnumerable<K> downloads, Func<Assets.AccountAssetInfo<K>, Enums.AssetResultReason, bool> funcAddAsset = null, Func<Assets.AccountAssetInfo<K>, Enums.GarnishmentResultReason, bool> funcRequestGarn = null);
    }
}
