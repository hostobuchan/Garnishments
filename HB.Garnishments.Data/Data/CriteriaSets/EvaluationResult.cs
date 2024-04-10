using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CriteriaSets
{
    public struct EvaluationResult
    {
        public EvaluationSet Set { get; set; }
        public EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> Criteria { get; set; }
        public Accounts.EvaluateeAccount Account { get; set; }
        public Requests.AssetRequest Request { get; set; }
        public EvaluationCriteria.CriteriaSets.CriteriaParameters.Base.Eval Result { get; set; }

        public override string ToString()
        {
            return $"[{(Result.Success ? "PASS" : "FAIL")}] {Set.Description} - {Criteria.Name} - {Result.Info}";
        }
    }
}
