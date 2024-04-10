using RecordTypes.EDI.EDIDataTypes;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedBool : EncapsulatedDataType
    {
        private string Affirmative;
        private string Negative;
        public new bool? Value { get { try { return string.IsNullOrEmpty(base.Value) ? null : (bool?)(base.Value == this.Affirmative); } catch { return null; } } set { base.Value = !value.HasValue ? "" : value.Value ? this.Affirmative : this.Negative; } }

        public DelimitedBool(StringHolder DataString, string Affirmative = "Y", string Negative = "N") : base(DataString)
        {
            this.Affirmative = Affirmative;
            this.Negative = Negative;
        }
    }
}
