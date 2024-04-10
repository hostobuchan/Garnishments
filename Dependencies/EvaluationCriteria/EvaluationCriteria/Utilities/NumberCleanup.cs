using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EvaluationCriteria
{
    public static partial class Utilities
    {
        public static string NumberCleanup(string theString, Type theType)
        {
            if (!string.IsNullOrEmpty(theString))
            {
                if (theType == typeof(int))
                {
                    Regex regex = new Regex(@"\d");
                    MatchCollection match = regex.Matches(theString);
                    StringBuilder sb = new StringBuilder();
                    foreach (Match m in match)
                    {
                        if (m.Success)
                        {
                            sb.Append(m.Value);
                        }
                    }
                    return sb.ToString();
                }
                else if (theType == typeof(double))
                {
                    Regex regex = new Regex(@"[\d\.]");
                    MatchCollection match = regex.Matches(theString);
                    StringBuilder sb = new StringBuilder();
                    foreach (Match m in match)
                    {
                        if (m.Success)
                        {
                            sb.Append(m.Value);
                        }
                    }
                    return sb.ToString();
                }
                return theString;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
