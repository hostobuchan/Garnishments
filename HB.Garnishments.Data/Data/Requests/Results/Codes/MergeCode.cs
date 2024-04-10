using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Code", Namespace = "")]
    public class MergeCode
    {
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        [DataMember(Name = "XCODE")]
        public string XCode { get; private set; }
        [DataMember(Name = "Values")]
        public List<MergeCodeFieldValue> Values { get; private set; }


        public override string ToString()
        {
            return $"{this.XCode}";
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (Values == null) Values = new List<MergeCodeFieldValue>();
        }
    }
}
