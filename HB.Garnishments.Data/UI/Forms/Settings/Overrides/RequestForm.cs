using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HB.Garnishments.UI.Forms.Settings.Overrides
{
    public partial class RequestForm : Form
    {
        bool AllowEdit = false;
        public event EventHandler<Data.Requests.AssetRequest> RequestUpdated;

        Data.Requests.AssetRequest Request { get; set; }
        BindingList<Data.Requests.Status> Statuses { get; set; }

        public RequestForm(Data.Requests.AssetRequest request, bool disableEdit = false)
        {
            this.AllowEdit = !disableEdit;
            this.Request = request;
            this.Statuses = new BindingList<Data.Requests.Status>(Request.History.OrderBy(h => h.Date).ToList());
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            // Set Requested Asset Info
            UI.Controls.Assets.AssetInfoControl infoControl = new Controls.Assets.AssetInfoControl(this.Request.Asset);
            ElementHost host = new ElementHost();
            host.Child = infoControl;
            this.grpInfo.Controls.Add(host);
            host.Dock = DockStyle.Fill;
            // Set Request Processing History
            this.lstHistory.DataSource = Statuses;
            // Set Registered Agent Info
            if (this.Request.RegisteredAgent.HasValue)
            {
                var agent = UI.Settings.Properties.RegisteredAgents.FirstOrDefault(ra => ra.ID == this.Request.RegisteredAgent && ra.AssetType == Request.Asset.Type);
                if (agent != null)
                {
                    Controls.Assets.RegisteredAgentControl agentControl = new Controls.Assets.RegisteredAgentControl(agent, this.AllowEdit);
                    agentControl.EditClicked += AgentControl_EditClicked;
                    ElementHost host2 = new ElementHost();
                    host2.Child = agentControl;
                    this.grpRegisteredAgent.Controls.Add(host2);
                    host2.Dock = DockStyle.Fill;
                }
            }
        }

        private async void AgentControl_EditClicked(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler(AgentControl_EditClicked), sender, e);
                }
                return;
            }


            try
            {
                // Display Form Asking for New Registered Agent #
                EnterNumberForm enf = new EnterNumberForm("Enter Bank/Employer No.", 5);
                if (enf.ShowDialog(this) == DialogResult.OK)
                {
                    // Display Form Asking for Confirmation to Change Registered Agent ## From @ => To @ ##
                    ChangeRegisteredAgentForm caf = new ChangeRegisteredAgentForm(
                        this.Request.Asset.Type,
                        this.Request.RegisteredAgent.HasValue ? this.Request.RegisteredAgent.Value : 0,
                        enf.EnteredNumber);
                    if (caf.ShowDialog(this) == DialogResult.OK)
                    {
                        await this.Request.SetRegisteredAgentAsync(enf.EnteredNumber == 0 ? (int?)null : enf.EnteredNumber);

                        this.grpInfo.Controls.Clear();
                        this.grpRegisteredAgent.Controls.Clear();
                        RequestForm_Load(sender, e);

                        this.RequestUpdated?.Invoke(sender, this.Request);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnlStatusInfo.Controls.Clear();
            if ((sender as ListBox)?.SelectedItem is Data.Requests.Status)
            {
                // Set Status Info to Panel
                UI.Controls.Requests.RequestStatusControl statusControl = new Controls.Requests.RequestStatusControl((sender as ListBox).SelectedItem as Data.Requests.Status);

                // Disable Edit Items ?
                if (this.AllowEdit)
                {
                    statusControl.AddNewStatus += StatusControl_AddNewStatus;
                    statusControl.ChangeResult += StatusControl_ChangeResult;
                    statusControl.UpdateNote += StatusControl_UpdateNote;
                }
                else
                {
                    statusControl.AddNewStatus += (o, ev) => { MessageBox.Show("Not Allowed", "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); };
                    statusControl.ChangeResult += (o, ev) => { MessageBox.Show("Not Allowed", "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); };
                    statusControl.UpdateNote += (o, ev) => { MessageBox.Show("Not Allowed", "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); };
                }
                ElementHost host = new ElementHost();
                host.Child = statusControl;
                this.pnlStatusInfo.Controls.Add(host);
                host.Dock = DockStyle.Fill;
            }
        }

        private async void StatusControl_ChangeResult(object sender, Data.Requests.Status e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler<Data.Requests.Status>(StatusControl_ChangeResult), sender, e);
                }
                return;
            }

            // Request New Result Info
            var resultsHandler = await Data.DataHandler.GetResultCodeHandlerAsync(e.Type, this.Request.Asset.Type);
            if (resultsHandler.Results != null)
            {
                SelectComboForm<Data.Requests.Results.Result> selectResultForm = new SelectComboForm<Data.Requests.Results.Result>("Select Result", resultsHandler.Results.OrderBy(r => r.Description).ToList(), "N");
                if (selectResultForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Check For Garnishee Answer -- Add New Status If Already Answered
                    if (this.Request.CurrentStatus.Type == Data.Enums.Status.GarnisheeResponse)
                    {
                        ulong newStatusID = e.ID;
                        try
                        {
                            newStatusID = await Data.DataHandler.AddStatusAsync(
                               this.Request.ID,
                               e.Type,
                               UI.Settings.Properties.User.SamAccountName,
                               DateTime.Now,
                               selectResultForm.SelectedItem.ID
                           );
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            this.Request = await Data.DataHandler.GetAccountAssetRequestByIdAsync(this.Request.ID);
                            var newStatus = this.Request.History.FirstOrDefault(s => s.ID == newStatusID);
                            this.Statuses.Add(newStatus);
                            this.lstHistory.SelectedItem = newStatus;
                            this.RequestUpdated?.Invoke(sender, this.Request);
                        }
                        return;
                    }
                    else
                    {
                        try
                        {
                            await Data.DataHandler.UpdateStatusAsync(e.ID, UI.Settings.Properties.User.SamAccountName, DateTime.Now, selectResultForm.SelectedItem.ID);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            var prevStatus = this.Statuses.FirstOrDefault(s => s.ID == e.ID);
                            var prevStatusIndex = this.Statuses.IndexOf(prevStatus);
                            this.Request = await Data.DataHandler.GetAccountAssetRequestByIdAsync(this.Request.ID);
                            var status = this.Request.History.FirstOrDefault(s => s.ID == e.ID);
                            this.Statuses.Remove(prevStatus);
                            this.Statuses.Insert(prevStatusIndex, status);
                            this.lstHistory.SelectedItem = status;
                            this.RequestUpdated?.Invoke(sender, this.Request);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"No Possible Results for Status \"{Data.Dictionaries.StatusDescription[e.Type]}\"", "Invalid Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void StatusControl_UpdateNote(object sender, Data.Requests.Status e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler<Data.Requests.Status>(StatusControl_UpdateNote), sender, e);
                }
                return;
            }

            EnterTextForm etf = new EnterTextForm("Note", int.MaxValue, e.Note);
            if (etf.ShowDialog(this) == DialogResult.OK)
            {
                try
                { 
                    await Data.DataHandler.UpdateStatusAsync(e.ID, UI.Settings.Properties.User.SamAccountName, DateTime.Now, note: etf.EnteredText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Request = await Data.DataHandler.GetAccountAssetRequestByIdAsync(this.Request.ID);
                    var status = this.Request.History.FirstOrDefault(s => s.ID == e.ID);
                    this.lstHistory.SelectedItem = status;
                    this.RequestUpdated?.Invoke(sender, this.Request);
                }
            }
        }

        private async void StatusControl_AddNewStatus(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler(StatusControl_AddNewStatus), sender, e);
                }
                return;
            }

            // Check That Garn is Not Complete
            if (this.Request.CurrentStatus.Type == Data.Enums.Status.GarnisheeResponse)
            {
                MessageBox.Show("This Garnishment is Completed\r\n\r\nPlease Start a New Garnishment Request", "Invalid Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Check That Garn is Not Unprocessed
            else if (this.Request.CurrentStatus.Type == Data.Enums.Status.Requested)
            {
                MessageBox.Show("This Garnishment is Unprocessed\r\n\r\nYou Must Use The Processing Routine\r\nOr Account Will Be Improperly Coded\r\n\r\nWhen Selecting Registered Agent\r\nClick the \"Deny Garnishment\" Button", "Invalid Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Request New Status Info
            AddStatusForm addStatusForm = new AddStatusForm(this.Request);
            if (addStatusForm.ShowDialog(this) == DialogResult.OK)
            {
                ulong newStatusID = this.Request.CurrentStatus.ID;
                try
                { 
                    newStatusID = await Data.DataHandler.AddStatusAsync(
                        this.Request.ID,
                        addStatusForm.EnteredStatus,
                        UI.Settings.Properties.User.SamAccountName,
                        DateTime.Now,
                        addStatusForm.EnteredResult?.ID,
                        addStatusForm.EnteredNote
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Request = await Data.DataHandler.GetAccountAssetRequestByIdAsync(this.Request.ID);
                    var newStatus = this.Request.History.FirstOrDefault(s => s.ID == newStatusID);
                    this.Statuses.Add(newStatus);
                    this.lstHistory.SelectedItem = newStatus;
                    this.RequestUpdated?.Invoke(sender, this.Request);
                }
            }
        }
    }
}
