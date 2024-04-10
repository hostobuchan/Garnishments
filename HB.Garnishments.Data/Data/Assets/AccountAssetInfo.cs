using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets
{
    /// <summary>
    /// Asset Info for a Specific Debtor Asset
    /// </summary>
    [DataContract(Name = "AccountAssetInfo", Namespace = "")]
    public class AccountAssetInfo : Interfaces.IAssetInfo
    {
        private Requests.AssetRequest[] _Requests;

        /// <summary>
        /// Account
        /// </summary>
        [DataMember(Name = "Account")]
        public Accounts.Account Account { get; private set; }
        /// <summary>
        /// Debtor No.
        /// </summary>
        [DataMember(Name = "DEBTOR")]
        public byte Debtor { get; private set; }
        /// <summary>
        /// Asset Information
        /// </summary>
        [DataMember(Name = "Info")]
        public AssetInfo Info { get; private set; }
        /// <summary>
        /// Date of Information Entry
        /// </summary>
        [DataMember(Name = "DATE")]
        public DateTime DateAdded { get; private set; }
        /// <summary>
        /// User Who Entered Information
        /// </summary>
        [DataMember(Name = "User")]
        public Users.User UserAdded { get; private set; }
        /// <summary>
        /// Information Disposition
        /// <para>Known, Successful Capital Generator</para>
        /// </summary>
        [DataMember(Name = "GOOD")]
        public bool? Good { get; private set; }
        /// <summary>
        /// Additional Asset Information
        /// </summary>
        [DataMember(Name = "Attributes")]
        public Dictionary<AdditionalInfoType, string> AdditionalAttributes { get; private set; }

        /// <summary>
        /// Asset Name &amp; Type
        /// </summary>
        [IgnoreDataMember]
        public Base.Asset AssetName { get { return this.Info.AssetName; } }
        /// <summary>
        /// Asset Contact Info
        /// </summary>
        [IgnoreDataMember]
        public Base.Contact AssetContact { get { return this.Info.AssetContact; } }
        /// <summary>
        /// Asset Address Info
        /// </summary>
        [IgnoreDataMember]
        public Base.Location AssetLocation { get { return this.Info.AssetLocation; } }
        /// <summary>
        /// Asset Name
        /// </summary>
        [IgnoreDataMember]
        public string Name { get { return this.AssetName.Name; } }
        /// <summary>
        /// Asset Type
        /// </summary>
        [IgnoreDataMember]
        public Enums.AssetType Type { get { return this.AssetName.Type; } }
        /// <summary>
        /// Asset Address
        /// <para>Attention</para>
        /// </summary>
        [IgnoreDataMember]
        public string Attention { get { return this.AssetContact.Name; } }
        /// <summary>
        /// Asset Contact Name
        /// </summary>
        [IgnoreDataMember]
        public string Contact { get { return this.AssetContact.Name; } }
        /// <summary>
        /// Asset Address
        /// <para>Line 1</para>
        /// </summary>
        [IgnoreDataMember]
        public string Address1 { get { return this.Info.AssetLocation.Address1; } }
        /// <summary>
        /// Asset Address
        /// <para>Line 2</para>
        /// </summary>
        [IgnoreDataMember]
        public string Address2 { get { return this.Info.AssetLocation.Address2; } }
        /// <summary>
        /// Asset Address
        /// <para>City</para>
        /// </summary>
        [IgnoreDataMember]
        public string City { get { return this.Info.AssetLocation.City; } }
        /// <summary>
        /// Asset Address
        /// <para>State</para>
        /// </summary>
        [IgnoreDataMember]
        public string State { get { return this.Info.AssetLocation.State; } }
        /// <summary>
        /// Asset Address
        /// <para>Zip</para>
        /// </summary>
        [IgnoreDataMember]
        public string Zip { get { return this.Info.AssetLocation.Zip; } }
        /// <summary>
        /// Asset Address
        /// <para>City, State</para>
        /// </summary>
        [IgnoreDataMember]
        public string CityState { get { return $"{this.City}, {this.State}"; } }
        /// <summary>
        /// Asset Address
        /// <para>City, State Zip</para>
        /// </summary>
        [IgnoreDataMember]
        public string CityStateZip { get { return $"{this.City}, {this.State} {this.Zip}"; } }
        /// <summary>
        /// List of Phone Numbers
        /// </summary>
        [IgnoreDataMember]
        public Base.Phone[] Phones { get { return this.Info.Phones; } }

        /// <summary>
        /// Get List of Garnishment Requests
        /// </summary>
        /// <returns>Array of Asset Request</returns>
        public async Task<Requests.AssetRequest[]> GetRequestsAsync()
        {
            if (_Requests == null)
            {
                try
                {
                    _Requests = (await DataHandler.GetAccountAssetRequestsAsync(this)) ?? new Requests.AssetRequest[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return _Requests;
        }

        public override string ToString()
        {
            return $"[{this.Info.ID}] {this.AssetName} | {this.AssetContact} | {this.AssetLocation}";
        }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            //if (this.AdditionalAttributes == null) this.AdditionalAttributes = new Dictionary<string, string>();
            //if (this.AdditionalAttributes == null || this.AdditionalAttributes.Count == 0) this.AdditionalAttributes = new Dictionary<AdditionalInfoType, string>() { { new AdditionalInfoType(1, "VIN"), "12345678" } };
        }
    }
}
