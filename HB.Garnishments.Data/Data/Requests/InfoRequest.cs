using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    [DataContract(Name = "Request", Namespace = "")]
    public class InfoRequest : Request
    {
        [DataMember(Name = "Asset")]
        Assets.AssetInfo _Asset;

        [IgnoreDataMember]
        public Assets.AssetInfo Asset
        {
            get
            {
                return GetAssetInfo();
            }
            set
            {
                _Asset = value;
            }
        }

        public Assets.AssetInfo GetAssetInfo()
        {
            return GetAssetInfoAsync().Result;
        }
        public async Task<Assets.AssetInfo> GetAssetInfoAsync()
        {
            if (_Asset == null)
            {
                try
                {
                    _Asset = await DataHandler.GetAssetAsync(this.AssetInfoID);
                }
                catch { }
            }
            return _Asset;
        }

        public override string ToString(string format)
        {
            switch (format)
            {
                case "T":
                    return $"{Asset.AssetName.Type} - {Asset.AssetName.Name}";
                default:
                    return $"[{ID}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName}";
            }
        }
    }
}
