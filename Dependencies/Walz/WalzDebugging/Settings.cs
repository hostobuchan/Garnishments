using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WalzDebugging.Settings
{
    static class Connections
    {
        public static string ALLCLS { get { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } }
        public static string ControlPanels { get { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } }
    }
}
