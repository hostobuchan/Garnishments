using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Accounts
{
    [DataContract(Name = "EvaluateeDatahandler", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class EvaluateeDataHandler : EvaluationCriteria.Accounts.EvaluateeDataHandler<EvaluateeAccount, EvaluateeDebtor>
    {
        public static new EvaluateeDataHandler GetDataHandler()
        {
            return GetDataHandler(typeof(EvaluateeDataHandler)) as EvaluateeDataHandler;
        }
        public static new async Task<EvaluateeDataHandler> GetDataHandlerAsync()
        {
            return (await GetDataHandlerAsync(typeof(EvaluateeDataHandler))) as EvaluateeDataHandler;
        }
        public static new EvaluateeDataHandler GetDataHandler(IEnumerable<string> accounts)
        {
            return GetDataHandler(typeof(EvaluateeDataHandler), accounts) as EvaluateeDataHandler;
        }
        public static new async Task<EvaluateeDataHandler> GetDataHandlerAsync(IEnumerable<string> accounts)
        {
            return (await GetDataHandlerAsync(typeof(EvaluateeDataHandler), accounts)) as EvaluateeDataHandler;
        }
    }
}
