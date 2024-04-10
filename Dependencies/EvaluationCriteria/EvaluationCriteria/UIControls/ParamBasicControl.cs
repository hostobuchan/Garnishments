using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamBasicControl : ParamUserControl
    {
        CBasicParam Parameter { get; set; }

        public ParamBasicControl(CBasicParam Parameter)
        {
            this.Parameter = Parameter;
            InitializeComponent();
        }

        private void ParamBasicControl_Load(object sender, EventArgs e)
        {
            this.PARAM.SelectedIndex = this.Parameter.Param;
            this.VALUE.Text = this.Parameter.Value.ToString();
            UpdateControl();
        }

        private void PARAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void UpdateControl()
        {
            if (this.PARAM.SelectedIndex == 0 || this.PARAM.SelectedIndex == 5 || this.PARAM.SelectedIndex == 6)
            {
                this.VALUE.Visible = false;
            }
            else
            {
                this.VALUE.Visible = true;
            }
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            int V1 = 0;
            int.TryParse(this.VALUE.Text, out V1);
            this.Parameter.Param = this.PARAM.SelectedIndex;
            this.Parameter.Value = V1;
            OnUpdatedParameter();
        }

        private void VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[\d-]");
            if (e.KeyChar != (char)Keys.Back && !regex.Match(e.KeyChar.ToString()).Success)
            {
                e.Handled = true;
            }
        }
    }
}
