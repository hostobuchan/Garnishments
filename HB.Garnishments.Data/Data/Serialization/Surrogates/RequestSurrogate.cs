using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Serialization.Surrogates
{
    [DataContract(Name = "Request", Namespace = "")]
    struct RequestSurrogate
    {
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        [DataMember(Name = "FILENO")]
        public string FileNo { get; private set; }
        [DataMember(Name = "DEBTOR")]
        public byte Debtor { get; private set; }
        [DataMember(Name = "ANID")]
        public int AccountID { get; private set; }
        [DataMember(Name = "AIID")]
        public long AssetInfoID { get; private set; }
        [DataMember(Name = "RAID")]
        public int? RegisteredAgentID { get; private set; }
    }
}
