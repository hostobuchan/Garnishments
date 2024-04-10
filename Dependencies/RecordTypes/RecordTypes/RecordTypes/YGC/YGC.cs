using RecordTypes.YGC.Base;
using RecordTypes.YGC.DataTypes;
using RecordTypes.YGC.Enums;
using System;

namespace RecordTypes
{
    namespace YGC
    {
        #region Record Type 01
        /// <summary>
        /// Record Type 01 - New Account Information (Sender to Receiver)
        /// <para>This record is required to create a new account. It identifies the creditor and current owner of the debt and establishes the current financial state of the debt. Value = 01.</para>
        /// </summary>
        public class RecordType01 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the date the account was first sent to the receiver to work, regardless of when it was uploaded to YouveGotClaims®. If this field is left blank, YouveGotReports will automatically fill it with the date this record is processed.
            /// </summary>
            public YGCDate DATE_FORW { get; private set; }
            /// <summary>
            /// If you have bonded this account with a law listing company through your own means, enter the YGC ID of the listing company here. 
            /// <para>NOTE: Specifying a law list here does not trigger a bond request. To submit a coupon to bond the account through YGC, send a record 99.</para>
            /// Current values for LAW_LIST are:
            /// <para>NL = National List</para>
            /// <para>ALQ = American Legal Quarterly</para>
            /// <para>GB = General Bar</para>
            /// <para>CB = The Commercial Bar</para>
            /// <para>FL = Forwarders List</para>
            /// <para>CL = Columbia List</para>
            /// <para>WH = Wright Holmes</para>
            /// <para>IL = International Lawyer Referral</para>
            /// </summary>
            public YGCString LAW_LIST { get; private set; }
            /// <summary>
            /// This is the commission percentage the receiver earns as determined by the sender. There is no set numeric format.
            /// </summary>
            public YGCString COMM { get; private set; }
            /// <summary>
            /// This is the percentage the receiver earns if a suit is filed. There is no set numeric format.
            /// </summary>
            public YGCString SFEE { get; private set; }
            /// <summary>
            /// This is the dollar amount of the original principal due at time of placement.
            /// </summary>
            public YGCDecimal ORIG_CLAIM { get; private set; }
            /// <summary>
            /// This is the dollar amount of accrued interest.
            /// </summary>
            public YGCDecimal ORIG_INT { get; private set; }
            /// <summary>
            /// This is the last day of the accrual period.
            /// </summary>
            public YGCDate ORIG_INT_D { get; private set; }
            /// <summary>
            /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form:
            /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
            /// </summary>
            public YGCDecimal RATES_PRE { get; private set; }
            /// <summary>
            /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form:
            /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
            /// </summary>
            public YGCDecimal RATES_POST { get; private set; }
            /// <summary>
            /// This is the company name of either the original creditor or debt purchaser/collection agency.
            /// </summary>
            public YGCString CRED_NAME { get; private set; }
            /// <summary>
            /// This is an overflow field for CRED_NAME.
            /// </summary>
            public YGCString CRED_NAME2 { get; private set; }
            /// <summary>
            /// This is the company name of either the original creditor or debt purchaser/collection agency.
            /// </summary>
            public YGCString CRED_NAME_COMBINED { get; private set; }
            /// <summary>
            /// Street address of company in CRED_NAME.
            /// </summary>
            public YGCString CRED_STREET { get; private set; }
            /// <summary>
            /// City and state of company in CRED_NAME. Format this field as City ST or City,ST.
            /// <para>(Example: Linden NJ or Linden,NJ)</para>
            /// </summary>
            public YGCString CRED_CS { get; private set; }
            /// <summary>
            /// This is the creditor's zip code. It can accommodate the four-digit extension if you do not include the hyphen.
            /// </summary>
            public YGCString CRED_ZIP { get; private set; }
            /// <summary>
            /// This field represents everything the debtor owes at time of placement, excluding interest. It equals ORIG_CLAIM + LATE_FEE + CNTRCT_FEE + STAT_FEE + JDG_COSTS, where the last three fields are from a previous legal action defined in Record 07 and LATE_FEE is from this record.
            /// </summary>
            public YGCDecimal DEBT_BAL { get; private set; }
            /// <summary>
            /// User-defined. Suggested Codes:  ARB = Arbitration, MED = Medical, CARD = Credit Card, LOAN = loan
            /// </summary>
            public YGCString CTYPE { get; private set; }
            /// <summary>
            /// This is the date the last payment was received by the creditor.
            /// </summary>
            public YGCDate DATE_LPAY { get; private set; }
            /// <summary>
            /// This is the dollar amount of the last payment made to the creditor.
            /// </summary>
            public YGCDecimal AMT_LPAY { get; private set; }
            /// <summary>
            /// Typically, the date the credit card account was opened.
            /// </summary>
            public YGCDate DATE_OPEN { get; private set; }
            /// <summary>
            /// This is the date the creditor has determined the debt will not be paid through normal channels.
            /// </summary>
            public YGCDate CHRG_OFF_D { get; private set; }
            /// <summary>
            /// This is typically the same as ORIG_CLAIM.
            /// </summary>
            public YGCDecimal CHRG_OFF_A { get; private set; }
            /// <summary>
            /// If this debt was purchased, this field holds the date of purchase.
            /// </summary>
            public YGCDate PURCHASE_D { get; private set; }
            /// <summary>
            /// This is only filled out if this debt was purchased or if the sender is a collection agency. It holds the name of the original creditor company.
            /// </summary>
            public YGCString ORIG_CRED { get; private set; }
            /// <summary>
            /// This is an overflow field for ORIG_CRED.
            /// </summary>
            public YGCString ORIG_CRED2 { get; private set; }
            /// <summary>
            /// Combined Value of ORIG_CRED and ORIG_CRED2
            /// </summary>
            public YGCString ORIG_CRED_COMBINED { get; private set; }
            /// <summary>
            /// Typically pertains to purchased debt, and holds the number assigned to the batch of accounts purchased. Some reports in YouveGotReports can be filtered against this code.
            /// </summary>
            public YGCString PORT_ID { get; private set; }
            /// <summary>
            /// This is the standard code for the creditor's country.
            /// </summary>
            public YGCString CRED_CNTRY { get; private set; }
            /// <summary>
            /// This is the date the last payment was received by the issuer.
            /// </summary>
            public YGCDate LPAY_ISS_D { get; private set; }
            /// <summary>
            /// This is the amount of the last payment received by the issuer.
            /// </summary>
            public YGCDecimal LPAY_ISS_AMT { get; private set; }
            /// <summary>
            /// This flag indicates whether the sender has media to support a lawsuit. It is up to the receiver to determine what kind of media will ultimately be required, at which time they will send a request. Use values Y or N.
            /// </summary>
            public YGCString MEDIA { get; private set; }
            /// <summary>
            /// Date of Delinquency
            /// </summary>
            public YGCDate DELINQ_D { get; private set; }
            /// <summary>
            /// Date of Acceleration
            /// </summary>
            public YGCDate ACCEL_D { get; private set; }
            /// <summary>
            /// Date of Repossession
            /// </summary>
            public YGCDate REPO_D { get; private set; }
            /// <summary>
            /// Sale Date
            /// </summary>
            public YGCDate SALE_D { get; private set; }
            /// <summary>
            /// Maturity Date
            /// </summary>
            public YGCDate MATUR_D { get; private set; }
            /// <summary>
            /// The Statute of Limitations Start Date is calculated by the sender.
            /// </summary>
            public YGCDate SOL_START_D { get; private set; }
            /// <summary>
            /// The Statute of Limitations End Date is calculated by the sender. You can also refer to the Documents tab, Business Reference category for the current statutes for each state.
            /// </summary>
            public YGCDate SOL_END_D { get; private set; }
            /// <summary>
            /// This is the accumulation of late fees for non-payment before placement. This should be included in the calculation of DEBT_BAL.
            /// </summary>
            public YGCDecimal LATE_FEE { get; private set; }
            /// <summary>
            /// This represents the name of the creditor for the account before the current CRED_NAME or ORIG_CRED if present. If ORIG_CRED is not present, HIST_CRED1 should NOT be used in place of ORIG_CRED.
            /// </summary>
            public YGCString HIST_CRED1 { get; private set; }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED1.
            /// </summary>
            public YGCString HIST_CRED2 { get; private set; }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED2.
            /// </summary>
            public YGCString HIST_CRED3 { get; private set; }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED3.
            /// </summary>
            public YGCString HIST_CRED4 { get; private set; }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED4.
            /// </summary>
            public YGCString HIST_CRED5 { get; private set; }
            /// <summary>
            /// This holds any additional cost besides legal costs, such as bounced check fees, already incurred for pursuing this debt.
            /// </summary>
            public YGCDecimal ADDITIONAL_COST { get; private set; }
            /// <summary>
            /// If a sender recalls an account placed with YGC, this field holds the date it was recalled.
            /// </summary>
            public YGCDate RECALL_DATE { get; private set; }
            /// <summary>
            /// This is the number for the debt account before charge-off. This may differ from FORW_FILE.
            /// </summary>
            public YGCString PRECHARGEOFFACCOUNTNUMBER { get; private set; }
            /// <summary>
            /// This flag indicates whether the account requires a FACT Act statement.
            /// <para>Use values Y or N.</para>
            /// </summary>
            public YGCBool FACTACTSTATEMENT { get; private set; }
            /// <summary>
            /// This field is the Attorney, Agency or Vendor Code set by the current Creditor or Debt Buyer's internal system.
            /// </summary>
            public YGCString VENDORCODE { get; private set; }
            /// <summary>
            /// This field is used when original products or services were provided by one company for offer under another company's brand.  This allows the current Creditor or Debt Buyer to identify the private label from the original creditor. (Example: "Private Label" -GE/Walmart)
            /// </summary>
            public YGCString LOANDESCRIPTION { get; private set; }
            /// <summary>
            /// This is an overflow field for ORIG_CRED and ORIG_CRED2.
            /// </summary>
            public YGCString ORIG_CRED3 { get; private set; }
            /// <summary>
            /// Street address of company in ORIG_CRED.
            /// </summary>
            public YGCString ORIG_CRED_STREET { get; private set; }
            /// <summary>
            /// Street 2 field for address of company in ORIG_CRED.
            /// </summary>
            public YGCString ORIG_CRED_STREET2 { get; private set; }
            /// <summary>
            /// City of company in ORIG_CRED.
            /// </summary>
            public YGCString ORIG_CRED_CITY { get; private set; }
            /// <summary>
            /// State of company in ORIG_CRED.
            /// </summary>
            public YGCString ORIG_CRED_ST { get; private set; }
            /// <summary>
            /// This is the ORIG_CRED zip code.
            /// It can accommodate the four-digit extension if you do not include the hyphen.
            /// </summary>
            public YGCString ORIG_CRED_ZIP { get; private set; }
            /// <summary>
            /// The date in which the original creditor reported the date of default. 
            /// If the date was not reported by the original creditor, then the debt buyer calculates this field by subtracting 210 days from the charge off date.
            /// </summary>
            public YGCDate OCCURRENCE_D { get; private set; }
            /// <summary>
            /// This field indicates whether the date listed in the OCCURRENCE_D field was calculated or provided by the original creditor. 
            /// Use Values:
            /// C = Calculated
            /// P = Provided
            /// N = Not Applicable
            /// </summary>
            public YGCEnum<OccurrenceType, OccurrenceTypeValues> OCCURRENCE_D_FLAG { get; private set; }
            /// <summary>
            /// This is a post charge off fee that accrued prior to the time of placement. This should be included in the DEBT_BAL.
            /// </summary>
            public YGCDecimal POST_CHARGE_OFF_FEE { get; private set; }
            /// <summary>
            /// This is the total amount of payments that have accrued between CHRG_OFF_D and PURCHASE_D.
            /// </summary>
            public YGCDecimal SELLER_PAYMENTS { get; private set; }
            /// <summary>
            /// This is the total amount of credits that have accrued between CHRG_OFF_D and PURCHASE_D.
            /// </summary>
            public YGCDecimal SELLER_CREDITS { get; private set; }
            /// <summary>
            /// This is the total amount of payments and credits that have accrued between CHRG_OFF_D and PURCHASE_D.
            /// </summary>
            public YGCDecimal SELLER_PAYMENTS_CREDITS { get; private set; }
            /// <summary>
            /// This is the total amount of fees that have accrued between CHRG_OFF_D and PURCHASE_D.
            /// </summary>
            public YGCDecimal POSTCHARGEOFFSELLERFEES { get; private set; }
            #endregion

            protected RecordType01(int Record) : base(Record)
            {
                this.DATE_FORW = new YGCDate();
                this.LAW_LIST = new YGCString(4);
                this.COMM = new YGCString(4);
                this.SFEE = new YGCString(4);
                this.ORIG_CLAIM = new YGCDecimal(14, 2);
                this.ORIG_INT = new YGCDecimal(14, 2);
                this.ORIG_INT_D = new YGCDate();
                this.RATES_PRE = new YGCDecimal(5, 4, true);
                this.RATES_POST = new YGCDecimal(5, 4, true);
                this.CRED_NAME = new YGCString(25);
                this.CRED_NAME2 = new YGCString(25);
                this.CRED_NAME_COMBINED = new YGCString(50);
                this.CRED_STREET = new YGCString(25);
                this.CRED_CS = new YGCString(15);
                this.CRED_ZIP = new YGCString(9);
                this.DEBT_BAL = new YGCDecimal(14, 2);
                this.CTYPE = new YGCString(4);
                this.DATE_LPAY = new YGCDate();
                this.AMT_LPAY = new YGCDecimal(14, 2);
                this.DATE_OPEN = new YGCDate();
                this.CHRG_OFF_D = new YGCDate();
                this.CHRG_OFF_A = new YGCDecimal(14, 2);
                this.PURCHASE_D = new YGCDate();
                this.ORIG_CRED = new YGCString(30);
                this.ORIG_CRED2 = new YGCString(30);
                this.ORIG_CRED_COMBINED = new YGCString(60);
                this.PORT_ID = new YGCString(20);
                this.CRED_CNTRY = new YGCString(3);
                this.LPAY_ISS_D = new YGCDate();
                this.LPAY_ISS_AMT = new YGCDecimal(14, 2);
                this.MEDIA = new YGCString(1);
                this.DELINQ_D = new YGCDate();
                this.ACCEL_D = new YGCDate();
                this.REPO_D = new YGCDate();
                this.SALE_D = new YGCDate();
                this.MATUR_D = new YGCDate();
                this.SOL_START_D = new YGCDate();
                this.SOL_END_D = new YGCDate();
                this.LATE_FEE = new YGCDecimal(14, 2);
                this.HIST_CRED1 = new YGCString(50);
                this.HIST_CRED2 = new YGCString(50);
                this.HIST_CRED3 = new YGCString(50);
                this.HIST_CRED4 = new YGCString(50);
                this.HIST_CRED5 = new YGCString(50);
                this.ADDITIONAL_COST = new YGCDecimal(14, 2);
                this.RECALL_DATE = new YGCDate();
                this.PRECHARGEOFFACCOUNTNUMBER = new YGCString(20);
                this.FACTACTSTATEMENT = new YGCBool(1, "Y", "N");
                this.VENDORCODE = new YGCString(15);
                this.LOANDESCRIPTION = new YGCString(200);
                this.ORIG_CRED3 = new YGCString(30);
                this.ORIG_CRED_STREET = new YGCString(50);
                this.ORIG_CRED_STREET2 = new YGCString(50);
                this.ORIG_CRED_CITY = new YGCString(20);
                this.ORIG_CRED_ST = new YGCString(2);
                this.ORIG_CRED_ZIP = new YGCString(9);
                this.OCCURRENCE_D = new YGCDate();
                this.OCCURRENCE_D_FLAG = new YGCEnum<OccurrenceType, OccurrenceTypeValues>(1);
                this.POST_CHARGE_OFF_FEE = new YGCDecimal(14, 2);
                this.SELLER_PAYMENTS = new YGCDecimal(14, 2);
                this.SELLER_CREDITS = new YGCDecimal(14, 2);
                this.SELLER_PAYMENTS_CREDITS = new YGCDecimal(14, 2);
                this.POSTCHARGEOFFSELLERFEES = new YGCDecimal(14, 2);
            }
            public RecordType01() : this(1) { }
            public RecordType01(string RT01Entry) : base(RT01Entry)
            {
                try
                {
                    this.DATE_FORW = new YGCDate() { DataString = RT01Entry.Substring(67) };
                    this.LAW_LIST = new YGCString(4) { DataString = RT01Entry.Substring(75) };
                    this.COMM = new YGCString(4) { DataString = RT01Entry.Substring(79) };
                    this.SFEE = new YGCString(4) { DataString = RT01Entry.Substring(83) };
                    this.ORIG_CLAIM = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(87) };
                    this.ORIG_INT = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(101) };
                    this.ORIG_INT_D = new YGCDate() { DataString = RT01Entry.Substring(115) };
                    this.RATES_PRE = new YGCDecimal(5, 4, true) { DataString = RT01Entry.Substring(123) };
                    this.RATES_POST = new YGCDecimal(5, 4, true) { DataString = RT01Entry.Substring(128) };
                    this.CRED_NAME = new YGCString(25) { DataString = RT01Entry.Substring(133) };
                    this.CRED_NAME2 = new YGCString(25) { DataString = RT01Entry.Substring(158) };
                    this.CRED_NAME_COMBINED = new YGCString(50) { DataString = RT01Entry.Substring(133) };
                    this.CRED_STREET = new YGCString(25) { DataString = RT01Entry.Substring(183) };
                    this.CRED_CS = new YGCString(15) { DataString = RT01Entry.Substring(208) };
                    this.CRED_ZIP = new YGCString(9) { DataString = RT01Entry.Substring(223) };
                    this.DEBT_BAL = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(232) };
                    this.CTYPE = new YGCString(4) { DataString = RT01Entry.Substring(246) };
                    this.DATE_LPAY = new YGCDate() { DataString = RT01Entry.Substring(250) };
                    this.AMT_LPAY = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(258) };
                    this.DATE_OPEN = new YGCDate() { DataString = RT01Entry.Substring(272) };
                    this.CHRG_OFF_D = new YGCDate() { DataString = RT01Entry.Substring(280) };
                    this.CHRG_OFF_A = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(288) };
                    this.PURCHASE_D = new YGCDate() { DataString = RT01Entry.Substring(302) };
                    this.ORIG_CRED = new YGCString(30) { DataString = RT01Entry.Substring(310) };
                    this.ORIG_CRED2 = new YGCString(30) { DataString = RT01Entry.Substring(340) };
                    this.ORIG_CRED_COMBINED = new YGCString(60) { DataString = RT01Entry.Substring(310) };
                    this.PORT_ID = new YGCString(20) { DataString = RT01Entry.Substring(370) };
                    this.CRED_CNTRY = new YGCString(3) { DataString = RT01Entry.Substring(390) };
                    this.LPAY_ISS_D = new YGCDate() { DataString = RT01Entry.Substring(393) };
                    this.LPAY_ISS_AMT = new YGCDecimal(14, 2) { DataString = RT01Entry.Substring(401) };
                    this.MEDIA = new YGCString(1) { DataString = RT01Entry.Substring(415) };
                    this.DELINQ_D = new YGCDate() { DataString = RT01Entry.Substring(416) };
                    this.ACCEL_D = new YGCDate() { DataString = RT01Entry.Substring(424) };
                    this.REPO_D = new YGCDate() { DataString = RT01Entry.Substring(432) };
                    this.SALE_D = new YGCDate() { DataString = RT01Entry.Substring(440) };
                    this.MATUR_D = new YGCDate() { DataString = RT01Entry.Substring(448) };
                    this.SOL_START_D = new YGCDate() { DataString = RT01Entry.Substring(456) };
                    this.SOL_END_D = new YGCDate() { DataString = RT01Entry.Substring(464) };
                    this.LATE_FEE = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 472 ? RT01Entry.Substring(472) : "" };
                    this.HIST_CRED1 = new YGCString(50) { DataString = RT01Entry.Length > 486 ? RT01Entry.Substring(486) : "" };
                    this.HIST_CRED2 = new YGCString(50) { DataString = RT01Entry.Length > 536 ? RT01Entry.Substring(536) : "" };
                    this.HIST_CRED3 = new YGCString(50) { DataString = RT01Entry.Length > 586 ? RT01Entry.Substring(586) : "" };
                    this.HIST_CRED4 = new YGCString(50) { DataString = RT01Entry.Length > 636 ? RT01Entry.Substring(636) : "" };
                    this.HIST_CRED5 = new YGCString(50) { DataString = RT01Entry.Length > 686 ? RT01Entry.Substring(686) : "" };
                    this.ADDITIONAL_COST = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 736 ? RT01Entry.Substring(736) : "" };
                    this.RECALL_DATE = new YGCDate() { DataString = RT01Entry.Length > 750 ? RT01Entry.Substring(750) : "" };
                    this.PRECHARGEOFFACCOUNTNUMBER = new YGCString(20) { DataString = RT01Entry.Length > 758 ? RT01Entry.Substring(758) : "" };
                    this.FACTACTSTATEMENT = new YGCBool(1, "Y", "N") { DataString = RT01Entry.Length > 778 ? RT01Entry.Substring(778) : "" };
                    this.VENDORCODE = new YGCString(15) { DataString = RT01Entry.Length > 779 ? RT01Entry.Substring(779) : "" };
                    this.LOANDESCRIPTION = new YGCString(200) { DataString = RT01Entry.Length > 794 ? RT01Entry.Substring(794) : "" };
                    this.ORIG_CRED3 = new YGCString(30) { DataString = RT01Entry.Length > 994 ? RT01Entry.Substring(994) : "" };
                    this.ORIG_CRED_STREET = new YGCString(50) { DataString = RT01Entry.Length > 1024 ? RT01Entry.Substring(1024) : "" };
                    this.ORIG_CRED_STREET2 = new YGCString(50) { DataString = RT01Entry.Length > 1074 ? RT01Entry.Substring(1074) : "" };
                    this.ORIG_CRED_CITY = new YGCString(20) { DataString = RT01Entry.Length > 1124 ? RT01Entry.Substring(1124) : "" };
                    this.ORIG_CRED_ST = new YGCString(2) { DataString = RT01Entry.Length > 1144 ? RT01Entry.Substring(1144) : "" };
                    this.ORIG_CRED_ZIP = new YGCString(9) { DataString = RT01Entry.Length > 1146 ? RT01Entry.Substring(1146) : "" };
                    this.OCCURRENCE_D = new YGCDate() { DataString = RT01Entry.Length > 1155 ? RT01Entry.Substring(1155) : "" };
                    this.OCCURRENCE_D_FLAG = new YGCEnum<OccurrenceType, OccurrenceTypeValues>(1) { DataString = RT01Entry.Length > 1163 ? RT01Entry.Substring(1163) : "" };
                    this.POST_CHARGE_OFF_FEE = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 1164 ? RT01Entry.Substring(1164) : "" };
                    this.SELLER_PAYMENTS = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 1178 ? RT01Entry.Substring(1178) : "" };
                    this.SELLER_CREDITS = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 1192 ? RT01Entry.Substring(1192) : "" };
                    this.SELLER_PAYMENTS_CREDITS = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 1206 ? RT01Entry.Substring(1206) : "" };
                    this.POSTCHARGEOFFSELLERFEES = new YGCDecimal(14, 2) { DataString = RT01Entry.Length > 1220 ? RT01Entry.Substring(1220) : "" };

                }
                catch
                {
                    if (this.DATE_FORW == null) this.DATE_FORW = new YGCDate();
                    if (this.LAW_LIST == null) this.LAW_LIST = new YGCString(4);
                    if (this.COMM == null) this.COMM = new YGCString(4);
                    if (this.SFEE == null) this.SFEE = new YGCString(4);
                    if (this.ORIG_CLAIM == null) this.ORIG_CLAIM = new YGCDecimal(14, 2);
                    if (this.ORIG_INT == null) this.ORIG_INT = new YGCDecimal(14, 2);
                    if (this.ORIG_INT_D == null) this.ORIG_INT_D = new YGCDate();
                    if (this.RATES_PRE == null) this.RATES_PRE = new YGCDecimal(5, 4);
                    if (this.RATES_POST == null) this.RATES_POST = new YGCDecimal(5, 4);
                    if (this.CRED_NAME == null) this.CRED_NAME = new YGCString(25);
                    if (this.CRED_NAME2 == null) this.CRED_NAME2 = new YGCString(25);
                    if (this.CRED_STREET == null) this.CRED_STREET = new YGCString(25);
                    if (this.CRED_CS == null) this.CRED_CS = new YGCString(15);
                    if (this.CRED_ZIP == null) this.CRED_ZIP = new YGCString(9);
                    if (this.DEBT_BAL == null) this.DEBT_BAL = new YGCDecimal(14, 2);
                    if (this.CTYPE == null) this.CTYPE = new YGCString(4);
                    if (this.DATE_LPAY == null) this.DATE_LPAY = new YGCDate();
                    if (this.AMT_LPAY == null) this.AMT_LPAY = new YGCDecimal(14, 2);
                    if (this.DATE_OPEN == null) this.DATE_OPEN = new YGCDate();
                    if (this.CHRG_OFF_D == null) this.CHRG_OFF_D = new YGCDate();
                    if (this.CHRG_OFF_A == null) this.CHRG_OFF_A = new YGCDecimal(14, 2);
                    if (this.PURCHASE_D == null) this.PURCHASE_D = new YGCDate();
                    if (this.ORIG_CRED == null) this.ORIG_CRED = new YGCString(30);
                    if (this.ORIG_CRED2 == null) this.ORIG_CRED2 = new YGCString(30);
                    if (this.PORT_ID == null) this.PORT_ID = new YGCString(20);
                    if (this.CRED_CNTRY == null) this.CRED_CNTRY = new YGCString(3);
                    if (this.LPAY_ISS_D == null) this.LPAY_ISS_D = new YGCDate();
                    if (this.LPAY_ISS_AMT == null) this.LPAY_ISS_AMT = new YGCDecimal(14, 2);
                    if (this.MEDIA == null) this.MEDIA = new YGCString(1);
                    if (this.DELINQ_D == null) this.DELINQ_D = new YGCDate();
                    if (this.ACCEL_D == null) this.ACCEL_D = new YGCDate();
                    if (this.REPO_D == null) this.REPO_D = new YGCDate();
                    if (this.SALE_D == null) this.SALE_D = new YGCDate();
                    if (this.MATUR_D == null) this.MATUR_D = new YGCDate();
                    if (this.SOL_START_D == null) this.SOL_START_D = new YGCDate();
                    if (this.SOL_END_D == null) this.SOL_END_D = new YGCDate();
                    if (this.LATE_FEE == null) this.LATE_FEE = new YGCDecimal(14, 2);
                    if (this.HIST_CRED1 == null) this.HIST_CRED1 = new YGCString(50);
                    if (this.HIST_CRED2 == null) this.HIST_CRED2 = new YGCString(50);
                    if (this.HIST_CRED3 == null) this.HIST_CRED3 = new YGCString(50);
                    if (this.HIST_CRED4 == null) this.HIST_CRED4 = new YGCString(50);
                    if (this.HIST_CRED5 == null) this.HIST_CRED5 = new YGCString(50);
                    if (this.ADDITIONAL_COST == null) this.ADDITIONAL_COST = new YGCDecimal(14, 2);
                    if (this.RECALL_DATE == null) this.RECALL_DATE = new YGCDate();
                    if (this.PRECHARGEOFFACCOUNTNUMBER == null) this.PRECHARGEOFFACCOUNTNUMBER = new YGCString(20);
                    if (this.FACTACTSTATEMENT == null) this.FACTACTSTATEMENT = new YGCBool(1, "Y", "N");
                    if (this.VENDORCODE == null) this.VENDORCODE = new YGCString(15);
                    if (this.LOANDESCRIPTION == null) this.LOANDESCRIPTION = new YGCString(200);
                    if (this.ORIG_CRED3 == null) this.ORIG_CRED3 = new YGCString(30);
                    if (this.ORIG_CRED_STREET == null) this.ORIG_CRED_STREET = new YGCString(50);
                    if (this.ORIG_CRED_STREET2 == null) this.ORIG_CRED_STREET2 = new YGCString(50);
                    if (this.ORIG_CRED_CITY == null) this.ORIG_CRED_CITY = new YGCString(20);
                    if (this.ORIG_CRED_ST == null) this.ORIG_CRED_ST = new YGCString(2);
                    if (this.ORIG_CRED_ZIP == null) this.ORIG_CRED_ZIP = new YGCString(9);
                    if (this.OCCURRENCE_D == null) this.OCCURRENCE_D = new YGCDate();
                    if (this.OCCURRENCE_D_FLAG == null) this.OCCURRENCE_D_FLAG = new YGCEnum<OccurrenceType, OccurrenceTypeValues>(1);
                    if (this.POST_CHARGE_OFF_FEE == null) this.POST_CHARGE_OFF_FEE = new YGCDecimal(14, 2);
                    if (this.SELLER_PAYMENTS == null) this.SELLER_PAYMENTS = new YGCDecimal(14, 2);
                    if (this.SELLER_CREDITS == null) this.SELLER_CREDITS = new YGCDecimal(14, 2);
                    if (this.SELLER_PAYMENTS_CREDITS == null) this.SELLER_PAYMENTS_CREDITS = new YGCDecimal(14, 2);
                    if (this.POSTCHARGEOFFSELLERFEES == null) this.POSTCHARGEOFFSELLERFEES = new YGCDecimal(14, 2);

                }
            }

            public override Type GetType() { return typeof(RecordType01); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}",
                    base.ToString(),
                    this.DATE_FORW,
                    this.LAW_LIST,
                    this.COMM,
                    this.SFEE,
                    this.ORIG_CLAIM,
                    this.ORIG_INT,
                    this.ORIG_INT_D,
                    this.RATES_PRE,
                    this.RATES_POST,
                    this.CRED_NAME,
                    this.CRED_NAME2,
                    this.CRED_STREET,
                    this.CRED_CS,
                    this.CRED_ZIP,
                    this.DEBT_BAL,
                    this.CTYPE,
                    this.DATE_LPAY,
                    this.AMT_LPAY,
                    this.DATE_OPEN,
                    this.CHRG_OFF_D,
                    this.CHRG_OFF_A,
                    this.PURCHASE_D,
                    this.ORIG_CRED,
                    this.ORIG_CRED2,
                    this.PORT_ID,
                    this.CRED_CNTRY,
                    this.LPAY_ISS_D,
                    this.LPAY_ISS_AMT,
                    this.MEDIA,
                    this.DELINQ_D,
                    this.ACCEL_D,
                    this.REPO_D,
                    this.SALE_D,
                    this.MATUR_D,
                    this.SOL_START_D,
                    this.SOL_END_D,
                    this.LATE_FEE,
                    this.HIST_CRED1,
                    this.HIST_CRED2,
                    this.HIST_CRED3,
                    this.HIST_CRED4,
                    this.HIST_CRED5,
                    this.ADDITIONAL_COST,
                    this.RECALL_DATE,
                    this.PRECHARGEOFFACCOUNTNUMBER,
                    this.FACTACTSTATEMENT,
                    this.VENDORCODE,
                    this.LOANDESCRIPTION,
                    this.ORIG_CRED3,
                    this.ORIG_CRED_STREET,
                    this.ORIG_CRED_STREET2,
                    this.ORIG_CRED_CITY,
                    this.ORIG_CRED_ST,
                    this.ORIG_CRED_ZIP,
                    this.OCCURRENCE_D,
                    this.OCCURRENCE_D_FLAG,
                    this.POST_CHARGE_OFF_FEE,
                    this.SELLER_PAYMENTS,
                    this.SELLER_CREDITS,
                    this.SELLER_PAYMENTS_CREDITS,
                    this.POSTCHARGEOFFSELLERFEES);
            }
        }
        #endregion

        #region Record Type 02
        /// <summary>
        /// Record Type 02 - Primary Debtor Information (Sender to Receiver)
        /// <para>This record identifies the primary debtor and significant legal events. Record type value = 02. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 02, 31 or 52, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
        /// </summary>
        public class RecordType02 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the primary debtor's name. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D1_NAME { get; private set; }
            /// <summary>
            /// This single-byte field is a code for the salutation. The valid codes are:
            /// <para>1 - Mr.</para>
            /// <para>2 - Mrs.</para>
            /// <para>3 - Ms.</para>
            /// <para>4 - Mr. &amp; Mrs.</para>
            /// <para>5 - Dr.</para>
            /// <para>6 - Capt.</para>
            /// <para>7-9 - Gentlemen</para>
            /// </summary>
            public YGCEnum<Enums.Salutation> D1_SALUT { get; private set; }
            /// <summary>
            /// This is the primary debtor's alias name. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D1_ALIAS { get; private set; }
            /// <summary>
            /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET { get; private set; }
            /// <summary>
            /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
            /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
            /// </para></summary>
            public YGCString D1_CS { get; private set; }
            /// <summary>
            /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_ZIP { get; private set; }
            /// <summary>
            /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_PHONE { get; private set; }
            /// <summary>
            /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_FAX { get; private set; }
            /// <summary>
            /// This is the primary debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public YGCString D1_SSN { get; private set; }
            /// <summary>
            /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
            /// </summary>
            public YGCString RFILE { get; private set; }
            /// <summary>
            /// This is the primary debtor's date of birth.
            /// </summary>
            public YGCDate D1_DOB { get; private set; }
            /// <summary>
            /// This is the primary debtor's driver's license number.
            /// </summary>
            public YGCString D1_DL { get; private set; }
            /// <summary>
            /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STATE { get; private set; }
            /// <summary>
            /// Set this field to Y if the served papers were returned.
            /// </summary>
            public YGCBool D1_MAIL { get; private set; }
            /// <summary>
            /// This is the date the suit was served.
            /// </summary>
            public YGCDate SERVICE_D { get; private set; }
            /// <summary>
            /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
            /// </summary>
            public YGCDate ANSWER_DUE_D { get; private set; }
            /// <summary>
            /// This is the date the debtor's response was filed.
            /// </summary>
            public YGCDate ANSWER_FILE_D { get; private set; }
            /// <summary>
            /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
            /// </summary>
            public YGCDate DEFAULT_D { get; private set; }
            /// <summary>
            /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
            /// </summary>
            public YGCDate TRIAL_D { get; private set; }
            /// <summary>
            /// This is the date of the latest hearing on a motion filed by either party.
            /// </summary>
            public YGCDate HEARING_D { get; private set; }
            /// <summary>
            /// This is the date a lien was filed against a debtor's property.
            /// </summary>
            public YGCDate LIEN_D { get; private set; }
            /// <summary>
            /// This is the date garnishment against the debtor's wages was established.
            /// </summary>
            public YGCDate GARN_D { get; private set; }
            ///<summary>
            ///This is the method used to serve the papers. 
            ///<para>The valid codes are: </para>
            ///<para>PER = Personal</para>
            ///<para>CER = Certified Mail</para>
            ///<para>SUB = Sub-service</para>
            ///<para>POS = Posting (left at front door)</para>
            ///<para>FIR = First Class Mail</para>
            ///</summary>
            public YGCString SERVICE_TYPE { get; private set; }
            /// <summary>
            /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET2 { get; private set; }
            /// <summary>
            /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public YGCString D1_CITY { get; private set; }
            /// <summary>
            /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_CELL { get; private set; }
            /// <summary>
            /// Fair Isaac credit score
            /// </summary>
            public YGCNumber SCORE_FICO { get; private set; }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public YGCNumber SCORE_COLLECT { get; private set; }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public YGCNumber SCORE_OTHER { get; private set; }
            /// <summary>
            /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public YGCString D1_CNTRY { get; private set; }
            /// <summary>
            /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET_LONG { get; private set; }
            /// <summary>
            /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET2_LONG { get; private set; }
            /// <summary>
            /// Debtor First Name
            /// </summary>
            public YGCString FIRSTNAME { get; private set; }
            /// <summary>
            /// Debtor Last Name
            /// </summary>
            public YGCString LASTNAME { get; private set; }
            /// <summary>
            /// Creditor-Internal calculated score
            /// </summary>
            public YGCString SCOREINTERNAL { get; private set; }
            /// <summary>
            /// This field is the Placement or Internal Delinquency Stage of the account at the time of placement used by the the Creditor or Debt Buyer (User Defined).
            /// </summary>
            public YGCString STAGE { get; private set; }
            #endregion

            protected RecordType02(int Record) : base(Record)
            {
                this.D1_NAME = new YGCString(30);
                this.D1_SALUT = new YGCEnum<Salutation>(1);
                this.D1_ALIAS = new YGCString(25);
                this.D1_STREET = new YGCString(25);
                this.D1_CS = new YGCString(23);
                this.D1_ZIP = new YGCString(9);
                this.D1_PHONE = new YGCString(12);
                this.D1_FAX = new YGCString(12);
                this.D1_SSN = new YGCString(15);
                this.RFILE = new YGCString(8);
                this.D1_DOB = new YGCDate();
                this.D1_DL = new YGCString(17);
                this.D1_STATE = new YGCString(3);
                this.D1_MAIL = new YGCBool(1, "Y");
                this.SERVICE_D = new YGCDate();
                this.ANSWER_DUE_D = new YGCDate();
                this.ANSWER_FILE_D = new YGCDate();
                this.DEFAULT_D = new YGCDate();
                this.TRIAL_D = new YGCDate();
                this.HEARING_D = new YGCDate();
                this.LIEN_D = new YGCDate();
                this.GARN_D = new YGCDate();
                this.SERVICE_TYPE = new YGCString(4);
                this.D1_STREET2 = new YGCString(25);
                this.D1_CITY = new YGCString(30);
                this.D1_CELL = new YGCString(12);
                this.SCORE_FICO = new YGCNumber(3);
                this.SCORE_COLLECT = new YGCNumber(3);
                this.SCORE_OTHER = new YGCNumber(3);
                this.D1_CNTRY = new YGCString(3);
                this.D1_STREET_LONG = new YGCString(50);
                this.D1_STREET2_LONG = new YGCString(50);
                this.FIRSTNAME = new YGCString(30);
                this.LASTNAME = new YGCString(30);
                this.SCOREINTERNAL = new YGCString(5);
                this.STAGE = new YGCString(10);
            }
            public RecordType02() : this(2) { }
            public RecordType02(string RT02Entry) : base(RT02Entry)
            {
                try
                {
                    this.D1_NAME = new YGCString(30) { DataString = RT02Entry.Substring(67) };
                    this.D1_SALUT = new YGCEnum<Salutation>(1) { DataString = RT02Entry.Substring(97) };
                    this.D1_ALIAS = new YGCString(25) { DataString = RT02Entry.Substring(98) };
                    this.D1_STREET = new YGCString(25) { DataString = RT02Entry.Substring(123) };
                    this.D1_CS = new YGCString(23) { DataString = RT02Entry.Substring(148) };
                    this.D1_ZIP = new YGCString(9) { DataString = RT02Entry.Substring(171) };
                    this.D1_PHONE = new YGCString(12) { DataString = RT02Entry.Substring(180) };
                    this.D1_FAX = new YGCString(12) { DataString = RT02Entry.Substring(192) };
                    this.D1_SSN = new YGCString(15) { DataString = RT02Entry.Substring(204) };
                    this.RFILE = new YGCString(8) { DataString = RT02Entry.Substring(219) };
                    this.D1_DOB = new YGCDate() { DataString = RT02Entry.Substring(227) };
                    this.D1_DL = new YGCString(17) { DataString = RT02Entry.Substring(235) };
                    this.D1_STATE = new YGCString(3) { DataString = RT02Entry.Substring(252) };
                    this.D1_MAIL = new YGCBool(1, "Y") { DataString = RT02Entry.Substring(255) };
                    this.SERVICE_D = new YGCDate() { DataString = RT02Entry.Substring(256) };
                    this.ANSWER_DUE_D = new YGCDate() { DataString = RT02Entry.Substring(264) };
                    this.ANSWER_FILE_D = new YGCDate() { DataString = RT02Entry.Substring(272) };
                    this.DEFAULT_D = new YGCDate() { DataString = RT02Entry.Substring(280) };
                    this.TRIAL_D = new YGCDate() { DataString = RT02Entry.Substring(288) };
                    this.HEARING_D = new YGCDate() { DataString = RT02Entry.Substring(296) };
                    this.LIEN_D = new YGCDate() { DataString = RT02Entry.Substring(304) };
                    this.GARN_D = new YGCDate() { DataString = RT02Entry.Substring(312) };
                    this.SERVICE_TYPE = new YGCString(4) { DataString = RT02Entry.Substring(320) };
                    this.D1_STREET2 = new YGCString(25) { DataString = RT02Entry.Substring(324) };
                    this.D1_CITY = new YGCString(30) { DataString = RT02Entry.Substring(349) };
                    this.D1_CELL = new YGCString(12) { DataString = RT02Entry.Substring(379) };
                    this.SCORE_FICO = new YGCNumber(3) { DataString = RT02Entry.Substring(391) };
                    this.SCORE_COLLECT = new YGCNumber(3) { DataString = RT02Entry.Substring(394) };
                    this.SCORE_OTHER = new YGCNumber(3) { DataString = RT02Entry.Substring(397) };
                    this.D1_CNTRY = new YGCString(3) { DataString = RT02Entry.Length > 400 ? RT02Entry.Substring(400, 3) : "" };
                    this.D1_STREET_LONG = new YGCString(50) { DataString = RT02Entry.Length > 403 ? RT02Entry.Substring(403, 50) : "" };
                    this.D1_STREET2_LONG = new YGCString(50) { DataString = RT02Entry.Length > 453 ? RT02Entry.Substring(453, 50) : "" };
                    this.FIRSTNAME = new YGCString(30) { DataString = RT02Entry.Length > 503 ? RT02Entry.Substring(503) : "" };
                    this.LASTNAME = new YGCString(30) { DataString = RT02Entry.Length > 533 ? RT02Entry.Substring(533) : "" };
                    this.SCOREINTERNAL = new YGCString(5) { DataString = RT02Entry.Length > 563 ? RT02Entry.Substring(563) : "" };
                    this.STAGE = new YGCString(10) { DataString = RT02Entry.Length > 568 ? RT02Entry.Substring(568) : "" };
                }
                catch
                {
                    if (this.D1_NAME == null) this.D1_NAME = new YGCString(30);
                    if (this.D1_SALUT == null) this.D1_SALUT = new YGCEnum<Salutation>(1);
                    if (this.D1_ALIAS == null) this.D1_ALIAS = new YGCString(25);
                    if (this.D1_STREET == null) this.D1_STREET = new YGCString(25);
                    if (this.D1_CS == null) this.D1_CS = new YGCString(23);
                    if (this.D1_ZIP == null) this.D1_ZIP = new YGCString(9);
                    if (this.D1_PHONE == null) this.D1_PHONE = new YGCString(12);
                    if (this.D1_FAX == null) this.D1_FAX = new YGCString(12);
                    if (this.D1_SSN == null) this.D1_SSN = new YGCString(15);
                    if (this.RFILE == null) this.RFILE = new YGCString(8);
                    if (this.D1_DOB == null) this.D1_DOB = new YGCDate();
                    if (this.D1_DL == null) this.D1_DL = new YGCString(17);
                    if (this.D1_STATE == null) this.D1_STATE = new YGCString(3);
                    if (this.D1_MAIL == null) this.D1_MAIL = new YGCBool(1, "Y");
                    if (this.SERVICE_D == null) this.SERVICE_D = new YGCDate();
                    if (this.ANSWER_DUE_D == null) this.ANSWER_DUE_D = new YGCDate();
                    if (this.ANSWER_FILE_D == null) this.ANSWER_FILE_D = new YGCDate();
                    if (this.DEFAULT_D == null) this.DEFAULT_D = new YGCDate();
                    if (this.TRIAL_D == null) this.TRIAL_D = new YGCDate();
                    if (this.HEARING_D == null) this.HEARING_D = new YGCDate();
                    if (this.LIEN_D == null) this.LIEN_D = new YGCDate();
                    if (this.GARN_D == null) this.GARN_D = new YGCDate();
                    if (this.SERVICE_TYPE == null) this.SERVICE_TYPE = new YGCString(4);
                    if (this.D1_STREET2 == null) this.D1_STREET2 = new YGCString(25);
                    if (this.D1_CITY == null) this.D1_CITY = new YGCString(30);
                    if (this.D1_CELL == null) this.D1_CELL = new YGCString(12);
                    if (this.SCORE_FICO == null) this.SCORE_FICO = new YGCNumber(3);
                    if (this.SCORE_COLLECT == null) this.SCORE_COLLECT = new YGCNumber(3);
                    if (this.SCORE_OTHER == null) this.SCORE_OTHER = new YGCNumber(3);
                    if (this.D1_CNTRY == null) this.D1_CNTRY = new YGCString(3);
                    if (this.D1_STREET_LONG == null) this.D1_STREET_LONG = new YGCString(50);
                    if (this.D1_STREET2_LONG == null) this.D1_STREET2_LONG = new YGCString(50);
                    if (this.FIRSTNAME == null) this.FIRSTNAME = new YGCString(30);
                    if (this.LASTNAME == null) this.LASTNAME = new YGCString(30);
                    if (this.SCOREINTERNAL == null) this.SCOREINTERNAL = new YGCString(5);
                    if (this.STAGE == null) this.STAGE = new YGCString(10);
                }
            }

            public override Type GetType() { return typeof(RecordType02); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}",
                    base.ToString(),
                    this.D1_NAME,
                    this.D1_SALUT,
                    this.D1_ALIAS,
                    this.D1_STREET,
                    this.D1_CS,
                    this.D1_ZIP,
                    this.D1_PHONE,
                    this.D1_FAX,
                    this.D1_SSN,
                    this.RFILE,
                    this.D1_DOB,
                    this.D1_DL,
                    this.D1_STATE,
                    this.D1_MAIL,
                    this.SERVICE_D,
                    this.ANSWER_DUE_D,
                    this.ANSWER_FILE_D,
                    this.DEFAULT_D,
                    this.TRIAL_D,
                    this.HEARING_D,
                    this.LIEN_D,
                    this.GARN_D,
                    this.SERVICE_TYPE,
                    this.D1_STREET2,
                    this.D1_CITY,
                    this.D1_CELL,
                    this.SCORE_FICO,
                    this.SCORE_COLLECT,
                    this.SCORE_OTHER,
                    this.D1_CNTRY,
                    this.D1_STREET_LONG,
                    this.D1_STREET2_LONG,
                    this.FIRSTNAME,
                    this.LASTNAME,
                    this.SCOREINTERNAL,
                    this.STAGE);
            }
        }
        #endregion

        #region Record Type 03
        /// <summary>
        /// Record Type 03 - 2nd and 3rd Debtor Information (Sender to Receiver)
        /// <para>If more than one name appears on the original contract between creditor and debtor, they are identified here. This can also be used for a co-signer. Record type value = 03. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 03, 33 or 53, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
        /// </summary>
        public class RecordType03 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the name of the second debtor or co-signer. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D2_NAME { get; private set; }
            /// <summary>
            /// This is the second debtor's street address. If there is no D2_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
            /// </summary>
            public YGCString D2_STREET { get; private set; }
            /// <summary>
            /// This is the second debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D2_CSZ field but no D2_CNTRY field populated in this record, the country will become empty on YouveGotReports.
            /// </summary>
            public YGCString D2_CSZ { get; private set; }
            /// <summary>
            /// This is the second debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D2_PHONE { get; private set; }
            /// <summary>
            /// This is the second debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public YGCString D2_SSN { get; private set; }
            /// <summary>
            /// This is the name of the third debtor or co-signer. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D3_NAME { get; private set; }
            /// <summary>
            /// This is the third debtor's street address. If there is no D3_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
            /// </summary>
            public YGCString D3_STREET { get; private set; }
            /// <summary>
            /// This is the third debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D3_CSZ field but no D3_CNTRY field populated in this record, the country will become empty on YouveGotReports.
            /// </summary>
            public YGCString D3_CSZ { get; private set; }
            /// <summary>
            /// This is the third debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D3_PHONE { get; private set; }
            /// <summary>
            /// This is the third debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public YGCString D3_SSN { get; private set; }
            /// <summary>
            /// This is the second debtor's date of birth.
            /// </summary>
            public YGCDate D2_DOB { get; private set; }
            /// <summary>
            /// This is the third debtor's date of birth.
            /// </summary>
            public YGCDate D3_DOB { get; private set; }
            /// <summary>
            /// This is the second debtor's driver's license number.
            /// </summary>
            public YGCString D2_DL { get; private set; }
            /// <summary>
            /// This is the third debtor's driver's license number.
            /// </summary>
            public YGCString D3_DL { get; private set; }
            /// <summary>
            /// This is the standard code for the second debtor's country. If there is a D2_CNTRY field but no D2_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
            /// </summary>
            public YGCString D2_CNTRY { get; private set; }
            /// <summary>
            /// This is the standard code for the third debtor's country. If there is a D3_CNTRY field but no D3_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
            /// </summary>
            public YGCString D3_CNTRY { get; private set; }
            /// <summary>
            /// This field serves to deliver the entire 2nd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D2_STREET in case your receivers cannot yet accept this newer field. If there is a D2_STREET_LONG field but no D2_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D2_STREET_LONG { get; private set; }
            /// <summary>
            /// This is an overflow field for D2_STREET_LONG. If there is a D2_STREET2_LONG field but no D2_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D2_STREET2_LONG { get; private set; }
            /// <summary>
            /// This field serves to deliver the entire 3rd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D3_STREET in case your receivers cannot yet accept this newer field. If there is a D3_STREET_LONG field but no D3_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D3_STREET_LONG { get; private set; }
            /// <summary>
            /// This is an overflow field for D3_STREET_LONG. If there is a D3_STREET2_LONG field but no D3_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D3_STREET2_LONG { get; private set; }
            /// <summary>
            /// This is for the 2nd debtor's first name. Include middle name here if available. Use this with D2_LNAME instead of D2_NAME if your collection software enables it.
            /// </summary>
            public YGCString D2_FNAME { get; private set; }
            /// <summary>
            /// This is for the 2nd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D2_FNAME instead of D2_NAME if your collection software enables it.
            /// </summary>
            public YGCString D2_LNAME { get; private set; }
            /// <summary>
            /// This is for the 2nd debtor's city. Use this with D2_STATE and D2_ZIP instead of D2_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D2_CITY { get; private set; }
            /// <summary>
            /// This is for the 2nd debtor's state. Use the standard abbreviation. Use this with D2_CITY and D2_ZIP instead of D2_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D2_STATE { get; private set; }
            /// <summary>
            /// This is for the 2nd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D2_CITY and D2_STATE instead of D2_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D2_ZIP { get; private set; }
            /// <summary>
            /// This is for the 3rd debtor's first name. Include middle name here if available. Use this with D3_LNAME instead of D3_NAME if your collection software enables it.
            /// </summary>
            public YGCString D3_FNAME { get; private set; }
            /// <summary>
            /// This is for the 3rd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D3_FNAME instead of D3_NAME if your collection software enables it.
            /// </summary>
            public YGCString D3_LNAME { get; private set; }
            /// <summary>
            /// This is for the 3rd debtor's city. Use this with D3_STATE and D3_ZIP instead of D3_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D3_CITY { get; private set; }
            /// <summary>
            /// This is for the 3rd debtor's state. Use the standard abbreviation. Use this with D3_CITY and D3_ZIP instead of D3_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D3_STATE { get; private set; }
            /// <summary>
            /// This is for the 3rd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D3_CITY and D3_STATE instead of D3_CSZ if your collection software enables it.
            /// </summary>
            public YGCString D3_ZIP { get; private set; }
            #endregion

            protected RecordType03(int Record) : base(Record)
            {
                this.D2_NAME = new YGCString(25);
                this.D2_STREET = new YGCString(25);
                this.D2_CSZ = new YGCString(25);
                this.D2_PHONE = new YGCString(15);
                this.D2_SSN = new YGCString(15);
                this.D3_NAME = new YGCString(25);
                this.D3_STREET = new YGCString(25);
                this.D3_CSZ = new YGCString(25);
                this.D3_PHONE = new YGCString(15);
                this.D3_SSN = new YGCString(15);
                this.D2_DOB = new YGCDate();
                this.D3_DOB = new YGCDate();
                this.D2_DL = new YGCString(17);
                this.D3_DL = new YGCString(17);
                this.D2_CNTRY = new YGCString(3);
                this.D3_CNTRY = new YGCString(3);
                this.D2_STREET_LONG = new YGCString(50);
                this.D2_STREET2_LONG = new YGCString(50);
                this.D3_STREET_LONG = new YGCString(50);
                this.D3_STREET2_LONG = new YGCString(50);
                this.D2_FNAME = new YGCString(30);
                this.D2_LNAME = new YGCString(30);
                this.D2_CITY = new YGCString(30);
                this.D2_STATE = new YGCString(3);
                this.D2_ZIP = new YGCString(9);
                this.D3_FNAME = new YGCString(30);
                this.D3_LNAME = new YGCString(30);
                this.D3_CITY = new YGCString(30);
                this.D3_STATE = new YGCString(3);
                this.D3_ZIP = new YGCString(9);
            }
            public RecordType03() : this(3) { }
            public RecordType03(string RT03Entry) : base(RT03Entry)
            {
                try
                {
                    this.D2_NAME = new YGCString(25) { DataString = RT03Entry.Substring(67) };
                    this.D2_STREET = new YGCString(25) { DataString = RT03Entry.Substring(92) };
                    this.D2_CSZ = new YGCString(25) { DataString = RT03Entry.Substring(117) };
                    this.D2_PHONE = new YGCString(15) { DataString = RT03Entry.Substring(142) };
                    this.D2_SSN = new YGCString(15) { DataString = RT03Entry.Substring(157) };
                    this.D3_NAME = new YGCString(25) { DataString = RT03Entry.Substring(172) };
                    this.D3_STREET = new YGCString(25) { DataString = RT03Entry.Substring(197) };
                    this.D3_CSZ = new YGCString(25) { DataString = RT03Entry.Substring(222) };
                    this.D3_PHONE = new YGCString(15) { DataString = RT03Entry.Substring(247) };
                    this.D3_SSN = new YGCString(15) { DataString = RT03Entry.Substring(262) };
                    this.D2_DOB = new YGCDate() { DataString = RT03Entry.Substring(277) };
                    this.D3_DOB = new YGCDate() { DataString = RT03Entry.Substring(285) };
                    this.D2_DL = new YGCString(17) { DataString = RT03Entry.Substring(293) };
                    this.D3_DL = new YGCString(17) { DataString = RT03Entry.Substring(310) };
                    this.D2_CNTRY = new YGCString(3) { DataString = RT03Entry.Length > 327 ? RT03Entry.Substring(327) : "" };
                    this.D3_CNTRY = new YGCString(3) { DataString = RT03Entry.Length > 330 ? RT03Entry.Substring(330) : "" };
                    this.D2_STREET_LONG = new YGCString(50) { DataString = RT03Entry.Length > 333 ? RT03Entry.Substring(333) : "" };
                    this.D2_STREET2_LONG = new YGCString(50) { DataString = RT03Entry.Length > 383 ? RT03Entry.Substring(383) : "" };
                    this.D3_STREET_LONG = new YGCString(50) { DataString = RT03Entry.Length > 433 ? RT03Entry.Substring(433) : "" };
                    this.D3_STREET2_LONG = new YGCString(50) { DataString = RT03Entry.Length > 483 ? RT03Entry.Substring(483) : "" };
                    this.D2_FNAME = new YGCString(30) { DataString = RT03Entry.Length > 533 ? RT03Entry.Substring(533) : "" };
                    this.D2_LNAME = new YGCString(30) { DataString = RT03Entry.Length > 563 ? RT03Entry.Substring(563) : "" };
                    this.D2_CITY = new YGCString(30) { DataString = RT03Entry.Length > 593 ? RT03Entry.Substring(593) : "" };
                    this.D2_STATE = new YGCString(3) { DataString = RT03Entry.Length > 623 ? RT03Entry.Substring(623) : "" };
                    this.D2_ZIP = new YGCString(9) { DataString = RT03Entry.Length > 626 ? RT03Entry.Substring(626) : "" };
                    this.D3_FNAME = new YGCString(30) { DataString = RT03Entry.Length > 635 ? RT03Entry.Substring(635) : "" };
                    this.D3_LNAME = new YGCString(30) { DataString = RT03Entry.Length > 665 ? RT03Entry.Substring(665) : "" };
                    this.D3_CITY = new YGCString(30) { DataString = RT03Entry.Length > 695 ? RT03Entry.Substring(695) : "" };
                    this.D3_STATE = new YGCString(3) { DataString = RT03Entry.Length > 725 ? RT03Entry.Substring(725) : "" };
                    this.D3_ZIP = new YGCString(9) { DataString = RT03Entry.Length > 728 ? RT03Entry.Substring(728) : "" };
                }
                catch
                {
                    if (this.D2_NAME == null) this.D2_NAME = new YGCString(25);
                    if (this.D2_STREET == null) this.D2_STREET = new YGCString(25);
                    if (this.D2_CSZ == null) this.D2_CSZ = new YGCString(25);
                    if (this.D2_PHONE == null) this.D2_PHONE = new YGCString(15);
                    if (this.D2_SSN == null) this.D2_SSN = new YGCString(15);
                    if (this.D3_NAME == null) this.D3_NAME = new YGCString(25);
                    if (this.D3_STREET == null) this.D3_STREET = new YGCString(25);
                    if (this.D3_CSZ == null) this.D3_CSZ = new YGCString(25);
                    if (this.D3_PHONE == null) this.D3_PHONE = new YGCString(15);
                    if (this.D3_SSN == null) this.D3_SSN = new YGCString(15);
                    if (this.D2_DOB == null) this.D2_DOB = new YGCDate();
                    if (this.D3_DOB == null) this.D3_DOB = new YGCDate();
                    if (this.D2_DL == null) this.D2_DL = new YGCString(17);
                    if (this.D3_DL == null) this.D3_DL = new YGCString(17);
                    if (this.D2_CNTRY == null) this.D2_CNTRY = new YGCString(3);
                    if (this.D3_CNTRY == null) this.D3_CNTRY = new YGCString(3);
                    if (this.D2_STREET_LONG == null) this.D2_STREET_LONG = new YGCString(50);
                    if (this.D2_STREET2_LONG == null) this.D2_STREET2_LONG = new YGCString(50);
                    if (this.D3_STREET_LONG == null) this.D3_STREET_LONG = new YGCString(50);
                    if (this.D3_STREET2_LONG == null) this.D3_STREET2_LONG = new YGCString(50);
                    if (this.D2_FNAME == null) this.D2_FNAME = new YGCString(30);
                    if (this.D2_LNAME == null) this.D2_LNAME = new YGCString(30);
                    if (this.D2_CITY == null) this.D2_CITY = new YGCString(30);
                    if (this.D2_STATE == null) this.D2_STATE = new YGCString(3);
                    if (this.D2_ZIP == null) this.D2_ZIP = new YGCString(9);
                    if (this.D3_FNAME == null) this.D3_FNAME = new YGCString(30);
                    if (this.D3_LNAME == null) this.D3_LNAME = new YGCString(30);
                    if (this.D3_CITY == null) this.D3_CITY = new YGCString(30);
                    if (this.D3_STATE == null) this.D3_STATE = new YGCString(3);
                    if (this.D3_ZIP == null) this.D3_ZIP = new YGCString(9);
                }
            }

            public override Type GetType() { return typeof(RecordType03); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}",
                    base.ToString(),
                    this.D2_NAME,
                    this.D2_STREET,
                    this.D2_CSZ,
                    this.D2_PHONE,
                    this.D2_SSN,
                    this.D3_NAME,
                    this.D3_STREET,
                    this.D3_CSZ,
                    this.D3_PHONE,
                    this.D3_SSN,
                    this.D2_DOB,
                    this.D3_DOB,
                    this.D2_DL,
                    this.D3_DL,
                    this.D2_CNTRY,
                    this.D3_CNTRY,
                    this.D2_STREET_LONG,
                    this.D2_STREET2_LONG,
                    this.D3_STREET_LONG,
                    this.D3_STREET2_LONG,
                    this.D2_FNAME,
                    this.D2_LNAME,
                    this.D2_CITY,
                    this.D2_STATE,
                    this.D2_ZIP,
                    this.D3_FNAME,
                    this.D3_LNAME,
                    this.D3_CITY,
                    this.D3_STATE,
                    this.D3_ZIP);
            }
        }
        #endregion

        #region Record Type 04
        /// <summary>
        /// Record Type 04 - Employment Information (Sender to Receiver)
        /// <para>This record holds the debtors' employment information. You can submit a record for each of 3 distinct debtors for the same account. Value = 04.</para>
        /// </summary>
        public class RecordType04 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Company name of debtor's employer.
            /// </summary>
            public YGCString Employer_Name { get; private set; }
            /// <summary>
            /// This is the employer's street address.
            /// </summary>
            public YGCString Employer_Street { get; private set; }
            /// <summary>
            /// This is the employer's PO Box number.
            /// </summary>
            public YGCString Employer_PO_Box { get; private set; }
            /// <summary>
            /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
            /// </summary>
            public YGCString Employer_City_State { get; private set; }
            /// <summary>
            /// This is the employer's zip code. It can accommodate the four-digit extension with the hyphen.
            /// </summary>
            public YGCString Employer_Zip { get; private set; }
            /// <summary>
            /// This is the employer's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString Employer_Phone { get; private set; }
            /// <summary>
            /// This is the employer's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString Employer_Fax { get; private set; }
            /// <summary>
            /// Department or personnel for correspondence to employer.
            /// </summary>
            public YGCString Employer_ATTN { get; private set; }
            /// <summary>
            /// This is the contact person at the payroll department.
            /// </summary>
            public YGCString Employer_Payroll { get; private set; }
            /// <summary>
            /// This is used to distinguish up to 3 employment records. Values are 1, 2 or 3. 
            /// <para>(Ex: The record 04 for the primary debtor can have EMP_NO = 1 and the record 04 for the co-signer can have EMP_NO = 2.)</para>
            /// <para>If a record 04 is sent with the same EMP_NO as a previous record 04 for the same account, the second record will overwrite the first.</para>
            /// </summary>
            public YGCNumber Debtor_Number { get; private set; }
            /// <summary>
            /// This is the debtor name. There is no format restriction.
            /// </summary>
            public YGCString Employee_Name { get; private set; }
            /// <summary>
            /// Income Earned Per Pay Frequency Period
            /// </summary>
            public YGCDecimal Employee_Income { get; private set; }
            /// <summary>
            /// Freqeuncy with Which the Listed Income is Distributed
            /// </summary>
            public YGCEnum<IncomeFrequency, IncomeFrequencyValue> Employee_Frequency { get; private set; }
            /// <summary>
            /// This is the employee's title at the given employer.
            /// </summary>
            public YGCString Employee_Position { get; private set; }
            /// <summary>
            /// Number of Months with Employer
            /// </summary>
            public YGCNumber Employee_Tenure { get; private set; }
            /// <summary>
            /// This is the standard code for the employee's country.
            /// </summary>
            public YGCString Employer_Country { get; private set; }
            #endregion

            protected RecordType04(int Record) : base(Record)
            {
                this.Employer_Name = new YGCString(40);
                this.Employer_Street = new YGCString(40);
                this.Employer_PO_Box = new YGCString(40);
                this.Employer_City_State = new YGCString(30);
                this.Employer_Zip = new YGCString(10);
                this.Employer_Phone = new YGCString(15);
                this.Employer_Fax = new YGCString(15);
                this.Employer_ATTN = new YGCString(40);
                this.Employer_Payroll = new YGCString(40);
                this.Debtor_Number = new YGCNumber(3);
                this.Employee_Name = new YGCString(30);
                this.Employee_Income = new YGCDecimal(14, 2);
                this.Employee_Frequency = new YGCEnum<IncomeFrequency, IncomeFrequencyValue>(1);
                this.Employee_Position = new YGCString(20);
                this.Employee_Tenure = new YGCNumber(3);
                this.Employer_Country = new YGCString(3);
            }
            public RecordType04() : this(4) { }
            public RecordType04(string RT04Entry) : base(RT04Entry)
            {
                try
                {
                    this.Employer_Name = new YGCString(40) { DataString = RT04Entry.Substring(67) };
                    this.Employer_Street = new YGCString(40) { DataString = RT04Entry.Substring(107) };
                    this.Employer_PO_Box = new YGCString(40) { DataString = RT04Entry.Substring(147) };
                    this.Employer_City_State = new YGCString(30) { DataString = RT04Entry.Substring(187) };
                    this.Employer_Zip = new YGCString(10) { DataString = RT04Entry.Substring(217) };
                    this.Employer_Phone = new YGCString(15) { DataString = RT04Entry.Substring(227) };
                    this.Employer_Fax = new YGCString(15) { DataString = RT04Entry.Substring(242) };
                    this.Employer_ATTN = new YGCString(40) { DataString = RT04Entry.Substring(257) };
                    this.Employer_Payroll = new YGCString(40) { DataString = RT04Entry.Substring(297) };
                    this.Debtor_Number = new YGCNumber(3) { DataString = RT04Entry.Substring(337) };
                    this.Employee_Name = new YGCString(30) { DataString = RT04Entry.Substring(340) };
                    this.Employee_Income = new YGCDecimal(14, 2) { DataString = RT04Entry.Substring(370) };
                    this.Employee_Frequency = new YGCEnum<IncomeFrequency, IncomeFrequencyValue>(1) { DataString = RT04Entry.Substring(384) };
                    this.Employee_Position = new YGCString(20) { DataString = RT04Entry.Substring(385) };
                    this.Employee_Tenure = new YGCNumber(3) { DataString = RT04Entry.Substring(405) };
                    this.Employer_Country = new YGCString(3) { DataString = RT04Entry.Substring(408) };
                }
                catch
                {
                    if (this.Employer_Name == null) this.Employer_Name = new YGCString(40);
                    if (this.Employer_Street == null) this.Employer_Street = new YGCString(40);
                    if (this.Employer_PO_Box == null) this.Employer_PO_Box = new YGCString(40);
                    if (this.Employer_City_State == null) this.Employer_City_State = new YGCString(30);
                    if (this.Employer_Zip == null) this.Employer_Zip = new YGCString(10);
                    if (this.Employer_Phone == null) this.Employer_Phone = new YGCString(15);
                    if (this.Employer_Fax == null) this.Employer_Fax = new YGCString(15);
                    if (this.Employer_ATTN == null) this.Employer_ATTN = new YGCString(40);
                    if (this.Employer_Payroll == null) this.Employer_Payroll = new YGCString(40);
                    if (this.Debtor_Number == null) this.Debtor_Number = new YGCNumber(3);
                    if (this.Employee_Name == null) this.Employee_Name = new YGCString(30);
                    if (this.Employee_Income == null) this.Employee_Income = new YGCDecimal(14, 2);
                    if (this.Employee_Frequency == null) this.Employee_Frequency = new YGCEnum<IncomeFrequency, IncomeFrequencyValue>(1);
                    if (this.Employee_Position == null) this.Employee_Position = new YGCString(20);
                    if (this.Employee_Tenure == null) this.Employee_Tenure = new YGCNumber(3);
                    if (this.Employer_Country == null) this.Employer_Country = new YGCString(3);
                }
            }

            public override Type GetType() { return typeof(RecordType04); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}",
                    base.ToString(),
                    this.Employer_Name,
                    this.Employer_Street,
                    this.Employer_PO_Box,
                    this.Employer_City_State,
                    this.Employer_Zip,
                    this.Employer_Phone,
                    this.Employer_Fax,
                    this.Employer_ATTN,
                    this.Employer_Payroll,
                    this.Debtor_Number,
                    this.Employee_Name,
                    this.Employee_Income,
                    this.Employee_Frequency,
                    this.Employee_Position,
                    this.Employee_Tenure,
                    this.Employer_Country);
            }
        }
        #endregion

        #region Record Type 05
        /// <summary>
        /// Record Type 05 - Bank/Asset Information (Sender to Receiver)
        /// <para>This record holds any bank account information and non-auto or non-real estate asset information for the debtor. You can submit a record for each of 3 distinct bank accounts for the same debtor. Value = 05.</para>
        /// </summary>
        public class RecordType05 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Currently a free field
            /// </summary>
            public YGCString FILLER { get; private set; }
            /// <summary>
            /// This is the name of the debtor's bank.
            /// </summary>
            public YGCString BANK_NAME { get; private set; }
            /// <summary>
            /// This is the debtor's bank street address.
            /// </summary>
            public YGCString BANK_STREET { get; private set; }
            /// <summary>
            /// This is the bank's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
            /// </summary>
            public YGCString BANK_CSZ { get; private set; }
            /// <summary>
            /// This is the contact name for the bank.
            /// </summary>
            public YGCString BANK_ATTN { get; private set; }
            /// <summary>
            /// This is the bank's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString BANK_PHONE { get; private set; }
            /// <summary>
            /// This is the bank's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString BANK_FAX { get; private set; }
            /// <summary>
            /// This is the debtor's bank account number.
            /// </summary>
            public YGCString BANK_ACCT { get; private set; }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public YGCString MISC_ASSET1 { get; private set; }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public YGCString MISC_ASSET2 { get; private set; }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public YGCString MISC_ASSET3 { get; private set; }
            /// <summary>
            /// This field holds a relevant phone number relating to the miscellaneous asset. If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public YGCString MISC_PHONE { get; private set; }
            /// <summary>
            /// This is used to distinguish up to 3 bank records. Values are 1, 2 or 3. 
            /// <para>(Ex: The record 05 for the debtor's checking account can have BANK_NO = 1 and the record 05 for the debtor's savings account can have BANK_NO = 2.)</para>
            /// <para>If a record 05 is sent with the same BANK_NO as a previous record 05 for the same account, the second record will overwrite the first. If you have a miscellaneous asset described in this record, this value must be 1.</para>
            /// </summary>
            public YGCNumber BANK_NO { get; private set; }
            /// <summary>
            /// This is the standard code for the bank's country.
            /// </summary>
            public YGCString BANK_CNTRY { get; private set; }
            /// <summary>
            /// This is the ABA routing number for the bank account. You may use this field in addition to or instead of the BANK_ACCT field.
            /// </summary>
            public YGCString ROUTINGNUMBER { get; private set; }
            #endregion

            protected RecordType05(int Record) : base(Record)
            {
                this.FILLER = new YGCString(20);
                this.BANK_NAME = new YGCString(30);
                this.BANK_STREET = new YGCString(30);
                this.BANK_CSZ = new YGCString(30);
                this.BANK_ATTN = new YGCString(30);
                this.BANK_PHONE = new YGCString(15);
                this.BANK_FAX = new YGCString(15);
                this.BANK_ACCT = new YGCString(17);
                this.MISC_ASSET1 = new YGCString(25);
                this.MISC_ASSET2 = new YGCString(25);
                this.MISC_ASSET3 = new YGCString(25);
                this.MISC_PHONE = new YGCString(15);
                this.BANK_NO = new YGCNumber(3);
                this.BANK_CNTRY = new YGCString(3);
                this.ROUTINGNUMBER = new YGCString(15);
            }
            public RecordType05() : this(5) { }
            public RecordType05(string RT05Entry) : base(RT05Entry)
            {
                try
                {
                    this.FILLER = new YGCString(20) { DataString = RT05Entry.Substring(67) };
                    this.BANK_NAME = new YGCString(30) { DataString = RT05Entry.Substring(87) };
                    this.BANK_STREET = new YGCString(30) { DataString = RT05Entry.Substring(117) };
                    this.BANK_CSZ = new YGCString(30) { DataString = RT05Entry.Substring(147) };
                    this.BANK_ATTN = new YGCString(30) { DataString = RT05Entry.Substring(177) };
                    this.BANK_PHONE = new YGCString(15) { DataString = RT05Entry.Substring(207) };
                    this.BANK_FAX = new YGCString(15) { DataString = RT05Entry.Substring(222) };
                    this.BANK_ACCT = new YGCString(17) { DataString = RT05Entry.Substring(237) };
                    this.MISC_ASSET1 = new YGCString(25) { DataString = RT05Entry.Substring(254) };
                    this.MISC_ASSET2 = new YGCString(25) { DataString = RT05Entry.Substring(279) };
                    this.MISC_ASSET3 = new YGCString(25) { DataString = RT05Entry.Substring(304) };
                    this.MISC_PHONE = new YGCString(15) { DataString = RT05Entry.Substring(329) };
                    this.BANK_NO = new YGCNumber(3) { DataString = RT05Entry.Substring(344) };
                    this.BANK_CNTRY = new YGCString(3) { DataString = RT05Entry.Substring(347) };
                    this.ROUTINGNUMBER = new YGCString(15) { DataString = RT05Entry.Substring(350) };
                }
                catch
                {
                    if (this.FILLER == null) this.FILLER = new YGCString(20);
                    if (this.BANK_NAME == null) this.BANK_NAME = new YGCString(30);
                    if (this.BANK_STREET == null) this.BANK_STREET = new YGCString(30);
                    if (this.BANK_CSZ == null) this.BANK_CSZ = new YGCString(30);
                    if (this.BANK_ATTN == null) this.BANK_ATTN = new YGCString(30);
                    if (this.BANK_PHONE == null) this.BANK_PHONE = new YGCString(15);
                    if (this.BANK_FAX == null) this.BANK_FAX = new YGCString(15);
                    if (this.BANK_ACCT == null) this.BANK_ACCT = new YGCString(17);
                    if (this.MISC_ASSET1 == null) this.MISC_ASSET1 = new YGCString(25);
                    if (this.MISC_ASSET2 == null) this.MISC_ASSET2 = new YGCString(25);
                    if (this.MISC_ASSET3 == null) this.MISC_ASSET3 = new YGCString(25);
                    if (this.MISC_PHONE == null) this.MISC_PHONE = new YGCString(15);
                    if (this.BANK_NO == null) this.BANK_NO = new YGCNumber(3);
                    if (this.BANK_CNTRY == null) this.BANK_CNTRY = new YGCString(3);
                    if (this.ROUTINGNUMBER == null) this.ROUTINGNUMBER = new YGCString(15);
                }
            }

            public override Type GetType() { return typeof(RecordType05); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                    base.ToString(),
                    this.FILLER,
                    this.BANK_NAME,
                    this.BANK_STREET,
                    this.BANK_CSZ,
                    this.BANK_ATTN,
                    this.BANK_PHONE,
                    this.BANK_FAX,
                    this.BANK_ACCT,
                    this.MISC_ASSET1,
                    this.MISC_ASSET2,
                    this.MISC_ASSET3,
                    this.MISC_PHONE,
                    this.BANK_NO,
                    this.BANK_CNTRY,
                    this.ROUTINGNUMBER);
            }
        }
        #endregion

        #region Record Type 06
        /// <summary>
        /// Record Type 06 - Misc Information (Sender to Receiver)
        /// <para>This record is for debtor attorney information and any miscellaneous information that no other record in the DataStandard accommodates. You can submit a record for each of 3 debtor attorneys for the same account. Value = 06.</para>
        /// </summary>
        public class RecordType06 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is for the name of the debtor's attorney. There is no format restriction.
            /// </summary>
            public YGCString ADVA_NAME { get; private set; }
            /// <summary>
            /// This is the name of the debtor's attorney's firm.
            /// </summary>
            public YGCString ADVA_FIRM { get; private set; }
            /// <summary>
            /// This is an overflow field for ADVA_FIRM.
            /// </summary>
            public YGCString ADVA_FIRM2 { get; private set; }
            /// <summary>
            /// This is the attorney's street address.
            /// </summary>
            public YGCString ADVA_STREET { get; private set; }
            /// <summary>
            /// This is the attorney's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
            /// </summary>
            public YGCString ADVA_CSZ { get; private set; }
            /// <summary>
            /// This is the attorney's preferred salutation in correspondence, such as "Dear Attorney Lewis:"
            /// </summary>
            public YGCString ADVA_SALUT { get; private set; }
            /// <summary>
            /// This is the attorney's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString ADVA_PHONE { get; private set; }
            /// <summary>
            /// This is the attorney's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString ADVA_FAX { get; private set; }
            /// <summary>
            /// This is the internal file number at the debtor's attorney's firm for this account.
            /// </summary>
            public YGCString ADVA_FILENO { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCDate MISC_DATE1 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCDate MISC_DATE2 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCDecimal MISC_AMT1 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCDecimal MISC_AMT2 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCString MISC_COMM1 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCString MISC_COMM2 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCString MISC_COMM3 { get; private set; }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public YGCString MISC_COMM4 { get; private set; }
            /// <summary>
            /// This is used to distinguish up to 3 debtor attorney records. Values are 1, 2 or 3.
            /// <para>(Ex: The record 06 for the debtor's primary attorney can have ADVA_NUM = 1 and the record 06 for the debtor's secondary attorney can have ADVA_NUM = 2.)</para>
            /// <para>If a record 06 is sent with the same ADVA_NUM as a previous record 06 for the same account, the second record will overwrite the first. If you have miscellaneous information described in this record, this value must be 1.</para>
            /// </summary>
            public YGCNumber ADVA_NUM { get; private set; }
            /// <summary>
            /// This is the standard code for the debtor attorney's country.
            /// </summary>
            public YGCString ADVA_CNTRY { get; private set; }
            #endregion

            protected RecordType06(int Record) : base(Record)
            {
                this.ADVA_NAME = new YGCString(30);
                this.ADVA_FIRM = new YGCString(30);
                this.ADVA_FIRM2 = new YGCString(30);
                this.ADVA_STREET = new YGCString(30);
                this.ADVA_CSZ = new YGCString(30);
                this.ADVA_SALUT = new YGCString(30);
                this.ADVA_PHONE = new YGCString(30);
                this.ADVA_FAX = new YGCString(15);
                this.ADVA_FILENO = new YGCString(12);
                this.MISC_DATE1 = new YGCDate();
                this.MISC_DATE2 = new YGCDate();
                this.MISC_AMT1 = new YGCDecimal(14, 2);
                this.MISC_AMT2 = new YGCDecimal(14, 2);
                this.MISC_COMM1 = new YGCString(15);
                this.MISC_COMM2 = new YGCString(15);
                this.MISC_COMM3 = new YGCString(15);
                this.MISC_COMM4 = new YGCString(15);
                this.ADVA_NUM = new YGCNumber(3);
                this.ADVA_CNTRY = new YGCString(3);
            }
            public RecordType06() : this(6) { }
            public RecordType06(string RT06Entry) : base(RT06Entry)
            {
                try
                {
                    this.ADVA_NAME = new YGCString(30) { DataString = RT06Entry.Substring(67) };
                    this.ADVA_FIRM = new YGCString(30) { DataString = RT06Entry.Substring(97) };
                    this.ADVA_FIRM2 = new YGCString(30) { DataString = RT06Entry.Substring(127) };
                    this.ADVA_STREET = new YGCString(30) { DataString = RT06Entry.Substring(157) };
                    this.ADVA_CSZ = new YGCString(30) { DataString = RT06Entry.Substring(187) };
                    this.ADVA_SALUT = new YGCString(30) { DataString = RT06Entry.Substring(217) };
                    this.ADVA_PHONE = new YGCString(30) { DataString = RT06Entry.Substring(247) };
                    this.ADVA_FAX = new YGCString(15) { DataString = RT06Entry.Substring(277) };
                    this.ADVA_FILENO = new YGCString(12) { DataString = RT06Entry.Substring(292) };
                    this.MISC_DATE1 = new YGCDate() { DataString = RT06Entry.Substring(304) };
                    this.MISC_DATE2 = new YGCDate() { DataString = RT06Entry.Substring(312) };
                    this.MISC_AMT1 = new YGCDecimal(14, 2) { DataString = RT06Entry.Substring(320) };
                    this.MISC_AMT2 = new YGCDecimal(14, 2) { DataString = RT06Entry.Substring(334) };
                    this.MISC_COMM1 = new YGCString(15) { DataString = RT06Entry.Substring(348) };
                    this.MISC_COMM2 = new YGCString(15) { DataString = RT06Entry.Substring(363) };
                    this.MISC_COMM3 = new YGCString(15) { DataString = RT06Entry.Substring(378) };
                    this.MISC_COMM4 = new YGCString(15) { DataString = RT06Entry.Substring(393) };
                    this.ADVA_NUM = new YGCNumber(3) { DataString = RT06Entry.Substring(408) };
                    this.ADVA_CNTRY = new YGCString(3) { DataString = RT06Entry.Substring(411) };
                }
                catch
                {
                    if (this.ADVA_NAME == null) this.ADVA_NAME = new YGCString(30);
                    if (this.ADVA_FIRM == null) this.ADVA_FIRM = new YGCString(30);
                    if (this.ADVA_FIRM2 == null) this.ADVA_FIRM2 = new YGCString(30);
                    if (this.ADVA_STREET == null) this.ADVA_STREET = new YGCString(30);
                    if (this.ADVA_CSZ == null) this.ADVA_CSZ = new YGCString(30);
                    if (this.ADVA_SALUT == null) this.ADVA_SALUT = new YGCString(30);
                    if (this.ADVA_PHONE == null) this.ADVA_PHONE = new YGCString(30);
                    if (this.ADVA_FAX == null) this.ADVA_FAX = new YGCString(15);
                    if (this.ADVA_FILENO == null) this.ADVA_FILENO = new YGCString(12);
                    if (this.MISC_DATE1 == null) this.MISC_DATE1 = new YGCDate();
                    if (this.MISC_DATE2 == null) this.MISC_DATE2 = new YGCDate();
                    if (this.MISC_AMT1 == null) this.MISC_AMT1 = new YGCDecimal(14, 2);
                    if (this.MISC_AMT2 == null) this.MISC_AMT2 = new YGCDecimal(14, 2);
                    if (this.MISC_COMM1 == null) this.MISC_COMM1 = new YGCString(15);
                    if (this.MISC_COMM2 == null) this.MISC_COMM2 = new YGCString(15);
                    if (this.MISC_COMM3 == null) this.MISC_COMM3 = new YGCString(15);
                    if (this.MISC_COMM4 == null) this.MISC_COMM4 = new YGCString(15);
                    if (this.ADVA_NUM == null) this.ADVA_NUM = new YGCNumber(3);
                    if (this.ADVA_CNTRY == null) this.ADVA_CNTRY = new YGCString(3);
                }
            }

            public override Type GetType() { return typeof(RecordType06); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}",
                    base.ToString(),
                    this.ADVA_NAME,
                    this.ADVA_FIRM,
                    this.ADVA_FIRM2,
                    this.ADVA_STREET,
                    this.ADVA_CSZ,
                    this.ADVA_SALUT,
                    this.ADVA_PHONE,
                    this.ADVA_FAX,
                    this.ADVA_FILENO,
                    this.MISC_DATE1,
                    this.MISC_DATE2,
                    this.MISC_AMT1,
                    this.MISC_AMT2,
                    this.MISC_COMM1,
                    this.MISC_COMM2,
                    this.MISC_COMM3,
                    this.MISC_COMM4,
                    this.ADVA_NUM,
                    this.ADVA_CNTRY);
            }
        }
        #endregion

        #region Record Type 07
        /// <summary>
        /// Record Type 07 - Legal Information (Sender to Receiver)
        /// <para>This record reports information regarding a suit filed against the debtor. More details can be provided in return by the receiver (agency/firm) in record 41. Value = 07.</para>
        /// </summary>
        public class RecordType07 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the county the suit was filed in.
            /// </summary>
            public YGCString CRT_COUNTY { get; private set; }
            /// <summary>
            /// Typically the full name of the court. Ex: Gwinnett County State Court, THE NINTH JUDICIAL CIRCUIT.
            /// </summary>
            public YGCString CRT_DESIG { get; private set; }
            /// <summary>
            /// This is a more brief description of the court. Ex: Superior, Supreme, Circuit
            /// </summary>
            public YGCString CRT_TYPE { get; private set; }
            /// <summary>
            /// This could be the official name of the sheriff's office or title, such as GLENN COUNTY SHERIFF, or the full name of the sheriff, which can be split between this field and SHER_NAME2.
            /// </summary>
            public YGCString SHER_NAME { get; private set; }
            /// <summary>
            /// This can be an overflow field for SHER_NAME.
            /// </summary>
            public YGCString SHER_NAME2 { get; private set; }
            /// <summary>
            /// This is the sheriff's office's street address.
            /// </summary>
            public YGCString SHER_STREET { get; private set; }
            /// <summary>
            /// This is the sheriff's office's city, state and zip code.
            /// <para>Format this field as City ST Zip or City,ST Zip.</para>
            /// <para>(Example: Linden NJ 07036 or Linden,NJ 07036)</para>
            /// </summary>
            public YGCString SHER_CSZ { get; private set; }
            /// <summary>
            /// This is the dollar amount requested in the suit. If this field is populated, SUIT_DATE must contain a valid date.
            /// </summary>
            public YGCDecimal SUIT_AMT { get; private set; }
            /// <summary>
            /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
            /// </summary>
            public YGCDecimal CONTRACT_FEE { get; private set; }
            /// <summary>
            /// This is the statutory fee awarded only to the attorney (not remitted on), determined by the debtor state.
            /// </summary>
            public YGCDecimal STAT_FEE { get; private set; }
            /// <summary>
            /// Initial number assigned to the suit.
            /// </summary>
            public YGCString DOCKET_NO { get; private set; }
            /// <summary>
            /// Depending on the state the suit is filed in, a new number may be assigned upon judgment.
            /// </summary>
            public YGCString JUDGMENT_NO { get; private set; }
            /// <summary>
            /// If the debtor has filed for bankruptcy, this is the court-issued number.
            /// </summary>
            public YGCString BKCY_NO { get; private set; }
            /// <summary>
            /// This is the date the suit was filed. If this field is populated, SUIT_AMT must be non-zero.
            /// </summary>
            public YGCDate SUIT_DATE { get; private set; }
            /// <summary>
            /// This is the date the judgment was entered. If this field is populated, JDGMNT_AMT must be non-zero.
            /// </summary>
            public YGCDate JUDGMENT_DATE { get; private set; }
            /// <summary>
            /// This is the dollar amount awarded in the judgment. If this field is populated, JDGMNT_DATE must contain a valid date.
            /// </summary>
            public YGCDecimal JUDGMENT_AMT { get; private set; }
            /// <summary>
            /// This is the principal amount of the judgment.
            /// </summary>
            public YGCDecimal JUDGMENT_PRIN { get; private set; }
            /// <summary>
            /// This is the dollar amount of the interest due before the judgment was rendered.
            /// </summary>
            public YGCDecimal PREJ_INT { get; private set; }
            /// <summary>
            /// This is the sum of the costs to the sender to carry the suit forward, such as attorney fees.
            /// </summary>
            public YGCDecimal JUDGMENT_COSTS { get; private set; }
            /// <summary>
            /// This is the difference between what was requested in the suit and the judgment amount.
            /// </summary>
            public YGCDecimal ADJUSTMENT { get; private set; }
            /// <summary>
            /// This is the standard code for the sheriff's country.
            /// </summary>
            public YGCString SHER_CNTRY { get; private set; }
            /// <summary>
            /// This is name of the court or clerk.
            /// </summary>
            public YGCString CRT_NAME { get; private set; }
            /// <summary>
            /// This is the date when the suit judgment expires. It is usually between 7 and 12 years from the filing date.
            /// </summary>
            public YGCDate JDGMNT_EXP_DATE { get; private set; }
            /// <summary>
            /// This is a free-form text field explaining the reason for filing suit.
            /// </summary>
            public YGCString SUIT_REASON { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the primary debtor from the latest Record 02, Record 31 or Record 51.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR1 { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the 2nd debtor name in the latest Record 03, Record 33 or Record 53.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR2 { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the 3rd debtor name in the latest Record 03, Record 33 or Record 53.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR3 { get; private set; }
            /// <summary>
            /// This is the date the judgment was recorded; this may be different than JDGMNT_DATE.
            /// </summary>
            public YGCDate JUDGMENT_RECORDED_DATE { get; private set; }
            /// <summary>
            /// This is the date that the suit documents were generated and sent to court, as opposed to the date the suit was filed by the court.
            /// </summary>
            public YGCDate SUIT_ISSUED_DATE { get; private set; }
            #endregion

            protected RecordType07(int Record) : base(Record)
            {
                this.CRT_COUNTY = new YGCString(20);
                this.CRT_DESIG = new YGCString(30);
                this.CRT_TYPE = new YGCString(15);
                this.SHER_NAME = new YGCString(25);
                this.SHER_NAME2 = new YGCString(25);
                this.SHER_STREET = new YGCString(25);
                this.SHER_CSZ = new YGCString(25);
                this.SUIT_AMT = new YGCDecimal(14, 2);
                this.CONTRACT_FEE = new YGCDecimal(14, 2);
                this.STAT_FEE = new YGCDecimal(14, 2);
                this.DOCKET_NO = new YGCString(15);
                this.JUDGMENT_NO = new YGCString(12);
                this.BKCY_NO = new YGCString(12);
                this.SUIT_DATE = new YGCDate();
                this.JUDGMENT_DATE = new YGCDate();
                this.JUDGMENT_AMT = new YGCDecimal(14, 2);
                this.JUDGMENT_PRIN = new YGCDecimal(14, 2);
                this.PREJ_INT = new YGCDecimal(14, 2);
                this.JUDGMENT_COSTS = new YGCDecimal(14, 2);
                this.ADJUSTMENT = new YGCDecimal(14, 2);
                this.SHER_CNTRY = new YGCString(3);
                this.CRT_NAME = new YGCString(50);
                this.JDGMNT_EXP_DATE = new YGCDate();
                this.SUIT_REASON = new YGCString(30);
                this.JUDGMENTONDEBTOR1 = new YGCBool(1, "Y");
                this.JUDGMENTONDEBTOR2 = new YGCBool(1, "Y");
                this.JUDGMENTONDEBTOR3 = new YGCBool(1, "Y");
                this.JUDGMENT_RECORDED_DATE = new YGCDate();
                this.SUIT_ISSUED_DATE = new YGCDate();
            }
            public RecordType07() : this(7) { }
            public RecordType07(string RT07Entry) : base(RT07Entry)
            {
                try
                {
                    this.CRT_COUNTY = new YGCString(20) { DataString = RT07Entry.Substring(67) };
                    this.CRT_DESIG = new YGCString(30) { DataString = RT07Entry.Substring(87) };
                    this.CRT_TYPE = new YGCString(15) { DataString = RT07Entry.Substring(117) };
                    this.SHER_NAME = new YGCString(25) { DataString = RT07Entry.Substring(132) };
                    this.SHER_NAME2 = new YGCString(25) { DataString = RT07Entry.Substring(157) };
                    this.SHER_STREET = new YGCString(25) { DataString = RT07Entry.Substring(182) };
                    this.SHER_CSZ = new YGCString(25) { DataString = RT07Entry.Substring(207) };
                    this.SUIT_AMT = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(232) };
                    this.CONTRACT_FEE = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(246) };
                    this.STAT_FEE = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(260) };
                    this.DOCKET_NO = new YGCString(15) { DataString = RT07Entry.Substring(274) };
                    this.JUDGMENT_NO = new YGCString(12) { DataString = RT07Entry.Substring(289) };
                    this.BKCY_NO = new YGCString(12) { DataString = RT07Entry.Substring(301) };
                    this.SUIT_DATE = new YGCDate() { DataString = RT07Entry.Substring(313) };
                    this.JUDGMENT_DATE = new YGCDate() { DataString = RT07Entry.Substring(321) };
                    this.JUDGMENT_AMT = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(329) };
                    this.JUDGMENT_PRIN = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(343) };
                    this.PREJ_INT = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(357) };
                    this.JUDGMENT_COSTS = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(371) };
                    this.ADJUSTMENT = new YGCDecimal(14, 2) { DataString = RT07Entry.Substring(385) };
                    this.SHER_CNTRY = new YGCString(3) { DataString = RT07Entry.Substring(399) };
                    this.CRT_NAME = new YGCString(50) { DataString = RT07Entry.Length > 402 ? RT07Entry.Substring(402) : "" };
                    this.JDGMNT_EXP_DATE = new YGCDate() { DataString = RT07Entry.Length > 452 ? RT07Entry.Substring(452) : "" };
                    this.SUIT_REASON = new YGCString(30) { DataString = RT07Entry.Length > 460 ? RT07Entry.Substring(460) : "" };
                    this.JUDGMENTONDEBTOR1 = new YGCBool(1, "Y") { DataString = RT07Entry.Length > 490 ? RT07Entry.Substring(490) : "" };
                    this.JUDGMENTONDEBTOR2 = new YGCBool(1, "Y") { DataString = RT07Entry.Length > 491 ? RT07Entry.Substring(491) : "" };
                    this.JUDGMENTONDEBTOR3 = new YGCBool(1, "Y") { DataString = RT07Entry.Length > 492 ? RT07Entry.Substring(492) : "" };
                    this.JUDGMENT_RECORDED_DATE = new YGCDate() { DataString = RT07Entry.Length > 493 ? RT07Entry.Substring(493) : "" };
                    this.SUIT_ISSUED_DATE = new YGCDate() { DataString = RT07Entry.Length > 501 ? RT07Entry.Substring(501) : "" };
                }
                catch
                {
                    if (this.CRT_COUNTY == null) this.CRT_COUNTY = new YGCString(20);
                    if (this.CRT_DESIG == null) this.CRT_DESIG = new YGCString(30);
                    if (this.CRT_TYPE == null) this.CRT_TYPE = new YGCString(15);
                    if (this.SHER_NAME == null) this.SHER_NAME = new YGCString(25);
                    if (this.SHER_NAME2 == null) this.SHER_NAME2 = new YGCString(25);
                    if (this.SHER_STREET == null) this.SHER_STREET = new YGCString(25);
                    if (this.SHER_CSZ == null) this.SHER_CSZ = new YGCString(25);
                    if (this.SUIT_AMT == null) this.SUIT_AMT = new YGCDecimal(14, 2);
                    if (this.CONTRACT_FEE == null) this.CONTRACT_FEE = new YGCDecimal(14, 2);
                    if (this.STAT_FEE == null) this.STAT_FEE = new YGCDecimal(14, 2);
                    if (this.DOCKET_NO == null) this.DOCKET_NO = new YGCString(15);
                    if (this.JUDGMENT_NO == null) this.JUDGMENT_NO = new YGCString(12);
                    if (this.BKCY_NO == null) this.BKCY_NO = new YGCString(12);
                    if (this.SUIT_DATE == null) this.SUIT_DATE = new YGCDate();
                    if (this.JUDGMENT_DATE == null) this.JUDGMENT_DATE = new YGCDate();
                    if (this.JUDGMENT_AMT == null) this.JUDGMENT_AMT = new YGCDecimal(14, 2);
                    if (this.JUDGMENT_PRIN == null) this.JUDGMENT_PRIN = new YGCDecimal(14, 2);
                    if (this.PREJ_INT == null) this.PREJ_INT = new YGCDecimal(14, 2);
                    if (this.JUDGMENT_COSTS == null) this.JUDGMENT_COSTS = new YGCDecimal(14, 2);
                    if (this.ADJUSTMENT == null) this.ADJUSTMENT = new YGCDecimal(14, 2);
                    if (this.SHER_CNTRY == null) this.SHER_CNTRY = new YGCString(3);
                    if (this.CRT_NAME == null) this.CRT_NAME = new YGCString(50);
                    if (this.JDGMNT_EXP_DATE == null) this.JDGMNT_EXP_DATE = new YGCDate();
                    if (this.SUIT_REASON == null) this.SUIT_REASON = new YGCString(30);
                    if (this.JUDGMENTONDEBTOR1 == null) this.JUDGMENTONDEBTOR1 = new YGCBool(1, "Y");
                    if (this.JUDGMENTONDEBTOR2 == null) this.JUDGMENTONDEBTOR2 = new YGCBool(1, "Y");
                    if (this.JUDGMENTONDEBTOR3 == null) this.JUDGMENTONDEBTOR3 = new YGCBool(1, "Y");
                    if (this.JUDGMENT_RECORDED_DATE == null) this.JUDGMENT_RECORDED_DATE = new YGCDate();
                    if (this.SUIT_ISSUED_DATE == null) this.SUIT_ISSUED_DATE = new YGCDate();
                }
            }

            public override Type GetType() { return typeof(RecordType07); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}",
                    base.ToString(),
                    this.CRT_COUNTY,
                    this.CRT_DESIG,
                    this.CRT_TYPE,
                    this.SHER_NAME,
                    this.SHER_NAME2,
                    this.SHER_STREET,
                    this.SHER_CSZ,
                    this.SUIT_AMT,
                    this.CONTRACT_FEE,
                    this.STAT_FEE,
                    this.DOCKET_NO,
                    this.JUDGMENT_NO,
                    this.BKCY_NO,
                    this.SUIT_DATE,
                    this.JUDGMENT_DATE,
                    this.JUDGMENT_AMT,
                    this.JUDGMENT_PRIN,
                    this.PREJ_INT,
                    this.JUDGMENT_COSTS,
                    this.ADJUSTMENT,
                    this.SHER_CNTRY,
                    this.CRT_NAME,
                    this.JDGMNT_EXP_DATE,
                    this.SUIT_REASON,
                    this.JUDGMENTONDEBTOR1,
                    this.JUDGMENTONDEBTOR2,
                    this.JUDGMENTONDEBTOR3,
                    this.JUDGMENT_RECORDED_DATE,
                    this.SUIT_ISSUED_DATE);
            }
        }
        #endregion

        #region Record Type 08
        /// <summary>
        /// Record Type 08 - Caption - Legal Names (Sender to Receiver)
        /// <para>This record is for entering the caption; i.e., the parties named in the suit. The Plaintiffs are typically the original creditors and the Defendants are the debtors. Value = 08.</para>
        /// </summary>
        public class RecordType08 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the combination of all plaintiff fields
            /// <para>(DO NOT USE - This field will not write to output file)</para>
            /// </summary>
            public YGCString PLAINTIFF_COMBINED { get; private set; }
            /// <summary>
            /// This is the first plaintiff, typically the debt owner or original creditor. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_1 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the second plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_2 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the third plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_3 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the fourth plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_4 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the fifth plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_5 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the sixth plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_6 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous plaintiff line or for the seventh plaintiff. There is no format restriction.
            /// </summary>
            public YGCExtString PLAINTIFF_7 { get; private set; }
            /// <summary>
            /// This is the full name of the first defendant, typically the debtor. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_1 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the second defendant. It can also be an alias of the primary defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_2 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the third defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_3 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the fourth defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_4 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the fifth defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_5 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the sixth defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_6 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the seventh defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_7 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the eighth defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_8 { get; private set; }
            /// <summary>
            /// This can be used as overflow for the previous defendant line or for the ninth defendant. There is no format restriction.
            /// </summary>
            public YGCExtString DEFENDANT_9 { get; private set; }
            #endregion

            protected RecordType08(int Record) : base(Record)
            {
                this.PLAINTIFF_COMBINED = new YGCString(210);
                this.PLAINTIFF_7 = new YGCExtString(30, null);
                this.PLAINTIFF_6 = new YGCExtString(30, this.PLAINTIFF_7);
                this.PLAINTIFF_5 = new YGCExtString(30, this.PLAINTIFF_6);
                this.PLAINTIFF_4 = new YGCExtString(30, this.PLAINTIFF_5);
                this.PLAINTIFF_3 = new YGCExtString(30, this.PLAINTIFF_4);
                this.PLAINTIFF_2 = new YGCExtString(30, this.PLAINTIFF_3);
                this.PLAINTIFF_1 = new YGCExtString(30, this.PLAINTIFF_2);
                this.DEFENDANT_9 = new YGCExtString(30, null);
                this.DEFENDANT_8 = new YGCExtString(30, this.DEFENDANT_9);
                this.DEFENDANT_7 = new YGCExtString(30, this.DEFENDANT_8);
                this.DEFENDANT_6 = new YGCExtString(30, this.DEFENDANT_7);
                this.DEFENDANT_5 = new YGCExtString(30, this.DEFENDANT_6);
                this.DEFENDANT_4 = new YGCExtString(30, this.DEFENDANT_5);
                this.DEFENDANT_3 = new YGCExtString(30, this.DEFENDANT_4);
                this.DEFENDANT_2 = new YGCExtString(30, this.DEFENDANT_3);
                this.DEFENDANT_1 = new YGCExtString(30, this.DEFENDANT_2);
            }
            public RecordType08() : this(8) { }
            public RecordType08(string RT08Entry) : base(RT08Entry)
            {
                this.PLAINTIFF_COMBINED = new YGCString(210) { DataString = RT08Entry.Length > 67 ? RT08Entry.Substring(67) : "" };
                this.PLAINTIFF_7 = new YGCExtString(30, null) { DataString = RT08Entry.Length > 247 ? RT08Entry.Substring(247) : "" };
                this.PLAINTIFF_6 = new YGCExtString(30, this.PLAINTIFF_7) { DataString = RT08Entry.Length > 247 ? RT08Entry.Substring(217, 30) : RT08Entry.Length > 217 ? RT08Entry.Substring(217) : "" };
                this.PLAINTIFF_5 = new YGCExtString(30, this.PLAINTIFF_6) { DataString = RT08Entry.Length > 187 ? RT08Entry.Substring(217, 30) : RT08Entry.Length > 187 ? RT08Entry.Substring(187) : "" };
                this.PLAINTIFF_4 = new YGCExtString(30, this.PLAINTIFF_5) { DataString = RT08Entry.Length > 157 ? RT08Entry.Substring(187, 30) : RT08Entry.Length > 157 ? RT08Entry.Substring(157) : "" };
                this.PLAINTIFF_3 = new YGCExtString(30, this.PLAINTIFF_4) { DataString = RT08Entry.Length > 157 ? RT08Entry.Substring(127, 30) : RT08Entry.Length > 127 ? RT08Entry.Substring(127) : "" };
                this.PLAINTIFF_2 = new YGCExtString(30, this.PLAINTIFF_3) { DataString = RT08Entry.Length > 127 ? RT08Entry.Substring(97, 30) : RT08Entry.Length > 97 ? RT08Entry.Substring(97) : "" };
                this.PLAINTIFF_1 = new YGCExtString(30, this.PLAINTIFF_2) { DataString = RT08Entry.Length > 97 ? RT08Entry.Substring(67, 30) : RT08Entry.Length > 67 ? RT08Entry.Substring(67) : "" };
                this.DEFENDANT_9 = new YGCExtString(30, null) { DataString = RT08Entry.Length > 517 ? RT08Entry.Substring(517) : "" };
                this.DEFENDANT_8 = new YGCExtString(30, this.DEFENDANT_9) { DataString = RT08Entry.Length > 517 ? RT08Entry.Substring(487, 30) : RT08Entry.Length > 487 ? RT08Entry.Substring(487) : "" };
                this.DEFENDANT_7 = new YGCExtString(30, this.DEFENDANT_8) { DataString = RT08Entry.Length > 487 ? RT08Entry.Substring(457, 30) : RT08Entry.Length > 457 ? RT08Entry.Substring(457) : "" };
                this.DEFENDANT_6 = new YGCExtString(30, this.DEFENDANT_7) { DataString = RT08Entry.Length > 457 ? RT08Entry.Substring(427, 30) : RT08Entry.Length > 427 ? RT08Entry.Substring(427) : "" };
                this.DEFENDANT_5 = new YGCExtString(30, this.DEFENDANT_6) { DataString = RT08Entry.Length > 427 ? RT08Entry.Substring(397, 30) : RT08Entry.Length > 397 ? RT08Entry.Substring(397) : "" };
                this.DEFENDANT_4 = new YGCExtString(30, this.DEFENDANT_5) { DataString = RT08Entry.Length > 397 ? RT08Entry.Substring(367, 30) : RT08Entry.Length > 367 ? RT08Entry.Substring(367) : "" };
                this.DEFENDANT_3 = new YGCExtString(30, this.DEFENDANT_4) { DataString = RT08Entry.Length > 367 ? RT08Entry.Substring(337, 30) : RT08Entry.Length > 337 ? RT08Entry.Substring(337) : "" };
                this.DEFENDANT_2 = new YGCExtString(30, this.DEFENDANT_3) { DataString = RT08Entry.Length > 337 ? RT08Entry.Substring(307, 30) : RT08Entry.Length > 307 ? RT08Entry.Substring(307) : "" };
                this.DEFENDANT_1 = new YGCExtString(30, this.DEFENDANT_2) { DataString = RT08Entry.Length > 307 ? RT08Entry.Substring(277, 30) : RT08Entry.Length > 277 ? RT08Entry.Substring(277) : "" };
            }

            public override Type GetType() { return typeof(RecordType08); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}",
                    base.ToString(),
                    this.PLAINTIFF_1,
                    this.PLAINTIFF_2,
                    this.PLAINTIFF_3,
                    this.PLAINTIFF_4,
                    this.PLAINTIFF_5,
                    this.PLAINTIFF_6,
                    this.PLAINTIFF_7,
                    this.DEFENDANT_1,
                    this.DEFENDANT_2,
                    this.DEFENDANT_3,
                    this.DEFENDANT_4,
                    this.DEFENDANT_5,
                    this.DEFENDANT_6,
                    this.DEFENDANT_7,
                    this.DEFENDANT_8,
                    this.DEFENDANT_9);
            }
        }
        #endregion

        #region Record Type 09
        /// <summary>
        /// Record Type 09 - Message (Sender to Receiver)
        /// <para>Senders communicate status updates to their receivers with this record type. It should include the PCODE to clearly identify the update.</para>
        /// <para>Examples of status updates are direct payment, suit filed, account refused, judgment issued and account closed. Value = 09.</para>
        /// </summary>
        public class RecordType09 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the date on which the event reported occurred.
            /// </summary>
            public YGCDate PDATE { get; private set; }
            /// <summary>
            /// This field indicates the type of status update. Some codes trigger changes to the account in YouveGotReports. See Status Codes tab for details; YGC highly recommends using a PCODE.
            /// </summary>
            public YGCString PCODE { get; private set; }
            /// <summary>
            /// This is a free-text comment or description to accompany the message. It should have a CRLF after the last character, which therefore means you don't have to right-pad the field with spaces to fill all 1024 characters.
            /// </summary>
            public YGCExtString PCMT { get; private set; }
            /// <summary>
            /// Note 2
            /// </summary>
            public YGCExtString NOTE02 { get; private set; }
            /// <summary>
            /// Note 3
            /// </summary>
            public YGCExtString NOTE03 { get; private set; }
            /// <summary>
            /// Note 4
            /// </summary>
            public YGCExtString NOTE04 { get; private set; }
            /// <summary>
            /// Note 5
            /// </summary>
            public YGCExtString NOTE05 { get; private set; }
            /// <summary>
            /// Note 6
            /// </summary>
            public YGCExtString NOTE06 { get; private set; }
            /// <summary>
            /// Note 7
            /// </summary>
            public YGCExtString NOTE07 { get; private set; }
            /// <summary>
            /// Note 8
            /// </summary>
            public YGCExtString NOTE08 { get; private set; }
            /// <summary>
            /// Note 9
            /// </summary>
            public YGCExtString NOTE09 { get; private set; }
            /// <summary>
            /// Note 10
            /// </summary>
            public YGCExtString NOTE10 { get; private set; }
            /// <summary>
            /// Note 11
            /// </summary>
            public YGCExtString NOTE11 { get; private set; }
            /// <summary>
            /// Note 12
            /// </summary>
            public YGCExtString NOTE12 { get; private set; }
            /// <summary>
            /// Note 13
            /// </summary>
            public YGCExtString NOTE13 { get; private set; }
            /// <summary>
            /// Note 14
            /// </summary>
            public YGCExtString NOTE14 { get; private set; }
            /// <summary>
            /// Note 15
            /// </summary>
            public YGCExtString NOTE15 { get; private set; }
            /// <summary>
            /// Note 16
            /// </summary>
            public YGCExtString NOTE16 { get; private set; }
            /// <summary>
            /// Note 17
            /// </summary>
            public YGCExtString NOTE17 { get; private set; }
            /// <summary>
            /// This is the time on which the event reported occurred. The format is HHMMSS on a 24-hour clock.
            /// </summary>
            public YGCTime PTIME { get; private set; }
            /// <summary>
            /// Local time zone of the sender delivering the status update.Must be expressed in GMT format. For example:
            /// <para>GMT-5 = USA Eastern</para>
            /// <para>GMT-6 = USA Central</para>
            /// <para>GMT-7 = USA Mountain</para>
            /// <para>GMT-8 = USA Pacific</para>
            /// <para>GMT-9 = Alaska</para>
            /// <para>GMT-10 = Hawaii</para>
            /// </summary>
            public YGCString PTIME_ZONE { get; private set; }
            /// <summary>
            /// Phone number dialed to contact the debtor.
            /// </summary>
            public YGCString PHONE_NUMBER { get; private set; }
            /// <summary>
            /// Call Direction
            /// <para>Values: I/O. Incoming or outgoing.</para>
            /// </summary>
            public YGCEnum<CallDirection, CallDirectionValues> CALL_DIRECTION { get; private set; }
            /// <summary>
            /// Debtor
            /// <para>Values: 1 = Debtor 1, 2 = Debtor 2.</para>
            /// </summary>
            public YGCNumber DBTR_TYPE { get; private set; }
            #endregion

            protected RecordType09(int Record) : base(Record)
            {
                this.PDATE = new YGCDate();
                this.PCODE = new YGCString(8);
                this.NOTE17 = new YGCExtString(57, null);
                this.NOTE16 = new YGCExtString(57, this.NOTE17);
                this.NOTE15 = new YGCExtString(57, this.NOTE16);
                this.NOTE14 = new YGCExtString(57, this.NOTE15);
                this.NOTE13 = new YGCExtString(57, this.NOTE14);
                this.NOTE12 = new YGCExtString(57, this.NOTE13);
                this.NOTE11 = new YGCExtString(57, this.NOTE12);
                this.NOTE10 = new YGCExtString(57, this.NOTE11);
                this.NOTE09 = new YGCExtString(57, this.NOTE10);
                this.NOTE08 = new YGCExtString(57, this.NOTE09);
                this.NOTE07 = new YGCExtString(57, this.NOTE08);
                this.NOTE06 = new YGCExtString(57, this.NOTE07);
                this.NOTE05 = new YGCExtString(57, this.NOTE06);
                this.NOTE04 = new YGCExtString(57, this.NOTE05);
                this.NOTE03 = new YGCExtString(57, this.NOTE04);
                this.NOTE02 = new YGCExtString(57, this.NOTE03);
                this.PCMT = new YGCExtString(48, this.NOTE02);
                this.PTIME = new YGCTime();
                this.PTIME_ZONE = new YGCString(9);
                this.PHONE_NUMBER = new YGCString(15);
                this.CALL_DIRECTION = new YGCEnum<CallDirection, CallDirectionValues>(1);
                this.DBTR_TYPE = new YGCNumber(3);
            }
            public RecordType09() : this(9) { }
            public RecordType09(string RT09Entry) : base(RT09Entry)
            {
                try
                {
                    this.PDATE = new YGCDate() { DataString = RT09Entry.Substring(67, 8) };
                    this.PCODE = new YGCString(8) { DataString = RT09Entry.Substring(75, 8) };
                    this.NOTE17 = new YGCExtString(57, null) { DataString = RT09Entry.Length > 976 ? RT09Entry.Substring(976) : "" };
                    this.NOTE16 = new YGCExtString(57, this.NOTE17) { DataString = RT09Entry.Length > 975 ? RT09Entry.Substring(919, 57) : RT09Entry.Length > 919 ? RT09Entry.Substring(919) : "" };
                    this.NOTE15 = new YGCExtString(57, this.NOTE16) { DataString = RT09Entry.Length > 918 ? RT09Entry.Substring(862, 57) : RT09Entry.Length > 862 ? RT09Entry.Substring(862) : "" };
                    this.NOTE14 = new YGCExtString(57, this.NOTE15) { DataString = RT09Entry.Length > 861 ? RT09Entry.Substring(805, 57) : RT09Entry.Length > 805 ? RT09Entry.Substring(805) : "" };
                    this.NOTE13 = new YGCExtString(57, this.NOTE14) { DataString = RT09Entry.Length > 804 ? RT09Entry.Substring(748, 57) : RT09Entry.Length > 748 ? RT09Entry.Substring(748) : "" };
                    this.NOTE12 = new YGCExtString(57, this.NOTE13) { DataString = RT09Entry.Length > 747 ? RT09Entry.Substring(691, 57) : RT09Entry.Length > 691 ? RT09Entry.Substring(691) : "" };
                    this.NOTE11 = new YGCExtString(57, this.NOTE12) { DataString = RT09Entry.Length > 690 ? RT09Entry.Substring(634, 57) : RT09Entry.Length > 634 ? RT09Entry.Substring(634) : "" };
                    this.NOTE10 = new YGCExtString(57, this.NOTE11) { DataString = RT09Entry.Length > 633 ? RT09Entry.Substring(577, 57) : RT09Entry.Length > 577 ? RT09Entry.Substring(577) : "" };
                    this.NOTE09 = new YGCExtString(57, this.NOTE10) { DataString = RT09Entry.Length > 576 ? RT09Entry.Substring(520, 57) : RT09Entry.Length > 520 ? RT09Entry.Substring(520) : "" };
                    this.NOTE08 = new YGCExtString(57, this.NOTE09) { DataString = RT09Entry.Length > 519 ? RT09Entry.Substring(463, 57) : RT09Entry.Length > 463 ? RT09Entry.Substring(463) : "" };
                    this.NOTE07 = new YGCExtString(57, this.NOTE08) { DataString = RT09Entry.Length > 462 ? RT09Entry.Substring(406, 57) : RT09Entry.Length > 406 ? RT09Entry.Substring(406) : "" };
                    this.NOTE06 = new YGCExtString(57, this.NOTE07) { DataString = RT09Entry.Length > 405 ? RT09Entry.Substring(349, 57) : RT09Entry.Length > 349 ? RT09Entry.Substring(349) : "" };
                    this.NOTE05 = new YGCExtString(57, this.NOTE06) { DataString = RT09Entry.Length > 348 ? RT09Entry.Substring(292, 57) : RT09Entry.Length > 292 ? RT09Entry.Substring(292) : "" };
                    this.NOTE04 = new YGCExtString(57, this.NOTE05) { DataString = RT09Entry.Length > 291 ? RT09Entry.Substring(245, 57) : RT09Entry.Length > 245 ? RT09Entry.Substring(245) : "" };
                    this.NOTE03 = new YGCExtString(57, this.NOTE04) { DataString = RT09Entry.Length > 244 ? RT09Entry.Substring(188, 57) : RT09Entry.Length > 188 ? RT09Entry.Substring(188) : "" };
                    this.NOTE02 = new YGCExtString(57, this.NOTE03) { DataString = RT09Entry.Length > 187 ? RT09Entry.Substring(131, 57) : RT09Entry.Length > 131 ? RT09Entry.Substring(131) : "" };
                    this.PCMT = new YGCExtString(48, this.NOTE02) { DataString = RT09Entry.Length > 130 ? RT09Entry.Substring(83, 48) : RT09Entry.Length > 83 ? RT09Entry.Substring(83) : "" };
                    this.PTIME = new YGCTime() { DataString = RT09Entry.Length > 1107 ? RT09Entry.Substring(1107) : "" };
                    this.PTIME_ZONE = new YGCString(9) { DataString = RT09Entry.Length > 1113 ? RT09Entry.Substring(1113) : "" };
                    this.PHONE_NUMBER = new YGCString(15) { DataString = RT09Entry.Length > 1122 ? RT09Entry.Substring(1122) : "" };
                    this.CALL_DIRECTION = new YGCEnum<CallDirection, CallDirectionValues>(1) { DataString = RT09Entry.Length > 1137 ? RT09Entry.Substring(1137) : "" };
                    this.DBTR_TYPE = new YGCNumber(3) { DataString = RT09Entry.Length > 1138 ? RT09Entry.Substring(1138) : "" };
                }
                catch { }
            }

            public override Type GetType() { return typeof(RecordType09); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}",
                    base.ToString(),
                    this.PDATE,
                    this.PCODE,
                    this.PCMT,
                    this.NOTE02,
                    this.NOTE03,
                    this.NOTE04,
                    this.NOTE05,
                    this.NOTE06,
                    this.NOTE07,
                    this.NOTE08,
                    this.NOTE09,
                    this.NOTE10,
                    this.NOTE11,
                    this.NOTE12,
                    this.NOTE13,
                    this.NOTE14,
                    this.NOTE15,
                    this.NOTE16,
                    this.NOTE17,
                    this.PTIME,
                    this.PTIME_ZONE,
                    this.PHONE_NUMBER,
                    this.CALL_DIRECTION,
                    this.DBTR_TYPE);
            }
        }
        #endregion

        #region Record Type 10
        /// <summary>
        /// Record Type 10 - Itemized Financial Transaction (Sender to Receiver)
        /// </summary>
        public class RecordType10 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// TRANSACTION DATE
            /// </summary>
            public YGCDate TA_DATE { get; private set; }
            /// <summary>
            /// TRANSACTION CODE
            /// </summary>
            public YGCEnum<TransactionCode> TA_CODE { get; private set; }
            /// <summary>
            /// Check/TA #
            /// </summary>
            public YGCNumber TA_NUMBER { get; private set; }
            /// <summary>
            /// TRANSACTION COMMENT
            /// </summary>
            public YGCString TA_CMT { get; private set; }
            /// <summary>
            /// AMOUNT OF TRANSACTION
            /// </summary>
            public YGCDecimal TA_AMT { get; private set; }
            /// <summary>
            /// N/A - Not Converted
            /// <para>LINE1_1</para>
            /// </summary>
            public YGCDecimal LINE1_1 { get; private set; }
            /// <summary>
            /// Costs Recovered
            /// <para>LINE1_2</para>
            /// </summary>
            public YGCDecimal COSTS { get; private set; }
            /// <summary>
            /// Stat Fee Recovered
            /// <para>LINE1_3</para>
            /// </summary>
            public YGCDecimal STAT_FEE { get; private set; }
            /// <summary>
            /// Net to Client (No Costs)
            /// <para>LINE1_4</para>
            /// </summary>
            public YGCDecimal NET_CLIENT { get; private set; }
            /// <summary>
            /// Debtor Balance
            /// <para>LINE1_5</para>
            /// </summary>
            public YGCDecimal BALANCE { get; private set; }
            /// <summary>
            /// Amount Received
            /// <para>LINE1_6</para>
            /// </summary>
            public YGCDecimal RECEIVED { get; private set; }
            /// <summary>
            /// Principal Collected
            /// <para>LINE2_1</para>
            /// </summary>
            public YGCDecimal PRINCIPAL { get; private set; }
            /// <summary>
            /// Interest Collected
            /// <para>LINE2_2</para>
            /// </summary>
            public YGCDecimal INTEREST { get; private set; }
            /// <summary>
            /// Commulative Collected
            /// <para>LINE2_3</para>
            /// </summary>
            public YGCDecimal LINE2_3 { get; private set; }
            /// <summary>
            /// Costs Returned
            /// <para>LINE2_4</para>
            /// </summary>
            public YGCDecimal COSTS_RETURNED { get; private set; }
            /// <summary>
            /// Costs Expended
            /// <para>LINE2_5</para>
            /// </summary>
            public YGCDecimal COSTS_EXPENDED { get; private set; }
            /// <summary>
            /// Costs Received from Client
            /// <para>LINE2_6</para>
            /// </summary>
            public YGCDecimal COSTS_RECEIVED { get; private set; }
            /// <summary>
            /// Suit Fees Received
            /// <para>LINE2_7</para>
            /// </summary>
            public YGCDecimal SUIT_FEE { get; private set; }
            /// <summary>
            /// Commissions Received
            /// <para>LINE2_8</para>
            /// </summary>
            public YGCDecimal COMMISSION { get; private set; }
            /// <summary>
            /// "B"efore "P"ost Suit after "J"udg
            /// </summary>
            public YGCEnum<Disposition, DispositionValues> BPJ { get; private set; }
            /// <summary>
            /// Amount Advanced out of Pocket
            /// </summary>
            public YGCDecimal ADJUST { get; private set; }
            /// <summary>
            /// Bill debtor for this Transaction
            /// </summary>
            public YGCString BILL { get; private set; }
            /// <summary>
            /// Total Accrued Interest 
            /// </summary>
            public YGCDecimal INT { get; private set; }
            /// <summary>
            /// Cost Balance as of This TA 
            /// </summary>
            public YGCDecimal COST_BAL { get; private set; }
            /// <summary>
            /// Amount Applied to Coll &amp; Hold
            /// </summary>
            public YGCDecimal COLL_HOLD { get; private set; }
            /// <summary>
            /// Amount Applied to Co-Co Fees
            /// </summary>
            public YGCDecimal CO_FEES { get; private set; }
            /// <summary>
            /// Amount Applied to Merchandise
            /// </summary>
            public YGCDecimal MERCHANDICE { get; private set; }
            /// <summary>
            /// Amount Applied to Tax Rebate
            /// </summary>
            public YGCDecimal TAX_REBATE { get; private set; }
            /// <summary>
            /// N/A - Ignored - Use for 3rd Party
            /// </summary>
            public YGCDecimal TAMT1 { get; private set; }
            #endregion

            protected RecordType10(int Record) : base(Record)
            {
                this.TA_DATE = new YGCDate();
                this.TA_CODE = new YGCEnum<TransactionCode>(2);
                this.TA_NUMBER = new YGCNumber(5);
                this.TA_CMT = new YGCString(16);
                this.TA_AMT = new YGCDecimal(14, 2);
                this.LINE1_1 = new YGCDecimal(14, 2);
                this.COSTS = new YGCDecimal(14, 2);
                this.STAT_FEE = new YGCDecimal(14, 2);
                this.NET_CLIENT = new YGCDecimal(14, 2);
                this.BALANCE = new YGCDecimal(14, 2);
                this.RECEIVED = new YGCDecimal(14, 2);
                this.PRINCIPAL = new YGCDecimal(14, 2);
                this.INTEREST = new YGCDecimal(14, 2);
                this.LINE2_3 = new YGCDecimal(14, 2);
                this.COSTS_RETURNED = new YGCDecimal(14, 2);
                this.COSTS_EXPENDED = new YGCDecimal(14, 2);
                this.COSTS_RECEIVED = new YGCDecimal(14, 2);
                this.SUIT_FEE = new YGCDecimal(14, 2);
                this.COMMISSION = new YGCDecimal(14, 2);
                this.BPJ = new YGCEnum<Disposition, DispositionValues>(1);
                this.ADJUST = new YGCDecimal(14, 2);
                this.BILL = new YGCString(1);
                this.INT = new YGCDecimal(14, 2);
                this.COST_BAL = new YGCDecimal(14, 2);
                this.COLL_HOLD = new YGCDecimal(14, 2);
                this.CO_FEES = new YGCDecimal(14, 2);
                this.MERCHANDICE = new YGCDecimal(14, 2);
                this.TAX_REBATE = new YGCDecimal(14, 2);
                this.TAMT1 = new YGCDecimal(14, 2);
            }
            public RecordType10() : this(10) { }
            public RecordType10(string RT10Entry) : base(RT10Entry)
            {
                this.TA_DATE = new YGCDate() { DataString = RT10Entry.Length > 67 ? RT10Entry.Substring(67) : "" };
                this.TA_CODE = new YGCEnum<TransactionCode>(2) { DataString = RT10Entry.Length > 75 ? RT10Entry.Substring(75) : "" };
                this.TA_NUMBER = new YGCNumber(5) { DataString = RT10Entry.Length > 77 ? RT10Entry.Substring(77) : "" };
                this.TA_CMT = new YGCString(16) { DataString = RT10Entry.Length > 82 ? RT10Entry.Substring(82) : "" };
                this.TA_AMT = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 98 ? RT10Entry.Substring(98) : "" };
                this.LINE1_1 = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 112 ? RT10Entry.Substring(112) : "" };
                this.COSTS = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 126 ? RT10Entry.Substring(126) : "" };
                this.STAT_FEE = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 140 ? RT10Entry.Substring(140) : "" };
                this.NET_CLIENT = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 154 ? RT10Entry.Substring(154) : "" };
                this.BALANCE = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 168 ? RT10Entry.Substring(168) : "" };
                this.RECEIVED = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 182 ? RT10Entry.Substring(182) : "" };
                this.PRINCIPAL = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 196 ? RT10Entry.Substring(196) : "" };
                this.INTEREST = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 210 ? RT10Entry.Substring(210) : "" };
                this.LINE2_3 = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 224 ? RT10Entry.Substring(224) : "" };
                this.COSTS_RETURNED = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 238 ? RT10Entry.Substring(238) : "" };
                this.COSTS_EXPENDED = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 252 ? RT10Entry.Substring(252) : "" };
                this.COSTS_RECEIVED = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 266 ? RT10Entry.Substring(266) : "" };
                this.SUIT_FEE = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 280 ? RT10Entry.Substring(280) : "" };
                this.COMMISSION = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 294 ? RT10Entry.Substring(294) : "" };
                this.BPJ = new YGCEnum<Disposition, DispositionValues>(1) { DataString = RT10Entry.Length > 308 ? RT10Entry.Substring(308) : "" };
                this.ADJUST = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 309 ? RT10Entry.Substring(309) : "" };
                this.BILL = new YGCString(1) { DataString = RT10Entry.Length > 323 ? RT10Entry.Substring(323) : "" };
                this.INT = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 324 ? RT10Entry.Substring(324) : "" };
                this.COST_BAL = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 338 ? RT10Entry.Substring(338) : "" };
                this.COLL_HOLD = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 352 ? RT10Entry.Substring(352) : "" };
                this.CO_FEES = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 366 ? RT10Entry.Substring(366) : "" };
                this.MERCHANDICE = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 380 ? RT10Entry.Substring(380) : "" };
                this.TAX_REBATE = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 394 ? RT10Entry.Substring(394) : "" };
                this.TAMT1 = new YGCDecimal(14, 2) { DataString = RT10Entry.Length > 408 ? RT10Entry.Substring(408) : "" };
            }

            public override Type GetType() { return typeof(RecordType10); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}",
                    base.ToString(),
                    this.TA_DATE,
                    this.TA_CODE,
                    this.TA_NUMBER,
                    this.TA_CMT,
                    this.TA_AMT,
                    this.LINE1_1,
                    this.COSTS,
                    this.STAT_FEE,
                    this.NET_CLIENT,
                    this.BALANCE,
                    this.RECEIVED,
                    this.PRINCIPAL,
                    this.INTEREST,
                    this.LINE2_3,
                    this.COSTS_RETURNED,
                    this.COSTS_EXPENDED,
                    this.COSTS_RECEIVED,
                    this.SUIT_FEE,
                    this.COMMISSION,
                    this.BPJ,
                    this.ADJUST,
                    this.BILL,
                    this.INT,
                    this.COST_BAL,
                    this.COLL_HOLD,
                    this.CO_FEES,
                    this.MERCHANDICE,
                    this.TAX_REBATE,
                    this.TAMT1
                    );
            }
        }
        #endregion

        #region Record Type 12
        /// <summary>
        /// Record Type 12 - Direct Payments (Sender to Receiver)
        /// <para>If the debtor made a direct payment to the sender instead of the firm, this record should be sent. The receiver should then send a record 30 (Payments) to the sender with a RET_CODE of 06. Value = 12.</para>
        /// </summary>
        public class RecordType12 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the date the direct payment was made.
            /// </summary>
            public YGCDate DP_DATE { get; private set; }
            /// <summary>
            /// This is a comment describing the payment, such as "REVERSAL/NSF" or "Payment."
            /// </summary>
            public YGCString DP_CMT { get; private set; }
            /// <summary>
            /// Amount Applied to Merchandise
            /// <para>This is a payment in the form of returned merchandise.</para>
            /// </summary>
            public YGCDecimal DP_MERCH { get; private set; }
            /// <summary>
            /// Amount of Payment w/ Fee Due
            /// <para>This is an actual payment made directly to the sender or creditor that requires fee.</para>
            /// <para>Use DP_NOFEE if no fee assessed</para>
            /// </summary>
            public YGCDecimal DP_CASH { get; private set; }
            /// <summary>
            /// Amount of Payment w/ No Fee Due
            /// <para>This is the payment received before a demand letter could be sent; thus, no fee is due to the firm.</para>
            /// <para>Use DP_CASH if fee should be assessed</para>
            /// </summary>
            public YGCDecimal DP_NOFEE { get; private set; }
            #endregion

            public RecordType12() : base(12)
            {
                this.DP_DATE = new YGCDate();
                this.DP_CMT = new YGCString(20);
                this.DP_MERCH = new YGCDecimal(14, 2);
                this.DP_CASH = new YGCDecimal(14, 2);
                this.DP_NOFEE = new YGCDecimal(14, 2);
            }
            public RecordType12(string RT12Entry) : base(RT12Entry)
            {
                try
                {
                    this.DP_DATE = new YGCDate() { DataString = RT12Entry.Substring(67) };
                    this.DP_CMT = new YGCString(20) { DataString = RT12Entry.Substring(75) };
                    this.DP_MERCH = new YGCDecimal(14, 2) { DataString = RT12Entry.Substring(95) };
                    this.DP_CASH = new YGCDecimal(14, 2) { DataString = RT12Entry.Substring(109) };
                    this.DP_NOFEE = new YGCDecimal(14, 2) { DataString = RT12Entry.Substring(123) };
                }
                catch
                {
                    if (this.DP_DATE == null) this.DP_DATE = new YGCDate();
                    if (this.DP_CMT == null) this.DP_CMT = new YGCString(20);
                    if (this.DP_MERCH == null) this.DP_MERCH = new YGCDecimal(14, 2);
                    if (this.DP_CASH == null) this.DP_CASH = new YGCDecimal(14, 2);
                    if (this.DP_NOFEE == null) this.DP_NOFEE = new YGCDecimal(14, 2);
                }
            }

            public override Type GetType() { return typeof(RecordType12); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}",
                    base.ToString(),
                    this.DP_DATE,
                    this.DP_CMT,
                    this.DP_MERCH,
                    this.DP_CASH,
                    this.DP_NOFEE);
            }
        }
        #endregion

        #region Record Type 13
        /// <summary>
        /// Record Type 13 - Reseller Information/Chain of Title (Sender to Receiver)
        /// </summary>
        public class RecordType13 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_NAME { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_ADDRESS1 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_ADDRESS2 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_CITY { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_COUNTY { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_STATE { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_ZIP { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString RESELLER_COUNTRY { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCDate RESALE_DATE { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString BILL_OF_SALE_NO { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_NAME { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_ADDRESS1 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_ADDRESS2 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_CITY { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_COUNTY { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_STATE { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_ZIP { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public YGCString SOLD_TO_COUNTRY { get; private set; }
            #endregion

            public RecordType13()
                : base(13)
            {
                this.RESELLER_NAME = new YGCString(30);
                this.RESELLER_ADDRESS1 = new YGCString(50);
                this.RESELLER_ADDRESS2 = new YGCString(50);
                this.RESELLER_CITY = new YGCString(25);
                this.RESELLER_COUNTY = new YGCString(20);
                this.RESELLER_STATE = new YGCString(3);
                this.RESELLER_ZIP = new YGCString(10);
                this.RESELLER_COUNTRY = new YGCString(3);
                this.RESALE_DATE = new YGCDate();
                this.BILL_OF_SALE_NO = new YGCString(30);
                this.SOLD_TO_NAME = new YGCString(30);
                this.SOLD_TO_ADDRESS1 = new YGCString(50);
                this.SOLD_TO_ADDRESS2 = new YGCString(50);
                this.SOLD_TO_CITY = new YGCString(25);
                this.SOLD_TO_COUNTY = new YGCString(20);
                this.SOLD_TO_STATE = new YGCString(3);
                this.SOLD_TO_ZIP = new YGCString(10);
                this.SOLD_TO_COUNTRY = new YGCString(3);
            }
            public RecordType13(string RT13Entry)
                : base(RT13Entry)
            {
                this.RESELLER_NAME = new YGCString(30) { DataString = RT13Entry.Length > 67 ? RT13Entry.Substring(67) : "" };
                this.RESELLER_ADDRESS1 = new YGCString(50) { DataString = RT13Entry.Length > 97 ? RT13Entry.Substring(97) : "" };
                this.RESELLER_ADDRESS2 = new YGCString(50) { DataString = RT13Entry.Length > 147 ? RT13Entry.Substring(147) : "" };
                this.RESELLER_CITY = new YGCString(25) { DataString = RT13Entry.Length > 197 ? RT13Entry.Substring(197) : "" };
                this.RESELLER_COUNTY = new YGCString(20) { DataString = RT13Entry.Length > 222 ? RT13Entry.Substring(222) : "" };
                this.RESELLER_STATE = new YGCString(3) { DataString = RT13Entry.Length > 242 ? RT13Entry.Substring(242) : "" };
                this.RESELLER_ZIP = new YGCString(10) { DataString = RT13Entry.Length > 245 ? RT13Entry.Substring(245) : "" };
                this.RESELLER_COUNTRY = new YGCString(3) { DataString = RT13Entry.Length > 255 ? RT13Entry.Substring(255) : "" };
                this.RESALE_DATE = new YGCDate() { DataString = RT13Entry.Length > 258 ? RT13Entry.Substring(258) : "" };
                this.BILL_OF_SALE_NO = new YGCString(30) { DataString = RT13Entry.Length > 266 ? RT13Entry.Substring(266) : "" };
                this.SOLD_TO_NAME = new YGCString(30) { DataString = RT13Entry.Length > 296 ? RT13Entry.Substring(296) : "" };
                this.SOLD_TO_ADDRESS1 = new YGCString(50) { DataString = RT13Entry.Length > 326 ? RT13Entry.Substring(326) : "" };
                this.SOLD_TO_ADDRESS2 = new YGCString(50) { DataString = RT13Entry.Length > 376 ? RT13Entry.Substring(376) : "" };
                this.SOLD_TO_CITY = new YGCString(25) { DataString = RT13Entry.Length > 426 ? RT13Entry.Substring(426) : "" };
                this.SOLD_TO_COUNTY = new YGCString(20) { DataString = RT13Entry.Length > 451 ? RT13Entry.Substring(451) : "" };
                this.SOLD_TO_STATE = new YGCString(3) { DataString = RT13Entry.Length > 471 ? RT13Entry.Substring(471) : "" };
                this.SOLD_TO_ZIP = new YGCString(10) { DataString = RT13Entry.Length > 474 ? RT13Entry.Substring(474) : "" };
                this.SOLD_TO_COUNTRY = new YGCString(3) { DataString = RT13Entry.Length > 484 ? RT13Entry.Substring(484) : "" };
            }

            public override Type GetType() { return typeof(RecordType13); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}",
                    base.ToString(),
                    this.RESELLER_NAME,
                    this.RESELLER_ADDRESS1,
                    this.RESELLER_ADDRESS2,
                    this.RESELLER_CITY,
                    this.RESELLER_COUNTY,
                    this.RESELLER_STATE,
                    this.RESELLER_ZIP,
                    this.RESELLER_COUNTRY,
                    this.RESALE_DATE,
                    this.BILL_OF_SALE_NO,
                    this.SOLD_TO_NAME,
                    this.SOLD_TO_ADDRESS1,
                    this.SOLD_TO_ADDRESS2,
                    this.SOLD_TO_CITY,
                    this.SOLD_TO_COUNTY,
                    this.SOLD_TO_STATE,
                    this.SOLD_TO_ZIP,
                    this.SOLD_TO_COUNTRY);
            }
        }
        #endregion

        #region Record Type 14
        /// <summary>
        /// Record Type 14 - Block Infinity
        /// </summary>
        public class RecordType14 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Block Infinity Code
            /// </summary>
            public YGCString INF_CODE { get; private set; }
            /// <summary>
            /// Block Infinity Data
            /// </summary>
            public YGCString INF_DATA { get; private set; }
            #endregion

            public RecordType14() : base(14)
            {
                this.INF_CODE = new YGCString(8);
                this.INF_DATA = new YGCString(70);
            }
            public RecordType14(string RT14Entry) : base(RT14Entry)
            {
                try
                {
                    this.INF_CODE = new YGCString(8) { DataString = RT14Entry.Length > 67 ? RT14Entry.Substring(67) : "" };
                    this.INF_DATA = new YGCString(70) { DataString = RT14Entry.Length > 75 ? RT14Entry.Substring(75) : "" };
                }
                catch
                {
                    if (this.INF_CODE == null) this.INF_CODE = new YGCString(8);
                    if (this.INF_DATA == null) this.INF_DATA = new YGCString(70);
                }
            }

            public override Type GetType() { return typeof(RecordType14); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}",
                    base.ToString(),
                    this.INF_CODE,
                    this.INF_DATA);
            }
        }
        #endregion

        #region Record Type 16
        /// <summary>
        /// Record Type 16 - Physical Assets (property/vehicle) (Sender to Receiver)
        /// <para>This record is sent by the sender to record a real estate or automobile asset owned by a debtor. Value = 16.</para>
        /// </summary>
        public class RecordType16 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This identifies which debtor in the account owns the asset. Values are:
            /// <para>1 - Primary debtor</para>
            /// <para>2 - Second debtor</para>
            /// <para>3 - Third debtor</para>
            /// </summary>
            public YGCNumber DEBTOR_NO { get; private set; }
            /// <summary>
            /// This number distinguishes assets owned by the debtor.
            /// </summary>
            public YGCString ASSET_ID { get; private set; }
            /// <summary>
            /// This is the full name of the debtor who owns this asset.
            /// </summary>
            public YGCString ASSET_OWNER { get; private set; }
            /// <summary>
            /// This is the street address where the asset is located.
            /// </summary>
            public YGCString STREET { get; private set; }
            /// <summary>
            /// This is an overflow field for STREET.
            /// </summary>
            public YGCString STREET_2 { get; private set; }
            /// <summary>
            /// This is an overflow field for STREET_2.
            /// </summary>
            public YGCString STREET_3 { get; private set; }
            /// <summary>
            /// This is the city where the asset is located.
            /// </summary>
            public YGCString CITY { get; private set; }
            /// <summary>
            /// This can be used as an alternative or supplement to CITY. It can hold the town or borough where the asset is located.
            /// </summary>
            public YGCString TOWN { get; private set; }
            /// <summary>
            /// This is the county where the asset is located.
            /// </summary>
            public YGCString CNTY { get; private set; }
            /// <summary>
            /// This is the abbreviation of the state where the asset is located.
            /// </summary>
            public YGCString STATE { get; private set; }
            /// <summary>
            /// This is the zip code where the asset is located.
            /// </summary>
            public YGCString ZIP { get; private set; }
            /// <summary>
            /// This is the abbreviation of the country where the asset is located.
            /// </summary>
            public YGCString CNTRY { get; private set; }
            /// <summary>
            /// This is the phone number of the asset location. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString PHONE { get; private set; }
            /// <summary>
            /// This is the block number if this is a land asset.
            /// </summary>
            public YGCString BLOCK { get; private set; }
            /// <summary>
            /// This is the lot number if this is a land asset.
            /// </summary>
            public YGCString LOT { get; private set; }
            /// <summary>
            /// This is the dollar value of the asset.
            /// </summary>
            public YGCDecimal ASSET_VALUE { get; private set; }
            /// <summary>
            /// This is a free-text description of the asset.
            /// </summary>
            public YGCString ASSET_DESC { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the vehicle ID number.
            /// </summary>
            public YGCString ASSET_VIN { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the license plate number.
            /// </summary>
            public YGCString ASSET_LIC_PLATE { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the color.
            /// </summary>
            public YGCString ASSET_COLOR { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the year the auto was made.
            /// </summary>
            public YGCString ASSET_YEAR { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the name of the make.
            /// </summary>
            public YGCString ASSET_MAKE { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the name of the model.
            /// </summary>
            public YGCString ASSET_MODEL { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the repossession file number assigned by the creditor.
            /// </summary>
            public YGCString REPO_FILE_NUM { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the date the repossession occurred.
            /// </summary>
            public YGCDate REPO_D { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the value of the auto. This is the same as ASSET_VALUE.
            /// </summary>
            public YGCDecimal REPO_AMT { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the name of the new owner as stated on the title.
            /// </summary>
            public YGCString CERT_TITLE_NAME { get; private set; }
            /// <summary>
            /// If the asset is an automobile, this is the date the certification title was transferred.
            /// </summary>
            public YGCDate CERT_TITLE_D { get; private set; }
            /// <summary>
            /// If the asset is real estate, this is the foreclosure date.
            /// </summary>
            public YGCDate MORT_FRCL_D { get; private set; }
            /// <summary>
            /// This is the court-issued case number for the foreclosure.
            /// </summary>
            public YGCString MORT_FRCL_FILENO { get; private set; }
            /// <summary>
            /// This is the date the court dismisses the foreclosure for whatever reason.
            /// </summary>
            public YGCDate MORT_FRCL_DISMIS_D { get; private set; }
            /// <summary>
            /// This is the periodic or total payment on the mortgage.
            /// </summary>
            public YGCDecimal MORT_PMT { get; private set; }
            /// <summary>
            /// This is the mortgage interest rate. It takes the decimal form:
            /// <para>Ex: .195 is the value of this field if the interest rate is 19.5%.</para>
            /// </summary>
            public YGCDecimal MORT_RATE { get; private set; }
            /// <summary>
            /// This is the number of the book in the local records that the property is filed in. The liber number can be put here.
            /// </summary>
            public YGCString MORT_BOOK_1 { get; private set; }
            /// <summary>
            /// This is the page number in the book that the property is filed in.
            /// </summary>
            public YGCString MORT_PAGE_1 { get; private set; }
            /// <summary>
            /// This can be used for the portfolio number of the property if MORT_BOOK_1 holds the liber number.
            /// </summary>
            public YGCString MORT_BOOK_2 { get; private set; }
            /// <summary>
            /// This is the page number corresponding to MORT_BOOK_2.
            /// </summary>
            public YGCString MORT_PAGE_2 { get; private set; }
            /// <summary>
            /// This is the date the mortgage is entered in the local record book.
            /// </summary>
            public YGCDate MORT_RECRD_D { get; private set; }
            /// <summary>
            /// This is the date the mortgage is due.
            /// </summary>
            public YGCDate MORT_DUE_D { get; private set; }
            /// <summary>
            /// This is the number given by the land records office in the county where the property is located.
            /// </summary>
            public YGCString LIEN_FILE_NUM { get; private set; }
            /// <summary>
            /// This is the case number assigned by the bank holding the lien.
            /// </summary>
            public YGCString LIEN_CASE_NUM { get; private set; }
            /// <summary>
            /// This is the date the lien is established.
            /// </summary>
            public YGCDate LIEN_D { get; private set; }
            /// <summary>
            /// This is the number of the book in the local records that the lien is filed in.
            /// </summary>
            public YGCString LIEN_BOOK { get; private set; }
            /// <summary>
            /// This is the page number in the book that the lien is filed in.
            /// </summary>
            public YGCString LIEN_PAGE { get; private set; }
            /// <summary>
            /// Was there a response to the lien? Values are Y/N.
            /// </summary>
            public YGCBool LIEN_AOL { get; private set; }
            /// <summary>
            /// This is the date the lien release (the confirmation of the lien's payment) is filed.
            /// </summary>
            public YGCDate LIEN_RLSE_D { get; private set; }
            /// <summary>
            /// This is the number of the book in the local records the lien release is filed in.
            /// </summary>
            public YGCString LIEN_RLSE_BOOK { get; private set; }
            /// <summary>
            /// This is the page number in the book in the local records the lien release is filed in.
            /// </summary>
            public YGCString LIEN_RLSE_PAGE { get; private set; }
            /// <summary>
            /// This is the date when the lien is foreclosed upon.
            /// </summary>
            public YGCDate LIEN_LITIG_D { get; private set; }
            /// <summary>
            /// This is the number of the book in the local records that the lien is filed in when it is foreclosed upon.
            /// </summary>
            public YGCString LIEN_LITIG_BOOK { get; private set; }
            /// <summary>
            /// This is the page number in the book that the lien is filed in when it is foreclosed upon.
            /// </summary>
            public YGCString LIEN_LITIG_PAGE { get; private set; }
            #endregion

            public RecordType16() : base(16)
            {
                this.DEBTOR_NO = new YGCNumber(3);
                this.ASSET_ID = new YGCString(3);
                this.ASSET_OWNER = new YGCString(60);
                this.STREET = new YGCString(40);
                this.STREET_2 = new YGCString(40);
                this.STREET_3 = new YGCString(40);
                this.CITY = new YGCString(30);
                this.TOWN = new YGCString(30);
                this.CNTY = new YGCString(30);
                this.STATE = new YGCString(2);
                this.ZIP = new YGCString(20);
                this.CNTRY = new YGCString(3);
                this.PHONE = new YGCString(20);
                this.BLOCK = new YGCString(10);
                this.LOT = new YGCString(10);
                this.ASSET_VALUE = new YGCDecimal(9, 2);
                this.ASSET_DESC = new YGCString(40);
                this.ASSET_VIN = new YGCString(20);
                this.ASSET_LIC_PLATE = new YGCString(10);
                this.ASSET_COLOR = new YGCString(15);
                this.ASSET_YEAR = new YGCString(4);
                this.ASSET_MAKE = new YGCString(20);
                this.ASSET_MODEL = new YGCString(20);
                this.REPO_FILE_NUM = new YGCString(15);
                this.REPO_D = new YGCDate();
                this.REPO_AMT = new YGCDecimal(9, 2);
                this.CERT_TITLE_NAME = new YGCString(40);
                this.CERT_TITLE_D = new YGCDate();
                this.MORT_FRCL_D = new YGCDate();
                this.MORT_FRCL_FILENO = new YGCString(15);
                this.MORT_FRCL_DISMIS_D = new YGCDate();
                this.MORT_PMT = new YGCDecimal(7, 2);
                this.MORT_RATE = new YGCDecimal(4, 3);
                this.MORT_BOOK_1 = new YGCString(5);
                this.MORT_PAGE_1 = new YGCString(5);
                this.MORT_BOOK_2 = new YGCString(5);
                this.MORT_PAGE_2 = new YGCString(5);
                this.MORT_RECRD_D = new YGCDate();
                this.MORT_DUE_D = new YGCDate();
                this.LIEN_FILE_NUM = new YGCString(15);
                this.LIEN_CASE_NUM = new YGCString(15);
                this.LIEN_D = new YGCDate();
                this.LIEN_BOOK = new YGCString(5);
                this.LIEN_PAGE = new YGCString(5);
                this.LIEN_AOL = new YGCBool(1, "Y", "N");
                this.LIEN_RLSE_D = new YGCDate();
                this.LIEN_RLSE_BOOK = new YGCString(5);
                this.LIEN_RLSE_PAGE = new YGCString(5);
                this.LIEN_LITIG_D = new YGCDate();
                this.LIEN_LITIG_BOOK = new YGCString(5);
                this.LIEN_LITIG_PAGE = new YGCString(5);
            }
            public RecordType16(string RT16Entry) : base(RT16Entry)
            {
                try
                {
                    this.DEBTOR_NO = new YGCNumber(3) { DataString = RT16Entry.Substring(67) };
                    this.ASSET_ID = new YGCString(3) { DataString = RT16Entry.Substring(70) };
                    this.ASSET_OWNER = new YGCString(60) { DataString = RT16Entry.Substring(73) };
                    this.STREET = new YGCString(40) { DataString = RT16Entry.Substring(133) };
                    this.STREET_2 = new YGCString(40) { DataString = RT16Entry.Substring(173) };
                    this.STREET_3 = new YGCString(40) { DataString = RT16Entry.Substring(213) };
                    this.CITY = new YGCString(30) { DataString = RT16Entry.Substring(253) };
                    this.TOWN = new YGCString(30) { DataString = RT16Entry.Substring(283) };
                    this.CNTY = new YGCString(30) { DataString = RT16Entry.Substring(313) };
                    this.STATE = new YGCString(2) { DataString = RT16Entry.Substring(343) };
                    this.ZIP = new YGCString(20) { DataString = RT16Entry.Substring(345) };
                    this.CNTRY = new YGCString(3) { DataString = RT16Entry.Substring(365) };
                    this.PHONE = new YGCString(20) { DataString = RT16Entry.Substring(368) };
                    this.BLOCK = new YGCString(10) { DataString = RT16Entry.Substring(388) };
                    this.LOT = new YGCString(10) { DataString = RT16Entry.Substring(398) };
                    this.ASSET_VALUE = new YGCDecimal(9, 2) { DataString = RT16Entry.Substring(408) };
                    this.ASSET_DESC = new YGCString(40) { DataString = RT16Entry.Substring(417) };
                    this.ASSET_VIN = new YGCString(20) { DataString = RT16Entry.Substring(457) };
                    this.ASSET_LIC_PLATE = new YGCString(10) { DataString = RT16Entry.Substring(477) };
                    this.ASSET_COLOR = new YGCString(15) { DataString = RT16Entry.Substring(487) };
                    this.ASSET_YEAR = new YGCString(4) { DataString = RT16Entry.Substring(502) };
                    this.ASSET_MAKE = new YGCString(20) { DataString = RT16Entry.Substring(506) };
                    this.ASSET_MODEL = new YGCString(20) { DataString = RT16Entry.Substring(526) };
                    this.REPO_FILE_NUM = new YGCString(15) { DataString = RT16Entry.Substring(546) };
                    this.REPO_D = new YGCDate() { DataString = RT16Entry.Substring(561) };
                    this.REPO_AMT = new YGCDecimal(9, 2) { DataString = RT16Entry.Substring(569) };
                    this.CERT_TITLE_NAME = new YGCString(40) { DataString = RT16Entry.Substring(578) };
                    this.CERT_TITLE_D = new YGCDate() { DataString = RT16Entry.Substring(618) };
                    this.MORT_FRCL_D = new YGCDate() { DataString = RT16Entry.Substring(626) };
                    this.MORT_FRCL_FILENO = new YGCString(15) { DataString = RT16Entry.Substring(634) };
                    this.MORT_FRCL_DISMIS_D = new YGCDate() { DataString = RT16Entry.Substring(649) };
                    this.MORT_PMT = new YGCDecimal(7, 2) { DataString = RT16Entry.Substring(657) };
                    this.MORT_RATE = new YGCDecimal(4, 3) { DataString = RT16Entry.Substring(664) };
                    this.MORT_BOOK_1 = new YGCString(5) { DataString = RT16Entry.Substring(668) };
                    this.MORT_PAGE_1 = new YGCString(5) { DataString = RT16Entry.Substring(673) };
                    this.MORT_BOOK_2 = new YGCString(5) { DataString = RT16Entry.Substring(678) };
                    this.MORT_PAGE_2 = new YGCString(5) { DataString = RT16Entry.Substring(683) };
                    this.MORT_RECRD_D = new YGCDate() { DataString = RT16Entry.Substring(688) };
                    this.MORT_DUE_D = new YGCDate() { DataString = RT16Entry.Substring(696) };
                    this.LIEN_FILE_NUM = new YGCString(15) { DataString = RT16Entry.Substring(704) };
                    this.LIEN_CASE_NUM = new YGCString(15) { DataString = RT16Entry.Substring(719) };
                    this.LIEN_D = new YGCDate() { DataString = RT16Entry.Substring(734) };
                    this.LIEN_BOOK = new YGCString(5) { DataString = RT16Entry.Substring(742) };
                    this.LIEN_PAGE = new YGCString(5) { DataString = RT16Entry.Substring(747) };
                    this.LIEN_AOL = new YGCBool(1, "Y", "N") { DataString = RT16Entry.Substring(752) };
                    this.LIEN_RLSE_D = new YGCDate() { DataString = RT16Entry.Substring(753) };
                    this.LIEN_RLSE_BOOK = new YGCString(5) { DataString = RT16Entry.Substring(761) };
                    this.LIEN_RLSE_PAGE = new YGCString(5) { DataString = RT16Entry.Substring(766) };
                    this.LIEN_LITIG_D = new YGCDate() { DataString = RT16Entry.Substring(771) };
                    this.LIEN_LITIG_BOOK = new YGCString(5) { DataString = RT16Entry.Substring(779) };
                    this.LIEN_LITIG_PAGE = new YGCString(5) { DataString = RT16Entry.Substring(784) };
                }
                catch
                {
                    if (this.DEBTOR_NO == null) this.DEBTOR_NO = new YGCNumber(3);
                    if (this.ASSET_ID == null) this.ASSET_ID = new YGCString(3);
                    if (this.ASSET_OWNER == null) this.ASSET_OWNER = new YGCString(60);
                    if (this.STREET == null) this.STREET = new YGCString(40);
                    if (this.STREET_2 == null) this.STREET_2 = new YGCString(40);
                    if (this.STREET_3 == null) this.STREET_3 = new YGCString(40);
                    if (this.CITY == null) this.CITY = new YGCString(30);
                    if (this.TOWN == null) this.TOWN = new YGCString(30);
                    if (this.CNTY == null) this.CNTY = new YGCString(30);
                    if (this.STATE == null) this.STATE = new YGCString(2);
                    if (this.ZIP == null) this.ZIP = new YGCString(20);
                    if (this.CNTRY == null) this.CNTRY = new YGCString(3);
                    if (this.PHONE == null) this.PHONE = new YGCString(20);
                    if (this.BLOCK == null) this.BLOCK = new YGCString(10);
                    if (this.LOT == null) this.LOT = new YGCString(10);
                    if (this.ASSET_VALUE == null) this.ASSET_VALUE = new YGCDecimal(9, 2);
                    if (this.ASSET_DESC == null) this.ASSET_DESC = new YGCString(40);
                    if (this.ASSET_VIN == null) this.ASSET_VIN = new YGCString(20);
                    if (this.ASSET_LIC_PLATE == null) this.ASSET_LIC_PLATE = new YGCString(10);
                    if (this.ASSET_COLOR == null) this.ASSET_COLOR = new YGCString(15);
                    if (this.ASSET_YEAR == null) this.ASSET_YEAR = new YGCString(4);
                    if (this.ASSET_MAKE == null) this.ASSET_MAKE = new YGCString(20);
                    if (this.ASSET_MODEL == null) this.ASSET_MODEL = new YGCString(20);
                    if (this.REPO_FILE_NUM == null) this.REPO_FILE_NUM = new YGCString(15);
                    if (this.REPO_D == null) this.REPO_D = new YGCDate();
                    if (this.REPO_AMT == null) this.REPO_AMT = new YGCDecimal(9, 2);
                    if (this.CERT_TITLE_NAME == null) this.CERT_TITLE_NAME = new YGCString(40);
                    if (this.CERT_TITLE_D == null) this.CERT_TITLE_D = new YGCDate();
                    if (this.MORT_FRCL_D == null) this.MORT_FRCL_D = new YGCDate();
                    if (this.MORT_FRCL_FILENO == null) this.MORT_FRCL_FILENO = new YGCString(15);
                    if (this.MORT_FRCL_DISMIS_D == null) this.MORT_FRCL_DISMIS_D = new YGCDate();
                    if (this.MORT_PMT == null) this.MORT_PMT = new YGCDecimal(7, 2);
                    if (this.MORT_RATE == null) this.MORT_RATE = new YGCDecimal(4, 3);
                    if (this.MORT_BOOK_1 == null) this.MORT_BOOK_1 = new YGCString(5);
                    if (this.MORT_PAGE_1 == null) this.MORT_PAGE_1 = new YGCString(5);
                    if (this.MORT_BOOK_2 == null) this.MORT_BOOK_2 = new YGCString(5);
                    if (this.MORT_PAGE_2 == null) this.MORT_PAGE_2 = new YGCString(5);
                    if (this.MORT_RECRD_D == null) this.MORT_RECRD_D = new YGCDate();
                    if (this.MORT_DUE_D == null) this.MORT_DUE_D = new YGCDate();
                    if (this.LIEN_FILE_NUM == null) this.LIEN_FILE_NUM = new YGCString(15);
                    if (this.LIEN_CASE_NUM == null) this.LIEN_CASE_NUM = new YGCString(15);
                    if (this.LIEN_D == null) this.LIEN_D = new YGCDate();
                    if (this.LIEN_BOOK == null) this.LIEN_BOOK = new YGCString(5);
                    if (this.LIEN_PAGE == null) this.LIEN_PAGE = new YGCString(5);
                    if (this.LIEN_AOL == null) this.LIEN_AOL = new YGCBool(1, "Y", "N");
                    if (this.LIEN_RLSE_D == null) this.LIEN_RLSE_D = new YGCDate();
                    if (this.LIEN_RLSE_BOOK == null) this.LIEN_RLSE_BOOK = new YGCString(5);
                    if (this.LIEN_RLSE_PAGE == null) this.LIEN_RLSE_PAGE = new YGCString(5);
                    if (this.LIEN_LITIG_D == null) this.LIEN_LITIG_D = new YGCDate();
                    if (this.LIEN_LITIG_BOOK == null) this.LIEN_LITIG_BOOK = new YGCString(5);
                    if (this.LIEN_LITIG_PAGE == null) this.LIEN_LITIG_PAGE = new YGCString(5);
                }
            }

            public override Type GetType() { return typeof(RecordType16); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}",
                    base.ToString(),
                    this.DEBTOR_NO,
                    this.ASSET_ID,
                    this.ASSET_OWNER,
                    this.STREET,
                    this.STREET_2,
                    this.STREET_3,
                    this.CITY,
                    this.TOWN,
                    this.CNTY,
                    this.STATE,
                    this.ZIP,
                    this.CNTRY,
                    this.PHONE,
                    this.BLOCK,
                    this.LOT,
                    this.ASSET_VALUE,
                    this.ASSET_DESC,
                    this.ASSET_VIN,
                    this.ASSET_LIC_PLATE,
                    this.ASSET_COLOR,
                    this.ASSET_YEAR,
                    this.ASSET_MAKE,
                    this.ASSET_MODEL,
                    this.REPO_FILE_NUM,
                    this.REPO_D,
                    this.REPO_AMT,
                    this.CERT_TITLE_NAME,
                    this.CERT_TITLE_D,
                    this.MORT_FRCL_D,
                    this.MORT_FRCL_FILENO,
                    this.MORT_FRCL_DISMIS_D,
                    this.MORT_PMT,
                    this.MORT_RATE,
                    this.MORT_BOOK_1,
                    this.MORT_PAGE_1,
                    this.MORT_BOOK_2,
                    this.MORT_PAGE_2,
                    this.MORT_RECRD_D,
                    this.MORT_DUE_D,
                    this.LIEN_FILE_NUM,
                    this.LIEN_CASE_NUM,
                    this.LIEN_D,
                    this.LIEN_BOOK,
                    this.LIEN_PAGE,
                    this.LIEN_AOL,
                    this.LIEN_RLSE_D,
                    this.LIEN_RLSE_BOOK,
                    this.LIEN_RLSE_PAGE,
                    this.LIEN_LITIG_D,
                    this.LIEN_LITIG_BOOK,
                    this.LIEN_LITIG_PAGE);
            }
        }
        #endregion

        #region Record Type 18
        /// <summary>
        /// Record Type 18 - Inventory Reconciliation Record (Sender to Receiver)						
        /// <para>This record is used to verify that a sender's account is in a receiver's collection system. Typically, you would send this to Client Services outside of the normal YGC workflow to reconcile accounts with your receiver. If you deliver this record in the normal YGC workflow, it will still be delivered but not uploaded to YouveGotReports. This record is also used to acknowledge a reconciliation record (type 38) from a receiver. Value = 18.</para>
        /// </summary>
        public class RecordType18 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the date the account was placed.
            /// </summary>
            public YGCDate DPLACED { get; private set; }
            /// <summary>
            /// This is the primary debtor's name. The format is Lastname/Firstname.
            /// </summary>
            public YGCString DEBT_NAME { get; private set; }
            /// <summary>
            /// This is the name of the creditor company. You can use CRED_NAME2 for overflow.
            /// </summary>
            public YGCString CRED_NAME { get; private set; }
            /// <summary>
            /// This is the dollar amount of the placement due.
            /// </summary>
            public YGCDecimal D1_BAL { get; private set; }
            /// <summary>
            /// This is the last date that interest on the debt was calculated.
            /// </summary>
            public YGCDate IDATE { get; private set; }
            /// <summary>
            /// This is the dollar amount of total interest accrued.
            /// </summary>
            public YGCDecimal IAMT { get; private set; }
            /// <summary>
            /// This is the dollar amount of interest actually due as of IDATE, which may not equal IAMT.
            /// </summary>
            public YGCDecimal IDUE { get; private set; }
            /// <summary>
            /// This is the total dollar amount already paid towards the debt.
            /// </summary>
            public YGCDecimal PAID { get; private set; }
            /// <summary>
            /// This is the current administrative costs incurred for the account.
            /// </summary>
            public YGCDecimal COST_BAL { get; private set; }
            /// <summary>
            /// This is the debtor's city and state.
            ///<para>Format this field as City ST or City,ST.</para>
            ///<para>(Example: Linden NJ or Linden,NJ)</para>
            /// </summary>
            public YGCString DEBT_CS { get; private set; }
            /// <summary>
            /// This is the debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen.
            /// </summary>
            public YGCString DEBT_ZIP { get; private set; }
            /// <summary>
            /// This is an overflow field for CRED_NAME.
            /// </summary>
            public YGCString CRED_NAME2 { get; private set; }
            /// <summary>
            /// This is the commission percentage the receiver earns as determined by the sender. There is no set numeric format.
            /// </summary>
            public YGCString COMM { get; private set; }
            /// <summary>
            /// This is the percentage the receiver earns for the suit. There is no set numeric format.
            /// </summary>
            public YGCString SFEE { get; private set; }
            /// <summary>
            /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
            /// </summary>
            public YGCString RFILE { get; private set; }
            /// <summary>
            /// This is the standard code for the debtor's country.
            /// </summary>
            public YGCString DEBT_CNTRY { get; private set; }
            /// <summary>
            /// This is the dollar amount of the original principal due at time of placement.
            /// </summary>
            public YGCDecimal ORIGINAL_PLACED_BALANCE { get; private set; }
            /// <summary>
            /// Date the placement was closed. Leave blank for open accounts.
            /// </summary>
            public YGCDate CLOSED_DATE { get; private set; }
            /// <summary>
            /// Status Code by which the placement was closed. Typically this is a valid PCODE. Leave blank for open accounts.
            /// </summary>
            public YGCString CLOSED_CODE { get; private set; }
            #endregion

            public RecordType18() : base(18)
            {
                this.DPLACED = new YGCDate();
                this.DEBT_NAME = new YGCString(30);
                this.CRED_NAME = new YGCString(30);
                this.D1_BAL = new YGCDecimal(14, 2);
                this.IDATE = new YGCDate();
                this.IAMT = new YGCDecimal(14, 2);
                this.IDUE = new YGCDecimal(14, 2);
                this.PAID = new YGCDecimal(14, 2);
                this.COST_BAL = new YGCDecimal(14, 2);
                this.DEBT_CS = new YGCString(23);
                this.DEBT_ZIP = new YGCString(9);
                this.CRED_NAME2 = new YGCString(25);
                this.COMM = new YGCString(4);
                this.SFEE = new YGCString(4);
                this.RFILE = new YGCString(8);
                this.DEBT_CNTRY = new YGCString(3);
                this.ORIGINAL_PLACED_BALANCE = new YGCDecimal(14, 2);
                this.CLOSED_DATE = new YGCDate();
                this.CLOSED_CODE = new YGCString(8);
            }
            public RecordType18(string RT18Entry) : base(RT18Entry)
            {
                try
                {
                    this.DPLACED = new YGCDate() { DataString = RT18Entry.Substring(67) };
                    this.DEBT_NAME = new YGCString(30) { DataString = RT18Entry.Substring(75) };
                    this.CRED_NAME = new YGCString(30) { DataString = RT18Entry.Substring(105) };
                    this.D1_BAL = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(135) };
                    this.IDATE = new YGCDate() { DataString = RT18Entry.Substring(149) };
                    this.IAMT = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(157) };
                    this.IDUE = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(171) };
                    this.PAID = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(185) };
                    this.COST_BAL = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(199) };
                    this.DEBT_CS = new YGCString(23) { DataString = RT18Entry.Substring(213) };
                    this.DEBT_ZIP = new YGCString(9) { DataString = RT18Entry.Substring(236) };
                    this.CRED_NAME2 = new YGCString(25) { DataString = RT18Entry.Substring(245) };
                    this.COMM = new YGCString(4) { DataString = RT18Entry.Substring(270) };
                    this.SFEE = new YGCString(4) { DataString = RT18Entry.Substring(274) };
                    this.RFILE = new YGCString(8) { DataString = RT18Entry.Substring(278) };
                    this.DEBT_CNTRY = new YGCString(3) { DataString = RT18Entry.Substring(286) };
                    this.ORIGINAL_PLACED_BALANCE = new YGCDecimal(14, 2) { DataString = RT18Entry.Substring(289) };
                    this.CLOSED_DATE = new YGCDate() { DataString = RT18Entry.Substring(303) };
                    this.CLOSED_CODE = new YGCString(8) { DataString = RT18Entry.Substring(311) };
                }
                catch
                {
                    if (this.DPLACED == null) this.DPLACED = new YGCDate();
                    if (this.DEBT_NAME == null) this.DEBT_NAME = new YGCString(30);
                    if (this.CRED_NAME == null) this.CRED_NAME = new YGCString(30);
                    if (this.D1_BAL == null) this.D1_BAL = new YGCDecimal(14, 2);
                    if (this.IDATE == null) this.IDATE = new YGCDate();
                    if (this.IAMT == null) this.IAMT = new YGCDecimal(14, 2);
                    if (this.IDUE == null) this.IDUE = new YGCDecimal(14, 2);
                    if (this.PAID == null) this.PAID = new YGCDecimal(14, 2);
                    if (this.COST_BAL == null) this.COST_BAL = new YGCDecimal(14, 2);
                    if (this.DEBT_CS == null) this.DEBT_CS = new YGCString(23);
                    if (this.DEBT_ZIP == null) this.DEBT_ZIP = new YGCString(9);
                    if (this.CRED_NAME2 == null) this.CRED_NAME2 = new YGCString(25);
                    if (this.COMM == null) this.COMM = new YGCString(4);
                    if (this.SFEE == null) this.SFEE = new YGCString(4);
                    if (this.RFILE == null) this.RFILE = new YGCString(8);
                    if (this.DEBT_CNTRY == null) this.DEBT_CNTRY = new YGCString(3);
                    if (this.ORIGINAL_PLACED_BALANCE == null) this.ORIGINAL_PLACED_BALANCE = new YGCDecimal(14, 2);
                    if (this.CLOSED_DATE == null) this.CLOSED_DATE = new YGCDate();
                    if (this.CLOSED_CODE == null) this.CLOSED_CODE = new YGCString(8);
                }
            }

            public override Type GetType() { return typeof(RecordType18); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}",
                    base.ToString(),
                    this.DPLACED,
                    this.DEBT_NAME,
                    this.CRED_NAME,
                    this.D1_BAL,
                    this.IDATE,
                    this.IAMT,
                    this.IDUE,
                    this.PAID,
                    this.COST_BAL,
                    this.DEBT_CS,
                    this.DEBT_ZIP,
                    this.CRED_NAME2,
                    this.COMM,
                    this.SFEE,
                    this.RFILE,
                    this.DEBT_CNTRY,
                    this.ORIGINAL_PLACED_BALANCE,
                    this.CLOSED_DATE,
                    this.CLOSED_CODE);
            }
        }
        #endregion

        #region Record Type 19
        /// <summary>
        /// Record Type 19 - Bankruptcy (Sender to Receiver)
        /// </summary>
        public class RecordType19 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This identifies which debtor in the account is filing for bankruptcy.
            /// <para></para>
            /// <para>1 = Primary debtor</para>
            /// <para>2 = Second debtor</para>
            /// <para>3 = Third debtor</para>
            /// <para></para>
            /// <para>The primary debtor is determined by the latest record 02 or 31 and the second and third debtors are determined by the latest record 03 or 33.<para>
            /// </summary>
            public YGCNumber DBTR_NUM { get; private set; }
            /// <summary>
            /// Chapter within the bankruptcy code; typically 7 or 13 for individuals.
            /// </summary>
            public YGCNumber CHAPTER { get; private set; }
            /// <summary>
            /// This is the court-issued case number for the bankruptcy.
            /// </summary>
            public YGCString BK_FILENO { get; private set; }
            /// <summary>
            /// This is a non-restrictive field describing where the bankruptcy was filed. You can use this for the jurisdiction of the court.
            /// </summary>
            public YGCString LOC { get; private set; }
            /// <summary>
            /// This is the date the bankruptcy was filed.
            /// </summary>
            public YGCDate FILED_DATE { get; private set; }
            /// <summary>
            /// If something was handled incorrectly, the bankruptcy may be dismissed; this is the date of dismissal.
            /// </summary>
            public YGCDate DSMIS_DATE { get; private set; }
            /// <summary>
            /// The date a discharge is issued for the debtor. The discharge relieves the debtor of personal liability outside of what is put up as collateral.
            /// </summary>
            public YGCDate DSCHG_DATE { get; private set; }
            /// <summary>
            /// This is the date the court actually adjudicates the case.
            /// </summary>
            public YGCDate CLOSE_DATE { get; private set; }
            /// <summary>
            /// This is the date the bankruptcy is converted from one chapter to another, usually from 13 to 7.
            /// </summary>
            public YGCDate CNVRT_DATE { get; private set; }
            /// <summary>
            /// This is the date that the initial meeting takes place between creditors and debtors, usually 30 days after FILED_DATE.
            /// </summary>
            public YGCDate MTG_341_DATE { get; private set; }
            /// <summary>
            /// This is the time of day that the initial meeting takes place between creditors and debtors.  It should be in one of the following formats:
            /// <para></para>
            /// <para>HH:MM:SS if using 24-hour notation</para>
            /// <para>HH:MM AM if using 12-hour notation for before noon</para>
            /// <para>HH:MM PM if using 12-hour notation for after noon</para>
            /// </summary>
            public YGCTime MTG_341_TIME { get; private set; }
            /// <summary>
            /// This is the location that the initial meeting takes place between creditors and debtors. There is no format restriction.
            /// </summary>
            public YGCString MTG_341_LOC { get; private set; }
            /// <summary>
            /// These are the initials of the name of the bankruptcy judge.
            /// </summary>
            public YGCString JUDGE_INIT { get; private set; }
            /// <summary>
            /// The debtor may choose to re-affirm the debt that would have been discharged by the bankruptcy. This is the amount the debtor agrees to pay in full; the re-affirmation survives the bankruptcy.
            /// </summary>
            public YGCDecimal REAF_AMT { get; private set; }
            /// <summary>
            /// This is the date when the re-affirmation was signed.
            /// </summary>
            public YGCDate REAF_DATE { get; private set; }
            /// <summary>
            /// This is the periodic payment in a Chapter 11 or 13 bankruptcy or a lump sum payment of a Chapter 7 bankruptcy.
            /// </summary>
            public YGCDecimal PAY_AMT { get; private set; }
            /// <summary>
            /// This is the date that the payment arrangement was agreed upon.
            /// </summary>
            public YGCDate PAY_DATE { get; private set; }
            /// <summary>
            /// This is the date the judge approves the payment plan.
            /// </summary>
            public YGCDate CONF_DATE { get; private set; }
            /// <summary>
            /// This is the date the debtor caught up with paying all arrearages, fees and interest.
            /// </summary>
            public YGCDate CURE_DATE { get; private set; }
            /// <summary>
            /// If the bankruptcy proceedings were put on hold, this is the date the stay was lifted.
            /// </summary>
            public YGCDate STAY_LIFTED_DATE { get; private set; }
            #endregion

            public RecordType19()
                : base(19)
            {
                this.DBTR_NUM = new YGCNumber(3);
                this.CHAPTER = new YGCNumber(3);
                this.BK_FILENO = new YGCString(15);
                this.LOC = new YGCString(40);
                this.FILED_DATE = new YGCDate();
                this.DSMIS_DATE = new YGCDate();
                this.DSCHG_DATE = new YGCDate();
                this.CLOSE_DATE = new YGCDate();
                this.CNVRT_DATE = new YGCDate();
                this.MTG_341_DATE = new YGCDate();
                this.MTG_341_TIME = new YGCTime();
                this.MTG_341_LOC = new YGCString(40);
                this.JUDGE_INIT = new YGCString(3);
                this.REAF_AMT = new YGCDecimal(14, 2);
                this.REAF_DATE = new YGCDate();
                this.PAY_AMT = new YGCDecimal(14, 2);
                this.PAY_DATE = new YGCDate();
                this.CONF_DATE = new YGCDate();
                this.CURE_DATE = new YGCDate();
                this.STAY_LIFTED_DATE = new YGCDate();
            }
            public RecordType19(string RT19Entry)
                : base(RT19Entry)
            {
                this.DBTR_NUM = new YGCNumber(3) { DataString = RT19Entry.Length > 67 ? RT19Entry.Substring(67) : "" };
                this.CHAPTER = new YGCNumber(3) { DataString = RT19Entry.Length > 70 ? RT19Entry.Substring(70) : "" };
                this.BK_FILENO = new YGCString(15) { DataString = RT19Entry.Length > 73 ? RT19Entry.Substring(73) : "" };
                this.LOC = new YGCString(40) { DataString = RT19Entry.Length > 88 ? RT19Entry.Substring(88) : "" };
                this.FILED_DATE = new YGCDate() { DataString = RT19Entry.Length > 128 ? RT19Entry.Substring(128) : "" };
                this.DSMIS_DATE = new YGCDate() { DataString = RT19Entry.Length > 136 ? RT19Entry.Substring(136) : "" };
                this.DSCHG_DATE = new YGCDate() { DataString = RT19Entry.Length > 144 ? RT19Entry.Substring(144) : "" };
                this.CLOSE_DATE = new YGCDate() { DataString = RT19Entry.Length > 152 ? RT19Entry.Substring(152) : "" };
                this.CNVRT_DATE = new YGCDate() { DataString = RT19Entry.Length > 160 ? RT19Entry.Substring(160) : "" };
                this.MTG_341_DATE = new YGCDate() { DataString = RT19Entry.Length > 168 ? RT19Entry.Substring(168) : "" };
                this.MTG_341_TIME = new YGCTime() { DataString = RT19Entry.Length > 176 ? RT19Entry.Substring(176) : "" };
                this.MTG_341_LOC = new YGCString(40) { DataString = RT19Entry.Length > 184 ? RT19Entry.Substring(184) : "" };
                this.JUDGE_INIT = new YGCString(3) { DataString = RT19Entry.Length > 224 ? RT19Entry.Substring(224) : "" };
                this.REAF_AMT = new YGCDecimal(14, 2) { DataString = RT19Entry.Length > 227 ? RT19Entry.Substring(227) : "" };
                this.REAF_DATE = new YGCDate() { DataString = RT19Entry.Length > 241 ? RT19Entry.Substring(241) : "" };
                this.PAY_AMT = new YGCDecimal(14, 2) { DataString = RT19Entry.Length > 249 ? RT19Entry.Substring(249) : "" };
                this.PAY_DATE = new YGCDate() { DataString = RT19Entry.Length > 263 ? RT19Entry.Substring(263) : "" };
                this.CONF_DATE = new YGCDate() { DataString = RT19Entry.Length > 271 ? RT19Entry.Substring(271) : "" };
                this.CURE_DATE = new YGCDate() { DataString = RT19Entry.Length > 279 ? RT19Entry.Substring(279) : "" };
                this.STAY_LIFTED_DATE = new YGCDate() { DataString = RT19Entry.Length > 287 ? RT19Entry.Substring(287) : "" };
            }

            public override Type GetType() { return typeof(RecordType19); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}",
                    base.ToString(),
                    this.DBTR_NUM,
                    this.CHAPTER,
                    this.BK_FILENO,
                    this.LOC,
                    this.FILED_DATE,
                    this.DSMIS_DATE,
                    this.DSCHG_DATE,
                    this.CLOSE_DATE,
                    this.CNVRT_DATE,
                    this.MTG_341_DATE,
                    this.MTG_341_TIME,
                    this.MTG_341_LOC,
                    this.JUDGE_INIT,
                    this.REAF_AMT,
                    this.REAF_DATE,
                    this.PAY_AMT,
                    this.PAY_DATE,
                    this.CONF_DATE,
                    this.CURE_DATE,
                    this.STAY_LIFTED_DATE
                    );
            }
        }
        #endregion

        #region Record Type 20
        /// <summary>
        /// Record Type 20 - Balance/Interest Update (Sender to Receiver)						
        /// <para>This record is used by senders to update account balance or interest information. YGC recommends sending this record whenever a payment is received and therefore when a record 12 or 30 is sent. It should also be sent when the legal status of the account changes, such as a suit is filed or a judgment is rendered. Value = 20</para>
        /// </summary>
        public class RecordType20 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form.
            /// </summary>
            public YGCDecimal RATES_PRE { get; set; }
            /// <summary>
            /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form.
            /// </summary>
            public YGCDecimal RATES_POST { get; set; }
            /// <summary>
            /// This is the interest dollar amount accrued each day.
            /// </summary>
            public YGCDecimal PER_DIEM { get; set; }
            /// <summary>
            /// This is the dollar amount upon which the interest is accrued. It is usually just the principal, but can include other factors such as attorney fees and costs.
            /// </summary>
            public YGCDecimal INT_BASE { get; set; }
            /// <summary>
            /// This is the total interest accrued to date.
            /// </summary>
            public YGCDecimal IAMOUNT { get; set; }
            /// <summary>
            /// The total amount paid to date applied towards interest.
            /// </summary>
            public YGCDecimal IPAID { get; set; }
            /// <summary>
            /// This is the date the interest was last calculated.
            /// </summary>
            public YGCDate IDATE { get; set; }
            /// <summary>
            /// This is the total principal owed on the debt.
            /// </summary>
            public YGCDecimal PRIN_AMT { get; set; }
            /// <summary>
            /// The total amount paid to date applied towards principal.
            /// </summary>
            public YGCDecimal PRIN_PAID { get; set; }
            /// <summary>
            /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
            /// </summary>
            public YGCDecimal CNTRCT_AMT { get; set; }
            /// <summary>
            /// This is the amount actually paid towards the contract fee (CNTRCT_AMT).
            /// </summary>
            public YGCDecimal CNTRCT_PAID { get; set; }
            /// <summary>
            /// This is the statutory fee awarded only to the attorney, determined by the debtor state.
            /// </summary>
            public YGCDecimal STAT_AMT { get; set; }
            /// <summary>
            /// This is the amount actually paid towards the statutory fee (STAT_AMT).
            /// </summary>
            public YGCDecimal STAT_PAID { get; set; }
            /// <summary>
            /// This is the dollar amount of the legal costs billed.
            /// </summary>
            public YGCDecimal COST_AMT { get; set; }
            /// <summary>
            /// This is the dollar amount of the legal costs actually paid.
            /// </summary>
            public YGCDecimal COST_PAID { get; set; }
            /// <summary>
            /// This is the debtor principal and costs due. The interest is not to be included here.
            /// </summary>
            public YGCDecimal DBAL { get; set; }
            /// <summary>
            /// This is the dollar amount of the debtor interest due.
            /// </summary>
            public YGCDecimal IBAL { get; set; }
            #endregion

            public RecordType20() : base(20)
            {
                this.RATES_PRE = new YGCDecimal(5, 4);
                this.RATES_POST = new YGCDecimal(5, 4);
                this.PER_DIEM = new YGCDecimal(14, 8);
                this.INT_BASE = new YGCDecimal(14, 2);
                this.IAMOUNT = new YGCDecimal(14, 2);
                this.IPAID = new YGCDecimal(14, 2);
                this.IDATE = new YGCDate();
                this.PRIN_AMT = new YGCDecimal(14, 2);
                this.PRIN_PAID = new YGCDecimal(14, 2);
                this.CNTRCT_AMT = new YGCDecimal(14, 2);
                this.CNTRCT_PAID = new YGCDecimal(14, 2);
                this.STAT_AMT = new YGCDecimal(14, 2);
                this.STAT_PAID = new YGCDecimal(14, 2);
                this.COST_AMT = new YGCDecimal(14, 2);
                this.COST_PAID = new YGCDecimal(14, 2);
                this.DBAL = new YGCDecimal(14, 2);
                this.IBAL = new YGCDecimal(14, 2);
            }
            public RecordType20(string RT20Entry) : base(RT20Entry)
            {
                try
                {
                    this.RATES_PRE = new YGCDecimal(5, 4) { DataString = RT20Entry.Substring(67) };
                    this.RATES_POST = new YGCDecimal(5, 4) { DataString = RT20Entry.Substring(72) };
                    this.PER_DIEM = new YGCDecimal(14, 8) { DataString = RT20Entry.Substring(77) };
                    this.INT_BASE = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(91) };
                    this.IAMOUNT = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(105) };
                    this.IPAID = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(119) };
                    this.IDATE = new YGCDate() { DataString = RT20Entry.Substring(133) };
                    this.PRIN_AMT = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(141) };
                    this.PRIN_PAID = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(155) };
                    this.CNTRCT_AMT = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(169) };
                    this.CNTRCT_PAID = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(183) };
                    this.STAT_AMT = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(197) };
                    this.STAT_PAID = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(211) };
                    this.COST_AMT = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(225) };
                    this.COST_PAID = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(239) };
                    this.DBAL = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(253) };
                    this.IBAL = new YGCDecimal(14, 2) { DataString = RT20Entry.Substring(267) };
                }
                catch
                {
                    if (this.RATES_PRE == null) this.RATES_PRE = new YGCDecimal(5, 4);
                    if (this.RATES_POST == null) this.RATES_POST = new YGCDecimal(5, 4);
                    if (this.PER_DIEM == null) this.PER_DIEM = new YGCDecimal(14, 8);
                    if (this.INT_BASE == null) this.INT_BASE = new YGCDecimal(14, 2);
                    if (this.IAMOUNT == null) this.IAMOUNT = new YGCDecimal(14, 2);
                    if (this.IPAID == null) this.IPAID = new YGCDecimal(14, 2);
                    if (this.IDATE == null) this.IDATE = new YGCDate();
                    if (this.PRIN_AMT == null) this.PRIN_AMT = new YGCDecimal(14, 2);
                    if (this.PRIN_PAID == null) this.PRIN_PAID = new YGCDecimal(14, 2);
                    if (this.CNTRCT_AMT == null) this.CNTRCT_AMT = new YGCDecimal(14, 2);
                    if (this.CNTRCT_PAID == null) this.CNTRCT_PAID = new YGCDecimal(14, 2);
                    if (this.STAT_AMT == null) this.STAT_AMT = new YGCDecimal(14, 2);
                    if (this.STAT_PAID == null) this.STAT_PAID = new YGCDecimal(14, 2);
                    if (this.COST_AMT == null) this.COST_AMT = new YGCDecimal(14, 2);
                    if (this.COST_PAID == null) this.COST_PAID = new YGCDecimal(14, 2);
                    if (this.DBAL == null) this.DBAL = new YGCDecimal(14, 2);
                    if (this.IBAL == null) this.IBAL = new YGCDecimal(14, 2);
                }
            }

            public override Type GetType() { return typeof(RecordType20); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}",
                    base.ToString(),
                    this.RATES_PRE,
                    this.RATES_POST,
                    this.PER_DIEM,
                    this.INT_BASE,
                    this.IAMOUNT,
                    this.IPAID,
                    this.IDATE,
                    this.PRIN_AMT,
                    this.PRIN_PAID,
                    this.CNTRCT_AMT,
                    this.CNTRCT_PAID,
                    this.STAT_AMT,
                    this.STAT_PAID,
                    this.COST_AMT,
                    this.COST_PAID,
                    this.DBAL,
                    this.IBAL);
            }
        }
        #endregion

        #region Record Type 23
        /// <summary>
        /// Record Type 23 - Historical Debtor Phone Number (Sender to Receiver)
        /// <para>This record is for submitting a phone number history for one of the debtors. It represents the number for a specific period of time, so multiple records can be sent to record the phone number history of the same debtor or same account. Value = 23.</para>
        /// </summary>
        public class RecordType23 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This identifies which debtor in the account had this phone number.
            /// <para>Values are:</para>
            /// <para>1 = Primary debtor</para>
            /// <para>2 = Second debtor</para>
            /// <para>3 = Third debtor</para>
            /// <para>These numbers were assigned in the latest record 02 or 31 (primary) and 03 or 33 (2nd and 3rd debtors).</para>
            /// </summary>
            public YGCNumber DBTR_NUM { get; private set; }
            /// <summary>
            /// This is the first name of the debtor who had this phone number.
            /// </summary>
            public YGCString F_NAME { get; private set; }
            /// <summary>
            /// This is the middle name of the debtor who had this phone number.
            /// </summary>
            public YGCString M_NAME { get; private set; }
            /// <summary>
            /// This is the last name of the debtor who had this phone number.
            /// </summary>
            public YGCString L_NAME { get; private set; }
            /// <summary>
            /// This is the suffix of the debtor's name (ex, Jr., III) who had this phone number.
            /// </summary>
            public YGCString SUFFIX { get; private set; }
            /// <summary>
            /// This is the old phone number of the debtor. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString PHONE { get; private set; }
            /// <summary>
            /// This is the extension of the debtor's phone number.
            /// </summary>
            public YGCString PH_EXT { get; private set; }
            /// <summary>
            /// This is the type of phone line this number represents.
            /// <para>Values are:</para>
            /// <para>H = Home</para>
            /// <para>H2 = Secondary Home</para>
            /// <para>C = Cell</para>
            /// <para>C2 = Secondary Cell</para>
            /// <para>W = Work</para>
            /// <para>W2 = Secondary Work</para>
            /// <para>F = Fax</para>
            /// </summary>
            public YGCEnum<PhoneType, PhoneTypeValue> PH_TYPE { get; private set; }
            /// <summary>
            /// This is the date that the debtor's phone number was given to the source indicated in SOURCE for verification.
            /// </summary>
            public YGCDate OBTAINED_D { get; private set; }
            /// <summary>
            /// This is the source verifying the debtor's old number.
            /// <para>Accepted values are:</para>
            /// <para>ISS = Issuer</para>
            /// <para>RW = Riskwise (LexisNexis)</para>
            /// <para>CAO = Collection Agency (Outsourcing)</para>
            /// <para>IA = Acxiom</para>
            /// <para>NS = NSTN</para>
            /// <para>MAN = Manual</para>
            /// <para>FD = First Data</para>
            /// <para>WATCH = Transunion</para>
            /// <para>USPS = US Postal Service</para>
            /// <para>SCNL = Experian</para>
            /// <para>TUSCOR = Transunion</para>
            /// <para>NCOA = National Change of Address Service</para>
            /// <para>YGC = YouveGotClaims/ACC</para>
            /// <para>USPS1, USPS2 and USPS3 = US Postal Service internal codes</para>
            /// <para>FRSDTA = First Data</para>
            /// <para>NAN = National Attorney Network</para>
            /// </summary>
            public YGCEnum<PhoneSource, PhoneSourceValues> SOURCE { get; private set; }
            /// <summary>
            /// This is the verification status:
            /// <para>V = Verified</para>
            /// <para>N = Not Verified</para>
            /// <para>B = Bad</para>
            /// <para>If the value is B, then BAD_REASON should be populated.</para>
            /// </summary>
            public YGCEnum<Verification, VerificationValues> VERIFY_STAT { get; private set; }
            /// <summary>
            /// It is strongly recommended that if the number is bad, a reason is given here.
            /// </summary>
            public YGCString BAD_REASON { get; private set; }
            /// <summary>
            /// This is the date the number was verified.
            /// </summary>
            public YGCDate VERIFY_D { get; private set; }
            /// <summary>
            /// This is the date the debtor began using this number.
            /// </summary>
            public YGCDate START_D { get; private set; }
            /// <summary>
            /// This is the date the debtor stopped using this number.
            /// </summary>
            public YGCDate END_D { get; private set; }
            #endregion

            public RecordType23() : base(23)
            {
                this.DBTR_NUM = new YGCNumber(3);
                this.F_NAME = new YGCString(50);
                this.M_NAME = new YGCString(25);
                this.L_NAME = new YGCString(50);
                this.SUFFIX = new YGCString(5);
                this.PHONE = new YGCString(15);
                this.PH_EXT = new YGCString(5);
                this.PH_TYPE = new YGCEnum<PhoneType, PhoneTypeValue>(2);
                this.OBTAINED_D = new YGCDate();
                this.SOURCE = new YGCEnum<PhoneSource, PhoneSourceValues>(10);
                this.VERIFY_STAT = new YGCEnum<Verification, VerificationValues>(1);
                this.BAD_REASON = new YGCString(25);
                this.VERIFY_D = new YGCDate();
                this.START_D = new YGCDate();
                this.END_D = new YGCDate();
            }
            public RecordType23(string RT23Entry) : base(RT23Entry)
            {
                try
                {
                    this.DBTR_NUM = new YGCNumber(3) { DataString = RT23Entry.Length > 67 ? RT23Entry.Substring(67) : "" };
                    this.F_NAME = new YGCString(50) { DataString = RT23Entry.Length > 70 ? RT23Entry.Substring(70) : "" };
                    this.M_NAME = new YGCString(25) { DataString = RT23Entry.Length > 120 ? RT23Entry.Substring(120) : "" };
                    this.L_NAME = new YGCString(50) { DataString = RT23Entry.Length > 145 ? RT23Entry.Substring(145) : "" };
                    this.SUFFIX = new YGCString(5) { DataString = RT23Entry.Length > 195 ? RT23Entry.Substring(195) : "" };
                    this.PHONE = new YGCString(15) { DataString = RT23Entry.Length > 200 ? RT23Entry.Substring(200) : "" };
                    this.PH_EXT = new YGCString(5) { DataString = RT23Entry.Length > 215 ? RT23Entry.Substring(215) : "" };
                    this.PH_TYPE = new YGCEnum<PhoneType, PhoneTypeValue>(2) { DataString = RT23Entry.Length > 220 ? RT23Entry.Substring(220) : "" };
                    this.OBTAINED_D = new YGCDate() { DataString = RT23Entry.Length > 222 ? RT23Entry.Substring(222) : "" };
                    this.SOURCE = new YGCEnum<PhoneSource, PhoneSourceValues>(10) { DataString = RT23Entry.Length > 230 ? RT23Entry.Substring(230) : "" };
                    this.VERIFY_STAT = new YGCEnum<Verification, VerificationValues>(1) { DataString = RT23Entry.Length > 240 ? RT23Entry.Substring(240) : "" };
                    this.BAD_REASON = new YGCString(25) { DataString = RT23Entry.Length > 241 ? RT23Entry.Substring(241) : "" };
                    this.VERIFY_D = new YGCDate() { DataString = RT23Entry.Length > 266 ? RT23Entry.Substring(266) : "" };
                    this.START_D = new YGCDate() { DataString = RT23Entry.Length > 274 ? RT23Entry.Substring(274) : "" };
                    this.END_D = new YGCDate() { DataString = RT23Entry.Length > 282 ? RT23Entry.Substring(282) : "" };
                }
                catch
                {
                    if (this.DBTR_NUM == null) this.DBTR_NUM = new YGCNumber(3);
                    if (this.F_NAME == null) this.F_NAME = new YGCString(50);
                    if (this.M_NAME == null) this.M_NAME = new YGCString(25);
                    if (this.L_NAME == null) this.L_NAME = new YGCString(50);
                    if (this.SUFFIX == null) this.SUFFIX = new YGCString(5);
                    if (this.PHONE == null) this.PHONE = new YGCString(15);
                    if (this.PH_EXT == null) this.PH_EXT = new YGCString(5);
                    if (this.PH_TYPE == null) this.PH_TYPE = new YGCEnum<PhoneType, PhoneTypeValue>(2);
                    if (this.OBTAINED_D == null) this.OBTAINED_D = new YGCDate();
                    if (this.SOURCE == null) this.SOURCE = new YGCEnum<PhoneSource, PhoneSourceValues>(10);
                    if (this.VERIFY_STAT == null) this.VERIFY_STAT = new YGCEnum<Verification, VerificationValues>(1);
                    if (this.BAD_REASON == null) this.BAD_REASON = new YGCString(25);
                    if (this.VERIFY_D == null) this.VERIFY_D = new YGCDate();
                    if (this.START_D == null) this.START_D = new YGCDate();
                    if (this.END_D == null) this.END_D = new YGCDate();
                }
            }

            public override Type GetType() { return typeof(RecordType23); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                    base.ToString(),
                    this.DBTR_NUM,
                    this.F_NAME,
                    this.M_NAME,
                    this.L_NAME,
                    this.SUFFIX,
                    this.PHONE,
                    this.PH_EXT,
                    this.PH_TYPE,
                    this.OBTAINED_D,
                    this.SOURCE,
                    this.VERIFY_STAT,
                    this.BAD_REASON,
                    this.VERIFY_D,
                    this.START_D,
                    this.END_D);
            }
        }
        #endregion

        #region Record Type 24
        /// <summary>
        /// Record Type 24 - Historical Financial Transaction (Sender to Receiver)						
        /// <para>This record reports a financial transaction made before the account is sent to the receiver. Multiple records can be sent to record the transaction history of the same account. The receiver can compile all of these records through their collection system into one statement as evidence. Value = 24.</para>
        /// </summary>
        public class RecordType24 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the date the transaction was received and posted into the sender's system.
            /// </summary>
            public YGCDate POST_D { get; private set; }
            /// <summary>
            /// This identifies the type or payment or cost. Use an Accounting or Disbursement YGC Status Code (see the Status Codes tab for a complete list).
            /// </summary>
            public YGCString TRANS_CD { get; private set; }
            /// <summary>
            /// Unique ID/reference number for this transaction.
            /// </summary>
            public YGCNumber TRANS_NUM { get; private set; }
            /// <summary>
            /// Typically, this is the total amount of the payment to the sender, which may not include Statutory Attorney Fees awarded in some states.
            /// </summary>
            public YGCDecimal TOTAL_COLL { get; private set; }
            /// <summary>
            /// This is the amount of principal collected on the payment.
            /// </summary>
            public YGCDecimal PRIN_COLL { get; private set; }
            /// <summary>
            /// This is the amount of interest collected on the payment.
            /// </summary>
            public YGCDecimal INT_COLL { get; private set; }
            /// <summary>
            /// This is the amount of costs recovered on the transaction.
            /// </summary>
            public YGCDecimal COST_COLL { get; private set; }
            /// <summary>
            /// This is the amount of statutory attorney fees recovered in this payment. Typically these are state awarded fees that do not get remitted on.
            /// </summary>
            public YGCDecimal STATU_COLL { get; private set; }
            /// <summary>
            /// This is the commission earned on this transaction.
            /// </summary>
            public YGCDecimal COMM { get; private set; }
            /// <summary>
            /// This is the debtor balance that existed as of the posting of this transaction.
            /// </summary>
            public YGCDecimal DBTR_BAL { get; private set; }
            /// <summary>
            /// This field is not applicable for payments. It is only used to report a cost disbursement.
            /// </summary>
            public YGCDecimal COST_EXP { get; private set; }
            /// <summary>
            /// This is a description of the transaction, such as Payment, Suit Filed, Audit, Collection, or Court Costs.
            /// </summary>
            public YGCString DESC { get; private set; }
            /// <summary>
            /// This is a specific description of the transaction. For payments, it will typically include the check number.
            /// </summary>
            public YGCString CMT { get; private set; }
            #endregion

            public RecordType24() : base(24)
            {
                this.POST_D = new YGCDate();
                this.TRANS_CD = new YGCString(8);
                this.TRANS_NUM = new YGCNumber(6);
                this.TOTAL_COLL = new YGCDecimal(14, 2);
                this.PRIN_COLL = new YGCDecimal(14, 2);
                this.INT_COLL = new YGCDecimal(14, 2);
                this.COST_COLL = new YGCDecimal(14, 2);
                this.STATU_COLL = new YGCDecimal(14, 2);
                this.COMM = new YGCDecimal(14, 2);
                this.DBTR_BAL = new YGCDecimal(14, 2);
                this.COST_EXP = new YGCDecimal(14, 2);
                this.DESC = new YGCString(30);
                this.CMT = new YGCString(30);
            }
            public RecordType24(string RT24Entry) : base(RT24Entry)
            {
                try
                {
                    this.POST_D = new YGCDate() { DataString = RT24Entry.Substring(67) };
                    this.TRANS_CD = new YGCString(8) { DataString = RT24Entry.Substring(75) };
                    this.TRANS_NUM = new YGCNumber(6) { DataString = RT24Entry.Substring(83) };
                    this.TOTAL_COLL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(89) };
                    this.PRIN_COLL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(103) };
                    this.INT_COLL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(117) };
                    this.COST_COLL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(131) };
                    this.STATU_COLL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(145) };
                    this.COMM = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(159) };
                    this.DBTR_BAL = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(173) };
                    this.COST_EXP = new YGCDecimal(14, 2) { DataString = RT24Entry.Substring(187) };
                    this.DESC = new YGCString(30) { DataString = RT24Entry.Substring(201) };
                    this.CMT = new YGCString(30) { DataString = RT24Entry.Substring(231) };
                }
                catch
                {
                    if (this.POST_D == null) this.POST_D = new YGCDate();
                    if (this.TRANS_CD == null) this.TRANS_CD = new YGCString(8);
                    if (this.TRANS_NUM == null) this.TRANS_NUM = new YGCNumber(6);
                    if (this.TOTAL_COLL == null) this.TOTAL_COLL = new YGCDecimal(14, 2);
                    if (this.PRIN_COLL == null) this.PRIN_COLL = new YGCDecimal(14, 2);
                    if (this.INT_COLL == null) this.INT_COLL = new YGCDecimal(14, 2);
                    if (this.COST_COLL == null) this.COST_COLL = new YGCDecimal(14, 2);
                    if (this.STATU_COLL == null) this.STATU_COLL = new YGCDecimal(14, 2);
                    if (this.COMM == null) this.COMM = new YGCDecimal(14, 2);
                    if (this.DBTR_BAL == null) this.DBTR_BAL = new YGCDecimal(14, 2);
                    if (this.COST_EXP == null) this.COST_EXP = new YGCDecimal(14, 2);
                    if (this.DESC == null) this.DESC = new YGCString(30);
                    if (this.CMT == null) this.CMT = new YGCString(30);
                }
            }

            public override Type GetType() { return typeof(RecordType24); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}",
                    base.ToString(),
                    this.POST_D,
                    this.TRANS_CD,
                    this.TRANS_NUM,
                    this.TOTAL_COLL,
                    this.PRIN_COLL,
                    this.INT_COLL,
                    this.COST_COLL,
                    this.STATU_COLL,
                    this.COMM,
                    this.DBTR_BAL,
                    this.COST_EXP,
                    this.DESC,
                    this.CMT);
            }
        }
        #endregion

        #region Record Type 25
        /// <summary>
        /// Record Type 25 - Service Information (Sender to Receiver)											
        /// </summary>
        public class RecordType25 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the serving individual's first name.
            /// </summary>
            public YGCString SERVER_FIRSTNAME { get; private set; }
            /// <summary>
            /// This is the serving individual's middle name or initial.
            /// </summary>
            public YGCString SERVER_MIDDLENAME { get; private set; }
            /// <summary>
            /// This is the serving individual's last name.
            /// </summary>
            public YGCString SERVER_LASTNAME { get; private set; }
            /// <summary>
            /// This is the name of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_COMPANY { get; private set; }
            /// <summary>
            /// This is the address line 1 of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_ADDRESS1 { get; private set; }
            /// <summary>
            /// This is the address line 2 of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_ADDRESS2 { get; private set; }
            /// <summary>
            /// This is the city of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_CITY { get; private set; }
            /// <summary>
            /// This is the state of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_STATE { get; private set; }
            /// <summary>
            /// This is the postal code of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_ZIP { get; private set; }
            /// <summary>
            /// This is the country of the company that employs server specified in ServerFirstName and ServerLastName.
            /// </summary>
            public YGCString SERVER_COUNTRY { get; private set; }
            /// <summary>
            /// Server reference, work order, or job number.
            /// </summary>
            public YGCString SERVER_REFERENCENUMBER { get; private set; }
            /// <summary>
            /// Cost of the service.
            /// </summary>
            public YGCDecimal COSTOFSERVICE { get; private set; }
            /// <summary>
            /// This is used to distinguish up to three Service records.  If this record for the primary debtor then this value will be 1, if this record for the co-signer then this value will be 2, etc.  If a record is sent with the same DebtorNumberServed as a previous record for the same account, the second record will overwrite the first.
            /// </summary>
            public YGCNumber DEBTORNUMBERSERVED { get; private set; }
            /// <summary>
            /// This is the date & time that service was executed
            /// </summary>
            public YGCDateTime SERVED_DATETIME { get; private set; }
            /// <summary>
            /// This is the method used to serve the papers. The valid codes are:
            /// <para>PER = Personal</para>
            /// <para>CER = Certified Mail</para>
            /// <para>SUB = Sub-service</para>
            /// <para>POS = Posting (left at front door)</para>
            /// <para>FIR = First Class Mail</para>
            /// </summary>
            public YGCEnum<ServiceType, ServiceTypeValues> SERVICE_TYPE { get; private set; }
            /// <summary>
            /// This is the address where the service was completed.  Field may be the same as D1_Street or D2_Street.
            /// </summary>
            public YGCString SERVED_ADDRESS1 { get; private set; }
            /// <summary>
            /// This is the address where the service was completed.  Field may be the same as D1_Street or D2_Street.
            /// </summary>
            public YGCString SERVED_ADDERSS2 { get; private set; }
            /// <summary>
            /// This is the city where the service was completed.  Field may be the same as D1_CITY or D2_CITY.
            /// </summary>
            public YGCString SERVED_CITY { get; private set; }
            /// <summary>
            /// This is the county the ServedAtCity is located in.
            /// </summary>
            public YGCString SERVED_COUNTY { get; private set; }
            /// <summary>
            /// This is the state where the service was completed.  Field may be the same as D1_STATE or D2_STATE.
            /// </summary>
            public YGCString SERVED_STATE { get; private set; }
            /// <summary>
            /// This is the postal code where the service was completed.  Field may be the same as D1_ZIP or D2_ZIP.
            /// </summary>
            public YGCString SERVED_ZIP { get; private set; }
            /// <summary>
            /// This is the standard code for the Country where service was completed.
            /// </summary>
            public YGCString SERVED_COUNTRY { get; private set; }
            /// <summary>
            /// Degrees, Minutes and Seconds (DDD MM SS.S) or Decimal Degrees (DDD.DDDDD) or Degrees and Decimal Minutes (DD MM.MMM).
            /// </summary>
            public YGCString SERVED_LATITUDE_FORMAT { get; private set; }
            /// <summary>
            /// This is the Latitude information where service was completed.
            /// </summary>
            public YGCString SERVED_LATITUDE { get; private set; }
            /// <summary>
            /// Degrees, Minutes and Seconds (DDD MM SS.S) or Decimal Degrees (DDD.DDDDD) or Degrees and Decimal Minutes (DD MM.MMM).
            /// </summary>
            public YGCString SERVED_LONGITUDE_FORMAT { get; private set; }
            /// <summary>
            /// This is the Longitude information where service was completed.
            /// </summary>
            public YGCString SERVED_LONGITUDE { get; private set; }
            /// <summary>
            /// This is the first name of the individual who was served.
            /// </summary>
            public YGCString SERVED_FIRSTNAME { get; private set; }
            /// <summary>
            /// This is the middle name or initial of the individual who was served.
            /// </summary>
            public YGCString SERVED_MIDDLENAME { get; private set; }
            /// <summary>
            /// This is the last name of the individual who was served.
            /// </summary>
            public YGCString SERVED_LASTNAME { get; private set; }
            /// <summary>
            /// This is the descirption of the relationship to the debtor being served.
            /// </summary>
            public YGCString RELATIONSHIPTODEBTOR { get; private set; }
            /// <summary>
            /// This is the marital status of the debtor being served provided by person served (ServedToFirstName and ServedToLastName).
            /// </summary>
            public YGCString DEBTOR_MARITAL_STATUS { get; private set; }
            /// <summary>
            /// This is the military status of the debtor being served provided by by person served (ServedToFirstName and ServedToLastName).
            /// </summary>
            public YGCString DEBTOR_MILITARY_STATUS { get; private set; }
            /// <summary>
            /// This is the Sex type of the person serviced in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_SEX { get; private set; }
            /// <summary>
            /// This is the race of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_RACE { get; private set; }
            /// <summary>
            /// This is the age in years of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCNumber SERVED_PERSON_AGE { get; private set; }
            /// <summary>
            /// This is the eye color of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_EYECOLOR { get; private set; }
            /// <summary>
            /// This is the hair color of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_HAIRCOLOR { get; private set; }
            /// <summary>
            /// This is the approximate height (in feet and inches) of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_HEIGHT { get; private set; }
            /// <summary>
            /// This is the approximate weight in pounds of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCNumber SERVED_PERSON_WEIGHT { get; private set; }
            /// <summary>
            /// Identifying features of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_IDENTIFYINGFEATURES { get; private set; }
            /// <summary>
            /// This is the approximate build type of the person served in ServedToFirstName and ServedToLastName.
            /// </summary>
            public YGCString SERVED_PERSON_BUILD { get; private set; }
            #endregion

            public RecordType25()
                : base(25)
            {
                this.SERVER_FIRSTNAME = new YGCString(30);
                this.SERVER_MIDDLENAME = new YGCString(30);
                this.SERVER_LASTNAME = new YGCString(30);
                this.SERVER_COMPANY = new YGCString(50);
                this.SERVER_ADDRESS1 = new YGCString(50);
                this.SERVER_ADDRESS2 = new YGCString(50);
                this.SERVER_CITY = new YGCString(25);
                this.SERVER_STATE = new YGCString(2);
                this.SERVER_ZIP = new YGCString(9);
                this.SERVER_COUNTRY = new YGCString(3);
                this.SERVER_REFERENCENUMBER = new YGCString(20);
                this.COSTOFSERVICE = new YGCDecimal(14, 2);
                this.DEBTORNUMBERSERVED = new YGCNumber(1);
                this.SERVED_DATETIME = new YGCDateTime();
                this.SERVICE_TYPE = new YGCEnum<ServiceType, ServiceTypeValues>(4);
                this.SERVED_ADDRESS1 = new YGCString(50);
                this.SERVED_ADDERSS2 = new YGCString(50);
                this.SERVED_CITY = new YGCString(25);
                this.SERVED_COUNTY = new YGCString(20);
                this.SERVED_STATE = new YGCString(2);
                this.SERVED_ZIP = new YGCString(9);
                this.SERVED_COUNTRY = new YGCString(3);
                this.SERVED_LATITUDE_FORMAT = new YGCString(3);
                this.SERVED_LATITUDE = new YGCString(16);
                this.SERVED_LONGITUDE_FORMAT = new YGCString(3);
                this.SERVED_LONGITUDE = new YGCString(16);
                this.SERVED_FIRSTNAME = new YGCString(50);
                this.SERVED_MIDDLENAME = new YGCString(50);
                this.SERVED_LASTNAME = new YGCString(50);
                this.RELATIONSHIPTODEBTOR = new YGCString(25);
                this.DEBTOR_MARITAL_STATUS = new YGCString(15);
                this.DEBTOR_MILITARY_STATUS = new YGCString(25);
                this.SERVED_PERSON_SEX = new YGCString(7);
                this.SERVED_PERSON_RACE = new YGCString(20);
                this.SERVED_PERSON_AGE = new YGCNumber(3);
                this.SERVED_PERSON_EYECOLOR = new YGCString(10);
                this.SERVED_PERSON_HAIRCOLOR = new YGCString(15);
                this.SERVED_PERSON_HEIGHT = new YGCString(6);
                this.SERVED_PERSON_WEIGHT = new YGCNumber(3);
                this.SERVED_PERSON_IDENTIFYINGFEATURES = new YGCString(60);
                this.SERVED_PERSON_BUILD = new YGCString(15);
            }
            public RecordType25(string RT25Entry) : base(RT25Entry)
            {
                this.SERVER_FIRSTNAME = new YGCString(30) { DataString = RT25Entry.Length > 67 ? RT25Entry.Substring(67) : "" };
                this.SERVER_MIDDLENAME = new YGCString(30) { DataString = RT25Entry.Length > 97 ? RT25Entry.Substring(97) : "" };
                this.SERVER_LASTNAME = new YGCString(30) { DataString = RT25Entry.Length > 127 ? RT25Entry.Substring(127) : "" };
                this.SERVER_COMPANY = new YGCString(50) { DataString = RT25Entry.Length > 157 ? RT25Entry.Substring(157) : "" };
                this.SERVER_ADDRESS1 = new YGCString(50) { DataString = RT25Entry.Length > 207 ? RT25Entry.Substring(207) : "" };
                this.SERVER_ADDRESS2 = new YGCString(50) { DataString = RT25Entry.Length > 257 ? RT25Entry.Substring(257) : "" };
                this.SERVER_CITY = new YGCString(25) { DataString = RT25Entry.Length > 307 ? RT25Entry.Substring(307) : "" };
                this.SERVER_STATE = new YGCString(2) { DataString = RT25Entry.Length > 332 ? RT25Entry.Substring(332) : "" };
                this.SERVER_ZIP = new YGCString(9) { DataString = RT25Entry.Length > 334 ? RT25Entry.Substring(334) : "" };
                this.SERVER_COUNTRY = new YGCString(3) { DataString = RT25Entry.Length > 343 ? RT25Entry.Substring(343) : "" };
                this.SERVER_REFERENCENUMBER = new YGCString(20) { DataString = RT25Entry.Length > 346 ? RT25Entry.Substring(346) : "" };
                this.COSTOFSERVICE = new YGCDecimal(14, 2) { DataString = RT25Entry.Length > 366 ? RT25Entry.Substring(366) : "" };
                this.DEBTORNUMBERSERVED = new YGCNumber(1) { DataString = RT25Entry.Length > 380 ? RT25Entry.Substring(380) : "" };
                this.SERVED_DATETIME = new YGCDateTime() { DataString = RT25Entry.Length > 381 ? RT25Entry.Substring(381) : "" };
                this.SERVICE_TYPE = new YGCEnum<ServiceType, ServiceTypeValues>(4) { DataString = RT25Entry.Length > 395 ? RT25Entry.Substring(395) : "" };
                this.SERVED_ADDRESS1 = new YGCString(50) { DataString = RT25Entry.Length > 399 ? RT25Entry.Substring(399) : "" };
                this.SERVED_ADDERSS2 = new YGCString(50) { DataString = RT25Entry.Length > 449 ? RT25Entry.Substring(449) : "" };
                this.SERVED_CITY = new YGCString(25) { DataString = RT25Entry.Length > 499 ? RT25Entry.Substring(499) : "" };
                this.SERVED_COUNTY = new YGCString(20) { DataString = RT25Entry.Length > 524 ? RT25Entry.Substring(524) : "" };
                this.SERVED_STATE = new YGCString(2) { DataString = RT25Entry.Length > 544 ? RT25Entry.Substring(544) : "" };
                this.SERVED_ZIP = new YGCString(9) { DataString = RT25Entry.Length > 546 ? RT25Entry.Substring(546) : "" };
                this.SERVED_COUNTRY = new YGCString(3) { DataString = RT25Entry.Length > 555 ? RT25Entry.Substring(555) : "" };
                this.SERVED_LATITUDE_FORMAT = new YGCString(3) { DataString = RT25Entry.Length > 558 ? RT25Entry.Substring(558) : "" };
                this.SERVED_LATITUDE = new YGCString(16) { DataString = RT25Entry.Length > 561 ? RT25Entry.Substring(561) : "" };
                this.SERVED_LONGITUDE_FORMAT = new YGCString(3) { DataString = RT25Entry.Length > 577 ? RT25Entry.Substring(577) : "" };
                this.SERVED_LONGITUDE = new YGCString(16) { DataString = RT25Entry.Length > 580 ? RT25Entry.Substring(580) : "" };
                this.SERVED_FIRSTNAME = new YGCString(50) { DataString = RT25Entry.Length > 596 ? RT25Entry.Substring(596) : "" };
                this.SERVED_MIDDLENAME = new YGCString(50) { DataString = RT25Entry.Length > 646 ? RT25Entry.Substring(646) : "" };
                this.SERVED_LASTNAME = new YGCString(50) { DataString = RT25Entry.Length > 696 ? RT25Entry.Substring(696) : "" };
                this.RELATIONSHIPTODEBTOR = new YGCString(25) { DataString = RT25Entry.Length > 746 ? RT25Entry.Substring(746) : "" };
                this.DEBTOR_MARITAL_STATUS = new YGCString(15) { DataString = RT25Entry.Length > 771 ? RT25Entry.Substring(771) : "" };
                this.DEBTOR_MILITARY_STATUS = new YGCString(25) { DataString = RT25Entry.Length > 786 ? RT25Entry.Substring(786) : "" };
                this.SERVED_PERSON_SEX = new YGCString(7) { DataString = RT25Entry.Length > 811 ? RT25Entry.Substring(811) : "" };
                this.SERVED_PERSON_RACE = new YGCString(20) { DataString = RT25Entry.Length > 818 ? RT25Entry.Substring(818) : "" };
                this.SERVED_PERSON_AGE = new YGCNumber(3) { DataString = RT25Entry.Length > 838 ? RT25Entry.Substring(838) : "" };
                this.SERVED_PERSON_EYECOLOR = new YGCString(10) { DataString = RT25Entry.Length > 841 ? RT25Entry.Substring(841) : "" };
                this.SERVED_PERSON_HAIRCOLOR = new YGCString(15) { DataString = RT25Entry.Length > 851 ? RT25Entry.Substring(851) : "" };
                this.SERVED_PERSON_HEIGHT = new YGCString(6) { DataString = RT25Entry.Length > 866 ? RT25Entry.Substring(866) : "" };
                this.SERVED_PERSON_WEIGHT = new YGCNumber(3) { DataString = RT25Entry.Length > 872 ? RT25Entry.Substring(872) : "" };
                this.SERVED_PERSON_IDENTIFYINGFEATURES = new YGCString(60) { DataString = RT25Entry.Length > 875 ? RT25Entry.Substring(875) : "" };
                this.SERVED_PERSON_BUILD = new YGCString(15) { DataString = RT25Entry.Length > 935 ? RT25Entry.Substring(935) : "" };
            }

            public override Type GetType() { return typeof(RecordType25); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}",
                    base.ToString(),
                    this.SERVER_FIRSTNAME,
                    this.SERVER_MIDDLENAME,
                    this.SERVER_LASTNAME,
                    this.SERVER_COMPANY,
                    this.SERVER_ADDRESS1,
                    this.SERVER_ADDRESS2,
                    this.SERVER_CITY,
                    this.SERVER_STATE,
                    this.SERVER_ZIP,
                    this.SERVER_COUNTRY,
                    this.SERVER_REFERENCENUMBER,
                    this.COSTOFSERVICE,
                    this.DEBTORNUMBERSERVED,
                    this.SERVED_DATETIME,
                    this.SERVICE_TYPE,
                    this.SERVED_ADDRESS1,
                    this.SERVED_ADDERSS2,
                    this.SERVED_CITY,
                    this.SERVED_COUNTY,
                    this.SERVED_STATE,
                    this.SERVED_ZIP,
                    this.SERVED_COUNTRY,
                    this.SERVED_LATITUDE_FORMAT,
                    this.SERVED_LATITUDE,
                    this.SERVED_LONGITUDE_FORMAT,
                    this.SERVED_LONGITUDE,
                    this.SERVED_FIRSTNAME,
                    this.SERVED_MIDDLENAME,
                    this.SERVED_LASTNAME,
                    this.RELATIONSHIPTODEBTOR,
                    this.DEBTOR_MARITAL_STATUS,
                    this.DEBTOR_MILITARY_STATUS,
                    this.SERVED_PERSON_SEX,
                    this.SERVED_PERSON_RACE,
                    this.SERVED_PERSON_AGE,
                    this.SERVED_PERSON_EYECOLOR,
                    this.SERVED_PERSON_HAIRCOLOR,
                    this.SERVED_PERSON_HEIGHT,
                    this.SERVED_PERSON_WEIGHT,
                    this.SERVED_PERSON_IDENTIFYINGFEATURES,
                    this.SERVED_PERSON_BUILD);
            }
        }
        #endregion

        #region Record Type 30
        /// <summary>
        /// Record Type 30 - Payments to Client
        /// </summary>
        public class RecordType30 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// Financial Code 1-6    and   51-81
            /// </summary>
            public YGCNumber RET_CODE { get; private set; }
            /// <summary>
            /// Transaction, Disbursement or Payment Release Date
            /// </summary>
            public YGCDate PAY_DATE { get; private set; }
            /// <summary>
            /// Gross Payment Amount
            /// </summary>
            public YGCDecimal GROSS_PAY { get; private set; }
            /// <summary>
            /// Net Client
            /// </summary>
            public YGCDecimal NET_CLIENT { get; private set; }
            /// <summary>
            /// Check Amount to Client
            /// </summary>
            public YGCDecimal CHECK_AMT { get; private set; }
            /// <summary>
            /// Costs Returned
            /// </summary>
            public YGCDecimal COST_RET { get; private set; }
            /// <summary>
            /// Fees on Remittance
            /// </summary>
            public YGCDecimal FEES { get; private set; }
            /// <summary>
            /// Agent/Co-Counsel Fees
            /// </summary>
            public YGCDecimal AGENT_FEES { get; private set; }
            /// <summary>
            /// Forwarder Commissions
            /// </summary>
            public YGCDecimal FORW_CUT { get; private set; }
            /// <summary>
            /// Costs Recovered {May not have been returned}
            /// </summary>
            public YGCDecimal COST_REC { get; private set; }
            /// <summary>
            /// (B)efore Suit - (P)ost Suit - Post (J)udgment
            /// </summary>
            public YGCString BPJ { get; private set; }
            /// <summary>
            /// Transaction #
            /// </summary>
            public YGCNumber TA_NO { get; private set; }
            /// <summary>
            /// Remittance Batch #
            /// </summary>
            public YGCNumber RMIT_NO { get; private set; }
            /// <summary>
            /// Stat Fees Collected
            /// </summary>
            public YGCDecimal LINE1_3 { get; private set; }
            /// <summary>
            /// Debtor Balance - As of Posting
            /// </summary>
            public YGCDecimal LINE1_5 { get; private set; }
            /// <summary>
            /// Amount Received
            /// </summary>
            public YGCDecimal LINE1_6 { get; private set; }
            /// <summary>
            /// Principal Collected
            /// </summary>
            public YGCDecimal LINE2_1 { get; private set; }
            /// <summary>
            /// Interest Collected
            /// </summary>
            public YGCDecimal LINE2_2 { get; private set; }
            /// <summary>
            /// Costs Expended
            /// </summary>
            public YGCDecimal LINE2_5 { get; private set; }
            /// <summary>
            /// Costs Received (Special)
            /// </summary>
            public YGCDecimal LINE2_6 { get; private set; }
            /// <summary>
            /// Suit Fees
            /// </summary>
            public YGCDecimal LINE2_7 { get; private set; }
            /// <summary>
            /// Commissions
            /// </summary>
            public YGCDecimal LINE2_8 { get; private set; }
            /// <summary>
            /// Description of transaction
            /// </summary>
            public YGCString DESC { get; private set; }
            /// <summary>
            /// Date Transaction was Posted
            /// </summary>
            public YGCDate POST_DATE { get; private set; }
            /// <summary>
            /// Date Transaction was Remitted
            /// </summary>
            public YGCDate REMIT_DATE { get; private set; }
            /// <summary>
            /// Transaction Code (See **Below)
            /// </summary>
            public YGCString TA_CODE { get; private set; }
            /// <summary>
            /// "This provides a more specific description of this transaction. For payments, this can typically include the check number."
            /// </summary>
            public YGCString COMMENT { get; private set; }
            /// <summary>
            /// Only used in a payment reversal, it is the transaction number (TA_NO) of the original payment that is being reversed.
            /// </summary>
            public YGCNumber ORIGINALTANUMBER { get; private set; }
            /// <summary>
            /// Only used in a payment reversal, it is the remit number (RMIT_NO) of the original payment that is being reversed.
            /// </summary>
            public YGCNumber ORIGINALREMITNUMBER { get; private set; }
            /// <summary>
            /// Only used in a payment reversal, it is the remit date (REMIT_DATE) of the original payment that is being reversed.
            /// </summary>
            public YGCDate ORIGINALREMITDATE { get; private set; }
            /// <summary>
            /// "Portion of costs expended (LINE2_5) that is recoverable from the debtor. This is only used for cost expended transactions."
            /// </summary>
            public YGCDecimal COSTSPENTRECOVERABLEFROMDEBTOR { get; private set; }
            /// <summary>
            /// "Portion of costs expended (LINE2_5) that is not recoverable from the debtor. This is only used for cost expended transactions."
            /// </summary>
            public YGCDecimal COSTSPENTNONRECOVERABLEFROMDEBTOR { get; private set; }
            /// <summary>
            /// Portion of costs expended (LINE2_5) that is billable to the client. This is only used for cost expended transactions.
            /// </summary>
            public YGCDecimal COSTSPENTRECOVERABLEFROMCLIENT { get; private set; }
            /// <summary>
            /// "Portion of costs expended (LINE2_5) that is not billable to the client. This is only used for cost expended transactions."
            /// </summary>
            public YGCDecimal COSTSPENTNONRECOVERABLEFROMCLIENT { get; private set; }
            /// <summary>
            /// "This is used to distinguish up to 3 Execution records. Values are 1, 2 or 3.      (Ex: This record for the primary debtor can have EXECUTE_NO = 1 and the this record for the co-signer can have EXECUTE_NO = 2.)      If this record is sent with the same Execute_NO as a previous record for the same account, the second record will overwrite the first.    "
            /// </summary>
            public YGCNumber DEBTORNUMBER { get; private set; }

            #endregion

            public RecordType30()
                : base(30)
            {
                this.RET_CODE = new YGCNumber(2);
                this.PAY_DATE = new YGCDate();
                this.GROSS_PAY = new YGCDecimal(14, 2);
                this.NET_CLIENT = new YGCDecimal(14, 2);
                this.CHECK_AMT = new YGCDecimal(14, 2);
                this.COST_RET = new YGCDecimal(14, 2);
                this.FEES = new YGCDecimal(14, 2);
                this.AGENT_FEES = new YGCDecimal(14, 2);
                this.FORW_CUT = new YGCDecimal(14, 2);
                this.COST_REC = new YGCDecimal(14, 2);
                this.BPJ = new YGCString(1);
                this.TA_NO = new YGCNumber(6);
                this.RMIT_NO = new YGCNumber(6);
                this.LINE1_3 = new YGCDecimal(14, 2);
                this.LINE1_5 = new YGCDecimal(14, 2);
                this.LINE1_6 = new YGCDecimal(14, 2);
                this.LINE2_1 = new YGCDecimal(14, 2);
                this.LINE2_2 = new YGCDecimal(14, 2);
                this.LINE2_5 = new YGCDecimal(14, 2);
                this.LINE2_6 = new YGCDecimal(14, 2);
                this.LINE2_7 = new YGCDecimal(14, 2);
                this.LINE2_8 = new YGCDecimal(14, 2);
                this.DESC = new YGCString(30);
                this.POST_DATE = new YGCDate();
                this.REMIT_DATE = new YGCDate();
                this.TA_CODE = new YGCString(8);
                this.COMMENT = new YGCString(30);
                this.ORIGINALTANUMBER = new YGCNumber(6);
                this.ORIGINALREMITNUMBER = new YGCNumber(6);
                this.ORIGINALREMITDATE = new YGCDate();
                this.COSTSPENTRECOVERABLEFROMDEBTOR = new YGCDecimal(14, 2);
                this.COSTSPENTNONRECOVERABLEFROMDEBTOR = new YGCDecimal(14, 2);
                this.COSTSPENTRECOVERABLEFROMCLIENT = new YGCDecimal(14, 2);
                this.COSTSPENTNONRECOVERABLEFROMCLIENT = new YGCDecimal(14, 2);
                this.DEBTORNUMBER = new YGCNumber(1);
            }
            public RecordType30(string RT30Entry)
                : base(RT30Entry)
            {
                this.RET_CODE = new YGCNumber(2) { DataString = RT30Entry.Length > 67 ? RT30Entry.Substring(67) : "" };
                this.PAY_DATE = new YGCDate() { DataString = RT30Entry.Length > 69 ? RT30Entry.Substring(69) : "" };
                this.GROSS_PAY = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 77 ? RT30Entry.Substring(77) : "" };
                this.NET_CLIENT = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 91 ? RT30Entry.Substring(91) : "" };
                this.CHECK_AMT = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 105 ? RT30Entry.Substring(105) : "" };
                this.COST_RET = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 119 ? RT30Entry.Substring(119) : "" };
                this.FEES = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 133 ? RT30Entry.Substring(133) : "" };
                this.AGENT_FEES = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 147 ? RT30Entry.Substring(147) : "" };
                this.FORW_CUT = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 161 ? RT30Entry.Substring(161) : "" };
                this.COST_REC = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 175 ? RT30Entry.Substring(175) : "" };
                this.BPJ = new YGCString(1) { DataString = RT30Entry.Length > 189 ? RT30Entry.Substring(189) : "" };
                this.TA_NO = new YGCNumber(6) { DataString = RT30Entry.Length > 190 ? RT30Entry.Substring(190) : "" };
                this.RMIT_NO = new YGCNumber(6) { DataString = RT30Entry.Length > 196 ? RT30Entry.Substring(196) : "" };
                this.LINE1_3 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 202 ? RT30Entry.Substring(202) : "" };
                this.LINE1_5 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 216 ? RT30Entry.Substring(216) : "" };
                this.LINE1_6 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 230 ? RT30Entry.Substring(230) : "" };
                this.LINE2_1 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 244 ? RT30Entry.Substring(244) : "" };
                this.LINE2_2 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 258 ? RT30Entry.Substring(258) : "" };
                this.LINE2_5 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 272 ? RT30Entry.Substring(272) : "" };
                this.LINE2_6 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 286 ? RT30Entry.Substring(286) : "" };
                this.LINE2_7 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 300 ? RT30Entry.Substring(300) : "" };
                this.LINE2_8 = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 314 ? RT30Entry.Substring(314) : "" };
                this.DESC = new YGCString(30) { DataString = RT30Entry.Length > 328 ? RT30Entry.Substring(328) : "" };
                this.POST_DATE = new YGCDate() { DataString = RT30Entry.Length > 358 ? RT30Entry.Substring(358) : "" };
                this.REMIT_DATE = new YGCDate() { DataString = RT30Entry.Length > 366 ? RT30Entry.Substring(366) : "" };
                this.TA_CODE = new YGCString(8) { DataString = RT30Entry.Length > 374 ? RT30Entry.Substring(374) : "" };
                this.COMMENT = new YGCString(30) { DataString = RT30Entry.Length > 382 ? RT30Entry.Substring(382) : "" };
                this.ORIGINALTANUMBER = new YGCNumber(6) { DataString = RT30Entry.Length > 412 ? RT30Entry.Substring(412) : "" };
                this.ORIGINALREMITNUMBER = new YGCNumber(6) { DataString = RT30Entry.Length > 418 ? RT30Entry.Substring(418) : "" };
                this.ORIGINALREMITDATE = new YGCDate { DataString = RT30Entry.Length > 424 ? RT30Entry.Substring(424) : "" };
                this.COSTSPENTRECOVERABLEFROMDEBTOR = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 432 ? RT30Entry.Substring(432) : "" };
                this.COSTSPENTNONRECOVERABLEFROMDEBTOR = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 446 ? RT30Entry.Substring(446) : "" };
                this.COSTSPENTRECOVERABLEFROMCLIENT = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 460 ? RT30Entry.Substring(460) : "" };
                this.COSTSPENTNONRECOVERABLEFROMCLIENT = new YGCDecimal(14, 2) { DataString = RT30Entry.Length > 474 ? RT30Entry.Substring(474) : "" };
                this.DEBTORNUMBER = new YGCNumber(1) { DataString = RT30Entry.Length > 488 ? RT30Entry.Substring(488) : "" };
            }

            public override Type GetType() { return typeof(RecordType30); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}",
                    base.ToString(),
                    this.RET_CODE,
                    this.PAY_DATE,
                    this.GROSS_PAY,
                    this.NET_CLIENT,
                    this.CHECK_AMT,
                    this.COST_RET,
                    this.FEES,
                    this.AGENT_FEES,
                    this.FORW_CUT,
                    this.COST_REC,
                    this.BPJ,
                    this.TA_NO,
                    this.RMIT_NO,
                    this.LINE1_3,
                    this.LINE1_5,
                    this.LINE1_6,
                    this.LINE2_1,
                    this.LINE2_2,
                    this.LINE2_5,
                    this.LINE2_6,
                    this.LINE2_7,
                    this.LINE2_8,
                    this.DESC,
                    this.POST_DATE,
                    this.REMIT_DATE,
                    this.TA_CODE,
                    this.COMMENT,
                    this.ORIGINALTANUMBER,
                    this.ORIGINALREMITNUMBER,
                    this.ORIGINALREMITDATE,
                    this.COSTSPENTRECOVERABLEFROMDEBTOR,
                    this.COSTSPENTNONRECOVERABLEFROMDEBTOR,
                    this.COSTSPENTRECOVERABLEFROMCLIENT,
                    this.COSTSPENTNONRECOVERABLEFROMCLIENT,
                    this.DEBTORNUMBER);
            }
        }
        #endregion

        #region Record Type 31
        /// <summary>
        /// Record Type 31 - Primary Debtor Information (Receiver to Sender)
        /// </summary>
        public class RecordType31 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the primary debtor's name. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D1_NAME { get; private set; }
            /// <summary>
            /// This single-byte field is a code for the salutation. The valid codes are:
            /// <para>1 - Mr.</para>
            /// <para>2 - Mrs.</para>
            /// <para>3 - Ms.</para>
            /// <para>4 - Mr. &amp; Mrs.</para>
            /// <para>5 - Dr.</para>
            /// <para>6 - Capt.</para>
            /// <para>7-9 - Gentlemen</para>
            /// </summary>
            public YGCEnum<Enums.Salutation> D1_SALUT { get; private set; }
            /// <summary>
            /// This is the primary debtor's alias name. The format is Lastname/Firstname.
            /// </summary>
            public YGCString D1_ALIAS { get; private set; }
            /// <summary>
            /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET { get; private set; }
            /// <summary>
            /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
            /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
            /// </para></summary>
            public YGCString D1_CS { get; private set; }
            /// <summary>
            /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_ZIP { get; private set; }
            /// <summary>
            /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_PHONE { get; private set; }
            /// <summary>
            /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_FAX { get; private set; }
            /// <summary>
            /// This is the primary debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public YGCString D1_SSN { get; private set; }
            /// <summary>
            /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
            /// </summary>
            public YGCString RFILE { get; private set; }
            /// <summary>
            /// This is the primary debtor's date of birth.
            /// </summary>
            public YGCDate D1_DOB { get; private set; }
            /// <summary>
            /// This is the primary debtor's driver's license number.
            /// </summary>
            public YGCString D1_DL { get; private set; }
            /// <summary>
            /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STATE { get; private set; }
            /// <summary>
            /// Set this field to Y if the served papers were returned.
            /// </summary>
            public YGCBool D1_MAIL { get; private set; }
            /// <summary>
            /// This is the date the suit was served.
            /// </summary>
            public YGCDate SERVICE_D { get; private set; }
            /// <summary>
            /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
            /// </summary>
            public YGCDate ANSWER_DUE_D { get; private set; }
            /// <summary>
            /// This is the date the debtor's response was filed.
            /// </summary>
            public YGCDate ANSWER_FILE_D { get; private set; }
            /// <summary>
            /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
            /// </summary>
            public YGCDate DEFAULT_D { get; private set; }
            /// <summary>
            /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
            /// </summary>
            public YGCDate TRIAL_D { get; private set; }
            /// <summary>
            /// This is the date of the latest hearing on a motion filed by either party.
            /// </summary>
            public YGCDate HEARING_D { get; private set; }
            /// <summary>
            /// This is the date a lien was filed against a debtor's property.
            /// </summary>
            public YGCDate LIEN_D { get; private set; }
            /// <summary>
            /// This is the date garnishment against the debtor's wages was established.
            /// </summary>
            public YGCDate GARN_D { get; private set; }
            ///<summary>
            ///This is the method used to serve the papers. 
            ///<para>The valid codes are: </para>
            ///<para>PER = Personal</para>
            ///<para>CER = Certified Mail</para>
            ///<para>SUB = Sub-service</para>
            ///<para>POS = Posting (left at front door)</para>
            ///<para>FIR = First Class Mail</para>
            ///</summary>
            public YGCString SERVICE_TYPE { get; private set; }
            /// <summary>
            /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET2 { get; private set; }
            /// <summary>
            /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public YGCString D1_CITY { get; private set; }
            /// <summary>
            /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public YGCString D1_CELL { get; private set; }
            /// <summary>
            /// Fair Isaac credit score
            /// </summary>
            public YGCNumber SCORE_FICO { get; private set; }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public YGCNumber SCORE_COLLECT { get; private set; }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public YGCNumber SCORE_OTHER { get; private set; }
            /// <summary>
            /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public YGCString D1_CNTRY { get; private set; }
            /// <summary>
            /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET_LONG { get; private set; }
            /// <summary>
            /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public YGCString D1_STREET2_LONG { get; private set; }
            /// <summary>
            /// Debtor First Name
            /// </summary>
            public YGCString FIRSTNAME { get; private set; }
            /// <summary>
            /// Debtor Last Name
            /// </summary>
            public YGCString LASTNAME { get; private set; }
            /// <summary>
            /// Creditor-Internal calculated score
            /// </summary>
            public YGCString SCOREINTERNAL { get; private set; }
            /// <summary>
            /// This field is the Placement or Internal Delinquency Stage of the account at the time of placement used by the the Creditor or Debt Buyer (User Defined).
            /// </summary>
            public YGCString STAGE { get; private set; }
            #endregion

            public RecordType31()
                : base(31)
            {
                this.D1_NAME = new YGCString(30);
                this.D1_SALUT = new YGCEnum<Salutation>(1);
                this.D1_ALIAS = new YGCString(25);
                this.D1_STREET = new YGCString(25);
                this.D1_CS = new YGCString(23);
                this.D1_ZIP = new YGCString(9);
                this.D1_PHONE = new YGCString(12);
                this.D1_FAX = new YGCString(12);
                this.D1_SSN = new YGCString(15);
                this.RFILE = new YGCString(8);
                this.D1_DOB = new YGCDate();
                this.D1_DL = new YGCString(17);
                this.D1_STATE = new YGCString(3);
                this.D1_MAIL = new YGCBool(1, "Y");
                this.SERVICE_D = new YGCDate();
                this.ANSWER_DUE_D = new YGCDate();
                this.ANSWER_FILE_D = new YGCDate();
                this.DEFAULT_D = new YGCDate();
                this.TRIAL_D = new YGCDate();
                this.HEARING_D = new YGCDate();
                this.LIEN_D = new YGCDate();
                this.GARN_D = new YGCDate();
                this.SERVICE_TYPE = new YGCString(4);
                this.D1_STREET2 = new YGCString(25);
                this.D1_CITY = new YGCString(30);
                this.D1_CELL = new YGCString(12);
                this.SCORE_FICO = new YGCNumber(3);
                this.SCORE_COLLECT = new YGCNumber(3);
                this.SCORE_OTHER = new YGCNumber(3);
                this.D1_CNTRY = new YGCString(3);
                this.D1_STREET_LONG = new YGCString(50);
                this.D1_STREET2_LONG = new YGCString(50);
                this.FIRSTNAME = new YGCString(30);
                this.LASTNAME = new YGCString(30);
                this.SCOREINTERNAL = new YGCString(5);
                this.STAGE = new YGCString(10);
            }
            public RecordType31(string RT31Entry)
                : base(RT31Entry)
            {
                try
                {
                    this.D1_NAME = new YGCString(30) { DataString = RT31Entry.Substring(67) };
                    this.D1_SALUT = new YGCEnum<Salutation>(1) { DataString = RT31Entry.Substring(97) };
                    this.D1_ALIAS = new YGCString(25) { DataString = RT31Entry.Substring(98) };
                    this.D1_STREET = new YGCString(25) { DataString = RT31Entry.Substring(123) };
                    this.D1_CS = new YGCString(23) { DataString = RT31Entry.Substring(148) };
                    this.D1_ZIP = new YGCString(9) { DataString = RT31Entry.Substring(171) };
                    this.D1_PHONE = new YGCString(12) { DataString = RT31Entry.Substring(180) };
                    this.D1_FAX = new YGCString(12) { DataString = RT31Entry.Substring(192) };
                    this.D1_SSN = new YGCString(15) { DataString = RT31Entry.Substring(204) };
                    this.RFILE = new YGCString(8) { DataString = RT31Entry.Substring(219) };
                    this.D1_DOB = new YGCDate() { DataString = RT31Entry.Substring(227) };
                    this.D1_DL = new YGCString(17) { DataString = RT31Entry.Substring(235) };
                    this.D1_STATE = new YGCString(3) { DataString = RT31Entry.Substring(252) };
                    this.D1_MAIL = new YGCBool(1, "Y") { DataString = RT31Entry.Substring(255) };
                    this.SERVICE_D = new YGCDate() { DataString = RT31Entry.Substring(256) };
                    this.ANSWER_DUE_D = new YGCDate() { DataString = RT31Entry.Substring(264) };
                    this.ANSWER_FILE_D = new YGCDate() { DataString = RT31Entry.Substring(272) };
                    this.DEFAULT_D = new YGCDate() { DataString = RT31Entry.Substring(280) };
                    this.TRIAL_D = new YGCDate() { DataString = RT31Entry.Substring(288) };
                    this.HEARING_D = new YGCDate() { DataString = RT31Entry.Substring(296) };
                    this.LIEN_D = new YGCDate() { DataString = RT31Entry.Substring(304) };
                    this.GARN_D = new YGCDate() { DataString = RT31Entry.Substring(312) };
                    this.SERVICE_TYPE = new YGCString(4) { DataString = RT31Entry.Substring(320) };
                    this.D1_STREET2 = new YGCString(25) { DataString = RT31Entry.Substring(324) };
                    this.D1_CITY = new YGCString(30) { DataString = RT31Entry.Substring(349) };
                    this.D1_CELL = new YGCString(12) { DataString = RT31Entry.Substring(379) };
                    this.SCORE_FICO = new YGCNumber(3) { DataString = RT31Entry.Substring(391) };
                    this.SCORE_COLLECT = new YGCNumber(3) { DataString = RT31Entry.Substring(394) };
                    this.SCORE_OTHER = new YGCNumber(3) { DataString = RT31Entry.Substring(397) };
                    this.D1_CNTRY = new YGCString(3) { DataString = RT31Entry.Length > 400 ? RT31Entry.Substring(400, 3) : "" };
                    this.D1_STREET_LONG = new YGCString(50) { DataString = RT31Entry.Length > 403 ? RT31Entry.Substring(403, 50) : "" };
                    this.D1_STREET2_LONG = new YGCString(50) { DataString = RT31Entry.Length > 453 ? RT31Entry.Substring(453, 50) : "" };
                    this.FIRSTNAME = new YGCString(30) { DataString = RT31Entry.Length > 503 ? RT31Entry.Substring(503) : "" };
                    this.LASTNAME = new YGCString(30) { DataString = RT31Entry.Length > 533 ? RT31Entry.Substring(533) : "" };
                    this.SCOREINTERNAL = new YGCString(5) { DataString = RT31Entry.Length > 563 ? RT31Entry.Substring(563) : "" };
                    this.STAGE = new YGCString(10) { DataString = RT31Entry.Length > 568 ? RT31Entry.Substring(568) : "" };
                }
                catch
                {
                    if (this.D1_NAME == null) this.D1_NAME = new YGCString(30);
                    if (this.D1_SALUT == null) this.D1_SALUT = new YGCEnum<Salutation>(1);
                    if (this.D1_ALIAS == null) this.D1_ALIAS = new YGCString(25);
                    if (this.D1_STREET == null) this.D1_STREET = new YGCString(25);
                    if (this.D1_CS == null) this.D1_CS = new YGCString(23);
                    if (this.D1_ZIP == null) this.D1_ZIP = new YGCString(9);
                    if (this.D1_PHONE == null) this.D1_PHONE = new YGCString(12);
                    if (this.D1_FAX == null) this.D1_FAX = new YGCString(12);
                    if (this.D1_SSN == null) this.D1_SSN = new YGCString(15);
                    if (this.RFILE == null) this.RFILE = new YGCString(8);
                    if (this.D1_DOB == null) this.D1_DOB = new YGCDate();
                    if (this.D1_DL == null) this.D1_DL = new YGCString(17);
                    if (this.D1_STATE == null) this.D1_STATE = new YGCString(3);
                    if (this.D1_MAIL == null) this.D1_MAIL = new YGCBool(1, "Y");
                    if (this.SERVICE_D == null) this.SERVICE_D = new YGCDate();
                    if (this.ANSWER_DUE_D == null) this.ANSWER_DUE_D = new YGCDate();
                    if (this.ANSWER_FILE_D == null) this.ANSWER_FILE_D = new YGCDate();
                    if (this.DEFAULT_D == null) this.DEFAULT_D = new YGCDate();
                    if (this.TRIAL_D == null) this.TRIAL_D = new YGCDate();
                    if (this.HEARING_D == null) this.HEARING_D = new YGCDate();
                    if (this.LIEN_D == null) this.LIEN_D = new YGCDate();
                    if (this.GARN_D == null) this.GARN_D = new YGCDate();
                    if (this.SERVICE_TYPE == null) this.SERVICE_TYPE = new YGCString(4);
                    if (this.D1_STREET2 == null) this.D1_STREET2 = new YGCString(25);
                    if (this.D1_CITY == null) this.D1_CITY = new YGCString(30);
                    if (this.D1_CELL == null) this.D1_CELL = new YGCString(12);
                    if (this.SCORE_FICO == null) this.SCORE_FICO = new YGCNumber(3);
                    if (this.SCORE_COLLECT == null) this.SCORE_COLLECT = new YGCNumber(3);
                    if (this.SCORE_OTHER == null) this.SCORE_OTHER = new YGCNumber(3);
                    if (this.D1_CNTRY == null) this.D1_CNTRY = new YGCString(3);
                    if (this.D1_STREET_LONG == null) this.D1_STREET_LONG = new YGCString(50);
                    if (this.D1_STREET2_LONG == null) this.D1_STREET2_LONG = new YGCString(50);
                    if (this.FIRSTNAME == null) this.FIRSTNAME = new YGCString(30);
                    if (this.LASTNAME == null) this.LASTNAME = new YGCString(30);
                    if (this.SCOREINTERNAL == null) this.SCOREINTERNAL = new YGCString(5);
                    if (this.STAGE == null) this.STAGE = new YGCString(10);
                }
            }

            public override Type GetType() { return typeof(RecordType31); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}",
                    base.ToString(),
                    this.D1_NAME,
                    this.D1_SALUT,
                    this.D1_ALIAS,
                    this.D1_STREET,
                    this.D1_CS,
                    this.D1_ZIP,
                    this.D1_PHONE,
                    this.D1_FAX,
                    this.D1_SSN,
                    this.RFILE,
                    this.D1_DOB,
                    this.D1_DL,
                    this.D1_STATE,
                    this.D1_MAIL,
                    this.SERVICE_D,
                    this.ANSWER_DUE_D,
                    this.ANSWER_FILE_D,
                    this.DEFAULT_D,
                    this.TRIAL_D,
                    this.HEARING_D,
                    this.LIEN_D,
                    this.GARN_D,
                    this.SERVICE_TYPE,
                    this.D1_STREET2,
                    this.D1_CITY,
                    this.D1_CELL,
                    this.SCORE_FICO,
                    this.SCORE_COLLECT,
                    this.SCORE_OTHER,
                    this.D1_CNTRY,
                    this.D1_STREET_LONG,
                    this.D1_STREET2_LONG,
                    this.FIRSTNAME,
                    this.LASTNAME,
                    this.SCOREINTERNAL,
                    this.STAGE);
            }
        }
        #endregion

        #region Record Type 39
        /// <summary>
        /// Record Type 39 - Message (Receiver to Sender)
        /// </summary>
        public class RecordType39 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// Event Date
            /// </summary>
            public YGCDate PDATE { get; private set; }
            /// <summary>
            /// Status Update Code
            /// </summary>
            public YGCString PCODE { get; private set; }
            /// <summary>
            /// Free Text Comment
            /// </summary>
            public YGCString PCMT { get; private set; }
            /// <summary>
            /// Time at which the event occurred
            /// <para>24-Hour Format</para>
            /// </summary>
            public YGCTime PTIME { get; private set; }
            /// <summary>
            /// Local Time Zone
            /// </summary>
            public YGCDict<Enums.TimeZone> PTIME_ZONE { get; private set; }
            /// <summary>
            /// Phone Number Dialed
            /// </summary>
            public YGCString PHONE_NUMBER { get; private set; }
            /// <summary>
            /// Call Direction
            /// </summary>
            public YGCEnum<Enums.CallDirection, Enums.CallDirectionValues> CALL_DIRECTION { get; private set; }
            /// <summary>
            /// Debtor Number
            /// </summary>
            public YGCNumber DBTR_TYPE { get; private set; }
            #endregion

            public RecordType39()
                : base(39)
            {
                this.PDATE = new YGCDate();
                this.PCODE = new YGCString(8);
                this.PCMT = new YGCString(1024);
                this.PTIME = new YGCTime();
                this.PTIME_ZONE = new YGCDict<Enums.TimeZone>(9, Dictionaries.TimeZoneDictionary);
                this.PHONE_NUMBER = new YGCString(15);
                this.CALL_DIRECTION = new YGCEnum<CallDirection, CallDirectionValues>(1);
                this.DBTR_TYPE = new YGCNumber(3);
            }
            public RecordType39(string RT39Entry)
                : base(RT39Entry)
            {
                try
                {
                    this.PDATE = new YGCDate() { DataString = RT39Entry.Length > 67 ? RT39Entry.Substring(67) : "" };
                    this.PCODE = new YGCString(8) { DataString = RT39Entry.Length > 75 ? RT39Entry.Substring(75) : "" };
                    this.PCMT = new YGCString(1024) { DataString = RT39Entry.Length > 83 ? RT39Entry.Substring(83) : "" };
                    this.PTIME = new YGCTime() { DataString = RT39Entry.Length > 1107 ? RT39Entry.Substring(1107) : "" };
                    this.PTIME_ZONE = new YGCDict<Enums.TimeZone>(9, Dictionaries.TimeZoneDictionary) { DataString = RT39Entry.Length > 1113 ? RT39Entry.Substring(1113) : "" };
                    this.PHONE_NUMBER = new YGCString(15) { DataString = RT39Entry.Length > 1122 ? RT39Entry.Substring(1122) : "" };
                    this.CALL_DIRECTION = new YGCEnum<CallDirection, CallDirectionValues>(1) { DataString = RT39Entry.Length > 1137 ? RT39Entry.Substring(1137) : "" };
                    this.DBTR_TYPE = new YGCNumber(3) { DataString = RT39Entry.Length > 1138 ? RT39Entry.Substring(1138) : "" };
                }
                catch
                {
                    if (this.PDATE == null) this.PDATE = new YGCDate();
                    if (this.PCODE == null) this.PCODE = new YGCString(8);
                    if (this.PCMT == null) this.PCMT = new YGCString(1024);
                    if (this.PTIME == null) this.PTIME = new YGCTime();
                    if (this.PTIME_ZONE == null) this.PTIME_ZONE = new YGCDict<Enums.TimeZone>(9, Dictionaries.TimeZoneDictionary);
                    if (this.PHONE_NUMBER == null) this.PHONE_NUMBER = new YGCString(15);
                    if (this.CALL_DIRECTION == null) this.CALL_DIRECTION = new YGCEnum<CallDirection, CallDirectionValues>(1);
                    if (this.DBTR_TYPE == null) this.DBTR_TYPE = new YGCNumber(3);
                }
            }

            public override Type GetType() { return typeof(RecordType39); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                    base.ToString(),
                    this.PDATE,
                    this.PCODE,
                    this.PCMT,
                    this.PTIME,
                    this.PTIME_ZONE,
                    this.PHONE_NUMBER,
                    this.CALL_DIRECTION,
                    this.DBTR_TYPE);
            }
        }
        #endregion

        #region Record Type 41
        /// <summary>
        /// Record Type 41 - Suit / Judgment Information (Receiver to Sender)
        /// </summary>
        public class RecordType41 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the dollar amount requested in the suit. If this field is populated, SUIT_DATE must contain a valid date.
            /// </summary>
            public YGCDecimal SUIT_AMT { get; private set; }
            /// <summary>
            /// This is the date the suit was filed. If this field is populated, SUIT_AMT must be non-zero.
            /// </summary>
            public YGCDate SUIT_DATE { get; private set; }
            /// <summary>
            /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
            /// </summary>
            public YGCDecimal CNTRCT_FEE { get; private set; }
            /// <summary>
            /// This is the statutory fee awarded only to the attorney, determined by the debtor state.
            /// </summary>
            public YGCDecimal STAT_FEE { get; private set; }
            /// <summary>
            /// Initial number assigned to the suit.
            /// </summary>
            public YGCString DOCKET_NO { get; private set; }
            /// <summary>
            /// Depending on the state the suit is filed in, a new number may be assigned upon judgment.
            /// </summary>
            public YGCString JDGMNT_NO { get; private set; }
            /// <summary>
            /// This is the date the judgment was entered. If this field is populated, JDGMNT_AMT must be non-zero.
            /// </summary>
            public YGCDate JDGMNT_DATE { get; private set; }
            /// <summary>
            /// This is the dollar amount awarded in the judgment. If this field is populated, JDGMNT_DATE must contain a valid date.
            /// </summary>
            public YGCDecimal JDGMNT_AMT { get; private set; }
            /// <summary>
            /// This is the dollar amount of the interest due before the judgment was rendered.
            /// </summary>
            public YGCDecimal PREJ_INT { get; private set; }
            /// <summary>
            /// This is the sum of the costs to the sender to carry the suit forward, such as attorney fees.
            /// </summary>
            public YGCDecimal JDG_COSTS { get; private set; }
            /// <summary>
            /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form.
            /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
            /// </summary>
            public YGCDecimal RATES_PRE { get; private set; }
            /// <summary>
            /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form.
            /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
            /// </summary>
            public YGCDecimal RATES_POST { get; private set; }
            /// <summary>
            /// Set this to Y if the statutory fee is actually kept by the law firm.
            /// </summary>
            public YGCBool STAT_FLAG { get; private set; }
            /// <summary>
            /// Set this to Y if PREJ_INT is added to the principal when judgment is rendered.
            /// </summary>
            public YGCBool INT_FLAG { get; private set; }
            /// <summary>
            /// This is the principal amount of the judgment.
            /// </summary>
            public YGCDecimal JDG_PRIN { get; private set; }
            /// <summary>
            /// This is the difference between what was requested in the suit and the judgment amount.
            /// </summary>
            public YGCDecimal ADJUSTMENT { get; private set; }
            /// <summary>
            /// This is the sum of PREJ_INT + JDG_COSTS + JUDG_PRIN + CNTRCT_FEE + STAT_FEE.
            /// </summary>
            public YGCDecimal JDGMNT_BAL { get; private set; }
            /// <summary>
            /// This is the county where the suit was filed.
            /// </summary>
            public YGCString LEGAL_COUNTY { get; private set; }
            /// <summary>
            /// This is the abbreviation of the state where the suit was filed.
            /// </summary>
            public YGCString LEGAL_STATE { get; private set; }
            /// <summary>
            /// Typically the full name of the court. Ex: Gwinnett County State Court, THE NINTH JUDICIAL CIRCUIT.
            /// </summary>
            public YGCString CRT_DESIG { get; private set; }
            /// <summary>
            /// This is a more brief description of the court. Ex: Superior, Supreme, Circuit
            /// </summary>
            public YGCString CRT_TYPE { get; private set; }
            /// <summary>
            /// This is the date when the suit judgment expires. It is usually between 7 and 12 years from the filing date.
            /// </summary>
            public YGCDate JDGMNT_EXP_DATE { get; private set; }
            /// <summary>
            /// This is the standard code for the country where the suit was filed.
            /// </summary>
            public YGCString LEGAL_CNTRY { get; private set; }
            /// <summary>
            /// This is name of the court or clerk.
            /// </summary>
            public YGCString CRT_NAME { get; private set; }
            /// <summary>
            /// This is a free-form text field explaining the reason for filing suit.
            /// </summary>
            public YGCString SUITREASON { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the primary debtor from the latest Record 02, Record 31 or Record 51.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR1 { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the 2nd debtor name in the latest Record 03, Record 33 or Record 53.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR2 { get; private set; }
            /// <summary>
            /// (Y/N) This denotes if the judgment is for the 3rd debtor name in the latest Record 03, Record 33 or Record 53.
            /// </summary>
            public YGCBool JUDGMENTONDEBTOR3 { get; private set; }
            /// <summary>
            /// This is the date the judgment was recorded; this may be different than JDGMNT_DATE.
            /// </summary>
            public YGCDate JUDGMENTRECORDEDDATE { get; private set; }
            /// <summary>
            /// This is the date that the law firm generated the suit documents and sent them to court, as opposed to the date the suit was filed by the court.
            /// </summary>
            public YGCDate SUITISSUEDDATE { get; private set; }
            #endregion

            public RecordType41() : base(41)
            {
                this.SUIT_AMT = new YGCDecimal(14, 2);
                this.SUIT_DATE = new YGCDate();
                this.CNTRCT_FEE = new YGCDecimal(14, 2);
                this.STAT_FEE = new YGCDecimal(14, 2);
                this.DOCKET_NO = new YGCString(15);
                this.JDGMNT_NO = new YGCString(15);
                this.JDGMNT_DATE = new YGCDate();
                this.JDGMNT_AMT = new YGCDecimal(14, 2);
                this.PREJ_INT = new YGCDecimal(14, 2);
                this.JDG_COSTS = new YGCDecimal(14, 2);
                this.RATES_PRE = new YGCDecimal(5, 4);
                this.RATES_POST = new YGCDecimal(5, 4);
                this.STAT_FLAG = new YGCBool(1, "Y", "N");
                this.INT_FLAG = new YGCBool(1, "Y", "N");
                this.JDG_PRIN = new YGCDecimal(14, 2);
                this.ADJUSTMENT = new YGCDecimal(14, 2);
                this.JDGMNT_BAL = new YGCDecimal(14, 2);
                this.LEGAL_COUNTY = new YGCString(20);
                this.LEGAL_STATE = new YGCString(4);
                this.CRT_DESIG = new YGCString(30);
                this.CRT_TYPE = new YGCString(15);
                this.JDGMNT_EXP_DATE = new YGCDate();
                this.LEGAL_CNTRY = new YGCString(3);
                this.CRT_NAME = new YGCString(50);
                this.SUITREASON = new YGCString(30);
                this.JUDGMENTONDEBTOR1 = new YGCBool(1, "Y", "N");
                this.JUDGMENTONDEBTOR2 = new YGCBool(1, "Y", "N");
                this.JUDGMENTONDEBTOR3 = new YGCBool(1, "Y", "N");
                this.JUDGMENTRECORDEDDATE = new YGCDate();
                this.SUITISSUEDDATE = new YGCDate();
            }
            public RecordType41(string RT41Entry)
                : base(RT41Entry)
            {
                this.SUIT_AMT = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 67 ? RT41Entry.Substring(67) : "" };
                this.SUIT_DATE = new YGCDate() { DataString = RT41Entry.Length > 81 ? RT41Entry.Substring(81) : "" };
                this.CNTRCT_FEE = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 89 ? RT41Entry.Substring(89) : "" };
                this.STAT_FEE = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 103 ? RT41Entry.Substring(103) : "" };
                this.DOCKET_NO = new YGCString(15) { DataString = RT41Entry.Length > 117 ? RT41Entry.Substring(117) : "" };
                this.JDGMNT_NO = new YGCString(15) { DataString = RT41Entry.Length > 132 ? RT41Entry.Substring(132) : "" };
                this.JDGMNT_DATE = new YGCDate() { DataString = RT41Entry.Length > 147 ? RT41Entry.Substring(147) : "" };
                this.JDGMNT_AMT = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 155 ? RT41Entry.Substring(155) : "" };
                this.PREJ_INT = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 169 ? RT41Entry.Substring(169) : "" };
                this.JDG_COSTS = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 183 ? RT41Entry.Substring(183) : "" };
                this.RATES_PRE = new YGCDecimal(5, 4) { DataString = RT41Entry.Length > 197 ? RT41Entry.Substring(197) : "" };
                this.RATES_POST = new YGCDecimal(5, 4) { DataString = RT41Entry.Length > 202 ? RT41Entry.Substring(202) : "" };
                this.STAT_FLAG = new YGCBool(1, "Y", "N") { DataString = RT41Entry.Length > 207 ? RT41Entry.Substring(207) : "" };
                this.INT_FLAG = new YGCBool(1, "Y", "N") { DataString = RT41Entry.Length > 208 ? RT41Entry.Substring(208) : "" };
                this.JDG_PRIN = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 209 ? RT41Entry.Substring(209) : "" };
                this.ADJUSTMENT = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 223 ? RT41Entry.Substring(223) : "" };
                this.JDGMNT_BAL = new YGCDecimal(14, 2) { DataString = RT41Entry.Length > 237 ? RT41Entry.Substring(237) : "" };
                this.LEGAL_COUNTY = new YGCString(20) { DataString = RT41Entry.Length > 251 ? RT41Entry.Substring(251) : "" };
                this.LEGAL_STATE = new YGCString(4) { DataString = RT41Entry.Length > 271 ? RT41Entry.Substring(271) : "" };
                this.CRT_DESIG = new YGCString(30) { DataString = RT41Entry.Length > 275 ? RT41Entry.Substring(275) : "" };
                this.CRT_TYPE = new YGCString(15) { DataString = RT41Entry.Length > 305 ? RT41Entry.Substring(305) : "" };
                this.JDGMNT_EXP_DATE = new YGCDate() { DataString = RT41Entry.Length > 320 ? RT41Entry.Substring(320) : "" };
                this.LEGAL_CNTRY = new YGCString(3) { DataString = RT41Entry.Length > 328 ? RT41Entry.Substring(328) : "" };
                this.CRT_NAME = new YGCString(50) { DataString = RT41Entry.Length > 331 ? RT41Entry.Substring(331) : "" };
                this.SUITREASON = new YGCString(30) { DataString = RT41Entry.Length > 381 ? RT41Entry.Substring(381) : "" };
                this.JUDGMENTONDEBTOR1 = new YGCBool(1, "Y", "N") { DataString = RT41Entry.Length > 411 ? RT41Entry.Substring(411) : "" };
                this.JUDGMENTONDEBTOR2 = new YGCBool(1, "Y", "N") { DataString = RT41Entry.Length > 412 ? RT41Entry.Substring(412) : "" };
                this.JUDGMENTONDEBTOR3 = new YGCBool(1, "Y", "N") { DataString = RT41Entry.Length > 413 ? RT41Entry.Substring(413) : "" };
                this.JUDGMENTRECORDEDDATE = new YGCDate() { DataString = RT41Entry.Length > 414 ? RT41Entry.Substring(414) : "" };
                this.SUITISSUEDDATE = new YGCDate() { DataString = RT41Entry.Length > 422 ? RT41Entry.Substring(422) : "" };
            }

            public override Type GetType() { return typeof(RecordType41); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}",
                    base.ToString(),
                    this.SUIT_AMT,
                    this.SUIT_DATE,
                    this.CNTRCT_FEE,
                    this.STAT_FEE,
                    this.DOCKET_NO,
                    this.JDGMNT_NO,
                    this.JDGMNT_DATE,
                    this.JDGMNT_AMT,
                    this.PREJ_INT,
                    this.JDG_COSTS,
                    this.RATES_PRE,
                    this.RATES_POST,
                    this.STAT_FLAG,
                    this.INT_FLAG,
                    this.JDG_PRIN,
                    this.ADJUSTMENT,
                    this.JDGMNT_BAL,
                    this.LEGAL_COUNTY,
                    this.LEGAL_STATE,
                    this.CRT_DESIG,
                    this.CRT_TYPE,
                    this.JDGMNT_EXP_DATE,
                    this.LEGAL_CNTRY,
                    this.CRT_NAME,
                    this.SUITREASON,
                    this.JUDGMENTONDEBTOR1,
                    this.JUDGMENTONDEBTOR2,
                    this.JUDGMENTONDEBTOR3,
                    this.JUDGMENTRECORDEDDATE,
                    this.SUITISSUEDDATE);
            }
        }
        #endregion

        #region Record Type 42
        /// <summary>
        /// Record Type 42 - Balance/Interest Update (Receiver to Sender)
        /// </summary>
        public class RecordType42 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form.
            /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
            /// </summary>
            public YGCDecimal RATES_PRE { get; private set; }
            /// <summary>
            /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form.
            /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
            /// </summary>
            public YGCDecimal RATES_POST { get; private set; }
            /// <summary>
            /// This is the interest dollar amount accrued each day.
            /// </summary>
            public YGCDecimal PER_DIEM { get; private set; }
            /// <summary>
            /// This is the dollar amount upon which the interest is accrued. It is usually just the principal, but can include other factors such as attorney fees and costs.
            /// </summary>
            public YGCDecimal INT_BASE { get; private set; }
            /// <summary>
            /// This is the total interest accrued to date.
            /// </summary>
            public YGCDecimal IAMOUNT { get; private set; }
            /// <summary>
            /// The total amount paid to date applied towards interest.
            /// </summary>
            public YGCDecimal IPAID { get; private set; }
            /// <summary>
            /// This is the date the interest was last calculated.
            /// </summary>
            public YGCDate IDATE { get; private set; }
            /// <summary>
            /// This is the total principal owed on the debt.
            /// </summary>
            public YGCDecimal PRIN_AMT { get; private set; }
            /// <summary>
            /// The total amount paid to date applied towards principal.
            /// </summary>
            public YGCDecimal PRIN_PAID { get; private set; }
            /// <summary>
            /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
            /// </summary>
            public YGCDecimal CNTRCT_AMT { get; private set; }
            /// <summary>
            /// This is the amount actually paid towards the contract fee (CNTRCT_AMT).
            /// </summary>
            public YGCDecimal CNTRCT_PAID { get; private set; }
            /// <summary>
            /// This is the statutory fee awarded only to the attorney, determined by the debtor state.
            /// </summary>
            public YGCDecimal STAT_AMT { get; private set; }
            /// <summary>
            /// This is the amount actually paid towards the statutory fee (STAT_AMT).
            /// </summary>
            public YGCDecimal STAT_PAID { get; private set; }
            /// <summary>
            /// This is the dollar amount of the legal costs billed.
            /// </summary>
            public YGCDecimal COST_AMT { get; private set; }
            /// <summary>
            /// This is the dollar amount of the legal costs actually paid.
            /// </summary>
            public YGCDecimal COST_PAID { get; private set; }
            /// <summary>
            /// This is the debtor principal and costs due. The interest is not to be included here.
            /// </summary>
            public YGCDecimal DBAL { get; private set; }
            /// <summary>
            /// This is the dollar amount of the debtor interest due.
            /// </summary>
            public YGCDecimal IBAL { get; private set; }
            /// <summary>
            /// Set this to Y if the statutory fee is actually kept by the law firm.
            /// </summary>
            public YGCBool STAT_FLAG { get; private set; }
            #endregion

            public RecordType42()
                : base(42)
            {
                this.RATES_PRE = new YGCDecimal(5, 4);
                this.RATES_POST = new YGCDecimal(5, 4);
                this.PER_DIEM = new YGCDecimal(14, 8);
                this.INT_BASE = new YGCDecimal(14, 2);
                this.IAMOUNT = new YGCDecimal(14, 2);
                this.IPAID = new YGCDecimal(14, 2);
                this.IDATE = new YGCDate();
                this.PRIN_AMT = new YGCDecimal(14, 2);
                this.PRIN_PAID = new YGCDecimal(14, 2);
                this.CNTRCT_AMT = new YGCDecimal(14, 2);
                this.CNTRCT_PAID = new YGCDecimal(14, 2);
                this.STAT_AMT = new YGCDecimal(14, 2);
                this.STAT_PAID = new YGCDecimal(14, 2);
                this.COST_AMT = new YGCDecimal(14, 2);
                this.COST_PAID = new YGCDecimal(14, 2);
                this.DBAL = new YGCDecimal(14, 2);
                this.IBAL = new YGCDecimal(14, 2);
                this.STAT_FLAG = new YGCBool(1, "Y");
            }
            public RecordType42(string RT42Entry)
                : base(RT42Entry)
            {
                this.RATES_PRE = new YGCDecimal(5, 4) { DataString = RT42Entry.Length > 67 ? RT42Entry.Substring(67) : "" };
                this.RATES_POST = new YGCDecimal(5, 4) { DataString = RT42Entry.Length > 72 ? RT42Entry.Substring(72) : "" };
                this.PER_DIEM = new YGCDecimal(14, 8) { DataString = RT42Entry.Length > 77 ? RT42Entry.Substring(77) : "" };
                this.INT_BASE = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 91 ? RT42Entry.Substring(91) : "" };
                this.IAMOUNT = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 105 ? RT42Entry.Substring(105) : "" };
                this.IPAID = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 119 ? RT42Entry.Substring(119) : "" };
                this.IDATE = new YGCDate() { DataString = RT42Entry.Length > 133 ? RT42Entry.Substring(133) : "" };
                this.PRIN_AMT = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 141 ? RT42Entry.Substring(141) : "" };
                this.PRIN_PAID = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 155 ? RT42Entry.Substring(155) : "" };
                this.CNTRCT_AMT = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 169 ? RT42Entry.Substring(169) : "" };
                this.CNTRCT_PAID = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 183 ? RT42Entry.Substring(183) : "" };
                this.STAT_AMT = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 197 ? RT42Entry.Substring(197) : "" };
                this.STAT_PAID = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 211 ? RT42Entry.Substring(211) : "" };
                this.COST_AMT = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 225 ? RT42Entry.Substring(225) : "" };
                this.COST_PAID = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 239 ? RT42Entry.Substring(239) : "" };
                this.DBAL = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 253 ? RT42Entry.Substring(253) : "" };
                this.IBAL = new YGCDecimal(14, 2) { DataString = RT42Entry.Length > 267 ? RT42Entry.Substring(267) : "" };
                this.STAT_FLAG = new YGCBool(1, "Y") { DataString = RT42Entry.Length > 281 ? RT42Entry.Substring(281) : "" };
            }

            public override Type GetType() { return typeof(RecordType42); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}",
                    base.ToString(),
                    this.RATES_PRE,
                    this.RATES_POST,
                    this.PER_DIEM,
                    this.INT_BASE,
                    this.IAMOUNT,
                    this.IPAID,
                    this.IDATE,
                    this.PRIN_AMT,
                    this.PRIN_PAID,
                    this.CNTRCT_AMT,
                    this.CNTRCT_PAID,
                    this.STAT_AMT,
                    this.STAT_PAID,
                    this.COST_AMT,
                    this.COST_PAID,
                    this.DBAL,
                    this.IBAL,
                    this.STAT_FLAG
                    );
            }
        }
        #endregion

        #region Record Type 44
        /// <summary>
        /// Record Type 44 - Bankruptcy (Receiver to Sender)
        /// </summary>
        public class RecordType44 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <summary>
            /// This identifies which debtor in the account is filing for bankruptcy.
            /// <para></para>
            /// <para>1 = Primary debtor</para>
            /// <para>2 = Second debtor</para>
            /// <para>3 = Third debtor</para>
            /// <para></para>
            /// <para>The primary debtor is determined by the latest record 02 or 31 and the second and third debtors are determined by the latest record 03 or 33.<para>
            /// </summary>
            public YGCNumber DBTR_NUM { get; private set; }
            /// <summary>
            /// Chapter within the bankruptcy code; typically 7 or 13 for individuals.
            /// </summary>
            public YGCNumber CHAPTER { get; private set; }
            /// <summary>
            /// This is the court-issued case number for the bankruptcy.
            /// </summary>
            public YGCString BK_FILENO { get; private set; }
            /// <summary>
            /// This is a non-restrictive field describing where the bankruptcy was filed. You can use this for the jurisdiction of the court.
            /// </summary>
            public YGCString LOC { get; private set; }
            /// <summary>
            /// This is the date the bankruptcy was filed.
            /// </summary>
            public YGCDate FILED_DATE { get; private set; }
            /// <summary>
            /// If something was handled incorrectly, the bankruptcy may be dismissed; this is the date of dismissal.
            /// </summary>
            public YGCDate DSMIS_DATE { get; private set; }
            /// <summary>
            /// The date a discharge is issued for the debtor. The discharge relieves the debtor of personal liability outside of what is put up as collateral.
            /// </summary>
            public YGCDate DSCHG_DATE { get; private set; }
            /// <summary>
            /// This is the date the court actually adjudicates the case.
            /// </summary>
            public YGCDate CLOSE_DATE { get; private set; }
            /// <summary>
            /// This is the date the bankruptcy is converted from one chapter to another, usually from 13 to 7.
            /// </summary>
            public YGCDate CNVRT_DATE { get; private set; }
            /// <summary>
            /// This is the date that the initial meeting takes place between creditors and debtors, usually 30 days after FILED_DATE.
            /// </summary>
            public YGCDate MTG_341_DATE { get; private set; }
            /// <summary>
            /// This is the time of day that the initial meeting takes place between creditors and debtors.  It should be in one of the following formats:
            /// <para></para>
            /// <para>HH:MM:SS if using 24-hour notation</para>
            /// <para>HH:MM AM if using 12-hour notation for before noon</para>
            /// <para>HH:MM PM if using 12-hour notation for after noon</para>
            /// </summary>
            public YGCTime MTG_341_TIME { get; private set; }
            /// <summary>
            /// This is the location that the initial meeting takes place between creditors and debtors. There is no format restriction.
            /// </summary>
            public YGCString MTG_341_LOC { get; private set; }
            /// <summary>
            /// These are the initials of the name of the bankruptcy judge.
            /// </summary>
            public YGCString JUDGE_INIT { get; private set; }
            /// <summary>
            /// The debtor may choose to re-affirm the debt that would have been discharged by the bankruptcy. This is the amount the debtor agrees to pay in full; the re-affirmation survives the bankruptcy.
            /// </summary>
            public YGCDecimal REAF_AMT { get; private set; }
            /// <summary>
            /// This is the date when the re-affirmation was signed.
            /// </summary>
            public YGCDate REAF_DATE { get; private set; }
            /// <summary>
            /// This is the periodic payment in a Chapter 11 or 13 bankruptcy or a lump sum payment of a Chapter 7 bankruptcy.
            /// </summary>
            public YGCDecimal PAY_AMT { get; private set; }
            /// <summary>
            /// This is the date that the payment arrangement was agreed upon.
            /// </summary>
            public YGCDate PAY_DATE { get; private set; }
            /// <summary>
            /// This is the date the judge approves the payment plan.
            /// </summary>
            public YGCDate CONF_DATE { get; private set; }
            /// <summary>
            /// This is the date the debtor caught up with paying all arrearages, fees and interest.
            /// </summary>
            public YGCDate CURE_DATE { get; private set; }
            /// <summary>
            /// If the bankruptcy proceedings were put on hold, this is the date the stay was lifted.
            /// </summary>
            public YGCDate STAY_LIFTED_DATE { get; private set; }
            #endregion

            public RecordType44()
                : base(44)
            {
                this.DBTR_NUM = new YGCNumber(3);
                this.CHAPTER = new YGCNumber(3);
                this.BK_FILENO = new YGCString(15);
                this.LOC = new YGCString(40);
                this.FILED_DATE = new YGCDate();
                this.DSMIS_DATE = new YGCDate();
                this.DSCHG_DATE = new YGCDate();
                this.CLOSE_DATE = new YGCDate();
                this.CNVRT_DATE = new YGCDate();
                this.MTG_341_DATE = new YGCDate();
                this.MTG_341_TIME = new YGCTime();
                this.MTG_341_LOC = new YGCString(40);
                this.JUDGE_INIT = new YGCString(3);
                this.REAF_AMT = new YGCDecimal(14, 2);
                this.REAF_DATE = new YGCDate();
                this.PAY_AMT = new YGCDecimal(14, 2);
                this.PAY_DATE = new YGCDate();
                this.CONF_DATE = new YGCDate();
                this.CURE_DATE = new YGCDate();
                this.STAY_LIFTED_DATE = new YGCDate();
            }
            public RecordType44(string RT44Entry)
                : base(RT44Entry)
            {
                this.DBTR_NUM = new YGCNumber(3) { DataString = RT44Entry.Length > 67 ? RT44Entry.Substring(67) : "" };
                this.CHAPTER = new YGCNumber(3) { DataString = RT44Entry.Length > 70 ? RT44Entry.Substring(70) : "" };
                this.BK_FILENO = new YGCString(15) { DataString = RT44Entry.Length > 73 ? RT44Entry.Substring(73) : "" };
                this.LOC = new YGCString(40) { DataString = RT44Entry.Length > 88 ? RT44Entry.Substring(88) : "" };
                this.FILED_DATE = new YGCDate() { DataString = RT44Entry.Length > 128 ? RT44Entry.Substring(128) : "" };
                this.DSMIS_DATE = new YGCDate() { DataString = RT44Entry.Length > 136 ? RT44Entry.Substring(136) : "" };
                this.DSCHG_DATE = new YGCDate() { DataString = RT44Entry.Length > 144 ? RT44Entry.Substring(144) : "" };
                this.CLOSE_DATE = new YGCDate() { DataString = RT44Entry.Length > 152 ? RT44Entry.Substring(152) : "" };
                this.CNVRT_DATE = new YGCDate() { DataString = RT44Entry.Length > 160 ? RT44Entry.Substring(160) : "" };
                this.MTG_341_DATE = new YGCDate() { DataString = RT44Entry.Length > 168 ? RT44Entry.Substring(168) : "" };
                this.MTG_341_TIME = new YGCTime() { DataString = RT44Entry.Length > 176 ? RT44Entry.Substring(176) : "" };
                this.MTG_341_LOC = new YGCString(40) { DataString = RT44Entry.Length > 184 ? RT44Entry.Substring(184) : "" };
                this.JUDGE_INIT = new YGCString(3) { DataString = RT44Entry.Length > 224 ? RT44Entry.Substring(224) : "" };
                this.REAF_AMT = new YGCDecimal(14, 2) { DataString = RT44Entry.Length > 227 ? RT44Entry.Substring(227) : "" };
                this.REAF_DATE = new YGCDate() { DataString = RT44Entry.Length > 241 ? RT44Entry.Substring(241) : "" };
                this.PAY_AMT = new YGCDecimal(14, 2) { DataString = RT44Entry.Length > 249 ? RT44Entry.Substring(249) : "" };
                this.PAY_DATE = new YGCDate() { DataString = RT44Entry.Length > 263 ? RT44Entry.Substring(263) : "" };
                this.CONF_DATE = new YGCDate() { DataString = RT44Entry.Length > 271 ? RT44Entry.Substring(271) : "" };
                this.CURE_DATE = new YGCDate() { DataString = RT44Entry.Length > 279 ? RT44Entry.Substring(279) : "" };
                this.STAY_LIFTED_DATE = new YGCDate() { DataString = RT44Entry.Length > 287 ? RT44Entry.Substring(287) : "" };
            }

            public override Type GetType() { return typeof(RecordType44); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}",
                    base.ToString(),
                    this.DBTR_NUM,
                    this.CHAPTER,
                    this.BK_FILENO,
                    this.LOC,
                    this.FILED_DATE,
                    this.DSMIS_DATE,
                    this.DSCHG_DATE,
                    this.CLOSE_DATE,
                    this.CNVRT_DATE,
                    this.MTG_341_DATE,
                    this.MTG_341_TIME,
                    this.MTG_341_LOC,
                    this.JUDGE_INIT,
                    this.REAF_AMT,
                    this.REAF_DATE,
                    this.PAY_AMT,
                    this.PAY_DATE,
                    this.CONF_DATE,
                    this.CURE_DATE,
                    this.STAY_LIFTED_DATE
                    );
            }
        }
        #endregion

        #region Record Type 46
        /// <summary>
        /// Record Type 46 - Physical Assets (property/vehicle) (Receiver to Sender)
        /// <para>This record is sent by the receiver to record a real estate or automobile asset owned by a debtor. Value = 46.</para>
        /// </summary>
        public class RecordType46 : YGCReceiver2SenderRecord
        {
            #region Public Properties
            /// <Summary>
            /// This identifies which debtor in the account owns the asset. Values are:
            /// <para>   1 - Primary debtor</para>
            /// <para>   2 - Second debtor</para>
            /// <para>   3 - Third debtor</para>
            /// </Summary>
            public YGCNumber DBTR_NUM { get; private set; }
            /// <Summary>
            /// This number distinguishes assets owned by the debtor.
            /// </Summary>
            public YGCString ASSET_ID { get; private set; }
            /// <Summary>
            /// This is the full name of the debtor who owns this asset.
            /// </Summary>
            public YGCString ASSET_OWNER { get; private set; }
            /// <Summary>
            /// This is the street address where the asset is located.
            /// </Summary>
            public YGCString STREET { get; private set; }

            /// <Summary>
            /// This is an overflow field for STREET.
            /// </Summary>
            public YGCString STREET_2 { get; private set; }
            /// <Summary>
            /// This is an overflow field for STREET_2.
            /// </Summary>
            public YGCString STREET_3 { get; private set; }
            /// <Summary>
            /// This is the city where the asset is located.
            /// </Summary>
            public YGCString CITY { get; private set; }
            /// <Summary>
            /// This can be used as an alternative or supplement to CITY. It can hold the town or borough where the asset is located.
            /// </Summary>
            public YGCString TOWN { get; private set; }
            /// <Summary>
            /// This is the county where the asset is located.
            /// </Summary>
            public YGCString CNTY { get; private set; }
            /// <Summary>
            /// This is the abbreviation of the state where the asset is located.
            /// </Summary>
            public YGCString STATE { get; private set; }
            /// <Summary>
            /// This is the zip code where the asset is located.
            /// </Summary>
            public YGCString ZIP { get; private set; }
            /// <Summary>
            /// This is the abbreviation of the country where the asset is located.
            /// </Summary>
            public YGCString CNTRY { get; private set; }
            /// <Summary>
            /// This is the phone number of the asset location. It can accommodate separators for the area code and exchange.
            /// </Summary>
            public YGCString PHONE { get; private set; }
            /// <Summary>
            /// This is the block number if this is a land asset.
            /// </Summary>
            public YGCString BLOCK { get; private set; }
            /// <Summary>
            /// This is the lot number if this is a land asset.
            /// </Summary>
            public YGCString LOT { get; private set; }
            /// <Summary>
            /// This is the dollar value of the asset.
            /// </Summary>
            public YGCDecimal ASSET_VALUE { get; private set; }
            /// <Summary>
            /// This is a free-text description of the asset.
            /// </Summary>
            public YGCString ASSET_DESC { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the vehicle ID number.
            /// </Summary>
            public YGCString ASSET_VIN { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the license plate number.
            /// </Summary>
            public YGCString ASSET_LIC_PLATE { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the color.
            /// </Summary>
            public YGCString ASSET_COLOR { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the year the auto was made.
            /// </Summary>
            public YGCString ASSET_YEAR { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the name of the make.
            /// </Summary>
            public YGCString ASSET_MAKE { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the name of the model.
            /// </Summary>
            public YGCString ASSET_MODEL { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the repossession file number assigned by the creditor.
            /// </Summary>
            public YGCString REPO_FILE_NUM { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the date the repossession occurred.
            /// </Summary>
            public YGCDate REPO_D { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the value of the auto. This is the same as ASSET_VALUE.
            /// </Summary>
            public YGCDecimal REPO_AMT { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the name of the new owner as stated on the title.
            /// </Summary>
            public YGCString CERT_TITLE_NAME { get; private set; }
            /// <Summary>
            /// If the asset is an automobile, this is the date the certification title was transferred.
            /// </Summary>
            public YGCDate CERT_TITLE_D { get; private set; }
            /// <Summary>
            /// If the asset is real estate, this is the foreclosure date.
            /// </Summary>
            public YGCDate MORT_FRCL_D { get; private set; }
            /// <Summary>
            /// This is the court-issued case number for the foreclosure.
            /// </Summary>
            public YGCString MORT_FRCL_FILENO { get; private set; }
            /// <Summary>
            /// This is the date the court dismisses the foreclosure for whatever reason.
            /// </Summary>
            public YGCDate MORT_FRCL_DISMIS_D { get; private set; }
            /// <Summary>
            /// This is the periodic or total payment on the mortgage.
            /// </Summary>
            public YGCDecimal MORT_PMT { get; private set; }
            /// <Summary>
            /// This is the mortgage interest rate. It takes the decimal form:
            /// <para>   Ex: .195 is the value of this field if the interest rate is 19.5%.</para>
            /// </Summary>
            public YGCDecimal MORT_RATE { get; private set; }
            /// <Summary>
            /// This is the number of the book in the local records that the property is filed in. The liber number can be put here.
            /// </Summary>
            public YGCString MORT_BOOK_1 { get; private set; }
            /// <Summary>
            /// This is the page number in the book that the property is filed in.
            /// </Summary>
            public YGCString MORT_PAGE_1 { get; private set; }
            /// <Summary>
            /// This can be used for the portfolio number of the property if MORT_BOOK_1 holds the liber number.
            /// </Summary>
            public YGCString MORT_BOOK_2 { get; private set; }
            /// <Summary>
            /// This is the page number corresponding to MORT_BOOK_2.
            /// </Summary>
            public YGCString MORT_PAGE_2 { get; private set; }
            /// <Summary>
            /// This is the date the mortgage is entered in the local record book.
            /// </Summary>
            public YGCDate MORT_RECRD_D { get; private set; }
            /// <Summary>
            /// This is the date the mortgage is due.
            /// </Summary>
            public YGCDate MORT_DUE_D { get; private set; }
            /// <Summary>
            /// This is the number given by the land records office in the county where the property is located.
            /// </Summary>
            public YGCString LIEN_FILE_NUM { get; private set; }
            /// <Summary>
            /// This is the case number assigned by the bank holding the lien.
            /// </Summary>
            public YGCString LIEN_CASE_NUM { get; private set; }
            /// <Summary>
            /// This is the date the lien is established.
            /// </Summary>
            public YGCDate LIEN_D { get; private set; }
            /// <Summary>
            /// This is the number of the book in the local records that the lien is filed in.
            /// </Summary>
            public YGCString LIEN_BOOK { get; private set; }
            /// <Summary>
            /// This is the page number in the book that the lien is filed in.
            /// </Summary>
            public YGCString LIEN_PAGE { get; private set; }
            /// <Summary>
            /// Was there a response to the lien? Values are Y/N.
            /// </Summary>
            public YGCString LIEN_AOL { get; private set; }
            /// <Summary>
            /// This is the date the lien release (the confirmation of the lien's payment) is filed.
            /// </Summary>
            public YGCDate LIEN_RLSE_D { get; private set; }
            /// <Summary>
            /// This is the number of the book in the local records the lien release is filed in.
            /// </Summary>
            public YGCString LIEN_RLSE_BOOK { get; private set; }
            /// <Summary>
            /// This is the page number in the book in the local records the lien release is filed in.
            /// </Summary>
            public YGCString LIEN_RLSE_PAGE { get; private set; }
            /// <Summary>
            /// This is the date when the lien is foreclosed upon.
            /// </Summary>
            public YGCDate LIEN_LITIG_D { get; private set; }
            /// <Summary>
            /// This is the number of the book in the local records that the lien is filed in when it is foreclosed upon.
            /// </Summary>
            public YGCString LIEN_LITIG_BOOK { get; private set; }
            /// <Summary>
            /// This is the page number in the book that the lien is filed in when it is foreclosed upon.
            /// </Summary>
            public YGCString LIEN_LITIG_PAGE { get; private set; }
            #endregion

            public RecordType46() : base(46)
            {
                this.DBTR_NUM = new YGCNumber(3);
                this.ASSET_ID = new YGCString(3);
                this.ASSET_OWNER = new YGCString(60);
                this.STREET = new YGCString(40);
                this.STREET_2 = new YGCString(40);
                this.STREET_3 = new YGCString(40);
                this.CITY = new YGCString(30);
                this.TOWN = new YGCString(30);
                this.CNTY = new YGCString(30);
                this.STATE = new YGCString(2);
                this.ZIP = new YGCString(20);
                this.CNTRY = new YGCString(3);
                this.PHONE = new YGCString(20);
                this.BLOCK = new YGCString(10);
                this.LOT = new YGCString(10);
                this.ASSET_VALUE = new YGCDecimal(9, 2);
                this.ASSET_DESC = new YGCString(40);
                this.ASSET_VIN = new YGCString(20);
                this.ASSET_LIC_PLATE = new YGCString(10);
                this.ASSET_COLOR = new YGCString(15);
                this.ASSET_YEAR = new YGCString(4);
                this.ASSET_MAKE = new YGCString(20);
                this.ASSET_MODEL = new YGCString(20);
                this.REPO_FILE_NUM = new YGCString(15);
                this.REPO_D = new YGCDate();
                this.REPO_AMT = new YGCDecimal(9, 2);
                this.CERT_TITLE_NAME = new YGCString(40);
                this.CERT_TITLE_D = new YGCDate();
                this.MORT_FRCL_D = new YGCDate();
                this.MORT_FRCL_FILENO = new YGCString(15);
                this.MORT_FRCL_DISMIS_D = new YGCDate();
                this.MORT_PMT = new YGCDecimal(7, 2);
                this.MORT_RATE = new YGCDecimal(4, 3);
                this.MORT_BOOK_1 = new YGCString(5);
                this.MORT_PAGE_1 = new YGCString(5);
                this.MORT_BOOK_2 = new YGCString(5);
                this.MORT_PAGE_2 = new YGCString(5);
                this.MORT_RECRD_D = new YGCDate();
                this.MORT_DUE_D = new YGCDate();
                this.LIEN_FILE_NUM = new YGCString(15);
                this.LIEN_CASE_NUM = new YGCString(15);
                this.LIEN_D = new YGCDate();
                this.LIEN_BOOK = new YGCString(5);
                this.LIEN_PAGE = new YGCString(5);
                this.LIEN_AOL = new YGCString(1);
                this.LIEN_RLSE_D = new YGCDate();
                this.LIEN_RLSE_BOOK = new YGCString(5);
                this.LIEN_RLSE_PAGE = new YGCString(5);
                this.LIEN_LITIG_D = new YGCDate();
                this.LIEN_LITIG_BOOK = new YGCString(5);
                this.LIEN_LITIG_PAGE = new YGCString(5);
            }
            public RecordType46(string RT46Entry) : base(RT46Entry)
            {
                this.DBTR_NUM = new YGCNumber(3) { DataString = RT46Entry.Length > 67 ? RT46Entry.Substring(67) : "" };
                this.ASSET_ID = new YGCString(3) { DataString = RT46Entry.Length > 70 ? RT46Entry.Substring(70) : "" };
                this.ASSET_OWNER = new YGCString(60) { DataString = RT46Entry.Length > 73 ? RT46Entry.Substring(73) : "" };
                this.STREET = new YGCString(40) { DataString = RT46Entry.Length > 133 ? RT46Entry.Substring(133) : "" };
                this.STREET_2 = new YGCString(40) { DataString = RT46Entry.Length > 173 ? RT46Entry.Substring(173) : "" };
                this.STREET_3 = new YGCString(40) { DataString = RT46Entry.Length > 213 ? RT46Entry.Substring(213) : "" };
                this.CITY = new YGCString(30) { DataString = RT46Entry.Length > 253 ? RT46Entry.Substring(253) : "" };
                this.TOWN = new YGCString(30) { DataString = RT46Entry.Length > 283 ? RT46Entry.Substring(283) : "" };
                this.CNTY = new YGCString(30) { DataString = RT46Entry.Length > 313 ? RT46Entry.Substring(313) : "" };
                this.STATE = new YGCString(2) { DataString = RT46Entry.Length > 343 ? RT46Entry.Substring(343) : "" };
                this.ZIP = new YGCString(20) { DataString = RT46Entry.Length > 345 ? RT46Entry.Substring(345) : "" };
                this.CNTRY = new YGCString(3) { DataString = RT46Entry.Length > 365 ? RT46Entry.Substring(365) : "" };
                this.PHONE = new YGCString(20) { DataString = RT46Entry.Length > 368 ? RT46Entry.Substring(368) : "" };
                this.BLOCK = new YGCString(10) { DataString = RT46Entry.Length > 388 ? RT46Entry.Substring(388) : "" };
                this.LOT = new YGCString(10) { DataString = RT46Entry.Length > 398 ? RT46Entry.Substring(398) : "" };
                this.ASSET_VALUE = new YGCDecimal(9, 2) { DataString = RT46Entry.Length > 408 ? RT46Entry.Substring(408) : "" };
                this.ASSET_DESC = new YGCString(40) { DataString = RT46Entry.Length > 417 ? RT46Entry.Substring(417) : "" };
                this.ASSET_VIN = new YGCString(20) { DataString = RT46Entry.Length > 457 ? RT46Entry.Substring(457) : "" };
                this.ASSET_LIC_PLATE = new YGCString(10) { DataString = RT46Entry.Length > 477 ? RT46Entry.Substring(477) : "" };
                this.ASSET_COLOR = new YGCString(15) { DataString = RT46Entry.Length > 487 ? RT46Entry.Substring(487) : "" };
                this.ASSET_YEAR = new YGCString(4) { DataString = RT46Entry.Length > 502 ? RT46Entry.Substring(502) : "" };
                this.ASSET_MAKE = new YGCString(20) { DataString = RT46Entry.Length > 506 ? RT46Entry.Substring(506) : "" };
                this.ASSET_MODEL = new YGCString(20) { DataString = RT46Entry.Length > 526 ? RT46Entry.Substring(526) : "" };
                this.REPO_FILE_NUM = new YGCString(15) { DataString = RT46Entry.Length > 546 ? RT46Entry.Substring(546) : "" };
                this.REPO_D = new YGCDate() { DataString = RT46Entry.Length > 561 ? RT46Entry.Substring(561) : "" };
                this.REPO_AMT = new YGCDecimal(9, 2) { DataString = RT46Entry.Length > 569 ? RT46Entry.Substring(569) : "" };
                this.CERT_TITLE_NAME = new YGCString(40) { DataString = RT46Entry.Length > 578 ? RT46Entry.Substring(578) : "" };
                this.CERT_TITLE_D = new YGCDate() { DataString = RT46Entry.Length > 618 ? RT46Entry.Substring(618) : "" };
                this.MORT_FRCL_D = new YGCDate() { DataString = RT46Entry.Length > 626 ? RT46Entry.Substring(626) : "" };
                this.MORT_FRCL_FILENO = new YGCString(15) { DataString = RT46Entry.Length > 634 ? RT46Entry.Substring(634) : "" };
                this.MORT_FRCL_DISMIS_D = new YGCDate() { DataString = RT46Entry.Length > 649 ? RT46Entry.Substring(649) : "" };
                this.MORT_PMT = new YGCDecimal(7, 2) { DataString = RT46Entry.Length > 657 ? RT46Entry.Substring(657) : "" };
                this.MORT_RATE = new YGCDecimal(4, 3) { DataString = RT46Entry.Length > 664 ? RT46Entry.Substring(664) : "" };
                this.MORT_BOOK_1 = new YGCString(5) { DataString = RT46Entry.Length > 668 ? RT46Entry.Substring(668) : "" };
                this.MORT_PAGE_1 = new YGCString(5) { DataString = RT46Entry.Length > 673 ? RT46Entry.Substring(673) : "" };
                this.MORT_BOOK_2 = new YGCString(5) { DataString = RT46Entry.Length > 678 ? RT46Entry.Substring(678) : "" };
                this.MORT_PAGE_2 = new YGCString(5) { DataString = RT46Entry.Length > 683 ? RT46Entry.Substring(683) : "" };
                this.MORT_RECRD_D = new YGCDate() { DataString = RT46Entry.Length > 688 ? RT46Entry.Substring(688) : "" };
                this.MORT_DUE_D = new YGCDate() { DataString = RT46Entry.Length > 696 ? RT46Entry.Substring(696) : "" };
                this.LIEN_FILE_NUM = new YGCString(15) { DataString = RT46Entry.Length > 704 ? RT46Entry.Substring(704) : "" };
                this.LIEN_CASE_NUM = new YGCString(15) { DataString = RT46Entry.Length > 719 ? RT46Entry.Substring(719) : "" };
                this.LIEN_D = new YGCDate() { DataString = RT46Entry.Length > 734 ? RT46Entry.Substring(734) : "" };
                this.LIEN_BOOK = new YGCString(5) { DataString = RT46Entry.Length > 742 ? RT46Entry.Substring(742) : "" };
                this.LIEN_PAGE = new YGCString(5) { DataString = RT46Entry.Length > 747 ? RT46Entry.Substring(747) : "" };
                this.LIEN_AOL = new YGCString(1) { DataString = RT46Entry.Length > 752 ? RT46Entry.Substring(752) : "" };
                this.LIEN_RLSE_D = new YGCDate() { DataString = RT46Entry.Length > 753 ? RT46Entry.Substring(753) : "" };
                this.LIEN_RLSE_BOOK = new YGCString(5) { DataString = RT46Entry.Length > 761 ? RT46Entry.Substring(761) : "" };
                this.LIEN_RLSE_PAGE = new YGCString(5) { DataString = RT46Entry.Length > 766 ? RT46Entry.Substring(766) : "" };
                this.LIEN_LITIG_D = new YGCDate() { DataString = RT46Entry.Length > 771 ? RT46Entry.Substring(771) : "" };
                this.LIEN_LITIG_BOOK = new YGCString(5) { DataString = RT46Entry.Length > 779 ? RT46Entry.Substring(779) : "" };
                this.LIEN_LITIG_PAGE = new YGCString(5) { DataString = RT46Entry.Length > 784 ? RT46Entry.Substring(784) : "" };

            }

            public override Type GetType() { return typeof(RecordType46); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}",
                    base.ToString(),
                    this.DBTR_NUM,
                    this.ASSET_ID,
                    this.ASSET_OWNER,
                    this.STREET,
                    this.STREET_2,
                    this.STREET_3,
                    this.CITY,
                    this.TOWN,
                    this.CNTY,
                    this.STATE,
                    this.ZIP,
                    this.CNTRY,
                    this.PHONE,
                    this.BLOCK,
                    this.LOT,
                    this.ASSET_VALUE,
                    this.ASSET_DESC,
                    this.ASSET_VIN,
                    this.ASSET_LIC_PLATE,
                    this.ASSET_COLOR,
                    this.ASSET_YEAR,
                    this.ASSET_MAKE,
                    this.ASSET_MODEL,
                    this.REPO_FILE_NUM,
                    this.REPO_D,
                    this.REPO_AMT,
                    this.CERT_TITLE_NAME,
                    this.CERT_TITLE_D,
                    this.MORT_FRCL_D,
                    this.MORT_FRCL_FILENO,
                    this.MORT_FRCL_DISMIS_D,
                    this.MORT_PMT,
                    this.MORT_RATE,
                    this.MORT_BOOK_1,
                    this.MORT_PAGE_1,
                    this.MORT_BOOK_2,
                    this.MORT_PAGE_2,
                    this.MORT_RECRD_D,
                    this.MORT_DUE_D,
                    this.LIEN_FILE_NUM,
                    this.LIEN_CASE_NUM,
                    this.LIEN_D,
                    this.LIEN_BOOK,
                    this.LIEN_PAGE,
                    this.LIEN_AOL,
                    this.LIEN_RLSE_D,
                    this.LIEN_RLSE_BOOK,
                    this.LIEN_RLSE_PAGE,
                    this.LIEN_LITIG_D,
                    this.LIEN_LITIG_BOOK,
                    this.LIEN_LITIG_PAGE
                    );
            }
        }
        #endregion

        #region Record Type 51
        /// <summary>
        /// Record Type 51 - Export - New Account Information						
        /// <para>This record is required to create a new account. It identifies the creditor and current owner of the debt and establishes the current financial state of the debt.This record type is identical to a record 01 except it is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC.YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 51. </para>
        /// </summary>
        public class RecordType51 : RecordType01
        {
            public RecordType51() : base(51) { }
            public RecordType51(string RT51Entry) : base(RT51Entry) { }

            public override Type GetType() { return typeof(RecordType51); }
        }
        #endregion

        #region Record Type 52
        /// <summary>
        /// Record Type 52 - Export - Primary Debtor Information
        /// <para>This record identifies the primary debtor and significant legal events.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC.YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Record type value = 52.</para>
        /// <para>YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 02, 31 or 52, in the address section of the details page.Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
        /// </summary>
        public class RecordType52 : RecordType02
        {
            public RecordType52() : base(52) { }
            public RecordType52(string RT52Entry) : base(RT52Entry) { }

            public override Type GetType() { return typeof(RecordType52); }
        }
        #endregion

        #region Record Type 53
        /// <summary>
        /// Record Type 53 - Export - 2nd and 3rd Debtor Information
        /// <para>If more than one name appears on the original contract between creditor and debtor, they are identified here.This can also be used for a co-signer.Record type value = 53. </para>
        /// <para>YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 03, 33 or 53, in the address section of the details page.Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
        /// </summary>
        public class RecordType53 : RecordType03
        {
            public RecordType53() : base(53) { }
            public RecordType53(string RT53Entry) : base(RT53Entry) { }

            public override Type GetType() { return typeof(RecordType53); }
        }
        #endregion

        #region Record Type 54
        /// <summary>
        /// Record Type 54 - Export - Employment Information
        /// <para>This record holds the debtors' employment information. You can submit a record for each of 3 distinct debtors for the same account. This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system.Value = 54.</para>
        /// </summary>
        public class RecordType54 : RecordType04
        {
            public RecordType54() : base(54) { }
            public RecordType54(string RT54Entry) : base(RT54Entry) { }

            public override Type GetType() { return typeof(RecordType54); }
        }
        #endregion

        #region Record Type 55
        /// <summary>
        /// Record Type 55 - Export - Bank/Asset Information
        /// <para>This record holds any bank account information and non-auto or non-real estate asset information for the debtor. You can submit a record for each of 3 distinct bank accounts for the same debtor.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 55.</para>
        /// </summary>
        public class RecordType55 : RecordType05
        {
            public RecordType55() : base(55) { }
            public RecordType55(string RT55Entry) : base(RT55Entry) { }

            public override Type GetType() { return typeof(RecordType55); }
        }
        #endregion

        #region Record Type 56
        /// <summary>
        /// Record Type 56 - Export - Misc Information
        /// <para>This record is for debtor attorney information and any miscellaneous information that no other record in the DataStandard accommodates. You can submit a record for each of 3 debtor attorneys for the same account.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC.YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 56.</para>
        /// </summary>
        public class RecordType56 : RecordType06
        {
            public RecordType56() : base(56) { }
            public RecordType56(string RT56Entry) : base(RT56Entry) { }

            public override Type GetType() { return typeof(RecordType56); }
        }
        #endregion

        #region Record Type 57
        /// <summary>
        /// Record Type 57 - Export - Legal Information
        /// <para>This record reports information regarding a suit filed against the debtor. More details can be provided in return by the receiver (agency/firm) in record 41. This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system.Value = 57.</para>
        /// </summary>
        public class RecordType57 : RecordType07
        {
            public RecordType57() : base(57) { }
            public RecordType57(string RT57Entry) : base(RT57Entry) { }

            public override Type GetType() { return typeof(RecordType57); }
        }
        #endregion

        #region Record Type 58
        /// <summary>
        /// Record Type 58 - Export - Caption - Legal Names
        /// <para>This record is for entering the caption; i.e., the parties named in the suit. The Plaintiffs are typically the original creditors and the Defendants are the debtors.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 58.</para>
        /// </summary>
        public class RecordType58 : RecordType08
        {
            public RecordType58() : base(58) { }
            public RecordType58(string RT58Entry) : base(RT58Entry) { }

            public override Type GetType() { return typeof(RecordType58); }
        }
        #endregion

        #region Record Type 59
        /// <summary>
        /// Record Type 59 - Export - Message
        /// <para>Senders communicate status updates to their receivers with this record type. It should include the PCODE to clearly identify the update. Examples of status updates are direct payment, suit filed, account refused, judgment issued and account closed.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 59.</para>
        /// </summary>
        public class RecordType59 : RecordType09
        {
            public RecordType59() : base(59) { }
            public RecordType59(string RT59Entry) : base(RT59Entry) { }

            public override Type GetType() { return typeof(RecordType59); }
        }
        #endregion

        #region Record Type 70
        /// <summary>
        /// Record Type 70 - 
        /// </summary>
        public class RecordType70 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Forwarder Number
            /// </summary>
            public YGCNumber FORW_NO { get; private set; }
            /// <summary>
            /// Co-Counsel Number
            /// </summary>
            public YGCNumber MASCO_NO { get; private set; }
            /// <summary>
            /// Adversary Attorney Number
            /// </summary>
            public YGCNumber ADVA_NO { get; private set; }
            /// <summary>
            /// Our Secretary/Paralegal Number
            /// </summary>
            public YGCNumber SECY_NO { get; private set; }
            /// <summary>
            /// Our Attorney Number
            /// </summary>
            public YGCNumber ATTY_NO { get; private set; }
            /// <summary>
            /// Our Collector Number
            /// </summary>
            public YGCNumber COLL_NO { get; private set; }
            /// <summary>
            /// Venue 1 Number
            /// </summary>
            public YGCNumber VENUE1_NO { get; private set; }
            /// <summary>
            /// Venue 2 Number
            /// </summary>
            public YGCNumber VENUE2_NO { get; private set; }
            /// <summary>
            /// Sheriff 1 Number
            /// </summary>
            public YGCNumber SHERIFF1_NO { get; private set; }
            /// <summary>
            /// Sheriff 2 Number
            /// </summary>
            public YGCNumber SHERIFF2_NO { get; private set; }
            /// <summary>
            /// Bank 1 Number
            /// </summary>
            public YGCNumber BANK1_NO { get; private set; }
            /// <summary>
            /// Bank 2 Number
            /// </summary>
            public YGCNumber BANK2_NO { get; private set; }
            /// <summary>
            /// Bank 3 Number
            /// </summary>
            public YGCNumber BANK3_NO { get; private set; }
            /// <summary>
            /// Employer 1 Number
            /// </summary>
            public YGCNumber EMPL1_NO { get; private set; }
            /// <summary>
            /// Employer 2 Number
            /// </summary>
            public YGCNumber EMPL2_NO { get; private set; }
            /// <summary>
            /// Employer 3 Number
            /// </summary>
            public YGCNumber EMPL3_NO { get; private set; }
            /// <summary>
            /// Status Code 1 (Date)
            /// </summary>
            public YGCDate STATUS1_D { get; private set; }
            /// <summary>
            /// Status Code 1 (Code)
            /// </summary>
            public YGCNumber STATUS1_CODE { get; private set; }
            /// <summary>
            /// Status Code 2 (Date)
            /// </summary>
            public YGCDate STATUS2_D { get; private set; }
            /// <summary>
            /// Status Code 2 (Code)
            /// </summary>
            public YGCNumber STATUS2_CODE { get; private set; }
            /// <summary>
            /// Internal Forwarder Reference Number
            /// </summary>
            public YGCString FREF { get; private set; }
            /// <summary>
            /// Co-Counsel Commission Rate
            /// </summary>
            public YGCDecimal CCOMM { get; private set; }
            /// <summary>
            /// Co-Counsel Suit Fee Rate
            /// </summary>
            public YGCDecimal CSFEE { get; private set; }
            /// <summary>
            /// Number of Debtors
            /// </summary>
            public YGCNumber DNO { get; private set; }
            /// <summary>
            /// Adversary Attorney 2 Number
            /// </summary>
            public YGCNumber ADVA_NO2 { get; private set; }
            /// <summary>
            /// Adversary Attorney 3 Number
            /// </summary>
            public YGCNumber ADVA_NO3 { get; private set; }
            /// <summary>
            /// Sales Person
            /// </summary>
            public YGCNumber SALES { get; private set; }
            #endregion

            public RecordType70() : base(70)
            {
                this.FORW_NO = new YGCNumber(5);
                this.MASCO_NO = new YGCNumber(5);
                this.ADVA_NO = new YGCNumber(5);
                this.SECY_NO = new YGCNumber(5);
                this.ATTY_NO = new YGCNumber(5);
                this.COLL_NO = new YGCNumber(5);
                this.VENUE1_NO = new YGCNumber(5);
                this.VENUE2_NO = new YGCNumber(5);
                this.SHERIFF1_NO = new YGCNumber(5);
                this.SHERIFF2_NO = new YGCNumber(5);
                this.BANK1_NO = new YGCNumber(5);
                this.BANK2_NO = new YGCNumber(5);
                this.BANK3_NO = new YGCNumber(5);
                this.EMPL1_NO = new YGCNumber(5);
                this.EMPL2_NO = new YGCNumber(5);
                this.EMPL3_NO = new YGCNumber(5);
                this.STATUS1_D = new YGCDate();
                this.STATUS1_CODE = new YGCNumber(3);
                this.STATUS2_D = new YGCDate();
                this.STATUS2_CODE = new YGCNumber(3);
                this.FREF = new YGCString(10);
                this.CCOMM = new YGCDecimal(4, 3);
                this.CSFEE = new YGCDecimal(4, 3);
                this.DNO = new YGCNumber(1);
                this.ADVA_NO2 = new YGCNumber(5);
                this.ADVA_NO3 = new YGCNumber(5);
                this.SALES = new YGCNumber(5);
            }
            public RecordType70(string RT70Entry) : base(RT70Entry)
            {
                try
                {
                    this.FORW_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 67 ? RT70Entry.Substring(67) : "" };
                    this.MASCO_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 72 ? RT70Entry.Substring(72) : "" };
                    this.ADVA_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 77 ? RT70Entry.Substring(77) : "" };
                    this.SECY_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 82 ? RT70Entry.Substring(82) : "" };
                    this.ATTY_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 87 ? RT70Entry.Substring(87) : "" };
                    this.COLL_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 92 ? RT70Entry.Substring(92) : "" };
                    this.VENUE1_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 97 ? RT70Entry.Substring(97) : "" };
                    this.VENUE2_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 102 ? RT70Entry.Substring(102) : "" };
                    this.SHERIFF1_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 107 ? RT70Entry.Substring(107) : "" };
                    this.SHERIFF2_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 112 ? RT70Entry.Substring(112) : "" };
                    this.BANK1_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 117 ? RT70Entry.Substring(117) : "" };
                    this.BANK2_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 122 ? RT70Entry.Substring(122) : "" };
                    this.BANK3_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 127 ? RT70Entry.Substring(127) : "" };
                    this.EMPL1_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 132 ? RT70Entry.Substring(132) : "" };
                    this.EMPL2_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 137 ? RT70Entry.Substring(137) : "" };
                    this.EMPL3_NO = new YGCNumber(5) { DataString = RT70Entry.Length > 142 ? RT70Entry.Substring(142) : "" };
                    this.STATUS1_D = new YGCDate() { DataString = RT70Entry.Length > 147 ? RT70Entry.Substring(147) : "" };
                    this.STATUS1_CODE = new YGCNumber(3) { DataString = RT70Entry.Length > 155 ? RT70Entry.Substring(155) : "" };
                    this.STATUS2_D = new YGCDate() { DataString = RT70Entry.Length > 158 ? RT70Entry.Substring(158) : "" };
                    this.STATUS2_CODE = new YGCNumber(3) { DataString = RT70Entry.Length > 166 ? RT70Entry.Substring(166) : "" };
                    this.FREF = new YGCString(10) { DataString = RT70Entry.Length > 169 ? RT70Entry.Substring(169) : "" };
                    this.CCOMM = new YGCDecimal(4, 3) { DataString = RT70Entry.Length > 179 ? RT70Entry.Substring(179) : "" };
                    this.CSFEE = new YGCDecimal(4, 3) { DataString = RT70Entry.Length > 183 ? RT70Entry.Substring(183) : "" };
                    this.DNO = new YGCNumber(1) { DataString = RT70Entry.Length > 187 ? RT70Entry.Substring(187) : "" };
                    this.ADVA_NO2 = new YGCNumber(5) { DataString = RT70Entry.Length > 188 ? RT70Entry.Substring(188) : "" };
                    this.ADVA_NO3 = new YGCNumber(5) { DataString = RT70Entry.Length > 193 ? RT70Entry.Substring(193) : "" };
                    this.SALES = new YGCNumber(5) { DataString = RT70Entry.Length > 198 ? RT70Entry.Substring(198) : "" };
                }
                catch
                {
                    if (this.FORW_NO == null) this.FORW_NO = new YGCNumber(5);
                    if (this.MASCO_NO == null) this.MASCO_NO = new YGCNumber(5);
                    if (this.ADVA_NO == null) this.ADVA_NO = new YGCNumber(5);
                    if (this.SECY_NO == null) this.SECY_NO = new YGCNumber(5);
                    if (this.ATTY_NO == null) this.ATTY_NO = new YGCNumber(5);
                    if (this.COLL_NO == null) this.COLL_NO = new YGCNumber(5);
                    if (this.VENUE1_NO == null) this.VENUE1_NO = new YGCNumber(5);
                    if (this.VENUE2_NO == null) this.VENUE2_NO = new YGCNumber(5);
                    if (this.SHERIFF1_NO == null) this.SHERIFF1_NO = new YGCNumber(5);
                    if (this.SHERIFF2_NO == null) this.SHERIFF2_NO = new YGCNumber(5);
                    if (this.BANK1_NO == null) this.BANK1_NO = new YGCNumber(5);
                    if (this.BANK2_NO == null) this.BANK2_NO = new YGCNumber(5);
                    if (this.BANK3_NO == null) this.BANK3_NO = new YGCNumber(5);
                    if (this.EMPL1_NO == null) this.EMPL1_NO = new YGCNumber(5);
                    if (this.EMPL2_NO == null) this.EMPL2_NO = new YGCNumber(5);
                    if (this.EMPL3_NO == null) this.EMPL3_NO = new YGCNumber(5);
                    if (this.STATUS1_D == null) this.STATUS1_D = new YGCDate();
                    if (this.STATUS1_CODE == null) this.STATUS1_CODE = new YGCNumber(3);
                    if (this.STATUS2_D == null) this.STATUS2_D = new YGCDate();
                    if (this.STATUS2_CODE == null) this.STATUS2_CODE = new YGCNumber(3);
                    if (this.FREF == null) this.FREF = new YGCString(10);
                    if (this.CCOMM == null) this.CCOMM = new YGCDecimal(4, 3);
                    if (this.CSFEE == null) this.CSFEE = new YGCDecimal(4, 3);
                    if (this.DNO == null) this.DNO = new YGCNumber(1);
                    if (this.ADVA_NO2 == null) this.ADVA_NO2 = new YGCNumber(5);
                    if (this.ADVA_NO3 == null) this.ADVA_NO3 = new YGCNumber(5);
                    if (this.SALES == null) this.SALES = new YGCNumber(5);
                }
            }

            public override Type GetType() { return typeof(RecordType70); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}",
                    base.ToString(),
                    this.FORW_NO,
                    this.MASCO_NO,
                    this.ADVA_NO,
                    this.SECY_NO,
                    this.ATTY_NO,
                    this.COLL_NO,
                    this.VENUE1_NO,
                    this.VENUE2_NO,
                    this.SHERIFF1_NO,
                    this.SHERIFF2_NO,
                    this.BANK1_NO,
                    this.BANK2_NO,
                    this.BANK3_NO,
                    this.EMPL1_NO,
                    this.EMPL2_NO,
                    this.EMPL3_NO,
                    this.STATUS1_D,
                    this.STATUS1_CODE,
                    this.STATUS2_D,
                    this.STATUS2_CODE,
                    this.FREF,
                    this.CCOMM,
                    this.CSFEE,
                    this.DNO,
                    this.ADVA_NO2,
                    this.ADVA_NO3,
                    this.SALES);
            }
        }
        #endregion

        #region Record Type 71
        /// <summary>
        /// Record Type 71 - Account Card Values
        /// </summary>
        public class RecordType71 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Original Claim Amount
            /// </summary>
            public YGCDecimal ORIG_CLAIM { get; private set; }
            /// <summary>
            /// Suit Amount
            /// </summary>
            public YGCDecimal SUIT_AMT { get; private set; }
            /// <summary>
            /// Statutory Attorney Fees Accrued
            /// </summary>
            public YGCDecimal STAT_FEE { get; private set; }
            /// <summary>
            /// Judgment Amount
            /// </summary>
            public YGCDecimal JMT_AMT { get; private set; }
            /// <summary>
            /// Debtor Balance
            /// </summary>
            public YGCDecimal DEBT_BAL { get; private set; }
            /// <summary>
            /// Amount of Interest Accrued
            /// </summary>
            public YGCDecimal INT_DUE { get; private set; }
            /// <summary>
            /// Date interest was calculated
            /// </summary>
            public YGCDate INT_DATE { get; private set; }
            /// <summary>
            /// Claim Adjusted Amount
            /// </summary>
            public YGCDecimal C_ADJUST { get; private set; }
            /// <summary>
            /// Contract Interest Fee
            /// </summary>
            public YGCDecimal CNTRACT_INT { get; private set; }
            /// <summary>
            /// Affidavit Amount
            /// </summary>
            public YGCDecimal AFFIDAVIT { get; private set; }
            /// <summary>
            /// Pre Judgment Interest
            /// </summary>
            public YGCDecimal ACCR_INT_BEF { get; private set; }
            /// <summary>
            /// Compromise Amount (Settlement)
            /// </summary>
            public YGCDecimal COMPR_AMT { get; private set; }
            /// <summary>
            /// Overpaid Amount
            /// </summary>
            public YGCDecimal OVERPAY_AMT { get; private set; }
            /// <summary>
            /// Collect & Hold
            /// </summary>
            public YGCDecimal COLL_HOLD { get; private set; }
            /// <summary>
            /// Principal Collected
            /// </summary>
            public YGCDecimal PRIN_COLL { get; private set; }
            /// <summary>
            /// Interest Collected
            /// </summary>
            public YGCDecimal INT_COLL { get; private set; }
            /// <summary>
            /// Commissions Received
            /// </summary>
            public YGCDecimal COMM_EARN { get; private set; }
            /// <summary>
            /// Suit Fees Received
            /// </summary>
            public YGCDecimal SFEE_EARN { get; private set; }
            /// <summary>
            /// Misc/Stat Fees Received
            /// </summary>
            public YGCDecimal STAT_EARN { get; private set; }
            /// <summary>
            /// Sales Tax Collected
            /// </summary>
            public YGCDecimal SALES_TAX { get; private set; }
            /// <summary>
            /// Merchandise Returned (Pre-Suit)
            /// </summary>
            public YGCDecimal MERCH_BEF { get; private set; }
            /// <summary>
            /// Merchandise Returned (Post-Suit)
            /// </summary>
            public YGCDecimal MERCH_POST { get; private set; }
            /// <summary>
            /// Merchandise Returned (Post-Judg)
            /// </summary>
            public YGCDecimal MERCH_POST_JUDG { get; private set; }
            /// <summary>
            /// Debtor Payments (Pre-Suit)
            /// </summary>
            public YGCDecimal CASH_BEF { get; private set; }
            /// <summary>
            /// Debtor Payments (Post-Suit)
            /// </summary>
            public YGCDecimal CASH_POST { get; private set; }
            /// <summary>
            /// Debtor Payments (Post-Judg)
            /// </summary>
            public YGCDecimal PAID_POST_JUDG { get; private set; }
            /// <summary>
            /// Direct Pay - No Fee due (Pre Suit)
            /// </summary>
            public YGCDecimal DP_PRE_SUIT_NF { get; private set; }
            /// <summary>
            /// Direct Pay - No Fee due (Post Suit)
            /// </summary>
            public YGCDecimal DP_POST_SUIT_NF { get; private set; }
            /// <summary>
            /// Direct Pay - No Fee due (Post Judg)
            /// </summary>
            public YGCDecimal DP_POST_JUDG_NF { get; private set; }
            /// <summary>
            /// Direct Pay - (All DP) (Pre Suit)
            /// </summary>
            public YGCDecimal DP_PRE_SUIT { get; private set; }
            /// <summary>
            /// Direct Pay - (All DP) (Post Suit)
            /// </summary>
            public YGCDecimal DP_POST_SUIT { get; private set; }
            /// <summary>
            /// Direct Pay - (All DP) (Post Judg)
            /// </summary>
            public YGCDecimal DP_POST_JUDG { get; private set; }
            /// <summary>
            /// Collected by Co-Co (Pre Suit)
            /// </summary>
            public YGCDecimal AGENT_BEFSCOLL { get; private set; }
            /// <summary>
            /// Collected by Co-Co (Post Suit)
            /// </summary>
            public YGCDecimal AGENT_PSCOLL { get; private set; }
            /// <summary>
            /// Collected by Co-Co (Post Judg)
            /// </summary>
            public YGCDecimal AGENT_PJCOLL { get; private set; }
            /// <summary>
            /// Costs Received (From Client)
            /// </summary>
            public YGCDecimal COST_RECEIVED { get; private set; }
            /// <summary>
            /// Costs Returned (To Client)
            /// </summary>
            public YGCDecimal COST_RET { get; private set; }
            /// <summary>
            /// Costs Balance (Still on Hand)
            /// </summary>
            public YGCDecimal COST_BAL { get; private set; }
            /// <summary>
            /// Non Recoverable Costs Expended
            /// </summary>
            public YGCDecimal NON_RCVRD_COST { get; private set; }
            /// <summary>
            /// Costs Expended Post Judgment
            /// </summary>
            public YGCDecimal COST_POST_JUDG { get; private set; }
            /// <summary>
            /// Costs Expended to Co-Co
            /// </summary>
            public YGCDecimal AGENT_COST { get; private set; }
            /// <summary>
            /// Costs Recovered Via Tax Rebate
            /// </summary>
            public YGCDecimal TAX_REBATE { get; private set; }
            /// <summary>
            /// Total Costs Expended
            /// </summary>
            public YGCDecimal TOT_EXP_COST { get; private set; }
            /// <summary>
            /// Recoverable Costs Expended
            /// </summary>
            public YGCDecimal COST_EXP { get; private set; }
            /// <summary>
            /// Costs Recovered
            /// </summary>
            public YGCDecimal COST_RECOVERED { get; private set; }
            /// <summary>
            /// Firm Costs Expended
            /// </summary>
            public YGCDecimal EXP_COST { get; private set; }
            #endregion

            public RecordType71() : base(71)
            {
                this.ORIG_CLAIM = new YGCDecimal(14, 2);
                this.SUIT_AMT = new YGCDecimal(14, 2);
                this.STAT_FEE = new YGCDecimal(14, 2);
                this.JMT_AMT = new YGCDecimal(14, 2);
                this.DEBT_BAL = new YGCDecimal(14, 2);
                this.INT_DUE = new YGCDecimal(14, 2);
                this.INT_DATE = new YGCDate();
                this.C_ADJUST = new YGCDecimal(14, 2);
                this.CNTRACT_INT = new YGCDecimal(14, 2);
                this.AFFIDAVIT = new YGCDecimal(14, 2);
                this.ACCR_INT_BEF = new YGCDecimal(14, 2);
                this.COMPR_AMT = new YGCDecimal(14, 2);
                this.OVERPAY_AMT = new YGCDecimal(14, 2);
                this.COLL_HOLD = new YGCDecimal(14, 2);
                this.PRIN_COLL = new YGCDecimal(14, 2);
                this.INT_COLL = new YGCDecimal(14, 2);
                this.COMM_EARN = new YGCDecimal(14, 2);
                this.SFEE_EARN = new YGCDecimal(14, 2);
                this.STAT_EARN = new YGCDecimal(14, 2);
                this.SALES_TAX = new YGCDecimal(14, 2);
                this.MERCH_BEF = new YGCDecimal(14, 2);
                this.MERCH_POST = new YGCDecimal(14, 2);
                this.MERCH_POST_JUDG = new YGCDecimal(14, 2);
                this.CASH_BEF = new YGCDecimal(14, 2);
                this.CASH_POST = new YGCDecimal(14, 2);
                this.PAID_POST_JUDG = new YGCDecimal(14, 2);
                this.DP_PRE_SUIT_NF = new YGCDecimal(14, 2);
                this.DP_POST_SUIT_NF = new YGCDecimal(14, 2);
                this.DP_POST_JUDG_NF = new YGCDecimal(14, 2);
                this.DP_PRE_SUIT = new YGCDecimal(14, 2);
                this.DP_POST_SUIT = new YGCDecimal(14, 2);
                this.DP_POST_JUDG = new YGCDecimal(14, 2);
                this.AGENT_BEFSCOLL = new YGCDecimal(14, 2);
                this.AGENT_PSCOLL = new YGCDecimal(14, 2);
                this.AGENT_PJCOLL = new YGCDecimal(14, 2);
                this.COST_RECEIVED = new YGCDecimal(14, 2);
                this.COST_RET = new YGCDecimal(14, 2);
                this.COST_BAL = new YGCDecimal(14, 2);
                this.NON_RCVRD_COST = new YGCDecimal(14, 2);
                this.COST_POST_JUDG = new YGCDecimal(14, 2);
                this.AGENT_COST = new YGCDecimal(14, 2);
                this.TAX_REBATE = new YGCDecimal(14, 2);
                this.TOT_EXP_COST = new YGCDecimal(14, 2);
                this.COST_EXP = new YGCDecimal(14, 2);
                this.COST_RECOVERED = new YGCDecimal(14, 2);
                this.EXP_COST = new YGCDecimal(14, 2);
            }
            public RecordType71(string RT71Entry) : base(RT71Entry)
            {
                this.ORIG_CLAIM = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 67 ? RT71Entry.Substring(67) : "" };
                this.SUIT_AMT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 81 ? RT71Entry.Substring(81) : "" };
                this.STAT_FEE = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 95 ? RT71Entry.Substring(95) : "" };
                this.JMT_AMT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 109 ? RT71Entry.Substring(109) : "" };
                this.DEBT_BAL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 123 ? RT71Entry.Substring(123) : "" };
                this.INT_DUE = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 137 ? RT71Entry.Substring(137) : "" };
                this.INT_DATE = new YGCDate() { DataString = RT71Entry.Length > 151 ? RT71Entry.Substring(151) : "" };
                this.C_ADJUST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 159 ? RT71Entry.Substring(159) : "" };
                this.CNTRACT_INT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 173 ? RT71Entry.Substring(173) : "" };
                this.AFFIDAVIT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 187 ? RT71Entry.Substring(187) : "" };
                this.ACCR_INT_BEF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 201 ? RT71Entry.Substring(201) : "" };
                this.COMPR_AMT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 215 ? RT71Entry.Substring(215) : "" };
                this.OVERPAY_AMT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 229 ? RT71Entry.Substring(229) : "" };
                this.COLL_HOLD = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 243 ? RT71Entry.Substring(243) : "" };
                this.PRIN_COLL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 257 ? RT71Entry.Substring(257) : "" };
                this.INT_COLL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 271 ? RT71Entry.Substring(271) : "" };
                this.COMM_EARN = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 285 ? RT71Entry.Substring(285) : "" };
                this.SFEE_EARN = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 299 ? RT71Entry.Substring(299) : "" };
                this.STAT_EARN = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 313 ? RT71Entry.Substring(313) : "" };
                this.SALES_TAX = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 327 ? RT71Entry.Substring(327) : "" };
                this.MERCH_BEF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 341 ? RT71Entry.Substring(341) : "" };
                this.MERCH_POST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 355 ? RT71Entry.Substring(355) : "" };
                this.MERCH_POST_JUDG = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 369 ? RT71Entry.Substring(369) : "" };
                this.CASH_BEF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 383 ? RT71Entry.Substring(383) : "" };
                this.CASH_POST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 397 ? RT71Entry.Substring(397) : "" };
                this.PAID_POST_JUDG = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 411 ? RT71Entry.Substring(411) : "" };
                this.DP_PRE_SUIT_NF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 425 ? RT71Entry.Substring(425) : "" };
                this.DP_POST_SUIT_NF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 439 ? RT71Entry.Substring(439) : "" };
                this.DP_POST_JUDG_NF = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 453 ? RT71Entry.Substring(453) : "" };
                this.DP_PRE_SUIT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 467 ? RT71Entry.Substring(467) : "" };
                this.DP_POST_SUIT = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 481 ? RT71Entry.Substring(481) : "" };
                this.DP_POST_JUDG = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 495 ? RT71Entry.Substring(495) : "" };
                this.AGENT_BEFSCOLL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 509 ? RT71Entry.Substring(509) : "" };
                this.AGENT_PSCOLL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 523 ? RT71Entry.Substring(523) : "" };
                this.AGENT_PJCOLL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 537 ? RT71Entry.Substring(537) : "" };
                this.COST_RECEIVED = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 551 ? RT71Entry.Substring(551) : "" };
                this.COST_RET = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 565 ? RT71Entry.Substring(565) : "" };
                this.COST_BAL = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 579 ? RT71Entry.Substring(579) : "" };
                this.NON_RCVRD_COST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 593 ? RT71Entry.Substring(593) : "" };
                this.COST_POST_JUDG = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 607 ? RT71Entry.Substring(607) : "" };
                this.AGENT_COST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 621 ? RT71Entry.Substring(621) : "" };
                this.TAX_REBATE = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 635 ? RT71Entry.Substring(635) : "" };
                this.TOT_EXP_COST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 649 ? RT71Entry.Substring(649) : "" };
                this.COST_EXP = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 663 ? RT71Entry.Substring(663) : "" };
                this.COST_RECOVERED = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 677 ? RT71Entry.Substring(677) : "" };
                this.EXP_COST = new YGCDecimal(14, 2) { DataString = RT71Entry.Length > 691 ? RT71Entry.Substring(691) : "" };
            }

            public override Type GetType() { return typeof(RecordType71); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}",
                    base.ToString(),
                    this.ORIG_CLAIM,
                    this.SUIT_AMT,
                    this.STAT_FEE,
                    this.JMT_AMT,
                    this.DEBT_BAL,
                    this.INT_DUE,
                    this.INT_DATE,
                    this.C_ADJUST,
                    this.CNTRACT_INT,
                    this.AFFIDAVIT,
                    this.ACCR_INT_BEF,
                    this.COMPR_AMT,
                    this.OVERPAY_AMT,
                    this.COLL_HOLD,
                    this.PRIN_COLL,
                    this.INT_COLL,
                    this.COMM_EARN,
                    this.SFEE_EARN,
                    this.STAT_EARN,
                    this.SALES_TAX,
                    this.MERCH_BEF,
                    this.MERCH_POST,
                    this.MERCH_POST_JUDG,
                    this.CASH_BEF,
                    this.CASH_POST,
                    this.PAID_POST_JUDG,
                    this.DP_PRE_SUIT_NF,
                    this.DP_POST_SUIT_NF,
                    this.DP_POST_JUDG_NF,
                    this.DP_PRE_SUIT,
                    this.DP_POST_SUIT,
                    this.DP_POST_JUDG,
                    this.AGENT_BEFSCOLL,
                    this.AGENT_PSCOLL,
                    this.AGENT_PJCOLL,
                    this.COST_RECEIVED,
                    this.COST_RET,
                    this.COST_BAL,
                    this.NON_RCVRD_COST,
                    this.COST_POST_JUDG,
                    this.AGENT_COST,
                    this.TAX_REBATE,
                    this.TOT_EXP_COST,
                    this.COST_EXP,
                    this.COST_RECOVERED,
                    this.EXP_COST
                    );
            }
        }
        #endregion

        #region Record Type 95
        /// <summary>
        /// Record Type 95 - Diary Entry
        /// </summary>
        public class RecordType95 : YGCSender2ReceiverRecord
        {
            #region Public Properties
            /// <summary>
            /// Claim Diary Date
            /// </summary>
            public YGCDate DIARY_DATE { get; private set; }
            /// <summary>
            /// Claim Diary Code
            /// </summary>
            public YGCNumber DIARY_CODE { get; private set; }
            /// <summary>
            /// Claim Diary Comment
            /// </summary>
            public YGCString DIARY_CMT { get; private set; }
            /// <summary>
            /// Claim Diary Queue
            /// </summary>
            public YGCString DIARY_QUEUE { get; private set; }
            /// <summary>
            /// Claim Diary Time
            /// </summary>
            public YGCStupidTime DIARY_TIME { get; private set; }
            /// <summary>
            /// Claim Diary Priority
            /// </summary>
            public YGCNumber DIARY_PRIORITY { get; private set; }
            #endregion

            public RecordType95() : base(95)
            {
                this.DIARY_DATE = new YGCDate();
                this.DIARY_CODE = new YGCNumber(8);
                this.DIARY_CMT = new YGCString(20);
                this.DIARY_QUEUE = new YGCString(8);
                this.DIARY_TIME = new YGCStupidTime();
                this.DIARY_PRIORITY = new YGCNumber(3);
            }
            public RecordType95(string RT95Entry) : base(RT95Entry)
            {
                try
                {
                    this.DIARY_DATE = new YGCDate() { DataString = RT95Entry.Length > 67 ? RT95Entry.Substring(67) : "" };
                    this.DIARY_CODE = new YGCNumber(8) { DataString = RT95Entry.Length > 75 ? RT95Entry.Substring(75) : "" };
                    this.DIARY_CMT = new YGCString(20) { DataString = RT95Entry.Length > 83 ? RT95Entry.Substring(83) : "" };
                    this.DIARY_QUEUE = new YGCString(8) { DataString = RT95Entry.Length > 103 ? RT95Entry.Substring(103) : "" };
                    this.DIARY_TIME = new YGCStupidTime() { DataString = RT95Entry.Length > 111 ? RT95Entry.Substring(111) : "" };
                    this.DIARY_PRIORITY = new YGCNumber(3) { DataString = RT95Entry.Length > 116 ? RT95Entry.Substring(116) : "" };
                }
                catch
                {
                    if (this.DIARY_DATE == null) this.DIARY_DATE = new YGCDate();
                    if (this.DIARY_CODE == null) this.DIARY_CODE = new YGCNumber(8);
                    if (this.DIARY_CMT == null) this.DIARY_CMT = new YGCString(20);
                    if (this.DIARY_QUEUE == null) this.DIARY_QUEUE = new YGCString(8);
                    if (this.DIARY_TIME == null) this.DIARY_TIME = new YGCStupidTime();
                    if (this.DIARY_PRIORITY == null) this.DIARY_PRIORITY = new YGCNumber(3);
                }
            }

            public override Type GetType() { return typeof(RecordType95); }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    base.ToString(),
                    this.DIARY_DATE,
                    this.DIARY_CODE,
                    this.DIARY_CMT,
                    this.DIARY_QUEUE,
                    this.DIARY_TIME,
                    this.DIARY_PRIORITY);
            }
        }
        #endregion
    }
}
