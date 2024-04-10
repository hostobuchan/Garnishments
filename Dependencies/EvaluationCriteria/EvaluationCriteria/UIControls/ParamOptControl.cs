using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.Enums;
using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class ParamOptControl : ParamUserControl
    {
        COptParam Parameter { get; set; }
        ICodeListManager CodeListManager { get; set; }
        Type CodeListManagerType { get; set; }

        public ParamOptControl(COptParam Parameter, Type CodeListManagerType)
        {
            this.Parameter = Parameter;
            this.CodeListManagerType = CodeListManagerType;
            this.CodeListManager = (ICodeListManager)CodeListManagerType.GetConstructor(new Type[] { }).Invoke(null);
            InitializeComponent();
        }

        private void UpdateForm()
        {
            this.CodeListManager = (ICodeListManager)CodeListManagerType.GetConstructor(new Type[] { }).Invoke(null);
            this.VALUE.DataSource = null;
            this.VALUE.DataSource = this.CodeListManager.GetCodeLists().Where(el => el.GetListType() == CodeListType.Numeric).OrderBy(el => el.GetName()).ToList();
            try
            {
                this.VALUE.SelectedItem = this.CodeListManager.FindCodeListById(int.Parse(this.Parameter.Value));
            }
            catch { }
            if (this.PARAM.SelectedIndex == 3 || this.PARAM.SelectedIndex == 4)
            {
                this.VALUE.Visible = true;
            }
            else
            {
                this.VALUE.Visible = false;
            }
        }

        private void ParamOptControl_Load(object sender, EventArgs e)
        {
            this.PARAM.SelectedIndex = this.Parameter.Param;
            UpdateForm();
        }

        private void PARAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            if (this.VALUE.SelectedItem is ICodeList)
            {
                this.Parameter.Param = this.PARAM.SelectedIndex;
                if (this.Parameter.Param == 3 || this.Parameter.Param == 4)
                {
                    this.Parameter.Value = ((ICodeList)this.VALUE.SelectedItem).GetID().Value.ToString();
                    this.Parameter.Value2 = ((ICodeList)this.VALUE.SelectedItem).GetName();
                }
                else
                {
                    this.Parameter.Value = "";
                    this.Parameter.Value2 = "";
                }
                OnUpdatedParameter();
            }
        }

        private void btnCLM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CodeListManagerForm clmf = new CodeListManagerForm(this.CodeListManager);
            if (clmf.ShowDialog(this) == DialogResult.OK)
            {
                this.CodeListManager = (ICodeListManager)CodeListManagerType.GetConstructor(new Type[] { }).Invoke(null);
                UpdateForm();
            }
        }
    }
}
