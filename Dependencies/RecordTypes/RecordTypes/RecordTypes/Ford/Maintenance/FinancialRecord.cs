using RecordTypes.RMS.DataTypes;
using RecordTypes.RMS.Maintenance;

namespace RecordTypes.Ford.Maintenance
{
    public class FinanacialRecord : MaintenanceInfo
    {
        #region Public Properties
        public RMSDict<Enums.FinancialTransactionCode> FinancialTransactionCode { get; private set; }
        public RMSZoneDecimal TransactionAmount { get; private set; }
        public RMSBool InterestFlag { get; private set; }
        public RMSBool SelfDirectedFlag { get; private set; }
        public RMSString TransactionDescription { get; private set; }
        public RMSZoneDecimal NetPaymentAmount { get; private set; }
        public RMSZoneDecimal CommissionPercent { get; private set; }
        public RMSEnum<IOFlag, IOFlagValue> IOFlag { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString RecovererID { get; private set; }
        public RMSString LoanCode { get; private set; }
        public RMSString Filler { get; private set; }
        #endregion

        public FinanacialRecord(string Record) : base(Record)
        {
            this.FinancialTransactionCode = new RMS.DataTypes.RMSDict<Enums.FinancialTransactionCode>(2, Dictionaries.FinancialTransactionCodes) { DataString = Record.Substring(32) };
            this.TransactionAmount = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(34) };
            this.InterestFlag = new RMSBool(1, "Y") { DataString = Record.Substring(44) };
            this.SelfDirectedFlag = new RMSBool(1, "Y") { DataString = Record.Substring(45) };
            this.TransactionDescription = new RMSString(20) { DataString = Record.Substring(46) };
            this.NetPaymentAmount = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(66) };
            this.CommissionPercent = new RMSZoneDecimal(4, 2) { DataString = Record.Substring(76) };
            this.IOFlag = new RMSEnum<IOFlag, IOFlagValue>(1) { DataString = Record.Substring(80) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(81) };
            this.RecovererID = new RMSString(8) { DataString = Record.Substring(85) };
            this.LoanCode = new RMSString(4) { DataString = Record.Substring(93) };
            this.Filler = new RMSString(3) { DataString = Record.Substring(97) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
                base.ToString(),
                this.TransactionAmount,
                this.InterestFlag,
                this.SelfDirectedFlag,
                this.TransactionDescription,
                this.NetPaymentAmount,
                this.CommissionPercent,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.LoanCode,
                this.Filler);
        }
    }
}
