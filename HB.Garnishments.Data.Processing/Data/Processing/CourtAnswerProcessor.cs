using HB.Garnishments.Data.Communications;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace HB.Garnishments.Data.Processing
{
    public class CourtAnswerProcessor : Processor
    {
        Walz.Data.UI.BatchManagementForm WalzManagementForm { get; set; }
        Walz.Data.DataHandler WalzManager { get; set; }
        Zips.Zip[] ZipCodeList { get; set; }

        public CourtAnswerProcessor(SynchronizationContext sync, HubManager hubManager, string userName, byte? salesRestriction = null) : base(sync, hubManager, Enums.Status.CourtResponse, userName, salesRestriction, Enums.Status.Filed, Enums.Status.HearingRequested)
        {
        }

        #region Override Result Additions to Add "File Marked Date"
        public override async Task AddNegativeResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            try
            {
                var result = await GetResultAsync(
                        request,
                        this.ProcessingStatus,
                        false,
                        new Requests.Results.Codes.MergeCodeInfo[] { new Requests.Results.Codes.MergeCodeInfo(Enums.ResultInfoType.Date, "File Marked Date") },
                        null,
                        selectResult
                    );
                if (result != null)
                {
                    AddNegativeItem(result);
                    await SetWorkingStatusAsync(result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override async Task AddPositiveResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            try
            {
                var result = await GetResultAsync(
                        request,
                        this.ProcessingStatus,
                        true,
                        new Requests.Results.Codes.MergeCodeInfo[] { new Requests.Results.Codes.MergeCodeInfo(Enums.ResultInfoType.Date, "File Marked Date") },
                        null,
                        selectResult
                    );
                if (result != null)
                {
                    AddPositiveItem(result);
                    await SetWorkingStatusAsync(result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public override async Task ProcessResultsAsync()
        {
            List<RecordTypes2.YGC.RecordType09> notes = new List<RecordTypes2.YGC.RecordType09>();
            List<RecordTypes2.YGC.RecordType95> diaries = new List<RecordTypes2.YGC.RecordType95>();
            List<RecordTypes.MergePops.MergePop> merges = new List<RecordTypes.MergePops.MergePop>();
            List<Exception> exceptions = new List<Exception>();
            ZipCodeList = await Zips.Zip.GetAllZipsAsync();

            this.WalzManager = await Walz.Data.DataHandler.GetDataHandlerAsync();

            #region Arkansas Routine
            if (this.SalesNo == 1) // AR
            {
                await CreateMailItems(this.SalesNo.Value, 5, notes, diaries);
            }
            else if (this.SalesNo == 5) // IN
            {
                await CreateMailItems(this.SalesNo.Value, 10, notes, diaries);
            }
            #endregion

            // Add Result Codes
            foreach (var requestResult in this.PositiveItemsPending.Union(this.NegativeItemsPending))
            {
                merges.AddRange(GenerateAnswerCodes(requestResult));
            }

            // Handle Export Records
            await GenerateExportsAsync(notes.OfType<RecordTypes.YGC.Base.YGCBase>().Union(diaries), merges, null);

            // Save Updates to Database by Adding Status
            foreach (var req in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.CourtResponse, this.UserName, DateTime.Now, req.SelectedResult?.ID);
                    try
                    {
                        await RemovePositiveResultAsync(req, true);

                        Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {req.Request.Asset.Account.FileNo} / Request# {req.Request.ID}"));
                }
            }
            foreach (var req in this.NegativeItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.CourtResponse, this.UserName, DateTime.Now, req.SelectedResult?.ID);
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Rejected, this.UserName, DateTime.Now, null, req.AdditionalValues.ContainsKey("Reason") && !string.IsNullOrWhiteSpace($"{req.AdditionalValues["Reason"]}") ? $"{req.AdditionalValues["Reason"]}" : null);
                    try
                    {
                        await RemoveNegativeResultAsync(req, true);

                        Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });
                    }
                    catch { }
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

        


        /// <summary>
        /// Send Certified Copies to Def/Atty via Walz
        /// </summary>
        /// <param name="salesNo">CLS Sales No. (Used to Determine State)</param>
        /// <param name="numPages">Est. Number of Pages in Envelope (Used to Calculate Weight)</param>
        /// <param name="notes">Reference to List of Notes</param>
        /// <param name="diaries">Reference to List of Diaries</param>
        /// <returns></returns>
        private async Task CreateMailItems(int salesNo, int numPages, List<RecordTypes2.YGC.RecordType09> notes, List<RecordTypes2.YGC.RecordType95> diaries)
        {
            List<CertifiedMail.WalzMailEntry> mailItems = new List<CertifiedMail.WalzMailEntry>();
            List<CertifiedMail.SSILetter> letters = new List<CertifiedMail.SSILetter>();
            foreach (var requestResult in this.PositiveItemsPending.ToArray())
            {
                Assets.Base.RegisteredAgent registeredAgent = null;
                try
                {
                    registeredAgent = Settings.Properties.RegisteredAgents.FirstOrDefault(agent => agent.ID == requestResult.Request.RegisteredAgent && agent.AssetType == requestResult.Request.Asset.Type);
                }
                catch (Exception ex)
                {

                }

                if (registeredAgent != null)
                {
                    // Send Cert Mail to Registered Agent - Get Tracking Info & Cost
                    List<CertifiedMail.WalzMailEntry> tempMailItems = new List<CertifiedMail.WalzMailEntry>();
                    //////////////////////////////////////////////////////////////
                    // Send to Debtor(s) / Opposing Counsel / Registered Agent ///
                    //////////////////////////////////////////////////////////////
                    #region Find All Mail Recipients
                    // Agent
                    tempMailItems.Add(new CertifiedMail.WalzMailEntry(2, requestResult.Account.FileNo, registeredAgent, true, false, true)); // What's going on with the "2" ???
                                                                                                                                             // Debtors(s) -- SEEMS TO BE IGNORED
                    foreach (var debtor in requestResult.Account.Debtors)
                    {
                        //if (debtor.ReturnMail)
                        //{
                        //    // There's Return Mail for This Debtor
                        //    this.Sync.Send((callback) =>
                        //    {
                        //        if (this.PositiveItemsPending.Contains(requestResult))
                        //        {
                        //            this.PositiveItemsPending.Remove(requestResult);
                        //        }
                        //        this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                        //            requestResult.Request,
                        //            new CriteriaSets.EvaluationResult[]
                        //            {
                        //                new CriteriaSets.EvaluationResult()
                        //                {
                        //                    Account = requestResult.Account,
                        //                    Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                        //                    Criteria = new CriteriaSets.EmptySimpleSet("Invalid Address"),
                        //                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"Debtor {debtor.Debtor} - Return Mail Flagged"),
                        //                    Request = requestResult.Request,
                        //                }
                        //            })
                        //        );
                        //    }, null);
                        //    continue;
                        //}
                        //else
                        if (string.IsNullOrEmpty(debtor.CLSName) || debtor.CLSName.Contains('/') && string.IsNullOrEmpty(debtor.SSN))
                        {
                            // This Debtor Has No SSN & Is Not A Business

                            // Is It The Requested Debtor?
                            if (debtor.Debtor == requestResult.Request.Asset.Debtor)
                            {
                                // Fail
                                this.Sync.Send((callback) =>
                                {
                                    if (this.PositiveItemsPending.Contains(requestResult))
                                    {
                                        this.PositiveItemsPending.Remove(requestResult);
                                    }
                                    this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                                        requestResult.Request,
                                        new CriteriaSets.EvaluationResult[]
                                        {
                                                new CriteriaSets.EvaluationResult()
                                                {
                                                    Account = requestResult.Account,
                                                    Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                                    Criteria = new CriteriaSets.EmptySimpleSet("Invalid Name / SSN"),
                                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"Debtor {debtor.Debtor} - No SSN Or Bad Name"),
                                                    Request = requestResult.Request,
                                                }
                                        })
                                    );
                                }, null);

                            }

                            continue;
                        }
                        else
                        {
                            tempMailItems.Add(new CertifiedMail.WalzMailEntry(2, requestResult.Account.FileNo, debtor, false, true, false));
                        }
                    }
                    // Counsel
                    if (requestResult.Account.AdversarialAttorney > 0)
                    {
                        var counsel = Settings.Properties.Counsels.FirstOrDefault(adva => adva.ID == requestResult.Account.AdversarialAttorney);
                        if (counsel != null)
                        {
                            try
                            {
                                tempMailItems.Add(new CertifiedMail.WalzMailEntry(2, requestResult.Account.FileNo, counsel, true, true, false));
                            }
                            catch (Exception ex)
                            {
                                // Invalid Address -- Most Likely
                                this.Sync.Send((callback) =>
                                {
                                    if (this.PositiveItemsPending.Contains(requestResult))
                                    {
                                        this.PositiveItemsPending.Remove(requestResult);
                                    }
                                    this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                                        requestResult.Request,
                                        new CriteriaSets.EvaluationResult[]
                                        {
                                        new CriteriaSets.EvaluationResult()
                                        {
                                            Account = requestResult.Account,
                                            Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                            Criteria = new CriteriaSets.EmptySimpleSet("Invalid Address"),
                                            Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"OC Information Invalid [{requestResult.Account.AdversarialAttorney}], Please Check for Proper Formatting \"[City], [State] [Zip]\""),
                                            Request = requestResult.Request,
                                        }
                                        })
                                    );
                                }, null);
                            }
                        }
                        else
                        {
                            this.Sync.Send((callback) =>
                            {
                                if (this.PositiveItemsPending.Contains(requestResult))
                                {
                                    this.PositiveItemsPending.Remove(requestResult);
                                }
                                this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                                    requestResult.Request,
                                    new CriteriaSets.EvaluationResult[]
                                    {
                                        new CriteriaSets.EvaluationResult()
                                        {
                                            Account = requestResult.Account,
                                            Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                            Criteria = new CriteriaSets.EmptySimpleSet("Not Found"),
                                            Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"OC Appears to Be Set [{requestResult.Account.AdversarialAttorney}], But Cannot Be Found"),
                                            Request = requestResult.Request,
                                        }
                                    })
                                );
                            }, null);
                        }
                    }
                    #endregion

                    /////////////////////////////////////////////////
                    ///  INVESTIGATE ALL PARTIES RECEIVING LETTER ///
                    /////////////////////////////////////////////////
                    #region Evalute Mail Recipients
                    List<CriteriaSets.EvaluationResult> evals = new List<CriteriaSets.EvaluationResult>();
                    foreach (var recipient in tempMailItems)
                    {
                        // Check Zip is 5 or 10 digits & Matches Format
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(?<First5>[\d]{5})(\-)?(?<Last4>[\d]{4})?");
                        var match = regex.Match(recipient.Zip ?? "");
                        if (match.Success && recipient.Zip.Length >= 5)
                        {
                            var first5 = match.Groups["First5"].Value;
                            var last4 = match.Groups["Last4"].Value;
                            // Check Zip Exists in Database
                            var zip = ZipCodeList.FirstOrDefault(z => z.ZipCode.Equals(first5));
                            if (zip == default)
                            {
                                // Not Found - Need to Contact I.T.
                                evals.Add(new CriteriaSets.EvaluationResult()
                                {
                                    Account = requestResult.Account,
                                    Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                    Criteria = new CriteriaSets.EmptySimpleSet("Zip Code Not Found"),
                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"{recipient.Name1} {recipient.Name2} - {recipient.Address1} - {recipient.City}, {recipient.State} {recipient.Zip}"),
                                    Request = requestResult.Request,
                                });
                                continue;
                            }
                            // Check Zip Matches State
                            if (!zip.StateAbrev.Trim().Equals(recipient.State.Trim(), StringComparison.OrdinalIgnoreCase))
                            {
                                // Zip Does Not Match State
                                evals.Add(new CriteriaSets.EvaluationResult()
                                {
                                    Account = requestResult.Account,
                                    Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                    Criteria = new CriteriaSets.EmptySimpleSet("Zip / State Mismatch"),
                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"{recipient.Name1} {recipient.Name2} - {recipient.Address1} - {recipient.City}, {recipient.State} {recipient.Zip}"),
                                    Request = requestResult.Request,
                                });
                            }
                        }
                        else
                        {
                            // Invalid Zip
                            evals.Add(new CriteriaSets.EvaluationResult()
                            {
                                Account = requestResult.Account,
                                Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Mail"),
                                Criteria = new CriteriaSets.EmptySimpleSet("Invalid Address"),
                                Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, $"{recipient.Name1} {recipient.Name2} - {recipient.Address1} - {recipient.City}, {recipient.State} {recipient.Zip}"),
                                Request = requestResult.Request,
                            });
                        }
                    }
                    #endregion

                    #region Final Checks to Cancel or Continue Request Processing
                    if (evals.Count > 0)
                    {
                        // One or More Issues Found - Stop Processing This One
                        this.Sync.Send((callback) =>
                        {

                            if (this.PositiveItemsPending.Contains(requestResult))
                            {
                                this.PositiveItemsPending.Remove(requestResult);
                            }
                            this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                                requestResult.Request,
                                evals.ToArray())
                            );
                        }, null);
                        continue;
                    }
                    else if (!this.PositiveItemsPending.Contains(requestResult))
                    {
                        continue;
                    }
                    else
                    {
                        // No Issues Found
                        mailItems.AddRange(tempMailItems);
                        letters.Add(new CertifiedMail.SSILetter(requestResult.Account, tempMailItems.Where(mail => mail.ReceivesSSILetter)));
                    }
                    #endregion

                    notes.Add(new RecordTypes2.YGC.RecordType09()
                    {
                        MASCO_FILE = requestResult.Account.FileNo,
                        NOTE02 = $"On {DateTime.Today:MM/dd/yyyy}",
                        NOTE03 = $"File Marked Received From Court: {(requestResult.AdditionalValues["File Marked Date"] as DateTime?):MM/dd/yyyy}",
                        NOTE04 = $"Type: {(requestResult.Request.Asset.Type == Enums.AssetType.Bank ? "BANK" : "POE")}"
                    });
                    notes.Add(new RecordTypes2.YGC.RecordType09()
                    {
                        MASCO_FILE = requestResult.Account.FileNo,
                        PCODE = "XDELG2",
                        PCMT = "Garn Label Prepped and Garn Mailed to: " + registeredAgent.Name
                    });
                    diaries.Add(new RecordTypes2.YGC.RecordType95()
                    {
                        MASCO_FILE = requestResult.Account.FileNo,
                        DIARY_CODE = requestResult.Request.Asset.Type == Enums.AssetType.Bank ? 614 : 615,
                        DIARY_DATE = DateTime.Today.AddDays(35),
                        DIARY_CMT = registeredAgent.Name,
                        DIARY_QUEUE = $"QSALES{salesNo:N2}"
                    });
                }
                else
                {
                    // Failure - Unable to Acquire Registered Agent Info
                    this.Sync.Send((callback) =>
                    {
                        if (this.PositiveItemsPending.Contains(requestResult))
                        {
                            this.PositiveItemsPending.Remove(requestResult);
                        }
                        this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(
                            requestResult.Request,
                            new CriteriaSets.EvaluationResult[]
                            {
                                new CriteriaSets.EvaluationResult()
                                {
                                    Account = requestResult.Account,
                                    Set = new CriteriaSets.EvaluationSet(null, 0, "Registered Agent"),
                                    Criteria = new CriteriaSets.EmptySimpleSet("ID"),
                                    Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, "Not Set / Not Found"),
                                    Request = requestResult.Request,
                                }
                            })
                        );
                    }, null);
                }
            }


            if (mailItems.Count > 0)
            {
                // Create SSI Letters
                #region SSI Generation / Save / Display
                if (letters.Count > 0)
                {
                    using (var letterStream = await CertifiedMail.SSILetterGenerator.GenerateLettersAsync(letters))
                    {
                        while (true)
                        {
                            try
                            {
                                // Save File Somewhere
                                using (System.IO.FileStream fs = new System.IO.FileStream(@"C:\HBTools\Temp.pdf", System.IO.FileMode.Create))
                                {
                                    await letterStream.CopyToAsync(fs);
                                    await fs.FlushAsync();
                                }

                                this.Sync.Send((callback) =>
                                {
                                    Form F = new Form();
                                    WebBrowser wb = new WebBrowser();
                                    F.Controls.Add(wb);
                                    wb.Dock = DockStyle.Fill;
                                    wb.Navigate(@"C:\HBTools\Temp.pdf");
                                    F.Text = "Formatted SSI Letters";
                                    F.Icon = Properties.Resources.HB;
                                    F.Width = 600;
                                    F.Height = 776;
                                    F.ShowDialog();
                                }, null);

                                break;
                            }
                            catch
                            {
                                this.Sync.Send((callback) =>
                                {

                                    MessageBox.Show("Please Close Previous SSI Letters Sheet", "Unable to Continue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                }, null);
                            }
                        }
                    }
                }
                #endregion
                // Create Mail Labels for Debtors & Counsel
                ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();// ms, ExcelInterface.Enums.ExcelFormat.xls, true);
                xlApp.xlBook.ActiveSheet.Name = "Mail Labels";
                // Add Header
                xlApp.xlBook.ActiveSheet.AddDataToWorkSheet(CertifiedMail.WalzMailEntry.HeaderLine.Split('\t'), null, 0, 1);
                // Add Data
                int row = 1;
                foreach (var CM in mailItems.Where(mail => mail.NeedsLabel))
                {
                    row++;
                    xlApp.xlBook.ActiveSheet.AddDataToWorkSheet(CM.ToString().Split('\t'), null, 0, row);
                }
                xlApp.ShowWorkBook();

                // Create Certified Mail for Garnishees
                if (this.WalzManagementForm == null || this.WalzManagementForm.IsDisposed)
                {
                    this.WalzManagementForm = new Walz.Data.UI.BatchManagementForm(new Walz.Data.Files.Batches.BatchBuilder(new Walz.Data.Files.Batches.BatchManager(this.WalzManager)));
                }
                foreach (var entry in mailItems.Where(mail => mail.NeedsCertifiedMail))
                {
                    this.WalzManagementForm.AddRecipient(
                        new Walz.Data.Recipient(
                            entry.FileNo,
                            this.WalzManager.Letters.Find(el => el.Description.StartsWith("8.5x11", StringComparison.OrdinalIgnoreCase)),
                            this.WalzManager.Envelopes.Find(el => el.Size == Walz.Data.Enums.FormType.CertifiedEnvelope_6x9),
                            numPages,
                            Walz.Data.Enums.ReturnReceiptType.ReturnReceiptElectronic,
                            false,
                            false
                        )
                        {
                            Name = entry.Name1,
                            Name2 = entry.Name2,
                            Address1 = entry.Address1,
                            Address2 = entry.Address2,
                            City = entry.City,
                            State = entry.State,
                            Zip = entry.Zip
                        }
                    );
                }

                this.Sync.Send((callback) =>
                {
                    // Show Walz Processing
                    this.WalzManagementForm.ShowDialog();
                    // Check That it Completed Successfully
                    if (MessageBox.Show("If Walz Completed Successfully, Click \"Yes\"\r\n\r\nOtherwise, To Re-Do, Click \"No\"", "Walz Successful?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        throw new NotImplementedException("Walz Failed\r\n\r\nCancelling Process");
                    }

                }, null);
            }
        }

        public override Task CancelResultsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
