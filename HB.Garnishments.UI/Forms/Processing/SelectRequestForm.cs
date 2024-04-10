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
    public partial class SelectRequestForm : Form
    {
        Data.Requests.AssetRequest[] Requests { get; set; }
        public Data.Requests.AssetRequest SelectedRequest { get; set; }

        public SelectRequestForm(IEnumerable<Data.Requests.AssetRequest> requests)
        {
            this.Requests = requests.ToArray();
            InitializeComponent();
        }

        private void SelectRequestForm_Load(object sender, EventArgs e)
        {
            UI.Controls.Requests.AssetRequestControl arc = new Controls.Requests.AssetRequestControl(this.Requests, true, "G");
            pnlRequests.Controls.Add(arc);
            arc.Dock = DockStyle.Fill;
            this.DataBindings.Add(nameof(SelectedRequest), arc, nameof(arc.SelectedRequest));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
