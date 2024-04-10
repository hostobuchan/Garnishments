namespace RecordTypes.Citi.Maintenance
{
    public class MaintenanceInfo : RMS.Maintenance.MaintenanceInfo
    {
        public MaintenanceInfo(string Record) : base(Record) { }
    }

    public class HeaderRecord : RMS.Maintenance.HeaderRecord
    {
        public HeaderRecord(string Record) : base(Record) { }
    }

    public class CommentRecord : RMS.Maintenance.CommentRecord
    {
        public CommentRecord(string Record) : base(Record) { }
    }

    public class FinanacialRecord : RMS.Maintenance.FinanacialRecord
    {
        public FinanacialRecord(string Record) : base(Record) { }
    }

    public abstract class MaintenanceRecord : RMS.Maintenance.MaintenanceRecord
    {
        public MaintenanceRecord(string Record) : base(Record) { }
    }

    public class MaintenanceRecord<T> : MaintenanceRecord where T : RMS.Maintenance.Transactions.Transaction
    {
        public new T FieldValue { get; private set; }

        public MaintenanceRecord(string Record)
            : base(Record)
        {
            FieldValue = (T)typeof(T).GetConstructor(new[] { typeof(string) }).Invoke(new[] { Record.Substring(40) });
        }

        public override RMS.Maintenance.Transactions.Transaction GetTransaction() { return this.FieldValue; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                this.TransactionDate,
                this.AccountNumber,
                this.TransactionCode,
                this.FieldCode,
                this.FieldValue,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.Filler1,
                this.ParentOrgCode,
                this.Filler2);
        }
    }
}
