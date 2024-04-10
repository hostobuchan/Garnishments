using RecordTypes.RMS.DataTypes;
using RecordTypes.RMS.Maintenance;
using RecordTypes.RMS.Maintenance.Transactions;

namespace RecordTypes.Ford.Maintenance
{
    public abstract class MaintenanceRecord : MaintenanceInfo
    {
        #region Public Properties
        public CitiMaintEnum<MaintenanceCode, MaintenanceCodeValue> FieldCode { get; private set; }
        public Transaction FieldValue { get { return this.GetTransaction(); } }
        public RMSEnum<IOFlag, IOFlagValue> IOFlag { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString RecovererID { get; private set; }
        public RMSString LoanCode { get; private set; }
        public RMSString Filler { get; private set; }
        #endregion

        public MaintenanceRecord(string Record) : base(Record)
        {
            this.FieldCode = new CitiMaintEnum<MaintenanceCode, MaintenanceCodeValue>(6) { DataString = Record.Substring(34) };
            this.IOFlag = new RMSEnum<IOFlag, IOFlagValue>(1) { DataString = Record.Substring(80) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(81) };
            this.RecovererID = new RMSString(8) { DataString = Record.Substring(85) };
            this.LoanCode = new RMSString(4) { DataString = Record.Substring(93) };
            this.Filler = new RMSString(3) { DataString = Record.Substring(97) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                base.ToString(),
                this.FieldCode,
                this.FieldValue,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.LoanCode,
                this.Filler);
        }

        public abstract Transaction GetTransaction();
    }

    public class MaintenanceRecord<T> : MaintenanceRecord where T : Transaction
    {
        public new T FieldValue { get; private set; }

        public MaintenanceRecord(string Record) : base(Record)
        {
            FieldValue = (T)typeof(T).GetConstructor(new[] { typeof(string) }).Invoke(new[] { Record.Substring(40) });
        }

        public override Transaction GetTransaction() { return this.FieldValue; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                this.TransactionDate,
                this.AccountNumber,
                this.TransactionCode,
                this.FieldCode,
                this.FieldValue,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.LoanCode,
                this.Filler);
        }
    }
}
