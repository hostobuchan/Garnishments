using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Enums;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;
using System.Threading;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;

namespace HB.Garnishments.Data.Processing
{
    public abstract class Processor : IProcessor
    {
        bool disposed = false;
        public delegate void GarnProblem(Exception ex, Request request);
        public event EventHandler<Exception> CommunicationProblem;
        public event GarnProblem GarnishmentProblem;
        public event ProgressChangedEventHandler ProcessingUpdate;

        public SynchronizationContext Sync { get; private set; }
        private Communications.HubManager HubManager { get; set; }
        public BindingList<AssetRequest> ItemsPending { get; private set; } = new BindingList<AssetRequest>();
        public BindingList<AssetRequestProcessingResult> NegativeItemsPending { get; private set; } = new BindingList<AssetRequestProcessingResult>();
        public BindingList<AssetRequestProcessingResult> PositiveItemsPending { get; private set; } = new BindingList<AssetRequestProcessingResult>();
        public BindingList<Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>> FailedItemsPending { get; private set; } = new BindingList<Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>>();
        public Enums.Status ProcessingStatus { get; private set; }
        public Enums.Status[] ItemsInStatus { get; private set; }
        public BindingList<AssetRequest> WorkedItems { get; private set; } = new BindingList<AssetRequest>();
        public BindingList<AssetRequestProcessingResult> MyWorkedPositiveItems { get; private set; } = new BindingList<AssetRequestProcessingResult>();
        public BindingList<AssetRequestProcessingResult> MyWorkedNegativeItems { get; private set; } = new BindingList<AssetRequestProcessingResult>();
        public string UserName { get; private set; }
        public byte? SalesNo { get; private set; }

        /// <summary>
        /// Construct Processor
        /// </summary>
        /// <param name="sync">UI Synchronization Context</param>
        /// <param name="hubManager">Communication Manager (live updates)</param>
        /// <param name="process">Current Process Step</param>
        /// <param name="userName">Current User</param>
        /// <param name="salesRestriction">Restrict State to Process</param>
        /// <param name="status">Previous Process Step(s)</param>
        protected Processor(SynchronizationContext sync, Communications.HubManager hubManager, Enums.Status process, string userName, byte? salesRestriction = null, params Enums.Status[] status)
        {
            this.Sync = sync;
            this.UserName = userName;
            this.ProcessingStatus = process;
            this.SalesNo = salesRestriction;
            this.ItemsInStatus = status;
            if (this.Sync == null) this.Sync = SynchronizationContext.Current;
            this.HubManager = hubManager;
        }

