using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    [DataContract(Name = "RequestInfos", Namespace = "")]
    class AccountRequestInfos
    {
        [DataMember(Name = "Accounts", Order = 0)]
        protected Accounts.Account[] _Accounts { get; private set; }
        [DataMember(Name = "Assets", Order = 1)]
        protected Assets.Base.Asset[] _Assets { get; private set; }
        [DataMember(Name = "Contacts", Order = 2)]
        protected Assets.Base.Contact[] _Contacts { get; private set; }
        [DataMember(Name = "Locations", Order = 3)]
        protected Assets.Base.Location[] _Locations { get; private set; }
        [DataMember(Name = "Users", Order = 4)]
        protected Users.User[] _Users { get; private set; }
        [DataMember(Name = "Phones", Order = 5)]
        protected Assets.Base.Phone[] _Phones { get; private set; }
        [DataMember(Name = "AITypes", Order = 6)]
        protected Assets.AdditionalInfoType[] _AITypes { get; private set; }
        [DataMember(Name = "AssetInfos", Order = 7)]
        protected Assets.AssetInfo[] _AssetInfos { get; private set; }
        [DataMember(Name = "AccountAssetInfos", Order = 8)]
        protected Assets.AccountAsset[] _AccountAssetInfos { get; private set; }
        [DataMember(Name = "Results", Order = 9)]
        protected Results.Result[] _Results { get; private set; }
        [DataMember(Name = "Requests", Order = 10)]
        public Requests.AssetRequest[] Requests { get; private set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (this.Requests == null) this.Requests = new AssetRequest[0];
        }
    }
}
