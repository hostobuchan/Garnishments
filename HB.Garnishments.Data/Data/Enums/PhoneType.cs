using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Enums
{
    [DataContract]
    public enum PhoneType
    {
        [EnumMember(Value = "0")]
        Home = 0,
        [EnumMember(Value = "1")]
        Work = 1,
        [EnumMember(Value = "2")]
        Cell = 2,
        [EnumMember(Value = "3")]
        Fax = 3,
        [EnumMember(Value = "4")]
        Other = 4
    }
}
