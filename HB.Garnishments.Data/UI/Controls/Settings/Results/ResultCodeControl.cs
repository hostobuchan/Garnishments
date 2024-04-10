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
    public partial class ResultCodeControl : UserControl
    {
        Data.Requests.Results.ResultCodeHandler CodeHandler { get; set; }
        Data.Requests.Results.Codes.MergeCode Code { get; set; }

        public ResultCodeControl(Data.Requests.Results.ResultCodeHandler codeHandler, Data.Requests.Results.Codes.MergeCode code)
        {
            this.CodeHandler = codeHandler;
            this.Code = code;
            InitializeComponent();
        }

        private void ResultCodePanel_Load(object sender, EventArgs e)
        {
            this.txtXCode.Text = this.Code.XCode;
            foreach (var cvalue in this.Code.Values)
            {
                var tBox = this.Controls.OfType<TextBox>().FirstOrDefault(tb => string.Equals(tb.Tag.ToString(), cvalue.Field.MergeField, StringComparison.OrdinalIgnoreCase));
                tBox.Text = cvalue.Info.Value;
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var values = this.Code.Values.FirstOrDefault(val => string.Equals(val.Field.MergeField, button.Tag.ToString(), StringComparison.OrdinalIgnoreCase));
                Forms.Settings.Results.FieldValueSelectionForm selectionForm = new Forms.Settings.Results.FieldValueSelectionForm(this.CodeHandler, values?.Info);
                if (selectionForm.ShowDialog(this) == DialogResult.OK)
                {
                    var newValue = await Data.Requests.Results.ResultCodeHandler.UpdateMergeCodeFieldValueAsync(this.Code, this.CodeHandler.Fields.FirstOrDefault(f => string.Equals(f.MergeField, button.Tag.ToString(), StringComparison.OrdinalIgnoreCase)), selectionForm.SelectedInfo);
                    ResultCodePanel_Load(sender, e);
                }
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var values = this.Code.Values.FirstOrDefault(val => string.Equals(val.Field.MergeField, button.Tag.ToString(), StringComparison.OrdinalIgnoreCase));
                if (values != null)
                {
                    await Data.Requests.Results.ResultCodeHandler.DeleteMergeCodeFieldValueAsync(this.Code, values.Field, values.Info);
                    var tBox = this.Controls.OfType<TextBox>().FirstOrDefault(tb => string.Equals(tb.Tag.ToString(), values.Field.MergeField, StringComparison.OrdinalIgnoreCase));
                    tBox.Text = "";
                }
            }
        }
    }
}
