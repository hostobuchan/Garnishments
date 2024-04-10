using System;
using System.Text;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Number without displayed decimal
    /// </summary>
    public class RMSZoneDecimal : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private int Precision;
        public new decimal? Value
        {
            get
            {
                try
                {
                    return FromZoneDecimal(base.Value, Precision);
                }
                catch { return null; }
            }
            set
            {
                if (value == null)
                {
                    base.DataString = "".PadRight(base.DataLength, '0');
                }
                else
                {
                    string Zone = ToZoneDecimal(value.Value, Precision);
                    base.DataString = Zone.PadLeft(base.DataLength, '0');
                }
            }
        }

        public RMSZoneDecimal(int Scale, int Precision) : base(Scale)
        {
            this.Precision = Precision;
        }

        public static decimal FromZoneDecimal(string Input, int Precision)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[\d\.\}\{A-R]");
            StringBuilder sb = new StringBuilder();
            bool IsNeg = false;
            foreach (System.Text.RegularExpressions.Match M in regex.Matches(Input))
            {
                regex = new System.Text.RegularExpressions.Regex(@"[\}\{A-R]");
                System.Text.RegularExpressions.Match M2 = regex.Match(M.Value);
                if (M2.Success)
                {
                    switch (M2.Value)
                    {
                        case "}":
                            sb.Append("0");
                            IsNeg = true;
                            break;
                        case "{":
                            sb.Append("0");
                            IsNeg = false;
                            break;
                        case "J":
                            sb.Append("1");
                            IsNeg = true;
                            break;
                        case "A":
                            sb.Append("1");
                            IsNeg = false;
                            break;
                        case "K":
                            sb.Append("2");
                            IsNeg = true;
                            break;
                        case "B":
                            sb.Append("2");
                            IsNeg = false;
                            break;
                        case "L":
                            sb.Append("3");
                            IsNeg = true;
                            break;
                        case "C":
                            sb.Append("3");
                            IsNeg = false;
                            break;
                        case "M":
                            sb.Append("4");
                            IsNeg = true;
                            break;
                        case "D":
                            sb.Append("4");
                            IsNeg = false;
                            break;
                        case "N":
                            sb.Append("5");
                            IsNeg = true;
                            break;
                        case "E":
                            sb.Append("5");
                            IsNeg = false;
                            break;
                        case "O":
                            sb.Append("6");
                            IsNeg = true;
                            break;
                        case "F":
                            sb.Append("6");
                            IsNeg = false;
                            break;
                        case "P":
                            sb.Append("7");
                            IsNeg = true;
                            break;
                        case "G":
                            sb.Append("7");
                            IsNeg = false;
                            break;
                        case "Q":
                            sb.Append("8");
                            IsNeg = true;
                            break;
                        case "H":
                            sb.Append("8");
                            IsNeg = false;
                            break;
                        case "R":
                            sb.Append("9");
                            IsNeg = true;
                            break;
                        case "I":
                            sb.Append("9");
                            IsNeg = false;
                            break;
                    }
                }
                else
                {
                    sb.Append(M.Value);
                }
            }

            decimal Result = decimal.Parse(sb.ToString()) / (decimal)(Math.Pow(10, Precision));
            if (IsNeg) Result *= (decimal)(-1);
            return Result;
        }
        public static string ToZoneDecimal(decimal Input, int Precision, bool PositiveAlso = false)
        {
            string Compare = Input.ToString("F" + Precision.ToString()).Replace(".", "");
            StringBuilder sb = new StringBuilder();
            if (Input > 0)
            {
                sb.Append(Compare.Substring(0, Compare.Length - 1));
            }
            else
            {
                sb.Append(Compare.Substring(1, Compare.Length - 2));
            }
            switch (Compare.ToCharArray()[Compare.ToCharArray().GetUpperBound(0)])
            {
                case '0':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("{");
                        else
                            sb.Append("0");
                    }
                    else
                        sb.Append("}");
                    break;
                case '1':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("A");
                        else
                            sb.Append("1");
                    }
                    else
                        sb.Append("J");
                    break;
                case '2':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("B");
                        else
                            sb.Append("2");
                    }
                    else
                        sb.Append("K");
                    break;
                case '3':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("C");
                        else
                            sb.Append("3");
                    }
                    else
                        sb.Append("L");
                    break;
                case '4':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("D");
                        else
                            sb.Append("4");
                    }
                    else
                        sb.Append("M");
                    break;
                case '5':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("E");
                        else
                            sb.Append("5");
                    }
                    else
                        sb.Append("N");
                    break;
                case '6':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("F");
                        else
                            sb.Append("6");
                    }
                    else
                        sb.Append("O");
                    break;
                case '7':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("G");
                        else
                            sb.Append("7");
                    }
                    else
                        sb.Append("P");
                    break;
                case '8':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("H");
                        else
                            sb.Append("8");
                    }
                    else
                        sb.Append("Q");
                    break;
                case '9':
                    if (Input > 0)
                    {
                        if (PositiveAlso)
                            sb.Append("I");
                        else
                            sb.Append("9");
                    }
                    else
                        sb.Append("R");
                    break;
            }
            return sb.ToString();
        }
    }
}
