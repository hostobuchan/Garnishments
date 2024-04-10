using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Enums
{
    [DataContract]
    public enum AssetType
    {
        [EnumMember(Value = "0")]
        Employer = 0,
        [EnumMember(Value = "1")]
        Bank = 1,
        [EnumMember(Value = "2")]
        Vehicle = 2,
        [EnumMember(Value = "3")]
        RealEstate = 3,
        [EnumMember(Value = "4")]
        Other = 4
    }
}
