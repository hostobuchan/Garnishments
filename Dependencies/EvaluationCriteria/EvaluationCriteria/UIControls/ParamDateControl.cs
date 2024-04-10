using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamDateControl : ParamUserControl
    {
        CDateParam Parameter { get; set; }

        public ParamDateControl(CDateParam Parameter)
        {
            this.Parameter = Parameter;
            InitializeComponent();
        }

        private void ParamDateControl_Load(object sender, EventArgs e)
        {
            this.PARAM.SelectedIndex = this.Parameter.Param;
            this.VALUE.Text = this.Parameter.Value.ToString();
            this.VALUE2.Text = this.Parameter.Value2.ToString();
            UpdateControl();
        }

        private void PARAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void UpdateControl()
        {
            if (this.PARAM.SelectedIndex > 2)
            {
                this.VALUE.Visible = true;
                this.lblMID.Text = "Days Ago";
                this.lblMID.Visible = true;
                if (this.PARAM.SelectedIndex > 4 && this.PARAM.SelectedIndex < 6)
                {
                    this.VALUE2.Visible = true;
                    this.lblMID.Text = "  And  ";
                    this.lblEnd.Visible = true;
                }
                else
                {
                    this.VALUE2.Visible = false;
                    this.lblEnd.Visible = false;
                }
            }
            else
            {
                this.VALUE.Visible = false;
                this.VALUE2.Visible = false;
                this.lblMID.Visible = false;
                this.lblEnd.Visible = false;
            }
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            int V1 = 0;
            int V2 = 0;
            int.TryParse(this.VALUE.Text, out V1);
            int.TryParse(this.VALUE2.Text, out V2);
            this.Parameter.Param = this.PARAM.SelectedIndex;
            this.Parameter.Value = V1;
            this.Parameter.Value2 = V2;
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
