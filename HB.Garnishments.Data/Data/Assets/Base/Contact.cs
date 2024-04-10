using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Asset Contact Name
    /// </summary>
    [DataContract(Name = "Contact", Namespace = "", IsReference = true)]
    public class Contact
    {
        /// <summary>
        /// ID
        /// <para>[ACID]</para>
        /// </summary>
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        /// <summary>
        /// Contact Name
        /// </summary>
        [DataMember(Name = "NAME")]
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"Contact: {this.ID} {this.Name}";
        }
    }
}
