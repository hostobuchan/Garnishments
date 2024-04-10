namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Basic Character String
    /// </summary>
    public class RMSString : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

        public RMSString(int DataLength) : base(DataLength) { }
    }
}
