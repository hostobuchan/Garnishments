using System.Collections.Generic;
using System.Linq;

namespace System.Windows.Forms
{
    public partial class SelectComboFormStruct<T> : SelectComboForm where T : struct
    {
        public Nullable<T> SelectedItem { get { return this.COMBO.SelectedItem as Nullable<T>; } }

        public SelectComboFormStruct(string Title, IEnumerable<T> DataSource)
        {
            InitializeComponent();
            this.Text = Title;
            this.COMBO.DataSource = DataSource.ToArray();
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
