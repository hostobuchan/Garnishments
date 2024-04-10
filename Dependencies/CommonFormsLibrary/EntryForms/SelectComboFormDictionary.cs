using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class SelectComboForm<T, K> : SelectComboForm where T : class
    {
        public K SelectedItem { get { return (K)this.COMBO.SelectedValue; } }

        public SelectComboForm(string Title, Dictionary<T, K> DataSource)
        {
            InitializeComponent();
            this.Text = Title;
            this.COMBO.DataSource = DataSource.ToList();
            this.COMBO.DisplayMember = "Key";
            this.COMBO.ValueMember = "Value";
        }

        private void ENTRY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OK_Click(sender, e);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (this.COMBO.SelectedValue is K)
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
