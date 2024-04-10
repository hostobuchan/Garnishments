using System;
using System.Linq;

namespace RecordTypes.Banko.Base.DataTypes
{
    /// <summary>
    /// Basic Character String
    /// </summary>
    public class BankoString : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

        public BankoString(int DataLength) : base(DataLength) { }
    }
    /// <summary>
    /// Date Stored as CCYYMMDD
    /// </summary>
    public class BankoDate : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private string _Format;
        public new virtual DateTime? Value { get { try { return DateTime.ParseExact(base.Value, _Format, System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString(_Format); } }

        public BankoDate(string format = "yyyyMMdd", int length = 8) : base(length)
        {
            _Format = format;
        }
    }
    /// <summary>
    /// Date and Time Stored as CCYYMMDDHH:MM
    /// </summary>
    public class BankoDateTime : BankoDate
    {
        public BankoDateTime() : base("yyyyMMddHH:mm", 13) { }
    }
    /// <summary>
    /// Date and Time Stored as CCYYMMDDHHMM
    /// </summary>
    public class LitDateTime : BankoDate
    {
        public LitDateTime() : base("yyyyMMddHHmm", 12) { }
    }
    /// <summary>
    /// Number with shown decimal
    /// </summary>
    public class BankoDecimal : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private int Scale { get; set; }

        public new decimal? Value { get { try { return decimal.Parse(base.Value.RemoveSpecialCharacters()); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("F" + Scale.ToString()); } }

        public BankoDecimal(int Precision, int Scale) : base(Precision) { this.Scale = Scale; }
    }
    /// <summary>
    /// Basic Number with no decimals
    /// </summary>
    public class BankoNumber : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.DataString = value == null || value < 0 || value > 10 * (base.DataLength + 1) ? "" : value.ToString().PadLeft(base.DataLength, '0'); } }

        public BankoNumber(int DataLength) : base(DataLength) { }
    }
    /// <summary>
    /// Boolean data determined by the presence of char/string
    /// </summary>
    public class BankoBool : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private string Compare;
        private string Fail;
        public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

        public BankoBool(int DataLength, string CompareString, string FailString = "")
            : base(DataLength)
        {
            this.Compare = CompareString;
            this.Fail = FailString;
        }
    }
    /// <summary>
    /// Data is associated with a specific list
    /// </summary>
    /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
    public class BankoEnum<T> : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(T), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = Convert.ToInt32(value).ToString().PadLeft(base.DataLength, '0'); } }

        public BankoEnum(int DataLength) : base(DataLength) { }
    }
    /// <summary>
    /// Data is associated with a specific list
    /// </summary>
    /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
    /// <typeparam name="Q">Enumeration of List of Values</typeparam>
    public class BankoEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = value.ToString() == "Nothing" ? "".PadRight(base.DataLength, ' ') : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

        public BankoEnum(int DataLength) : base(DataLength) { }
    }
    /// <summary>
    /// Key Matching Fields
    /// </summary>
    public class BankoMatches : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public bool SSN { get { return base.Value.Contains('S'); } }
        public bool SSN_Redacted { get { return base.Value.Contains('R'); } }
        public bool SSN_Probable { get { return base.Value.Contains('P'); } }
        public bool Name { get { return base.Value.Contains('N'); } }
        public bool Address { get { return base.Value.Contains('A'); } }
        public bool CityState { get { return base.Value.Contains('C'); } }
        public bool Zip { get { return base.Value.Contains('Z'); } }
        public int? Matches { get { try { return int.Parse(base.Value.Substring(0, base.Value.IndexOf('-'))); } catch { return null; } } }

        public BankoMatches(int DataLength) : base(DataLength) { }
    }
}
