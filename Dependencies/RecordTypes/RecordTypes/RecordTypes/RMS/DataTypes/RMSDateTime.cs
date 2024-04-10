using System;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// DateTime Stored as YYYYMMDDHHMM
    /// </summary>
    public class RMSDateTime : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture); } catch { try { return DateTime.ParseExact(base.Value.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMddHHmm"); } }

        public RMSDateTime() : base(12) { }
    }
}
