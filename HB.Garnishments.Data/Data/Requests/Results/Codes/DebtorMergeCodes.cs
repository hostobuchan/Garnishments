using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Debtor", Namespace = "")]
    public class DebtorMergeCodes
    {
        [DataMember(Name = "Number")]
        public byte Number { get; private set; }
        [DataMember(Name = "Codes")]
        public List<MergeCode> Codes { get; private set; } = new List<MergeCode>();

        public DebtorMergeCodes(byte number)
        {
            this.Number = number;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (this.Codes == null) this.Codes = new List<MergeCode>();
        }
    }
}
