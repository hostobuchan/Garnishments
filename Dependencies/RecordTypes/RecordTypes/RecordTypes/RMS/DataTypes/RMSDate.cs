using System;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Date Stored as YYYYMMDD
    /// </summary>
    public class RMSDate : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMdd"); } }

        public RMSDate() : base(8) { }
    }
}
