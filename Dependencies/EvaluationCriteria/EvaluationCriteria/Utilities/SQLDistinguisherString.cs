using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCriteria
{
    public static partial class Utilities
    {
        public static string SQLDistinguisherString(Dictionary<string, object> SQLDistinguisher)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, object> KVP in SQLDistinguisher)
            {
                sb.Append(string.Format("[{0}]={1} AND ", KVP.Key, KVP.Value is bool ? Convert.ToByte(KVP.Value) : KVP.Value));
            }
            return sb.Length > 5 ? sb.ToString().Substring(0, sb.Length - 5) : "";
        }
    }
}
