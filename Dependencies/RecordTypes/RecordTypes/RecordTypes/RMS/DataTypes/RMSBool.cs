namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Boolean data determined by the presence of char/string
    /// </summary>
    public class RMSBool : RecordTypes.EDI.EDIDataTypes.DataType
    {
        private string Compare;
        private string Fail;
        public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

        public RMSBool(int DataLength, string CompareString, string FailString = "")
            : base(DataLength)
        {
            this.Compare = CompareString;
            this.Fail = FailString;
        }
    }
}
