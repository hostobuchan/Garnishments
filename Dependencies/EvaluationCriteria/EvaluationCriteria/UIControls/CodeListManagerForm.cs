using EvaluationCriteria.Enums;
using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class CodeListManagerForm : Form
    {
        ICodeListManager CodeListManager { get; set; }

        public CodeListManagerForm(ICodeListManager CodeListManager)
        {
            this.CodeListManager = CodeListManager;
            InitializeComponent();
        }

        private void CodeListManagerForm_Load(object sender, EventArgs e)
        {
            this.CODELISTS.DataSource = null;
            this.CODELISTS.DataSource = this.CodeListManager.GetCodeLists().OrderBy(el => el.GetName()).ToList();
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm();
            if (enf.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CodeListTypeForm cltf = new CodeListTypeForm();
                ICodeList CL = null;
                if (cltf.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
                    CL = this.CodeListManager.NewCodeList(enf.NAME.Text, CodeListType.Numeric);
                else
                    CL = this.CodeListManager.NewCodeList(enf.NAME.Text, CodeListType.Text);
                this.CodeListManager.AddCodeList(CL);
                CodeListManagerForm_Load(this, e);
            }
        }

        private void REMOVE_Click(object sender, EventArgs e)
        {

        }

        private void CODELISTS_DoubleClick(object sender, EventArgs e)
        {
            if (this.CODELISTS.SelectedItem is ICodeList)
            {
                if (((ICodeList)this.CODELISTS.SelectedItem).GetListType() == CodeListType.Numeric)
                {
                    CodeListListForm cllf = new CodeListListForm((ICodeList)this.CODELISTS.SelectedItem);
                    if (cllf.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        ((ICodeList)this.CODELISTS.SelectedItem).SaveCodes();
                    }
                }
                else
                {
                    CodeStringListForm cslf = new CodeStringListForm((ICodeList)this.CODELISTS.SelectedItem);
                    if (cslf.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        ((ICodeList)this.CODELISTS.SelectedItem).SaveCodes();
                    }
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
