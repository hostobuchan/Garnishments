using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets.Base
{
    /// <summary>
    /// Source of Asset Information
    /// </summary>
    [DataContract(Name = "Source", Namespace = "")]
    public class Source
    {
        [DataMember(Name = "UID")]
        public short ID { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; private set; }
        
        public Source(short id, string description)
        {
            this.ID = id;
            this.Description = description;
        }

        public override string ToString()
        {
            return $"{this.Description}";
        }
    }
}
