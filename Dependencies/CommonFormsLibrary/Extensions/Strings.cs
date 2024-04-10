using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static partial class Extensions
    {
        public static string NumbersOnly(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                StringBuilder sb = new StringBuilder();
                foreach (Match match in Regex.Matches(input, @"\d"))
                {
                    sb.Append(match.Value);
                }
                return sb.ToString();
            }
            else
            {
                return input;
            }
        }
        public static string AsPhone(this string input)
        {
            var numbers = input.NumbersOnly();
            if (!string.IsNullOrEmpty(numbers))
            {
                return numbers.Length == 11 ? $"+{numbers.Substring(0, 1)} ({numbers.Substring(1, 3)}) {numbers.Substring(4, 3)}-{numbers.Substring(7)}" : numbers.Length == 10 ? $"({numbers.Substring(0, 3)}) {numbers.Substring(3, 3)}-{numbers.Substring(6)}" : numbers.Length == 7 ? $"{numbers.Substring(0, 3)}-{numbers.Substring(3)}" : numbers;
            }
            else
            {
                return input;
            }
        }
        public static string AsZip(this string input)
        {
            var numbers = input.NumbersOnly();
            if (!string.IsNullOrEmpty(numbers))
            {
                return numbers.Length == 9 ? $"{numbers.Substring(0, 5)}-{numbers.Substring(5)}" : numbers;
            }
            else
            {
                return input;
            }
        }
    }
}
