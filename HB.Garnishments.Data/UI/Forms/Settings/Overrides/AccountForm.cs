using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Overrides
{
    public partial class AccountForm : Form
    {
        BindingList<Data.Assets.AccountAsset> Assets { get; set; }
        BindingList<Data.Requests.AssetRequest> Requests { get; set; }

        public AccountForm(IEnumerable<Data.Assets.AccountAsset> assets, IEnumerable<Data.Requests.AssetRequest> requests)
        {
            this.Assets = new BindingList<Data.Assets.AccountAsset>(assets.ToList());
            this.Requests = new BindingList<Data.Requests.AssetRequest>(requests.ToList());
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            this.lstAssets.DataSource = this.Assets;
            this.lstAssets.FormatString = "D";
            //this.lstAssets.DisplayMember = "Name";
        }

        private void lstAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear Display
            this.pnlAsset.Controls.Clear();
            // Clear Requests
            this.grpRequests.Controls.Clear();
            if ((sender as ListBox)?.SelectedItem is Data.Assets.AccountAsset)
            {
                var asset = (sender as ListBox).SelectedItem as Data.Assets.AccountAsset;
                // Add Asset Display
                UI.Controls.Assets.AssetControl assetControl = new Controls.Assets.AssetControl(asset);
                this.pnlAsset.Controls.Add(assetControl);
                assetControl.Dock = DockStyle.Fill;
                // Add Requests Display
                UI.Controls.Requests.AssetRequestControl requestControl = new Controls.Requests.AssetRequestControl(this.Requests.Where(r => r.Asset.AssetName.ID == asset.Asset.ID && r.Asset.Debtor == asset.Debtor));
                this.grpRequests.Controls.Add(requestControl);
                requestControl.Dock = DockStyle.Fill;
            }
        }
    }
}
