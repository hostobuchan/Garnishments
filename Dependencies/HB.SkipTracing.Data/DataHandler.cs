using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.SkipTracing.Data.Accounts;
using RecordTypes.MergePops;
using RecordTypes.YGC.Base;
using System.Data.SqlClient;
using System.Data;

namespace HB.SkipTracing.Data
{
    public abstract class DataHandler<T, K> : Interfaces.IDataHandler<T, K> where T : Records.UploadRecord where K : Records.DownloadRecord
    {
        public abstract string Vendor { get; }

        public virtual IEnumerable<RecordTypes.YGC.Base.YGCBase> GenerateUploadNotesForBatch(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched)
        {
            foreach (var upload in uploads)
            {
                yield return new RecordTypes2.YGC.RecordType09()
                {
                    MASCO_FILE = upload.FileNo,
                    PCMT = $"Claim sent to {this.Vendor} for {assetTypeSearched} search"
                };
            }
        }
        public virtual async Task<IEnumerable<RecordTypes.YGC.Base.YGCBase>> GenerateUploadNotesForBatchAsync(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched)
        {
            return await Task.Run(() => GenerateUploadNotesForBatch(uploads, assetTypeSearched).ToArray());
        }

        public virtual IEnumerable<MergePop> GenerateUploadMergesForBatch(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched)
        {
            if (assetTypeSearched.HasFlag(Enums.AssetType.Employer))
            {
                foreach (var upload in uploads)
                {
                    yield return new MergePop()
                    {
                        FILENO = upload.FileNo,
                        LLCODE = "XPOESKIP"
                    };
                }
            }
            if (assetTypeSearched.HasFlag(Enums.AssetType.Bank))
            {
                foreach (var upload in uploads)
                {
                    yield return new MergePop()
                    {
                        FILENO = upload.FileNo,
                        LLCODE = "XBNKSKIP"
                    };
                }
            }
        }
        public virtual async Task<IEnumerable<MergePop>> GenerateUploadMergesForBatchAsync(IEnumerable<T> uploads, Enums.AssetType assetTypeSearched)
        {
            return await Task.Run(() => GenerateUploadMergesForBatch(uploads, assetTypeSearched).ToArray());
        }


        public virtual IEnumerable<MergePop> GenerateMergesForBatch(IEnumerable<K> downloads)
        {
            foreach (var rec in downloads)
            {
                if (rec.AssetType == Enums.AssetType.Employer)
                {
                    yield return new MergePop()
                    {
                        FILENO = rec.FileNo,
                        LLCODE = "X12P"
                    };
                }
                else if (rec.AssetType == Enums.AssetType.Bank)
                {
                    yield return new MergePop()
                    {
                        FILENO = rec.FileNo,
                        LLCODE = "XBANKVER"
                    };
                }
            }
        }
        public virtual async Task<IEnumerable<MergePop>> GenerateMergesForBatchAsync(IEnumerable<K> downloads)
        {
            return await Task.Run(() => GenerateMergesForBatch(downloads));
        }

        public virtual IEnumerable<YGCBase> GenerateYGCRecordsForBatch(IEnumerable<K> downloads)
        {
            foreach (var rec in downloads)
            {
                if (rec.ImportSettings.HasFlag(Enums.ImportSettings.Import))
                {
                    yield return new RecordTypes2.YGC.RecordType09()
                    {
                        MASCO_FILE = rec.FileNo,
                        PDATE = DateTime.Today,
                        PCMT = $"{(rec.ImportSettings.HasFlag(Enums.ImportSettings.ManuallyVerify) ? "Unverified" : "Verified")} {(rec.AssetType == Enums.AssetType.Bank ? "Bank" : "POE")} info received from {rec.Vendor}",
                        NOTE02 = $"Debtor {rec.DebtorNo}",
                        NOTE03 = rec.Name,
                        NOTE04 = rec.Address1,
                        NOTE05 = string.IsNullOrWhiteSpace(rec.Address2) ? $"{rec.City}, {rec.State} {rec.Zip}" : rec.Address2,
                        NOTE06 = string.IsNullOrWhiteSpace(rec.Address2) ? rec.Phone : $"{rec.City}, {rec.State} {rec.Zip}",
                        NOTE07 = string.IsNullOrWhiteSpace(rec.Address2) ? "" : rec.Phone
                    };
                }
            }
        }
        public virtual async Task<IEnumerable<YGCBase>> GenerateYGCRecordsForBatchAsync(IEnumerable<K> downloads)
        {
            return await Task.Run(() => GenerateYGCRecordsForBatch(downloads).ToArray());
        }

