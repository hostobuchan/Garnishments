using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.Enums;
using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamStringControl : ParamUserControl
    {
        CStringParam Parameter { get; set; }
        ICodeListManager CodeListManager { get; set; }
        Type CodeListManagerType { get; set; }

        public ParamStringControl(CStringParam Parameter, Type CodeListManagerType)
        {
            this.Parameter = Parameter;
            this.CodeListManagerType = CodeListManagerType;
            this.CodeListManager = (ICodeListManager)CodeListManagerType.GetConstructor(new Type[] { }).Invoke(null);
            InitializeComponent();
        }

        private void ParamBoolControl_Load(object sender, EventArgs e)
        {
            this.VALUE.DataSource = null;
            this.VALUE.DataSource = this.CodeListManager.GetCodeLists().Where(el => el.GetListType() == CodeListType.Text).ToList();
            try
            {
                this.VALUE.SelectedItem = this.CodeListManager.FindCodeListById(int.Parse(this.Parameter.Value));
            }
            catch { }
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            if (this.VALUE.SelectedItem is ICodeList)
            {
                this.Parameter.Value = ((ICodeList)this.VALUE.SelectedItem).GetID().Value.ToString();
                this.Parameter.Value2 = ((ICodeList)this.VALUE.SelectedItem).GetName();
                OnUpdatedParameter();
            }
        }

        private void btnCLM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CodeListManagerForm clmf = new CodeListManagerForm(this.CodeListManager);
            if (clmf.ShowDialog(this) == DialogResult.OK)
            {
                this.CodeListManager = (ICodeListManager)CodeListManagerType.GetConstructor(new Type[] { }).Invoke(null);
                ParamBoolControl_Load(this, e);
            }
        }
    }
}
