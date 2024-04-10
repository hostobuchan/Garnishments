using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Accounts
{
    [DataContract(Name = "EvaluateeDebtor", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class EvaluateeDebtor : EvaluationCriteria.Accounts.EvaluateeDebtor, Interfaces.IAddressable
    {
        [IgnoreDataMember]
        public new string Name { get { return $"{base.NameFirst} {base.NameMiddle} {base.NameLast}"; } }
        [IgnoreDataMember]
        public string Attention { get { return null; } }
    }
}
