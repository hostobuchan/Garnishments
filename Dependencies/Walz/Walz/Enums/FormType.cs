using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    [DataContract(Name = "FormType", Namespace = "")]
    public enum FormType : byte
    {
        [EnumMember(Value = "0")]
        Unknown = 0,
        [EnumMember(Value = "1")]
        None = 1,
        [EnumMember(Value = "2")]
        CertifiedEnvelope_10 = 2,
        [EnumMember(Value = "3")]
        CertifiedEnvelope_6x9 = 3,
        [EnumMember(Value = "4")]
        CertifiedEnvelope_9x12 = 4,
        [EnumMember(Value = "5")]
        SinglePieceMailer = 5
    }
}
