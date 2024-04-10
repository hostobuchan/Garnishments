using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Results
{
    public partial class NewFieldValueForm : Form
    {
        public string EnteredDescription { get; private set; }
        public Data.Enums.ResultInfoType EnteredType { get; private set; }
        public string EnteredObjectReference { get; private set; }
        public string EnteredParameterReference { get; private set; }

        public NewFieldValueForm()
        {
            InitializeComponent();
        }

        private void NewFieldValueForm_Load(object sender, EventArgs e)
        {
            this.grpReference.Visible = false;
            this.Height = 158;

            this.cboType.DataSource = Enum.GetValues(typeof(Data.Enums.ResultInfoType)).OfType<Data.Enums.ResultInfoType>().OrderBy(rit => rit.ToString()).ToList();
        }

        private void chkAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                this.grpReference.Visible = true;
                this.Height = 271;
            }
            else
            {
                this.grpReference.Visible = false;
                this.Height = 158;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtDescription.Text)
                && this.cboType.SelectedItem is Data.Enums.ResultInfoType)
            {
                this.EnteredDescription = this.txtDescription.Text;
                this.EnteredType = (Data.Enums.ResultInfoType)this.cboType.SelectedItem;
                if (this.chkAdvanced.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(this.txtRefObject.Text)
                        && !string.IsNullOrWhiteSpace(this.txtRefParameter.Text))
                    {
                        this.EnteredObjectReference = this.txtRefObject.Text;
                        this.EnteredParameterReference = this.txtRefParameter.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You Must Either: Uncheck Advanced Settings / Fill In Reference Parameters", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.EnteredObjectReference = string.Empty;
                    this.EnteredParameterReference = string.Empty;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
