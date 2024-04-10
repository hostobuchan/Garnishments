using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Bindings;

namespace System.Windows.Forms
{
    public partial class FieldMappingInvertedDataGridView<TKey, TValue> : UserControl, INotifyPropertyChanged where TKey : struct
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Func<string, string, bool> ValidateSelection;

        public FieldMappingInvertedDataGridView(DictionaryBindingList<TKey, TValue> fieldMapping, string keyName, string valueName, bool exclusionaryList)
        {
            InitializeComponent();


            this.dgvFieldMapping.AutoGenerateColumns = false;
            this.dgvFieldMapping.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = keyName,
                DataPropertyName = "Key",
                HeaderText = keyName,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Popup,
                SortMode = DataGridViewColumnSortMode.Automatic,
                ValueType = typeof(TKey),
                DataSource = exclusionaryList ? new System.Windows.Forms.DataSources.DictionaryInvertedLimitedDataSource<TKey, TValue>(fieldMapping) : (IList)Enum.GetValues(typeof(TKey))
            });
            this.dgvFieldMapping.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = valueName,
                DataPropertyName = "Value",
                HeaderText = valueName,
                ValueType = typeof(TValue)
            });

            this.dgvFieldMapping.DataSource = fieldMapping;
        }

        private void dgvFieldMapping_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Cancel = !ValidateSelection?.Invoke((sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString(), e.FormattedValue.ToString()) ?? false;
            }
        }

        private void dgvFieldMapping_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvFieldMapping_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
