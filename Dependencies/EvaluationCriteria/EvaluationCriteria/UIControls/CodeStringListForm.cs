using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class CodeStringListForm : Form
    {
        ICodeList CodeList { get; set; }

        public CodeStringListForm(ICodeList CodeList)
        {
            this.CodeList = CodeList;
            InitializeComponent();
        }

        private void CodeStringListForm_Load(object sender, EventArgs e)
        {
            this.CODES.DataSource = null;
            this.CODES.DataSource = CodeList.GetCodeStrings().OrderBy(el => el).ToList();
            this.CODE_IN.Text = "";
            this.CODE_IN.Focus();
        }

        private void CODE_IN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ADD_CODE_Click(this, e);
            }
        }

        private void ADD_CODE_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CODE_IN.Text))
            {
                CodeList.GetCodeStrings().Add(this.CODE_IN.Text);
                CodeStringListForm_Load(this, e);
            }
        }

        private void CODES_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.CODES.SelectedItem is string)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.CodeList.GetCodeStrings().Remove((string)this.CODES.SelectedItem);
                    CodeStringListForm_Load(this, e);
                }
            }
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
