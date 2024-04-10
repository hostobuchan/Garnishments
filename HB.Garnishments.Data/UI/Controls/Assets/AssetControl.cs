using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HB.Garnishments.UI.Controls.Assets
{
    public partial class AssetControl : UserControl
    {
        public Data.Assets.AccountAsset Asset { get; private set; }
        BindingList<Data.Assets.AccountAssetInfo> Infos { get; set; }

        public AssetControl(Data.Assets.AccountAsset asset)
        {
            this.Asset = asset;
            this.Infos = new BindingList<Data.Assets.AccountAssetInfo>(asset.History.OrderByDescending(h => h.DateAdded).ToList());
            InitializeComponent();
        }

        private void AssetControl_Load(object sender, EventArgs e)
        {
            this.lstHistory.DataSource = this.Infos;
            this.lstHistory.DisplayMember = "DateAdded";
            this.lstHistory.FormatString = "MMM d, yyyy";
        }

        private void lstHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            if ((sender as ListBox)?.SelectedItem is Data.Assets.AccountAssetInfo)
            {
                AssetInfoControl infoControl = new AssetInfoControl((sender as ListBox).SelectedItem as Data.Assets.AccountAssetInfo);
                ElementHost host = new ElementHost();
                host.Child = infoControl;
                this.splitContainer1.Panel2.Controls.Add(host);
                host.Dock = DockStyle.Fill;
            }
        }
    }
}
