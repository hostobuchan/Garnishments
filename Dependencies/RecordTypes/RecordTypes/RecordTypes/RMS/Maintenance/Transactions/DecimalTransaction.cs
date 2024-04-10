using RecordTypes.RMS.DataTypes;

namespace RecordTypes.RMS.Maintenance.Transactions
{
    public class DecimalTransaction : Transaction
    {
        public new RMSZoneDecimal FieldValue { get; private set; }
        public RMSString Filler { get; private set; }

        public DecimalTransaction(string FieldValue) : base(FieldValue)
        {
            this.FieldValue = new RMSZoneDecimal(11, 2) { DataString = FieldValue };
            this.Filler = new RMSString(29) { DataString = FieldValue.Substring(11) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                this.FieldValue,
                this.Filler);
        }
    }
}
