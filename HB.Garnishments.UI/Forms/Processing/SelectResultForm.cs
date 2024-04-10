using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Processing
{
    public partial class SelectResultForm : Form
    {
        BindingList<Data.Requests.Results.Result> Results { get; set; }
        public Data.Requests.Results.Result SelectedResult { get; private set; }

        public SelectResultForm(IEnumerable<Data.Requests.Results.Result> results)
        {
            this.Results = new BindingList<Data.Requests.Results.Result>(results.OrderBy(r => r.Description).ToList());
            InitializeComponent();
        }

        private void SelectResultForm_Load(object sender, EventArgs e)
        {
            this.cboResult.DataSource = this.Results;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboResult.SelectedItem is Data.Requests.Results.Result)
            {
                this.SelectedResult = this.cboResult.SelectedItem as Data.Requests.Results.Result;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
