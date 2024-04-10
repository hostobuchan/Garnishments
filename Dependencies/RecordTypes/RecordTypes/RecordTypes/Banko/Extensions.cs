using System.Text;

namespace RecordTypes.Banko
{
    public static class Extensions
    {
        public static string BankruptcyCaseNo(this string Original)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w{2}-\w{5}");
            if (regex.Match(Original).Success && Original.Length == 8)
            {
                return Original;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                regex = new System.Text.RegularExpressions.Regex(@"\w");
                foreach (System.Text.RegularExpressions.Match M in regex.Matches(Original))
                {
                    if (M.Success)
                    {
                        sb.Append(M.Value);
                    }
                    if (sb.Length == 2)
                        sb.Append("-");
                }
                return sb.ToString();
            }
        }
    }
}
