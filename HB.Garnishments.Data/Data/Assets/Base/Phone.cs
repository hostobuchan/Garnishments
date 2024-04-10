using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Phone Number &amp; Type
    /// </summary>
    [DataContract(Name = "Phone", Namespace = "", IsReference = true)]
    public class Phone
    {
        /// <summary>
        /// ID
        /// <para>[PID]</para>
        /// </summary>
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        /// <summary>
        /// Phone Type
        /// <para>(Home, Business, etc.)</para>
        /// </summary>
        [DataMember(Name = "TYPE")]
        public Enums.PhoneType Type { get; private set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        [DataMember(Name = "PHONE")]
        public string PhoneNumber { get; private set; }

        public Phone(int id, Enums.PhoneType type, string number)
        {
            this.ID = id;
            this.Type = type;
            this.PhoneNumber = number;
        }
    }
}
