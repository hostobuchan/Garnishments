using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Walz.Data.UI
{
    public partial class SettingsForm : Form
    {
        public DataHandler DataHandler { get; private set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            DataHandler = Task.Run(async () => await DataHandler.GetDataHandlerAsync()).Result;
            var dictBind = new System.Windows.Forms.Bindings.DictionaryBindingList<Enums.CostType, decimal>(DataHandler.Costs);
            var source = new System.Windows.Forms.DataSources.DictionaryInvertedLimitedDataSource<Enums.CostType, decimal>(dictBind);
            FieldMappingInvertedDataGridView<Enums.CostType, decimal> costDataGrid = new FieldMappingInvertedDataGridView<Enums.CostType, decimal>(
                dictBind,
                "Cost",
                "Amount",
                false);
            grpCosts.Controls.Add(costDataGrid);
            costDataGrid.Dock = DockStyle.Fill;
            dgvLetters.AutoGenerateColumns = false;
            dgvLetters.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Description",
                Name = "Description",
                HeaderText = "Letter Type",
                ValueType = typeof(string)
            });
            dgvLetters.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Weight",
                Name = "Weight",
                HeaderText = "Letter Weight (oz.)",
                ValueType = typeof(decimal)
            });
            dgvLetters.DataSource = new BindingList<Letter>(DataHandler.Letters);
            dgvEnvelopes.AutoGenerateColumns = false;
            dgvEnvelopes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Size",
                Name = "Size",
                HeaderText = "Size",
                ValueType = typeof(Enums.FormType)
            });
            dgvEnvelopes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Description",
                Name = "Description",
                HeaderText = "Description"
            });
            dgvEnvelopes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Weight",
                Name = "Weight",
                HeaderText = "Weight (oz)"
            });
            dgvEnvelopes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "StartingCost",
                Name = "StartingCost",
                HeaderText = "Base Cost"
            });
            dgvEnvelopes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CostPerOz",
                Name = "CostPerOz",
                HeaderText = "Cost Per Oz."
            });
            dgvEnvelopes.DataSource = new BindingList<Envelope>(DataHandler.Envelopes);
            dgvModifiers.AutoGenerateColumns = false;
            dgvModifiers.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Envelope",
                Visible = false
            });
            dgvModifiers.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Weight",
                Name = "Weight",
                HeaderText = "Weight (oz)",
                ValueType = typeof(decimal)
            });
            dgvModifiers.Columns.Add(new DataGridViewComboBoxColumn()
            {
                DataPropertyName = "CostModifier",
                Name = "CostModifier",
                HeaderText = "Cost Adjustment Type",
                ValueType = typeof(Enums.CostModifier),
                DataSource = Enum.GetValues(typeof(Enums.CostModifier))
            });
            dgvModifiers.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Amount",
                Name = "Amount",
                HeaderText = "Adjustment Amount",
                ValueType = typeof(decimal)
            });
            dgvModifiers.DataSource = null;
        }

        private void dgvEnvelopes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                var env = e.Row.DataBoundItem as Envelope;
                if (env != null)
                {
                    dgvModifiers.DataSource = env.CostModifiers;
                }
            }
            else
            {
                dgvModifiers.DataSource = null;
            }
        }

        private void dgvModifiers_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                var env = dgvEnvelopes.SelectedRows[0].DataBoundItem as Envelope;
                if (env != null)
                {
                    e.Row.Cells[0].Value = env;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
