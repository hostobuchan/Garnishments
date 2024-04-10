using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Enums
{
    [DataContract(Namespace = "")]
    public enum Status
    {
        [EnumMember(Value = "0")]
        Requested = 0,
        [EnumMember(Value = "1")]
        Canceled = 1,
        [EnumMember(Value = "2")]
        Rejected = 2,
        [EnumMember(Value = "3")]
        Processed = 3,
        [EnumMember(Value = "4")]
        Signed = 4,
        [EnumMember(Value = "5")]
        Filed = 5,
        [EnumMember(Value = "6")]
        CourtResponse = 6,
        [EnumMember(Value = "7")]
        GarnisheeResponse = 7,
        [EnumMember(Value = "8")]
        HearingRequested = 8
    }
}
