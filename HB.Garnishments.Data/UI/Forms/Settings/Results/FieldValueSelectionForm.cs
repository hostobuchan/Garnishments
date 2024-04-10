using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Results
{
    public partial class FieldValueSelectionForm : Form
    {
        Data.Requests.Results.ResultCodeHandler CodeHandler { get; set; }


        public Data.Requests.Results.Codes.MergeCodeInfo SelectedInfo { get; private set; }


        public FieldValueSelectionForm(Data.Requests.Results.ResultCodeHandler codeHandler, Data.Requests.Results.Codes.MergeCodeInfo selectedInfo = null)
        {
            this.CodeHandler = codeHandler;
            this.SelectedInfo = selectedInfo;
            InitializeComponent();
        }

        private void SimpleSetSelectionForm_Load(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler(SimpleSetSelectionForm_Load), sender, e);
                }
                return;
            }

            UpdateComboList(this.CodeHandler.Infos);
            if (this.SelectedInfo != null) this.cboInfos.SelectedItem = this.SelectedInfo;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            NewFieldValueForm nfvf = new NewFieldValueForm();
            if (nfvf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var newInfoValue = await this.CodeHandler.AddMergeCodeInfoAsync(nfvf.EnteredDescription, nfvf.EnteredType, nfvf.EnteredObjectReference, nfvf.EnteredParameterReference);
                    this.SelectedInfo = newInfoValue;
                    SimpleSetSelectionForm_Load(sender, e);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to Add Info\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboInfos.SelectedItem is Data.Requests.Results.Codes.MergeCodeInfo)
            {
                this.SelectedInfo = this.cboInfos.SelectedItem as Data.Requests.Results.Codes.MergeCodeInfo;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateComboList(IEnumerable<Data.Requests.Results.Codes.MergeCodeInfo> infos)
        {
            this.cboInfos.DataSource = null;
            this.cboInfos.DataSource = infos.OrderBy(el => el.Value).ToList();
            this.cboInfos.DisplayMember = "Value";
        }
    }
}