        #region Setup / Initialization / Evaluation
        public virtual async Task LoadAsync()
        {
            List<Data.Requests.AssetRequest> requests = new List<AssetRequest>();
            foreach (var status in this.ItemsInStatus)
            {
                requests.AddRange(await Data.DataHandler.GetAccountAssetRequestsInfoInStatusAsync(status));
            }
            this.Sync?.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.RaiseListChangedEvents = false;
                    foreach (var r in requests)
                    {
                        this.ItemsPending.Add(r);
                    }
                    this.ItemsPending.RaiseListChangedEvents = true;
                    this.ItemsPending.ResetBindings();
                }

            }, null);

            await SetupCommunications();
        }

        protected async Task SetupCommunications()
        {
            this.HubManager.GarnAdded += Communicated_GarnUpdate;
            this.HubManager.GarnUpdated += Communicated_GarnUpdate;
            this.HubManager.GarnWorking += Communicated_GarnWorking;
            try
            {
                await this.HubManager.Start();//.Wait(5000);
            }
            catch (Exception ex)
            {
                CommunicationProblem?.Invoke(this, ex);
            }
        }

        public virtual async Task EvaluateExclusionsAsync()
        {
            var filenos = this.ItemsPending.GroupBy(item => item.Asset.Account.FileNo).Select(group => group.Key).ToArray();
            var evaluatees = await Accounts.EvaluateeDataHandler.GetDataHandlerAsync(filenos); //filenos.Select(fileno => Task.Run(async () => { return await EvaluationCriteria.Accounts.EvaluateeDataHandler.GetEvaluateeAccountAsync(fileno); }).Result).AsParallel().ToArray();
            var crit = await CriteriaSets.CriteriaDataHandler.CreateCriteriaDataHandlerAsync();
            lock (this)
            {
                // Disable Visual Updates
                this.ItemsPending.RaiseListChangedEvents = false;
                this.FailedItemsPending.RaiseListChangedEvents = false;

                try
                {
                    foreach (var request in this.ItemsPending.ToArray())
                    {
                        var eval = evaluatees.Accounts.Find(e => string.Equals(e.FileNo, request.Asset.Account.FileNo, StringComparison.OrdinalIgnoreCase));
                        if (eval != null)
                        {
                            if (this.SalesNo.HasValue)
                            {
                                if (eval.SalesNo != this.SalesNo)
                                {
                                    // Wrong State - Not Processing at This Time
                                    this.ItemsPending.Remove(request);
                                    continue;
                                }
                                else
                                {
                                    // Set Up Evaluation for Correct Debtor
                                    eval.Debtor = request.Asset.Debtor;
                                }
                            }

                            if ((request.OverrideByUser?.ID ?? 0) != 0)
                            {
                                // Override Criteria By User
                                this.ItemsPending.Remove(request);
                                this.ItemsPending.Add(new AssetRequestAndAccount(request, eval));
                            }
                            else
                            {
                                var result = crit.Evaluate(request, eval);
                                if (result.Count(r => !r.Result.Success) > 0)
                                {
                                    //this.Sync?.Send((o) =>
                                    //{

                                    this.ItemsPending.Remove(request);
                                    this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(request, result.Where(r => !r.Result.Success).ToArray()));

                                    //}, null);
                                }
                                else
                                {
                                    this.ItemsPending.Remove(request);
                                    this.ItemsPending.Add(new AssetRequestAndAccount(request, eval));
                                }
                            }
                        }
                        else
                        {
                            // Could Not Find Account Info
                            //this.Sync?.Send((o) =>
                            //{

                            this.ItemsPending.Remove(request);
                            this.FailedItemsPending.Add(new Tuple<AssetRequest, CriteriaSets.EvaluationResult[]>(request,
                                new CriteriaSets.EvaluationResult[]
                                {
                                    new CriteriaSets.EvaluationResult()
                                    {
                                        Account = new Accounts.EvaluateeAccount(){ FileNo = request.Asset.Account.FileNo },
                                        Set = new CriteriaSets.EvaluationSet(null, 0, "No Account"),
                                        Criteria = new CriteriaSets.EmptySimpleSet("Unable to find account"),
                                        Result = new EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval(false, "Account Not Open / Invalid"),
                                        Request = request
                                    }
                                })
                            );

                            //}, null);
                        }
                    }
                }
                catch(Exception ex)
                {
                    
                }
                finally
                {
                    // Re-Enable Visual Controls
                    this.Sync?.Send((o) =>
                    {
                        this.ItemsPending.RaiseListChangedEvents = true;
                        this.ItemsPending.ResetBindings();
                        this.FailedItemsPending.RaiseListChangedEvents = true;
                        this.FailedItemsPending.ResetBindings();
                    }, null);
                }
            }
        }
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////
        /// THESE MUST BE DONE WITH MANUAL "SEND" COMMANDS - OTHERWISE, WILL CREATE DEADLOCKS ///
        /////////////////////////////////////////////////////////////////////////////////////////
        #region Communicated Updates
        private void Communicated_GarnWorking(int requestId, bool working, string userName)
        {
            lock (this)
            {
                try
                {
                    if (UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)) return;
                    if (working) // Started Working
                    {
                        if (this.ItemsPending.Count(asset => asset.ID == requestId) > 0) // Not Worked by Current User
                        {
                            var request = this.ItemsPending.FirstOrDefault(asset => asset.ID == requestId);
                            // Add to Worked by Other Party
                            this.Sync.Send((o) =>
                            {
                                this.ItemsPending.Remove(request);
                                this.WorkedItems.Add(request);
                            }, null);
                        }
                        else if (this.PositiveItemsPending.Count(asset => asset.Request.ID == requestId) > 0) // Worked by Current User - Good Result
                        {
                            var request = this.PositiveItemsPending.FirstOrDefault(asset => asset.Request.ID == requestId);
                            // Send Message to User
                            GarnishmentProblem?.Invoke(new Exception($"Another User Has Begun Working This Request.  It Has Been Removed from Your Worklist.\r\n\r\nFileNo: {request.Request.Asset.Account.FileNo}\r\nType: {request.Request.Asset.Type}"), request.Request);
                            // Add to Worked by Other Worklist
                            this.Sync.Send((o) =>
                            {
                                this.PositiveItemsPending.Remove(request);
                                this.MyWorkedPositiveItems.Add(request);
                            }, null);
                        }
                        else if (this.NegativeItemsPending.Count(asset => asset.Request.ID == requestId) > 0) // Worked by Current User - Bad Result
                        {
                            var request = this.NegativeItemsPending.FirstOrDefault(asset => asset.Request.ID == requestId);
                            // Send Message to User
                            GarnishmentProblem?.Invoke(new Exception($"Another User Has Begun Working This Request.  It Has Been Removed from Your Worklist.\r\n\r\nFileNo: {request.Request.Asset.Account.FileNo}\r\nType: {request.Request.Asset.Type}"), request.Request);
                            // Add to Worked by Other Worklist
                            this.Sync.Send((o) =>
                            {
                                this.NegativeItemsPending.Remove(request);
                                this.MyWorkedNegativeItems.Add(request);
                            }, null);
                        }
                    }
                    else // No Longer Working
                    {
                        var newRequest = Data.DataHandler.GetAccountAssetRequestInfoByIdAsync(requestId).Result;
                        if (this.ItemsInStatus.Contains(newRequest.CurrentStatus.Type))
                        {
                            if (this.WorkedItems.Count(asset => asset.ID == requestId) > 0) // Not Worked by Current User
                            {
                                var request = this.WorkedItems.FirstOrDefault(asset => asset.ID == requestId);
                                // Remove from Worked by Other Party
                                this.Sync.Send((o) =>
                                {
                                    this.WorkedItems.Remove(request);
                                    this.ItemsPending.Add(request);
                                }, null);
                            }
                            else if (this.MyWorkedPositiveItems.Count(asset => asset.Request.ID == requestId) > 0) // Worked by Current User - Good Result
                            {
                                var request = this.MyWorkedPositiveItems.FirstOrDefault(asset => asset.Request.ID == requestId);
                                // Send Message to User
                                GarnishmentProblem?.Invoke(new Exception($"Another User Has Stopped Working This Request.  It Has Been Replaced into Your Worklist.\r\n\r\nFileNo: {request.Request.Asset.Account.FileNo}\r\nType: {request.Request.Asset.Type}"), request.Request);
                                // Add to Worked by Other Worklist
                                this.Sync.Send((o) =>
                                {
                                    this.MyWorkedPositiveItems.Remove(request);
                                    this.PositiveItemsPending.Add(request);
                                }, null);
                            }
                            else if (this.MyWorkedNegativeItems.Count(asset => asset.Request.ID == requestId) > 0) // Worked by Current User - Bad Result
                            {
                                var request = this.MyWorkedNegativeItems.FirstOrDefault(asset => asset.Request.ID == requestId);
                                // Send Message to User
                                GarnishmentProblem?.Invoke(new Exception($"Another User Has Stopped Working This Request.  It Has Been Replaced into Your Worklist.\r\n\r\nFileNo: {request.Request.Asset.Account.FileNo}\r\nType: {request.Request.Asset.Type}"), request.Request);
                                // Add to Worked by Other Worklist
                                this.Sync.Send((o) =>
                                {
                                    this.MyWorkedNegativeItems.Remove(request);
                                    this.NegativeItemsPending.Add(request);
                                }, null);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void Communicated_GarnUpdate(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId, int requestId)
        {
            lock (this)
            {
                try
                {
                    var newRequestInfo = Data.DataHandler.GetAccountAssetRequestInfoByIdAsync(requestId).Result;
                    var eval = Data.Accounts.EvaluateeDataHandler.GetEvaluateeAccountAsync(fileNo).Result;
                    var newRequest = new Data.Requests.AssetRequestAndAccount(newRequestInfo, eval);
                    if (this.ItemsInStatus.Contains(newRequest.CurrentStatus.Type))
                    {
                        if (this.ItemsPending.Count(request => request.ID == requestId) > 0)
                        {
                            // Nothing Done Yet
                            this.Sync.Send((o) =>
                            {
                                var request = this.ItemsPending.FirstOrDefault(req => req.ID == requestId);
                                int index = this.ItemsPending.IndexOf(request);
                                this.ItemsPending.Remove(request);
                                this.ItemsPending.Insert(index, newRequest);
                            }, null);
                        }
                        else if (this.WorkedItems.Count(request => request.ID == requestId) > 0)
                        {
                            // Another User Working Item
                            this.Sync.Send((o) =>
                            {
                                var request = this.WorkedItems.FirstOrDefault(req => req.ID == requestId);
                                this.WorkedItems.Remove(request);
                                this.WorkedItems.Add(newRequest);
                            }, null);
                        }
                        else if (this.PositiveItemsPending.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Currently Working - Positive Result
                            var cur = this.PositiveItemsPending.FirstOrDefault(request => request.Request.ID == requestId);
                            var index = this.PositiveItemsPending.IndexOf(cur);
                            AssetRequestProcessingResult newResult = new AssetRequestProcessingResult(newRequest)
                            {
                                SelectedResult = cur.SelectedResult
                            };

                            this.Sync.Send((o) =>
                            {
                                this.PositiveItemsPending.Remove(cur);
                                this.PositiveItemsPending.Insert(index, newResult);
                            }, null);
                        }
                        else if (this.NegativeItemsPending.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Currently Working - Negative Result
                            var cur = this.NegativeItemsPending.FirstOrDefault(request => request.Request.ID == requestId);
                            var index = this.NegativeItemsPending.IndexOf(cur);
                            AssetRequestProcessingResult newResult = new AssetRequestProcessingResult(newRequest)
                            {
                                SelectedResult = cur.SelectedResult
                            };

                            this.Sync.Send((o) =>
                            {
                                this.NegativeItemsPending.Remove(cur);
                                this.NegativeItemsPending.Insert(index, newResult);
                            }, null);
                        }
                        else if (this.MyWorkedPositiveItems.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Another user Working Item - Removed from My List
                            var cur = this.MyWorkedPositiveItems.FirstOrDefault(request => request.Request.ID == requestId);
                            AssetRequestProcessingResult newResult = new AssetRequestProcessingResult(newRequest)
                            {
                                SelectedResult = cur.SelectedResult
                            };

                            this.Sync.Send((o) =>
                            {
                                this.MyWorkedPositiveItems.Remove(cur);
                                this.MyWorkedPositiveItems.Add(newResult);
                            }, null);
                        }
                        else if (this.MyWorkedNegativeItems.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Another user working Item - Remove from My List
                            var cur = this.MyWorkedNegativeItems.FirstOrDefault(request => request.Request.ID == requestId);
                            AssetRequestProcessingResult newResult = new AssetRequestProcessingResult(newRequest)
                            {
                                SelectedResult = cur.SelectedResult
                            };

                            this.Sync.Send((o) =>
                            {
                                this.MyWorkedNegativeItems.Remove(cur);
                                this.MyWorkedNegativeItems.Add(newResult);
                            }, null);
                        }
                        else
                        {
                            // Check if Sales No. Restriction
                            if (this.SalesNo.HasValue && newRequest.SalesNo != this.SalesNo.Value)
                            {
                                // Do Nothing - Not Applicable
                                return;
                            }

                            // Not Currently In Worklist - ADD
                            this.Sync.Send((o) =>
                            {
                                this.ItemsPending.Add(newRequest);
                            }, null);
                        }
                    }
                    else // No Longer Applicable to this 
                    {
                        if (this.ItemsPending.Count(request => request.ID == requestId) > 0)
                        {
                            // Nothing Done Yet
                            this.Sync.Send((o) =>
                            {
                                this.ItemsPending.Remove(this.ItemsPending.FirstOrDefault(request => request.ID == requestId));
                            }, null);
                        }
                        else if (this.WorkedItems.Count(request => request.ID == requestId) > 0)
                        {
                            // Another User Working Item
                            this.Sync.Send((o) =>
                            {
                                this.WorkedItems.Remove(this.WorkedItems.FirstOrDefault(request => request.ID == requestId));
                            }, null);
                        }
                        else if (this.PositiveItemsPending.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Currently Working - Positive Result
                            GarnishmentProblem?.Invoke(new Exception($"This Garn Request Has Been Updated By Another User.\r\nIt Is No Longer In This Status\r\n\r\nFileNo: {newRequest.Asset.Account.FileNo}\r\nType: {newRequest.Asset.Type}"), newRequest);
                            this.Sync.Send((o) =>
                            {
                                this.PositiveItemsPending.Remove(this.PositiveItemsPending.FirstOrDefault(request => request.Request.ID == newRequest.ID));
                            }, null);
                        }
                        else if (this.NegativeItemsPending.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Currently Working - Negative Result
                            GarnishmentProblem?.Invoke(new Exception($"This Garn Request Has Been Updated By Another User.\r\nIt Is No Longer In This Status\r\n\r\nFileNo: {newRequest.Asset.Account.FileNo}\r\nType: {newRequest.Asset.Type}"), newRequest);
                            this.Sync.Send((o) =>
                            {
                                this.NegativeItemsPending.Remove(this.NegativeItemsPending.FirstOrDefault(request => request.Request.ID == newRequest.ID));
                            }, null);
                        }
                        else if (this.MyWorkedPositiveItems.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Another user Working Item - Removed from My List
                            this.Sync.Send((o) =>
                            {
                                this.MyWorkedPositiveItems.Remove(this.MyWorkedPositiveItems.FirstOrDefault(request => request.Request.ID == newRequest.ID));
                            }, null);
                        }
                        else if (this.MyWorkedNegativeItems.Count(request => request.Request.ID == requestId) > 0)
                        {
                            // Another user working Item - Remove from My List
                            this.Sync.Send((o) =>
                            {
                                this.MyWorkedNegativeItems.Remove(this.MyWorkedNegativeItems.FirstOrDefault(request => request.Request.ID == newRequest.ID));
                            }, null);
                        }
                    }
                }
                catch { }
            }
        }
        #endregion

        #region Synchronously Add / Remove BindingList<> Items
        private void AddPendingItem(AssetRequest item)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Add(item);
                }

            }, null);
        }
        private void RemovePendingItem(AssetRequest item)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Remove(item);
                }
            }, null);
        }
        private void AddWorkedItem(AssetRequest item)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Remove(item);
                    this.WorkedItems.Add(item);
                }
            }, null);
        }
        private void RemoveWorkedItem(AssetRequest item)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Add(item);
                    this.WorkedItems.Remove(item);
                }
            }, null);
        }
        private void AddWorkedItem(AssetRequestProcessingResult item, bool wasPositive)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    if (wasPositive)
                    {
                        this.PositiveItemsPending.Remove(item);
                        this.MyWorkedPositiveItems.Add(item);
                    }
                    else
                    {
                        this.NegativeItemsPending.Remove(item);
                        this.MyWorkedNegativeItems.Add(item);
                    }
                }
            }, null);
        }
        private void RemoveWorkedItem(AssetRequestProcessingResult item, bool wasPositive)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    if (wasPositive)
                    {
                        this.MyWorkedPositiveItems.Remove(item);
                        this.PositiveItemsPending.Add(item);
                    }
                    else
                    {
                        this.MyWorkedNegativeItems.Remove(item);
                        this.NegativeItemsPending.Add(item);
                    }
                }
            }, null);
        }
        //////////////////////////////////////////////
        /// THERE's A POSSIBILITY FOR TIMING ISSUE ///
        //////////////////////////////////////////////
        protected void AddPositiveItem(AssetRequestProcessingResult item, int? index = null)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Remove(item.Request);
                    if (index.HasValue)
                    {
                        this.PositiveItemsPending.Insert(index.Value, item);
                    }
                    else
                    {
                        this.PositiveItemsPending.Add(item);
                    }
                }
            }, null);
        }
        private void RemovePositiveItem(AssetRequestProcessingResult item, bool permanent = false)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    if (!permanent) this.ItemsPending.Add(new AssetRequestAndAccount(item.Request, item.Account));
                    this.PositiveItemsPending.Remove(item);
                }
            }, null);
        }
        //////////////////////////////////////////////
        /// THERE's A POSSIBILITY FOR TIMING ISSUE ///
        //////////////////////////////////////////////
        protected void AddNegativeItem(AssetRequestProcessingResult item, int? index = null)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    this.ItemsPending.Remove(item.Request);
                    if (index.HasValue)
                    {
                        this.NegativeItemsPending.Insert(index.Value, item);
                    }
                    else
                    {
                        this.NegativeItemsPending.Add(item);
                    }
                }
            }, null);
        }
        private void RemoveNegativeItem(AssetRequestProcessingResult item, bool permanent = false)
        {
            this.Sync.Send((o) =>
            {
                lock (this)
                {
                    if (!permanent) this.ItemsPending.Add(new AssetRequestAndAccount(item.Request, item.Account));
                    this.NegativeItemsPending.Remove(item);
                }
            }, null);
        }
        #endregion

        #region Add / Remove Selections (Synchronous) **CALLS ASYNC SYNCHRONOUSLY**
        public virtual void AddNegativeResult(AssetRequestAndAccount request, Func<IEnumerable<Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Result, Dictionary<string, object>>> selectResult) { Task.Run(async () => await AddNegativeResultAsync(request, selectResult)).Wait(); }
        public virtual void AddPositiveResult(AssetRequestAndAccount request, Func<IEnumerable<Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Result, Dictionary<string, object>>> selectResult){ Task.Run(async () => await AddPositiveResultAsync(request, selectResult)).Wait(); }
        public virtual void RemoveNegativeResult(AssetRequestProcessingResult request, bool permanent = false) { Task.Run(async () => await RemoveNegativeResultAsync(request, permanent)).Wait(); }
        public virtual void RemovePositiveResult(AssetRequestProcessingResult request, bool permanent = false) { Task.Run(async () => await RemovePositiveResultAsync(request, permanent)).Wait(); }
        #endregion

        #region Add / Remove Selections (Asynchronous) **VIRTUAL**

        public virtual async Task AddNegativeResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            try
            {
                var result = await GetResultAsync(request, this.ProcessingStatus, false, null, null, selectResult);
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

        public virtual async Task AddPositiveResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            try
            {
                var result = await GetResultAsync(request, this.ProcessingStatus, true, null, null, selectResult);
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

        public virtual async Task RemoveNegativeResultAsync(AssetRequestProcessingResult request, bool permanent = false)
        {
            RemoveNegativeItem(request, permanent);
            await RemoveWorkingStatusAsync(request.Request);
        }

        public virtual async Task RemovePositiveResultAsync(AssetRequestProcessingResult request, bool permanent = false)
        {
            RemovePositiveItem(request, permanent);
            await RemoveWorkingStatusAsync(request.Request);
        }
        #endregion



        protected async Task<AssetRequestProcessingResult> GetResultAsync(AssetRequestAndAccount request, Enums.Status status, bool good, IEnumerable<Requests.Results.Codes.MergeCodeInfo> additions, Dictionary<string, object> previousValues, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult)
        {
            AssetRequestProcessingResult result = null;
            try
            {
                ResultCodeHandler possResults = null;
                Tuple<Result, Dictionary<string, object>> selResult = null;
                try
                {
                    possResults = await DataHandler.GetResultCodeHandlerAsync(status, request.Asset.Type);
                    selResult = selectResult?.Invoke(possResults.Results?.Where(res => res.Good == good), additions, previousValues);
                }
                catch (NotImplementedException nex)
                {
                    return null;
                }
                catch
                {
                    selResult = selectResult?.Invoke(null, additions, previousValues);
                }
                // Basic Result Data & Entered Info
                result = new AssetRequestProcessingResult(request)
                {
                    SelectedResult = selResult?.Item1,
                    AdditionalValues = selResult?.Item2 ?? new Dictionary<string, object>()
                };
                // Referenced Data
                if (result.SelectedResult != null)
                {
                    var addlValues = GetReferencedData(result);
                    if (result.AdditionalValues == null)
                    {
                        result.AdditionalValues = addlValues;
                    }
                    else
                    {
                        foreach (var val in addlValues)
                        {
                            result.AdditionalValues.Add(val.Key, val.Value);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public abstract Task ProcessResultsAsync();
        public abstract Task CancelResultsAsync();

        protected async Task GenerateExportsAsync(IEnumerable<RecordTypes.YGC.Base.YGCBase> ygc, IEnumerable<RecordTypes.MergePops.MergePop> merges, IEnumerable<RecordTypes.CMEDI.Base.CMEDIBase> imports)
        {
            // Check for YGC Export Records
            if (ygc.Count() > 0)
            {
                try
                {
                    await RecordTypes.Output.Send_YGC_Imp_Async(ygc);
                }
                catch { }
            }
            // Check for Merge Export Records
            if (merges.Count() > 0 || imports?.Count() > 0)
            {
                // Save Export Records
                System.IO.DirectoryInfo directory = null;
                string mergeFileLocation = string.Empty;
                string importFileLocation = string.Empty;

                // Ask Where to Save Output Files - And Don't Take "No" for an Answer
                this.Sync.Send((callback) =>
                {
                    CommonOpenFileDialog folderDialog = new CommonOpenFileDialog()
                    {
                        EnsurePathExists = true,
                        IsFolderPicker = true,
                        Title = "Select Folder to Save Merge Files"
                    };
                    while (folderDialog.ShowDialog() != CommonFileDialogResult.Ok) { }

                    directory = new System.IO.DirectoryInfo(folderDialog.FileName);

                }, null);

                // Get Unused Output File Name
                while (string.IsNullOrEmpty(mergeFileLocation) || System.IO.File.Exists(mergeFileLocation) || string.IsNullOrEmpty(importFileLocation) || System.IO.File.Exists(importFileLocation))
                {
                    mergeFileLocation = System.IO.Path.Combine(directory.FullName, $"GarnMerge_{DateTime.Now:yyyyMMdd-HHmmss}.txt");
                    importFileLocation = System.IO.Path.Combine(directory.FullName, $"GarnMerge_CMEDI_{DateTime.Now:yyyyMMdd-HHmmss}.txt");
                }
                // Write Output File(s)
                if (merges.Count() > 0)
                {
                    using (RecordTypes.MergePops.FileWriter writer = new RecordTypes.MergePops.FileWriter(mergeFileLocation))
                    {
                        writer.WriteFile(merges);
                    }
                }
                if (imports?.Count() > 0)
                {
                    using (RecordTypes.CMEDI.FileWriter writer = new RecordTypes.CMEDI.FileWriter(importFileLocation))
                    {
                        writer.WriteFile(imports);
                    }
                }

                // Alert User to Import Records
                if (merges.Count() > 0)
                {
                    this.Sync.Send((callback) =>
                    {
                        MessageBox.Show($"Please Open CLS and Press 1-3-6-5-4\n\nThen Press F5 to Search For File\nLocate File: {mergeFileLocation}\nClick Open to Select File\nPress Enter to Add Documents\n\nThen Escape Back To Main Menu\nPress 3-8-3 to Merge All Documents", "CLS Document Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }, null);
                }
                if (imports?.Count() > 0)
                {
                    this.Sync.Send((callback) =>
                    {
                        MessageBox.Show($"Please Open CLS and Press 4-2-1-1 (Password is POTUS if needed)\n\nThen Select The \"Collection-Master EDI Standard\"\n\nEnter \"1\" as the Starting File Number\n\nThen, Under EDI File Name, Search For File\nLocate File: {importFileLocation}\nClick Open to Select File\nPress Enter to Import EDI Information\n\nPress Escape to Return to Main Menu", "CLS EDI Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }, null);
                }
            }
        }
        protected async Task HandleFailures(bool expunge = true)
        {
            List<Exception> exceptions = new List<Exception>();
            var failureTable = Rejects.ReportGenerator.CreateRejectTable();
            failureTable.TableName = "Exceptions Report";
            foreach (var failure in this.FailedItemsPending)
            {
                Rejects.ReportGenerator.AddRejectToTable(failureTable, failure);
                if (expunge || string.Equals(failure.Item2.FirstOrDefault().Set?.Description, "No Account", StringComparison.OrdinalIgnoreCase))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var eval in failure.Item2)
                    {
                        sb.Append($"{eval.Set.Description} ({eval.Result.Info}), ");
                    }
                    try
                    {
                        await Data.DataHandler.AddStatusAsync(failure.Item1.ID, Enums.Status.Rejected, this.UserName, DateTime.Now, null, sb.Length > 2 ? sb.ToString().Substring(0, sb.Length - 2) : null);

                        Task.Run(async () => { await CommunicateGarnUpdated(failure.Item1); });
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(new Exception($"Failed to Update Garn Info: FileNo# {failure.Item1.Asset.Account.FileNo} / Request# {failure.Item1.ID}"));
                    }
                }
            }
            ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();
            xlApp.xlBook.AddWorksheetFromTable(failureTable);
            xlApp.ShowWorkBook();
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

        #region Look Up Referenced Data

        protected Dictionary<string, object> GetReferencedData(AssetRequestProcessingResult result)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            var fieldValues = result.SelectedResult.MergeCodes.SelectMany(merge => merge.DebtorCodes.SelectMany(debtor => debtor.Codes.SelectMany(code => code.Values))).GroupBy(value => value.Info.Value);
            foreach (var fv in fieldValues)
            {
                var fieldValue = fv.FirstOrDefault();
                if (result.AdditionalValues.ContainsKey(fieldValue.Info.Value)) continue; // This Data Has Already Been Entered / Overridden
                if (!string.IsNullOrWhiteSpace(fieldValue.Info.ReferenceObject) && !string.IsNullOrWhiteSpace(fieldValue.Info.ReferenceParameter))
                {
                    var objArry = fieldValue.Info.ReferenceObject.Split('.');
                    object objRef = result;
                    for (int i = 0; i < objArry.Length; i++)
                    {
                        PropertyInfo info = objRef.GetType().GetProperty(objArry[i]);
                        objRef = info.GetValue(objRef);
                    }
                    PropertyInfo info2 = objRef.GetType().GetProperty(fieldValue.Info.ReferenceParameter);
                    values.Add(fieldValue.Info.Value, info2.GetValue(objRef));
                }
            }
            return values;
        }

        #endregion

        #region Generate Export Codes

        public static IEnumerable<RecordTypes.MergePops.MergePop> GenerateAnswerCodes(Requests.Results.AssetRequestProcessingResult result)
        {
            foreach (var code in result.SelectedResult[result.Account.SalesNo]?[result.Request.Asset.Debtor]?.Codes ?? new List<Requests.Results.Codes.MergeCode>())
            {
                // Check That It's Not and EDI Code
                if (!System.Text.RegularExpressions.Regex.IsMatch(code.XCode, @"[\w]+\."))
                {
                    var xcode = new RecordTypes.MergePops.MergePop()
                    {
                        FILENO = result.Account.FileNo,
                        LLCODE = code.XCode
                    };
                    foreach (var val in code.Values)
                    {
                        xcode.GetType().GetProperty(val.Field.MergeField).SetValue(xcode, GetAdditionalValueString(result.AdditionalValues[val.Info.Value]));
                    }
                    yield return xcode;
                }
            }
        }
        public static IEnumerable<RecordTypes.CMEDI.Base.CMEDIBase> GenerateAnswerImports(Requests.Results.AssetRequestProcessingResult result)
        {
            List<RecordTypes.CMEDI.Base.CMEDIBase> importRecords = new List<RecordTypes.CMEDI.Base.CMEDIBase>();
            foreach (var code in result.SelectedResult[result.Account.SalesNo]?[result.Request.Asset.Debtor]?.Codes ?? new List<Requests.Results.Codes.MergeCode>())
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(code.XCode, @"(?<EDI>[\w]+)\.(?<Record>[\d]+)\.(?<Field>[\w]+)");
                // Check That It's Not and EDI Code
                if (match.Success && string.Equals(match.Groups["EDI"].Value, "CMEDI", StringComparison.OrdinalIgnoreCase))
                {
                    var ediRecord = System.Reflection.TypeInfo.GetType($"RecordTypes.CMEDI.Record{match.Groups["Record"].Value}").GetConstructor(null).Invoke(null);
                    
                    if (ediRecord is RecordTypes.CMEDI.Base.CMEDIAccountRecord)
                    {
                        ((RecordTypes.CMEDI.Base.CMEDIAccountRecord)ediRecord).FIRM_FILENO = result.Account.FileNo;
                    }

                    var debtorProp = ediRecord.GetType().GetProperty("NUMBER");
                    if (debtorProp != null)
                    {
                        debtorProp.SetValue(ediRecord, result.Account.Debtor.ToString());
                    }
                    foreach (var val in code.Values)
                    {
                        ediRecord.GetType().GetProperty(match.Groups["Field"].Value).SetValue(ediRecord, GetAdditionalValueString(result.AdditionalValues[val.Info.Value]));
                    }

                    importRecords.Add(ediRecord as RecordTypes.CMEDI.Base.CMEDIBase);
                }
            }

            List<RecordTypes.CMEDI.Base.CMEDIBase> compiledImportRecords = new List<RecordTypes.CMEDI.Base.CMEDIBase>();
            foreach (var rec in importRecords.GroupBy(r => r.GetType()))
            {
                var newRec = rec.Key.GetConstructor(null).Invoke(null);
                foreach (var iRec in rec)
                {
                    foreach (var prop in iRec.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        var val = prop.GetValue(iRec, null);
                        if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                        {
                            prop.SetValue(newRec, val);
                        }
                    }
                }
                compiledImportRecords.Add(newRec as RecordTypes.CMEDI.Base.CMEDIBase);
            }

            return compiledImportRecords;
        }

        public static string GetAdditionalValueString(object val, string dateFormat = "M/d/yyyy")
        {
            if (val is string)
            {
                return (string)val;
            }
            else if (val is DateTime)
            {
                return ((DateTime)val).ToString(dateFormat);
            }
            else if (val is decimal)
            {
                return ((decimal)val).ToString();
            }
            else
            {
                return val.ToString();
            }
        }

        #endregion



        protected virtual async Task SetWorkingStatusAsync(AssetRequestProcessingResult request)
        {
            await Task.Run(() =>
            {
                try
                {
                    this.HubManager.WorkingGarn(request.Request, true, UserName);
                }
                catch (Exception ex)
                {
                    CommunicationProblem?.Invoke(this, ex);
                }
            });            
        }
        protected virtual async Task RemoveWorkingStatusAsync(AssetRequest request)
        {
            await Task.Run(() =>
            {
                try
                {
                    this.HubManager.WorkingGarn(request, false, UserName);
                }
                catch (Exception ex)
                {
                    CommunicationProblem?.Invoke(this, ex);
                }
            });
        }
        protected virtual async Task CommunicateGarnUpdated(AssetRequest request)
        {
            await Task.Run(() =>
            {
                try
                {
                    //this.HubManager.GarnUpdate(request.Asset.Account.ID, request.Asset.Account.FileNo, request.Asset.Debtor, request.Asset.AssetName.ID, request.AssetInfoID, request.ID);
                }
                catch (Exception ex)
                {
                    //CommunicationProblem?.Invoke(this, ex);
                }
            });
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        ~Processor()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            this.HubManager.GarnAdded -= Communicated_GarnUpdate;
            this.HubManager.GarnUpdated -= Communicated_GarnUpdate;
            this.HubManager.GarnWorking -= Communicated_GarnWorking;
            this.ItemsPending.Clear();

            foreach (var request in this.PositiveItemsPending.Union(this.NegativeItemsPending))
            {
                Task.Run(async () => { try { await RemoveWorkingStatusAsync(request.Request); } catch { } });
            }
            this.PositiveItemsPending.Clear();
            this.NegativeItemsPending.Clear();

            disposed = true;
        }
        #endregion
    }
}
