using RecordTypes2.CM.Attributes;
using RecordTypes2.CM.DataTypes;
using System;

namespace RecordTypes2.CM
{
    /// <summary>
    /// CM EDI Record 110 Historical Financial Transactions
    /// <para>Historical Financial Transactions (FINAN)</para>
    /// <para>These Transactions are stored in the F1-F2 screen, but do not actually affect the accounting.</para>
    /// </summary>
    public class RecordType110 : Base.CMEDISenderReceiverRecord
    {
        #region Overridden Record Attribute
        [CMEDIField("110", 0, true)]
        protected override CMEDINumber _Record { get; set; }
        #endregion

        #region Protected Properties
        [CMEDIField("TRANS_DATE", 9, true)]
        protected CMEDIDate _TRANS_DATE { get; private set; }
        [CMEDIField("TRANS_TIME", 10)]
        protected CMEDITime _TRANS_TIME { get; private set; }
        [CMEDIField("CODE", 11, true)]
        protected CMEDINumber _CODE { get; private set; }
        [CMEDIField("REFNO", 12, true)]
        protected CMEDINumber _REFNO { get; private set; }
        [CMEDIField("COMMENT", 13)]
        protected CMEDIString _COMMENT { get; private set; }
        [CMEDIField("AMOUNT", 14, true)]
        protected CMEDIDecimal _AMOUNT { get; private set; }
        [CMEDIField("LINE1_1", 15)]
        protected CMEDIDecimal _LINE1_1 { get; private set; }
        [CMEDIField("COSTS", 16, true)]
        protected CMEDIDecimal _COSTS { get; private set; }
        [CMEDIField("STAT_FEE", 17, true)]
        protected CMEDIDecimal _STAT_FEE { get; private set; }
        [CMEDIField("NET", 18, true)]
        protected CMEDIDecimal _NET { get; private set; }
        [CMEDIField("BALANCE", 19, true)]
        protected CMEDIDecimal _BALANCE { get; private set; }
        [CMEDIField("RECEIVED", 20, true)]
        protected CMEDIDecimal _RECEIVED { get; private set; }
        [CMEDIField("PRINCIPAL", 21, true)]
        protected CMEDIDecimal _PRINCIPAL { get; private set; }
        [CMEDIField("INTEREST", 22, true)]
        protected CMEDIDecimal _INTEREST { get; private set; }
        [CMEDIField("LINE2_3", 23, true)]
        protected CMEDIDecimal _LINE2_3 { get; private set; }
        [CMEDIField("COSTS_RETURNED", 24, true)]
        protected CMEDIDecimal _COSTS_RETURNED { get; private set; }
        [CMEDIField("COSTS_EXPENDED", 25, true)]
        protected CMEDIDecimal _COSTS_EXPENDED { get; private set; }
        [CMEDIField("COSTS_RECEIVED", 26, true)]
        protected CMEDIDecimal _COSTS_RECEIVED { get; private set; }
        [CMEDIField("SUIT_FEE", 27, true)]
        protected CMEDIDecimal _SUIT_FEE { get; private set; }
        [CMEDIField("COMMISSION", 28, true)]
        protected CMEDIDecimal _COMMISSION { get; private set; }
        [CMEDIField("PRIN", 29, true)]
        protected CMEDIDecimal _PRIN { get; private set; }
        [CMEDIField("CONTRACT", 30, true)]
        protected CMEDIDecimal _CONTRACT { get; private set; }
        [CMEDIField("LEGAL", 31, true)]
        protected CMEDIDecimal _LEGAL { get; private set; }
        [CMEDIField("LINE3_4", 32)]
        protected CMEDIDecimal _LINE3_4 { get; private set; }
        [CMEDIField("LINE3_5", 33)]
        protected CMEDIDecimal _LINE3_5 { get; private set; }
        [CMEDIField("LINE3_6", 34)]
        protected CMEDIDecimal _LINE3_6 { get; private set; }
        [CMEDIField("LINE3_7", 35)]
        protected CMEDIDecimal _LINE3_7 { get; private set; }
        [CMEDIField("OTHER", 36)]
        protected CMEDIDecimal _OTHER { get; private set; }
        [CMEDIField("BPJ", 37)]
        protected CMEDIDictionary<Enums.Disposition> _BPJ { get; private set; }
        [CMEDIField("ADJUSTMENTS", 38)]
        protected CMEDIDecimal _ADJUSTMENTS { get; private set; }
        [CMEDIField("BILL", 39)]
        protected CMEDIBool _BILL { get; private set; }
        [CMEDIField("INT", 40)]
        protected CMEDIDecimal _INT { get; private set; }
        [CMEDIField("COST_BALANCE", 41)]
        protected CMEDIDecimal _COST_BALANCE { get; private set; }
        [CMEDIField("COLL_HOLD", 42)]
        protected CMEDIDecimal _COLL_HOLD { get; private set; }
        [CMEDIField("CO_FEES", 43)]
        protected CMEDIDecimal _CO_FEES { get; private set; }
        [CMEDIField("MERCHANDICE", 44)]
        protected CMEDIDecimal _MERCHANDISE { get; private set; }
        [CMEDIField("TAX_REBATE", 45)]
        protected CMEDIDecimal _TAX_REBATE { get; private set; }
        [CMEDIField("LINE4_1", 46)]
        protected CMEDIDecimal _LINE4_1 { get; private set; }
        [CMEDIField("LINE4_2", 47)]
        protected CMEDIDecimal _LINE4_2 { get; private set; }
        [CMEDIField("LINE4_3", 48)]
        protected CMEDIDecimal _LINE4_3 { get; private set; }
        [CMEDIField("LINE4_4", 49)]
        protected CMEDIDecimal _LINE4_4 { get; private set; }
        [CMEDIField("LINE4_10", 50)]
        protected CMEDIDecimal _LINE4_10 { get; private set; }
        #endregion

