using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Serialization.Surrogates
{
    [DataContract(Name = "RequestStatus", Namespace = "")]
    struct RequestStatusSurrogate
    {
        [DataMember(Name = "UID")]
        public ulong ID { get; private set; }
        [DataMember(Name = "RID")]
        public int RequestID { get; private set; }
        [DataMember(Name = "STATUS")]
        public Enums.Status Type { get; private set; }
        [DataMember(Name = "DATE")]
        public DateTime Date { get; private set; }
        [DataMember(Name = "USERID")]
        public int UserID { get; private set; }
        [DataMember(Name = "RESULT")]
        public short? ResultID { get; private set; }
        [DataMember(Name = "NOTE")]
        public string Note { get; private set; }
    }
}
