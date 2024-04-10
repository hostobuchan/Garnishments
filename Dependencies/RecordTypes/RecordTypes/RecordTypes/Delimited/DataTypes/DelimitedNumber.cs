using RecordTypes.EDI.EDIDataTypes;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedNumber : EncapsulatedDataType
    {
        public new virtual int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.Value = value.HasValue ? value.ToString() : ""; } }

        public DelimitedNumber(StringHolder DataString, int Length = 0, bool Padded = false, char PaddingCharacter = '0') : base(DataString, Length, Padded, PaddingCharacter) { }
    }
}