        #region Public Properties
        /// <summary>
        /// Transaction Date
        /// </summary>
        public DateTime? TRANS_DATE { get { return _TRANS_DATE.Value; } set { _TRANS_DATE.Value = value; } }
        /// <summary>
        /// Transaction Time (Not Yet Implemented)
        /// </summary>
        public DateTime? TRANS_TIME { get { return _TRANS_TIME.Value; } set { _TRANS_TIME.Value = value; } }
        /// <summary>
        /// TRANSACTION CODE
        /// </summary>
        public int? CODE { get { return _CODE.Value; } set { _CODE.Value = value; } }
        /// <summary>
        /// Check/TA #
        /// </summary>
        public int? REFNO { get { return _REFNO.Value; } set { _REFNO.Value = value; } }
        /// <summary>
        /// TRANSACTION COMMENT
        /// </summary>
        public string COMMENT { get { return _COMMENT.Value; } set { _COMMENT.Value = value; } }
        /// <summary>
        /// AMOUNT OF TRANSACTION
        /// </summary>
        public decimal? AMOUNT { get { return _AMOUNT.Value; } set { _AMOUNT.Value = value; } }
        /// <summary>
        /// ** Reserved for Future Use **
        /// </summary>
        public decimal? LINE1_1 { get { return _LINE1_1.Value; } set { _LINE1_1.Value = value; } }
        /// <summary>
        /// Costs Recovered
        /// </summary>
        public decimal? COSTS { get { return _COSTS.Value; } set { _COSTS.Value = value; } }
        /// <summary>
        /// Stat Fee Recovered
        /// </summary>
        public decimal? STAT_FEE { get { return _STAT_FEE.Value; } set { _STAT_FEE.Value = value; } }
        /// <summary>
        /// Net to Client (No Costs)
        /// </summary>
        public decimal? NET { get { return _NET.Value; } set { _NET.Value = value; } }
        /// <summary>
        /// Debtor Balance
        /// </summary>
        public decimal? BALANCE { get { return _BALANCE.Value; } set { _BALANCE.Value = value; } }
        /// <summary>
        /// Amount Received
        /// </summary>
        public decimal? RECEIVED { get { return _RECEIVED.Value; } set { _RECEIVED.Value = value; } }
        /// <summary>
        /// Principal Collected (SUM of LINE3(1-8))
        /// </summary>
        public decimal? PRINCIPAL { get { return _PRINCIPAL.Value; } set { _PRINCIPAL.Value = value; } }
        /// <summary>
        /// Interest Collected
        /// </summary>
        public decimal? INTEREST { get { return _INTEREST.Value; } set { _INTEREST.Value = value; } }
        /// <summary>
        /// Commulative Collected (Not Yet Implemented)
        /// </summary>
        public decimal? LINE2_3 { get { return _LINE2_3.Value; } set { _LINE2_3.Value = value; } }
        /// <summary>
        /// Costs Returned
        /// </summary>
        public decimal? COSTS_RETURNED { get { return _COSTS_RETURNED.Value; } set { _COSTS_RETURNED.Value = value; } }
        /// <summary>
        /// Costs Expended
        /// </summary>
        public decimal? COSTS_EXPENDED { get { return _COSTS_EXPENDED.Value; } set { _COSTS_EXPENDED.Value = value; } }
        /// <summary>
        /// Costs Received from Client
        /// </summary>
        public decimal? COSTS_RECEIVED { get { return _COSTS_RECEIVED.Value; } set { _COSTS_RECEIVED.Value = value; } }
        /// <summary>
        /// Suit Fees Received
        /// </summary>
        public decimal? SUIT_FEE { get { return _SUIT_FEE.Value; } set { _SUIT_FEE.Value = value; } }
        /// <summary>
        /// Commissions Received
        /// </summary>
        public decimal? COMMISSION { get { return _COMMISSION.Value; } set { _COMMISSION.Value = value; } }
        /// <summary>
        /// Amount of "Principal" applied to Principal
        /// </summary>
        public decimal? PRIN { get { return _PRIN.Value; } set { _PRIN.Value = value; } }
        /// <summary>
        /// Amount of "Principal" applied to Contract Fees
        /// </summary>
        public decimal? CONTRACT { get { return _CONTRACT.Value; } set { _CONTRACT.Value = value; } }
        /// <summary>
        /// Amount of "Principal" applied to Legal Fees
        /// </summary>
        public decimal? LEGAL { get { return _LEGAL.Value; } set { _LEGAL.Value = value; } }
        /// <summary>
        /// ** Reserved for Future Use **
        /// </summary>
        public decimal? LINE3_4 { get { return _LINE3_4.Value; } set { _LINE3_4.Value = value; } }
        /// <summary>
        /// ** Reserved for Future Use **
        /// </summary>
        public decimal? LINE3_5 { get { return _LINE3_5.Value; } set { _LINE3_5.Value = value; } }
        /// <summary>
        /// ** Reserved for Future Use **
        /// </summary>
        public decimal? LINE3_6 { get { return _LINE3_6.Value; } set { _LINE3_6.Value = value; } }
        /// <summary>
        /// ** Reserved for Future Use **
        /// </summary>
        public decimal? LINE3_7 { get { return _LINE3_7.Value; } set { _LINE3_7.Value = value; } }
        /// <summary>
        /// Amount of "Principal" applied to Other
        /// </summary>
        public decimal? OTHER { get { return _OTHER.Value; } set { _OTHER.Value = value; } }
        /// <summary>
        /// Account Disposition
        /// <para>"B"efore "P"ost Suit after "J"UDG</para>
        /// </summary>
        public Enums.Disposition BPJ { get { return _BPJ.Value; } set { _BPJ.Value = value; } }
        /// <summary>
        /// Amount Advanced out of Pocket
        /// </summary>
        public decimal? ADJUSTMENTS { get { return _ADJUSTMENTS.Value; } set { _ADJUSTMENTS.Value = value; } }
        /// <summary>
        /// Bill debtor for this Transaction
        /// </summary>
        public bool? BILL { get { return _BILL.Value; } set { _BILL.Value = value; } }
        /// <summary>
        /// Total Accrued Interest
        /// </summary>
        public decimal? INT { get { return _INT.Value; } set { _INT.Value = value; } }
        /// <summary>
        /// Cost Balance as of This TA
        /// </summary>
        public decimal? COST_BALANCE { get { return _COST_BALANCE.Value; } set { _COST_BALANCE.Value = value; } }
        /// <summary>
        /// Amount Applied to Coll & Hold
        /// </summary>
        public decimal? COLL_HOLD { get { return _COLL_HOLD.Value; } set { _COLL_HOLD.Value = value; } }
        /// <summary>
        /// Amount Applied to Co-Co Fees
        /// </summary>
        public decimal? CO_FEES { get { return _CO_FEES.Value; } set { _CO_FEES.Value = value; } }
        /// <summary>
        /// Amount Applied to Merchandise
        /// </summary>
        public decimal? MERCHANDISE { get { return _MERCHANDISE.Value; } set { _MERCHANDISE.Value = value; } }
        /// <summary>
        /// Amount Applied to Tax Rebate
        /// </summary>
        public decimal? TAX_REBATE { get { return _TAX_REBATE.Value; } set { _TAX_REBATE.Value = value; } }
        /// <summary>
        /// Existing Costs Recovered
        /// </summary>
        public decimal? LINE4_1 { get { return _LINE4_1.Value; } set { _LINE4_1.Value = value; } }
        /// <summary>
        /// Non-Recoverable Costs Recovered
        /// </summary>
        public decimal? LINE4_2 { get { return _LINE4_2.Value; } set { _LINE4_2.Value = value; } }
        /// <summary>
        /// Pending Costs Recovered 
        /// </summary>
        public decimal? LINE4_3 { get { return _LINE4_3.Value; } set { _LINE4_3.Value = value; } }
        /// <summary>
        /// Firm Costs Recovered
        /// </summary>
        public decimal? LINE4_4 { get { return _LINE4_4.Value; } set { _LINE4_4.Value = value; } }
        /// <summary>
        /// Other Costs Recovered
        /// </summary>
        public decimal? LINE4_10 { get { return _LINE4_10.Value; } set { _LINE4_10.Value = value; } }
        #endregion

