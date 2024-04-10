using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HB.Garnishments.UI.Forms.Processing
{
    public partial class RegisteredAgentSelectionForm : Form
    {
        public event Action RequestSkipped;
        public event Action<string> RequestDenied;
        public event Action<Data.Assets.Base.RegisteredAgent> AgentSelected;

        SynchronizationContext Synchronization { get; set; }
        BindingList<Data.Assets.Base.RegisteredAgent> LimitedRegisteredAgents { get; set; } = new BindingList<Data.Assets.Base.RegisteredAgent>();
        Data.Requests.AssetRequest CurrentRequest { get; set; }
        bool RestrictState { get; set; } = true;
        string LastSearch { get; set; }

        public RegisteredAgentSelectionForm(Data.Requests.AssetRequest request)
        {
            this.Synchronization = SynchronizationContext.Current;
            this.CurrentRequest = request;
            InitializeComponent();
        }

        private void RegisteredAgentSelectionForm_Load(object sender, EventArgs e)
        {
            this.lstAgents.DataSource = this.LimitedRegisteredAgents;
        }

        private async void RegisteredAgentSelectionForm_Shown(object sender, EventArgs e)
        {
            await NewRequestAssignedAsync(this.CurrentRequest);
        }

        #region Interface
        private void lstAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnlSelectedAgent.Controls.Clear();
            if ((sender as ListBox)?.SelectedItem is Data.Assets.Base.RegisteredAgent)
            {
                var agent = (sender as ListBox).SelectedItem as Data.Assets.Base.RegisteredAgent;
                SetSelectedAgent(agent);             
            }
        }

        #region Search Options

        private void txtUseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnUseNumber_Click(sender, e);
            }
        }

        private void btnUseNumber_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtUseNumber.Text))
            {
                int number;
                if (int.TryParse(this.txtUseNumber.Text, out number))
                {
                    this.LimitedRegisteredAgents.Clear();
                    var selAgent = UI.Settings.Properties.RegisteredAgents.FirstOrDefault(agent => agent.AssetType == this.CurrentRequest.Asset.Type && agent.ID == number);
                    if (selAgent != null)
                    {
                        this.LimitedRegisteredAgents.Add(selAgent);
                        if (this.LimitedRegisteredAgents.FirstOrDefault() != null)
                        {
                            SetSelectedAgent(this.LimitedRegisteredAgents.First());
                        }
                    }
                    else
                    {
                        if (MessageBox.Show($"Requested {this.CurrentRequest.Asset.Type} #{number} Cannot Be Found\r\n\r\nWould You Like to Use This Number, Anyway?\r\n\r\n*If You Have Just Added This Agent to CLS, Select \"Yes\"", "Use Entered Agent?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.AgentSelected?.Invoke(new Data.Assets.UnknownAgent(number, this.CurrentRequest.Asset.Type));
                        }
                    }
                }
                this.txtUseNumber.Text = string.Empty;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSearch_Click(sender, e);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtSearch.Text))
            {
                await SearchAsync(this.txtSearch.Text, this.CurrentRequest.Asset.Type);
                this.txtSearch.Text = string.Empty;
            }
        }

        private async void btnExpandSearch_Click(object sender, EventArgs e)
        {
            this.RestrictState = false;
            await SearchAsync(LastSearch, this.CurrentRequest.Asset.Type);
        }
        #endregion

        #region Actions
        private void btnUseAgent_Click(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem is Data.Assets.Base.RegisteredAgent)
            {
                this.AgentSelected?.Invoke(lstAgents.SelectedItem as Data.Assets.Base.RegisteredAgent);
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.RequestSkipped?.Invoke();
        }

        private void btnDeny_Click(object sender, EventArgs e)
        {
            EnterTextForm textForm = new EnterTextForm("Enter Denial Reason");
            if (textForm.ShowDialog(this) == DialogResult.OK)
            {
                this.RequestDenied?.Invoke(textForm.EnteredText);
            }
        }
        #endregion
        #endregion

        public async Task NewRequestAssignedAsync(Data.Requests.AssetRequest request)
        {
            // Reset Asset Display Panel
            this.Synchronization.Send((o)=>{
                lock (this)
                {
                    this.pnlRequestedAsset.Controls.Clear();
                    UI.Controls.Assets.AssetInfoControl assetControl = new Controls.Assets.AssetInfoControl(request.Asset);
                    ElementHost host = new ElementHost();
                    host.Child = assetControl;
                    this.pnlRequestedAsset.Controls.Add(host);
                    host.Dock = DockStyle.Fill;
                    this.txtDebtor.Text = $"Debtor {request.Asset.Debtor}";
                    this.txtRequestedBy.Text = request[Data.Enums.Status.Requested]?.User.DisplayName ?? "unknown";
                }
            }, null);
            // Reset Agents and Restrictions
            lock (this)
            {
                this.RestrictState = true;
                this.CurrentRequest = request;
            }
            await SearchAsync($"{request.Asset.Name},{request.Asset.Address1}", request.Asset.Type);

            // Reset Asset Info History
            this.Synchronization.Send((o) =>
            {
                this.pnlAccountHistory.Controls.Clear();
                this.pnlInfoHistory.Controls.Clear();
            }, null);

            try
            { 
                // Load Account Asset Info History
                var accountHistory = await Data.DataHandler.GetAccountAssetRequestsAsync(request.Asset);

                this.Synchronization.Send((o) =>
                {
                    UI.Controls.Requests.AccountAssetRequestHistoryControl accountControl = new Controls.Requests.AccountAssetRequestHistoryControl(accountHistory);
                    ElementHost host = new ElementHost();
                    host.Child = accountControl;
                    pnlAccountHistory.Controls.Add(host);
                    host.Dock = DockStyle.Fill;
                }, null);

            }
            catch (Exception ex)
            {

            }

            try
            {
                // Load Asset Info History
                var infoHistory = await Data.DataHandler.GetRequestsForAssetInfoAsync(request.AssetInfoID);

                this.Synchronization.Send((o) =>
                {
                    UI.Controls.Requests.AssetRequestHistoryControl historyControl = new Controls.Requests.AssetRequestHistoryControl(infoHistory);
                    ElementHost host = new ElementHost();
                    host.Child = historyControl;
                    pnlInfoHistory.Controls.Add(host);
                    host.Dock = DockStyle.Fill;
                }, null);

            }
            catch (Exception ex)
            {

            }
        }

        private void SetSelectedAgent(Data.Assets.Base.RegisteredAgent agent)
        {
            this.Synchronization.Send((o) =>
            {
                lock (this)
                {
                    UI.Controls.Assets.RegisteredAgentControl agentControl = new Controls.Assets.RegisteredAgentControl(agent);
                    ElementHost host = new ElementHost();
                    host.Child = agentControl;
                    this.pnlSelectedAgent.Controls.Add(host);
                    host.Dock = DockStyle.Fill;
                }
            }, null);
        }

        private Task SearchAsync(string searchString, Data.Enums.AssetType assetType)
        {
            this.Synchronization.Send((o) => {
                lock (this)
                {
                    this.LimitedRegisteredAgents.Clear();
                }

            }, null);

            return Task.Run(() =>
            {
                this.LastSearch = searchString;
                var searchSplit = searchString.Split(',');
                var searchSplit2 = searchSplit[0].Split(' ');

                var matches = new List<Data.Assets.Base.RegisteredAgent>();

                foreach (var element in searchSplit2)
                {
                    matches.AddRange(UI.Settings.Properties.RegisteredAgents.Where(agent =>
                        agent.AssetType == assetType
                        && (agent.Name?.Replace(",", " ").Replace("-", " ").ToUpper().Contains(element.ToUpper()) ?? false)
                        && (this.RestrictState ? agent.State?.ToUpper() == this.CurrentRequest.Asset.State.ToUpper() : true)));
                }

                if (searchSplit.Length > 1)
                {
                    matches.AddRange(UI.Settings.Properties.RegisteredAgents.Where(agent =>
                    agent.AssetType == assetType
                    && (agent.Name?.ToUpper().Contains(searchSplit[0].Trim().ToUpper()) ?? false)
                    && (agent.CityStateZip?.ToUpper().Contains(searchSplit[1].Trim().ToUpper()) ?? false)
                    && (this.RestrictState ? agent.State?.ToUpper() == this.CurrentRequest.Asset.State.ToUpper() : true)));
                }

                this.Synchronization.Send((o) => {
                    lock (this)
                    {
                        try
                        {
                            this.LimitedRegisteredAgents.RaiseListChangedEvents = false;
                            if (matches.GroupBy(a => a.ID).Where(g => g.Count() > 1).Count() > 0)
                            {
                                foreach (var agent in matches.GroupBy(a => a.ID).Where(g => g.Count() > 1).OrderByDescending(g => g.Count()).Select(g => g.FirstOrDefault()))
                                {
                                    this.LimitedRegisteredAgents.Add(agent);
                                }
                            }
                            else
                            {
                                foreach (var agent in matches.GroupBy(a => a.ID).OrderByDescending(g => g.Count()).Select(g => g.FirstOrDefault()))
                                {
                                    this.LimitedRegisteredAgents.Add(agent);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            this.LimitedRegisteredAgents.RaiseListChangedEvents = true;
                            this.LimitedRegisteredAgents.ResetBindings();
                        }
                        if (this.LimitedRegisteredAgents.FirstOrDefault() != null)
                        {
                            SetSelectedAgent(this.LimitedRegisteredAgents.First());
                        }
                    }

                }, null);
            });
        }

        private void RegisteredAgentSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    break;
            }
        }
    }
}
