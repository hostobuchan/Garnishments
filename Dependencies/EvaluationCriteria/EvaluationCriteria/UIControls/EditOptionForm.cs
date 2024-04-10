using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class EditOptionForm : Form
    {
        CParam Parameter { get; set; }
        Type CodeListManagerType { get; set; }

        public EditOptionForm(CParam Parameter, Type CodeListManagerType)
        {
            this.Parameter = Parameter;
            this.CodeListManagerType = CodeListManagerType;
            InitializeComponent();
            this.Text += " " + this.Parameter.DataName;
        }

        private void EditOptionForm_Load(object sender, EventArgs e)
        {
            if (this.Parameter is CDateParam)
            {
                ParamDateControl pdc = new ParamDateControl((CDateParam)this.Parameter);
                this.Controls.Add(pdc);
                pdc.Dock = DockStyle.Fill;
                pdc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CBasicParam)
            {
                ParamBasicControl pbc = new ParamBasicControl((CBasicParam)this.Parameter);
                this.Controls.Add(pbc);
                pbc.Dock = DockStyle.Fill;
                pbc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CClassParam)
            {
                ParamClassControl pcc = new ParamClassControl((CClassParam)this.Parameter);
                this.Controls.Add(pcc);
                pcc.Dock = DockStyle.Fill;
                pcc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CSimpleParam)
            {
                ParamSimpleControl psc = new ParamSimpleControl((CSimpleParam)this.Parameter);
                this.Controls.Add(psc);
                psc.Dock = DockStyle.Fill;
                psc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CBoolParam)
            {
                ParamBoolControl pbc = new ParamBoolControl((CBoolParam)this.Parameter, this.CodeListManagerType);
                this.Controls.Add(pbc);
                pbc.Dock = DockStyle.Fill;
                pbc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is COptParam)
            {
                ParamOptControl poc = new ParamOptControl((COptParam)this.Parameter, this.CodeListManagerType);
                this.Controls.Add(poc);
                poc.Dock = DockStyle.Fill;
                poc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CStringParam)
            {
                ParamStringControl psc = new ParamStringControl((CStringParam)this.Parameter, this.CodeListManagerType);
                this.Controls.Add(psc);
                psc.Dock = DockStyle.Fill;
                psc.UpdatedParameter += new EventHandler(CloseForm);
            }
            else if (this.Parameter is CMediaParam)
            {
                ParamMediaControl psc = new ParamMediaControl((CMediaParam)this.Parameter);
                this.Controls.Add(psc);
                psc.Dock = DockStyle.Fill;
                psc.UpdatedParameter += new EventHandler(CloseForm);
            }
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
