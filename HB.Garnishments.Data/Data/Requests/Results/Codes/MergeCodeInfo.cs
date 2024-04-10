using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Info", Namespace = "")]
    public class MergeCodeInfo
    {
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        [DataMember(Name = "Type")]
        public Enums.ResultInfoType InfoType { get; private set; }
        [DataMember(Name = "Value")]
        public string Value { get; private set; }
        [DataMember(Name = "RefObject")]
        public string ReferenceObject { get; private set; }
        [DataMember(Name = "Parameter")]
        public string ReferenceParameter { get; private set; }

        public MergeCodeInfo(Enums.ResultInfoType infoType, string value)
        {
            this.ID = 0;
            this.InfoType = infoType;
            this.Value = value;
        }
    }
}
