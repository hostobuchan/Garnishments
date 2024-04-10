using RecordTypes2.CM.Attributes;
using RecordTypes2.CM.DataTypes;

namespace RecordTypes2.CM
{
    /// <summary>
    /// Update Account Card  Values (Excluding Interest)  
    /// <para>This record is used to Update the Account Card Values</para>
    /// </summary>
    public class RecordType172 : Base.CMEDISenderReceiverRecord
    {
        #region Overridden Record Attribute
        [CMEDIField("172", 0, true)]
        protected override CMEDINumber _Record { get; set; }
        #endregion

        #region Protected Properties
        [CMEDIField("ORIG_CLAIM", 9)]
        protected CMEDIDecimal _ORIG_CLAIM { get; private set; }
        [CMEDIField("SUIT_AMT", 10)]
        protected CMEDIDecimal _SUIT_AMT { get; private set; }
        [CMEDIField("STAT_FEE", 11)]
        protected CMEDIDecimal _STAT_FEE { get; private set; }
        [CMEDIField("JMT_AMT", 12)]
        protected CMEDIDecimal _JMT_AMT { get; private set; }
        [CMEDIField("BALANCE", 13)]
        protected CMEDIDecimal _BALANCE { get; private set; }
        [CMEDIField("COST_BAL", 14)]
        protected CMEDIDecimal _COST_BAL { get; private set; }
        [CMEDIField("COLL_HOLD", 15)]
        protected CMEDIDecimal _COLL_HOLD { get; private set; }
        [CMEDIField("PRIN_COLL", 16)]
        protected CMEDIDecimal _PRIN_COLL { get; private set; }
        [CMEDIField("INT_COLL", 17)]
        protected CMEDIDecimal _INT_COLL { get; private set; }
        [CMEDIField("MERCH_BEF", 18)]
        protected CMEDIDecimal _MERCH_BEF { get; private set; }
        [CMEDIField("MERCH_POST", 19)]
        protected CMEDIDecimal _MERCH_POST { get; private set; }
        [CMEDIField("CASH_BEF", 20)]
        protected CMEDIDecimal _CASH_BEF { get; private set; }
        [CMEDIField("CASH_POST", 21)]
        protected CMEDIDecimal _CASH_POST { get; private set; }
        [CMEDIField("COST_RECEIVED", 22)]
        protected CMEDIDecimal _COST_RECEIVED { get; private set; }
        [CMEDIField("COST_RET", 23)]
        protected CMEDIDecimal _COST_RET { get; private set; }
        [CMEDIField("COST_EXP", 24)]
        protected CMEDIDecimal _COST_EXP { get; private set; }
        [CMEDIField("COST_RECOVERED", 25)]
        protected CMEDIDecimal _COST_RECOVERED { get; private set; }
        [CMEDIField("COMM_EARN", 26)]
        protected CMEDIDecimal _COMM_EARN { get; private set; }
        [CMEDIField("SFEE_EARN", 27)]
        protected CMEDIDecimal _SFEE_EARN { get; private set; }
        [CMEDIField("STAT_EARN", 28)]
        protected CMEDIDecimal _STAT_EARN { get; private set; }
        [CMEDIField("COST_POST_JUDG", 29)]
        protected CMEDIDecimal _COST_POST_JUDG { get; private set; }
        [CMEDIField("AFFIDAVIT", 30)]
        protected CMEDIDecimal _AFFIDAVIT { get; private set; }
        [CMEDIField("PAID_POST_JUDG", 31)]
        protected CMEDIDecimal _PAID_POST_JUDG { get; private set; }
        [CMEDIField("NON_RCVRD_COST", 32)]
        protected CMEDIDecimal _NON_RCVRD_COST { get; private set; }
        [CMEDIField("TOT_EXP_COST", 33)]
        protected CMEDIDecimal _TOT_EXP_COST { get; private set; }
        [CMEDIField("DP_PRE_SUIT_NF", 34)]
        protected CMEDIDecimal _DP_PRE_SUIT_NF { get; private set; }
        [CMEDIField("DP_POST_SUIT_NF", 35)]
        protected CMEDIDecimal _DP_POST_SUIT_NF { get; private set; }
        [CMEDIField("DP_POST_JUDG_NF", 36)]
        protected CMEDIDecimal _DP_POST_JUDG_NF { get; private set; }
        [CMEDIField("MERCH_POST_JUDG", 37)]
        protected CMEDIDecimal _MERCH_POST_JUDG { get; private set; }
        [CMEDIField("EXP_COST", 38)]
        protected CMEDIDecimal _EXP_COST { get; private set; }
        [CMEDIField("DP_PRE_SUIT", 39)]
        protected CMEDIDecimal _DP_PRE_SUIT { get; private set; }
        [CMEDIField("DP_POST_SUIT", 40)]
        protected CMEDIDecimal _DP_POST_SUIT { get; private set; }
        [CMEDIField("DP_POST_JUDG", 41)]
        protected CMEDIDecimal _DP_POST_JUDG { get; private set; }
        [CMEDIField("ACCR_INT_BEF", 42)]
        protected CMEDIDecimal _ACCR_INT_BEF { get; private set; }
        [CMEDIField("TAX_REBATE", 43)]
        protected CMEDIDecimal _TAX_REBATE { get; private set; }
        [CMEDIField("AGENT_BEFSCOLL", 44)]
        protected CMEDIDecimal _AGENT_BEFSCOLL { get; private set; }
        [CMEDIField("AGENT_PSCOLL", 45)]
        protected CMEDIDecimal _AGENT_PSCOLL { get; private set; }
        [CMEDIField("AGENT_PJCOLL", 46)]
        protected CMEDIDecimal _AGENT_PJCOLL { get; private set; }
        [CMEDIField("AGENT_COST", 47)]
        protected CMEDIDecimal _AGENT_COST { get; private set; }
        [CMEDIField("SALES_TAX", 48)]
        protected CMEDIDecimal _SALES_TAX { get; private set; }
        [CMEDIField("C_ADJUST", 49)]
        protected CMEDIDecimal _C_ADJUST { get; private set; }
        [CMEDIField("COMPR_AMT", 50)]
        protected CMEDIDecimal _COMPR_AMT { get; private set; }
        [CMEDIField("OVERPAY_AMT", 51)]
        protected CMEDIDecimal _OVERPAY_AMT { get; private set; }
        [CMEDIField("COCO_COMM_EARN", 52)]
        protected CMEDIDecimal _COCO_COMM_EARN { get; private set; }
        [CMEDIField("COCO_SFEE_EARN", 53)]
        protected CMEDIDecimal _COCO_SFEE_EARN { get; private set; }
        [CMEDIField("PRINCIPAL_COLL", 54)]
        protected CMEDIDecimal _PRINCIPAL_COLL { get; private set; }
        [CMEDIField("CONTRACT_COLL", 55)]
        protected CMEDIDecimal _CONTRACT_COLL { get; private set; }
        [CMEDIField("FEES_COLL", 56)]
        protected CMEDIDecimal _FEES_COLL { get; private set; }
        [CMEDIField("CHARGES_COLL", 57)]
        protected CMEDIDecimal _CHARGES_COLL { get; private set; }
        [CMEDIField("OTHER_COLL", 58)]
        protected CMEDIDecimal _OTHER_COLL { get; private set; }
        [CMEDIField("OTHER_FEES_COLL", 59)]
        protected CMEDIDecimal _OTHER_FEES_COLL { get; private set; }
        [CMEDIField("PEND_COLL", 60)]
        protected CMEDIDecimal _PEND_COLL { get; private set; }
        [CMEDIField("NR_COST_COLL", 61)]
        protected CMEDIDecimal _NR_COST_COLL { get; private set; }
        [CMEDIField("REC_COSTS_COLL", 62)]
        protected CMEDIDecimal _REC_COSTS_COLL { get; private set; }
        [CMEDIField("FIRM_COSTS_COLL", 63)]
        protected CMEDIDecimal _FIRM_COSTS_COLL { get; private set; }
        [CMEDIField("OTHER_COST_COLL", 64)]
        protected CMEDIDecimal _OTHER_COST_COLL { get; private set; }
        [CMEDIField("FREC37", 65)]
        protected CMEDIDecimal _FREC37 { get; private set; }
        [CMEDIField("FREC38", 66)]
        protected CMEDIDecimal _FREC38 { get; private set; }
        [CMEDIField("FREC39", 67)]
        protected CMEDIDecimal _FREC39 { get; private set; }
        [CMEDIField("FREC40", 68)]
        protected CMEDIDecimal _FREC40 { get; private set; }
        [CMEDIField("EXP_M_COST", 69)]
        protected CMEDIDecimal _EXP_M_COST { get; private set; }
        [CMEDIField("EXP_D_COST", 70)]
        protected CMEDIDecimal _EXP_D_COST { get; private set; }
        [CMEDIField("EXP_P_COST", 71)]
        protected CMEDIDecimal _EXP_P_COST { get; private set; }
        [CMEDIField("EXP_R_COST", 72)]
        protected CMEDIDecimal _EXP_R_COST { get; private set; }
        [CMEDIField("WO_REC_COSTS", 73)]
        protected CMEDIDecimal _WO_REC_COSTS { get; private set; }
        [CMEDIField("WO_NR_COSTS", 74)]
        protected CMEDIDecimal _WO_NR_COSTS { get; private set; }
        [CMEDIField("WO_FEES", 75)]
        protected CMEDIDecimal _WO_FEES { get; private set; }
        [CMEDIField("I_1100_IR_J", 76)]
        protected CMEDIDecimal _I_1100_IR_J { get; private set; }
        [CMEDIField("I_3100_IR_J", 77)]
        protected CMEDIDecimal _I_3100_IR_J { get; private set; }
        [CMEDIField("I_4100_IR_J", 78)]
        protected CMEDIDecimal _I_4100_IR_J { get; private set; }
        [CMEDIField("I_5100_IR_J", 79)]
        protected CMEDIDecimal _I_5100_IR_J { get; private set; }
        [CMEDIField("I_6100_IR_J", 80)]
        protected CMEDIDecimal _I_6100_IR_J { get; private set; }
        [CMEDIField("I_9100_IR_J", 81)]
        protected CMEDIDecimal _I_9100_IR_J { get; private set; }
        #endregion

