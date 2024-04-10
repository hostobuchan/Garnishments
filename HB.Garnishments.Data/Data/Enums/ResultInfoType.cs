using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Enums
{
    [DataContract]
    public enum ResultInfoType
    {
        [EnumMember(Value = "0")]
        String = 0,
        [EnumMember(Value = "1")]
        Date = 1,
        [EnumMember(Value = "2")]
        Money = 2
    }
}
