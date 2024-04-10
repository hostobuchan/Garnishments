using RecordTypes.RMS.DataTypes;

namespace RecordTypes.RMS.Maintenance.Transactions
{
    public class DateTransaction : Transaction
    {
        public new RMSDate FieldValue { get; private set; }
        public RMSString Filler { get; private set; }

        public DateTransaction(string FieldValue) : base(FieldValue)
        {
            this.FieldValue = new RMSDate() { DataString = FieldValue };
            this.Filler = new RMSString(32) { DataString = FieldValue.Substring(8) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                this.FieldValue,
                this.Filler);
        }
    }
}
