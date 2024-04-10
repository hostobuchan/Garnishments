using System;
using System.Configuration;

namespace RemoteFile.Settings
{
    static class Connections
    {
        public static string ALLCLS { get { try { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ALLCLS\" Entry"); } } }
        public static string ControlPanels { get { try { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ControlPanels\" Entry"); } } }
    }
}
