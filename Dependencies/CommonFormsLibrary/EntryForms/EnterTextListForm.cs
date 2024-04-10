using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class EnterTextListForm : Form
    {
        public BindingList<string> EnteredTextList { get; private set; }

        public EnterTextListForm(string Title, int MaxLength, List<string> TextList = null)
        {
            InitializeComponent();
            this.Text = Title;
            this.txtEntry.MaxLength = MaxLength;
            this.EnteredTextList = TextList == null ? new BindingList<string>() : new BindingList<string>(TextList.ToList());
            this.lstEntries.DataSource = this.EnteredTextList;
        }

        private void txtEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnAdd_Click(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtEntry.Text))
            {
                this.EnteredTextList.Add(this.txtEntry.Text);
                this.txtEntry.Text = "";
                this.txtEntry.Focus();
            }
        }

        private void lstEntries_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                btnRemove_Click(sender, e);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select File With Text Entries";
                ofd.Filter = "Text Files|*.csv;*.txt";
                ofd.FilterIndex = 1;
                if (ofd.ShowDialog(this) == Forms.DialogResult.OK)
                {
                    using (System.IO.StreamReader reader = new IO.StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            this.EnteredTextList.Add(line.Length <= this.txtEntry.MaxLength ? line : line.Substring(0, this.txtEntry.MaxLength));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occurred While Importing File\n\n" + ex.Message, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.EnteredTextList.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstEntries.SelectedItem is string)
            {
                this.EnteredTextList.Remove(this.lstEntries.SelectedItem as string);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
