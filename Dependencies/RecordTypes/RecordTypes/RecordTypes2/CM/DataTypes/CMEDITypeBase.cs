using RecordTypes.EDI.EDIDataTypes;

namespace RecordTypes2.CM.DataTypes
{
    public abstract class CMEDITypeBase : EncapsulatedDataType
    {
        public CMEDITypeBase(StringHolder Data, int DataLength = 0, bool PaddingCharacters = false, char PaddingCharacter = ' ') : base(Data, DataLength, PaddingCharacters, PaddingCharacter)
        {
        }
    }
}
