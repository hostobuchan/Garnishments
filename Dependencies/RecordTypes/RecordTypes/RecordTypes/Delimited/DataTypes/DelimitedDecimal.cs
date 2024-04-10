using RecordTypes.EDI.EDIDataTypes;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedDecimal : EncapsulatedDataType
    {
        private int Scale { get; set; }
        public new decimal? Value
        {
            get { try { return decimal.Parse(base.Value); } catch { return null; } }
            set { base.Value = value.HasValue ? value.Value.ToString(string.Format("F{0}", this.Scale)) : (0).ToString(string.Format("F{0}", this.Scale)); }
        }

        public DelimitedDecimal(StringHolder DataString, int Precision = 0, int Scale = 2) : base(DataString, Precision)
        {
            this.Scale = Scale;
        }
    }
}
