using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace HB.Garnishments.Data.Settings
{
    public static class Connections
    {
        private static string _ALLCLS;
        private static string _ControlPanels;
        private static string _Garnishments;
        private static string _STDB;
        private static string _Walz;

        public static string ALLCLS
        {
            get
            {
                if (_ALLCLS == null)
                {
                    try
                    {
                        _ALLCLS =  ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString;
                    }
                    catch
                    {
                        throw new NullReferenceException("App Connection Strings Missing \"ALLCLS\" Entry");
                    }
                }
                return _ALLCLS;
            }
            set
            {
                _ALLCLS = value;
            }
        }
        public static string ControlPanels
        {
            get
            {
                if (_ControlPanels == null)
                {
                    try
                    {
                        _ControlPanels = ConfigurationManager.ConnectionStrings["ControlPanels"].ConnectionString;
                    }
                    catch { throw new NullReferenceException("App Connection Strings Missing \"ControlPanels\" Entry"); }
                }
                return _ControlPanels;
            }
            set
            {
                _ControlPanels = value;
            }
        }
        public static string Garnishments
        {
            get
            {
                if (_Garnishments == null)
                {
                    try
                    {
                        _Garnishments = ConfigurationManager.ConnectionStrings["Garnishments"].ConnectionString;
                    }
                    catch { throw new NullReferenceException("App Connection Strings Missing \"Garnishments\" Entry"); }
                }
                return _Garnishments;
            }
            set
            {
                _Garnishments = value;
            }
        }
        public static string STDB
        {
            get
            {
                if (_STDB == null)
                {
                    try
                    {
                        _STDB = ConfigurationManager.ConnectionStrings["STDB"].ConnectionString;
                    }
                    catch { throw new NullReferenceException("App Connection Strings Missing \"STDB\" Entry"); }
                }
                return _STDB;
            }
            set
            {
                _STDB = value;
            }
        }
        public static string Walz
        {
            get
            {
                if (_Walz == null)
                {
                    try
                    {
                        _Walz = ConfigurationManager.ConnectionStrings["Walz"].ConnectionString;
                    }
                    catch { throw new NullReferenceException("App Connection Strings Missing \"Walz\" Entry"); }
                }
                return _Walz;
            }
            set
            {
                _Walz = value;
            }
        }
    }
    public static class Properties
    {
        // Criteria Related Items
        private static string _SQLStates;
        private static string _SQLRoutines;
        private static string _SQLEvalSets;
        public static string SQLStates
        {
            get
            {
                if (_SQLStates == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["States"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"States\" Entry");
                    }
                }
                return _SQLStates;
            }
            set
            {
                _SQLStates = value;
            }
        }
        public static string SQLRoutines
        {
            get
            {
                if (_SQLRoutines == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["Routines"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"Routines\" Entry");
                    }
                }
                return _SQLRoutines;
            }
            set
            {
                _SQLRoutines = value;
            }
        }
        public static string SQLEvalSets
        {
            get
            {
                if (_SQLEvalSets == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["EvalSets"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"EvalSets\" Entry");
                    }
                }
                return _SQLEvalSets;
            }
            set
            {
                _SQLEvalSets = value;
            }
        }


        // Lookup Items
        public static Data.Assets.Base.RegisteredAgent[] RegisteredAgents { get; set; }
        public static Data.Accounts.Venue[] Venues { get; set; }
        public static Data.Accounts.Counsel[] Counsels { get; set; }
        // Static Values -- User-Based
        public static string FaxNumber { get { return RegistryHandler.ReadString("SOFTWARE\\Hosto & Buchan, P.L.L.C.", "Fax"); } set { RegistryHandler.WriteValue("SOFTWARE\\Hosto & Buchan, P.L.L.C.", "Fax", value); } }

        // RNN SCRA Files
        public static string SCRADownloadFolder { get { try { return ConfigurationManager.AppSettings["SCRA_SaveFolder_Downloads"]; } catch { throw new NullReferenceException("App Settings Missing \"SCRA_SaveFolder_Downloads\" Entry"); } } }
    }
    static class StoredProcedures
    {
        // Routine
        public static string Remove_Routine { get { try { return ConfigurationManager.AppSettings["Action_Remove_Routine"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_Routine\" Entry"); } } }
        // Routine Evaluations
        public static string Add_RoutineEval { get { try { return ConfigurationManager.AppSettings["Action_Add_RoutineEval"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Add_RoutineEval\" Entry"); } } }
        public static string Remove_RoutineEval { get { try { return ConfigurationManager.AppSettings["Action_Remove_RoutineEval"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_RoutineEval\" Entry"); } } }
        public static string Update_RoutineEval { get { try { return ConfigurationManager.AppSettings["Action_Update_RoutineEval"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Update_RoutineEval\" Entry"); } } }
        // Evaluation Sets
        public static string Add_EvalSet { get { try { return ConfigurationManager.AppSettings["Action_Add_EvalSet"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Add_EvalSet\" Entry"); } } }
        public static string Remove_EvalSet { get { try { return ConfigurationManager.AppSettings["Action_Remove_EvalSet"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_EvalSet\" Entry"); } } }
        // Code Lists
        public static string Add_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Add_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Add_CodeList\" Entry"); } } }
        public static string Remove_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Remove_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_CodeList\" Entry"); } } }
        public static string Update_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Update_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Update_CodeList\" Entry"); } } }
    }
}
