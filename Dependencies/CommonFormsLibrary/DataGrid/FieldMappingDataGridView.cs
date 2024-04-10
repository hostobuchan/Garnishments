using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Bindings;

namespace System.Windows.Forms
{
    public partial class FieldMappingDataGridView<TValue> : UserControl, INotifyPropertyChanged where TValue : struct
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Func<string, string, bool> ValidateSelection;
        protected void OnPropertyChanged(DataGridViewRow row, DataGridViewCellEventArgs e)
        {
            var dataBoundItem = row.DataBoundItem as Pair<string, TValue>;
            if (dataBoundItem != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dataBoundItem.Key));
            }
        }

        public FieldMappingDataGridView(DictionaryBindingList<string, TValue> fieldMapping, string keyName, string valueName, bool exclusionaryList)
        {
            InitializeComponent();


            this.dgvFieldMapping.AutoGenerateColumns = false;
            this.dgvFieldMapping.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = keyName,
                DataPropertyName = "Key",
                HeaderText = keyName,
            });
            this.dgvFieldMapping.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = valueName,
                DataPropertyName = "Value",
                HeaderText = valueName,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Popup,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(TValue),
                DataSource = exclusionaryList ? new System.Windows.Forms.DataSources.DictionaryLimitedDataSource<string, TValue>(fieldMapping) : (IList)Enum.GetValues(typeof(TValue))
            });

            this.dgvFieldMapping.DataSource = fieldMapping;
        }

        private void dgvFieldMapping_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Cancel = !ValidateSelection?.Invoke((sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString(), e.FormattedValue.ToString()) ?? false;
            }
        }

        private void dgvFieldMapping_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            OnPropertyChanged((sender as DataGridView)?.CurrentRow, e);
        }

        private void dgvFieldMapping_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
