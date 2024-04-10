using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Asset Address Information
    /// </summary>
    [DataContract(Name = "Location", Namespace = "", IsReference = true)]
    public class Location
    {
        /// <summary>
        /// ID
        /// <para>[ALID]</para>
        /// </summary>
        [DataMember(Name = "UID")]
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; private set; }
        /// <summary>
        /// Address
        /// <para>Line 1</para>
        /// </summary>
        [DataMember(Name = "ADDRESS1")]
        [JsonProperty(PropertyName = "Address1")]
        public string Address1 { get; private set; }
        /// <summary>
        /// Address
        /// <para>Line 2</para>
        /// </summary>
        [DataMember(Name = "ADDRESS2")]
        [JsonProperty(PropertyName = "Address2")]
        public string Address2 { get; private set; }
        /// <summary>
        /// Address
        /// <para>City</para>
        /// </summary>
        [DataMember(Name = "CITY")]
        [JsonProperty(PropertyName = "City")]
        public string City { get; private set; }
        /// <summary>
        /// Address
        /// <para>State</para>
        /// </summary>
        [DataMember(Name = "STATE")]
        [JsonProperty(PropertyName = "State")]
        public string State { get; private set; }
        /// <summary>
        /// Address
        /// <para>Zip</para>
        /// </summary>
        [DataMember(Name = "ZIP")]
        [JsonProperty(PropertyName = "Zip")]
        public string Zip { get; private set; }

        public Location(int id, string address1, string address2, string city, string state, string zip)
        {
            this.ID = id;
            this.Address1 = address1;
            this.Address2 = address2;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public override string ToString()
        {
            return $"Location: {this.ID} {this.Address1} {this.Address2}, {this.City}, {this.State} {this.Zip}";
        }
    }
}
