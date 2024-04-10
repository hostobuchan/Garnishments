using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Walz.Data.Settings
{
    static class Connections
    {
        public static string ALLCLS { get { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } }
        public static string ControlPanels { get { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } }
        public static string Walz { get { return ConfigurationManager.ConnectionStrings["Walz"].ConnectionString; } }
    }

    static class Locations
    {
        public static string SaveLocation { get { return ConfigurationManager.AppSettings["SaveLocation"]; } }
        public static string SFTP_TO_Location { get { return ConfigurationManager.AppSettings["SFTP_TO_Location"]; } }
        public static string SFTP_FROM_Location { get { return ConfigurationManager.AppSettings["SFTP_FROM_Location"]; } }
    }
}