        #region Public Properties
        /// <Summary>
        /// Original Claim Amount               
        /// </Summary>
        public decimal? ORIG_CLAIM { get { return _ORIG_CLAIM.Value; } set { _ORIG_CLAIM.Value = value; } }
        /// <Summary>
        /// Suit Amount                         
        /// </Summary>
        public decimal? SUIT_AMT { get { return _SUIT_AMT.Value; } set { _SUIT_AMT.Value = value; } }
        /// <Summary>
        /// Statutory Attorney Fees             
        /// </Summary>
        public decimal? STAT_FEE { get { return _STAT_FEE.Value; } set { _STAT_FEE.Value = value; } }
        /// <Summary>
        /// Judgment Amount                     
        /// </Summary>
        public decimal? JMT_AMT { get { return _JMT_AMT.Value; } set { _JMT_AMT.Value = value; } }
        /// <Summary>
        /// Debtor Balance (Princ/Costs/Stat)   
        /// </Summary>
        public decimal? BALANCE { get { return _BALANCE.Value; } set { _BALANCE.Value = value; } }
        /// <Summary>
        /// Costs Balance                       
        /// </Summary>
        public decimal? COST_BAL { get { return _COST_BAL.Value; } set { _COST_BAL.Value = value; } }
        /// <Summary>
        /// Collect & Hold                      
        /// </Summary>
        public decimal? COLL_HOLD { get { return _COLL_HOLD.Value; } set { _COLL_HOLD.Value = value; } }
        /// <Summary>
        /// Principal Collected -Includes Other 
        /// </Summary>
        public decimal? PRIN_COLL { get { return _PRIN_COLL.Value; } set { _PRIN_COLL.Value = value; } }
        /// <Summary>
        /// Interest Collected                  
        /// </Summary>
        public decimal? INT_COLL { get { return _INT_COLL.Value; } set { _INT_COLL.Value = value; } }
        /// <Summary>
        /// Merchandise Returned Before To Suit 
        /// </Summary>
        public decimal? MERCH_BEF { get { return _MERCH_BEF.Value; } set { _MERCH_BEF.Value = value; } }
        /// <Summary>
        /// Merchandise Returned Post Suit      
        /// </Summary>
        public decimal? MERCH_POST { get { return _MERCH_POST.Value; } set { _MERCH_POST.Value = value; } }
        /// <Summary>
        /// Cash Paid Prior To Suit             
        /// </Summary>
        public decimal? CASH_BEF { get { return _CASH_BEF.Value; } set { _CASH_BEF.Value = value; } }
        /// <Summary>
        /// Cash Paid Post Suit                 
        /// </Summary>
        public decimal? CASH_POST { get { return _CASH_POST.Value; } set { _CASH_POST.Value = value; } }
        /// <Summary>
        /// Costs Received From Forwarder       
        /// </Summary>
        public decimal? COST_RECEIVED { get { return _COST_RECEIVED.Value; } set { _COST_RECEIVED.Value = value; } }
        /// <Summary>
        /// Costs Returned To Forwarder         
        /// </Summary>
        public decimal? COST_RET { get { return _COST_RET.Value; } set { _COST_RET.Value = value; } }
        /// <Summary>
        /// Costs Expended (Billable To Debtor) 
        /// </Summary>
        public decimal? COST_EXP { get { return _COST_EXP.Value; } set { _COST_EXP.Value = value; } }
        /// <Summary>
        /// Costs Recovered From Debtor         
        /// </Summary>
        public decimal? COST_RECOVERED { get { return _COST_RECOVERED.Value; } set { _COST_RECOVERED.Value = value; } }
        /// <Summary>
        /// Commissions Received                
        /// </Summary>
        public decimal? COMM_EARN { get { return _COMM_EARN.Value; } set { _COMM_EARN.Value = value; } }
        /// <Summary>
        /// Suit Fees Received                  
        /// </Summary>
        public decimal? SFEE_EARN { get { return _SFEE_EARN.Value; } set { _SFEE_EARN.Value = value; } }
        /// <Summary>
        /// Statutory Atty Fees Received        
        /// </Summary>
        public decimal? STAT_EARN { get { return _STAT_EARN.Value; } set { _STAT_EARN.Value = value; } }
        /// <Summary>
        /// Costs Post Judgment                 
        /// </Summary>
        public decimal? COST_POST_JUDG { get { return _COST_POST_JUDG.Value; } set { _COST_POST_JUDG.Value = value; } }
        /// <Summary>
        /// Affidavit Amount                    
        /// </Summary>
        public decimal? AFFIDAVIT { get { return _AFFIDAVIT.Value; } set { _AFFIDAVIT.Value = value; } }
        /// <Summary>
        /// Paid Post Judgment                  
        /// </Summary>
        public decimal? PAID_POST_JUDG { get { return _PAID_POST_JUDG.Value; } set { _PAID_POST_JUDG.Value = value; } }
        /// <Summary>
        /// Non Recoverable Costs Expended      
        /// </Summary>
        public decimal? NON_RCVRD_COST { get { return _NON_RCVRD_COST.Value; } set { _NON_RCVRD_COST.Value = value; } }
        /// <Summary>
        /// Total Costs Expended                
        /// </Summary>
        public decimal? TOT_EXP_COST { get { return _TOT_EXP_COST.Value; } set { _TOT_EXP_COST.Value = value; } }
        /// <Summary>
        /// Dp Pre Suit No Fee Due              
        /// </Summary>
        public decimal? DP_PRE_SUIT_NF { get { return _DP_PRE_SUIT_NF.Value; } set { _DP_PRE_SUIT_NF.Value = value; } }
        /// <Summary>
        /// Dp Post Suit No Fee Due             
        /// </Summary>
        public decimal? DP_POST_SUIT_NF { get { return _DP_POST_SUIT_NF.Value; } set { _DP_POST_SUIT_NF.Value = value; } }
        /// <Summary>
        /// Dp Post Judgment No Fee Due         
        /// </Summary>
        public decimal? DP_POST_JUDG_NF { get { return _DP_POST_JUDG_NF.Value; } set { _DP_POST_JUDG_NF.Value = value; } }
        /// <Summary>
        /// Post Judgment Merchandise Returned  
        /// </Summary>
        public decimal? MERCH_POST_JUDG { get { return _MERCH_POST_JUDG.Value; } set { _MERCH_POST_JUDG.Value = value; } }
        /// <Summary>
        /// Costs Expended (Firm Costs)         
        /// </Summary>
        public decimal? EXP_COST { get { return _EXP_COST.Value; } set { _EXP_COST.Value = value; } }
        /// <Summary>
        /// Pre Suit Direct Payment             
        /// </Summary>
        public decimal? DP_PRE_SUIT { get { return _DP_PRE_SUIT.Value; } set { _DP_PRE_SUIT.Value = value; } }
        /// <Summary>
        /// Post Suit Direct Payment            
        /// </Summary>
        public decimal? DP_POST_SUIT { get { return _DP_POST_SUIT.Value; } set { _DP_POST_SUIT.Value = value; } }
        /// <Summary>
        /// Post Judgment Direct Payment        
        /// </Summary>
        public decimal? DP_POST_JUDG { get { return _DP_POST_JUDG.Value; } set { _DP_POST_JUDG.Value = value; } }
        /// <Summary>
        /// Pre Judgment Accrued Interest       
        /// </Summary>
        public decimal? ACCR_INT_BEF { get { return _ACCR_INT_BEF.Value; } set { _ACCR_INT_BEF.Value = value; } }
        /// <Summary>
        /// Costs Recovered Via Tax Rebate      
        /// </Summary>
        public decimal? TAX_REBATE { get { return _TAX_REBATE.Value; } set { _TAX_REBATE.Value = value; } }
        /// <Summary>
        /// Pre Suit Coll By Agent              
        /// </Summary>
        public decimal? AGENT_BEFSCOLL { get { return _AGENT_BEFSCOLL.Value; } set { _AGENT_BEFSCOLL.Value = value; } }
        /// <Summary>
        /// Post Suit Coll By Agent             
        /// </Summary>
        public decimal? AGENT_PSCOLL { get { return _AGENT_PSCOLL.Value; } set { _AGENT_PSCOLL.Value = value; } }
        /// <Summary>
        /// Post Judgment Coll By Agent         
        /// </Summary>
        public decimal? AGENT_PJCOLL { get { return _AGENT_PJCOLL.Value; } set { _AGENT_PJCOLL.Value = value; } }
        /// <Summary>
        /// Costs Sent To Agent                 
        /// </Summary>
        public decimal? AGENT_COST { get { return _AGENT_COST.Value; } set { _AGENT_COST.Value = value; } }
        /// <Summary>
        /// Sales Tax Received                  
        /// </Summary>
        public decimal? SALES_TAX { get { return _SALES_TAX.Value; } set { _SALES_TAX.Value = value; } }
        /// <Summary>
        /// Claim Adjustment 
        /// </Summary>
        public decimal? C_ADJUST { get { return _C_ADJUST.Value; } set { _C_ADJUST.Value = value; } }
        /// <Summary>
        /// Compromise Amount
        /// </Summary>
        public decimal? COMPR_AMT { get { return _COMPR_AMT.Value; } set { _COMPR_AMT.Value = value; } }
        /// <Summary>
        /// Overpay Amount 
        /// </Summary>
        public decimal? OVERPAY_AMT { get { return _OVERPAY_AMT.Value; } set { _OVERPAY_AMT.Value = value; } }
        /// <Summary>
        /// Co-council Commissions Earned       
        /// </Summary>
        public decimal? COCO_COMM_EARN { get { return _COCO_COMM_EARN.Value; } set { _COCO_COMM_EARN.Value = value; } }
        /// <Summary>
        /// Co-council Suit Fees Earned         
        /// </Summary>
        public decimal? COCO_SFEE_EARN { get { return _COCO_SFEE_EARN.Value; } set { _COCO_SFEE_EARN.Value = value; } }
        /// <Summary>
        /// Principal Or Placement Amount Coll  
        /// </Summary>
        public decimal? PRINCIPAL_COLL { get { return _PRINCIPAL_COLL.Value; } set { _PRINCIPAL_COLL.Value = value; } }
        /// <Summary>
        /// Contract Fees Recovered & Remitted  
        /// </Summary>
        public decimal? CONTRACT_COLL { get { return _CONTRACT_COLL.Value; } set { _CONTRACT_COLL.Value = value; } }
        /// <Summary>
        /// Attorney Fees Collected [stat Fee]  
        /// </Summary>
        public decimal? FEES_COLL { get { return _FEES_COLL.Value; } set { _FEES_COLL.Value = value; } }
        /// <Summary>
        /// Collection Charges Collected        
        /// </Summary>
        public decimal? CHARGES_COLL { get { return _CHARGES_COLL.Value; } set { _CHARGES_COLL.Value = value; } }
        /// <Summary>
        /// Other/Additional Amounts Collected  
        /// </Summary>
        public decimal? OTHER_COLL { get { return _OTHER_COLL.Value; } set { _OTHER_COLL.Value = value; } }
        /// <Summary>
        /// Other Fees And Earnings             
        /// </Summary>
        public decimal? OTHER_FEES_COLL { get { return _OTHER_FEES_COLL.Value; } set { _OTHER_FEES_COLL.Value = value; } }
        /// <Summary>
        /// Pending Costs Recovered
        /// </Summary>
        public decimal? PEND_COLL { get { return _PEND_COLL.Value; } set { _PEND_COLL.Value = value; } }
        /// <Summary>
        /// Non-recoverable Costs Recovered
        /// </Summary>
        public decimal? NR_COST_COLL { get { return _NR_COST_COLL.Value; } set { _NR_COST_COLL.Value = value; } }
        /// <Summary>
        /// Recoverable Costs Recovered 
        /// </Summary>
        public decimal? REC_COSTS_COLL { get { return _REC_COSTS_COLL.Value; } set { _REC_COSTS_COLL.Value = value; } }
        /// <Summary>
        /// Firm Costs Recovered
        /// </Summary>
        public decimal? FIRM_COSTS_COLL { get { return _FIRM_COSTS_COLL.Value; } set { _FIRM_COSTS_COLL.Value = value; } }
        /// <Summary>
        /// Other Costs Recovered
        /// </Summary>
        public decimal? OTHER_COST_COLL { get { return _OTHER_COST_COLL.Value; } set { _OTHER_COST_COLL.Value = value; } }
        /// <Summary>
        /// N/A - Reserved for Future Use
        /// </Summary>
        public decimal? FREC37 { get { return _FREC37.Value; } set { _FREC37.Value = value; } }
        /// <Summary>
        /// N/A - Reserved for Future Use
        /// </Summary>
        public decimal? FREC38 { get { return _FREC38.Value; } set { _FREC38.Value = value; } }
        /// <Summary>
        /// N/A - Reserved for Future Use
        /// </Summary>
        public decimal? FREC39 { get { return _FREC39.Value; } set { _FREC39.Value = value; } }
        /// <Summary>
        /// N/A - Reserved for Future Use
        /// </Summary>
        public decimal? FREC40 { get { return _FREC40.Value; } set { _FREC40.Value = value; } }
        /// <Summary>
        /// Costs Funded by Misc Cost Charge 
        /// </Summary>
        public decimal? EXP_M_COST { get { return _EXP_M_COST.Value; } set { _EXP_M_COST.Value = value; } }
        /// <Summary>
        /// Costs Funded by Deposit          
        /// </Summary>
        public decimal? EXP_D_COST { get { return _EXP_D_COST.Value; } set { _EXP_D_COST.Value = value; } }
        /// <Summary>
        /// Pending Costs Expended
        /// </Summary>
        public decimal? EXP_P_COST { get { return _EXP_P_COST.Value; } set { _EXP_P_COST.Value = value; } }
        /// <Summary>
        /// Recoverable Costs Expended
        /// </Summary>
        public decimal? EXP_R_COST { get { return _EXP_R_COST.Value; } set { _EXP_R_COST.Value = value; } }
        /// <Summary>
        /// Write-off Recoverable Costs
        /// </Summary>
        public decimal? WO_REC_COSTS { get { return _WO_REC_COSTS.Value; } set { _WO_REC_COSTS.Value = value; } }
        /// <Summary>
        /// Write-off Non-recoverable Costs
        /// </Summary>
        public decimal? WO_NR_COSTS { get { return _WO_NR_COSTS.Value; } set { _WO_NR_COSTS.Value = value; } }
        /// <Summary>
        /// Write-off Fees
        /// </Summary>
        public decimal? WO_FEES { get { return _WO_FEES.Value; } set { _WO_FEES.Value = value; } }
        /// <Summary>
        /// Interest % on Principal (P)      (J)
        /// </Summary>
        public decimal? I_1100_IR_J { get { return _I_1100_IR_J.Value; } set { _I_1100_IR_J.Value = value; } }
        /// <Summary>
        /// Interest % on Attorney Fees(O)   (J)
        /// </Summary>
        public decimal? I_3100_IR_J { get { return _I_3100_IR_J.Value; } set { _I_3100_IR_J.Value = value; } }
        /// <Summary>
        /// Interest % on Costs (C)          (J)
        /// </Summary>
        public decimal? I_4100_IR_J { get { return _I_4100_IR_J.Value; } set { _I_4100_IR_J.Value = value; } }
        /// <Summary>
        /// Interest % on Firm Charges(F)    (J)
        /// </Summary>
        public decimal? I_5100_IR_J { get { return _I_5100_IR_J.Value; } set { _I_5100_IR_J.Value = value; } }
        /// <Summary>
        /// Interest % on Atty Fees (S/L)    (J)
        /// </Summary>
        public decimal? I_6100_IR_J { get { return _I_6100_IR_J.Value; } set { _I_6100_IR_J.Value = value; } }
        /// <Summary>
        /// Interest Rate on Other (V)       (J)
        /// </Summary>
        public decimal? I_9100_IR_J { get { return _I_9100_IR_J.Value; } set { _I_9100_IR_J.Value = value; } }

