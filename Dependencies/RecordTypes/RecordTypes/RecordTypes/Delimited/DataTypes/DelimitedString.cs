using RecordTypes.EDI.EDIDataTypes;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedString : EncapsulatedDataType
    {
        public new string Value { get { return base.Value; } set { base.Value = value; } }

        public DelimitedString(StringHolder DataString) : base(DataString) { }
    }
}
