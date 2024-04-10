using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Field", Namespace = "")]
    public class MergeCodeField
    {
        [DataMember(Name = "UID")]
        public byte ID { get; private set; }
        [DataMember(Name = "FIELD")]
        public string MergeField { get; private set; }
    }
}
