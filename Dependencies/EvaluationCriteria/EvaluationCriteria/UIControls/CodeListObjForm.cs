using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class CodeListObjForm<T> : Form where T : ICriteriaParam
    {
        ICodeList CodeList { get; set; }

        public CodeListObjForm(ICodeList CodeList)
        {
            this.CodeList = CodeList;
            InitializeComponent();
        }

        private void CodeListListForm_Load(object sender, EventArgs e)
        {
            this.CODES.DataSource = null;
            this.CODES.DataSource = CodeList.GetCodes().Values.OrderBy(el => el.Code).ToList();
            this.DAYS_IN.Text = "";
            this.CONST_IN.SelectedIndex = 0;
            this.CODE_IN.Text = "";
            this.CODE_IN.Focus();
        }

        private void CODE_IN_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\d");
            if (e.KeyChar == (char)Keys.Enter)
            {
                ADD_CODE_Click(this, e);
            }
            else if (e.KeyChar != (char)Keys.Back && !regex.Match(e.KeyChar.ToString()).Success)
            {
                e.Handled = true;
            }
        }

        private void ADD_CODE_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CODE_IN.Text))
            {
                int Days = 0;
                int.TryParse(this.DAYS_IN.Text, out Days);
                CodeList.GetCodes().Add(int.Parse(this.CODE_IN.Text), this.CONST_IN.SelectedIndex, Days);
                CodeListListForm_Load(this, e);
            }
        }

        private void CODES_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.CODES.SelectedItem is CodeElements)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.CodeList.GetCodes().Remove((CodeElements)this.CODES.SelectedItem);
                    CodeListListForm_Load(this, e);
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
