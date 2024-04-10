using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Value", Namespace = "")]
    public class MergeCodeFieldValue
    {
        [DataMember(Name = "Field")]
        public MergeCodeField Field { get; private set; }
        [DataMember(Name = "Info")]
        public MergeCodeInfo Info { get; private set; }

        public MergeCodeFieldValue(MergeCodeField field, MergeCodeInfo value)
        {
            this.Field = field;
            this.Info = value;
        }
    }
}
