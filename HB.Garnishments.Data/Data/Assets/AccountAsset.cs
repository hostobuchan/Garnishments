using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets
{
    /// <summary>
    /// Asset Info History for a Specific Debtor Asset
    /// </summary>
    [DataContract(Name = "AccountAsset", Namespace = "")]
    public class AccountAsset : IFormattable
    {
        /// <summary>
        /// Account
        /// </summary>
        [DataMember(Name = "Account")]
        public Accounts.Account Account { get; private set; }
        /// <summary>
        /// Asset Name &amp; Type
        /// </summary>
        [DataMember(Name = "Asset")]
        public Base.Asset Asset { get; private set; }
        /// <summary>
        /// Debtor No.
        /// </summary>
        [DataMember(Name = "DEBTOR")]
        public byte Debtor { get; private set; }
        /// <summary>
        /// Historical Changes for Debtor Specific Asset
        /// </summary>
        [DataMember(Name = "History")]
        public AccountAssetInfo[] History { get; private set; }

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
            return ToString("G");
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "D":
                    return $"[D{this.Debtor}] {this.Name}";
                default:
                    return $"{this.CurrentInfo}";
            }
        }
    }
}
