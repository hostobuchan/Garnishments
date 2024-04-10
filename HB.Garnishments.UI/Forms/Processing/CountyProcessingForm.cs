using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HB.Garnishments.Data.Extensions;

namespace HB.Garnishments.UI.Forms.Processing
{
    public partial class CountyProcessingForm : Form
    {
        bool CommunicationFailed = false;
        Data.Processing.IProcessor Processor { get; set; }

        BindingList<IGrouping<DateTime?, Data.Requests.AssetRequestAndAccount>> Requests_Date { get; set; } = new BindingList<IGrouping<DateTime?, Data.Requests.AssetRequestAndAccount>>();
        BindingList<IGrouping<string, Data.Requests.AssetRequestAndAccount>> Requests_County { get; set; } = new BindingList<IGrouping<string, Data.Requests.AssetRequestAndAccount>>();
        BindingList<Data.Requests.AssetRequestAndAccount> Requests { get; set; } = new BindingList<Data.Requests.AssetRequestAndAccount>();

        RegisteredAgentSelectionForm SelectionForm { get; set; }

        DateTime? SelectedDate { get; set; }
        string SelectedCounty { get; set; }
        Data.Requests.AssetRequestAndAccount SelectedRequest { get; set; }

        bool _Updating = false;

        public CountyProcessingForm(Data.Processing.IProcessor processor)
        {
            this.Processor = processor;
            this.Processor.ItemsPending.ListChanged += ItemsPending_ListChanged;
            this.Processor.GarnishmentProblem += Processor_GarnishmentProblem;
            this.Processor.CommunicationProblem += Processor_CommunicationProblem;
            InitializeComponent();
        }

        #region Processor Handling
        private void Processor_CommunicationProblem(object sender, Exception e)
        {
            try
            {
                if (InvokeRequired)
                {
                    if (IsHandleCreated)
                    {
                        this.Invoke(new EventHandler<Exception>(Processor_CommunicationProblem), sender, e);
                    }
                    return;
                }

                if (!this.CommunicationFailed)
                {
                    this.CommunicationFailed = true;
                    this.lblCommunicationStatus.Image = HB.Garnishments.UI.Properties.Resources.alert;
                    this.lblStatus.Text += " - No Communication!";
                }
                this.lblCommunicationStatus.AutoToolTip = true;
                this.lblCommunicationStatus.ToolTipText = e.Message;
            }
            catch { }
        }

