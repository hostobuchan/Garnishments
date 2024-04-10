using RecordTypes2.CM.Attributes;
using RecordTypes2.CM.DataTypes;

namespace RecordTypes2.CM.Base
{
    public abstract class CMEDISenderReceiverRecord : CMEDIRecordBase
    {
        #region Protected Properties
        [CMEDIField("FORW_REFNO", 4, true)]
        protected CMEDIString _FORW_REFNO { get; private set; }
        [CMEDIField("FORW_FILENO", 5, true)]
        protected CMEDIString _FORW_FILENO { get; private set; }
        [CMEDIField("FIRM_FILENO", 6, true)]
        protected CMEDIString _FIRM_FILENO { get; private set; }
        [CMEDIField("SENDER_ID", 7, true)]
        protected CMEDIString _SENDER_ID { get; private set; }
        [CMEDIField("RECEIVER_ID", 8, true)]
        protected CMEDIString _RECEIVER_ID { get; private set; }
        #endregion

        #region Public Properties
        /// <summary>
        /// Forwarder or Sender's internal File #
        /// </summary>
        public string FORW_REFNO { get { return this._FORW_REFNO.Value; } set { this._FORW_REFNO.Value = value; } }
        /// <summary>
        /// Forwarder's File # (Credit Card #)
        /// </summary>
        public string FORW_FILENO { get { return this._FORW_FILENO.Value; } set { this._FORW_FILENO.Value = value; } }
        /// <summary>
        /// Receiver's File # (Usually Left Blank)
        /// </summary>
        public string FIRM_FILENO { get { return this._FIRM_FILENO.Value; } set { this._FIRM_FILENO.Value = value; } }
        /// <summary>
        /// Sender ID Code
        /// </summary>
        public string SENDER_ID { get { return this._SENDER_ID.Value; } set { this._SENDER_ID.Value = value; } }
        /// <summary>
        /// Receiver ID Code
        /// </summary>
        public string RECEIVER_ID { get { return this._RECEIVER_ID.Value; } set { this._RECEIVER_ID.Value = value; } }
        #endregion

        public CMEDISenderReceiverRecord(int recordType, string data) : base(recordType, data)
        {
            Initialize();
        }
        public CMEDISenderReceiverRecord(int recordType, int numFields) : base(recordType, numFields)
        {
            Initialize();
        }

        private void Initialize()
        {
            _FORW_REFNO = new CMEDIString(this.LineItems[4]);
            _FORW_FILENO = new CMEDIString(this.LineItems[5]);
            _FIRM_FILENO = new CMEDIString(this.LineItems[6]);
            _SENDER_ID = new CMEDIString(this.LineItems[7]);
            _RECEIVER_ID = new CMEDIString(this.LineItems[8]);
        }
    }
}
