using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public class ParamUserControl : UserControl
    {
        public event EventHandler UpdatedParameter;
        public void OnUpdatedParameter() { if (this.UpdatedParameter != null) this.UpdatedParameter(this, EventArgs.Empty); }
    }
}
