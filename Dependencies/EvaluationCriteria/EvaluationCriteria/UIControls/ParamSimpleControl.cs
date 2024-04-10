using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamSimpleControl : ParamUserControl
    {
        CSimpleParam Parameter { get; set; }

        public ParamSimpleControl(CSimpleParam Parameter)
        {
            this.Parameter = Parameter;
            InitializeComponent();
        }

        private void ParamSimpleControl_Load(object sender, EventArgs e)
        {
            this.PARAM.SelectedIndex = this.Parameter.Param;
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            this.Parameter.Param = this.PARAM.SelectedIndex;
            OnUpdatedParameter();
        }
    }
}
