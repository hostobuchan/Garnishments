using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    [DataContract(Name = "CostModifier", Namespace = "")]
    public enum CostModifier : byte
    {
        [EnumMember(Value = "0")]
        Unknown = 0,
        [EnumMember(Value = "1")]
        AddFixedCost = 1,
        [EnumMember(Value = "2")] 
        AddPerOzCost = 2,
        [EnumMember(Value = "3")] 
        SetMaxCost = 3
    }
}