        #region Assets & Garnishments DB
        public List<Reporting.AssetResult<K>> RequestGarnishmentsForBatch(IEnumerable<K> downloads, Func<Assets.AccountAssetInfo<K>, Enums.AssetResultReason, bool> funcAddAsset = null, Func<Assets.AccountAssetInfo<K>, Enums.GarnishmentResultReason, bool> funcRequestGarn = null)
        {
            return Task.Run(async () => await RequestGarnishmentsForBatchAsync(downloads, funcAddAsset, funcRequestGarn)).Result;
        }
        public async Task<List<Reporting.AssetResult<K>>> RequestGarnishmentsForBatchAsync(IEnumerable<K> downloads, Func<Assets.AccountAssetInfo<K>, Enums.AssetResultReason, bool> funcAddAsset = null, Func<Assets.AccountAssetInfo<K>, Enums.GarnishmentResultReason, bool> funcRequestGarn = null)
        {
            // Load Criteria Handler
            Garnishments.Data.CriteriaSets.CriteriaDataHandler criteria = await Garnishments.Data.CriteriaSets.CriteriaDataHandler.CreateCriteriaDataHandlerAsync();
            // Load Evaluatees for Accounts
            var evals = (await Garnishments.Data.Accounts.EvaluateeDataHandler.GetDataHandlerAsync(downloads.Select(down => down.FileNo)))?.Accounts;
            List<K> validDownloads = new List<K>();
            List<K> invalidDownloads = new List<K>();

            // Check for Missing Account Numbers
            if (evals == null || evals?.Count() != downloads.Count())
            {
                foreach (var d in downloads)
                {
                    if (evals?.Count(ev => ev.FileNo.Equals(d.FileNo, StringComparison.OrdinalIgnoreCase)) > 0)
                    {
                        validDownloads.Add(d);
                    }
                    else
                    {
                        invalidDownloads.Add(d);
                    }
                }
            }
            else
            {
                validDownloads.AddRange(downloads);
            }

            var loaded = validDownloads.AsParallel().Select(async down =>
            {
                var acctAssets = await Garnishments.Data.DataHandler.GetAccountAssetsAsync(down.FileNo);
                return new Assets.AccountAssetInfo<K>(down, acctAssets);
            }).Select(q => q.Result).ToArray();

            // Create Listing of Results for Report
            List<Reporting.AssetResult<K>> results = new List<Reporting.AssetResult<K>>();

            // Create YGC Records for Export
            List<RecordTypes.YGC.Base.YGCBase> Diaries = new List<YGCBase>();

            // Cycle through all records, creating an accounting of actions taken
            foreach (var assetRecord in loaded)
            {
                Reporting.AssetResult<K> result = new Reporting.AssetResult<K>(assetRecord.DownloadRecord, assetRecord);
                Garnishments.Data.Assets.AccountAsset newAsset = null;
                int requestId = 0;
                results.Add(result);

                if (assetRecord.IsNew)
                {
                    // New Asset - Proceed
                    result.AssetResultReason = Enums.AssetResultReason.NewInfo;

                    newAsset = await AddAsset(assetRecord, result);
                    if (newAsset == null) continue;

                    if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                    {
                        // Evaluate Account
                        if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), newAsset, result))
                        {
                            // Request Garn
                            requestId = await RequestGarn(newAsset.CurrentInfo, result);
                            // Add Codes
                            Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                        }
                    }
                }
                else
                {
                    // Existing Asset

                    if (assetRecord.IsGood)
                    {
                        // We Have a Confirmed Good Asset - Why Would We Change It?
                        result.AssetResultReason = Enums.AssetResultReason.PreviousInfoKnownGood;
                    }
                    else
                    {
                        result.AssetResultReason = Enums.AssetResultReason.AlreadyExists;
                    }


                    if (assetRecord.HasGarn)
                    {
                        // Has Previous Garn

                        if (assetRecord.HasActiveGarn)
                        {
                            // That Garn Is Currently Happening
                            result.GarnishmentResultReason = Enums.GarnishmentResultReason.PreviousGarnInPlace;
                            // DO NOT ALTER ASSET DB
                            result.Description += "Previously Known Info - Possible New Address - Garn Currently in Place";
                        }
                        else
                        {
                            // Not Currently Garned
                            if (assetRecord.GoodGarnResult)
                            {
                                // That Garn Was Good
                                result.GarnishmentResultReason = Enums.GarnishmentResultReason.PreviousGarnSuccessful;
                                // WHY ARE WE HERE!?!
                                if (funcAddAsset?.Invoke(assetRecord, Enums.AssetResultReason.PreviousInfoKnownGood) ?? false)
                                {
                                    // Add it Anyway - Some Dunce Says to Do It!
                                    newAsset = await AddAsset(assetRecord, result);
                                    if (newAsset == null) continue;

                                    if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                                    {
                                        if (await TestForUniqueness(assetRecord, newAsset, result))
                                        {
                                            // Evaluate Account
                                            if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), newAsset, result))
                                            {
                                                // Request Garn
                                                requestId = await RequestGarn(newAsset.CurrentInfo, result);
                                                // Add Codes
                                                Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                                    {
                                        if (funcRequestGarn?.Invoke(assetRecord, Enums.GarnishmentResultReason.PreviousGarnSuccessful) ?? false)
                                        {
                                            // Request Garn on Previous Asset

                                            // Evaluate Account
                                            if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), assetRecord.MatchingAsset, result))
                                            {
                                                // Request Garn
                                                requestId = await RequestGarn(assetRecord.MatchingAsset.CurrentInfo, result);
                                                // Add Codes
                                                Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Bad Garn

                                if (result.AssetResultReason != Enums.AssetResultReason.PreviousInfoKnownGood
                                    || (funcAddAsset?.Invoke(assetRecord, result.AssetResultReason) ?? false))
                                {
                                    // Add New Info
                                    newAsset = await AddAsset(assetRecord, result);
                                    if (newAsset == null) continue;

                                    if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                                    {
                                        if (await TestForUniqueness(assetRecord, newAsset, result))
                                        {
                                            // Evaluate Account
                                            if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), newAsset, result))
                                            {
                                                // Request Garn
                                                requestId = await RequestGarn(newAsset.CurrentInfo, result);
                                                // Add Codes
                                                Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // No Previous Garn
                        result.AssetResultReason = Enums.AssetResultReason.NoPreviousGarn;
                        result.GarnishmentResultReason = Enums.GarnishmentResultReason.Unknown;

                        if (funcAddAsset?.Invoke(assetRecord, Enums.AssetResultReason.NoPreviousGarn) ?? false)
                        {
                            // Add it Anyway - Some Dunce Says to Do It!
                            newAsset = await AddAsset(assetRecord, result);
                            if (newAsset == null) continue;

                            if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                            {
                                if (await TestForUniqueness(assetRecord, newAsset, result))
                                {
                                    // Evaluate Account
                                    if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), newAsset, result))
                                    {
                                        // Request Garn
                                        requestId = await RequestGarn(newAsset.CurrentInfo, result);
                                        // Add Codes
                                        Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (result.DownloadRecord.ImportSettings.HasFlag(Enums.ImportSettings.Garn))
                            {
                                if (funcRequestGarn?.Invoke(assetRecord, Enums.GarnishmentResultReason.NoPreviousGarn) ?? false)
                                {
                                    // Request Garn on Previous Asset

                                    // Evaluate Account
                                    if (EvaluateAccountForGarn(criteria, evals.Find(a => string.Equals(a.FileNo, assetRecord.DownloadRecord.FileNo, StringComparison.OrdinalIgnoreCase)), assetRecord.MatchingAsset, result))
                                    {
                                        // Request Garn
                                        requestId = await RequestGarn(assetRecord.MatchingAsset.CurrentInfo, result);
                                        // Add Codes
                                        Diaries.AddRange(GenerateGarnRequestCodes(assetRecord));
                                    }
                                }
                            }
                        }
                    }
                }

                result.NewAsset = newAsset?.CurrentInfo;
                result.GarnishmentID = requestId;
            }

            // Add Error Information for Failures
            foreach (var failure in invalidDownloads)
            {
                results.Add(new Reporting.AssetResult<K>(failure, new Assets.AccountAssetInfo<K>(failure, null))
                {
                    Result = Enums.AssetResultType.Error,
                    AssetResultReason = Enums.AssetResultReason.Error,
                    GarnishmentResultReason = Enums.GarnishmentResultReason.Error,
                    Description = "Account Information Invalid"
                });
            }

            // Export Diaries
            try { RecordTypes.Output.Send_YGC_Imp(Diaries); } catch { }

            // Generate Report
            await Reporting.ReportDataHandler<K>.GenerateReportAsync(results);

            return results;
        }

        // Save Garn ID to Download Info in Database
        public virtual void UpdateGarnID(List<Reporting.AssetResult<K>> garnResults, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                foreach (var result in garnResults)
                {
                    if (result.GarnishmentResultReason.HasFlag(Enums.GarnishmentResultReason.Requested) && result.GarnishmentID > 0)
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("UpdateBatchAssetGarn", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                            {
                                cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                                cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = result.DownloadRecord.DebtorNo });
                                cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.Int) { Value = result.DownloadRecord.AID });
                                cmd.Parameters.Add(new SqlParameter("@ASID", SqlDbType.Int) { Value = result.DownloadRecord.ASID });
                                cmd.Parameters.Add(new SqlParameter("@GARNID", SqlDbType.Int) { Value = result.GarnishmentID });

                                if (conn.State != ConnectionState.Open) conn.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch(Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }
        public virtual async Task UpdateGarnIDAsync(List<Reporting.AssetResult<K>> garnResults, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                foreach (var result in garnResults)
                {
                    if (result.GarnishmentResultReason.HasFlag(Enums.GarnishmentResultReason.Requested) && result.GarnishmentID > 0)
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("UpdateBatchAssetGarn", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                            {
                                cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                                cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = result.DownloadRecord.DebtorNo });
                                cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.Int) { Value = result.DownloadRecord.AID });
                                cmd.Parameters.Add(new SqlParameter("@ASID", SqlDbType.Int) { Value = result.DownloadRecord.ASID });
                                cmd.Parameters.Add(new SqlParameter("@GARNID", SqlDbType.Int) { Value = result.GarnishmentID });

                                if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        #region Asset & Garn Request Functions
        private static IEnumerable<RecordTypes.YGC.Base.YGCBase> GenerateGarnRequestCodes(Assets.AccountAssetInfo<K> assetInfo)
        {
            // Add 544
            yield return new RecordTypes2.YGC.RecordType95()
            {
                MASCO_FILE = assetInfo.DownloadRecord.FileNo,
                DIARY_CODE = 544,
                DIARY_DATE = DateTime.Today,
                DIARY_CMT = "WATERFALL GARN REQUEST"
            };
            // Add Note Line
            yield return new RecordTypes2.YGC.RecordType09()
            {
                MASCO_FILE = assetInfo.DownloadRecord.FileNo,
                PCMT = "Garnishment Requested",
                NOTE02 = "Waterfall Asset"
            };
        }
        private async Task<Garnishments.Data.Assets.AccountAsset> AddAsset(Assets.AccountAssetInfo<K> accountAssetInfo, Reporting.AssetResult<K> result)
        {
            try
            {
                // Add to DB
                var newAsset = await accountAssetInfo.AddAsset();
                result.Result |= Enums.AssetResultType.Added;
                return newAsset;
            }
            catch (Exception ex)
            {
                result.Result |= Enums.AssetResultType.Error;
                result.AssetResultReason = Enums.AssetResultReason.Error;
                result.GarnishmentResultReason = Enums.GarnishmentResultReason.Error;
                result.Description = ex.Message;
                return null;
            }
        }
        private async Task<bool> TestForUniqueness(Assets.AccountAssetInfo<K> accountAssetInfo, Garnishments.Data.Assets.AccountAsset newAsset, Reporting.AssetResult<K> result)
        {
            // Test that New Asset is Unique
            if (accountAssetInfo.AccountAssets.Assets.SelectMany(ass => ass.History).Count(aii => aii.Info.ID == newAsset.CurrentInfo.Info.ID) == 0)
            {
                // Not Found
                // Evaluate Account
                return true;
            }
            else
            {
                // Not Unique - Previously Entered
                result.AssetResultReason = Enums.AssetResultReason.ExactMatchOnFile;

                var prevGarns = await newAsset.CurrentInfo.GetRequestsAsync();
                if (prevGarns.Count() == 0)
                {
                    // No Previous Garnishments
                    return true;
                }
                else
                {
                    // Has Previous Garn
                    if (prevGarns.OrderByDescending(g => g.ID).FirstOrDefault().IsActive)
                    {
                        // Currently Active
                        result.GarnishmentResultReason = Enums.GarnishmentResultReason.PreviousGarnInPlace;
                        result.Description += "Exact Match on File - Garn Active";
                        return false;
                    }
                    else
                    {
                        // Not Active
                        if (prevGarns.OrderByDescending(g => g.ID).FirstOrDefault().CurrentStatus.Result.Good)
                        {
                            // Was Good
                            return true;
                        }
                        else
                        {
                            // Was Bad
                            result.GarnishmentResultReason = Enums.GarnishmentResultReason.KnownBadInfo;
                            result.Description += "Exact Match on File - Prev Garn Unsuccessfull";
                            return false;
                        }
                    }
                }
            }
        }
        private async Task<int> RequestGarn(Garnishments.Data.Assets.AccountAssetInfo accountAssetInfo, Reporting.AssetResult<K> result)
        {
            try
            {
                var requestId = await Garnishments.Data.DataHandler.RequestNewGarnishmentAsync(accountAssetInfo.Account.FileNo, accountAssetInfo.Debtor, accountAssetInfo.Info.ID, "Asset Waterfall");
                result.GarnishmentResultReason = Enums.GarnishmentResultReason.Requested;
                result.Result |= Enums.AssetResultType.Garnished;
                return requestId;
            }
            catch(Exception ex)
            {
                result.Result |= Enums.AssetResultType.Error;
                result.GarnishmentResultReason = Enums.GarnishmentResultReason.Error;
                result.Description += "\r\n\r\n" + ex.Message;
                return 0;
            }
        }
        private bool EvaluateAccountForGarn(Garnishments.Data.CriteriaSets.CriteriaDataHandler criteria, Garnishments.Data.Accounts.EvaluateeAccount evaluatee, Garnishments.Data.Assets.AccountAsset accountAsset, Reporting.AssetResult<K> result)
        {
            var results = criteria.Evaluate(accountAsset, evaluatee);
            if (results.Count() == 0)
            {
                result.GarnishmentResultReason = Enums.GarnishmentResultReason.AccountIneligible;
                result.Description += $"This State Is Not Properly Set Up\r\n\r\nSales: {evaluatee.SalesNo}";
                return false;
            }
            else if (results.Select(res => res.Result).Count(eval => !eval.Success) == 0)
            {
                // We're Good
                return true;
            }
            else
            {
                // Nope - You've Done it Now
                result.GarnishmentResultReason = Enums.GarnishmentResultReason.AccountIneligible;
                StringBuilder sb = new StringBuilder();
                foreach (var set in results.Where(r => !r.Result.Success).GroupBy(r => r.Set.Description))
                {
                    sb.AppendLine($"{set.Key}:");
                    foreach (var eval in results.Where(r => !r.Result.Success))
                    {
                        sb.AppendLine($"     {eval.Criteria.Name} - {eval.Result.Info}");
                    }
                }
                result.Description += $"{sb.ToString()}\r\n\r\n";
                return false;
            }
        }
        #endregion
        #endregion

        public void SaveDownloadRecords(IEnumerable<K> downloads, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                foreach (var download in downloads)
                {
                    using (SqlCommand cmd = new SqlCommand("AddBatchAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                        cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.Int) { Value = Convert.ToInt32(download.AssetType) });
                        cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar) { Value = download.FileNo });
                        cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = download.DebtorNo });
                        cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = download.Name });
                        cmd.Parameters.Add(new SqlParameter("@CONTACT", SqlDbType.NVarChar) { Value = $"{download.Contact}" });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS_LINE1", SqlDbType.NVarChar) { Value = download.Address1 });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS_LINE2", SqlDbType.NVarChar) { Value = download.Address2 });
                        cmd.Parameters.Add(new SqlParameter("@CITY", SqlDbType.NVarChar) { Value = download.City });
                        cmd.Parameters.Add(new SqlParameter("@STATE", SqlDbType.NChar, 2) { Value = download.State });
                        cmd.Parameters.Add(new SqlParameter("@ZIP", SqlDbType.NVarChar) { Value = download.Zip });
                        cmd.Parameters.Add(new SqlParameter("@PHONE", SqlDbType.NVarChar) { Value = download.Phone });
                        cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("@ASID", SqlDbType.Int) { Direction = ParameterDirection.Output });

                        if (conn.State != ConnectionState.Open) conn.Open();
                        cmd.ExecuteNonQuery();

                        download.AID = Convert.ToInt32(cmd.Parameters["@AID"].Value);
                        download.ASID = Convert.ToInt32(cmd.Parameters["@ASID"].Value);
                    }
                }
            }
        }
        public async Task SaveDownloadRecordsAsync(IEnumerable<K> downloads, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                foreach (var download in downloads)
                {
                    using (SqlCommand cmd = new SqlCommand("AddBatchAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                        cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.Int) { Value = Convert.ToInt32(download.AssetType) });
                        cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar) { Value = download.FileNo });
                        cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = download.DebtorNo });
                        cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = download.Name });
                        cmd.Parameters.Add(new SqlParameter("@CONTACT", SqlDbType.NVarChar) { Value = $"{download.Contact}" });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS_LINE1", SqlDbType.NVarChar) { Value = download.Address1 });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS_LINE2", SqlDbType.NVarChar) { Value = download.Address2 });
                        cmd.Parameters.Add(new SqlParameter("@CITY", SqlDbType.NVarChar) { Value = download.City });
                        cmd.Parameters.Add(new SqlParameter("@STATE", SqlDbType.NChar, 2) { Value = download.State });
                        cmd.Parameters.Add(new SqlParameter("@ZIP", SqlDbType.NVarChar) { Value = download.Zip });
                        cmd.Parameters.Add(new SqlParameter("@PHONE", SqlDbType.NVarChar) { Value = download.Phone });
                        cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("@ASID", SqlDbType.Int) { Direction = ParameterDirection.Output });

                        if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        download.AID = Convert.ToInt32(cmd.Parameters["@AID"].Value);
                        download.ASID = Convert.ToInt32(cmd.Parameters["@ASID"].Value);
                    }
                }
            }
        }

        public void SaveUploadRecords(IEnumerable<T> uploads, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var upload in uploads)
                        {
                            using (SqlCommand cmd = new SqlCommand("AddBatchUploadAccount", conn) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = tran })
                            {
                                cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                                cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar) { Value = upload.FileNo });
                                cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = upload.DebtorNo });

                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task SaveUploadRecordsAsync(IEnumerable<T> uploads, int batchId)
        {
            using (SqlConnection conn = new SqlConnection(Data.Settings.Connections.STDB))
            {
                await conn.OpenAsync();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var upload in uploads)
                        {
                            using (SqlCommand cmd = new SqlCommand("AddBatchUploadAccount", conn) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = tran })
                            {
                                cmd.Parameters.Add(new SqlParameter("@BID", SqlDbType.Int) { Value = batchId });
                                cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar) { Value = upload.FileNo });
                                cmd.Parameters.Add(new SqlParameter("@DEBTOR", SqlDbType.TinyInt) { Value = upload.DebtorNo });

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        tran.Commit();
                    }
                    catch(Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        private static Tuple<List<RecordTypes.YGC.Base.YGCBase>, List<RecordTypes.MergePops.MergePop>> GetOutputRecords(System.IO.DirectoryInfo directory)
        {
            List<RecordTypes.YGC.Base.YGCBase> ygcUpdates = new List<YGCBase>();
            List<RecordTypes.MergePops.MergePop> merges = new List<MergePop>();
            // Looking for these files names and combining them
            // YGC_Updates_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt
            // Merges_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt
            foreach (var file in directory.EnumerateFiles())
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, @"YGC_Updates_[\d]{4}-[\d]{2}-[\d]{2}_[\d]{2}-[\d]{2}-[\d]{2}\.txt"))
                {
                    using (RecordTypes2.FileReaders.FileReader<RecordTypes.YGC.Base.YGCBase> reader = new RecordTypes2.FileReaders.FileReader<YGCBase>(file.FullName, RecordTypes.SupportedEDITypes.YGC))
                    {
                        ygcUpdates.AddRange(reader.ReadFile());
                    }
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, @"Merges_[\d]{4}-[\d]{2}-[\d]{2}_[\d]{2}-[\d]{2}-[\d]{2}\.txt"))
                {
                    merges.AddRange(RecordTypes.MergePops.FileReader.ReadFile(file));
                }
            }
            return new Tuple<List<YGCBase>, List<MergePop>>(ygcUpdates, merges);
        }
        public static void CombineOutputFiles(System.IO.DirectoryInfo directory)
        {
            var records = GetOutputRecords(directory);

            #region Export Update Files
            if (records.Item1.Count() > 0)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(directory.FullName, $"YGC_Updates_Combined_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    foreach (var rec in records.Item1)
                    {
                        sw.WriteLine(rec.ToString());
                    }
                }
            }
            if (records.Item2.Count() > 0)
            {
                using (RecordTypes.MergePops.FileWriter sw = new RecordTypes.MergePops.FileWriter(System.IO.Path.Combine(directory.FullName, $"Merges_Combined_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    sw.WriteFile(records.Item2);
                }
            }
            #endregion
        }
        public static async Task CombineOutputFilesAsync(System.IO.DirectoryInfo directory)
        {
            var records = GetOutputRecords(directory);

            #region Export Update Files
            if (records.Item1.Count() > 0)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(directory.FullName, $"YGC_Updates_Combined_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    foreach (var rec in records.Item1)
                    {
                        await sw.WriteLineAsync(rec.ToString());
                    }
                }
            }
            if (records.Item2.Count() > 0)
            {
                using (RecordTypes.MergePops.FileWriter sw = new RecordTypes.MergePops.FileWriter(System.IO.Path.Combine(directory.FullName, $"Merges_Combined_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt")))
                {
                    sw.WriteFile(records.Item2);
                }
            }
            #endregion
        }

        #region Emails
        public static void SendManualSkipEmail(IEnumerable<K> downloads, Func<Exception, System.Net.NetworkCredential> loginFail = null)
        {
            ExchangeInterface.Exchange xcApp = new ExchangeInterface.Exchange();
            xcApp.UnAuthorizedException += loginFail;
            var body = GenerateManualSkipEmailBody(downloads.Where(d => d.ImportSettings.HasFlag(Enums.ImportSettings.ManuallyVerify)));
            xcApp.SendEmail("Skip Tracing - Unverified", body, new[] { "collections@hosto.com" }, new string[0], new string[0], true);
        }
        public static string GenerateManualSkipEmailBody(IEnumerable<K> downloads)
        {
            StringBuilder sb = new StringBuilder("<html><body><center><h2>Skip Trace Unverified Results</h2></center><p></p><table><thead><tr><th>FileNo.</th><th>Type</th><th>Name</th><th>Contact</th><th>Address</th><th>Phone></th><th>Info</th></tr></thead><tbody>");
            foreach (var rec in downloads)
            {
                sb.AppendLine($"<tr><td>{rec.FileNo}</td><td>{rec.AssetType}</td><td>{rec.Name}</td><td>{rec.Contact}</td><td>{rec.Address1} {rec.Address2}, {rec.City}, {rec.State} {rec.Zip}</td><td>{rec.Phone}</td><td>{rec.AdditionalInfo}</td></tr>");
            }
            sb.AppendLine("</tbody></table></body></html>");
            return sb.ToString();
        }

        #endregion
    }
}
