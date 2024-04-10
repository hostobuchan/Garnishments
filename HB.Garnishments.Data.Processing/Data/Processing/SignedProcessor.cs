using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HB.Garnishments.Data.Communications;
using HB.Garnishments.Data.Enums;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;

namespace HB.Garnishments.Data.Processing
{
    public class SignedProcessor : NoNegativeResultProcessor
    {
        Accounts.Venue[] Venues { get; set; }
        Assets.Base.RegisteredAgent[] Agents { get; set; }

        public SignedProcessor(SynchronizationContext sync, HubManager hubManager, IEnumerable<Accounts.Venue> venues, IEnumerable<Assets.Base.RegisteredAgent> agents, string userName, byte? salesRestriction = null) : base(sync, hubManager, Enums.Status.Signed, userName, salesRestriction, Enums.Status.Processed)
        {
            this.Venues = venues.ToArray();
            this.Agents = agents.ToArray();
        }

        

        public override async Task ProcessResultsAsync()
        {
            List<Exception> exceptions = new List<Exception>();
            List<RecordTypes2.YGC.RecordType09> notes = new List<RecordTypes2.YGC.RecordType09>();
            List<RecordTypes.MergePops.MergePop> merges = new List<RecordTypes.MergePops.MergePop>();
            List<Fees.Check> checks = new List<Fees.Check>();
            Fees.CourtFeeHandler feeHandler = await Fees.CourtFeeHandler.CreateCourtFeeHandler();

            // Create Cost Checks
            foreach (var request in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    notes.Add(new RecordTypes2.YGC.RecordType09()
                    {
                        MASCO_FILE = request.Account.FileNo,
                        PDATE = DateTime.Now,
                        PCMT = "Writ Sent to Accounting"
                    });
                    checks.AddRange(
                        feeHandler.GetCostCheck(
                            this.Sync,
                            request,
                            this.Venues.FirstOrDefault(ven => ven.VenueNo == request.Account.Venue),
                            this.Agents.FirstOrDefault(agent => agent.ID == request.Request.RegisteredAgent && agent.AssetType == request.Request.Asset.Type)
                        )
                    );
                }
                catch (Exception ex)
                {
                    await RemovePositiveResultAsync(request);
                    this.PositiveItemsPending.Remove(request);
                    this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(request.Request, new CriteriaSets.EvaluationResult[]{
                        new CriteriaSets.EvaluationResult()
                        {
                            Account = request.Account,
                            Set = new CriteriaSets.EvaluationSet(null, 0, "Check Creation"),
                            Criteria = new CriteriaSets.EmptySimpleSet("Unable to Create Accounting Checks"),
                            Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, ex.Message),
                            Request = request.Request
                        }
                    }));
                }
            }

            // Export Files
            if (notes.Count > 0)
            {
                try
                {
                    RecordTypes.Output.Send_YGC_Imp(notes);
                }
                catch { }
            }

            // Create Cost Spreadsheet
            if (checks.Count > 0)
            {
                // Save Export Records
                System.IO.DirectoryInfo directory = null;
                string costFileLocation = string.Empty;

                // Select Where to Export To....
                this.Sync.Send((callback) =>
                {
                    Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog folderDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog()
                    {
                        EnsurePathExists = true,
                        IsFolderPicker = true,
                        Title = "Select Folder to Save Cost Spreadsheet"
                    };
                    while (folderDialog.ShowDialog() != Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) { }

                    directory = new System.IO.DirectoryInfo(folderDialog.FileName);

                }, null);

                while (string.IsNullOrEmpty(costFileLocation) || System.IO.File.Exists(costFileLocation))
                {
                    costFileLocation = System.IO.Path.Combine(directory.FullName, $"GARNS_{DateTime.Now:yyyyMMddHHmmss}.csv");
                }

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(costFileLocation))
                {
                    await writer.WriteLineAsync("FILENO,CHECK_AMT,COMMENT,RET_CODE");
                    foreach (var check in checks)
                    {
                        await writer.WriteLineAsync($"{check.FileNo},${check.Amount:N2},{check.Comment.Replace(',',' ')},{check.CostCode}");
                    }

                    await writer.FlushAsync();
                }
                // Open Spreadsheet
                ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel(costFileLocation);
                xlApp.ShowWorkBook();
            }

            // Save Updates to Database
            foreach (var request in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(request.Request.ID, Enums.Status.Signed, this.UserName, DateTime.Now);
                    try
                    {
                        await RemovePositiveResultAsync(request, true);

                        Task.Run(async () => { await CommunicateGarnUpdated(request.Request); });
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {request.Request.Asset.Account.FileNo} / Request# {request.Request.ID}"));
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
