using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CriteriaSets
{
    public class EmptySimpleSet : EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor>
    {
        public EmptySimpleSet(string description) : base(new Dictionary<string, object>(), 0, description) { }
    }
}
