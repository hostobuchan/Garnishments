using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class AddOptionForm : Form
    {
        Criteria CriteriaSet { get; set; }
        bool ISNOT { get; set; }

        public AddOptionForm(Criteria CriteriaSet, bool ISNOT)
        {
            this.CriteriaSet = CriteriaSet;
            this.ISNOT = ISNOT;
            InitializeComponent();
        }

        private void AddOptionForm_Load(object sender, EventArgs e)
        {
            this.OPTION.DataSource = this.CriteriaSet.CriteriaItems.Select(el => el.Value).Where(el => el.Param == 0).OrderBy(el => el.DataName).ToList();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (this.OPTION.SelectedItem is CParam)
            {
                ((CParam)this.OPTION.SelectedItem).Param = 1;
                ((CParam)this.OPTION.SelectedItem).ISNOT = this.ISNOT;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
