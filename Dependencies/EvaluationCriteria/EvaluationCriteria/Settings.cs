using System;
using System.Configuration;

namespace EvaluationCriteria.Settings
{
    public static class Connections
    {
        private static string _ALLCLS;
        private static string _CriteriaDB;

        public static string ALLCLS
        {
            get
            {
                if (_ALLCLS == null)
                {
                    try
                    {
                        return ConfigurationManager.ConnectionStrings["ALLCLS"].ConnectionString;
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
        public static string CriteriaDB
        {
            get
            {
                if (_CriteriaDB == null)
                {
                    try
                    {
                        return ConfigurationManager.ConnectionStrings["CriteriaDB"].ConnectionString;
                    }
                    catch
                    {
                        throw new NullReferenceException("App Connection Strings Missing \"CriteriaDB\" Entry");
                    }
                }
                return _CriteriaDB;
            }
            set
            {
                _CriteriaDB = value;
            }
        }

    }

    public static class Properties
    {
        private static string _SQLSets;
        private static string _SQLOptions;
        private static string _SQLCodeLists;
        private static string _SQLCodes;
        private static string _SQLCodeStrings;

        public static string SQLSets
        {
            get
            {
                if (_SQLSets == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["CriteriaSets"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"CriteriaSets\" Entry");
                    }
                }
                return _SQLSets;
            }
            set
            {
                _SQLSets = value;
            }
        }
        public static string SQLOptions
        {
            get
            {
                if (_SQLOptions == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["CriteriaOptions"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"CriteriaOptions\" Entry");
                    }
                }
                return _SQLOptions;
            }
            set
            {
                _SQLOptions = value;
            }
        }
        public static string SQLCodeLists
        {
            get
            {
                if (_SQLCodeLists == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["CriteriaCodeLists"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"CriteriaCodeLists\" Entry");
                    }
                }
                return _SQLCodeLists;
            }
            set
            {
                _SQLCodeLists = value;
            }
        }
        public static string SQLCodes
        {
            get
            {
                if (_SQLCodes == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["CriteriaCodes"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"CriteriaCodes\" Entry");
                    }
                }
                return _SQLCodes;
            }
            set
            {
                _SQLCodes = value;
            }
        }
        public static string SQLCodeStrings
        {
            get
            {
                if (_SQLCodeStrings == null)
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["CriteriaCodeStrings"];
                    }
                    catch
                    {
                        throw new NullReferenceException("App Settings Missing \"CriteriaCodeStrings\" Entry");
                    }
                }
                return _SQLCodeStrings;
            }
            set
            {
                _SQLCodeStrings = value;
            }
        }

    }
    static class StoredProcedures
    {
        // Simple Sets
        public static string Add_SimpleSet { get { try { return ConfigurationManager.AppSettings["Action_Add_SimpleSet"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Add_SimpleSet\" Entry"); } } }
        public static string Remove_SimpleSet { get { try { return ConfigurationManager.AppSettings["Action_Remove_SimpleSet"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_SimpleSet\" Entry"); } } }
        public static string Update_SimpleSet { get { try { return ConfigurationManager.AppSettings["Action_Update_SimpleSet"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Update_SimpleSet\" Entry"); } } }
        // Code Lists
        public static string Add_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Add_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Add_CodeList\" Entry"); } } }
        public static string Remove_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Remove_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Remove_CodeList\" Entry"); } } }
        public static string Update_CodeList { get { try { return ConfigurationManager.AppSettings["Action_Update_CodeList"]; } catch { throw new NullReferenceException("App Settings Missing \"Action_Update_CodeList\" Entry"); } } }
    }
}
