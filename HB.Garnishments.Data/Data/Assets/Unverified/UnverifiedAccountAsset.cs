using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Unverified
{
    [DataContract(Name = "UnverifiedAccountAsset", Namespace = "")]
    public class UnverifiedAccountAsset
    {
        /// <summary>
        /// Account
        /// </summary>
        [DataMember(Name = "Account")]
        public Accounts.Account Account { get; set; }
        /// <summary>
        /// Asset Name &amp; Type
        /// </summary>
        [DataMember(Name = "Asset")]
        public Base.Asset Asset { get; set; }
        /// <summary>
        /// Debtor No.
        /// </summary>
        [DataMember(Name = "DEBTOR")]
        [JsonProperty("Debtor")]
        public byte Debtor { get; set; }
        /// <summary>
        /// Historical Changes for Debtor Specific Asset
        /// </summary>
        [DataMember(Name = "History")]
        public AccountAssetInfo[] History { get; set; } = new AccountAssetInfo[] { };
        /// <summary>
        /// Unverified Source
        /// </summary>
        [DataMember(Name = "Source")]
        public Base.Source Source { get; set; }
        /// <summary>
        /// Date Transmitted to Firm
        /// </summary>
        [DataMember(Name = "TRANSMIT_DATE")]
        [JsonProperty("TransmitDate")]
        public DateTime TransmittedDate { get; set; }

        /// <summary>
        /// Most Recent Asset Information
        /// </summary>
        [IgnoreDataMember]
        public AccountAssetInfo CurrentInfo { get { return this.History.FirstOrDefault(aai => aai.DateAdded == this.History.Max(aai2 => aai2.DateAdded)); } }
        /// <summary>
        /// Asset Name
        /// </summary>
        [IgnoreDataMember]
        public string Name { get { return this.Asset.Name; } }

        public override string ToString()
        {
            return ToString("S");
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "S":
                    return $"[From {this.Source.Description}] {this.Name}";
                case "D":
                    return $"[D{this.Debtor}] {this.Name}";
                default:
                    return $"{this.CurrentInfo}";
            }
        }
    }
}
