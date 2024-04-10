using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Asset Name &amp; Type
    /// </summary>
    [DataContract(Name = "Asset", Namespace = "", IsReference = true)]
    public class Asset
    {
        /// <summary>
        /// ID
        /// <para>[AID]</para>
        /// </summary>
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        /// <summary>
        /// Asset Name
        /// </summary>
        [DataMember(Name = "NAME")]
        public string Name { get; private set; }
        /// <summary>
        /// Asset Type
        /// </summary>
        [DataMember(Name = "ATID")]
        public Enums.AssetType Type { get; private set; }

        public Asset(int id, string name, Enums.AssetType type)
        {
            this.ID = id;
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return $"Asset: {this.ID} {this.Name}";
        }
    }
}
