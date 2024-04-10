using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Overrides
{
    public partial class AddStatusForm : Form
    {
        Data.Requests.AssetRequest Request { get; set; }
        BindingList<Tuple<Data.Enums.Status, string>> Statuses { get; set; }
        BindingList<Data.Requests.Results.Result> Results { get; set; } = new BindingList<Data.Requests.Results.Result>();

        public Data.Enums.Status EnteredStatus { get; private set; }
        public Data.Requests.Results.Result EnteredResult { get; private set; }
        public string EnteredNote { get; private set; }

        public AddStatusForm(Data.Requests.AssetRequest request)
        {
            this.Request = request;
            IEnumerable<Data.Enums.Status> validStatuses = null;

            var cpAdmin = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain), "CP_GarnManagementAdmin");
            var anAdmin = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain), "CP_Analytics_Admin");
            if (request.CurrentStatus.Type != Data.Enums.Status.Requested && (UI.Settings.Properties.User.IsMemberOf(cpAdmin) || UI.Settings.Properties.User.IsMemberOf(anAdmin)))
            {
                // Allow All Statuses to Advanced Users
                validStatuses = Enum.GetValues(typeof(Data.Enums.Status)).OfType<Data.Enums.Status>();
            }
            else
            {
                validStatuses = Enum.GetValues(typeof(Data.Enums.Status)).Where<Data.Enums.Status>(s => s < request.CurrentStatus.Type || s == Data.Enums.Status.Canceled || s == Data.Enums.Status.Rejected);
            }
            
            this.Statuses = new BindingList<Tuple<Data.Enums.Status, string>>(validStatuses.Select(s => new Tuple<Data.Enums.Status, string>(s, Data.Dictionaries.StatusAwaitingDescription[s])).OrderBy(vs => vs.Item2, StringComparer.OrdinalIgnoreCase).ToList());
            InitializeComponent();
        }

        private void AddStatusForm_Load(object sender, EventArgs e)
        {
            this.cboStatus.DataSource = this.Statuses;
            this.cboStatus.DisplayMember = "Item2";
            this.cboStatus.ValueMember = "Item1";
            this.cboResult.DataSource = this.Results;
            this.cboResult.DisplayMember = "Description";
        }

        private async void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Results.Clear();
            if ((sender as ComboBox)?.SelectedValue is Data.Enums.Status)
            {
                try
                {
                    var resultsHandler = await Data.DataHandler.GetResultCodeHandlerAsync((Data.Enums.Status)(sender as ComboBox).SelectedValue, this.Request.Asset.Type);
                    resultsHandler.Results.ForEach(r => this.Results.Add(r));
                }
                catch { }
            }
        }

        private void chkResult_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                this.cboResult.Enabled = (sender as CheckBox).Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var selValue = (Data.Enums.Status)this.cboStatus.SelectedValue;
            if (selValue > this.Request.CurrentStatus.Type || selValue == Data.Enums.Status.Canceled || selValue == Data.Enums.Status.Rejected)
            {
                if (MessageBox.Show("You Have Selected an Advance Status\r\n\r\nPlease Be Sure That You Update the Account\r\nManually.  The Panel WILL NOT.  This Will\r\nOnly Change The Status Within The Panel\r\n\r\nYou Must Also Remove All Follow-Up Diary Codes", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    return;
                }
            }
            this.EnteredStatus = selValue;
            this.EnteredResult = this.chkResult.Checked ? this.cboResult.SelectedItem as Data.Requests.Results.Result : null;
            this.EnteredNote = txtNote.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
