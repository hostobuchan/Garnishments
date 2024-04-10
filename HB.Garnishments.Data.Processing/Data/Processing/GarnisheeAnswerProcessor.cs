using HB.Garnishments.Data.Communications;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing
{
    public class GarnisheeAnswerProcessor : Processor
    {
        private CourtAnswerProcessor courtAnswers { get; set; }

        public GarnisheeAnswerProcessor(SynchronizationContext sync, HubManager hubManager, string userName, byte? salesRestriction = null) : base(sync, hubManager, Enums.Status.GarnisheeResponse, userName, salesRestriction, Enums.Status.Filed, Enums.Status.HearingRequested, Enums.Status.CourtResponse)
        {
            this.courtAnswers = new CourtAnswerProcessor(sync, hubManager, userName, salesRestriction);
        }

        #region Override Result Additions to Add Court Answer & "File Marked Date" (Where Necessary)
        public override async Task AddNegativeResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            try
            {
                AssetRequestProcessingResult result = null;
                if (request.CurrentStatus.Type == Enums.Status.Filed)
                {
                    // Need Court Response
                    result = await GetResultAsync(
                        request,
                        Enums.Status.CourtResponse,
                        true,
                        new Requests.Results.Codes.MergeCodeInfo[] { new Requests.Results.Codes.MergeCodeInfo(Enums.ResultInfoType.Date, "File Marked Date") },
                        new Dictionary<string, object>() { { "Reason", "" } },
                        selectResult
                    );
                    result.AdditionalValues["Reason"] = result.SelectedResult.Description;
                    if (result != null)
                    {
                        AddPositiveItem(result);
                    }
                    else
                    {
                        return;
                    }
                }

                result = await GetResultAsync(request, this.ProcessingStatus, false, null, result?.AdditionalValues, selectResult);
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
                AssetRequestProcessingResult result = null;
                if (request.CurrentStatus.Type == Enums.Status.Filed)
                {
                    // Need Court Response
                    result = await GetResultAsync(
                        request,
                        Enums.Status.CourtResponse,
                        true,
                        new Requests.Results.Codes.MergeCodeInfo[] { new Requests.Results.Codes.MergeCodeInfo(Enums.ResultInfoType.Date, "File Marked Date") },
                        new Dictionary<string, object>() { { "Reason", "" } },
                        selectResult
                    );
                    result.AdditionalValues["Reason"] = result.SelectedResult.Description;
                    if (result != null)
                    {
                        AddPositiveItem(result);
                    }
                    else
                    {
                        return;
                    }
                }

                result = await GetResultAsync(request, this.ProcessingStatus, true, null, result?.AdditionalValues, selectResult);
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
            List<RecordTypes.CMEDI.Base.CMEDIBase> imports = new List<RecordTypes.CMEDI.Base.CMEDIBase>();
            List<Exception> exceptions = new List<Exception>();

            // Add Result Codes
            foreach (var requestResult in this.PositiveItemsPending.Union(this.NegativeItemsPending))
            {
                merges.AddRange(GenerateAnswerCodes(requestResult));
                imports.AddRange(GenerateAnswerImports(requestResult));
            }

            // Handle Export Records
            await GenerateExportsAsync(notes.OfType<RecordTypes.YGC.Base.YGCBase>().Union(diaries), merges, imports);

            // Save Updates to Database by Adding Status
            foreach (var req in this.PositiveItemsPending.Union(this.NegativeItemsPending).ToArray())
            {
                try
                {
                    await Data.DataHandler.AddStatusAsync(req.Request.ID, req.SelectedResult.Status, this.UserName, DateTime.Now, req.SelectedResult?.ID);

                    Task.Run(async () => { await CommunicateGarnUpdated(req.Request); });
                }
                catch (Exception ex)
                {
                    exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {req.Request.Asset.Account.FileNo} / Request# {req.Request.ID}"));
                }
            }
            foreach (var req in this.PositiveItemsPending.ToArray())
            {
                try
                {
                    await RemovePositiveResultAsync(req, true);
                }
                catch { }
            }
            foreach (var req in this.NegativeItemsPending.ToArray())
            {
                try
                {
                    await RemoveNegativeResultAsync(req, true);
                }
                catch { }
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
