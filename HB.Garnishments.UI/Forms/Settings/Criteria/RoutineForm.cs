using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Criteria
{
    public partial class RoutineForm : Form
    {
        Data.CriteriaSets.CriteriaDataHandler Manager;
        Data.CriteriaSets.Routine Routine { get; set; }

        public RoutineForm(Data.CriteriaSets.CriteriaDataHandler handler, Data.CriteriaSets.Routine routine)
        {
            this.Manager = handler;
            this.Routine = routine;
            InitializeComponent();
            this.Text = routine.ToString();
            ContextMenus.EvaluationSetContextMenu escm = new ContextMenus.EvaluationSetContextMenu(handler);
            escm.DataChanged += (o, e) => { RoutineForm_Load(o, e); };
            this.lstEvals.ContextMenu = escm;
        }

        private void RoutineForm_Load(object sender, EventArgs e)
        {
            UpdateList(this.Routine.EvaluationSets.ToList());
        }

        private void RoutineForm_Shown(object sender, EventArgs e)
        {

        }

        private void lstEvals_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstEvals.SelectedItem is Data.CriteriaSets.EvaluationSet)
            {
                EvaluationSetForm esf = new EvaluationSetForm(this.lstEvals.SelectedItem as Data.CriteriaSets.EvaluationSet);
                esf.ShowDialog(this);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm("Create Evaluation Set") { Icon = Properties.Resources.add_icon };
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this.Routine.AddEvaluationSet(enf.EnteredText);
                    UpdateList(this.Routine.EvaluationSets.ToList());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Coudn't Add Evaluation Set\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstEvals.SelectedItem is Data.CriteriaSets.EvaluationSet)
            {
                try
                {
                    this.Routine.RemoveEvaluationSet(this.lstEvals.SelectedItem as Data.CriteriaSets.EvaluationSet);
                    UpdateList(this.Routine.EvaluationSets.ToList());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Coudn't Remove Evaluation Set\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateList(List<Data.CriteriaSets.EvaluationSet> sets)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<List<Data.CriteriaSets.EvaluationSet>>(UpdateList), sets);
                }
                return;
            }


            this.lstEvals.DataSource = null;
            this.lstEvals.DataSource = sets;
        }
    }
}
