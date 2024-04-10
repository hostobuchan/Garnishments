using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamClassControl : ParamUserControl
    {
        CClassParam Parameter { get; set; }

        public ParamClassControl(CClassParam Parameter)
        {
            this.Parameter = Parameter;
            InitializeComponent();
        }

        private void ParamClassControl_Load(object sender, EventArgs e)
        {
            this.PARAM.SelectedIndex = this.Parameter.Param;
            this.VALUE.Text = this.Parameter.Value;
            UpdateControl();
        }

        private void UpdateControl()
        {
            if (this.PARAM.SelectedIndex == 0)
            {
                this.VALUE.Visible = false;
            }
            else
            {
                this.VALUE.Visible = true;
            }
        }

        private void PARAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            this.Parameter.Param = this.PARAM.SelectedIndex;
            this.Parameter.Value = this.VALUE.Text;
            OnUpdatedParameter();
        }
    }
}