        #endregion

        public RecordType172() : base(172, 82)
        {
            Initialize();
        }
        public RecordType172(string record) : base(172, record)
        {
            Initialize();
        }

        private void Initialize()
        {
            this._ORIG_CLAIM = new CMEDIDecimal(LineItems[9]);
            this._SUIT_AMT = new CMEDIDecimal(LineItems[10]);
            this._STAT_FEE = new CMEDIDecimal(LineItems[11]);
            this._JMT_AMT = new CMEDIDecimal(LineItems[12]);
            this._BALANCE = new CMEDIDecimal(LineItems[13]);
            this._COST_BAL = new CMEDIDecimal(LineItems[14]);
            this._COLL_HOLD = new CMEDIDecimal(LineItems[15]);
            this._PRIN_COLL = new CMEDIDecimal(LineItems[16]);
            this._INT_COLL = new CMEDIDecimal(LineItems[17]);
            this._MERCH_BEF = new CMEDIDecimal(LineItems[18]);
            this._MERCH_POST = new CMEDIDecimal(LineItems[19]);
            this._CASH_BEF = new CMEDIDecimal(LineItems[20]);
            this._CASH_POST = new CMEDIDecimal(LineItems[21]);
            this._COST_RECEIVED = new CMEDIDecimal(LineItems[22]);
            this._COST_RET = new CMEDIDecimal(LineItems[23]);
            this._COST_EXP = new CMEDIDecimal(LineItems[24]);
            this._COST_RECOVERED = new CMEDIDecimal(LineItems[25]);
            this._COMM_EARN = new CMEDIDecimal(LineItems[26]);
            this._SFEE_EARN = new CMEDIDecimal(LineItems[27]);
            this._STAT_EARN = new CMEDIDecimal(LineItems[28]);
            this._COST_POST_JUDG = new CMEDIDecimal(LineItems[29]);
            this._AFFIDAVIT = new CMEDIDecimal(LineItems[30]);
            this._PAID_POST_JUDG = new CMEDIDecimal(LineItems[31]);
            this._NON_RCVRD_COST = new CMEDIDecimal(LineItems[32]);
            this._TOT_EXP_COST = new CMEDIDecimal(LineItems[33]);
            this._DP_PRE_SUIT_NF = new CMEDIDecimal(LineItems[34]);
            this._DP_POST_SUIT_NF = new CMEDIDecimal(LineItems[35]);
            this._DP_POST_JUDG_NF = new CMEDIDecimal(LineItems[36]);
            this._MERCH_POST_JUDG = new CMEDIDecimal(LineItems[37]);
            this._EXP_COST = new CMEDIDecimal(LineItems[38]);
            this._DP_PRE_SUIT = new CMEDIDecimal(LineItems[39]);
            this._DP_POST_SUIT = new CMEDIDecimal(LineItems[40]);
            this._DP_POST_JUDG = new CMEDIDecimal(LineItems[41]);
            this._ACCR_INT_BEF = new CMEDIDecimal(LineItems[42]);
            this._TAX_REBATE = new CMEDIDecimal(LineItems[43]);
            this._AGENT_BEFSCOLL = new CMEDIDecimal(LineItems[44]);
            this._AGENT_PSCOLL = new CMEDIDecimal(LineItems[45]);
            this._AGENT_PJCOLL = new CMEDIDecimal(LineItems[46]);
            this._AGENT_COST = new CMEDIDecimal(LineItems[47]);
            this._SALES_TAX = new CMEDIDecimal(LineItems[48]);
            this._C_ADJUST = new CMEDIDecimal(LineItems[49]);
            this._COMPR_AMT = new CMEDIDecimal(LineItems[50]);
            this._OVERPAY_AMT = new CMEDIDecimal(LineItems[51]);
            this._COCO_COMM_EARN = new CMEDIDecimal(LineItems[52]);
            this._COCO_SFEE_EARN = new CMEDIDecimal(LineItems[53]);
            this._PRINCIPAL_COLL = new CMEDIDecimal(LineItems[54]);
            this._CONTRACT_COLL = new CMEDIDecimal(LineItems[55]);
            this._FEES_COLL = new CMEDIDecimal(LineItems[56]);
            this._CHARGES_COLL = new CMEDIDecimal(LineItems[57]);
            this._OTHER_COLL = new CMEDIDecimal(LineItems[58]);
            this._OTHER_FEES_COLL = new CMEDIDecimal(LineItems[59]);
            this._PEND_COLL = new CMEDIDecimal(LineItems[60]);
            this._NR_COST_COLL = new CMEDIDecimal(LineItems[61]);
            this._REC_COSTS_COLL = new CMEDIDecimal(LineItems[62]);
            this._FIRM_COSTS_COLL = new CMEDIDecimal(LineItems[63]);
            this._OTHER_COST_COLL = new CMEDIDecimal(LineItems[64]);
            this._FREC37 = new CMEDIDecimal(LineItems[65]);
            this._FREC38 = new CMEDIDecimal(LineItems[66]);
            this._FREC39 = new CMEDIDecimal(LineItems[67]);
            this._FREC40 = new CMEDIDecimal(LineItems[68]);
            this._EXP_M_COST = new CMEDIDecimal(LineItems[69]);
            this._EXP_D_COST = new CMEDIDecimal(LineItems[70]);
            this._EXP_P_COST = new CMEDIDecimal(LineItems[71]);
            this._EXP_R_COST = new CMEDIDecimal(LineItems[72]);
            this._WO_REC_COSTS = new CMEDIDecimal(LineItems[73]);
            this._WO_NR_COSTS = new CMEDIDecimal(LineItems[74]);
            this._WO_FEES = new CMEDIDecimal(LineItems[75]);
            this._I_1100_IR_J = new CMEDIDecimal(LineItems[76]);
            this._I_3100_IR_J = new CMEDIDecimal(LineItems[77]);
            this._I_4100_IR_J = new CMEDIDecimal(LineItems[78]);
            this._I_5100_IR_J = new CMEDIDecimal(LineItems[79]);
            this._I_6100_IR_J = new CMEDIDecimal(LineItems[80]);
            this._I_9100_IR_J = new CMEDIDecimal(LineItems[81]);
        }
    }
}
