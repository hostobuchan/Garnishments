using System;

namespace RecordTypes2.YGC
{
    public class SenderToReceiverRecordExtension : RecordTypes.YGC.Base.YGCSender2ReceiverRecord
    {
        #region Public Properties
        public new string RECORD { get { return base.RECORD; } }
        /// <summary>
        /// This is the internal file number of the sender (client) placing the account.
        /// </summary>
        public new string FILENO { get { return base.FILENO.Value; } set { base.FILENO.Value = value; } }
        /// <summary>
        /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
        /// </summary>
        public new string FORW_FILE { get { return base.FORW_FILE.Value; } set { base.FORW_FILE.Value = value; } }
        /// <summary>
        /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
        /// </summary>
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value.ToUpper(); } set { base.MASCO_FILE.Value = value.ToUpper(); } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } protected set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } protected set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the remaining record info
        /// </summary>
        public string RemainingRecordData { get; private set; }
        #endregion

        public SenderToReceiverRecordExtension(int RecordType) : base(RecordType) { }
        public SenderToReceiverRecordExtension(string YGCRecord) : base(YGCRecord)
        {
            this.RemainingRecordData = YGCRecord.Length > 67 ? YGCRecord.Substring(67) : "";
        }

        public override Type GetType()
        {
            return typeof(SenderToReceiverRecordExtension);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                base.ToString(),
                this.RemainingRecordData);
        }

        public override bool Equals(object obj)
        {
            if (obj is ReceiverToSenderRecordExtension)
            {
                ReceiverToSenderRecordExtension other = (ReceiverToSenderRecordExtension)obj;
                return string.Equals(this.RECORD, other.RECORD)
                    && string.Equals(this.FILENO, other.FILENO)
                    && string.Equals(this.FORW_FILE, other.FORW_FILE)
                    && string.Equals(this.MASCO_FILE, other.MASCO_FILE)
                    && string.Equals(this.FORW_ID, other.FORW_ID)
                    && string.Equals(this.FIRM_ID, other.FIRM_ID)
                    && string.Equals(this.RemainingRecordData, other.RemainingRecordData, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return
                this.RECORD.GetHashCode()
                + this.FILENO.GetHashCode()
                + this.FORW_FILE.GetHashCode()
                + this.MASCO_FILE.GetHashCode()
                + this.FORW_ID.GetHashCode()
                + this.FIRM_ID.GetHashCode()
                + this.RemainingRecordData.GetHashCode();
        }
    }
}
