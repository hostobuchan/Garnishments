using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Unverified
{
    [DataContract(Name = "UnverifiedAccountAssets", Namespace = "")]
    public class UnverifiedAccountAssets
    {
        [DataMember(Name = "Account", Order = 0)]
        public Accounts.Account Account { get; private set; }
        [DataMember(Name = "Assets", Order = 1)]
        [JsonIgnore]
        protected Base.Asset[] _Assets { get; private set; }
        [DataMember(Name = "Contacts", Order = 2)]
        [JsonIgnore]
        protected Base.Contact[] _Contacts { get; private set; }
        [DataMember(Name = "Locations", Order = 3)]
        [JsonIgnore]
        protected Base.Location[] _Locations { get; private set; }
        [DataMember(Name = "Users", Order = 4)]
        [JsonIgnore]
        protected Users.User[] _Users { get; private set; }
        [DataMember(Name = "Phones", Order = 5)]
        [JsonIgnore]
        protected Base.Phone[] _Phones { get; private set; }
        [DataMember(Name = "AITypes", Order = 6)]
        [JsonIgnore]
        protected AdditionalInfoType[] _AITypes { get; private set; }
        [DataMember(Name = "Sources", Order = 7)]
        [JsonIgnore]
        protected Base.Source[] _Sources { get; private set; }
        [DataMember(Name = "AssetInfos", Order = 8)]
        [JsonIgnore]
        protected AssetInfo[] _AssetInfos { get; private set; }
        /// <summary>
        /// Assets Associated with Account
        /// </summary>
        [DataMember(Name = "UnverifiedAccountAssetInfos", Order = 9)]
        [JsonProperty("Assets")]
        public UnverifiedAccountAsset[] Assets { get; private set; }

        [JsonProperty("POEs")]
        public Dictionary<byte, int> POEs
        {
            get
            {
                Dictionary<byte, int> keyValuePairs = new Dictionary<byte, int>();
                foreach (var pair in Assets?.Where(a => a.Asset.Type == Enums.AssetType.Employer).GroupBy(a => a.Debtor) ?? new IGrouping<byte, UnverifiedAccountAsset>[0])
                {
                    keyValuePairs.Add(pair.Key, pair.Count());
                }
                return keyValuePairs;
            }
        }
        [JsonProperty("Banks")]
        public Dictionary<byte, int> Banks
        {
            get
            {
                Dictionary<byte, int> keyValuePairs = new Dictionary<byte, int>();
                foreach (var pair in Assets?.Where(a => a.Asset.Type == Enums.AssetType.Bank).GroupBy(a => a.Debtor) ?? new IGrouping<byte, UnverifiedAccountAsset>[0])
                {
                    keyValuePairs.Add(pair.Key, pair.Count());
                }
                return keyValuePairs;
            }
        }
    }
}
