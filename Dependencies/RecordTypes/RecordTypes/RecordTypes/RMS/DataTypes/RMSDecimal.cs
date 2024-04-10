using System;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Signed Number without displayed decimal
    /// </summary>
    public class RMSDecimal : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private int Precision;
        public new decimal? Value { get { try { return decimal.Parse(base.Value) / (decimal)(Math.Pow(10, Precision)); } catch { return null; } } set { base.DataString = value == null ? "" : (value.Value * (decimal)(Math.Pow(10, Precision))).ToString("F0").PadLeft(base.DataLength, '0'); } }

        public RMSDecimal(int Scale, int Precision)
            : base(Scale)
        {
            this.Precision = Precision;
        }
    }
}
