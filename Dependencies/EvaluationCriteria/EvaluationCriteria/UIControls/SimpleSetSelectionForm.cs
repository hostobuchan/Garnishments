using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class SimpleSetSelectionForm<T, K> : Form where T : Accounts.EvaluateeAccount<K> where K : Accounts.EvaluateeDebtor
    {
        private SimpleSets.SimpleSetDataHandler<T, K> Mananger { get; set; }

        public SimpleSets.SimpleSet<T, K> SelectedSet { get; private set; }

        public SimpleSetSelectionForm(SimpleSets.SimpleSetDataHandler<T, K> mananger)
        {
            this.Mananger = mananger;
            InitializeComponent();
        }

        private void SimpleSetSelectionForm_Load(object sender, EventArgs e)
        {
            UpdateComboList(this.Mananger.SimpleSets);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm();
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var newSet = this.Mananger.AddSet(enf.EnteredName);
                    UpdateComboList(this.Mananger.SimpleSets);
                    try
                    {
                        this.cboSimpleSet.SelectedItem = newSet;
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Add Simple Set\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboSimpleSet.SelectedItem is SimpleSets.SimpleSet<T, K>)
            {
                this.SelectedSet = this.cboSimpleSet.SelectedItem as SimpleSets.SimpleSet<T, K>;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateComboList(IEnumerable<SimpleSets.SimpleSet<T, K>> sets)
        {
            this.cboSimpleSet.DataSource = null;
            this.cboSimpleSet.DataSource = sets.OrderBy(el => el.Name).ToList();
        }
    }
}
