using System.Text;
using System.Text.RegularExpressions;

namespace RecordTypes
{
    public static class Extensions
    {
        public static string NumbersOnly(this string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                StringBuilder sb = new StringBuilder();
                Regex reg = new Regex(@"\d");
                foreach (Match M in reg.Matches(Input))
                {
                    if (M.Success) sb.Append(M.Value);
                }
                return sb.ToString();
            }
            else
            {
                return Input;
            }
        }

        public static string RemoveSpecialCharacters(this string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return Regex.Replace(Input, @"[^\w\.@-]", "");
            }
            else
            {
                return Input;
            }
        }
    }
}