        private void Processor_GarnishmentProblem(Exception ex, Data.Requests.Request request)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Data.Processing.Processor.GarnProblem(Processor_GarnishmentProblem), ex, request);
                }
                return;
            }

            MessageBox.Show($"A Work Item Has Changed Status\r\n\r\n{ex.Message}");
        }
        #endregion

        #region Initialization
        private void CountyProcessingForm_Load(object sender, EventArgs e)
        {
            this.lstDates.DataSource = this.Requests_Date;
            this.lstDates.ValueMember = "Key";
            this.lstDates.DisplayMember = "Key";
            this.lstCounties.DataSource = this.Requests_County;
            this.lstCounties.ValueMember = "Key";
            this.lstCounties.DisplayMember = "Key";
            this.lstRequests.DataSource = this.Requests;
            this.SelectionForm = new RegisteredAgentSelectionForm(null);
            this.SelectionForm.AgentSelected += SelectionForm_AgentSelected;
            this.SelectionForm.RequestDenied += SelectionForm_RequestDenied;
            this.SelectionForm.RequestSkipped += SelectionForm_RequestSkipped;
            UpdateStatus("Loading...", false);
        }

        private async void CountyProcessingForm_Shown(object sender, EventArgs e)
        {
            await this.Processor.LoadAsync().ContinueWith(async (task) =>
            {
                if (task.IsFaulted)
                {
                    UpdateStatus(this.CommunicationFailed ? "Failure!!! - Unable to Load Data! - No Communication!" : "Failure!!! - Unable to Load Data!", true);
                }
                else
                {
                    await this.Processor.EvaluateExclusionsAsync().ContinueWith((task2) =>
                    {
                        if (task2.IsFaulted)
                        {
                            UpdateStatus(this.CommunicationFailed ? "Failure!!! - Unable to Evaluate Accounts! - No Communication!" : "Failure!!! - Unable to Evaluate Accounts!", true);
                        }
                        else
                        {
                            UpdateDates();
                            UpdateStatus(this.CommunicationFailed ? "Ready! - No Communication!" : "Ready!", true);
                        }
                    });
                }
            });
        }
        #endregion

        #region User Interface

        #region List Changed / Selection Changed
        private void ItemsPending_ListChanged(object sender, ListChangedEventArgs e)
        {
            // Need to Check for New Requests and Add to Respective Buckets
            switch (e.ListChangedType)
            {
                ///////////////////////////////////////////////
                ///  MAYBE EVENTUALLY HANDLE THESE PROPERLY ///
                ///////////////////////////////////////////////
                ////// New Request Added to List
                ////case ListChangedType.ItemAdded:

                ////    break;
                ////// Request Removed from List
                ////case ListChangedType.ItemDeleted:

                ////    break;
                ////// Request Updated
                ////case ListChangedType.ItemChanged:

                ////    break;
                default:
                    UpdateDates();
                    break;
            }
        }

        private void lstDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Updating) return;
            lock (this)
            {
                try
                {
                    _Updating = true;

                    this.SelectedCounty = null;
                    this.SelectedRequest = null;
                    if ((sender as ListBox)?.SelectedValue is DateTime)
                    {
                        var newDate = (DateTime)(sender as ListBox).SelectedValue;
                        if (newDate != this.SelectedDate)
                        {
                            this.SelectedDate = newDate;
                        }
                    }
                    else
                    {
                        this.SelectedDate = null;
                    }
                    // Initiate Update
                    UpdateCounties(this.SelectedDate);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
        }

        private void lstCounties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Updating) return;
            lock (this)
            {
                try
                {
                    _Updating = true;

                    this.SelectedRequest = null;
                    if ((sender as ListBox)?.SelectedValue is string)
                    {
                        var newCounty = (sender as ListBox).SelectedValue as string;
                        if (!string.Equals(newCounty, this.SelectedCounty, StringComparison.OrdinalIgnoreCase))
                        {
                            this.SelectedCounty = newCounty;
                        }
                    }
                    else
                    {
                        this.SelectedCounty = null;
                    }
                    // Initiate Update
                    UpdateRequests(this.SelectedCounty);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
        }

        private void lstRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Updating) return;
            lock (this)
            {
                try
                {
                    _Updating = true;

                    
                    if ((sender as ListBox)?.SelectedValue is Data.Requests.AssetRequestAndAccount)
                    {
                        var newRequest = (sender as ListBox).SelectedValue as Data.Requests.AssetRequestAndAccount;
                        if (this.SelectedRequest == null || this.SelectedRequest != newRequest)
                        {
                            this.SelectedRequest = newRequest;
                            // Initiate Update

                        }
                    }
                    else
                    {
                        this.SelectedRequest = null;

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
        }
        #endregion

        #region Buttons
        private async void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.SelectedRequest != null)
            {
                await this.SelectionForm.NewRequestAssignedAsync(this.SelectedRequest);
                this.SelectionForm.Show(this);
            }
        }
        #endregion

        #endregion

        #region Dynamic List Updates
        private void UpdateDates()
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    Invoke(new Action(UpdateDates));
                }
                return;
            }

            lock (this)
            {
                try
                {
                    _Updating = true;

                    this.Requests_Date.Clear();
                    var dates = this.Processor.ItemsPending.OfType<Data.Requests.AssetRequestAndAccount>().GroupBy(req => (DateTime?)req.CurrentStatus.Date.Date).OrderBy(g => g.Key).ToArray();
                    foreach (var date in dates)
                    {
                        this.Requests_Date.Add(date);
                    }
                    if (this.SelectedDate.HasValue)
                    {
                        this.lstDates.SelectedItem = this.Requests_Date.FirstOrDefault(g => g.Key == this.SelectedDate);
                    }
                    this.SelectedDate = this.lstDates.SelectedValue as DateTime?;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
            UpdateCounties(this.SelectedDate);
        }

        private void UpdateCounties(DateTime? selectedDate)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    Invoke(new Action(UpdateDates));
                }
                return;
            }

            lock (this)
            {
                try
                {
                    _Updating = true;

                    this.Requests_County.Clear();

                    if (selectedDate.HasValue) // If We Have a Selection, Then Populate - Else, Leave Empty
                    {
                        var counties = this.Requests_Date.FirstOrDefault(g => g.Key == selectedDate).GroupBy(req => UI.Settings.Properties.Venues.FirstOrDefault(ven => ven.VenueNo == req.Account.Venue)?.County?.ToUpperInvariant() ?? UI.Settings.Properties.Venues.FirstOrDefault(ven => ven.VenueNo == 0)?.County?.ToUpperInvariant()).OrderBy(g => g.Key).ToArray();
                        foreach (var county in counties)
                        {
                            this.Requests_County.Add(county);
                        }
                        if (!string.IsNullOrWhiteSpace(this.SelectedCounty))
                        {
                            this.lstCounties.SelectedItem = this.Requests_County.FirstOrDefault(g => string.Equals(g.Key, this.SelectedCounty, StringComparison.OrdinalIgnoreCase));
                        }
                    }
                    this.SelectedCounty = this.lstCounties.SelectedValue as string;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
            UpdateRequests(this.SelectedCounty);
        }

        private void UpdateRequests(string selectedCounty)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    Invoke(new Action(UpdateDates));
                }
                return;
            }

            lock (this)
            {
                try
                {
                    _Updating = true;

                    this.Requests.Clear();

                    /// IGNORE - THIS CHECK IS NOT NECESSARY & EXCLUDES ACCOUNTS WITH PROBLEMS
                    //if (!string.IsNullOrWhiteSpace(selectedCounty)) // If We Have a Selection, Then Populate - Else, Leave Empty
                    //{
                        var requests = this.Requests_County.FirstOrDefault(g => string.Equals(g.Key, selectedCounty, StringComparison.OrdinalIgnoreCase)).OrderBy(req => req.Account.FileNo).ToArray();
                        foreach (var request in requests)
                        {
                            this.Requests.Add(request);
                        }
                        if (this.SelectedRequest != null)
                        {
                            this.lstRequests.SelectedItem = this.Requests.FirstOrDefault(req => req.ID == this.SelectedRequest.ID);
                        }
                    //}
                    this.SelectedRequest = this.lstRequests.SelectedItem as Data.Requests.AssetRequestAndAccount;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
                finally
                {
                    _Updating = false;
                }
            }
        }

        private void UpdateLists(IEnumerable<Data.Requests.AssetRequest> requests)
        {
            //////////////////////////////////////////
            ////    DITCH THIS IN ITS ENTIRETY    ////
            //// IT'S EASIER TO HANDLE BY REQUEST ////
            //////////////////////////////////////////
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<IEnumerable<Data.Requests.AssetRequest>>(UpdateLists), requests);
                }
                return;
            }

            lock (this)
            {
                try
                {
                    //_Updating = true;

                    //// Cleanup Date List
                    //var groupDate = requests.GroupBy(req => (DateTime?)req.CurrentStatus.Date).OrderBy(g => g.Key);
                    //foreach (var date in groupDate)
                    //{
                    //    if (!this.Requests_Date.Select(g => g.Key).Contains(date.Key))
                    //    {
                    //        int index = 0;
                    //        // Not In List - Add At Appropriate Spot
                    //        for (int i = 0; this.Requests_Date.Count < i && date.Key < this.Requests_Date[i].Key; i++)
                    //        {
                    //            index = i;
                    //        }
                    //        this.Requests_Date.Insert(index, date);
                    //    }
                    //    else
                    //    {
                    //        // In List - Check That Items Are Still The Same
                    //        var existing = this.Requests_Date.FirstOrDefault(g => g.Key == date.Key);
                    //        bool problem = false;
                    //        if (date.Count() == existing.Count())
                    //        {
                    //            // Same Number - Hmmm - Well, Let's Check If They're The Same Requests
                    //            foreach (var exist in existing)
                    //            {
                    //                // This is getting too complicated -- wtf?
                    //            }
                    //        }
                    //        else
                    //        {
                    //            // Not The Same Number of Requests - Fuck It - Reload The Thing.  It's Easier
                    //            this.Requests_Date.Clear();
                    //            foreach (var item in groupDate)
                    //            {
                    //                this.Requests_Date.Add(item);
                    //            }
                    //        }
                    //    }
                    //}
                    //foreach (var date in this.Requests_Date)
                    //{
                    //    if (!groupDate.Select(g => g.Key).Contains(date.Key))
                    //    {
                    //        this.Requests_Date.Remove(date);
                    //    }
                    //}
                    //// Cleanup County List
                    //if (this.SelectedDate == null || !groupDate.Select(g => g.Key).Contains(this.SelectedDate)) // Date No Longer Exists
                    //{
                    //    // Remove All From Lists
                    //    this.Requests_County.Clear();
                    //    this.Requests.Clear();
                    //    this.SelectedCounty = null;
                    //    this.SelectedDate = null;
                    //}
                    //else // Date Still Exists
                    //{
                    //    // Check If Elements Are Still The Same
                    //    var newCounties = groupDate.FirstOrDefault(g => g.Key == this.SelectedDate).OfType<Data.Requests.AssetRequestAndAccount>().GroupByCounty(UI.Settings.Properties.Venues).ToArray();
                    //    if (newCounties.Select(g => g.Key).Contains(this.SelectedCounty))
                    //    {

                    //    }
                    //    else
                    //    {

                    //    }
                    //}


                    //// Cleanup Request List
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    _Updating = false;
                }
            }
        }
        #endregion

        private void UpdateStatus(string status, bool enable)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<string, bool>(UpdateStatus), status, enable);
                }
                return;
            }

            this.lblStatus.Text = status;
            this.Enabled = enable;
        }

        #region Registered Agent Selection
        private void SelectionForm_RequestSkipped()
        {
            try
            {
                // Do Nothing
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.SelectionForm.Hide();
            }
        }

        private async void SelectionForm_RequestDenied(string reason)
        {
            try
            {
                await this.Processor.AddNegativeResultAsync(this.SelectedRequest, (list, add, prev) =>
                {
                    return new Tuple<Data.Requests.Results.Result, Dictionary<string, object>>(
                        list?.FirstOrDefault(res => string.Equals(res.Description, "Denied", StringComparison.OrdinalIgnoreCase)),
                        new Dictionary<string, object>()
                        {
                            { "Reason", reason }
                        });
                });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.SelectionForm.Hide();
            }
        }

        private async void SelectionForm_AgentSelected(Data.Assets.Base.RegisteredAgent agent)
        {
            try
            {
                this.SelectedRequest.RegisteredAgent = agent.ID;
                await this.Processor.AddPositiveResultAsync(this.SelectedRequest, (list, add, prev) =>
                {
                    return new Tuple<Data.Requests.Results.Result, Dictionary<string, object>>(
                        list?.FirstOrDefault(res => string.Equals(res.Description, "Accepted", StringComparison.OrdinalIgnoreCase)),
                        new Dictionary<string, object>()
                        {
                            { "RegisteredAgent", agent }
                        });
                });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.SelectionForm.Hide();
            }
        }
        #endregion


        private void CountyProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }

        #region IDisposable
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.SelectionForm.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
