using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;
using System.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HB.Garnishments.Data.Processing
{
    public class RequestProcessor : Processor
    {
        public RequestProcessor(SynchronizationContext sync, Communications.HubManager hubManager, string userName, byte salesNo) : base(sync, hubManager, Enums.Status.Processed, userName, salesNo, Enums.Status.Requested)
        {

        }


        public override async Task ProcessResultsAsync()
        {
            // Check for Anything to Do
            if (this.PositiveItemsPending.Union(this.NegativeItemsPending).Count() == 0)
            {
                this.Sync.Send((callback) =>
                {
                    if (this.FailedItemsPending.Count == 0)
                    {
                        MessageBox.Show("No Items Set for Processing", "Nothing to Do", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("No Items Selected\r\n\r\nBut, Some Items Failed Criteria Checks\r\nProcess Them?", "Process Failures?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }, null);
            }
            // Create Export Records
            List<RecordTypes2.YGC.RecordType09> notes = new List<RecordTypes2.YGC.RecordType09>();
            List<RecordTypes.MergePops.MergePop> merges = new List<RecordTypes.MergePops.MergePop>();
            foreach (var result in this.PositiveItemsPending.Union(this.NegativeItemsPending))
            {
                notes.Add(new RecordTypes2.YGC.RecordType09()
                {
                    MASCO_FILE = result.Account.FileNo,
                    PDATE = DateTime.Now,
                    PCMT = "Garn Processed"
                });
                merges.AddRange(GenerateAnswerCodes(result));
            }

            foreach (var failure in this.FailedItemsPending)
            {
                AssetRequestAndAccount request = new AssetRequestAndAccount(failure.Item1, failure.Item2.FirstOrDefault().Account);
                var result = await GetResultAsync(request, this.ProcessingStatus, false, null, null, (list, add, prev) =>
                {
                    return new Tuple<Data.Requests.Results.Result, Dictionary<string, object>>(
                        list?.FirstOrDefault(res => string.Equals(res.Description, "Denied", StringComparison.OrdinalIgnoreCase)),
                        new Dictionary<string, object>()
                        {
                            { "Reason", string.Concat(failure.Item2.Select(eval => eval.Result.Info)) }
                        });
                });
                notes.Add(new RecordTypes2.YGC.RecordType09()
                {
                    MASCO_FILE = result.Account.FileNo,
                    PDATE = DateTime.Now,
                    PCMT = "Garn Processed"
                });
                merges.AddRange(GenerateAnswerCodes(result));
            }

            // Handle Export Records
            await GenerateExportsAsync(notes, merges, null);
            // Check for Merge Export Records AR
            if (merges.Count > 0 && this.SalesNo == 1)
            {
                // For AR (Sales 1) - Run Diary Report to Generate Garn Documents
                this.Sync.Send((callback) =>
                {
                    MessageBox.Show("After Merging the Previous File,\nAt the Main Menu Press 3-3-(Enter Password)-2-1\n\nRun the Report for Diaries 545-547\nand in QSALES01", "Court Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }, null);
            }

            // Save Updates to Database by Adding Status
            List<Exception> exceptions = new List<Exception>();
            foreach (var req in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.UpdateRegisteredAgentAsync(req.Request.ID, req.Request.RegisteredAgent);
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Processed, this.UserName, DateTime.Now, req.SelectedResult?.ID);

                    // INDIANA E-FILES IMMEDIATELY
                    if (req.Account.SalesNo == 5)
                    {
                        await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Signed, this.UserName, DateTime.Now);
                        await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Filed, this.UserName, DateTime.Now);
                    }

                    Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });
                }
                catch(Exception ex)
                {
                    exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {req.Request.Asset.Account.FileNo} / Request# {req.Request.ID}"));
                }
            }
            foreach (var req in this.NegativeItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Processed, this.UserName, DateTime.Now, req.SelectedResult?.ID);
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, Enums.Status.Rejected, this.UserName, DateTime.Now, null, req.AdditionalValues.ContainsKey("Reason") && !string.IsNullOrWhiteSpace($"{req.AdditionalValues["Reason"]}") ? $"{req.AdditionalValues["Reason"]}" : null);

                    Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });
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
                    await HandleFailures();
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
