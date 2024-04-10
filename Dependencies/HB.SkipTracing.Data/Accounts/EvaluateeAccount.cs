using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Accounts
{
    [DataContract(Name = "EvaluateeAccount", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class EvaluateeAccount : EvaluationCriteria.Accounts.EvaluateeAccount<EvaluateeDebtor>
    {
        public IEnumerable<EvaluateeDebtor> Debtors;

        [DataMember(Order = 56)]
        public string ForwarderID { get; private set; }
        [DataMember(Order = 57)]
        public string FirmID { get; private set; }
    }
}
