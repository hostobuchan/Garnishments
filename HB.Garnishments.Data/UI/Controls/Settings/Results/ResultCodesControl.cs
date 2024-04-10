using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Controls.Settings.Results
{
    public partial class ResultCodesControl : UserControl
    {
        Data.Requests.Results.ResultCodeHandler CodeHandler { get; set; }
        Data.Requests.Results.Result Result { get; set; }
        byte SalesNo { get; set; }
        byte Debtor { get; set; }

        BindingList<Data.Requests.Results.Codes.MergeCode> Codes { get; set; }

        public ResultCodesControl(Data.Requests.Results.ResultCodeHandler codeHandler, Data.Requests.Results.Result result, byte salesNo, byte debtor)
        {
            this.CodeHandler = codeHandler;
            this.Result = result;
            this.SalesNo = salesNo;
            this.Debtor = debtor;
            try
            {
                this.Codes = new BindingList<Data.Requests.Results.Codes.MergeCode>(result[salesNo][debtor].Codes.OrderBy(c => c.XCode).ToList());
            }
            catch (Exception ex)
            {
                this.Codes = new BindingList<Data.Requests.Results.Codes.MergeCode>();
            }
            InitializeComponent();
        }

        private void ResultCodesPanel_Load(object sender, EventArgs e)
        {
            this.lstCodes.DataSource = this.Codes;
        }

        private void lstCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox)?.SelectedItem is Data.Requests.Results.Codes.MergeCode)
            {
                ResultCodeControl rcp = new ResultCodeControl(this.CodeHandler, (sender as ListBox).SelectedItem as Data.Requests.Results.Codes.MergeCode);
                pnlCodeValues.Controls.Clear();
                pnlCodeValues.Controls.Add(rcp);
                rcp.Dock = DockStyle.Fill;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm("Enter XCode", 8);
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var newCode = await Data.Requests.Results.ResultCodeHandler.AddMergeCodeAsync(this.Result, this.SalesNo, this.Debtor, enf.EnteredText);
                    if (this.Codes.FirstOrDefault(code => code.ID == newCode.ID) == null)
                    {
                        this.Codes.Add(newCode);
                        this.lstCodes.SelectedItem = newCode;
                    }
                    else
                    {
                        this.lstCodes.SelectedItem = this.Codes.FirstOrDefault(code => code.ID == newCode.ID);
                    }
                    lstCodes_SelectedIndexChanged(this.lstCodes, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void lstCodes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && (sender as ListBox).SelectedItem is Data.Requests.Results.Codes.MergeCode)
            {
                var code = (sender as ListBox).SelectedItem as Data.Requests.Results.Codes.MergeCode;
                try
                {
                    await Data.Requests.Results.ResultCodeHandler.DeleteMergeCode(this.Result, this.SalesNo, this.Debtor, code);
                    this.pnlCodeValues.Controls.Clear();
                    this.Codes.Remove(code);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
