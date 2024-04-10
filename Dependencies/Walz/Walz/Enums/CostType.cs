using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    [DataContract(Name = "CostType", Namespace = "")]
    public enum CostType : byte
    {
        [EnumMember(Value = "0")]
        Unknown = 0,
        [EnumMember(Value = "1")]
        CertifiedMail = 1,
        [EnumMember(Value = "2")]
        ElectronicReturnReceipt = 2,
        [EnumMember(Value = "3")]
        RestrictedDelivery = 3,
    }
}
