using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Settings
{
    static class Connections
    {
        public static string ALLCLS { get { try { return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ALLCLS\" Entry"); } } }
        public static string ControlPanels { get { try { return ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"ControlPanels\" Entry"); } } }
        public static string STDB { get { try { return ConfigurationManager.ConnectionStrings["STDB"].ConnectionString; } catch { throw new NullReferenceException("App Connection Strings Missing \"STDB\" Entry"); } } }
    }
    static class Properties
    {
        public static string Location_Certificates { get { return ConfigurationManager.AppSettings["DownloadFolder"]; } }
        public static string SaveLocation_Uploads { get { return ConfigurationManager.AppSettings["SaveFolder_Uploads"]; } }
        public static string SaveLocation_Downloads { get { return ConfigurationManager.AppSettings["SaveFolder_Downloads"]; } }
        public static string SaveLocation_AccountCertificate { get { return ConfigurationManager.AppSettings["SaveFolder_AccountCertificate"]; } }
        public static string Naming_Certificates { get { return ConfigurationManager.AppSettings["Naming_Certificates"]; } }
        public static string Naming_Hits { get { return ConfigurationManager.AppSettings["Naming_Hits"]; } }
    }
}
