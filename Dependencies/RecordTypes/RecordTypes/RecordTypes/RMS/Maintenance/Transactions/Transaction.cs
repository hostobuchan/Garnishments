using RecordTypes.RMS.DataTypes;

namespace RecordTypes.RMS.Maintenance.Transactions
{
    public class Transaction
    {
        public RMSString FieldValue { get; private set; }

        public Transaction(string FieldValue)
        {
            this.FieldValue = new RMSString(40) { DataString = FieldValue };
        }

        public override string ToString()
        {
            return this.FieldValue.ToString();
        }
    }
}
