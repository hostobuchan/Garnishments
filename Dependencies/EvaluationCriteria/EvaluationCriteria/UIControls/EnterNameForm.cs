using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class EnterNameForm : Form
    {
        public string EnteredName { get; private set; }

        public EnterNameForm()
        {
            InitializeComponent();
        }

        private void NAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OK_Click(sender, e);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (NAME.Text != "")
            {
                this.EnteredName = NAME.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