        public RecordType110(string data) : base(110, data)
        {
            Initialize();
        }
        public RecordType110() : base(110, 51)
        {
            Initialize();
        }

        private void Initialize()
        {
            _TRANS_DATE = new CMEDIDate(this.LineItems[9]);
            _TRANS_TIME = new CMEDITime(this.LineItems[10]);
            _CODE = new CMEDINumber(this.LineItems[11]);
            _REFNO = new CMEDINumber(this.LineItems[12]);
            _COMMENT = new CMEDIString(this.LineItems[13]);
            _AMOUNT = new CMEDIDecimal(this.LineItems[14]);
            _LINE1_1 = new CMEDIDecimal(this.LineItems[15]);
            _COSTS = new CMEDIDecimal(this.LineItems[16]);
            _STAT_FEE = new CMEDIDecimal(this.LineItems[17]);
            _NET = new CMEDIDecimal(this.LineItems[18]);
            _BALANCE = new CMEDIDecimal(this.LineItems[19]);
            _RECEIVED = new CMEDIDecimal(this.LineItems[20]);
            _PRINCIPAL = new CMEDIDecimal(this.LineItems[21]);
            _INTEREST = new CMEDIDecimal(this.LineItems[22]);
            _LINE2_3 = new CMEDIDecimal(this.LineItems[23]);
            _COSTS_RETURNED = new CMEDIDecimal(this.LineItems[24]);
            _COSTS_EXPENDED = new CMEDIDecimal(this.LineItems[25]);
            _COSTS_RECEIVED = new CMEDIDecimal(this.LineItems[26]);
            _SUIT_FEE = new CMEDIDecimal(this.LineItems[27]);
            _COMMISSION = new CMEDIDecimal(this.LineItems[28]);
            _PRIN = new CMEDIDecimal(this.LineItems[29]);
            _CONTRACT = new CMEDIDecimal(this.LineItems[30]);
            _LEGAL = new CMEDIDecimal(this.LineItems[31]);
            _LINE3_4 = new CMEDIDecimal(this.LineItems[32]);
            _LINE3_5 = new CMEDIDecimal(this.LineItems[33]);
            _LINE3_6 = new CMEDIDecimal(this.LineItems[34]);
            _LINE3_7 = new CMEDIDecimal(this.LineItems[35]);
            _OTHER = new CMEDIDecimal(this.LineItems[36]);
            _BPJ = new CMEDIDictionary<Enums.Disposition>(this.LineItems[37], Dictionaries.Disposition);
            _ADJUSTMENTS = new CMEDIDecimal(this.LineItems[38]);
            _BILL = new CMEDIBool(this.LineItems[39]);
            _INT = new CMEDIDecimal(this.LineItems[40]);
            _COST_BALANCE = new CMEDIDecimal(this.LineItems[41]);
            _COLL_HOLD = new CMEDIDecimal(this.LineItems[42]);
            _CO_FEES = new CMEDIDecimal(this.LineItems[43]);
            _MERCHANDISE = new CMEDIDecimal(this.LineItems[44]);
            _TAX_REBATE = new CMEDIDecimal(this.LineItems[45]);
            _LINE4_1 = new CMEDIDecimal(this.LineItems[46]);
            _LINE4_2 = new CMEDIDecimal(this.LineItems[47]);
            _LINE4_3 = new CMEDIDecimal(this.LineItems[48]);
            _LINE4_4 = new CMEDIDecimal(this.LineItems[49]);
            _LINE4_10 = new CMEDIDecimal(this.LineItems[50]);
        }
    }
}
