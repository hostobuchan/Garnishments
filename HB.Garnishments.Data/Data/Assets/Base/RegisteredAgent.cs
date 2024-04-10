using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Registered Agent Information
    /// </summary>
    [DataContract(Name = "Agent", Namespace = "")]
    public abstract class RegisteredAgent : IFormatProvider, IFormattable, Interfaces.IAddressable
    {
        /// <summary>
        /// ID
        /// <para>[EMPL_NO] or [BANK_NO]</para>
        /// </summary>
        [DataMember(Name = "ID")]
        public int ID { get; protected set; }
        /// <summary>
        /// Registered Agent Type
        /// <para>(Bank or Employer)</para>
        /// </summary>
        [IgnoreDataMember]
        public abstract Enums.AssetType AssetType { get; }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "NAME")]
        public string Name { get; private set; }
        /// <summary>
        /// Address
        /// <para>Attention</para>
        /// </summary>
        [DataMember(Name = "ATTN")]
        public string Attention { get; private set; }
        /// <summary>
        /// Address
        /// <para>Line 1</para>
        /// </summary>
        [DataMember(Name = "ADDR")]
        public string Address1 { get; private set; }
        /// <summary>
        /// Address
        /// <para>Line 2</para>
        /// </summary>
        [IgnoreDataMember]
        public string Address2 { get { return null; } }
        /// <summary>
        /// Address
        /// <para>City</para>
        /// </summary>
        [IgnoreDataMember]
        public abstract string City { get; }
        /// <summary>
        /// Address
        /// <para>State</para>
        /// </summary>
        [IgnoreDataMember]
        public abstract string State { get; }
        /// <summary>
        /// Address
        /// <para>Zip</para>
        /// </summary>
        [IgnoreDataMember]
        public abstract string Zip { get; }
        /// <summary>
        /// Address
        /// <para>City, State Zip</para>
        /// </summary>
        [IgnoreDataMember]
        public abstract string CityStateZip { get; }
        /// <summary>
        /// Phone Number
        /// </summary>
        [DataMember(Name = "PHONE")]
        public string Phone { get; private set; }
        /// <summary>
        /// Fax Number
        /// </summary>
        [DataMember(Name = "FAX")]
        public string Fax { get; private set; }
        /// <summary>
        /// Email
        /// </summary>
        [DataMember(Name = "EMAIL")]
        public string Email { get; private set; }
        /// <summary>
        /// Web Site
        /// </summary>
        [DataMember(Name = "HOME_PAGE")]
        public string HomePage { get; private set; }
        /// <summary>
        /// Phone Extension
        /// </summary>
        [DataMember(Name = "PHONE_EXT")]
        public string PhoneExt { get; private set; }
        [DataMember(Name = "INACTIVE_FLAG")]
        private string _InactiveFlag;
        /// <summary>
        /// Inactive
        /// </summary>
        [IgnoreDataMember]
        public bool Inactive { get { return !string.IsNullOrEmpty(this._InactiveFlag) && string.Equals(this._InactiveFlag, "Y", StringComparison.OrdinalIgnoreCase); } }


        #region IFormattable / ToString()
        public object GetFormat(Type formatType)
        {
            return this;
        }
        public override string ToString()
        {
            return ToString("", this);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                default:
                    return $"[{this.ID}] {this.Name} - {this.Attention} - {this.Address1}, {this.CityStateZip}";
            }
        }
        #endregion
    }
}
