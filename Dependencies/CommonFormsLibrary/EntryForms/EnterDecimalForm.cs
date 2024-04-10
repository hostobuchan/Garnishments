using System.Linq;

namespace System.Windows.Forms
{
    public partial class EnterDecimalForm : Form
    {
        public decimal EnteredNumber { get; private set; }
        public bool AllowNegative { get; set; }
        public int Precision { get; set; }
        private string RegexCheck { get { return this.AllowNegative ? @"(\-)?[0-9]*([\.])?[0-9]{0," + this.Precision.ToString() + @"}" : @"[0-9]*([\.])?[0-9]{0," + this.Precision.ToString() + @"}"; } }

        public EnterDecimalForm(string Title, int MaxLength = 50, int Precision = 2, bool AllowNegative = false)
        {
            InitializeComponent();
            this.Text = Title;
            this.ENTRY.MaxLength = MaxLength;
            this.AllowNegative = AllowNegative;
            this.Precision = Precision;
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
                var match = matches.OfType<System.Text.RegularExpressions.Match>().FirstOrDefault(m => m.Length == (this.ENTRY.Text + e.KeyChar.ToString()).Length);
                if (match == null || !match.Success)
                    e.Handled = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ENTRY.Text))
            {
                decimal Number;
                if (decimal.TryParse(this.ENTRY.Text, out Number))
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
