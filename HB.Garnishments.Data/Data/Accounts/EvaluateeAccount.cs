using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Accounts
{
    [DataContract(Name = "EvaluateeAccount", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class EvaluateeAccount : EvaluationCriteria.Accounts.EvaluateeAccount<EvaluateeDebtor>
    {

    }
}
