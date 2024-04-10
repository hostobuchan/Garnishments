namespace System.Windows.Forms
{
    public partial class EnterTextForm : Form
    {
        public string EnteredText { get; private set; }

        public EnterTextForm(string Title, int MaxLength = 256, string DefaultText = null)
        {
            InitializeComponent();
            this.Text = Title;
            this.ENTRY.MaxLength = MaxLength <= 0 ? 65535 : MaxLength;
            this.ENTRY.Text = string.IsNullOrEmpty(DefaultText) ? "" : DefaultText;
        }

        private void ENTRY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OK_Click(sender, e);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ENTRY.Text))
            {
                this.EnteredText = this.ENTRY.Text;
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
