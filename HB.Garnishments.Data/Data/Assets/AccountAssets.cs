using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets
{
    [DataContract(Name = "AccountAssets", Namespace = "")]
    public class AccountAssets
    {
        [DataMember(Name = "Account", Order = 0)]
        public Accounts.Account Account { get; private set; }
        [DataMember(Name = "Assets", Order = 1)]
        protected Base.Asset[] _Assets { get; private set; }
        [DataMember(Name = "Contacts", Order = 2)]
        protected Base.Contact[] _Contacts { get; private set; }
        [DataMember(Name = "Locations", Order = 3)]
        protected Base.Location[] _Locations { get; private set; }
        [DataMember(Name = "Users", Order = 4)]
        protected Users.User[] _Users { get; private set; }
        [DataMember(Name = "Phones", Order = 5)]
        protected Base.Phone[] _Phones { get; private set; }
        [DataMember(Name = "AITypes", Order = 6)]
        protected AdditionalInfoType[] _AITypes { get; private set; }
        [DataMember(Name = "AssetInfos", Order = 7)]
        protected AssetInfo[] _AssetInfos { get; private set; }
        /// <summary>
        /// Assets Associated with Account
        /// </summary>
        [DataMember(Name = "AccountAssetInfos", Order = 8)]
        public AccountAsset[] Assets { get; private set; }


    }
}
