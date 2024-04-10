using HB.Garnishments.Data.Requests.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Communications;
using HB.Garnishments.Data.Enums;
using System.Threading;
using HB.Garnishments.Data.Requests;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HB.Garnishments.Data.Processing
{
    public class FilingProcessor : NoNegativeResultProcessor
    {
        public FilingProcessor(SynchronizationContext sync, HubManager hubManager, string userName, byte? salesRestriction = null) : base(sync, hubManager, Enums.Status.Filed, userName, salesRestriction, Enums.Status.Signed)
        {
        }


        public override async Task ProcessResultsAsync ()
        {
            List<Exception> exceptions = new List<Exception>();
            List<RecordTypes2.YGC.RecordType09> notes = new List<RecordTypes2.YGC.RecordType09>();
            List<RecordTypes.MergePops.MergePop> merges = new List<RecordTypes.MergePops.MergePop>();

            ////////////////////////////
            /// ALL SCRUBS AUTOMATED ///
            ////////////////////////////
            /// CHECK FOR ACTIVE DUTY //
            ////////////////////////////

            var activeDutyStatus = await HB.SkipTracing.SCRA.Data.DataHandler.GetAccountStatusAsync(this.PositiveItemsPending.Select(req => new Tuple<string, byte>(req.Account.FileNo, req.Request.Asset.Debtor)));
            // WHAT DO YOU DO WITH NO RESULT IN DATABASE??? (NEITHER ACTIVE NOR INACTIVE)

            // Process for Active Results
            foreach (var item in activeDutyStatus?.Where(ads => ads.ActiveDuty))
            {
                foreach (var requestResult in this.PositiveItemsPending.Where(req => req.Account.FileNo == item.FileNo).ToArray())
                {
                    this.Sync.Send((callback) =>
                    {
                        this.PositiveItemsPending.Remove(requestResult);
                        this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                            requestResult.Request,
                            new CriteriaSets.EvaluationResult[]
                            {
                                new CriteriaSets.EvaluationResult()
                                {
                                    Account = requestResult.Account,
                                    Set = new CriteriaSets.EvaluationSet(null, 0, "SCRA"),
                                    Criteria = new CriteriaSets.EmptySimpleSet("Military Search"),
                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, "Active Duty"),
                                    Request = requestResult.Request,
                                }
                            })
                        );
                    }, null);

                    RecordTypes2.YGC.RecordType09 note = new RecordTypes2.YGC.RecordType09()
                    {
                        MASCO_FILE = item.FileNo,
                        PDATE = DateTime.Now,
                        PCMT = "CONSUMER IS ACTIVE DUTY MILITARY",
                        NOTE02 = "BK and Deceased scrub performed by Banko - NA"
                    };
                    notes.Add(note);

                    for (int i = 0; i < 3; i++)
                    {
                        note = new RecordTypes2.YGC.RecordType09()
                        {
                            MASCO_FILE = item.FileNo,
                            PCODE = "XCA",
                            PDATE = DateTime.Today
                        };

                        switch (i)
                        {
                            case 0:
                                note.PCMT = "Account Scrubbed For Bankruptcy  NA".ToUpper();
                                break;
                            case 1:
                                note.PCMT = "Account Scrubbed For Deceased  NA".ToUpper();
                                break;
                            case 2:
                                note.PCMT = "Account Scrubbed For Military".ToUpper();
                                break;
                        }
                        notes.Add(note);
                    }
                        

                    merges.Add(new RecordTypes.MergePops.MergePop()
                    {
                        FILENO = requestResult.Account.FileNo,
                        LLCODE = "XNOGARN"
                    });
                }
            }
            // Not Active Duty - Need Image in Scan
            System.IO.DirectoryInfo scraDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Settings.Properties.SCRADownloadFolder, "Processed"));
            // Get Most Recent Download File
            var zipFile = scraDir.EnumerateFiles("*.zip").OrderByDescending(fil => fil.CreationTime).FirstOrDefault();
            if (zipFile != null)
            {
                await Task.Run(async () =>
                {
                    using (Zip.ZipArchive zipArchive = Zip.ZipArchive.OpenOnFile(zipFile.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        foreach (var item in activeDutyStatus.Where(ads => !ads.ActiveDuty))
                        {
                            foreach (var requestResult in this.PositiveItemsPending.Where(req => req.Account.FileNo == item.FileNo).ToArray())
                            {
                                var certFile = await Task.Run(() => { return zipArchive.Files.FirstOrDefault(zip => zip.Name.StartsWith($"{requestResult.Account.FileNo}-{requestResult.Request.Asset.Debtor}", StringComparison.OrdinalIgnoreCase)); });
                                if (certFile == default)
                                {
                                    // Is It a Commercial Claim?     ||||| CAN WE DO THAT?!? |||||
                                    if (!requestResult.Account.CLSName.Contains('/')) continue;

                                    // No Certificate in Directory
                                    this.Sync.Send((callback) =>
                                    {
                                        this.PositiveItemsPending.Remove(requestResult);
                                        this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                                            requestResult.Request,
                                            new CriteriaSets.EvaluationResult[]
                                            {
                                                new CriteriaSets.EvaluationResult()
                                                {
                                                    Account = requestResult.Account,
                                                    Set = new CriteriaSets.EvaluationSet(null, 0, "SCRA"),
                                                    Criteria = new CriteriaSets.EmptySimpleSet("Military Search"),
                                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, "No Certificate Could Be Found in Recent Search"),
                                                    Request = requestResult.Request,
                                                }
                                            })
                                        );
                                    }, null);
                                }
                                else
                                {
                                    await HB.SkipTracing.SCRA.Data.DataHandler.SaveScrubFileToScan(requestResult.Account.FileNo, $"{requestResult.Request.Asset.Debtor}", certFile);
                                }
                            }

                            RecordTypes2.YGC.RecordType09 note = new RecordTypes2.YGC.RecordType09()
                            {
                                MASCO_FILE = item.FileNo,
                                PDATE = DateTime.Now,
                                PCMT = "PER DoD DATABASE: CONSUMER NOT ACTIVE DUTY MILITARY",
                                NOTE02 = "BK and Deceased scrub performed by Banko - NA"
                            };
                            notes.Add(note);

                            for (int i = 0; i < 3; i++)
                            {
                                note = new RecordTypes2.YGC.RecordType09()
                                {
                                    MASCO_FILE = item.FileNo,
                                    PCODE = "XCA",
                                    PDATE = DateTime.Today
                                };

                                switch (i)
                                {
                                    case 0:
                                        note.PCMT = "Account Scrubbed For Bankruptcy  NA".ToUpper();
                                        break;
                                    case 1:
                                        note.PCMT = "Account Scrubbed For Deceased  NA".ToUpper();
                                        break;
                                    case 2:
                                        note.PCMT = "Account Scrubbed For Military".ToUpper();
                                        break;
                                }
                                notes.Add(note);
                            }
                        }
                    }
                });
            }
            else
            {

            }

            /////////////////////////////
            ////// MTG v10 Workaround //
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////MTGInterface.MTGScrubForm scrubber = new MTGInterface.MTGScrubForm(this.PositiveItemsPending.Select(req => req.Account.FileNo).ToList());
            ////scrubber.ShowDialog();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ////MTGInterface.MTGScrubber.SendYGCRecords(scrubber.Results);
            ////merges.AddRange(MTGInterface.MTGScrubber.CreateMergePops(scrubber.Results));

            ////// Evaluate SCRA Active Duty Accounts
            ////foreach (MTGInterface.MTGScrubber.MTGScrubItem item in scrubber.Results.Where(el => el.ActiveMilitary.HasValue && el.ActiveMilitary.Value))
            ////{
            ////    foreach (var requestResult in this.PositiveItemsPending.Where(req => req.Account.FileNo == item.FileNo && req.Request.Asset.Debtor == item.Debtor).ToArray())
            ////    {
            ////        this.Sync.Send((callback) =>
            ////        {
            ////            this.PositiveItemsPending.Remove(requestResult);
            ////            this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
            ////                requestResult.Request,
            ////                new CriteriaSets.EvaluationResult[]
            ////                {
            ////                    new CriteriaSets.EvaluationResult()
            ////                    {
            ////                        Account = requestResult.Account,
            ////                        Set = new CriteriaSets.EvaluationSet(null, 0, "SCRA"),
            ////                        Criteria = new CriteriaSets.EmptySimpleSet("Military Search"),
            ////                        Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, "Active Duty"),
            ////                        Request = requestResult.Request,
            ////                    }
            ////                })
            ////            );
            ////        }, null);

            ////        merges.Add(new RecordTypes.MergePops.MergePop()
            ////        {
            ////            FILENO = requestResult.Account.FileNo,
            ////            LLCODE = "XNOGARN"
            ////        });
            ////    }
            ////}

            // Notate & Code Remaining Accounts
            foreach (var requestResult in this.PositiveItemsPending)
            {
                notes.Add(new RecordTypes2.YGC.RecordType09()
                {
                    MASCO_FILE = requestResult.Account.FileNo,
                    PDATE = DateTime.Now,
                    PCMT = "FINAL CHECKS COMPLETED",
                    NOTE02 = "Sending to Court"
                });
                switch (requestResult.Request.Asset.Type)
                {
                    case AssetType.Employer:
                        merges.Add(new RecordTypes.MergePops.MergePop()
                        {
                            FILENO = requestResult.Account.FileNo,
                            LLCODE = "XGARNME"
                        });
                        break;
                    case AssetType.Bank:
                        merges.Add(new RecordTypes.MergePops.MergePop()
                        {
                            FILENO = requestResult.Account.FileNo,
                            LLCODE = "XGARNMB"
                        });
                        break;
                }
                if (this.SalesNo == 1) // AR
                {
                    merges.Add(new RecordTypes.MergePops.MergePop()
                    {
                        FILENO = requestResult.Account.FileNo,
                        LLCODE = "XP608"
                    });
                }
            }


            // Handle Export Records
            await GenerateExportsAsync(notes, merges, null);


            // Save Updates to Database by Adding Status
            foreach (var req in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Filed, this.UserName, DateTime.Now);

                    Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });

                    await RemovePositiveResultAsync(req, true);
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {req.Request.Asset.Account.FileNo} / Request# {req.Request.ID}"));
                }
            }
            if (this.FailedItemsPending.Count > 0)
            {
                try
                {
                    await HandleFailures(false);
                }
                catch (AggregateException aex)
                {
                    exceptions.AddRange(aex.InnerExceptions);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            if (exceptions.Count > 0)
            {
                if (exceptions.Count > 1)
                {
                    throw new AggregateException("Multiple Requests Failed to Update", exceptions);
                }
                else
                {
                    throw exceptions.First();
                }
            }
        }

        public override async Task CancelResultsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
