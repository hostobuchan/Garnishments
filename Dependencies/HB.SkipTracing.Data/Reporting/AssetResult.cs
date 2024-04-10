using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Reporting
{
    public class AssetResult<T> where T : Records.DownloadRecord
    {
        public T DownloadRecord { get; private set; }
        public Assets.AccountAssetInfo<T> AssetInfo { get; private set; }
        public Garnishments.Data.Assets.AccountAssetInfo NewAsset { get; set; }
        public int GarnishmentID { get; set; }

        public Enums.AssetResultType Result { get; set; }
        public Enums.AssetResultReason AssetResultReason { get; set; }
        public Enums.GarnishmentResultReason GarnishmentResultReason { get; set; }
        public string Description { get; set; }

        public AssetResult(T downloadRecord, Assets.AccountAssetInfo<T> assetInfo)
        {
            this.DownloadRecord = downloadRecord;
            this.AssetInfo = assetInfo;
        }
    }
}
