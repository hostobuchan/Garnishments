using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Controls.Requests
{
    public partial class AssetRequestControl : UserControl
    {
        bool AllowEdit = false;
        string DisplayFormat;
        BindingList<Data.Requests.AssetRequest> Requests { get; set; }
        public Data.Requests.AssetRequest SelectedRequest
        {
            get
            {
                return this.lstRequests.SelectedItem as Data.Requests.AssetRequest;
            }
        }

        public AssetRequestControl(IEnumerable<Data.Requests.AssetRequest> requests, bool disableEdit = false, string displayFormat = "E")
        {
            this.AllowEdit = !disableEdit;
            this.DisplayFormat = displayFormat;
            this.Requests = new BindingList<Data.Requests.AssetRequest>(requests.OrderByDescending(r => r.CurrentStatus.Date).ToList());
            InitializeComponent();
        }

        private void AssetRequestControl_Load(object sender, EventArgs e)
        {
            this.lstRequests.DataSource = this.Requests;
            this.lstRequests.FormatString = this.DisplayFormat;
        }

        private void lstRequests_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as ListBox)?.SelectedItem is Data.Requests.AssetRequest)
            {
                Forms.Settings.Overrides.RequestForm requestForm = new Forms.Settings.Overrides.RequestForm((sender as ListBox).SelectedItem as Data.Requests.AssetRequest, !this.AllowEdit);
                requestForm.RequestUpdated += RequestForm_RequestUpdated;
                requestForm.ShowDialog(this);
            }
        }

        private void RequestForm_RequestUpdated(object sender, Data.Requests.AssetRequest e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new EventHandler<Data.Requests.AssetRequest>(RequestForm_RequestUpdated), sender, e);
                }
                return;
            }

            var index = this.Requests.IndexOf(this.Requests.FirstOrDefault(r => r.ID == e.ID));
            this.Requests.RemoveAt(index);
            this.Requests.Insert(index, e);
        }
    }
}
