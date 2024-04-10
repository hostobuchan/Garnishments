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
    public partial class EvaluationSetForm : Form
    {
        Data.CriteriaSets.EvaluationSet EvaluationSet { get; set; }

        public EvaluationSetForm(Data.CriteriaSets.EvaluationSet evaluationSet)
        {
            this.EvaluationSet = evaluationSet;
            InitializeComponent();
            EvaluationCriteria.UIControls.ContextMenus.SimpleSetContextMenu<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor> sscm = new EvaluationCriteria.UIControls.ContextMenus.SimpleSetContextMenu<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>(this.EvaluationSet.SimpleSetManager);
            sscm.NameRequested += new Func<string, string>((name) =>
            {
                EnterNameForm enf = new EnterNameForm("Enter New Name", Text: name);
                if (enf.ShowDialog(this) == DialogResult.OK)
                {
                    return enf.EnteredText;
                }else
                {
                    return string.Empty;
                }
            });
            sscm.DataUpdated += EvaluationSetForm_Load;
            this.lstSets.ContextMenu = sscm;
        }

        private void EvaluationSetForm_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void EvaluationSetForm_Shown(object sender, EventArgs e)
        {

        }

        private void lstSets_DoubleClick(object sender, EventArgs e)
        {
            if (lstSets.SelectedItem is EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>)
            {
                var set = lstSets.SelectedItem as EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>;
                EvaluationCriteria.CriteriaSets.Controls.CriteriaOptionForm cof = new EvaluationCriteria.CriteriaSets.Controls.CriteriaOptionForm(set.Criteria, typeof(EvaluationCriteria.CriteriaSets.CriteriaParameters.CodeLists.CodeListManager));
                cof.ShowDialog(this);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var sssf = new EvaluationCriteria.CriteriaSets.Controls.SimpleSetSelectionForm<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>(this.EvaluationSet.SimpleSetManager);
            if (sssf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this.EvaluationSet.AddCriteriaSet(sssf.SelectedSet);
                    UpdateList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to Add Set\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstSets.SelectedItem is EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>)
            {
                try
                {
                    this.EvaluationSet.RemoveCriteriaSet(this.lstSets.SelectedItem as EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Data.Accounts.EvaluateeAccount, Data.Accounts.EvaluateeDebtor>);
                    UpdateList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Add Set\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateList()
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action(UpdateList));
                }
                return;
            }

            this.lstSets.DataSource = null;
            this.lstSets.DataSource = this.EvaluationSet.SimpleSets;
        }
    }
}
