using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
    public partial class SelectComboForm<T> : SelectComboForm where T : class
    {
        public T SelectedItem { get { return this.COMBO.SelectedItem as T; } }

        public SelectComboForm(string Title, List<T> DataSource, string DisplayFormat = null, bool MonoType = false)
        {
            InitializeComponent();
            this.Text = Title;
            this.COMBO.DataSource = DataSource;
            this.COMBO.FormatString = DisplayFormat;
            if (MonoType) this.COMBO.Font = new Font("Courier New", 8);
        }

        private void ENTRY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OK_Click(sender, e);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (this.COMBO.SelectedItem is T)
            {
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
