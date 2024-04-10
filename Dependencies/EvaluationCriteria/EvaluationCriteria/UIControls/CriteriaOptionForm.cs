using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class CriteriaOptionForm : Form
    {
        Criteria Criteria { get; set; }
        Type CodeListManagerType { get; set; }

        public CriteriaOptionForm(Criteria Criteria, Type CodeListManagerType)
        {
            this.Criteria = Criteria;
            this.CodeListManagerType = CodeListManagerType;
            InitializeComponent();
        }

        private void RefreshFormData()
        {
            this.IS.Items.Clear();
            this.NOT.Items.Clear();
            foreach (KeyValuePair<string, CParam> KVP in this.Criteria.CriteriaItems)
            {
                if (KVP.Value.Param > 0)
                {
                    if (KVP.Value.ISNOT)
                    {
                        this.IS.Items.Add(KVP.Value);
                    }
                    else
                    {
                        this.NOT.Items.Add(KVP.Value);
                    }
                }
            }
        }

        private void CriteriaOptionForm_Load(object sender, EventArgs e)
        {
            RefreshFormData();
        }

        private void ADD_IS_Click(object sender, EventArgs e)
        {
            AddOptionForm aof = new AddOptionForm(this.Criteria, true);
            if (aof.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                RefreshFormData();
            }
        }

        private void REMOVE_IS_Click(object sender, EventArgs e)
        {
            if (this.IS.SelectedItem is CParam)
            {
                ((CParam)this.IS.SelectedItem).Param = 0;
                RefreshFormData();
            }
        }

        private void ADD_NOT_Click(object sender, EventArgs e)
        {
            AddOptionForm aof = new AddOptionForm(this.Criteria, false);
            if (aof.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                RefreshFormData();
            }
        }

        private void REMOVE_NOT_Click(object sender, EventArgs e)
        {
            if (this.NOT.SelectedItem is CParam)
            {
                ((CParam)this.NOT.SelectedItem).Param = 0;
                RefreshFormData();
            }
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.Criteria.Save();
            this.Close();
        }

        private void IS_DoubleClick(object sender, EventArgs e)
        {
            if (this.IS.SelectedItem is CParam)
            {
                EditOptionForm eof = new EditOptionForm((CParam)this.IS.SelectedItem, this.CodeListManagerType);
                if (eof.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    RefreshFormData();
            }
        }

        private void NOT_DoubleClick(object sender, EventArgs e)
        {
            if (this.NOT.SelectedItem is CParam)
            {
                EditOptionForm eof = new EditOptionForm((CParam)this.NOT.SelectedItem, this.CodeListManagerType);
                if (eof.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    RefreshFormData();
            }
        }
    }
}
