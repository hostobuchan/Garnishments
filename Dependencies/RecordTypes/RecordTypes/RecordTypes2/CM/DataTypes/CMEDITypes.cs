using RecordTypes.Delimited.DataTypes;
using RecordTypes.EDI.EDIDataTypes;
using System.Collections.Generic;

namespace RecordTypes2.CM.DataTypes
{
    public class CMEDIString : DelimitedString
    {
        public CMEDIString(StringHolder DataString) : base(DataString)
        {
        }
    }

    public class CMEDINumber : DelimitedNumber
    {
        public CMEDINumber(StringHolder DataString, int Length = 0, bool Padded = false, char PaddingCharacter = '0') : base(DataString, Length, Padded, PaddingCharacter)
        {
        }
    }

    public class CMEDIDecimal : DelimitedDecimal
    {
        public CMEDIDecimal(StringHolder DataString, int Precision = 0, int Scale = 2) : base(DataString, Precision, Scale)
        {
        }
    }

    public class CMEDIDate : DelimitedDate
    {
        public CMEDIDate(StringHolder DataString) : base(DataString, "MM/dd/yyyy")
        {
        }
    }

    public class CMEDITime : DelimitedDate
    {
        public CMEDITime(StringHolder DataString) : base(DataString, "HH:mm:ss")
        {
        }
    }

    public class CMEDIEnum<T> : DelimitedEnum<T>
    {
        public CMEDIEnum(StringHolder DataString, int DataLength = 2, bool Padded = true) : base(DataString, DataLength, Padded)
        {
        }
    }

    public class CMEDIEnum<T, Q> : DelimitedEnum<T, Q>
    {
        public CMEDIEnum(StringHolder DataString) : base(DataString)
        {
        }
    }

    public class CMEDIDictionary<T> : DelimitedDictionary<T> where T : struct
    {
        public CMEDIDictionary(StringHolder DataString, Dictionary<string, T> Lookup) : base(DataString, Lookup)
        {
        }
    }

    public class CMEDIBool : DelimitedBool
    {
        public CMEDIBool(StringHolder DataString, string Affirmative = "Y", string Negative = "N") : base(DataString, Affirmative, Negative)
        {
        }
    }
}
