namespace System.Windows.Forms
{
    public partial class EnterNumberForm : Form
    {
        public int EnteredNumber { get; private set; }
        public bool AllowNegative { get; set; }
        private string RegexCheck { get { return this.AllowNegative ? @"(-[0-9]*)|[0-9]+" : @"[0-9]+"; } }

        public EnterNumberForm(string Title, int MaxLength = 50, bool AllowNegative = false)
        {
            InitializeComponent();
            this.Text = Title;
            this.ENTRY.MaxLength = MaxLength;
            this.AllowNegative = AllowNegative;
        }

        private void ENTRY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
                return;
            else if (e.KeyChar == (char)Keys.Enter)
                OK_Click(sender, e);
            else
            {
                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(this.ENTRY.Text + e.KeyChar.ToString(), this.RegexCheck);
                if (matches.Count != 1 || matches[0].Length != (this.ENTRY.Text + e.KeyChar.ToString()).Length)
                    e.Handled = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ENTRY.Text))
            {
                int Number;
                if (int.TryParse(this.ENTRY.Text, out Number))
                {
                    this.EnteredNumber = Number;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Enter a Valid Number", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.ENTRY.SelectAll();
                }
            }
        }

        private void CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
