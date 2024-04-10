namespace System.Windows.Forms
{
    public partial class EnterNameForm : Form
    {
        public string EnteredText { get; private set; }

        public EnterNameForm(string Title, int MaxLength = 50, string Text = "")
        {
            InitializeComponent();
            this.Text = Title;
            this.ENTRY.MaxLength = MaxLength;
            this.ENTRY.Text = Text;
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
