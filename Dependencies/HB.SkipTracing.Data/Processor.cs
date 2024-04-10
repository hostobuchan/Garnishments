using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data
{
    public class Processor
    {
        /// <summary>
        /// Create Upload File
        /// </summary>
        /// <param name="fileNos">List of Accounts</param>
        /// <param name="file">Upload File Save Location</param>
        /// <returns></returns>
        public static async Task RunUploadRoutine(IEnumerable<string> fileNos, Enums.AssetType searchType, System.IO.FileInfo file, System.IO.DirectoryInfo outputDir, Dictionary<Enums.AssetType, IVendorSettings<Interfaces.IDataHandler<Records.UploadRecord, Records.DownloadRecord>, Records.UploadRecord, Records.DownloadRecord>> settings)
        {
            var batchHandler = settings[searchType].BatchHandler;
            var dataHandler = settings[searchType].DataHandler;
            var fileReader = settings[searchType].FileReader;

            var accts = await Accounts.DataHandler.GetAccountsAsync(fileNos.GroupBy(f => f).Select(f => f.Key).ToList());

            if (accts != null && accts.Count > 0)
            {
                // Rebuild Debtor Array Only Including Those With Valid SSN
                accts.ForEach(a => a.Debtors = a.Debtors.Where(d => !string.IsNullOrEmpty(d.SSN) && System.Text.RegularExpressions.Regex.IsMatch(d.SSN, @"[\d]{3}\-[\d]{2}\-[\d]{4}")).ToArray());

                var uploadRecords = await batchHandler.GenerateBatchUploadRecordsAsync(accts);

                await dataHandler.SaveUploadRecordsAsync(uploadRecords, await batchHandler.GetBatchIDAsync());

                await fileReader.CreateUploadFileAsync(file, uploadRecords);

                #region Create Update Files
                var notes = await dataHandler.GenerateUploadNotesForBatchAsync(uploadRecords, searchType);
                var merges = await dataHandler.GenerateUploadMergesForBatchAsync(uploadRecords, searchType);
                #endregion

                #region Export Update Files
                if (notes.Count() > 0)
                {
                    RecordTypes.Output.Send_YGC_Imp(notes);
                }
                if (merges.Count() > 0)
                {
                    using (RecordTypes.MergePops.FileWriter sw = new RecordTypes.MergePops.FileWriter(System.IO.Path.Combine(outputDir.FullName, $"Merges_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                    {
                        sw.WriteFile(merges);
                    }
                }
                #endregion
            }
            else
            {
                throw new InvalidOperationException("No Accounts Found");
            }
        }

        public static async Task RunDownloadRoutine(
            IEnumerable<System.IO.FileInfo> files,
            System.IO.DirectoryInfo outputDir,
            Dictionary<Enums.AssetType, 
                IVendorSettings<
                    Interfaces.IDataHandler<
                        Records.UploadRecord,
                        Records.DownloadRecord>
                    ,
                    Records.UploadRecord,
                    Records.DownloadRecord>
                > settings,
            Func<Batches.BatchInfo> requestBatchInfo,
            Func<Assets.AccountAssetInfo<Records.DownloadRecord>, Enums.AssetResultReason, bool> funcRequestAddAsset,
            Func<Assets.AccountAssetInfo<Records.DownloadRecord>, Enums.GarnishmentResultReason, bool> funcRequestGarn,
            Func<Exception, System.Net.NetworkCredential> failedLogin)
        {
            Enums.AssetType searchType = Enums.AssetType.Unknown;
            var batchHandler = settings.First().Value.BatchHandler;

            #region Get Batch Information
            int batchID = 0;
            if (await batchHandler.LoadBatchInfoAsync(files.FirstOrDefault()))
            {
                batchID = batchHandler.CurrentID.Value;
                searchType = batchHandler.SearchType;
            }
            else
            {
                // Request Batch - Unable to Detect
                if (requestBatchInfo == null)
                    throw new InvalidOperationException("Unable to Acquire Batch Info");
                else
                {
                    var batchInfo = requestBatchInfo();
                    batchID = batchInfo.ID;
                    searchType = batchInfo.SearchType;
                }
            }

            if (batchID == 0) throw new InvalidOperationException("Batch ID - Could Not Be Determined - Unable to Continue");
            #endregion

            // Load Registered Agents For Bank Batches 
            if (batchHandler.SearchType.HasFlag(Enums.AssetType.Bank))
            {
                Garnishments.UI.Settings.Properties.RegisteredAgents = await Garnishments.Data.DataHandler.GetRegisteredAgentsAsync();
            }

            // Re-Initiate Data Handlers with Search Type
            batchHandler = settings[searchType].BatchHandler;
            var dataHandler = settings[searchType].DataHandler;
            var fileReader = settings[searchType].FileReader;

            // Read Download Records
            var results = files.SelectMany(f => fileReader.ReadReturnFile(f)).ToList();

            // Find Account in CLS
            var evalAccounts = await Data.Accounts.DataHandler.GetAccountsAsync(results.GroupBy(res => res.FileNo).Select(g => g.Key).ToList());

            // Create Listing of Valid and Invalid Results
            List<Records.DownloadRecord> validResults = new List<Records.DownloadRecord>();
            List<Records.DownloadRecord> invalidResults = new List<Records.DownloadRecord>();
            foreach (var result in results)
            {
                if (evalAccounts?.Count(ev => ev.FileNo.Equals(result.FileNo, StringComparison.OrdinalIgnoreCase)) > 0)
                {
                    validResults.Add(result);
                }
                else
                {
                    invalidResults.Add(result);
                }
            }

            #region Create Update Files
            var ygcUpdates = (await dataHandler.GenerateYGCRecordsForBatchAsync(validResults)).ToArray();
            var merges = (await dataHandler.GenerateMergesForBatchAsync(validResults)).ToArray();
            #endregion

            

            #region Export Update Files
            if (ygcUpdates.Count() > 0)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(outputDir.FullName, $"YGC_Updates_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    foreach (var rec in ygcUpdates)
                    {
                        await sw.WriteLineAsync(rec.ToString());
                    }
                    sw.Flush();
                }
            }
            if (merges.Count() > 0)
            {
                using (RecordTypes.MergePops.FileWriter sw = new RecordTypes.MergePops.FileWriter(System.IO.Path.Combine(outputDir.FullName, $"Merges_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    sw.WriteFile(merges);
                }
            }
            #endregion

            await dataHandler.SaveDownloadRecordsAsync(results.Where(res => res.ImportSettings.HasFlag(Enums.ImportSettings.Import)), batchID); // Only run this process where we have a good indicator from the vendor
            var garnResults = await dataHandler.RequestGarnishmentsForBatchAsync(
                results.Where(res => res.ImportSettings.HasFlag(Enums.ImportSettings.AddAsset)), // Only run this process for results where we want to import to asset/garn panel
                funcRequestAddAsset, // Need to Generate UI
                funcRequestGarn // Need to Generate UI
                );
            await dataHandler.UpdateGarnIDAsync(garnResults, batchID);

            // Send Email for Manual Verification Items
            try
            {
                DataHandler<Records.UploadRecord, Records.DownloadRecord>.SendManualSkipEmail(validResults, ex => failedLogin?.Invoke(ex));
            }
            catch { }

            // Generate Addition Report for Invalid Results?  Or Allow Garnishment Request to Handle on its Own?
            //
            //
        }
    }
}
