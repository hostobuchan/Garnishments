using RecordTypes.EDI.EDIDataTypes;
using System;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedEnum<T> : DelimitedNumber
    {
        public new T Value { get { try { return (T)Enum.ToObject(typeof(T), base.Value.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = Convert.ToInt32(value); } }

        public DelimitedEnum(StringHolder DataString, int DataLength = 2, bool Padded = true) : base(DataString, DataLength, Padded) { }
    }
    public class DelimitedEnum<T, Q> : EncapsulatedDataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = value.ToString() == "Nothing" ? "" : ((Q)Enum.ToObject(typeof(T), value)).ToString(); } }

        public DelimitedEnum(StringHolder DataString) : base(DataString) { }
    }
}
