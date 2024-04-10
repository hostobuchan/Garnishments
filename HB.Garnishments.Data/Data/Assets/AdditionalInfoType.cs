using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets
{
    [DataContract(Name = "AIType", Namespace = "", IsReference = true)]
    public class AdditionalInfoType
    {
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; private set; }

        public AdditionalInfoType(int id, string description)
        {
            this.ID = id;
            this.Description = description;
        }
    }
}
