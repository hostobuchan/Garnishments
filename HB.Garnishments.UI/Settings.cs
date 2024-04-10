using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.UI.Settings
{
    static class Connections
    {
        public static string ALLCLS { get { try { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ALLCLS\" Entry"); } } }
        public static string ControlPanels { get { try { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ControlPanels\" Entry"); } } }
        public static string Garnishments { get { try { return ConfigurationManager.ConnectionStrings["Garnishments"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"Garnishments\" Entry"); } } }
        public static string Walz { get { try { return ConfigurationManager.ConnectionStrings["Walz"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"Walz\" Entry"); } } }
    }

    public static class Communications
    {
        public static Data.Communications.HubManager HubConnection { get; private set; } = new Data.Communications.HubManager();
    }
}
