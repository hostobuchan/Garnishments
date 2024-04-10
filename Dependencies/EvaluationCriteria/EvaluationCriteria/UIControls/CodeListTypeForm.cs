using System;
using System.Windows.Forms;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public partial class CodeListTypeForm : Form
    {
        public CodeListTypeForm()
        {
            InitializeComponent();
        }

        private void NUMBER_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void STRING_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }
    }
}
