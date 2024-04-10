using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace HB.SkipTracing.Data.Settings
{
    public static class Connections
    {
        public static string ALLCLS { get { try { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ALLCLS\" Entry"); } } }
        public static string ControlPanels { get { try { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ControlPanels\" Entry"); } } }
        public static string STDB { get { try { return ConfigurationManager.ConnectionStrings["STDB"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"STDB\" Entry"); } } }
    }

    internal static class Properties
    {
        private static string _UserName;
        public static string UserName { get { if (_UserName == null) _UserName = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), Environment.UserName).SamAccountName; return _UserName; } }
    }
}
