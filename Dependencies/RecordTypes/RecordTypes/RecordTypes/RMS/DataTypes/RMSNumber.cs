namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Basic Number with no decimals
    /// </summary>
    public class RMSNumber : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.DataString = value.ToString().PadLeft(base.DataLength, '0'); } }

        public RMSNumber(int DataLength) : base(DataLength) { }
    }
}
