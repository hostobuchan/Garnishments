using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.UI.Settings
{
    public static class Properties
    {
        static UserPrincipal _User;
        static EvaluationCriteria.CriteriaSets.State[] _States;

        public static UserPrincipal User { get { if (_User == null) _User = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), Environment.UserName); return _User; } }
        public static EvaluationCriteria.CriteriaSets.State[] States { get { if (_States == null) _States = Task.Run(async () => { return await Data.DataHandler.GetStatesAsync(); }).Result; return _States; } }
        public static Data.Assets.Base.RegisteredAgent[] RegisteredAgents { get { return Data.Settings.Properties.RegisteredAgents; } set { Data.Settings.Properties.RegisteredAgents = value; } }
        public static Data.Accounts.Venue[] Venues { get { return Data.Settings.Properties.Venues; } set { Data.Settings.Properties.Venues = value; } }
        public static Data.Accounts.Counsel[] Counsels { get { return Data.Settings.Properties.Counsels; } set { Data.Settings.Properties.Counsels = value; } }
    }
}
