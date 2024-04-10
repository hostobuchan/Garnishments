using RecordTypes.YGC.Enums;
using System;

namespace RecordTypes2.YGC
{
    #region Record Type 01
    /// <summary>
    /// Record Type 01 - New Account Information (Sender to Receiver)
    /// <para>This record is required to create a new account. It identifies the creditor and current owner of the debt and establishes the current financial state of the debt. Value = 01.</para>
    /// </summary>
    public class RecordType01 : RecordTypes.YGC.RecordType01
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the date the account was first sent to the receiver to work, regardless of when it was uploaded to YouveGotClaims®. If this field is left blank, YouveGotReports will automatically fill it with the date this record is processed.
        /// </summary>
        public new DateTime? DATE_FORW { get { return base.DATE_FORW.Value; } set { base.DATE_FORW.Value = value; } }
        /// <summary>
        /// If you have bonded this account with a law listing company through your own means, enter the YGC ID of the listing company here. 
        /// <para>NOTE: Specifying a law list here does not trigger a bond request. To submit a coupon to bond the account through YGC, send a record 99.</para>
        /// <para>Current values for LAW_LIST are:</para>
        /// <para>NL = National List</para>
        /// <para>ALQ = American Legal Quarterly</para>
        /// <para>GB = General Bar</para>
        /// <para>CB = The Commercial Bar</para>
        /// <para>FL = Forwarders List</para>
        /// <para>CL = Columbia List</para>
        /// <para>WH = Wright Holmes</para>
        /// <para>IL = International Lawyer Referral</para>
        /// </summary>
        public new string LAW_LIST { get { return base.LAW_LIST.Value; } set { base.LAW_LIST.Value = value; } }
        /// <summary>
        /// This is the commission percentage the receiver earns as determined by the sender. There is no set numeric format.
        /// </summary>
        public new string COMM { get { return base.COMM.Value; } set { base.COMM.Value = value; } }
        /// <summary>
        /// This is the percentage the receiver earns if a suit is filed. There is no set numeric format.
        /// </summary>
        public new string SFEE { get { return base.SFEE.Value; } set { base.SFEE.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the original principal due at time of placement.
        /// </summary>
        public new decimal? ORIG_CLAIM { get { return base.ORIG_CLAIM.Value; } set { base.ORIG_CLAIM.Value = value; } }
        /// <summary>
        /// This is the dollar amount of accrued interest.
        /// </summary>
        public new decimal? ORIG_INT { get { return base.ORIG_INT.Value; } set { base.ORIG_INT.Value = value; } }
        /// <summary>
        /// This is the last day of the accrual period.
        /// </summary>
        public new DateTime? ORIG_INT_D { get { return base.ORIG_INT_D.Value; } set { base.ORIG_INT_D.Value = value; } }
        /// <summary>
        /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form:
        /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
        /// </summary>
        public new decimal? RATES_PRE { get { return base.RATES_PRE.Value; } set { base.RATES_PRE.Value = value; } }
        /// <summary>
        /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form:
        /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
        /// </summary>
        public new decimal? RATES_POST { get { return base.RATES_POST.Value; } set { base.RATES_POST.Value = value; } }
        /// <summary>
        /// This is the company name of either the original creditor or debt purchaser/collection agency.
        /// </summary>
        public new string CRED_NAME { get { return base.CRED_NAME.Value; } set { base.CRED_NAME.Value = value; } }
        /// <summary>
        /// This is an overflow field for CRED_NAME.
        /// </summary>
        public new string CRED_NAME2 { get { return base.CRED_NAME2.Value; } set { base.CRED_NAME2.Value = value; } }
        /// <summary>
        /// This is the company name of either the original creditor or debt purchaser/collection agency.
        /// </summary>
        public new string CRED_NAME_COMBINED { get { return base.CRED_NAME_COMBINED.Value; } set { base.CRED_NAME_COMBINED.Value = value; } }
        /// <summary>
        /// Street address of company in CRED_NAME.
        /// </summary>
        public new string CRED_STREET { get { return base.CRED_STREET.Value; } set { base.CRED_STREET.Value = value; } }
        /// <summary>
        /// City and state of company in CRED_NAME. Format this field as City ST or City,ST.
        /// <para>(Example: Linden NJ or Linden,NJ)</para>
        /// </summary>
        public new string CRED_CS { get { return base.CRED_CS.Value; } set { base.CRED_CS.Value = value; } }
        /// <summary>
        /// This is the creditor's zip code. It can accommodate the four-digit extension if you do not include the hyphen.
        /// </summary>
        public new string CRED_ZIP { get { return base.CRED_ZIP.Value; } set { base.CRED_ZIP.Value = value; } }
        /// <summary>
        /// This field represents everything the debtor owes at time of placement, excluding interest. It equals ORIG_CLAIM + LATE_FEE + CNTRCT_FEE + STAT_FEE + JDG_COSTS, where the last three fields are from a previous legal action defined in Record 07 and LATE_FEE is from this record.
        /// </summary>
        public new decimal? DEBT_BAL { get { return base.DEBT_BAL.Value; } set { base.DEBT_BAL.Value = value; } }
        /// <summary>
        /// User-defined. Suggested Codes:  ARB = Arbitration, MED = Medical, CARD = Credit Card, LOAN = loan
        /// </summary>
        public new string CTYPE { get { return base.CTYPE.Value; } set { base.CTYPE.Value = value; } }
        /// <summary>
        /// This is the date the last payment was received by the creditor.
        /// </summary>
        public new DateTime? DATE_LPAY { get { return base.DATE_LPAY.Value; } set { base.DATE_LPAY.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the last payment made to the creditor.
        /// </summary>
        public new decimal? AMT_LPAY { get { return base.AMT_LPAY.Value; } set { base.AMT_LPAY.Value = value; } }
        /// <summary>
        /// Typically, the date the credit card account was opened.
        /// </summary>
        public new DateTime? DATE_OPEN { get { return base.DATE_OPEN.Value; } set { base.DATE_OPEN.Value = value; } }
        /// <summary>
        /// This is the date the creditor has determined the debt will not be paid through normal channels.
        /// </summary>
        public new DateTime? CHRG_OFF_D { get { return base.CHRG_OFF_D.Value; } set { base.CHRG_OFF_D.Value = value; } }
        /// <summary>
        /// This is typically the same as ORIG_CLAIM.
        /// </summary>
        public new decimal? CHRG_OFF_A { get { return base.CHRG_OFF_A.Value; } set { base.CHRG_OFF_A.Value = value; } }
        /// <summary>
        /// If this debt was purchased, this field holds the date of purchase.
        /// </summary>
        public new DateTime? PURCHASE_D { get { return base.PURCHASE_D.Value; } set { base.PURCHASE_D.Value = value; } }
        /// <summary>
        /// This is only filled out if this debt was purchased or if the sender is a collection agency. It holds the name of the original creditor company.
        /// </summary>
        public new string ORIG_CRED { get { return base.ORIG_CRED.Value; } set { base.ORIG_CRED.Value = value; } }
        /// <summary>
        /// This is an overflow field for ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED2 { get { return base.ORIG_CRED2.Value; } set { base.ORIG_CRED2.Value = value; } }
        /// <summary>
        /// Combined Value of ORIG_CRED and ORIG_CRED2
        /// </summary>
        public new string ORIG_CRED_COMBINED { get { return base.ORIG_CRED_COMBINED.Value; } set { base.ORIG_CRED_COMBINED.Value = value; } }
        /// <summary>
        /// Typically pertains to purchased debt, and holds the number assigned to the batch of accounts purchased. Some reports in YouveGotReports can be filtered against this code.
        /// </summary>
        public new string PORT_ID { get { return base.PORT_ID.Value; } set { base.PORT_ID.Value = value; } }
        /// <summary>
        /// This is the standard code for the creditor's country.
        /// </summary>
        public new string CRED_CNTRY { get { return base.CRED_CNTRY.Value; } set { base.CRED_CNTRY.Value = value; } }
        /// <summary>
        /// This is the date the last payment was received by the issuer.
        /// </summary>
        public new DateTime? LPAY_ISS_D { get { return base.LPAY_ISS_D.Value; } set { base.LPAY_ISS_D.Value = value; } }
        /// <summary>
        /// This is the amount of the last payment received by the issuer.
        /// </summary>
        public new decimal? LPAY_ISS_AMT { get { return base.LPAY_ISS_AMT.Value; } set { base.LPAY_ISS_AMT.Value = value; } }
        /// <summary>
        /// This flag indicates whether the sender has media to support a lawsuit. It is up to the receiver to determine what kind of media will ultimately be required, at which time they will send a request. Use values Y or N.
        /// </summary>
        public new string MEDIA { get { return base.MEDIA.Value; } set { base.MEDIA.Value = value; } }
        /// <summary>
        /// Date of Delinquency
        /// </summary>
        public new DateTime? DELINQ_D { get { return base.DELINQ_D.Value; } set { base.DELINQ_D.Value = value; } }
        /// <summary>
        /// Date of Acceleration
        /// </summary>
        public new DateTime? ACCEL_D { get { return base.ACCEL_D.Value; } set { base.ACCEL_D.Value = value; } }
        /// <summary>
        /// Date of Repossession
        /// </summary>
        public new DateTime? REPO_D { get { return base.REPO_D.Value; } set { base.REPO_D.Value = value; } }
        /// <summary>
        /// Sale Date
        /// </summary>
        public new DateTime? SALE_D { get { return base.SALE_D.Value; } set { base.SALE_D.Value = value; } }
        /// <summary>
        /// Maturity Date
        /// </summary>
        public new DateTime? MATUR_D { get { return base.MATUR_D.Value; } set { base.MATUR_D.Value = value; } }
        /// <summary>
        /// The Statute of Limitations Start Date is calculated by the sender.
        /// </summary>
        public new DateTime? SOL_START_D { get { return base.SOL_START_D.Value; } set { base.SOL_START_D.Value = value; } }
        /// <summary>
        /// The Statute of Limitations End Date is calculated by the sender. You can also refer to the Documents tab, Business Reference category for the current statutes for each state.
        /// </summary>
        public new DateTime? SOL_END_D { get { return base.SOL_END_D.Value; } set { base.SOL_END_D.Value = value; } }
        /// <summary>
        /// This is the accumulation of late fees for non-payment before placement. This should be included in the calculation of DEBT_BAL.
        /// </summary>
        public new decimal? LATE_FEE { get { return base.LATE_FEE.Value; } set { base.LATE_FEE.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before the current CRED_NAME or ORIG_CRED if present. If ORIG_CRED is not present, HIST_CRED1 should NOT be used in place of ORIG_CRED.
        /// </summary>
        public new string HIST_CRED1 { get { return base.HIST_CRED1.Value; } set { base.HIST_CRED1.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED1.
        /// </summary>
        public new string HIST_CRED2 { get { return base.HIST_CRED2.Value; } set { base.HIST_CRED2.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED2.
        /// </summary>
        public new string HIST_CRED3 { get { return base.HIST_CRED3.Value; } set { base.HIST_CRED3.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED3.
        /// </summary>
        public new string HIST_CRED4 { get { return base.HIST_CRED4.Value; } set { base.HIST_CRED4.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED4.
        /// </summary>
        public new string HIST_CRED5 { get { return base.HIST_CRED5.Value; } set { base.HIST_CRED5.Value = value; } }
        /// <summary>
        /// This holds any additional cost besides legal costs, such as bounced check fees, already incurred for pursuing this debt.
        /// </summary>
        public new decimal? ADDITIONAL_COST { get { return base.ADDITIONAL_COST.Value; } set { base.ADDITIONAL_COST.Value = value; } }
        /// <summary>
        /// If a sender recalls an account placed with YGC, this field holds the date it was recalled.
        /// </summary>
        public new DateTime? RECALL_DATE { get { return base.RECALL_DATE.Value; } set { base.RECALL_DATE.Value = value; } }
        /// <summary>
        /// This is the number for the debt account before charge-off. This may differ from FORW_FILE.
        /// </summary>
        public new string PRECHARGEOFFACCOUNTNUMBER { get { return base.PRECHARGEOFFACCOUNTNUMBER.Value; } set { base.PRECHARGEOFFACCOUNTNUMBER.Value = value; } }
        /// <summary>
        /// This flag indicates whether the account requires a FACT Act statement.
        /// <para>Use values Y or N.</para>
        /// </summary>
        public new bool FACTACTSTATEMENT { get { return base.FACTACTSTATEMENT.Value; } set { base.FACTACTSTATEMENT.Value = value; } }
        /// <summary>
        /// This field is the Attorney, Agency or Vendor Code set by the current Creditor or Debt Buyer's internal system.
        /// </summary>
        public new string VENDORCODE { get { return base.VENDORCODE.Value; } set { base.VENDORCODE.Value = value; } }
        /// <summary>
        /// This field is used when original products or services were provided by one company for offer under another company's brand.  This allows the current Creditor or Debt Buyer to identify the private label from the original creditor. (Example: "Private Label" -GE/Walmart)
        /// </summary>
        public new string LOANDESCRIPTION { get { return base.LOANDESCRIPTION.Value; } set { base.LOANDESCRIPTION.Value = value; } }
        /// <summary>
        /// This is an overflow field for ORIG_CRED and ORIG_CRED2.
        /// </summary>
        public new string ORIG_CRED3 { get { return base.ORIG_CRED3.Value; } set { base.ORIG_CRED3.Value = value; } }
        /// <summary>
        /// Street address of company in ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED_STREET { get { return base.ORIG_CRED_STREET.Value; } set { base.ORIG_CRED_STREET.Value = value; } }
        /// <summary>
        /// Street 2 field for address of company in ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED_STREET2 { get { return base.ORIG_CRED_STREET2.Value; } set { base.ORIG_CRED_STREET2.Value = value; } }
        /// <summary>
        /// City of company in ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED_CITY { get { return base.ORIG_CRED_CITY.Value; } set { base.ORIG_CRED_CITY.Value = value; } }
        /// <summary>
        /// State of company in ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED_ST { get { return base.ORIG_CRED_ST.Value; } set { base.ORIG_CRED_ST.Value = value; } }
        /// <summary>
        /// This is the ORIG_CRED zip code.
        /// <para>It can accommodate the four-digit extension if you do not include the hyphen.</para>
        /// </summary>
        public new string ORIG_CRED_ZIP { get { return base.ORIG_CRED_ZIP.Value; } set { base.ORIG_CRED_ZIP.Value = value; } }
        /// <summary>
        /// The date in which the original creditor reported the date of default. 
        /// <para>If the date was not reported by the original creditor, then the debt buyer calculates this field by subtracting 210 days from the charge off date.</para>
        /// </summary>
        public new DateTime? OCCURRENCE_D { get { return base.OCCURRENCE_D.Value; } set { base.OCCURRENCE_D.Value = value; } }
        /// <summary>
        /// This field indicates whether the date listed in the OCCURRENCE_D field was calculated or provided by the original creditor. 
        /// <para>Use Values:</para>
        /// <para>C = Calculated</para>
        /// <para>P = Provided</para>
        /// <para>N = Not Applicable</para>
        /// </summary>
        public new OccurrenceType OCCURRENCE_D_FLAG { get { return base.OCCURRENCE_D_FLAG.Value; } set { base.OCCURRENCE_D_FLAG.Value = value; } }
        /// <summary>
        /// This is a post charge off fee that accrued prior to the time of placement. This should be included in the DEBT_BAL.
        /// </summary>
        public new decimal? POST_CHARGE_OFF_FEE { get { return base.POST_CHARGE_OFF_FEE.Value; } set { base.POST_CHARGE_OFF_FEE.Value = value; } }
        /// <summary>
        /// This is the total amount of payments that have accrued between CHRG_OFF_D and PURCHASE_D.
        /// </summary>
        public new decimal? SELLER_PAYMENTS { get { return base.SELLER_PAYMENTS.Value; } set { base.SELLER_PAYMENTS.Value = value; } }
        /// <summary>
        /// This is the total amount of credits that have accrued between CHRG_OFF_D and PURCHASE_D.
        /// </summary>
        public new decimal? SELLER_CREDITS { get { return base.SELLER_CREDITS.Value; } set { base.SELLER_CREDITS.Value = value; } }
        /// <summary>
        /// This is the total amount of payments and credits that have accrued between CHRG_OFF_D and PURCHASE_D.
        /// </summary>
        public new decimal? SELLER_PAYMENTS_CREDITS { get { return base.SELLER_PAYMENTS_CREDITS.Value; } set { base.SELLER_PAYMENTS_CREDITS.Value = value; } }
        /// <summary>
        /// This is the total amount of fees that have accrued between CHRG_OFF_D and PURCHASE_D.
        /// </summary>
        public new decimal? POSTCHARGEOFFSELLERFEES { get { return base.POSTCHARGEOFFSELLERFEES.Value; } set { base.POSTCHARGEOFFSELLERFEES.Value = value; } }
        #endregion

        public RecordType01() : base() { }
        public RecordType01(string RT01Entry) : base(RT01Entry) { }

        public override Type GetType() { return typeof(RecordType01); }
    }
    #endregion

    #region Record Type 02
    /// <summary>
    /// Record Type 02 - Primary Debtor Information (Sender to Receiver)
    /// <para>This record identifies the primary debtor and significant legal events. Record type value = 02. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 02, 31 or 52, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
    /// </summary>
    public class RecordType02 : RecordTypes.YGC.RecordType02
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the primary debtor's name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_NAME { get { return base.D1_NAME.Value; } set { base.D1_NAME.Value = value; } }
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
        public new Salutation D1_SALUT { get { return base.D1_SALUT.Value; } set { base.D1_SALUT.Value = value; } }
        /// <summary>
        /// This is the primary debtor's alias name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_ALIAS { get { return base.D1_ALIAS.Value; } set { base.D1_ALIAS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET { get { return base.D1_STREET.Value; } set { base.D1_STREET.Value = value; } }
        /// <summary>
        /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
        /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
        /// </para></summary>
        public new string D1_CS { get { return base.D1_CS.Value; } set { base.D1_CS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
        /// </summary>
        public new string D1_ZIP { get { return base.D1_ZIP.Value; } set { base.D1_ZIP.Value = value; } }
        /// <summary>
        /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_PHONE { get { return base.D1_PHONE.Value; } set { base.D1_PHONE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_FAX { get { return base.D1_FAX.Value; } set { base.D1_FAX.Value = value; } }
        /// <summary>
        /// This is the primary debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D1_SSN { get { return base.D1_SSN.Value; } set { base.D1_SSN.Value = value; } }
        /// <summary>
        /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
        /// </summary>
        public new string RFILE { get { return base.RFILE.Value; } set { base.RFILE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's date of birth.
        /// </summary>
        public new DateTime? D1_DOB { get { return base.D1_DOB.Value; } set { base.D1_DOB.Value = value; } }
        /// <summary>
        /// This is the primary debtor's driver's license number.
        /// </summary>
        public new string D1_DL { get { return base.D1_DL.Value; } set { base.D1_DL.Value = value; } }
        /// <summary>
        /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STATE { get { return base.D1_STATE.Value; } set { base.D1_STATE.Value = value; } }
        /// <summary>
        /// Set this field to Y if the served papers were returned.
        /// </summary>
        public new bool D1_MAIL { get { return base.D1_MAIL.Value; } set { base.D1_MAIL.Value = value; } }
        /// <summary>
        /// This is the date the suit was served.
        /// </summary>
        public new DateTime? SERVICE_D { get { return base.SERVICE_D.Value; } set { base.SERVICE_D.Value = value; } }
        /// <summary>
        /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
        /// </summary>
        public new DateTime? ANSWER_DUE_D { get { return base.ANSWER_DUE_D.Value; } set { base.ANSWER_DUE_D.Value = value; } }
        /// <summary>
        /// This is the date the debtor's response was filed.
        /// </summary>
        public new DateTime? ANSWER_FILE_D { get { return base.ANSWER_FILE_D.Value; } set { base.ANSWER_FILE_D.Value = value; } }
        /// <summary>
        /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
        /// </summary>
        public new DateTime? DEFAULT_D { get { return base.DEFAULT_D.Value; } set { base.DEFAULT_D.Value = value; } }
        /// <summary>
        /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
        /// </summary>
        public new DateTime? TRIAL_D { get { return base.TRIAL_D.Value; } set { base.TRIAL_D.Value = value; } }
        /// <summary>
        /// This is the date of the latest hearing on a motion filed by either party.
        /// </summary>
        public new DateTime? HEARING_D { get { return base.HEARING_D.Value; } set { base.HEARING_D.Value = value; } }
        /// <summary>
        /// This is the date a lien was filed against a debtor's property.
        /// </summary>
        public new DateTime? LIEN_D { get { return base.LIEN_D.Value; } set { base.LIEN_D.Value = value; } }
        /// <summary>
        /// This is the date garnishment against the debtor's wages was established.
        /// </summary>
        public new DateTime? GARN_D { get { return base.GARN_D.Value; } set { base.GARN_D.Value = value; } }
        ///<summary>
        ///This is the method used to serve the papers. 
        ///<para>The valid codes are: </para>
        ///<para>PER = Personal</para>
        ///<para>CER = Certified Mail</para>
        ///<para>SUB = Sub-service</para>
        ///<para>POS = Posting (left at front door)</para>
        ///<para>FIR = First Class Mail</para>
        ///</summary>
        public new string SERVICE_TYPE { get { return base.SERVICE_TYPE.Value; } set { base.SERVICE_TYPE.Value = value; } }
        /// <summary>
        /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2 { get { return base.D1_STREET2.Value; } set { base.D1_STREET2.Value = value; } }
        /// <summary>
        /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CITY { get { return base.D1_CITY.Value; } set { base.D1_CITY.Value = value; } }
        /// <summary>
        /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_CELL { get { return base.D1_CELL.Value; } set { base.D1_CELL.Value = value; } }
        /// <summary>
        /// Fair Isaac credit score
        /// </summary>
        public new int? SCORE_FICO { get { return base.SCORE_FICO.Value; } set { base.SCORE_FICO.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_COLLECT { get { return base.SCORE_COLLECT.Value; } set { base.SCORE_COLLECT.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_OTHER { get { return base.SCORE_OTHER.Value; } set { base.SCORE_OTHER.Value = value; } }
        /// <summary>
        /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CNTRY { get { return base.D1_CNTRY.Value; } set { base.D1_CNTRY.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET_LONG { get { return base.D1_STREET_LONG.Value; } set { base.D1_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2_LONG { get { return base.D1_STREET2_LONG.Value; } set { base.D1_STREET2_LONG.Value = value; } }
        /// <summary>
        /// Debtor First Name
        /// </summary>
        public new string FIRSTNAME { get { return base.FIRSTNAME.Value; } set { base.FIRSTNAME.Value = value; } }
        /// <summary>
        /// Debtor Last Name
        /// </summary>
        public new string LASTNAME { get { return base.LASTNAME.Value; } set { base.LASTNAME.Value = value; } }
        /// <summary>
        /// Creditor-Internal calculated score
        /// </summary>
        public new string SCOREINTERNAL { get { return base.SCOREINTERNAL.Value; } set { base.SCOREINTERNAL.Value = value; } }
        /// <summary>
        /// This field is the Placement or Internal Delinquency Stage of the account at the time of placement used by the the Creditor or Debt Buyer (User Defined).
        /// </summary>
        public new string STAGE { get { return base.STAGE.Value; } set { base.STAGE.Value = value; } }
        #endregion

        public RecordType02() : base() { }
        public RecordType02(string RT02Entry) : base(RT02Entry) { }

        public override Type GetType() { return typeof(RecordType02); }
    }
    #endregion

    #region Record Type 03
    /// <summary>
    /// Record Type 03 - 2nd and 3rd Debtor Information (Sender to Receiver)
    /// <para>If more than one name appears on the original contract between creditor and debtor, they are identified here. This can also be used for a co-signer. Record type value = 03. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 03, 33 or 53, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.</para>
    /// </summary>
    public class RecordType03 : RecordTypes.YGC.RecordType03
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the name of the second debtor or co-signer. The format is Lastname/Firstname.
        /// </summary>
        public new string D2_NAME { get { return base.D2_NAME.Value; } set { base.D2_NAME.Value = value; } }
        /// <summary>
        /// This is the second debtor's street address. If there is no D2_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
        /// </summary>
        public new string D2_STREET { get { return base.D2_STREET.Value; } set { base.D2_STREET.Value = value; } }
        /// <summary>
        /// This is the second debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D2_CSZ field but no D2_CNTRY field populated in this record, the country will become empty on YouveGotReports.
        /// </summary>
        public new string D2_CSZ { get { return base.D2_CSZ.Value; } set { base.D2_CSZ.Value = value; } }
        /// <summary>
        /// This is the second debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D2_PHONE { get { return base.D2_PHONE.Value; } set { base.D2_PHONE.Value = value; } }
        /// <summary>
        /// This is the second debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D2_SSN { get { return base.D2_SSN.Value; } set { base.D2_SSN.Value = value; } }
        /// <summary>
        /// This is the name of the third debtor or co-signer. The format is Lastname/Firstname.
        /// </summary>
        public new string D3_NAME { get { return base.D3_NAME.Value; } set { base.D3_NAME.Value = value; } }
        /// <summary>
        /// This is the third debtor's street address. If there is no D3_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
        /// </summary>
        public new string D3_STREET { get { return base.D3_STREET.Value; } set { base.D3_STREET.Value = value; } }
        /// <summary>
        /// This is the third debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D3_CSZ field but no D3_CNTRY field populated in this record, the country will become empty on YouveGotReports.
        /// </summary>
        public new string D3_CSZ { get { return base.D3_CSZ.Value; } set { base.D3_CSZ.Value = value; } }
        /// <summary>
        /// This is the third debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D3_PHONE { get { return base.D3_PHONE.Value; } set { base.D3_PHONE.Value = value; } }
        /// <summary>
        /// This is the third debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D3_SSN { get { return base.D3_SSN.Value; } set { base.D3_SSN.Value = value; } }
        /// <summary>
        /// This is the second debtor's date of birth.
        /// </summary>
        public new DateTime? D2_DOB { get { return base.D2_DOB.Value; } set { base.D2_DOB.Value = value; } }
        /// <summary>
        /// This is the third debtor's date of birth.
        /// </summary>
        public new DateTime? D3_DOB { get { return base.D3_DOB.Value; } set { base.D3_DOB.Value = value; } }
        /// <summary>
        /// This is the second debtor's driver's license number.
        /// </summary>
        public new string D2_DL { get { return base.D2_DL.Value; } set { base.D2_DL.Value = value; } }
        /// <summary>
        /// This is the third debtor's driver's license number.
        /// </summary>
        public new string D3_DL { get { return base.D3_DL.Value; } set { base.D3_DL.Value = value; } }
        /// <summary>
        /// This is the standard code for the second debtor's country. If there is a D2_CNTRY field but no D2_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
        /// </summary>
        public new string D2_CNTRY { get { return base.D2_CNTRY.Value; } set { base.D2_CNTRY.Value = value; } }
        /// <summary>
        /// This is the standard code for the third debtor's country. If there is a D3_CNTRY field but no D3_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
        /// </summary>
        public new string D3_CNTRY { get { return base.D3_CNTRY.Value; } set { base.D3_CNTRY.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire 2nd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D2_STREET in case your receivers cannot yet accept this newer field. If there is a D2_STREET_LONG field but no D2_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D2_STREET_LONG { get { return base.D2_STREET_LONG.Value; } set { base.D2_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D2_STREET_LONG. If there is a D2_STREET2_LONG field but no D2_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D2_STREET2_LONG { get { return base.D2_STREET2_LONG.Value; } set { base.D2_STREET2_LONG.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire 3rd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D3_STREET in case your receivers cannot yet accept this newer field. If there is a D3_STREET_LONG field but no D3_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D3_STREET_LONG { get { return base.D3_STREET_LONG.Value; } set { base.D3_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D3_STREET_LONG. If there is a D3_STREET2_LONG field but no D3_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D3_STREET2_LONG { get { return base.D3_STREET2_LONG.Value; } set { base.D3_STREET2_LONG.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's first name. Include middle name here if available. Use this with D2_LNAME instead of D2_NAME if your collection software enables it.
        /// </summary>
        public new string D2_FNAME { get { return base.D2_FNAME.Value; } set { base.D2_FNAME.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D2_FNAME instead of D2_NAME if your collection software enables it.
        /// </summary>
        public new string D2_LNAME { get { return base.D2_LNAME.Value; } set { base.D2_LNAME.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's city. Use this with D2_STATE and D2_ZIP instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_CITY { get { return base.D2_CITY.Value; } set { base.D2_CITY.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's state. Use the standard abbreviation. Use this with D2_CITY and D2_ZIP instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_STATE { get { return base.D2_STATE.Value; } set { base.D2_STATE.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D2_CITY and D2_STATE instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_ZIP { get { return base.D2_ZIP.Value; } set { base.D2_ZIP.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's first name. Include middle name here if available. Use this with D3_LNAME instead of D3_NAME if your collection software enables it.
        /// </summary>
        public new string D3_FNAME { get { return base.D3_FNAME.Value; } set { base.D3_FNAME.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D3_FNAME instead of D3_NAME if your collection software enables it.
        /// </summary>
        public new string D3_LNAME { get { return base.D3_LNAME.Value; } set { base.D3_LNAME.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's city. Use this with D3_STATE and D3_ZIP instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_CITY { get { return base.D3_CITY.Value; } set { base.D3_CITY.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's state. Use the standard abbreviation. Use this with D3_CITY and D3_ZIP instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_STATE { get { return base.D3_STATE.Value; } set { base.D3_STATE.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D3_CITY and D3_STATE instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_ZIP { get { return base.D3_ZIP.Value; } set { base.D3_ZIP.Value = value; } }
        #endregion

        public RecordType03() : base() { }
        public RecordType03(string RT03Entry) : base(RT03Entry) { }

        public override Type GetType() { return typeof(RecordType03); }
    }
    #endregion

    #region Record Type 04
    /// <summary>
    /// Record Type 04 - Employment Information (Sender to Receiver)
    /// <para>This record holds the debtors' employment information. You can submit a record for each of 3 distinct debtors for the same account. Value = 04.</para>
    /// </summary>
    public class RecordType04 : RecordTypes.YGC.RecordType04
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Company name of debtor's employer.
        /// </summary>
        public new string Employer_Name { get { return base.Employer_Name.Value; } set { base.Employer_Name.Value = value; } }
        /// <summary>
        /// This is the employer's street address.
        /// </summary>
        public new string Employer_Street { get { return base.Employer_Street.Value; } set { base.Employer_Street.Value = value; } }
        /// <summary>
        /// This is the employer's PO Box number.
        /// </summary>
        public new string Employer_PO_Box { get { return base.Employer_PO_Box.Value; } set { base.Employer_PO_Box.Value = value; } }
        /// <summary>
        /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
        /// </summary>
        public new string Employer_City_State { get { return base.Employer_City_State.Value; } set { base.Employer_City_State.Value = value; } }
        /// <summary>
        /// This is the employer's zip code. It can accommodate the four-digit extension with the hyphen.
        /// </summary>
        public new string Employer_Zip { get { return base.Employer_Zip.Value; } set { base.Employer_Zip.Value = value; } }
        /// <summary>
        /// This is the employer's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string Employer_Phone { get { return base.Employer_Phone.Value; } set { base.Employer_Phone.Value = value; } }
        /// <summary>
        /// This is the employer's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string Employer_Fax { get { return base.Employer_Fax.Value; } set { base.Employer_Fax.Value = value; } }
        /// <summary>
        /// Department or personnel for correspondence to employer.
        /// </summary>
        public new string Employer_ATTN { get { return base.Employer_ATTN.Value; } set { base.Employer_ATTN.Value = value; } }
        /// <summary>
        /// This is the contact person at the payroll department.
        /// </summary>
        public new string Employer_Payroll { get { return base.Employer_Payroll.Value; } set { base.Employer_Payroll.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 employment records. Values are 1, 2 or 3. 
        /// <para>(Ex: The record 04 for the primary debtor can have EMP_NO = 1 and the record 04 for the co-signer can have EMP_NO = 2.)</para>
        /// <para>If a record 04 is sent with the same EMP_NO as a previous record 04 for the same account, the second record will overwrite the first.</para>
        /// </summary>
        public new int? Debtor_Number { get { return base.Debtor_Number.Value; } set { base.Debtor_Number.Value = value; } }
        /// <summary>
        /// This is the debtor name. There is no format restriction.
        /// </summary>
        public new string Employee_Name { get { return base.Employee_Name.Value; } set { base.Employee_Name.Value = value; } }
        /// <summary>
        /// Income Earned Per Pay Frequency Period
        /// </summary>
        public new decimal? Employee_Income { get { return base.Employee_Income.Value; } set { base.Employee_Income.Value = value; } }
        /// <summary>
        /// Freqeuncy with Which the Listed Income is Distributed
        /// </summary>
        public new IncomeFrequency Employee_Frequency { get { return base.Employee_Frequency.Value; } set { base.Employee_Frequency.Value = value; } }
        /// <summary>
        /// This is the employee's title at the given employer.
        /// </summary>
        public new string Employee_Position { get { return base.Employee_Position.Value; } set { base.Employee_Position.Value = value; } }
        /// <summary>
        /// Number of Months with Employer
        /// </summary>
        public new int? Employee_Tenure { get { return base.Employee_Tenure.Value; } set { base.Employee_Tenure.Value = value; } }
        /// <summary>
        /// This is the standard code for the employee's country.
        /// </summary>
        public new string Employer_Country { get { return base.Employer_Country.Value; } set { base.Employer_Country.Value = value; } }
        #endregion

        public RecordType04() : base() { }
        public RecordType04(string RT04Entry) : base(RT04Entry) { }

        public override Type GetType() { return typeof(RecordType04); }
    }
    #endregion

    #region Record Type 05
    /// <summary>
    /// Record Type 05 - Bank/Asset Information (Sender to Receiver)
    /// <para>This record holds any bank account information and non-auto or non-real estate asset information for the debtor. You can submit a record for each of 3 distinct bank accounts for the same debtor. Value = 05.</para>
    /// </summary>
    public class RecordType05 : RecordTypes.YGC.RecordType05
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Currently a free field
        /// </summary>
        public new string FILLER { get { return base.FILLER.Value; } set { base.FILLER.Value = value; } }
        /// <summary>
        /// This is the name of the debtor's bank.
        /// </summary>
        public new string BANK_NAME { get { return base.BANK_NAME.Value; } set { base.BANK_NAME.Value = value; } }
        /// <summary>
        /// This is the debtor's bank street address.
        /// </summary>
        public new string BANK_STREET { get { return base.BANK_STREET.Value; } set { base.BANK_STREET.Value = value; } }
        /// <summary>
        /// This is the bank's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
        /// </summary>
        public new string BANK_CSZ { get { return base.BANK_CSZ.Value; } set { base.BANK_CSZ.Value = value; } }
        /// <summary>
        /// This is the contact name for the bank.
        /// </summary>
        public new string BANK_ATTN { get { return base.BANK_ATTN.Value; } set { base.BANK_ATTN.Value = value; } }
        /// <summary>
        /// This is the bank's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string BANK_PHONE { get { return base.BANK_PHONE.Value; } set { base.BANK_PHONE.Value = value; } }
        /// <summary>
        /// This is the bank's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string BANK_FAX { get { return base.BANK_FAX.Value; } set { base.BANK_FAX.Value = value; } }
        /// <summary>
        /// This is the debtor's bank account number.
        /// </summary>
        public new string BANK_ACCT { get { return base.BANK_ACCT.Value; } set { base.BANK_ACCT.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET1 { get { return base.MISC_ASSET1.Value; } set { base.MISC_ASSET1.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET2 { get { return base.MISC_ASSET2.Value; } set { base.MISC_ASSET2.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET3 { get { return base.MISC_ASSET3.Value; } set { base.MISC_ASSET3.Value = value; } }
        /// <summary>
        /// This field holds a relevant phone number relating to the miscellaneous asset. If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_PHONE { get { return base.MISC_PHONE.Value; } set { base.MISC_PHONE.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 bank records. Values are 1, 2 or 3. 
        /// <para>(Ex: The record 05 for the debtor's checking account can have BANK_NO = 1 and the record 05 for the debtor's savings account can have BANK_NO = 2.)</para>
        /// <para>If a record 05 is sent with the same BANK_NO as a previous record 05 for the same account, the second record will overwrite the first. If you have a miscellaneous asset described in this record, this value must be 1.</para>
        /// </summary>
        public new int? BANK_NO { get { return base.BANK_NO.Value; } set { base.BANK_NO.Value = value; } }
        /// <summary>
        /// This is the standard code for the bank's country.
        /// </summary>
        public new string BANK_CNTRY { get { return base.BANK_CNTRY.Value; } set { base.BANK_CNTRY.Value = value; } }
        /// <summary>
        /// This is the ABA routing number for the bank account. You may use this field in addition to or instead of the BANK_ACCT field.
        /// </summary>
        public new string ROUTINGNUMBER { get { return base.ROUTINGNUMBER.Value; } set { base.ROUTINGNUMBER.Value = value; } }
        #endregion

        public RecordType05() : base() { }
        public RecordType05(string RT05Entry) : base(RT05Entry) { }

        public override Type GetType() { return typeof(RecordType05); }
    }
    #endregion

    #region Record Type 06
    /// <summary>
    /// Record Type 06 - Misc Information (Sender to Receiver)
    /// <para>This record is for debtor attorney information and any miscellaneous information that no other record in the DataStandard accommodates. You can submit a record for each of 3 debtor attorneys for the same account. Value = 06.</para>
    /// </summary>
    public class RecordType06 : RecordTypes.YGC.RecordType06
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is for the name of the debtor's attorney. There is no format restriction.
        /// </summary>
        public new string ADVA_NAME { get { return base.ADVA_NAME.Value; } set { base.ADVA_NAME.Value = value; } }
        /// <summary>
        /// This is the name of the debtor's attorney's firm.
        /// </summary>
        public new string ADVA_FIRM { get { return base.ADVA_FIRM.Value; } set { base.ADVA_FIRM.Value = value; } }
        /// <summary>
        /// This is an overflow field for ADVA_FIRM.
        /// </summary>
        public new string ADVA_FIRM2 { get { return base.ADVA_FIRM2.Value; } set { base.ADVA_FIRM2.Value = value; } }
        /// <summary>
        /// This is the attorney's street address.
        /// </summary>
        public new string ADVA_STREET { get { return base.ADVA_STREET.Value; } set { base.ADVA_STREET.Value = value; } }
        /// <summary>
        /// This is the attorney's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
        /// </summary>
        public new string ADVA_CSZ { get { return base.ADVA_CSZ.Value; } set { base.ADVA_CSZ.Value = value; } }
        /// <summary>
        /// This is the attorney's preferred salutation in correspondence, such as "Dear Attorney Lewis:"
        /// </summary>
        public new string ADVA_SALUT { get { return base.ADVA_SALUT.Value; } set { base.ADVA_SALUT.Value = value; } }
        /// <summary>
        /// This is the attorney's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string ADVA_PHONE { get { return base.ADVA_PHONE.Value; } set { base.ADVA_PHONE.Value = value; } }
        /// <summary>
        /// This is the attorney's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string ADVA_FAX { get { return base.ADVA_FAX.Value; } set { base.ADVA_FAX.Value = value; } }
        /// <summary>
        /// This is the internal file number at the debtor's attorney's firm for this account.
        /// </summary>
        public new string ADVA_FILENO { get { return base.ADVA_FILENO.Value; } set { base.ADVA_FILENO.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new DateTime? MISC_DATE1 { get { return base.MISC_DATE1.Value; } set { base.MISC_DATE1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new DateTime? MISC_DATE2 { get { return base.MISC_DATE2.Value; } set { base.MISC_DATE2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new decimal? MISC_AMT1 { get { return base.MISC_AMT1.Value; } set { base.MISC_AMT1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new decimal? MISC_AMT2 { get { return base.MISC_AMT2.Value; } set { base.MISC_AMT2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM1 { get { return base.MISC_COMM1.Value; } set { base.MISC_COMM1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM2 { get { return base.MISC_COMM2.Value; } set { base.MISC_COMM2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM3 { get { return base.MISC_COMM3.Value; } set { base.MISC_COMM3.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM4 { get { return base.MISC_COMM4.Value; } set { base.MISC_COMM4.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 debtor attorney records. Values are 1, 2 or 3.
        /// <para>(Ex: The record 06 for the debtor's primary attorney can have ADVA_NUM = 1 and the record 06 for the debtor's secondary attorney can have ADVA_NUM = 2.)</para>
        /// <para>If a record 06 is sent with the same ADVA_NUM as a previous record 06 for the same account, the second record will overwrite the first. If you have miscellaneous information described in this record, this value must be 1.</para>
        /// </summary>
        public new int? ADVA_NUM { get { return base.ADVA_NUM.Value; } set { base.ADVA_NUM.Value = value; } }
        /// <summary>
        /// This is the standard code for the debtor attorney's country.
        /// </summary>
        public new string ADVA_CNTRY { get { return base.ADVA_CNTRY.Value; } set { base.ADVA_CNTRY.Value = value; } }
        #endregion

        public RecordType06() : base() { }
        public RecordType06(string RT06Entry) : base(RT06Entry) { }

        public override Type GetType() { return typeof(RecordType06); }
    }
    #endregion

    #region Record Type 07
    /// <summary>
    /// Record Type 07 - Legal Information (Sender to Receiver)
    /// <para>This record reports information regarding a suit filed against the debtor. More details can be provided in return by the receiver (agency/firm) in record 41. Value = 07.</para>
    /// </summary>
    public class RecordType07 : RecordTypes.YGC.RecordType07
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the county the suit was filed in.
        /// </summary>
        public new string CRT_COUNTY { get { return base.CRT_COUNTY.Value; } set { base.CRT_COUNTY.Value = value; } }
        /// <summary>
        /// Typically the full name of the court. Ex: Gwinnett County State Court, THE NINTH JUDICIAL CIRCUIT.
        /// </summary>
        public new string CRT_DESIG { get { return base.CRT_DESIG.Value; } set { base.CRT_DESIG.Value = value; } }
        /// <summary>
        /// This is a more brief description of the court. Ex: Superior, Supreme, Circuit
        /// </summary>
        public new string CRT_TYPE { get { return base.CRT_TYPE.Value; } set { base.CRT_TYPE.Value = value; } }
        /// <summary>
        /// This could be the official name of the sheriff's office or title, such as GLENN COUNTY SHERIFF, or the full name of the sheriff, which can be split between this field and SHER_NAME2.
        /// </summary>
        public new string SHER_NAME { get { return base.SHER_NAME.Value; } set { base.SHER_NAME.Value = value; } }
        /// <summary>
        /// This can be an overflow field for SHER_NAME.
        /// </summary>
        public new string SHER_NAME2 { get { return base.SHER_NAME2.Value; } set { base.SHER_NAME2.Value = value; } }
        /// <summary>
        /// This is the sheriff's office's street address.
        /// </summary>
        public new string SHER_STREET { get { return base.SHER_STREET.Value; } set { base.SHER_STREET.Value = value; } }
        /// <summary>
        /// This is the sheriff's office's city, state and zip code.
        /// <para>Format this field as City ST Zip or City,ST Zip.</para>
        /// <para>(Example: Linden NJ 07036 or Linden,NJ 07036)</para>
        /// </summary>
        public new string SHER_CSZ { get { return base.SHER_CSZ.Value; } set { base.SHER_CSZ.Value = value; } }
        /// <summary>
        /// This is the dollar amount requested in the suit. If this field is populated, SUIT_DATE must contain a valid date.
        /// </summary>
        public new decimal? SUIT_AMT { get { return base.SUIT_AMT.Value; } set { base.SUIT_AMT.Value = value; } }
        /// <summary>
        /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
        /// </summary>
        public new decimal? CONTRACT_FEE { get { return base.CONTRACT_FEE.Value; } set { base.CONTRACT_FEE.Value = value; } }
        /// <summary>
        /// This is the statutory fee awarded only to the attorney (not remitted on), determined by the debtor state.
        /// </summary>
        public new decimal? STAT_FEE { get { return base.STAT_FEE.Value; } set { base.STAT_FEE.Value = value; } }
        /// <summary>
        /// Initial number assigned to the suit.
        /// </summary>
        public new string DOCKET_NO { get { return base.DOCKET_NO.Value; } set { base.DOCKET_NO.Value = value; } }
        /// <summary>
        /// Depending on the state the suit is filed in, a new number may be assigned upon judgment.
        /// </summary>
        public new string JUDGMENT_NO { get { return base.JUDGMENT_NO.Value; } set { base.JUDGMENT_NO.Value = value; } }
        /// <summary>
        /// If the debtor has filed for bankruptcy, this is the court-issued number.
        /// </summary>
        public new string BKCY_NO { get { return base.BKCY_NO.Value; } set { base.BKCY_NO.Value = value; } }
        /// <summary>
        /// This is the date the suit was filed. If this field is populated, SUIT_AMT must be non-zero.
        /// </summary>
        public new DateTime? SUIT_DATE { get { return base.SUIT_DATE.Value; } set { base.SUIT_DATE.Value = value; } }
        /// <summary>
        /// This is the date the judgment was entered. If this field is populated, JDGMNT_AMT must be non-zero.
        /// </summary>
        public new DateTime? JUDGMENT_DATE { get { return base.JUDGMENT_DATE.Value; } set { base.JUDGMENT_DATE.Value = value; } }
        /// <summary>
        /// This is the dollar amount awarded in the judgment. If this field is populated, JDGMNT_DATE must contain a valid date.
        /// </summary>
        public new decimal? JUDGMENT_AMT { get { return base.JUDGMENT_AMT.Value; } set { base.JUDGMENT_AMT.Value = value; } }
        /// <summary>
        /// This is the principal amount of the judgment.
        /// </summary>
        public new decimal? JUDGMENT_PRIN { get { return base.JUDGMENT_PRIN.Value; } set { base.JUDGMENT_PRIN.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the interest due before the judgment was rendered.
        /// </summary>
        public new decimal? PREJ_INT { get { return base.PREJ_INT.Value; } set { base.PREJ_INT.Value = value; } }
        /// <summary>
        /// This is the sum of the costs to the sender to carry the suit forward, such as attorney fees.
        /// </summary>
        public new decimal? JUDGMENT_COSTS { get { return base.JUDGMENT_COSTS.Value; } set { base.JUDGMENT_COSTS.Value = value; } }
        /// <summary>
        /// This is the difference between what was requested in the suit and the judgment amount.
        /// </summary>
        public new decimal? ADJUSTMENT { get { return base.ADJUSTMENT.Value; } set { base.ADJUSTMENT.Value = value; } }
        /// <summary>
        /// This is the standard code for the sheriff's country.
        /// </summary>
        public new string SHER_CNTRY { get { return base.SHER_CNTRY.Value; } set { base.SHER_CNTRY.Value = value; } }
        /// <summary>
        /// This is name of the court or clerk.
        /// </summary>
        public new string CRT_NAME { get { return base.CRT_NAME.Value; } set { base.CRT_NAME.Value = value; } }
        /// <summary>
        /// This is the date when the suit judgment expires. It is usually between 7 and 12 years from the filing date.
        /// </summary>
        public new DateTime? JDGMNT_EXP_DATE { get { return base.JDGMNT_EXP_DATE.Value; } set { base.JDGMNT_EXP_DATE.Value = value; } }
        /// <summary>
        /// This is a free-form text field explaining the reason for filing suit.
        /// </summary>
        public new string SUIT_REASON { get { return base.SUIT_REASON.Value; } set { base.SUIT_REASON.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the primary debtor from the latest Record 02, Record 31 or Record 51.
        /// </summary>
        public new bool JUDGMENTONDEBTOR1 { get { return base.JUDGMENTONDEBTOR1.Value; } set { base.JUDGMENTONDEBTOR1.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 2nd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR2 { get { return base.JUDGMENTONDEBTOR2.Value; } set { base.JUDGMENTONDEBTOR2.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 3rd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR3 { get { return base.JUDGMENTONDEBTOR3.Value; } set { base.JUDGMENTONDEBTOR3.Value = value; } }
        /// <summary>
        /// This is the date the judgment was recorded; this may be different than JDGMNT_DATE.
        /// </summary>
        public new DateTime? JUDGMENT_RECORDED_DATE { get { return base.JUDGMENT_RECORDED_DATE.Value; } set { base.JUDGMENT_RECORDED_DATE.Value = value; } }
        /// <summary>
        /// This is the date that the suit documents were generated and sent to court, as opposed to the date the suit was filed by the court.
        /// </summary>
        public new DateTime? SUIT_ISSUED_DATE { get { return base.SUIT_ISSUED_DATE.Value; } set { base.SUIT_ISSUED_DATE.Value = value; } }
        #endregion

        public RecordType07() : base() { }
        public RecordType07(string RT07Entry) : base(RT07Entry) { }

        public override Type GetType() { return typeof(RecordType07); }
    }
    #endregion

    #region Record Type 08
    /// <summary>
    /// Record Type 08 - Caption - Legal Names (Sender to Receiver)
    /// <para>This record is for entering the caption; i.e., the parties named in the suit. The Plaintiffs are typically the original creditors and the Defendants are the debtors. Value = 08.</para>
    /// </summary>
    public class RecordType08 : RecordTypes.YGC.RecordType08
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the combination of all plaintiff fields
        /// <para>(DO NOT USE - This field will not write to output file)</para>
        /// </summary>
        public new string PLAINTIFF_COMBINED { get { return base.PLAINTIFF_COMBINED.Value; } set { base.PLAINTIFF_COMBINED.Value = value; } }
        /// <summary>
        /// This is the first plaintiff, typically the debt owner or original creditor. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_1 { get { return base.PLAINTIFF_1.Value; } set { base.PLAINTIFF_1.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the second plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_2 { get { return base.PLAINTIFF_2.Value; } set { base.PLAINTIFF_2.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the third plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_3 { get { return base.PLAINTIFF_3.Value; } set { base.PLAINTIFF_3.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the fourth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_4 { get { return base.PLAINTIFF_4.Value; } set { base.PLAINTIFF_4.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the fifth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_5 { get { return base.PLAINTIFF_5.Value; } set { base.PLAINTIFF_5.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the sixth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_6 { get { return base.PLAINTIFF_6.Value; } set { base.PLAINTIFF_6.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the seventh plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_7 { get { return base.PLAINTIFF_7.Value; } set { base.PLAINTIFF_7.Value = value; } }
        /// <summary>
        /// This is the full name of the first defendant, typically the debtor. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_1 { get { return base.DEFENDANT_1.Value; } set { base.DEFENDANT_1.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the second defendant. It can also be an alias of the primary defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_2 { get { return base.DEFENDANT_2.Value; } set { base.DEFENDANT_2.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the third defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_3 { get { return base.DEFENDANT_3.Value; } set { base.DEFENDANT_3.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the fourth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_4 { get { return base.DEFENDANT_4.Value; } set { base.DEFENDANT_4.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the fifth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_5 { get { return base.DEFENDANT_5.Value; } set { base.DEFENDANT_5.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the sixth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_6 { get { return base.DEFENDANT_6.Value; } set { base.DEFENDANT_6.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the seventh defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_7 { get { return base.DEFENDANT_7.Value; } set { base.DEFENDANT_7.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the eighth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_8 { get { return base.DEFENDANT_8.Value; } set { base.DEFENDANT_8.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the ninth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_9 { get { return base.DEFENDANT_9.Value; } set { base.DEFENDANT_9.Value = value; } }
        #endregion

        public RecordType08() : base() { }
        public RecordType08(string RT08Entry) : base(RT08Entry) { }

        public override Type GetType() { return typeof(RecordType08); }
    }
    #endregion

    #region Record Type 09
    /// <summary>
    /// Record Type 09 - Message (Sender to Receiver)
    /// <para>Senders communicate status updates to their receivers with this record type. It should include the PCODE to clearly identify the update.</para>
    /// <para>Examples of status updates are direct payment, suit filed, account refused, judgment issued and account closed. Value = 09.</para>
    /// </summary>
    public class RecordType09 : RecordTypes.YGC.RecordType09
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the date on which the event reported occurred.
        /// </summary>
        public new DateTime? PDATE { get { return base.PDATE.Value; } set { base.PDATE.Value = value; } }
        /// <summary>
        /// This field indicates the type of status update. Some codes trigger changes to the account in YouveGotReports. See Status Codes tab for details; YGC highly recommends using a PCODE.
        /// </summary>
        public new string PCODE { get { return base.PCODE.Value; } set { base.PCODE.Value = value; } }
        /// <summary>
        /// This is a free-text comment or description to accompany the message. It should have a CRLF after the last character, which therefore means you don't have to right-pad the field with spaces to fill all 1024 characters.
        /// </summary>
        public new string PCMT { get { return base.PCMT.Value; } set { base.PCMT.Value = value; } }
        /// <summary>
        /// Note 2
        /// </summary>
        public new string NOTE02 { get { return base.NOTE02.Value; } set { base.NOTE02.Value = value; } }
        /// <summary>
        /// Note 3
        /// </summary>
        public new string NOTE03 { get { return base.NOTE03.Value; } set { base.NOTE03.Value = value; } }
        /// <summary>
        /// Note 4
        /// </summary>
        public new string NOTE04 { get { return base.NOTE04.Value; } set { base.NOTE04.Value = value; } }
        /// <summary>
        /// Note 5
        /// </summary>
        public new string NOTE05 { get { return base.NOTE05.Value; } set { base.NOTE05.Value = value; } }
        /// <summary>
        /// Note 6
        /// </summary>
        public new string NOTE06 { get { return base.NOTE06.Value; } set { base.NOTE06.Value = value; } }
        /// <summary>
        /// Note 7
        /// </summary>
        public new string NOTE07 { get { return base.NOTE07.Value; } set { base.NOTE07.Value = value; } }
        /// <summary>
        /// Note 8
        /// </summary>
        public new string NOTE08 { get { return base.NOTE08.Value; } set { base.NOTE08.Value = value; } }
        /// <summary>
        /// Note 9
        /// </summary>
        public new string NOTE09 { get { return base.NOTE09.Value; } set { base.NOTE09.Value = value; } }
        /// <summary>
        /// Note 10
        /// </summary>
        public new string NOTE10 { get { return base.NOTE10.Value; } set { base.NOTE10.Value = value; } }
        /// <summary>
        /// Note 11
        /// </summary>
        public new string NOTE11 { get { return base.NOTE11.Value; } set { base.NOTE11.Value = value; } }
        /// <summary>
        /// Note 12
        /// </summary>
        public new string NOTE12 { get { return base.NOTE12.Value; } set { base.NOTE12.Value = value; } }
        /// <summary>
        /// Note 13
        /// </summary>
        public new string NOTE13 { get { return base.NOTE13.Value; } set { base.NOTE13.Value = value; } }
        /// <summary>
        /// Note 14
        /// </summary>
        public new string NOTE14 { get { return base.NOTE14.Value; } set { base.NOTE14.Value = value; } }
        /// <summary>
        /// Note 15
        /// </summary>
        public new string NOTE15 { get { return base.NOTE15.Value; } set { base.NOTE15.Value = value; } }
        /// <summary>
        /// Note 16
        /// </summary>
        public new string NOTE16 { get { return base.NOTE16.Value; } set { base.NOTE16.Value = value; } }
        /// <summary>
        /// Note 17
        /// </summary>
        public new string NOTE17 { get { return base.NOTE17.Value; } set { base.NOTE17.Value = value; } }
        /// <summary>
        /// This is the time on which the event reported occurred. The format is HHMMSS on a 24-hour clock.
        /// </summary>
        public new DateTime? PTIME { get { return base.PTIME.Value; } set { base.PTIME.Value = value; } }
        /// <summary>
        /// Local time zone of the sender delivering the status update.Must be expressed in GMT format. For example:
        /// <para>GMT-5 = USA Eastern</para>
        /// <para>GMT-6 = USA Central</para>
        /// <para>GMT-7 = USA Mountain</para>
        /// <para>GMT-8 = USA Pacific</para>
        /// <para>GMT-9 = Alaska</para>
        /// <para>GMT-10 = Hawaii</para>
        /// </summary>
        public new string PTIME_ZONE { get { return base.PTIME_ZONE.Value; } set { base.PTIME_ZONE.Value = value; } }
        /// <summary>
        /// Phone number dialed to contact the debtor.
        /// </summary>
        public new string PHONE_NUMBER { get { return base.PHONE_NUMBER.Value; } set { base.PHONE_NUMBER.Value = value; } }
        /// <summary>
        /// Call Direction
        /// <para>Values: I/O. Incoming or outgoing.</para>
        /// </summary>
        public new CallDirection CALL_DIRECTION { get { return base.CALL_DIRECTION.Value; } set { base.CALL_DIRECTION.Value = value; } }
        /// <summary>
        /// Debtor
        /// <para>Values: 1 = Debtor 1, 2 = Debtor 2.</para>
        /// </summary>
        public new int? DBTR_TYPE { get { return base.DBTR_TYPE.Value; } set { base.DBTR_TYPE.Value = value; } }
        #endregion

        public RecordType09()
            : base() { }
        public RecordType09(string RT09Entry)
            : base(RT09Entry) { }

        public override Type GetType() { return typeof(RecordType09); }
    }
    #endregion

    #region Record Type 10
    /// <summary>
    /// Record Type 10 - Itemized Financial Transaction (Sender to Receiver)
    /// </summary>
    public class RecordType10 : RecordTypes.YGC.RecordType10
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// TRANSACTION DATE
        /// </summary>
        public new DateTime? TA_DATE { get { return base.TA_DATE.Value; } set { base.TA_DATE.Value = value; } }
        /// <summary>
        /// TRANSACTION CODE
        /// </summary>
        public new TransactionCode TA_CODE { get { return base.TA_CODE.Value; } set { base.TA_CODE.Value = value; } }
        /// <summary>
        /// Check/TA #
        /// </summary>
        public new int? TA_NUMBER { get { return base.TA_NUMBER.Value; } set { base.TA_NUMBER.Value = value; } }
        /// <summary>
        /// TRANSACTION COMMENT
        /// </summary>
        public new string TA_CMT { get { return base.TA_CMT.Value; } set { base.TA_CMT.Value = value; } }
        /// <summary>
        /// AMOUNT OF TRANSACTION
        /// </summary>
        public new decimal? TA_AMT { get { return base.TA_AMT.Value; } set { base.TA_AMT.Value = value; } }
        /// <summary>
        /// N/A - Not Converted
        /// </summary>
        public new decimal? LINE1_1 { get { return base.LINE1_1.Value; } set { base.LINE1_1.Value = value; } }
        /// <summary>
        /// Costs Recovered
        /// <para>LINE1_2</para>
        /// </summary>
        public new decimal? COSTS { get { return base.COSTS.Value; } set { base.COSTS.Value = value; } }
        /// <summary>
        /// Stat Fee Recovered
        /// <para>LINE1_3</para>
        /// </summary>
        public new decimal? STAT_FEE { get { return base.STAT_FEE.Value; } set { base.STAT_FEE.Value = value; } }
        /// <summary>
        /// Net to Client (No Costs)
        /// <para>LINE1_4</para>
        /// </summary>
        public new decimal? NET_CLIENT { get { return base.NET_CLIENT.Value; } set { base.NET_CLIENT.Value = value; } }
        /// <summary>
        /// Debtor Balance
        /// <para>LINE1_5</para>
        /// </summary>
        public new decimal? BALANCE { get { return base.BALANCE.Value; } set { base.BALANCE.Value = value; } }
        /// <summary>
        /// Amount Received
        /// <para>LINE1_6</para>
        /// </summary>
        public new decimal? RECEIVED { get { return base.RECEIVED.Value; } set { base.RECEIVED.Value = value; } }
        /// <summary>
        /// Principal Collected
        /// <para>LINE2_1</para>
        /// </summary>
        public new decimal? PRINCIPAL { get { return base.PRINCIPAL.Value; } set { base.PRINCIPAL.Value = value; } }
        /// <summary>
        /// Interest Collected
        /// <para>LINE2_2</para>
        /// </summary>
        public new decimal? INTEREST { get { return base.INTEREST.Value; } set { base.INTEREST.Value = value; } }
        /// <summary>
        /// Commulative Collected
        /// <para>LINE2_3</para>
        /// </summary>
        public new decimal? LINE2_3 { get { return base.LINE2_3.Value; } set { base.LINE2_3.Value = value; } }
        /// <summary>
        /// Costs Returned
        /// <para>LINE2_4</para>
        /// </summary>
        public new decimal? COSTS_RETURNED { get { return base.COSTS_RETURNED.Value; } set { base.COSTS_RETURNED.Value = value; } }
        /// <summary>
        /// Costs Expended
        /// <para>LINE2_5</para>
        /// </summary>
        public new decimal? COSTS_EXPENDED { get { return base.COSTS_EXPENDED.Value; } set { base.COSTS_EXPENDED.Value = value; } }
        /// <summary>
        /// Costs Received from Client
        /// <para>LINE2_6</para>
        /// </summary>
        public new decimal? COSTS_RECEIVED { get { return base.COSTS_RECEIVED.Value; } set { base.COSTS_RECEIVED.Value = value; } }
        /// <summary>
        /// Suit Fees Received
        /// <para>LINE2_7</para>
        /// </summary>
        public new decimal? SUIT_FEE { get { return base.SUIT_FEE.Value; } set { base.SUIT_FEE.Value = value; } }
        /// <summary>
        /// Commissions Received
        /// <para>LINE2_8</para>
        /// </summary>
        public new decimal? COMMISSION { get { return base.COMMISSION.Value; } set { base.COMMISSION.Value = value; } }
        /// <summary>
        /// "B"efore "P"ost Suit after "J"udg
        /// </summary>
        public new Disposition BPJ { get { return base.BPJ.Value; } set { base.BPJ.Value = value; } }
        /// <summary>
        /// Amount Advanced out of Pocket
        /// </summary>
        public new decimal? ADJUST { get { return base.ADJUST.Value; } set { base.ADJUST.Value = value; } }
        /// <summary>
        /// Bill debtor for this Transaction
        /// </summary>
        public new string BILL { get { return base.BILL.Value; } set { base.BILL.Value = value; } }
        /// <summary>
        /// Total Accrued Interest 
        /// </summary>
        public new decimal? INT { get { return base.INT.Value; } set { base.INT.Value = value; } }
        /// <summary>
        /// Cost Balance as of This TA 
        /// </summary>
        public new decimal? COST_BAL { get { return base.COST_BAL.Value; } set { base.COST_BAL.Value = value; } }
        /// <summary>
        /// Amount Applied to Coll &amp; Hold
        /// </summary>
        public new decimal? COLL_HOLD { get { return base.COLL_HOLD.Value; } set { base.COLL_HOLD.Value = value; } }
        /// <summary>
        /// Amount Applied to Co-Co Fees
        /// </summary>
        public new decimal? CO_FEES { get { return base.CO_FEES.Value; } set { base.CO_FEES.Value = value; } }
        /// <summary>
        /// Amount Applied to Merchandise
        /// </summary>
        public new decimal? MERCHANDICE { get { return base.MERCHANDICE.Value; } set { base.MERCHANDICE.Value = value; } }
        /// <summary>
        /// Amount Applied to Tax Rebate
        /// </summary>
        public new decimal? TAX_REBATE { get { return base.TAX_REBATE.Value; } set { base.TAX_REBATE.Value = value; } }
        /// <summary>
        /// N/A - Ignored - Use for 3rd Party
        /// </summary>
        public new decimal? TAMT1 { get { return base.TAMT1.Value; } set { base.TAMT1.Value = value; } }
        #endregion

        public RecordType10() : base() { }
        public RecordType10(string RT10Entry) : base(RT10Entry) { }

        public override Type GetType() { return typeof(RecordType10); }
    }
    #endregion

    #region Record Type 14
    /// <summary>
    /// Record Type 14 - Block Infinity
    /// </summary>
    public class RecordType14 : RecordTypes.YGC.RecordType14
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Block Infinity Code
        /// </summary>
        public new string INF_CODE { get { return base.INF_CODE.Value; } set { base.INF_CODE.Value = value; } }
        /// <summary>
        /// Block Infinity Data
        /// </summary>
        public new string INF_DATA { get { return base.INF_DATA.Value; } set { base.INF_DATA.Value = value; } }
        #endregion

        public RecordType14() : base() { }
        public RecordType14(string RT14Entry) : base(RT14Entry) { }

        public override Type GetType() { return typeof(RecordType14); }
    }
    #endregion

    #region Record Type 19
    /// <summary>
    /// Record Type 19 - Bankruptcy (Sender to Receiver)
    /// </summary>
    public class RecordType19 : RecordTypes.YGC.RecordType19
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This identifies which debtor in the account is filing for bankruptcy.
        /// <para></para>
        /// <para>1 = Primary debtor</para>
        /// <para>2 = Second debtor</para>
        /// <para>3 = Third debtor</para>
        /// <para></para>
        /// <para>The primary debtor is determined by the latest record 02 or 31 and the second and third debtors are determined by the latest record 03 or 33.<para>
        /// </summary>
        public new int? DBTR_NUM { get { return base.DBTR_NUM.Value; } set { base.DBTR_NUM.Value = value; } }
        /// <summary>
        /// Chapter within the bankruptcy code; typically 7 or 13 for individuals.
        /// </summary>
        public new int? CHAPTER { get { return base.CHAPTER.Value; } set { base.CHAPTER.Value = value; } }
        /// <summary>
        /// This is the court-issued case number for the bankruptcy.
        /// </summary>
        public new string BK_FILENO { get { return base.BK_FILENO.Value; } set { base.BK_FILENO.Value = value; } }
        /// <summary>
        /// This is a non-restrictive field describing where the bankruptcy was filed. You can use this for the jurisdiction of the court.
        /// </summary>
        public new string LOC { get { return base.LOC.Value; } set { base.LOC.Value = value; } }
        /// <summary>
        /// This is the date the bankruptcy was filed.
        /// </summary>
        public new DateTime? FILED_DATE { get { return base.FILED_DATE.Value; } set { base.FILED_DATE.Value = value; } }
        /// <summary>
        /// If something was handled incorrectly, the bankruptcy may be dismissed; this is the date of dismissal.
        /// </summary>
        public new DateTime? DSMIS_DATE { get { return base.DSMIS_DATE.Value; } set { base.DSMIS_DATE.Value = value; } }
        /// <summary>
        /// The date a discharge is issued for the debtor. The discharge relieves the debtor of personal liability outside of what is put up as collateral.
        /// </summary>
        public new DateTime? DSCHG_DATE { get { return base.DSCHG_DATE.Value; } set { base.DSCHG_DATE.Value = value; } }
        /// <summary>
        /// This is the date the court actually adjudicates the case.
        /// </summary>
        public new DateTime? CLOSE_DATE { get { return base.CLOSE_DATE.Value; } set { base.CLOSE_DATE.Value = value; } }
        /// <summary>
        /// This is the date the bankruptcy is converted from one chapter to another, usually from 13 to 7.
        /// </summary>
        public new DateTime? CNVRT_DATE { get { return base.CNVRT_DATE.Value; } set { base.CNVRT_DATE.Value = value; } }
        /// <summary>
        /// This is the date that the initial meeting takes place between creditors and debtors, usually 30 days after FILED_DATE.
        /// </summary>
        public new DateTime? MTG_341_DATE { get { return base.MTG_341_DATE.Value; } set { base.MTG_341_DATE.Value = value; } }
        /// <summary>
        /// This is the time of day that the initial meeting takes place between creditors and debtors.  It should be in one of the following formats:
        /// <para></para>
        /// <para>HH:MM:SS if using 24-hour notation</para>
        /// <para>HH:MM AM if using 12-hour notation for before noon</para>
        /// <para>HH:MM PM if using 12-hour notation for after noon</para>
        /// </summary>
        public new DateTime? MTG_341_TIME { get { return base.MTG_341_TIME.Value; } set { base.MTG_341_TIME.Value = value; } }
        /// <summary>
        /// This is the location that the initial meeting takes place between creditors and debtors. There is no format restriction.
        /// </summary>
        public new string MTG_341_LOC { get { return base.MTG_341_LOC.Value; } set { base.MTG_341_LOC.Value = value; } }
        /// <summary>
        /// These are the initials of the name of the bankruptcy judge.
        /// </summary>
        public new string JUDGE_INIT { get { return base.JUDGE_INIT.Value; } set { base.JUDGE_INIT.Value = value; } }
        /// <summary>
        /// The debtor may choose to re-affirm the debt that would have been discharged by the bankruptcy. This is the amount the debtor agrees to pay in full; the re-affirmation survives the bankruptcy.
        /// </summary>
        public new decimal? REAF_AMT { get { return base.REAF_AMT.Value; } set { base.REAF_AMT.Value = value; } }
        /// <summary>
        /// This is the date when the re-affirmation was signed.
        /// </summary>
        public new DateTime? REAF_DATE { get { return base.REAF_DATE.Value; } set { base.REAF_DATE.Value = value; } }
        /// <summary>
        /// This is the periodic payment in a Chapter 11 or 13 bankruptcy or a lump sum payment of a Chapter 7 bankruptcy.
        /// </summary>
        public new decimal? PAY_AMT { get { return base.PAY_AMT.Value; } set { base.PAY_AMT.Value = value; } }
        /// <summary>
        /// This is the date that the payment arrangement was agreed upon.
        /// </summary>
        public new DateTime? PAY_DATE { get { return base.PAY_DATE.Value; } set { base.PAY_DATE.Value = value; } }
        /// <summary>
        /// This is the date the judge approves the payment plan.
        /// </summary>
        public new DateTime? CONF_DATE { get { return base.CONF_DATE.Value; } set { base.CONF_DATE.Value = value; } }
        /// <summary>
        /// This is the date the debtor caught up with paying all arrearages, fees and interest.
        /// </summary>
        public new DateTime? CURE_DATE { get { return base.CURE_DATE.Value; } set { base.CURE_DATE.Value = value; } }
        /// <summary>
        /// If the bankruptcy proceedings were put on hold, this is the date the stay was lifted.
        /// </summary>
        public new DateTime? STAY_LIFTED_DATE { get { return base.STAY_LIFTED_DATE.Value; } set { base.STAY_LIFTED_DATE.Value = value; } }
        #endregion

        public RecordType19() : base() { }
        public RecordType19(string RT19Entry) : base(RT19Entry) { }

        public override Type GetType() { return typeof(RecordType19); }
    }
    #endregion

    #region Record Type 24
    /// <summary>
    /// Record Type 24 - Historical Financial Transaction (Sender to Receiver)						
    /// <para>This record reports a financial transaction made before the account is sent to the receiver. Multiple records can be sent to record the transaction history of the same account. The receiver can compile all of these records through their collection system into one statement as evidence. Value = 24.</para>
    /// </summary>
    public class RecordType24 : RecordTypes.YGC.RecordType24
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the date the transaction was received and posted into the sender's system.
        /// </summary>
        public new DateTime? POST_D { get { return base.POST_D.Value; } set { base.POST_D.Value = value; } }
        /// <summary>
        /// This identifies the type or payment or cost. Use an Accounting or Disbursement YGC Status Code (see the Status Codes tab for a complete list).
        /// </summary>
        public new string TRANS_CD { get { return base.TRANS_CD.Value; } set { base.TRANS_CD.Value = value; } }
        /// <summary>
        /// Unique ID/reference number for this transaction.
        /// </summary>
        public new int? TRANS_NUM { get { return base.TRANS_NUM.Value; } set { base.TRANS_NUM.Value = value; } }
        /// <summary>
        /// Typically, this is the total amount of the payment to the sender, which may not include Statutory Attorney Fees awarded in some states.
        /// </summary>
        public new decimal? TOTAL_COLL { get { return base.TOTAL_COLL.Value; } set { base.TOTAL_COLL.Value = value; } }
        /// <summary>
        /// This is the amount of principal collected on the payment.
        /// </summary>
        public new decimal? PRIN_COLL { get { return base.PRIN_COLL.Value; } set { base.PRIN_COLL.Value = value; } }
        /// <summary>
        /// This is the amount of interest collected on the payment.
        /// </summary>
        public new decimal? INT_COLL { get { return base.INT_COLL.Value; } set { base.INT_COLL.Value = value; } }
        /// <summary>
        /// This is the amount of costs recovered on the transaction.
        /// </summary>
        public new decimal? COST_COLL { get { return base.COST_COLL.Value; } set { base.COST_COLL.Value = value; } }
        /// <summary>
        /// This is the amount of statutory attorney fees recovered in this payment. Typically these are state awarded fees that do not get remitted on.
        /// </summary>
        public new decimal? STATU_COLL { get { return base.STATU_COLL.Value; } set { base.STATU_COLL.Value = value; } }
        /// <summary>
        /// This is the commission earned on this transaction.
        /// </summary>
        public new decimal? COMM { get { return base.COMM.Value; } set { base.COMM.Value = value; } }
        /// <summary>
        /// This is the debtor balance that existed as of the posting of this transaction.
        /// </summary>
        public new decimal? DBTR_BAL { get { return base.DBTR_BAL.Value; } set { base.DBTR_BAL.Value = value; } }
        /// <summary>
        /// This field is not applicable for payments. It is only used to report a cost disbursement.
        /// </summary>
        public new decimal? COST_EXP { get { return base.COST_EXP.Value; } set { base.COST_EXP.Value = value; } }
        /// <summary>
        /// This is a description of the transaction, such as Payment, Suit Filed, Audit, Collection, or Court Costs.
        /// </summary>
        public new string DESC { get { return base.DESC.Value; } set { base.DESC.Value = value; } }
        /// <summary>
        /// This is a specific description of the transaction. For payments, it will typically include the check number.
        /// </summary>
        public new string CMT { get { return base.CMT.Value; } set { base.CMT.Value = value; } }
        #endregion

        public RecordType24() : base() { }
        public RecordType24(string RT24Entry) : base(RT24Entry) { }

        public override Type GetType() { return typeof(RecordType24); }
    }
    #endregion

    #region Record Type 25
    /// <summary>
    /// Record Type 25 - Service Information (Sender to Receiver)											
    /// </summary>
    public class RecordType25 : RecordTypes.YGC.RecordType25
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the serving individual's first name.
        /// </summary>
        public new string SERVER_FIRSTNAME { get { return base.SERVER_FIRSTNAME.Value; } set { base.SERVER_FIRSTNAME.Value = value; } }
        /// <summary>
        /// This is the serving individual's middle name or initial.
        /// </summary>
        public new string SERVER_MIDDLENAME { get { return base.SERVER_MIDDLENAME.Value; } set { base.SERVER_MIDDLENAME.Value = value; } }
        /// <summary>
        /// This is the serving individual's last name.
        /// </summary>
        public new string SERVER_LASTNAME { get { return base.SERVER_LASTNAME.Value; } set { base.SERVER_LASTNAME.Value = value; } }
        /// <summary>
        /// This is the name of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_COMPANY { get { return base.SERVER_COMPANY.Value; } set { base.SERVER_COMPANY.Value = value; } }
        /// <summary>
        /// This is the address line 1 of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_ADDRESS1 { get { return base.SERVER_ADDRESS1.Value; } set { base.SERVER_ADDRESS1.Value = value; } }
        /// <summary>
        /// This is the address line 2 of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_ADDRESS2 { get { return base.SERVER_ADDRESS2.Value; } set { base.SERVER_ADDRESS2.Value = value; } }
        /// <summary>
        /// This is the city of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_CITY { get { return base.SERVER_CITY.Value; } set { base.SERVER_CITY.Value = value; } }
        /// <summary>
        /// This is the state of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_STATE { get { return base.SERVER_STATE.Value; } set { base.SERVER_STATE.Value = value; } }
        /// <summary>
        /// This is the postal code of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_ZIP { get { return base.SERVER_ZIP.Value; } set { base.SERVER_ZIP.Value = value; } }
        /// <summary>
        /// This is the country of the company that employs server specified in ServerFirstName and ServerLastName.
        /// </summary>
        public new string SERVER_COUNTRY { get { return base.SERVER_COUNTRY.Value; } set { base.SERVER_COUNTRY.Value = value; } }
        /// <summary>
        /// Server reference, work order, or job number.
        /// </summary>
        public new string SERVER_REFERENCENUMBER { get { return base.SERVER_REFERENCENUMBER.Value; } set { base.SERVER_REFERENCENUMBER.Value = value; } }
        /// <summary>
        /// Cost of the service.
        /// </summary>
        public new decimal? COSTOFSERVICE { get { return base.COSTOFSERVICE.Value; } set { base.COSTOFSERVICE.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to three Service records.  If this record for the primary debtor then this value will be 1, if this record for the co-signer then this value will be 2, etc.  If a record is sent with the same DebtorNumberServed as a previous record for the same account, the second record will overwrite the first.
        /// </summary>
        public new int? DEBTORNUMBERSERVED { get { return base.DEBTORNUMBERSERVED.Value; } set { base.DEBTORNUMBERSERVED.Value = value; } }
        /// <summary>
        /// This is the date & time that service was executed
        /// </summary>
        public new DateTime? SERVED_DATETIME { get { return base.SERVED_DATETIME.Value; } set { base.SERVED_DATETIME.Value = value; } }
        /// <summary>
        /// This is the method used to serve the papers. The valid codes are:
        /// <para>PER = Personal</para>
        /// <para>CER = Certified Mail</para>
        /// <para>SUB = Sub-service</para>
        /// <para>POS = Posting (left at front door)</para>
        /// <para>FIR = First Class Mail</para>
        /// </summary>
        public new ServiceType SERVICE_TYPE { get { return base.SERVICE_TYPE.Value; } set { base.SERVICE_TYPE.Value = value; } }
        /// <summary>
        /// This is the address where the service was completed.  Field may be the same as D1_Street or D2_Street.
        /// </summary>
        public new string SERVED_ADDRESS1 { get { return base.SERVED_ADDRESS1.Value; } set { base.SERVED_ADDRESS1.Value = value; } }
        /// <summary>
        /// This is the address where the service was completed.  Field may be the same as D1_Street or D2_Street.
        /// </summary>
        public new string SERVED_ADDERSS2 { get { return base.SERVED_ADDERSS2.Value; } set { base.SERVED_ADDERSS2.Value = value; } }
        /// <summary>
        /// This is the city where the service was completed.  Field may be the same as D1_CITY or D2_CITY.
        /// </summary>
        public new string SERVED_CITY { get { return base.SERVED_CITY.Value; } set { base.SERVED_CITY.Value = value; } }
        /// <summary>
        /// This is the county the ServedAtCity is located in.
        /// </summary>
        public new string SERVED_COUNTY { get { return base.SERVED_COUNTY.Value; } set { base.SERVED_COUNTY.Value = value; } }
        /// <summary>
        /// This is the state where the service was completed.  Field may be the same as D1_STATE or D2_STATE.
        /// </summary>
        public new string SERVED_STATE { get { return base.SERVED_STATE.Value; } set { base.SERVED_STATE.Value = value; } }
        /// <summary>
        /// This is the postal code where the service was completed.  Field may be the same as D1_ZIP or D2_ZIP.
        /// </summary>
        public new string SERVED_ZIP { get { return base.SERVED_ZIP.Value; } set { base.SERVED_ZIP.Value = value; } }
        /// <summary>
        /// This is the standard code for the Country where service was completed.
        /// </summary>
        public new string SERVED_COUNTRY { get { return base.SERVED_COUNTRY.Value; } set { base.SERVED_COUNTRY.Value = value; } }
        /// <summary>
        /// Degrees, Minutes and Seconds (DDD MM SS.S) or Decimal Degrees (DDD.DDDDD) or Degrees and Decimal Minutes (DD MM.MMM).
        /// </summary>
        public new string SERVED_LATITUDE_FORMAT { get { return base.SERVED_LATITUDE_FORMAT.Value; } set { base.SERVED_LATITUDE_FORMAT.Value = value; } }
        /// <summary>
        /// This is the Latitude information where service was completed.
        /// </summary>
        public new string SERVED_LATITUDE { get { return base.SERVED_LATITUDE.Value; } set { base.SERVED_LATITUDE.Value = value; } }
        /// <summary>
        /// Degrees, Minutes and Seconds (DDD MM SS.S) or Decimal Degrees (DDD.DDDDD) or Degrees and Decimal Minutes (DD MM.MMM).
        /// </summary>
        public new string SERVED_LONGITUDE_FORMAT { get { return base.SERVED_LONGITUDE_FORMAT.Value; } set { base.SERVED_LONGITUDE_FORMAT.Value = value; } }
        /// <summary>
        /// This is the Longitude information where service was completed.
        /// </summary>
        public new string SERVED_LONGITUDE { get { return base.SERVED_LONGITUDE.Value; } set { base.SERVED_LONGITUDE.Value = value; } }
        /// <summary>
        /// This is the first name of the individual who was served.
        /// </summary>
        public new string SERVED_FIRSTNAME { get { return base.SERVED_FIRSTNAME.Value; } set { base.SERVED_FIRSTNAME.Value = value; } }
        /// <summary>
        /// This is the middle name or initial of the individual who was served.
        /// </summary>
        public new string SERVED_MIDDLENAME { get { return base.SERVED_MIDDLENAME.Value; } set { base.SERVED_MIDDLENAME.Value = value; } }
        /// <summary>
        /// This is the last name of the individual who was served.
        /// </summary>
        public new string SERVED_LASTNAME { get { return base.SERVED_LASTNAME.Value; } set { base.SERVED_LASTNAME.Value = value; } }
        /// <summary>
        /// This is the descirption of the relationship to the debtor being served.
        /// </summary>
        public new string RELATIONSHIPTODEBTOR { get { return base.RELATIONSHIPTODEBTOR.Value; } set { base.RELATIONSHIPTODEBTOR.Value = value; } }
        /// <summary>
        /// This is the marital status of the debtor being served provided by person served (ServedToFirstName and ServedToLastName).
        /// </summary>
        public new string DEBTOR_MARITAL_STATUS { get { return base.DEBTOR_MARITAL_STATUS.Value; } set { base.DEBTOR_MARITAL_STATUS.Value = value; } }
        /// <summary>
        /// This is the military status of the debtor being served provided by by person served (ServedToFirstName and ServedToLastName).
        /// </summary>
        public new string DEBTOR_MILITARY_STATUS { get { return base.DEBTOR_MILITARY_STATUS.Value; } set { base.DEBTOR_MILITARY_STATUS.Value = value; } }
        /// <summary>
        /// This is the Sex type of the person serviced in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_SEX { get { return base.SERVED_PERSON_SEX.Value; } set { base.SERVED_PERSON_SEX.Value = value; } }
        /// <summary>
        /// This is the race of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_RACE { get { return base.SERVED_PERSON_RACE.Value; } set { base.SERVED_PERSON_RACE.Value = value; } }
        /// <summary>
        /// This is the age in years of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new int? SERVED_PERSON_AGE { get { return base.SERVED_PERSON_AGE.Value; } set { base.SERVED_PERSON_AGE.Value = value; } }
        /// <summary>
        /// This is the eye color of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_EYECOLOR { get { return base.SERVED_PERSON_EYECOLOR.Value; } set { base.SERVED_PERSON_EYECOLOR.Value = value; } }
        /// <summary>
        /// This is the hair color of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_HAIRCOLOR { get { return base.SERVED_PERSON_HAIRCOLOR.Value; } set { base.SERVED_PERSON_HAIRCOLOR.Value = value; } }
        /// <summary>
        /// This is the approximate height (in feet and inches) of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_HEIGHT { get { return base.SERVED_PERSON_HEIGHT.Value; } set { base.SERVED_PERSON_HEIGHT.Value = value; } }
        /// <summary>
        /// This is the approximate weight in pounds of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new int? SERVED_PERSON_WEIGHT { get { return base.SERVED_PERSON_WEIGHT.Value; } set { base.SERVED_PERSON_WEIGHT.Value = value; } }
        /// <summary>
        /// Identifying features of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_IDENTIFYINGFEATURES { get { return base.SERVED_PERSON_IDENTIFYINGFEATURES.Value; } set { base.SERVED_PERSON_IDENTIFYINGFEATURES.Value = value; } }
        /// <summary>
        /// This is the approximate build type of the person served in ServedToFirstName and ServedToLastName.
        /// </summary>
        public new string SERVED_PERSON_BUILD { get { return base.SERVED_PERSON_BUILD.Value; } set { base.SERVED_PERSON_BUILD.Value = value; } }
        #endregion

        public RecordType25() : base() { }
        public RecordType25(string RT25Entry) : base(RT25Entry) { }

        public override Type GetType() { return typeof(RecordType25); }
    }
    #endregion

    #region Record Type 30
    /// <summary>
    /// Record Type 30 - Payments to Client
    /// </summary>
    public class RecordType30 : RecordTypes.YGC.RecordType30
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// Financial Code 1-6    and   51-81
        /// </summary>
        public new int? RET_CODE { get { return base.RET_CODE.Value; } set { base.RET_CODE.Value = value; } }
        /// <summary>
        /// Transaction, Disbursement or Payment Release Date
        /// </summary>
        public new DateTime? PAY_DATE { get { return base.PAY_DATE.Value; } set { base.PAY_DATE.Value = value; } }
        /// <summary>
        /// Gross Payment Amount
        /// </summary>
        public new decimal? GROSS_PAY { get { return base.GROSS_PAY.Value; } set { base.GROSS_PAY.Value = value; } }
        /// <summary>
        /// Net Client
        /// </summary>
        public new decimal? NET_CLIENT { get { return base.NET_CLIENT.Value; } set { base.NET_CLIENT.Value = value; } }
        /// <summary>
        /// Check Amount to Client
        /// </summary>
        public new decimal? CHECK_AMT { get { return base.CHECK_AMT.Value; } set { base.CHECK_AMT.Value = value; } }
        /// <summary>
        /// Costs Returned
        /// </summary>
        public new decimal? COST_RET { get { return base.COST_RET.Value; } set { base.COST_RET.Value = value; } }
        /// <summary>
        /// Fees on Remittance
        /// </summary>
        public new decimal? FEES { get { return base.FEES.Value; } set { base.FEES.Value = value; } }
        /// <summary>
        /// Agent/Co-Counsel Fees
        /// </summary>
        public new decimal? AGENT_FEES { get { return base.AGENT_FEES.Value; } set { base.AGENT_FEES.Value = value; } }
        /// <summary>
        /// Forwarder Commissions
        /// </summary>
        public new decimal? FORW_CUT { get { return base.FORW_CUT.Value; } set { base.FORW_CUT.Value = value; } }
        /// <summary>
        /// Costs Recovered {May not have been returned}
        /// </summary>
        public new decimal? COST_REC { get { return base.COST_REC.Value; } set { base.COST_REC.Value = value; } }
        /// <summary>
        /// (B)efore Suit - (P)ost Suit - Post (J)udgment
        /// </summary>
        public new string BPJ { get { return base.BPJ.Value; } set { base.BPJ.Value = value; } }
        /// <summary>
        /// Transaction #
        /// </summary>
        public new int? TA_NO { get { return base.TA_NO.Value; } set { base.TA_NO.Value = value; } }
        /// <summary>
        /// Remittance Batch #
        /// </summary>
        public new int? RMIT_NO { get { return base.RMIT_NO.Value; } set { base.RMIT_NO.Value = value; } }
        /// <summary>
        /// Stat Fees Collected
        /// </summary>
        public new decimal? LINE1_3 { get { return base.LINE1_3.Value; } set { base.LINE1_3.Value = value; } }
        /// <summary>
        /// Debtor Balance - As of Posting
        /// </summary>
        public new decimal? LINE1_5 { get { return base.LINE1_5.Value; } set { base.LINE1_5.Value = value; } }
        /// <summary>
        /// Amount Received
        /// </summary>
        public new decimal? LINE1_6 { get { return base.LINE1_6.Value; } set { base.LINE1_6.Value = value; } }
        /// <summary>
        /// Principal Collected
        /// </summary>
        public new decimal? LINE2_1 { get { return base.LINE2_1.Value; } set { base.LINE2_1.Value = value; } }
        /// <summary>
        /// Interest Collected
        /// </summary>
        public new decimal? LINE2_2 { get { return base.LINE2_2.Value; } set { base.LINE2_2.Value = value; } }
        /// <summary>
        /// Costs Expended
        /// </summary>
        public new decimal? LINE2_5 { get { return base.LINE2_5.Value; } set { base.LINE2_5.Value = value; } }
        /// <summary>
        /// Costs Received (Special)
        /// </summary>
        public new decimal? LINE2_6 { get { return base.LINE2_6.Value; } set { base.LINE2_6.Value = value; } }
        /// <summary>
        /// Suit Fees
        /// </summary>
        public new decimal? LINE2_7 { get { return base.LINE2_7.Value; } set { base.LINE2_7.Value = value; } }
        /// <summary>
        /// Commissions
        /// </summary>
        public new decimal? LINE2_8 { get { return base.LINE2_8.Value; } set { base.LINE2_8.Value = value; } }
        /// <summary>
        /// Description of transaction
        /// </summary>
        public new string DESC { get { return base.DESC.Value; } set { base.DESC.Value = value; } }
        /// <summary>
        /// Date Transaction was Posted
        /// </summary>
        public new DateTime? POST_DATE { get { return base.POST_DATE.Value; } set { base.POST_DATE.Value = value; } }
        /// <summary>
        /// Date Transaction was Remitted
        /// </summary>
        public new DateTime? REMIT_DATE { get { return base.REMIT_DATE.Value; } set { base.REMIT_DATE.Value = value; } }
        /// <summary>
        /// Transaction Code (See **Below)
        /// </summary>
        public new string TA_CODE { get { return base.TA_CODE.Value; } set { base.TA_CODE.Value = value; } }
        /// <summary>
        /// "This provides a more specific description of this transaction. For payments, this can typically include the check number."
        /// </summary>
        public new string COMMENT { get { return base.COMMENT.Value; } set { base.COMMENT.Value = value; } }
        /// <summary>
        /// Only used in a payment reversal, it is the transaction number (TA_NO) of the original payment that is being reversed.
        /// </summary>
        public new int? ORIGINALTANUMBER { get { return base.ORIGINALTANUMBER.Value; } set { base.ORIGINALTANUMBER.Value = value; } }
        /// <summary>
        /// Only used in a payment reversal, it is the remit number (RMIT_NO) of the original payment that is being reversed.
        /// </summary>
        public new int? ORIGINALREMITNUMBER { get { return base.ORIGINALREMITNUMBER.Value; } set { base.ORIGINALREMITNUMBER.Value = value; } }
        /// <summary>
        /// Only used in a payment reversal, it is the remit date (REMIT_DATE) of the original payment that is being reversed.
        /// </summary>
        public new DateTime? ORIGINALREMITDATE { get { return base.ORIGINALREMITDATE.Value; } set { base.ORIGINALREMITDATE.Value = value; } }
        /// <summary>
        /// "Portion of costs expended (LINE2_5) that is recoverable from the debtor. This is only used for cost expended transactions."
        /// </summary>
        public new decimal? COSTSPENTRECOVERABLEFROMDEBTOR { get { return base.COSTSPENTRECOVERABLEFROMDEBTOR.Value; } set { base.COSTSPENTRECOVERABLEFROMDEBTOR.Value = value; } }
        /// <summary>
        /// "Portion of costs expended (LINE2_5) that is not recoverable from the debtor. This is only used for cost expended transactions."
        /// </summary>
        public new decimal? COSTSPENTNONRECOVERABLEFROMDEBTOR { get { return base.COSTSPENTNONRECOVERABLEFROMDEBTOR.Value; } set { base.COSTSPENTNONRECOVERABLEFROMDEBTOR.Value = value; } }
        /// <summary>
        /// Portion of costs expended (LINE2_5) that is billable to the client. This is only used for cost expended transactions.
        /// </summary>
        public new decimal? COSTSPENTRECOVERABLEFROMCLIENT { get { return base.COSTSPENTRECOVERABLEFROMCLIENT.Value; } set { base.COSTSPENTRECOVERABLEFROMCLIENT.Value = value; } }
        /// <summary>
        /// "Portion of costs expended (LINE2_5) that is not billable to the client. This is only used for cost expended transactions."
        /// </summary>
        public new decimal? COSTSPENTNONRECOVERABLEFROMCLIENT { get { return base.COSTSPENTNONRECOVERABLEFROMCLIENT.Value; } set { base.COSTSPENTNONRECOVERABLEFROMCLIENT.Value = value; } }
        /// <summary>
        /// "This is used to distinguish up to 3 Execution records. Values are 1, 2 or 3.      (Ex: This record for the primary debtor can have EXECUTE_NO = 1 and the this record for the co-signer can have EXECUTE_NO = 2.)      If this record is sent with the same Execute_NO as a previous record for the same account, the second record will overwrite the first.    "
        /// </summary>
        public new int? DEBTORNUMBER { get { return base.DEBTORNUMBER.Value; } set { base.DEBTORNUMBER.Value = value; } }
        #endregion

        public RecordType30() : base() { }
        public RecordType30(string RT30Entry) : base(RT30Entry) { }

        public override Type GetType() { return typeof(RecordType30); }
    }
    #endregion

    #region Record Type 31
    /// <summary>
    /// Record Type 31 - Primary Debtor Information (Receiver to Sender)
    /// </summary>
    public class RecordType31 : RecordTypes.YGC.RecordType31
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the primary debtor's name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_NAME { get { return base.D1_NAME.Value; } set { base.D1_NAME.Value = value; } }
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
        public new RecordTypes.YGC.Enums.Salutation D1_SALUT { get { return base.D1_SALUT.Value; } set { base.D1_SALUT.Value = value; } }
        /// <summary>
        /// This is the primary debtor's alias name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_ALIAS { get { return base.D1_ALIAS.Value; } set { base.D1_ALIAS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET { get { return base.D1_STREET.Value; } set { base.D1_STREET.Value = value; } }
        /// <summary>
        /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
        /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
        /// </para></summary>
        public new string D1_CS { get { return base.D1_CS.Value; } set { base.D1_CS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
        /// </summary>
        public new string D1_ZIP { get { return base.D1_ZIP.Value; } set { base.D1_ZIP.Value = value; } }
        /// <summary>
        /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_PHONE { get { return base.D1_PHONE.Value; } set { base.D1_PHONE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_FAX { get { return base.D1_FAX.Value; } set { base.D1_FAX.Value = value; } }
        /// <summary>
        /// This is the primary debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D1_SSN { get { return base.D1_SSN.Value; } set { base.D1_SSN.Value = value; } }
        /// <summary>
        /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
        /// </summary>
        public new string RFILE { get { return base.RFILE.Value; } set { base.RFILE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's date of birth.
        /// </summary>
        public new DateTime? D1_DOB { get { return base.D1_DOB.Value; } set { base.D1_DOB.Value = value; } }
        /// <summary>
        /// This is the primary debtor's driver's license number.
        /// </summary>
        public new string D1_DL { get { return base.D1_DL.Value; } set { base.D1_DL.Value = value; } }
        /// <summary>
        /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STATE { get { return base.D1_STATE.Value; } set { base.D1_STATE.Value = value; } }
        /// <summary>
        /// Set this field to Y if the served papers were returned.
        /// </summary>
        public new bool D1_MAIL { get { return base.D1_MAIL.Value; } set { base.D1_MAIL.Value = value; } }
        /// <summary>
        /// This is the date the suit was served.
        /// </summary>
        public new DateTime? SERVICE_D { get { return base.SERVICE_D.Value; } set { base.SERVICE_D.Value = value; } }
        /// <summary>
        /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
        /// </summary>
        public new DateTime? ANSWER_DUE_D { get { return base.ANSWER_DUE_D.Value; } set { base.ANSWER_DUE_D.Value = value; } }
        /// <summary>
        /// This is the date the debtor's response was filed.
        /// </summary>
        public new DateTime? ANSWER_FILE_D { get { return base.ANSWER_FILE_D.Value; } set { base.ANSWER_FILE_D.Value = value; } }
        /// <summary>
        /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
        /// </summary>
        public new DateTime? DEFAULT_D { get { return base.DEFAULT_D.Value; } set { base.DEFAULT_D.Value = value; } }
        /// <summary>
        /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
        /// </summary>
        public new DateTime? TRIAL_D { get { return base.TRIAL_D.Value; } set { base.TRIAL_D.Value = value; } }
        /// <summary>
        /// This is the date of the latest hearing on a motion filed by either party.
        /// </summary>
        public new DateTime? HEARING_D { get { return base.HEARING_D.Value; } set { base.HEARING_D.Value = value; } }
        /// <summary>
        /// This is the date a lien was filed against a debtor's property.
        /// </summary>
        public new DateTime? LIEN_D { get { return base.LIEN_D.Value; } set { base.LIEN_D.Value = value; } }
        /// <summary>
        /// This is the date garnishment against the debtor's wages was established.
        /// </summary>
        public new DateTime? GARN_D { get { return base.GARN_D.Value; } set { base.GARN_D.Value = value; } }
        ///<summary>
        ///This is the method used to serve the papers. 
        ///<para>The valid codes are: </para>
        ///<para>PER = Personal</para>
        ///<para>CER = Certified Mail</para>
        ///<para>SUB = Sub-service</para>
        ///<para>POS = Posting (left at front door)</para>
        ///<para>FIR = First Class Mail</para>
        ///</summary>
        public new string SERVICE_TYPE { get { return base.SERVICE_TYPE.Value; } set { base.SERVICE_TYPE.Value = value; } }
        /// <summary>
        /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2 { get { return base.D1_STREET2.Value; } set { base.D1_STREET2.Value = value; } }
        /// <summary>
        /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CITY { get { return base.D1_CITY.Value; } set { base.D1_CITY.Value = value; } }
        /// <summary>
        /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_CELL { get { return base.D1_CELL.Value; } set { base.D1_CELL.Value = value; } }
        /// <summary>
        /// Fair Isaac credit score
        /// </summary>
        public new int? SCORE_FICO { get { return base.SCORE_FICO.Value; } set { base.SCORE_FICO.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_COLLECT { get { return base.SCORE_COLLECT.Value; } set { base.SCORE_COLLECT.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_OTHER { get { return base.SCORE_OTHER.Value; } set { base.SCORE_OTHER.Value = value; } }
        /// <summary>
        /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CNTRY { get { return base.D1_CNTRY.Value; } set { base.D1_CNTRY.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET_LONG { get { return base.D1_STREET_LONG.Value; } set { base.D1_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2_LONG { get { return base.D1_STREET2_LONG.Value; } set { base.D1_STREET2_LONG.Value = value; } }
        /// <summary>
        /// Debtor First Name
        /// </summary>
        public new string FIRSTNAME { get { return base.FIRSTNAME.Value; } set { base.FIRSTNAME.Value = value; } }
        /// <summary>
        /// Debtor Last Name
        /// </summary>
        public new string LASTNAME { get { return base.LASTNAME.Value; } set { base.LASTNAME.Value = value; } }
        /// <summary>
        /// Creditor-Internal calculated score
        /// </summary>
        public new string SCOREINTERNAL { get { return base.SCOREINTERNAL.Value; } set { base.SCOREINTERNAL.Value = value; } }
        /// <summary>
        /// This field is the Placement or Internal Delinquency Stage of the account at the time of placement used by the the Creditor or Debt Buyer (User Defined).
        /// </summary>
        public new string STAGE { get { return base.STAGE.Value; } set { base.STAGE.Value = value; } }
        #endregion

        public RecordType31() : base() { }
        public RecordType31(string RT31Entry) : base(RT31Entry) { }

        public override Type GetType() { return typeof(RecordType31); }
    }
    #endregion

    #region Record Type 39
    /// <summary>
    /// Record Type 39 - Message (Receiver to Sender)
    /// </summary>
    public class RecordType39 : RecordTypes.YGC.RecordType39
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// Event Date
        /// </summary>
        public new DateTime? PDATE { get { return base.PDATE.Value; } set { base.PDATE.Value = value; } }
        /// <summary>
        /// Status Update Code
        /// </summary>
        public new string PCODE { get { return base.PCODE.Value; } set { base.PCODE.Value = value; } }
        /// <summary>
        /// Free Text Comment
        /// </summary>
        public new string PCMT { get { return base.PCMT.Value; } set { base.PCMT.Value = value; } }
        /// <summary>
        /// Time at which the event occurred
        /// <para>24-Hour Format</para>
        /// </summary>
        public new DateTime? PTIME { get { return base.PTIME.Value; } set { base.PTIME.Value = value; } }
        /// <summary>
        /// Local Time Zone
        /// </summary>
        public new RecordTypes.YGC.Enums.TimeZone PTIME_ZONE { get { return base.PTIME_ZONE.Value; } set { base.PTIME_ZONE.Value = value; } }
        /// <summary>
        /// Phone Number Dialed
        /// </summary>
        public new string PHONE_NUMBER { get { return base.PHONE_NUMBER.Value; } set { base.PHONE_NUMBER.Value = value; } }
        /// <summary>
        /// Call Direction
        /// </summary>
        public new RecordTypes.YGC.Enums.CallDirection CALL_DIRECTION { get { return base.CALL_DIRECTION.Value; } set { base.CALL_DIRECTION.Value = value; } }
        /// <summary>
        /// Debtor Number
        /// </summary>
        public new int? DBTR_TYPE { get { return base.DBTR_TYPE.Value; } set { base.DBTR_TYPE.Value = value; } }
        #endregion

        public RecordType39() : base() { }
        public RecordType39(string RT39Entry) : base(RT39Entry) { }

        public override Type GetType() { return typeof(RecordType39); }
    }
    #endregion

    #region Record Type 41
    /// <summary>
    /// Record Type 41 - Suit / Judgment Information
    /// </summary>
    public class RecordType41 : RecordTypes.YGC.RecordType41
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the dollar amount requested in the suit. If this field is populated, SUIT_DATE must contain a valid date.
        /// </summary>
        public new decimal? SUIT_AMT { get { return base.SUIT_AMT.Value; } set { base.SUIT_AMT.Value = value; } }
        /// <summary>
        /// This is the date the suit was filed. If this field is populated, SUIT_AMT must be non-zero.
        /// </summary>
        public new DateTime? SUIT_DATE { get { return base.SUIT_DATE.Value; } set { base.SUIT_DATE.Value = value; } }
        /// <summary>
        /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
        /// </summary>
        public new decimal? CNTRCT_FEE { get { return base.CNTRCT_FEE.Value; } set { base.CNTRCT_FEE.Value = value; } }
        /// <summary>
        /// This is the statutory fee awarded only to the attorney, determined by the debtor state.
        /// </summary>
        public new decimal? STAT_FEE { get { return base.STAT_FEE.Value; } set { base.STAT_FEE.Value = value; } }
        /// <summary>
        /// Initial number assigned to the suit.
        /// </summary>
        public new string DOCKET_NO { get { return base.DOCKET_NO.Value; } set { base.DOCKET_NO.Value = value; } }
        /// <summary>
        /// Depending on the state the suit is filed in, a new number may be assigned upon judgment.
        /// </summary>
        public new string JDGMNT_NO { get { return base.JDGMNT_NO.Value; } set { base.JDGMNT_NO.Value = value; } }
        /// <summary>
        /// This is the date the judgment was entered. If this field is populated, JDGMNT_AMT must be non-zero.
        /// </summary>
        public new DateTime? JDGMNT_DATE { get { return base.JDGMNT_DATE.Value; } set { base.JDGMNT_DATE.Value = value; } }
        /// <summary>
        /// This is the dollar amount awarded in the judgment. If this field is populated, JDGMNT_DATE must contain a valid date.
        /// </summary>
        public new decimal? JDGMNT_AMT { get { return base.JDGMNT_AMT.Value; } set { base.JDGMNT_AMT.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the interest due before the judgment was rendered.
        /// </summary>
        public new decimal? PREJ_INT { get { return base.PREJ_INT.Value; } set { base.PREJ_INT.Value = value; } }
        /// <summary>
        /// This is the sum of the costs to the sender to carry the suit forward, such as attorney fees.
        /// </summary>
        public new decimal? JDG_COSTS { get { return base.JDG_COSTS.Value; } set { base.JDG_COSTS.Value = value; } }
        /// <summary>
        /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form.
        /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
        /// </summary>
        public new decimal? RATES_PRE { get { return base.RATES_PRE.Value; } set { base.RATES_PRE.Value = value; } }
        /// <summary>
        /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form.
        /// <para>(Ex: .1950 is the value of this field if the interest rate is 19.5%.)</para>
        /// </summary>
        public new decimal? RATES_POST { get { return base.RATES_POST.Value; } set { base.RATES_POST.Value = value; } }
        /// <summary>
        /// Set this to Y if the statutory fee is actually kept by the law firm.
        /// </summary>
        public new bool STAT_FLAG { get { return base.STAT_FLAG.Value; } set { base.STAT_FLAG.Value = value; } }
        /// <summary>
        /// Set this to Y if PREJ_INT is added to the principal when judgment is rendered.
        /// </summary>
        public new bool INT_FLAG { get { return base.INT_FLAG.Value; } set { base.INT_FLAG.Value = value; } }
        /// <summary>
        /// This is the principal amount of the judgment.
        /// </summary>
        public new decimal? JDG_PRIN { get { return base.JDG_PRIN.Value; } set { base.JDG_PRIN.Value = value; } }
        /// <summary>
        /// This is the difference between what was requested in the suit and the judgment amount.
        /// </summary>
        public new decimal? ADJUSTMENT { get { return base.ADJUSTMENT.Value; } set { base.ADJUSTMENT.Value = value; } }
        /// <summary>
        /// This is the sum of PREJ_INT + JDG_COSTS + JUDG_PRIN + CNTRCT_FEE + STAT_FEE.
        /// </summary>
        public new decimal? JDGMNT_BAL { get { return base.JDGMNT_BAL.Value; } set { base.JDGMNT_BAL.Value = value; } }
        /// <summary>
        /// This is the county where the suit was filed.
        /// </summary>
        public new string LEGAL_COUNTY { get { return base.LEGAL_COUNTY.Value; } set { base.LEGAL_COUNTY.Value = value; } }
        /// <summary>
        /// This is the abbreviation of the state where the suit was filed.
        /// </summary>
        public new string LEGAL_STATE { get { return base.LEGAL_STATE.Value; } set { base.LEGAL_STATE.Value = value; } }
        /// <summary>
        /// Typically the full name of the court. Ex: Gwinnett County State Court, THE NINTH JUDICIAL CIRCUIT.
        /// </summary>
        public new string CRT_DESIG { get { return base.CRT_DESIG.Value; } set { base.CRT_DESIG.Value = value; } }
        /// <summary>
        /// This is a more brief description of the court. Ex: Superior, Supreme, Circuit
        /// </summary>
        public new string CRT_TYPE { get { return base.CRT_TYPE.Value; } set { base.CRT_TYPE.Value = value; } }
        /// <summary>
        /// This is the date when the suit judgment expires. It is usually between 7 and 12 years from the filing date.
        /// </summary>
        public new DateTime? JDGMNT_EXP_DATE { get { return base.JDGMNT_EXP_DATE.Value; } set { base.JDGMNT_EXP_DATE.Value = value; } }
        /// <summary>
        /// This is the standard code for the country where the suit was filed.
        /// </summary>
        public new string LEGAL_CNTRY { get { return base.LEGAL_CNTRY.Value; } set { base.LEGAL_CNTRY.Value = value; } }
        /// <summary>
        /// This is name of the court or clerk.
        /// </summary>
        public new string CRT_NAME { get { return base.CRT_NAME.Value; } set { base.CRT_NAME.Value = value; } }
        /// <summary>
        /// This is a free-form text field explaining the reason for filing suit.
        /// </summary>
        public new string SUITREASON { get { return base.SUITREASON.Value; } set { base.SUITREASON.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the primary debtor from the latest Record 02, Record 31 or Record 51.
        /// </summary>
        public new bool JUDGMENTONDEBTOR1 { get { return base.JUDGMENTONDEBTOR1.Value; } set { base.JUDGMENTONDEBTOR1.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 2nd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR2 { get { return base.JUDGMENTONDEBTOR2.Value; } set { base.JUDGMENTONDEBTOR2.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 3rd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR3 { get { return base.JUDGMENTONDEBTOR3.Value; } set { base.JUDGMENTONDEBTOR3.Value = value; } }
        /// <summary>
        /// This is the date the judgment was recorded; this may be different than JDGMNT_DATE.
        /// </summary>
        public new DateTime? JUDGMENTRECORDEDDATE { get { return base.JUDGMENTRECORDEDDATE.Value; } set { base.JUDGMENTRECORDEDDATE.Value = value; } }
        /// <summary>
        /// This is the date that the law firm generated the suit documents and sent them to court, as opposed to the date the suit was filed by the court.
        /// </summary>
        public new DateTime? SUITISSUEDDATE { get { return base.SUITISSUEDDATE.Value; } set { base.SUITISSUEDDATE.Value = value; } }
        #endregion

        public RecordType41() : base() { }
        public RecordType41(string RT41Entry) : base(RT41Entry) { }

        public override Type GetType() { return typeof(RecordType41); }
    }
    #endregion

    #region Record Type 44
    /// <summary>
    /// Record Type 44 - Bankruptcy (Receiver to Sender)
    /// </summary>
    public class RecordType44 : RecordTypes.YGC.RecordType44
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This identifies which debtor in the account is filing for bankruptcy.
        /// <para></para>
        /// <para>1 = Primary debtor</para>
        /// <para>2 = Second debtor</para>
        /// <para>3 = Third debtor</para>
        /// <para></para>
        /// <para>The primary debtor is determined by the latest record 02 or 31 and the second and third debtors are determined by the latest record 03 or 33.<para>
        /// </summary>
        public new int? DBTR_NUM { get { return base.DBTR_NUM.Value; } set { base.DBTR_NUM.Value = value; } }
        /// <summary>
        /// Chapter within the bankruptcy code; typically 7 or 13 for individuals.
        /// </summary>
        public new int? CHAPTER { get { return base.CHAPTER.Value; } set { base.CHAPTER.Value = value; } }
        /// <summary>
        /// This is the court-issued case number for the bankruptcy.
        /// </summary>
        public new string BK_FILENO { get { return base.BK_FILENO.Value; } set { base.BK_FILENO.Value = value; } }
        /// <summary>
        /// This is a non-restrictive field describing where the bankruptcy was filed. You can use this for the jurisdiction of the court.
        /// </summary>
        public new string LOC { get { return base.LOC.Value; } set { base.LOC.Value = value; } }
        /// <summary>
        /// This is the date the bankruptcy was filed.
        /// </summary>
        public new DateTime? FILED_DATE { get { return base.FILED_DATE.Value; } set { base.FILED_DATE.Value = value; } }
        /// <summary>
        /// If something was handled incorrectly, the bankruptcy may be dismissed; this is the date of dismissal.
        /// </summary>
        public new DateTime? DSMIS_DATE { get { return base.DSMIS_DATE.Value; } set { base.DSMIS_DATE.Value = value; } }
        /// <summary>
        /// The date a discharge is issued for the debtor. The discharge relieves the debtor of personal liability outside of what is put up as collateral.
        /// </summary>
        public new DateTime? DSCHG_DATE { get { return base.DSCHG_DATE.Value; } set { base.DSCHG_DATE.Value = value; } }
        /// <summary>
        /// This is the date the court actually adjudicates the case.
        /// </summary>
        public new DateTime? CLOSE_DATE { get { return base.CLOSE_DATE.Value; } set { base.CLOSE_DATE.Value = value; } }
        /// <summary>
        /// This is the date the bankruptcy is converted from one chapter to another, usually from 13 to 7.
        /// </summary>
        public new DateTime? CNVRT_DATE { get { return base.CNVRT_DATE.Value; } set { base.CNVRT_DATE.Value = value; } }
        /// <summary>
        /// This is the date that the initial meeting takes place between creditors and debtors, usually 30 days after FILED_DATE.
        /// </summary>
        public new DateTime? MTG_341_DATE { get { return base.MTG_341_DATE.Value; } set { base.MTG_341_DATE.Value = value; } }
        /// <summary>
        /// This is the time of day that the initial meeting takes place between creditors and debtors.  It should be in one of the following formats:
        /// <para></para>
        /// <para>HH:MM:SS if using 24-hour notation</para>
        /// <para>HH:MM AM if using 12-hour notation for before noon</para>
        /// <para>HH:MM PM if using 12-hour notation for after noon</para>
        /// </summary>
        public new DateTime? MTG_341_TIME { get { return base.MTG_341_TIME.Value; } set { base.MTG_341_TIME.Value = value; } }
        /// <summary>
        /// This is the location that the initial meeting takes place between creditors and debtors. There is no format restriction.
        /// </summary>
        public new string MTG_341_LOC { get { return base.MTG_341_LOC.Value; } set { base.MTG_341_LOC.Value = value; } }
        /// <summary>
        /// These are the initials of the name of the bankruptcy judge.
        /// </summary>
        public new string JUDGE_INIT { get { return base.JUDGE_INIT.Value; } set { base.JUDGE_INIT.Value = value; } }
        /// <summary>
        /// The debtor may choose to re-affirm the debt that would have been discharged by the bankruptcy. This is the amount the debtor agrees to pay in full; the re-affirmation survives the bankruptcy.
        /// </summary>
        public new decimal? REAF_AMT { get { return base.REAF_AMT.Value; } set { base.REAF_AMT.Value = value; } }
        /// <summary>
        /// This is the date when the re-affirmation was signed.
        /// </summary>
        public new DateTime? REAF_DATE { get { return base.REAF_DATE.Value; } set { base.REAF_DATE.Value = value; } }
        /// <summary>
        /// This is the periodic payment in a Chapter 11 or 13 bankruptcy or a lump sum payment of a Chapter 7 bankruptcy.
        /// </summary>
        public new decimal? PAY_AMT { get { return base.PAY_AMT.Value; } set { base.PAY_AMT.Value = value; } }
        /// <summary>
        /// This is the date that the payment arrangement was agreed upon.
        /// </summary>
        public new DateTime? PAY_DATE { get { return base.PAY_DATE.Value; } set { base.PAY_DATE.Value = value; } }
        /// <summary>
        /// This is the date the judge approves the payment plan.
        /// </summary>
        public new DateTime? CONF_DATE { get { return base.CONF_DATE.Value; } set { base.CONF_DATE.Value = value; } }
        /// <summary>
        /// This is the date the debtor caught up with paying all arrearages, fees and interest.
        /// </summary>
        public new DateTime? CURE_DATE { get { return base.CURE_DATE.Value; } set { base.CURE_DATE.Value = value; } }
        /// <summary>
        /// If the bankruptcy proceedings were put on hold, this is the date the stay was lifted.
        /// </summary>
        public new DateTime? STAY_LIFTED_DATE { get { return base.STAY_LIFTED_DATE.Value; } set { base.STAY_LIFTED_DATE.Value = value; } }
        #endregion

        public RecordType44() : base() { }
        public RecordType44(string RT44Entry) : base(RT44Entry) { }

        public override Type GetType() { return typeof(RecordType44); }
    }
    #endregion

    #region Record Type 46
    /// <summary>
    /// Record Type 46 - Physical Assets (property/vehicle) (Receiver to Sender)
    /// <para>This record is sent by the receiver to record a real estate or automobile asset owned by a debtor. Value = 46.</para>
    /// </summary>
    public class RecordType46 : RecordTypes.YGC.RecordType46
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <Summary>
        /// This identifies which debtor in the account owns the asset. Values are:
        /// <para>   1 - Primary debtor</para>
        /// <para>   2 - Second debtor</para>
        /// <para>   3 - Third debtor</para>
        /// </Summary>
        public new int? DBTR_NUM { get { return base.DBTR_NUM.Value; } set { base.DBTR_NUM.Value = value; } }
        /// <Summary>
        /// This number distinguishes assets owned by the debtor.
        /// </Summary>
        public new string ASSET_ID { get { return base.ASSET_ID.Value; } set { base.ASSET_ID.Value = value; } }
        /// <Summary>
        /// This is the full name of the debtor who owns this asset.
        /// </Summary>
        public new string ASSET_OWNER { get { return base.ASSET_OWNER.Value; } set { base.ASSET_OWNER.Value = value; } }
        /// <Summary>
        /// This is the street address where the asset is located.
        /// </Summary>
        public new string STREET { get { return base.STREET.Value; } set { base.STREET.Value = value; } }
        /// <Summary>
        /// This is an overflow field for STREET.
        /// </Summary>
        public new string STREET_2 { get { return base.STREET_2.Value; } set { base.STREET_2.Value = value; } }
        /// <Summary>
        /// This is an overflow field for STREET_2.
        /// </Summary>
        public new string STREET_3 { get { return base.STREET_3.Value; } set { base.STREET_3.Value = value; } }
        /// <Summary>
        /// This is the city where the asset is located.
        /// </Summary>
        public new string CITY { get { return base.CITY.Value; } set { base.CITY.Value = value; } }
        /// <Summary>
        /// This can be used as an alternative or supplement to CITY. It can hold the town or borough where the asset is located.
        /// </Summary>
        public new string TOWN { get { return base.TOWN.Value; } set { base.TOWN.Value = value; } }
        /// <Summary>
        /// This is the county where the asset is located.
        /// </Summary>
        public new string CNTY { get { return base.CNTY.Value; } set { base.CNTY.Value = value; } }
        /// <Summary>
        /// This is the abbreviation of the state where the asset is located.
        /// </Summary>
        public new string STATE { get { return base.STATE.Value; } set { base.STATE.Value = value; } }
        /// <Summary>
        /// This is the zip code where the asset is located.
        /// </Summary>
        public new string ZIP { get { return base.ZIP.Value; } set { base.ZIP.Value = value; } }
        /// <Summary>
        /// This is the abbreviation of the country where the asset is located.
        /// </Summary>
        public new string CNTRY { get { return base.CNTRY.Value; } set { base.CNTRY.Value = value; } }
        /// <Summary>
        /// This is the phone number of the asset location. It can accommodate separators for the area code and exchange.
        /// </Summary>
        public new string PHONE { get { return base.PHONE.Value; } set { base.PHONE.Value = value; } }
        /// <Summary>
        /// This is the block number if this is a land asset.
        /// </Summary>
        public new string BLOCK { get { return base.BLOCK.Value; } set { base.BLOCK.Value = value; } }
        /// <Summary>
        /// This is the lot number if this is a land asset.
        /// </Summary>
        public new string LOT { get { return base.LOT.Value; } set { base.LOT.Value = value; } }
        /// <Summary>
        /// This is the dollar value of the asset.
        /// </Summary>
        public new decimal? ASSET_VALUE { get { return base.ASSET_VALUE.Value; } set { base.ASSET_VALUE.Value = value; } }
        /// <Summary>
        /// This is a free-text description of the asset.
        /// </Summary>
        public new string ASSET_DESC { get { return base.ASSET_DESC.Value; } set { base.ASSET_DESC.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the vehicle ID number.
        /// </Summary>
        public new string ASSET_VIN { get { return base.ASSET_VIN.Value; } set { base.ASSET_VIN.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the license plate number.
        /// </Summary>
        public new string ASSET_LIC_PLATE { get { return base.ASSET_LIC_PLATE.Value; } set { base.ASSET_LIC_PLATE.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the color.
        /// </Summary>
        public new string ASSET_COLOR { get { return base.ASSET_COLOR.Value; } set { base.ASSET_COLOR.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the year the auto was made.
        /// </Summary>
        public new string ASSET_YEAR { get { return base.ASSET_YEAR.Value; } set { base.ASSET_YEAR.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the name of the make.
        /// </Summary>
        public new string ASSET_MAKE { get { return base.ASSET_MAKE.Value; } set { base.ASSET_MAKE.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the name of the model.
        /// </Summary>
        public new string ASSET_MODEL { get { return base.ASSET_MODEL.Value; } set { base.ASSET_MODEL.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the repossession file number assigned by the creditor.
        /// </Summary>
        public new string REPO_FILE_NUM { get { return base.REPO_FILE_NUM.Value; } set { base.REPO_FILE_NUM.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the date the repossession occurred.
        /// </Summary>
        public new DateTime? REPO_D { get { return base.REPO_D.Value; } set { base.REPO_D.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the value of the auto. This is the same as ASSET_VALUE.
        /// </Summary>
        public new decimal? REPO_AMT { get { return base.REPO_AMT.Value; } set { base.REPO_AMT.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the name of the new owner as stated on the title.
        /// </Summary>
        public new string CERT_TITLE_NAME { get { return base.CERT_TITLE_NAME.Value; } set { base.CERT_TITLE_NAME.Value = value; } }
        /// <Summary>
        /// If the asset is an automobile, this is the date the certification title was transferred.
        /// </Summary>
        public new DateTime? CERT_TITLE_D { get { return base.CERT_TITLE_D.Value; } set { base.CERT_TITLE_D.Value = value; } }
        /// <Summary>
        /// If the asset is real estate, this is the foreclosure date.
        /// </Summary>
        public new DateTime? MORT_FRCL_D { get { return base.MORT_FRCL_D.Value; } set { base.MORT_FRCL_D.Value = value; } }
        /// <Summary>
        /// This is the court-issued case number for the foreclosure.
        /// </Summary>
        public new string MORT_FRCL_FILENO { get { return base.MORT_FRCL_FILENO.Value; } set { base.MORT_FRCL_FILENO.Value = value; } }
        /// <Summary>
        /// This is the date the court dismisses the foreclosure for whatever reason.
        /// </Summary>
        public new DateTime? MORT_FRCL_DISMIS_D { get { return base.MORT_FRCL_DISMIS_D.Value; } set { base.MORT_FRCL_DISMIS_D.Value = value; } }
        /// <Summary>
        /// This is the periodic or total payment on the mortgage.
        /// </Summary>
        public new decimal? MORT_PMT { get { return base.MORT_PMT.Value; } set { base.MORT_PMT.Value = value; } }
        /// <Summary>
        /// This is the mortgage interest rate. It takes the decimal form:
        /// <para>   Ex: .195 is the value of this field if the interest rate is 19.5%.</para>
        /// </Summary>
        public new decimal? MORT_RATE { get { return base.MORT_RATE.Value; } set { base.MORT_RATE.Value = value; } }
        /// <Summary>
        /// This is the number of the book in the local records that the property is filed in. The liber number can be put here.
        /// </Summary>
        public new string MORT_BOOK_1 { get { return base.MORT_BOOK_1.Value; } set { base.MORT_BOOK_1.Value = value; } }
        /// <Summary>
        /// This is the page number in the book that the property is filed in.
        /// </Summary>
        public new string MORT_PAGE_1 { get { return base.MORT_PAGE_1.Value; } set { base.MORT_PAGE_1.Value = value; } }
        /// <Summary>
        /// This can be used for the portfolio number of the property if MORT_BOOK_1 holds the liber number.
        /// </Summary>
        public new string MORT_BOOK_2 { get { return base.MORT_BOOK_2.Value; } set { base.MORT_BOOK_2.Value = value; } }
        /// <Summary>
        /// This is the page number corresponding to MORT_BOOK_2.
        /// </Summary>
        public new string MORT_PAGE_2 { get { return base.MORT_PAGE_2.Value; } set { base.MORT_PAGE_2.Value = value; } }
        /// <Summary>
        /// This is the date the mortgage is entered in the local record book.
        /// </Summary>
        public new DateTime? MORT_RECRD_D { get { return base.MORT_RECRD_D.Value; } set { base.MORT_RECRD_D.Value = value; } }
        /// <Summary>
        /// This is the date the mortgage is due.
        /// </Summary>
        public new DateTime? MORT_DUE_D { get { return base.MORT_DUE_D.Value; } set { base.MORT_DUE_D.Value = value; } }
        /// <Summary>
        /// This is the number given by the land records office in the county where the property is located.
        /// </Summary>
        public new string LIEN_FILE_NUM { get { return base.LIEN_FILE_NUM.Value; } set { base.LIEN_FILE_NUM.Value = value; } }
        /// <Summary>
        /// This is the case number assigned by the bank holding the lien.
        /// </Summary>
        public new string LIEN_CASE_NUM { get { return base.LIEN_CASE_NUM.Value; } set { base.LIEN_CASE_NUM.Value = value; } }
        /// <Summary>
        /// This is the date the lien is established.
        /// </Summary>
        public new DateTime? LIEN_D { get { return base.LIEN_D.Value; } set { base.LIEN_D.Value = value; } }
        /// <Summary>
        /// This is the number of the book in the local records that the lien is filed in.
        /// </Summary>
        public new string LIEN_BOOK { get { return base.LIEN_BOOK.Value; } set { base.LIEN_BOOK.Value = value; } }
        /// <Summary>
        /// This is the page number in the book that the lien is filed in.
        /// </Summary>
        public new string LIEN_PAGE { get { return base.LIEN_PAGE.Value; } set { base.LIEN_PAGE.Value = value; } }
        /// <Summary>
        /// Was there a response to the lien? Values are Y/N.
        /// </Summary>
        public new string LIEN_AOL { get { return base.LIEN_AOL.Value; } set { base.LIEN_AOL.Value = value; } }
        /// <Summary>
        /// This is the date the lien release (the confirmation of the lien's payment) is filed.
        /// </Summary>
        public new DateTime? LIEN_RLSE_D { get { return base.LIEN_RLSE_D.Value; } set { base.LIEN_RLSE_D.Value = value; } }
        /// <Summary>
        /// This is the number of the book in the local records the lien release is filed in.
        /// </Summary>
        public new string LIEN_RLSE_BOOK { get { return base.LIEN_RLSE_BOOK.Value; } set { base.LIEN_RLSE_BOOK.Value = value; } }
        /// <Summary>
        /// This is the page number in the book in the local records the lien release is filed in.
        /// </Summary>
        public new string LIEN_RLSE_PAGE { get { return base.LIEN_RLSE_PAGE.Value; } set { base.LIEN_RLSE_PAGE.Value = value; } }
        /// <Summary>
        /// This is the date when the lien is foreclosed upon.
        /// </Summary>
        public new DateTime? LIEN_LITIG_D { get { return base.LIEN_LITIG_D.Value; } set { base.LIEN_LITIG_D.Value = value; } }
        /// <Summary>
        /// This is the number of the book in the local records that the lien is filed in when it is foreclosed upon.
        /// </Summary>
        public new string LIEN_LITIG_BOOK { get { return base.LIEN_LITIG_BOOK.Value; } set { base.LIEN_LITIG_BOOK.Value = value; } }
        /// <Summary>
        /// This is the page number in the book that the lien is filed in when it is foreclosed upon.
        /// </Summary>
        public new string LIEN_LITIG_PAGE { get { return base.LIEN_LITIG_PAGE.Value; } set { base.LIEN_LITIG_PAGE.Value = value; } }
        #endregion

        public RecordType46() : base() { }
        public RecordType46(string RT46Entry) : base(RT46Entry) { }

        public override Type GetType() { return typeof(RecordType46); }
    }
    #endregion

    #region Record Type 51
    /// <summary>
    /// Record Type 51 - Export - New Account Information						
    /// <para>This record is required to create a new account. It identifies the creditor and current owner of the debt and establishes the current financial state of the debt.This record type is identical to a record 01 except it is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC.YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 51. </para>
    /// </summary>
    public class RecordType51 : RecordTypes.YGC.RecordType51
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the date the account was first sent to the receiver to work, regardless of when it was uploaded to YouveGotClaims®. If this field is left blank, YouveGotReports will automatically fill it with the date this record is processed.
        /// </summary>
        public new DateTime? DATE_FORW { get { return base.DATE_FORW.Value; } set { base.DATE_FORW.Value = value; } }
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
        public new string LAW_LIST { get { return base.LAW_LIST.Value; } set { base.LAW_LIST.Value = value; } }
        /// <summary>
        /// This is the commission percentage the receiver earns as determined by the sender. There is no set numeric format.
        /// </summary>
        public new string COMM { get { return base.COMM.Value; } set { base.COMM.Value = value; } }
        /// <summary>
        /// This is the percentage the receiver earns if a suit is filed. There is no set numeric format.
        /// </summary>
        public new string SFEE { get { return base.SFEE.Value; } set { base.SFEE.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the original principal due at time of placement.
        /// </summary>
        public new decimal? ORIG_CLAIM { get { return base.ORIG_CLAIM.Value; } set { base.ORIG_CLAIM.Value = value; } }
        /// <summary>
        /// This is the dollar amount of accrued interest.
        /// </summary>
        public new decimal? ORIG_INT { get { return base.ORIG_INT.Value; } set { base.ORIG_INT.Value = value; } }
        /// <summary>
        /// This is the last day of the accrual period.
        /// </summary>
        public new DateTime? ORIG_INT_D { get { return base.ORIG_INT_D.Value; } set { base.ORIG_INT_D.Value = value; } }
        /// <summary>
        /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form:
        /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
        /// </summary>
        public new decimal? RATES_PRE { get { return base.RATES_PRE.Value; } set { base.RATES_PRE.Value = value; } }
        /// <summary>
        /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form:
        /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
        /// </summary>
        public new decimal? RATES_POST { get { return base.RATES_POST.Value; } set { base.RATES_POST.Value = value; } }
        /// <summary>
        /// This is the company name of either the original creditor or debt purchaser/collection agency.
        /// </summary>
        public new string CRED_NAME { get { return base.CRED_NAME.Value; } set { base.CRED_NAME.Value = value; } }
        /// <summary>
        /// This is an overflow field for CRED_NAME.
        /// </summary>
        public new string CRED_NAME2 { get { return base.CRED_NAME2.Value; } set { base.CRED_NAME2.Value = value; } }
        /// <summary>
        /// This is the company name of either the original creditor or debt purchaser/collection agency.
        /// </summary>
        public new string CRED_NAME_COMBINED { get { return base.CRED_NAME_COMBINED.Value; } set { base.CRED_NAME_COMBINED.Value = value; } }
        /// <summary>
        /// Street address of company in CRED_NAME.
        /// </summary>
        public new string CRED_STREET { get { return base.CRED_STREET.Value; } set { base.CRED_STREET.Value = value; } }
        /// <summary>
        /// City and state of company in CRED_NAME. Format this field as City ST or City,ST.
        /// <para>(Example: Linden NJ or Linden,NJ)</para>
        /// </summary>
        public new string CRED_CS { get { return base.CRED_CS.Value; } set { base.CRED_CS.Value = value; } }
        /// <summary>
        /// This is the creditor's zip code. It can accommodate the four-digit extension if you do not include the hyphen.
        /// </summary>
        public new string CRED_ZIP { get { return base.CRED_ZIP.Value; } set { base.CRED_ZIP.Value = value; } }
        /// <summary>
        /// This field represents everything the debtor owes at time of placement, excluding interest. It equals ORIG_CLAIM + LATE_FEE + CNTRCT_FEE + STAT_FEE + JDG_COSTS, where the last three fields are from a previous legal action defined in Record 07 and LATE_FEE is from this record.
        /// </summary>
        public new decimal? DEBT_BAL { get { return base.DEBT_BAL.Value; } set { base.DEBT_BAL.Value = value; } }
        /// <summary>
        /// User-defined. Suggested Codes:  ARB = Arbitration, MED = Medical, CARD = Credit Card, LOAN = loan
        /// </summary>
        public new string CTYPE { get { return base.CTYPE.Value; } set { base.CTYPE.Value = value; } }
        /// <summary>
        /// This is the date the last payment was received by the creditor.
        /// </summary>
        public new DateTime? DATE_LPAY { get { return base.DATE_LPAY.Value; } set { base.DATE_LPAY.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the last payment made to the creditor.
        /// </summary>
        public new decimal? AMT_LPAY { get { return base.AMT_LPAY.Value; } set { base.AMT_LPAY.Value = value; } }
        /// <summary>
        /// Typically, the date the credit card account was opened.
        /// </summary>
        public new DateTime? DATE_OPEN { get { return base.DATE_OPEN.Value; } set { base.DATE_OPEN.Value = value; } }
        /// <summary>
        /// This is the date the creditor has determined the debt will not be paid through normal channels.
        /// </summary>
        public new DateTime? CHRG_OFF_D { get { return base.CHRG_OFF_D.Value; } set { base.CHRG_OFF_D.Value = value; } }
        /// <summary>
        /// This is typically the same as ORIG_CLAIM.
        /// </summary>
        public new decimal? CHRG_OFF_A { get { return base.CHRG_OFF_A.Value; } set { base.CHRG_OFF_A.Value = value; } }
        /// <summary>
        /// If this debt was purchased, this field holds the date of purchase.
        /// </summary>
        public new DateTime? PURCHASE_D { get { return base.PURCHASE_D.Value; } set { base.PURCHASE_D.Value = value; } }
        /// <summary>
        /// This is only filled out if this debt was purchased or if the sender is a collection agency. It holds the name of the original creditor company.
        /// </summary>
        public new string ORIG_CRED { get { return base.ORIG_CRED.Value; } set { base.ORIG_CRED.Value = value; } }
        /// <summary>
        /// This is an overflow field for ORIG_CRED.
        /// </summary>
        public new string ORIG_CRED2 { get { return base.ORIG_CRED2.Value; } set { base.ORIG_CRED2.Value = value; } }
        /// <summary>
        /// Combined Value of ORIG_CRED and ORIG_CRED2
        /// </summary>
        public new string ORIG_CRED_COMBINED { get { return base.ORIG_CRED_COMBINED.Value; } set { base.ORIG_CRED_COMBINED.Value = value; } }
        /// <summary>
        /// Typically pertains to purchased debt, and holds the number assigned to the batch of accounts purchased. Some reports in YouveGotReports can be filtered against this code.
        /// </summary>
        public new string PORT_ID { get { return base.PORT_ID.Value; } set { base.PORT_ID.Value = value; } }
        /// <summary>
        /// This is the standard code for the creditor's country.
        /// </summary>
        public new string CRED_CNTRY { get { return base.CRED_CNTRY.Value; } set { base.CRED_CNTRY.Value = value; } }
        /// <summary>
        /// This is the date the last payment was received by the issuer.
        /// </summary>
        public new DateTime? LPAY_ISS_D { get { return base.LPAY_ISS_D.Value; } set { base.LPAY_ISS_D.Value = value; } }
        /// <summary>
        /// This is the amount of the last payment received by the issuer.
        /// </summary>
        public new decimal? LPAY_ISS_AMT { get { return base.LPAY_ISS_AMT.Value; } set { base.LPAY_ISS_AMT.Value = value; } }
        /// <summary>
        /// This flag indicates whether the sender has media to support a lawsuit. It is up to the receiver to determine what kind of media will ultimately be required, at which time they will send a request. Use values Y or N.
        /// </summary>
        public new string MEDIA { get { return base.MEDIA.Value; } set { base.MEDIA.Value = value; } }
        /// <summary>
        /// Date of Delinquency
        /// </summary>
        public new DateTime? DELINQ_D { get { return base.DELINQ_D.Value; } set { base.DELINQ_D.Value = value; } }
        /// <summary>
        /// Date of Acceleration
        /// </summary>
        public new DateTime? ACCEL_D { get { return base.ACCEL_D.Value; } set { base.ACCEL_D.Value = value; } }
        /// <summary>
        /// Date of Repossession
        /// </summary>
        public new DateTime? REPO_D { get { return base.REPO_D.Value; } set { base.REPO_D.Value = value; } }
        /// <summary>
        /// Sale Date
        /// </summary>
        public new DateTime? SALE_D { get { return base.SALE_D.Value; } set { base.SALE_D.Value = value; } }
        /// <summary>
        /// Maturity Date
        /// </summary>
        public new DateTime? MATUR_D { get { return base.MATUR_D.Value; } set { base.MATUR_D.Value = value; } }
        /// <summary>
        /// The Statute of Limitations Start Date is calculated by the sender.
        /// </summary>
        public new DateTime? SOL_START_D { get { return base.SOL_START_D.Value; } set { base.SOL_START_D.Value = value; } }
        /// <summary>
        /// The Statute of Limitations End Date is calculated by the sender. You can also refer to the Documents tab, Business Reference category for the current statutes for each state.
        /// </summary>
        public new DateTime? SOL_END_D { get { return base.SOL_END_D.Value; } set { base.SOL_END_D.Value = value; } }
        /// <summary>
        /// This is the accumulation of late fees for non-payment before placement. This should be included in the calculation of DEBT_BAL.
        /// </summary>
        public new decimal? LATE_FEE { get { return base.LATE_FEE.Value; } set { base.LATE_FEE.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before the current CRED_NAME or ORIG_CRED if present. If ORIG_CRED is not present, HIST_CRED1 should NOT be used in place of ORIG_CRED.
        /// </summary>
        public new string HIST_CRED1 { get { return base.HIST_CRED1.Value; } set { base.HIST_CRED1.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED1.
        /// </summary>
        public new string HIST_CRED2 { get { return base.HIST_CRED2.Value; } set { base.HIST_CRED2.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED2.
        /// </summary>
        public new string HIST_CRED3 { get { return base.HIST_CRED3.Value; } set { base.HIST_CRED3.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED3.
        /// </summary>
        public new string HIST_CRED4 { get { return base.HIST_CRED4.Value; } set { base.HIST_CRED4.Value = value; } }
        /// <summary>
        /// This represents the name of the creditor for the account before HIST_CRED4.
        /// </summary>
        public new string HIST_CRED5 { get { return base.HIST_CRED5.Value; } set { base.HIST_CRED5.Value = value; } }
        /// <summary>
        /// This holds any additional cost besides legal costs, such as bounced check fees, already incurred for pursuing this debt.
        /// </summary>
        public new decimal? ADDITIONAL_COST { get { return base.ADDITIONAL_COST.Value; } set { base.ADDITIONAL_COST.Value = value; } }
        /// <summary>
        /// If a sender recalls an account placed with YGC, this field holds the date it was recalled.
        /// </summary>
        public new DateTime? RECALL_DATE { get { return base.RECALL_DATE.Value; } set { base.RECALL_DATE.Value = value; } }
        /// <summary>
        /// This is the number for the debt account before charge-off. This may differ from FORW_FILE.
        /// </summary>
        public new string PRECHARGEOFFACCOUNTNUMBER { get { return base.PRECHARGEOFFACCOUNTNUMBER.Value; } set { base.PRECHARGEOFFACCOUNTNUMBER.Value = value; } }
        /// <summary>
        /// This flag indicates whether the account requires a FACT Act statement.
        /// <para>Use values Y or N.</para>
        /// </summary>
        public new bool FACTACTSTATEMENT { get { return base.FACTACTSTATEMENT.Value; } set { base.FACTACTSTATEMENT.Value = value; } }
        /// <summary>
        /// This field is the Attorney, Agency or Vendor Code set by the current Creditor or Debt Buyer's internal system.
        /// </summary>
        public new string VENDORCODE { get { return base.VENDORCODE.Value; } set { base.VENDORCODE.Value = value; } }
        /// <summary>
        /// This field is used when original products or services were provided by one company for offer under another company's brand.  This allows the current Creditor or Debt Buyer to identify the private label from the original creditor. (Example: "Private Label" -GE/Walmart)
        /// </summary>
        public new string LOANDESCRIPTION { get { return base.LOANDESCRIPTION.Value; } set { base.LOANDESCRIPTION.Value = value; } }
        #endregion

        public RecordType51() { }
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
    public class RecordType52 : RecordTypes.YGC.RecordType52
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the primary debtor's name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_NAME { get { return base.D1_NAME.Value; } set { base.D1_NAME.Value = value; } }
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
        public new Salutation D1_SALUT { get { return base.D1_SALUT.Value; } set { base.D1_SALUT.Value = value; } }
        /// <summary>
        /// This is the primary debtor's alias name. The format is Lastname/Firstname.
        /// </summary>
        public new string D1_ALIAS { get { return base.D1_ALIAS.Value; } set { base.D1_ALIAS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET { get { return base.D1_STREET.Value; } set { base.D1_STREET.Value = value; } }
        /// <summary>
        /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
        /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
        /// </para></summary>
        public new string D1_CS { get { return base.D1_CS.Value; } set { base.D1_CS.Value = value; } }
        /// <summary>
        /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
        /// </summary>
        public new string D1_ZIP { get { return base.D1_ZIP.Value; } set { base.D1_ZIP.Value = value; } }
        /// <summary>
        /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_PHONE { get { return base.D1_PHONE.Value; } set { base.D1_PHONE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_FAX { get { return base.D1_FAX.Value; } set { base.D1_FAX.Value = value; } }
        /// <summary>
        /// This is the primary debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D1_SSN { get { return base.D1_SSN.Value; } set { base.D1_SSN.Value = value; } }
        /// <summary>
        /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
        /// </summary>
        public new string RFILE { get { return base.RFILE.Value; } set { base.RFILE.Value = value; } }
        /// <summary>
        /// This is the primary debtor's date of birth.
        /// </summary>
        public new DateTime? D1_DOB { get { return base.D1_DOB.Value; } set { base.D1_DOB.Value = value; } }
        /// <summary>
        /// This is the primary debtor's driver's license number.
        /// </summary>
        public new string D1_DL { get { return base.D1_DL.Value; } set { base.D1_DL.Value = value; } }
        /// <summary>
        /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STATE { get { return base.D1_STATE.Value; } set { base.D1_STATE.Value = value; } }
        /// <summary>
        /// Set this field to Y if the served papers were returned.
        /// </summary>
        public new bool D1_MAIL { get { return base.D1_MAIL.Value; } set { base.D1_MAIL.Value = value; } }
        /// <summary>
        /// This is the date the suit was served.
        /// </summary>
        public new DateTime? SERVICE_D { get { return base.SERVICE_D.Value; } set { base.SERVICE_D.Value = value; } }
        /// <summary>
        /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
        /// </summary>
        public new DateTime? ANSWER_DUE_D { get { return base.ANSWER_DUE_D.Value; } set { base.ANSWER_DUE_D.Value = value; } }
        /// <summary>
        /// This is the date the debtor's response was filed.
        /// </summary>
        public new DateTime? ANSWER_FILE_D { get { return base.ANSWER_FILE_D.Value; } set { base.ANSWER_FILE_D.Value = value; } }
        /// <summary>
        /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
        /// </summary>
        public new DateTime? DEFAULT_D { get { return base.DEFAULT_D.Value; } set { base.DEFAULT_D.Value = value; } }
        /// <summary>
        /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
        /// </summary>
        public new DateTime? TRIAL_D { get { return base.TRIAL_D.Value; } set { base.TRIAL_D.Value = value; } }
        /// <summary>
        /// This is the date of the latest hearing on a motion filed by either party.
        /// </summary>
        public new DateTime? HEARING_D { get { return base.HEARING_D.Value; } set { base.HEARING_D.Value = value; } }
        /// <summary>
        /// This is the date a lien was filed against a debtor's property.
        /// </summary>
        public new DateTime? LIEN_D { get { return base.LIEN_D.Value; } set { base.LIEN_D.Value = value; } }
        /// <summary>
        /// This is the date garnishment against the debtor's wages was established.
        /// </summary>
        public new DateTime? GARN_D { get { return base.GARN_D.Value; } set { base.GARN_D.Value = value; } }
        ///<summary>
        ///This is the method used to serve the papers. 
        ///<para>The valid codes are: </para>
        ///<para>PER = Personal</para>
        ///<para>CER = Certified Mail</para>
        ///<para>SUB = Sub-service</para>
        ///<para>POS = Posting (left at front door)</para>
        ///<para>FIR = First Class Mail</para>
        ///</summary>
        public new string SERVICE_TYPE { get { return base.SERVICE_TYPE.Value; } set { base.SERVICE_TYPE.Value = value; } }
        /// <summary>
        /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2 { get { return base.D1_STREET2.Value; } set { base.D1_STREET2.Value = value; } }
        /// <summary>
        /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CITY { get { return base.D1_CITY.Value; } set { base.D1_CITY.Value = value; } }
        /// <summary>
        /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D1_CELL { get { return base.D1_CELL.Value; } set { base.D1_CELL.Value = value; } }
        /// <summary>
        /// Fair Isaac credit score
        /// </summary>
        public new int? SCORE_FICO { get { return base.SCORE_FICO.Value; } set { base.SCORE_FICO.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_COLLECT { get { return base.SCORE_COLLECT.Value; } set { base.SCORE_COLLECT.Value = value; } }
        /// <summary>
        /// Creditor-calculated score
        /// </summary>
        public new int? SCORE_OTHER { get { return base.SCORE_OTHER.Value; } set { base.SCORE_OTHER.Value = value; } }
        /// <summary>
        /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
        /// </summary>
        public new string D1_CNTRY { get { return base.D1_CNTRY.Value; } set { base.D1_CNTRY.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET_LONG { get { return base.D1_STREET_LONG.Value; } set { base.D1_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D1_STREET2_LONG { get { return base.D1_STREET2_LONG.Value; } set { base.D1_STREET2_LONG.Value = value; } }
        /// <summary>
        /// Debtor First Name
        /// </summary>
        public new string FIRSTNAME { get { return base.FIRSTNAME.Value; } set { base.FIRSTNAME.Value = value; } }
        /// <summary>
        /// Debtor Last Name
        /// </summary>
        public new string LASTNAME { get { return base.LASTNAME.Value; } set { base.LASTNAME.Value = value; } }
        /// <summary>
        /// Creditor-Internal calculated score
        /// </summary>
        public new string SCOREINTERNAL { get { return base.SCOREINTERNAL.Value; } set { base.SCOREINTERNAL.Value = value; } }
        /// <summary>
        /// This field is the Placement or Internal Delinquency Stage of the account at the time of placement used by the the Creditor or Debt Buyer (User Defined).
        /// </summary>
        public new string STAGE { get { return base.STAGE.Value; } set { base.STAGE.Value = value; } }
        #endregion

        public RecordType52() { }
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
    public class RecordType53 : RecordTypes.YGC.RecordType53
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the name of the second debtor or co-signer. The format is Lastname/Firstname.
        /// </summary>
        public new string D2_NAME { get { return base.D2_NAME.Value; } set { base.D2_NAME.Value = value; } }
        /// <summary>
        /// This is the second debtor's street address. If there is no D2_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
        /// </summary>
        public new string D2_STREET { get { return base.D2_STREET.Value; } set { base.D2_STREET.Value = value; } }
        /// <summary>
        /// This is the second debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D2_CSZ field but no D2_CNTRY field populated in this record, the country will become empty on YouveGotReports.
        /// </summary>
        public new string D2_CSZ { get { return base.D2_CSZ.Value; } set { base.D2_CSZ.Value = value; } }
        /// <summary>
        /// This is the second debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D2_PHONE { get { return base.D2_PHONE.Value; } set { base.D2_PHONE.Value = value; } }
        /// <summary>
        /// This is the second debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D2_SSN { get { return base.D2_SSN.Value; } set { base.D2_SSN.Value = value; } }
        /// <summary>
        /// This is the name of the third debtor or co-signer. The format is Lastname/Firstname.
        /// </summary>
        public new string D3_NAME { get { return base.D3_NAME.Value; } set { base.D3_NAME.Value = value; } }
        /// <summary>
        /// This is the third debtor's street address. If there is no D3_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
        /// </summary>
        public new string D3_STREET { get { return base.D3_STREET.Value; } set { base.D3_STREET.Value = value; } }
        /// <summary>
        /// This is the third debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D3_CSZ field but no D3_CNTRY field populated in this record, the country will become empty on YouveGotReports.
        /// </summary>
        public new string D3_CSZ { get { return base.D3_CSZ.Value; } set { base.D3_CSZ.Value = value; } }
        /// <summary>
        /// This is the third debtor's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string D3_PHONE { get { return base.D3_PHONE.Value; } set { base.D3_PHONE.Value = value; } }
        /// <summary>
        /// This is the third debtor's social security number. It can accommodate hyphens.
        /// </summary>
        public new string D3_SSN { get { return base.D3_SSN.Value; } set { base.D3_SSN.Value = value; } }
        /// <summary>
        /// This is the second debtor's date of birth.
        /// </summary>
        public new DateTime? D2_DOB { get { return base.D2_DOB.Value; } set { base.D2_DOB.Value = value; } }
        /// <summary>
        /// This is the third debtor's date of birth.
        /// </summary>
        public new DateTime? D3_DOB { get { return base.D3_DOB.Value; } set { base.D3_DOB.Value = value; } }
        /// <summary>
        /// This is the second debtor's driver's license number.
        /// </summary>
        public new string D2_DL { get { return base.D2_DL.Value; } set { base.D2_DL.Value = value; } }
        /// <summary>
        /// This is the third debtor's driver's license number.
        /// </summary>
        public new string D3_DL { get { return base.D3_DL.Value; } set { base.D3_DL.Value = value; } }
        /// <summary>
        /// This is the standard code for the second debtor's country. If there is a D2_CNTRY field but no D2_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
        /// </summary>
        public new string D2_CNTRY { get { return base.D2_CNTRY.Value; } set { base.D2_CNTRY.Value = value; } }
        /// <summary>
        /// This is the standard code for the third debtor's country. If there is a D3_CNTRY field but no D3_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
        /// </summary>
        public new string D3_CNTRY { get { return base.D3_CNTRY.Value; } set { base.D3_CNTRY.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire 2nd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D2_STREET in case your receivers cannot yet accept this newer field. If there is a D2_STREET_LONG field but no D2_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D2_STREET_LONG { get { return base.D2_STREET_LONG.Value; } set { base.D2_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D2_STREET_LONG. If there is a D2_STREET2_LONG field but no D2_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D2_STREET2_LONG { get { return base.D2_STREET2_LONG.Value; } set { base.D2_STREET2_LONG.Value = value; } }
        /// <summary>
        /// This field serves to deliver the entire 3rd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D3_STREET in case your receivers cannot yet accept this newer field. If there is a D3_STREET_LONG field but no D3_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
        /// </summary>
        public new string D3_STREET_LONG { get { return base.D3_STREET_LONG.Value; } set { base.D3_STREET_LONG.Value = value; } }
        /// <summary>
        /// This is an overflow field for D3_STREET_LONG. If there is a D3_STREET2_LONG field but no D3_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
        /// </summary>
        public new string D3_STREET2_LONG { get { return base.D3_STREET2_LONG.Value; } set { base.D3_STREET2_LONG.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's first name. Include middle name here if available. Use this with D2_LNAME instead of D2_NAME if your collection software enables it.
        /// </summary>
        public new string D2_FNAME { get { return base.D2_FNAME.Value; } set { base.D2_FNAME.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D2_FNAME instead of D2_NAME if your collection software enables it.
        /// </summary>
        public new string D2_LNAME { get { return base.D2_LNAME.Value; } set { base.D2_LNAME.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's city. Use this with D2_STATE and D2_ZIP instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_CITY { get { return base.D2_CITY.Value; } set { base.D2_CITY.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's state. Use the standard abbreviation. Use this with D2_CITY and D2_ZIP instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_STATE { get { return base.D2_STATE.Value; } set { base.D2_STATE.Value = value; } }
        /// <summary>
        /// This is for the 2nd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D2_CITY and D2_STATE instead of D2_CSZ if your collection software enables it.
        /// </summary>
        public new string D2_ZIP { get { return base.D2_ZIP.Value; } set { base.D2_ZIP.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's first name. Include middle name here if available. Use this with D3_LNAME instead of D3_NAME if your collection software enables it.
        /// </summary>
        public new string D3_FNAME { get { return base.D3_FNAME.Value; } set { base.D3_FNAME.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's last name. Include any suffix (Jr., III, etc.) if available. Use this with D3_FNAME instead of D3_NAME if your collection software enables it.
        /// </summary>
        public new string D3_LNAME { get { return base.D3_LNAME.Value; } set { base.D3_LNAME.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's city. Use this with D3_STATE and D3_ZIP instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_CITY { get { return base.D3_CITY.Value; } set { base.D3_CITY.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's state. Use the standard abbreviation. Use this with D3_CITY and D3_ZIP instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_STATE { get { return base.D3_STATE.Value; } set { base.D3_STATE.Value = value; } }
        /// <summary>
        /// This is for the 3rd debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. Use this with D3_CITY and D3_STATE instead of D3_CSZ if your collection software enables it.
        /// </summary>
        public new string D3_ZIP { get { return base.D3_ZIP.Value; } set { base.D3_ZIP.Value = value; } }
        #endregion

        public RecordType53() { }
        public RecordType53(string RT53Entry) : base(RT53Entry) { }

        public override Type GetType() { return typeof(RecordType53); }
    }
    #endregion

    #region Record Type 54
    /// <summary>
    /// Record Type 54 - Export - Employment Information
    /// <para>This record holds the debtors' employment information. You can submit a record for each of 3 distinct debtors for the same account. This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system.Value = 54.</para>
    /// </summary>
    public class RecordType54 : RecordTypes.YGC.RecordType54
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Company name of debtor's employer.
        /// </summary>
        public new string Employer_Name { get { return base.Employer_Name.Value; } set { base.Employer_Name.Value = value; } }
        /// <summary>
        /// This is the employer's street address.
        /// </summary>
        public new string Employer_Street { get { return base.Employer_Street.Value; } set { base.Employer_Street.Value = value; } }
        /// <summary>
        /// This is the employer's PO Box number.
        /// </summary>
        public new string Employer_PO_Box { get { return base.Employer_PO_Box.Value; } set { base.Employer_PO_Box.Value = value; } }
        /// <summary>
        /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
        /// </summary>
        public new string Employer_City_State { get { return base.Employer_City_State.Value; } set { base.Employer_City_State.Value = value; } }
        /// <summary>
        /// This is the employer's zip code. It can accommodate the four-digit extension with the hyphen.
        /// </summary>
        public new string Employer_Zip { get { return base.Employer_Zip.Value; } set { base.Employer_Zip.Value = value; } }
        /// <summary>
        /// This is the employer's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string Employer_Phone { get { return base.Employer_Phone.Value; } set { base.Employer_Phone.Value = value; } }
        /// <summary>
        /// This is the employer's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string Employer_Fax { get { return base.Employer_Fax.Value; } set { base.Employer_Fax.Value = value; } }
        /// <summary>
        /// Department or personnel for correspondence to employer.
        /// </summary>
        public new string Employer_ATTN { get { return base.Employer_ATTN.Value; } set { base.Employer_ATTN.Value = value; } }
        /// <summary>
        /// This is the contact person at the payroll department.
        /// </summary>
        public new string Employer_Payroll { get { return base.Employer_Payroll.Value; } set { base.Employer_Payroll.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 employment records. Values are 1, 2 or 3. 
        /// <para>(Ex: The record 04 for the primary debtor can have EMP_NO = 1 and the record 04 for the co-signer can have EMP_NO = 2.)</para>
        /// <para>If a record 04 is sent with the same EMP_NO as a previous record 04 for the same account, the second record will overwrite the first.</para>
        /// </summary>
        public new int? Debtor_Number { get { return base.Debtor_Number.Value; } set { base.Debtor_Number.Value = value; } }
        /// <summary>
        /// This is the debtor name. There is no format restriction.
        /// </summary>
        public new string Employee_Name { get { return base.Employee_Name.Value; } set { base.Employee_Name.Value = value; } }
        /// <summary>
        /// Income Earned Per Pay Frequency Period
        /// </summary>
        public new decimal? Employee_Income { get { return base.Employee_Income.Value; } set { base.Employee_Income.Value = value; } }
        /// <summary>
        /// Freqeuncy with Which the Listed Income is Distributed
        /// </summary>
        public new IncomeFrequency Employee_Frequency { get { return base.Employee_Frequency.Value; } set { base.Employee_Frequency.Value = value; } }
        /// <summary>
        /// This is the employee's title at the given employer.
        /// </summary>
        public new string Employee_Position { get { return base.Employee_Position.Value; } set { base.Employee_Position.Value = value; } }
        /// <summary>
        /// Number of Months with Employer
        /// </summary>
        public new int? Employee_Tenure { get { return base.Employee_Tenure.Value; } set { base.Employee_Tenure.Value = value; } }
        /// <summary>
        /// This is the standard code for the employee's country.
        /// </summary>
        public new string Employer_Country { get { return base.Employer_Country.Value; } set { base.Employer_Country.Value = value; } }
        #endregion

        public RecordType54() { }
        public RecordType54(string RT54Entry) : base(RT54Entry) { }

        public override Type GetType() { return typeof(RecordType54); }
    }
    #endregion

    #region Record Type 55
    /// <summary>
    /// Record Type 55 - Export - Bank/Asset Information
    /// <para>This record holds any bank account information and non-auto or non-real estate asset information for the debtor. You can submit a record for each of 3 distinct bank accounts for the same debtor.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 55.</para>
    /// </summary>
    public class RecordType55 : RecordTypes.YGC.RecordType55
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Currently a free field
        /// </summary>
        public new string FILLER { get { return base.FILLER.Value; } set { base.FILLER.Value = value; } }
        /// <summary>
        /// This is the name of the debtor's bank.
        /// </summary>
        public new string BANK_NAME { get { return base.BANK_NAME.Value; } set { base.BANK_NAME.Value = value; } }
        /// <summary>
        /// This is the debtor's bank street address.
        /// </summary>
        public new string BANK_STREET { get { return base.BANK_STREET.Value; } set { base.BANK_STREET.Value = value; } }
        /// <summary>
        /// This is the bank's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
        /// </summary>
        public new string BANK_CSZ { get { return base.BANK_CSZ.Value; } set { base.BANK_CSZ.Value = value; } }
        /// <summary>
        /// This is the contact name for the bank.
        /// </summary>
        public new string BANK_ATTN { get { return base.BANK_ATTN.Value; } set { base.BANK_ATTN.Value = value; } }
        /// <summary>
        /// This is the bank's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string BANK_PHONE { get { return base.BANK_PHONE.Value; } set { base.BANK_PHONE.Value = value; } }
        /// <summary>
        /// This is the bank's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string BANK_FAX { get { return base.BANK_FAX.Value; } set { base.BANK_FAX.Value = value; } }
        /// <summary>
        /// This is the debtor's bank account number.
        /// </summary>
        public new string BANK_ACCT { get { return base.BANK_ACCT.Value; } set { base.BANK_ACCT.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET1 { get { return base.MISC_ASSET1.Value; } set { base.MISC_ASSET1.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET2 { get { return base.MISC_ASSET2.Value; } set { base.MISC_ASSET2.Value = value; } }
        /// <summary>
        /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_ASSET3 { get { return base.MISC_ASSET3.Value; } set { base.MISC_ASSET3.Value = value; } }
        /// <summary>
        /// This field holds a relevant phone number relating to the miscellaneous asset. If this field is used, you MUST set BANK_NO to 1.
        /// </summary>
        public new string MISC_PHONE { get { return base.MISC_PHONE.Value; } set { base.MISC_PHONE.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 bank records. Values are 1, 2 or 3. 
        /// <para>(Ex: The record 05 for the debtor's checking account can have BANK_NO = 1 and the record 05 for the debtor's savings account can have BANK_NO = 2.)</para>
        /// <para>If a record 05 is sent with the same BANK_NO as a previous record 05 for the same account, the second record will overwrite the first. If you have a miscellaneous asset described in this record, this value must be 1.</para>
        /// </summary>
        public new int? BANK_NO { get { return base.BANK_NO.Value; } set { base.BANK_NO.Value = value; } }
        /// <summary>
        /// This is the standard code for the bank's country.
        /// </summary>
        public new string BANK_CNTRY { get { return base.BANK_CNTRY.Value; } set { base.BANK_CNTRY.Value = value; } }
        /// <summary>
        /// This is the ABA routing number for the bank account. You may use this field in addition to or instead of the BANK_ACCT field.
        /// </summary>
        public new string ROUTINGNUMBER { get { return base.ROUTINGNUMBER.Value; } set { base.ROUTINGNUMBER.Value = value; } }
        #endregion

        public RecordType55() { }
        public RecordType55(string RT55Entry) : base(RT55Entry) { }

        public override Type GetType() { return typeof(RecordType55); }
    }
    #endregion

    #region Record Type 56
    /// <summary>
    /// Record Type 56 - Export - Misc Information
    /// <para>This record is for debtor attorney information and any miscellaneous information that no other record in the DataStandard accommodates. You can submit a record for each of 3 debtor attorneys for the same account.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC.YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 56.</para>
    /// </summary>
    public class RecordType56 : RecordTypes.YGC.RecordType56
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is for the name of the debtor's attorney. There is no format restriction.
        /// </summary>
        public new string ADVA_NAME { get { return base.ADVA_NAME.Value; } set { base.ADVA_NAME.Value = value; } }
        /// <summary>
        /// This is the name of the debtor's attorney's firm.
        /// </summary>
        public new string ADVA_FIRM { get { return base.ADVA_FIRM.Value; } set { base.ADVA_FIRM.Value = value; } }
        /// <summary>
        /// This is an overflow field for ADVA_FIRM.
        /// </summary>
        public new string ADVA_FIRM2 { get { return base.ADVA_FIRM2.Value; } set { base.ADVA_FIRM2.Value = value; } }
        /// <summary>
        /// This is the attorney's street address.
        /// </summary>
        public new string ADVA_STREET { get { return base.ADVA_STREET.Value; } set { base.ADVA_STREET.Value = value; } }
        /// <summary>
        /// This is the attorney's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
        /// </summary>
        public new string ADVA_CSZ { get { return base.ADVA_CSZ.Value; } set { base.ADVA_CSZ.Value = value; } }
        /// <summary>
        /// This is the attorney's preferred salutation in correspondence, such as "Dear Attorney Lewis:"
        /// </summary>
        public new string ADVA_SALUT { get { return base.ADVA_SALUT.Value; } set { base.ADVA_SALUT.Value = value; } }
        /// <summary>
        /// This is the attorney's phone number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string ADVA_PHONE { get { return base.ADVA_PHONE.Value; } set { base.ADVA_PHONE.Value = value; } }
        /// <summary>
        /// This is the attorney's fax number. It can accommodate separators for the area code and exchange.
        /// </summary>
        public new string ADVA_FAX { get { return base.ADVA_FAX.Value; } set { base.ADVA_FAX.Value = value; } }
        /// <summary>
        /// This is the internal file number at the debtor's attorney's firm for this account.
        /// </summary>
        public new string ADVA_FILENO { get { return base.ADVA_FILENO.Value; } set { base.ADVA_FILENO.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new DateTime? MISC_DATE1 { get { return base.MISC_DATE1.Value; } set { base.MISC_DATE1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new DateTime? MISC_DATE2 { get { return base.MISC_DATE2.Value; } set { base.MISC_DATE2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new decimal? MISC_AMT1 { get { return base.MISC_AMT1.Value; } set { base.MISC_AMT1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new decimal? MISC_AMT2 { get { return base.MISC_AMT2.Value; } set { base.MISC_AMT2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM1 { get { return base.MISC_COMM1.Value; } set { base.MISC_COMM1.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM2 { get { return base.MISC_COMM2.Value; } set { base.MISC_COMM2.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM3 { get { return base.MISC_COMM3.Value; } set { base.MISC_COMM3.Value = value; } }
        /// <summary>
        /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
        /// </summary>
        public new string MISC_COMM4 { get { return base.MISC_COMM4.Value; } set { base.MISC_COMM4.Value = value; } }
        /// <summary>
        /// This is used to distinguish up to 3 debtor attorney records. Values are 1, 2 or 3.
        /// <para>(Ex: The record 06 for the debtor's primary attorney can have ADVA_NUM = 1 and the record 06 for the debtor's secondary attorney can have ADVA_NUM = 2.)</para>
        /// <para>If a record 06 is sent with the same ADVA_NUM as a previous record 06 for the same account, the second record will overwrite the first. If you have miscellaneous information described in this record, this value must be 1.</para>
        /// </summary>
        public new int? ADVA_NUM { get { return base.ADVA_NUM.Value; } set { base.ADVA_NUM.Value = value; } }
        /// <summary>
        /// This is the standard code for the debtor attorney's country.
        /// </summary>
        public new string ADVA_CNTRY { get { return base.ADVA_CNTRY.Value; } set { base.ADVA_CNTRY.Value = value; } }
        #endregion

        public RecordType56() { }
        public RecordType56(string RT56Entry) : base(RT56Entry) { }

        public override Type GetType() { return typeof(RecordType56); }
    }
    #endregion

    #region Record Type 57
    /// <summary>
    /// Record Type 57 - Export - Legal Information
    /// <para>This record reports information regarding a suit filed against the debtor. More details can be provided in return by the receiver (agency/firm) in record 41. This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system.Value = 57.</para>
    /// </summary>
    public class RecordType57 : RecordTypes.YGC.RecordType57
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the county the suit was filed in.
        /// </summary>
        public new string CRT_COUNTY { get { return base.CRT_COUNTY.Value; } set { base.CRT_COUNTY.Value = value; } }
        /// <summary>
        /// Typically the full name of the court. Ex: Gwinnett County State Court, THE NINTH JUDICIAL CIRCUIT.
        /// </summary>
        public new string CRT_DESIG { get { return base.CRT_DESIG.Value; } set { base.CRT_DESIG.Value = value; } }
        /// <summary>
        /// This is a more brief description of the court. Ex: Superior, Supreme, Circuit
        /// </summary>
        public new string CRT_TYPE { get { return base.CRT_TYPE.Value; } set { base.CRT_TYPE.Value = value; } }
        /// <summary>
        /// This could be the official name of the sheriff's office or title, such as GLENN COUNTY SHERIFF, or the full name of the sheriff, which can be split between this field and SHER_NAME2.
        /// </summary>
        public new string SHER_NAME { get { return base.SHER_NAME.Value; } set { base.SHER_NAME.Value = value; } }
        /// <summary>
        /// This can be an overflow field for SHER_NAME.
        /// </summary>
        public new string SHER_NAME2 { get { return base.SHER_NAME2.Value; } set { base.SHER_NAME2.Value = value; } }
        /// <summary>
        /// This is the sheriff's office's street address.
        /// </summary>
        public new string SHER_STREET { get { return base.SHER_STREET.Value; } set { base.SHER_STREET.Value = value; } }
        /// <summary>
        /// This is the sheriff's office's city, state and zip code.
        /// <para>Format this field as City ST Zip or City,ST Zip.</para>
        /// <para>(Example: Linden NJ 07036 or Linden,NJ 07036)</para>
        /// </summary>
        public new string SHER_CSZ { get { return base.SHER_CSZ.Value; } set { base.SHER_CSZ.Value = value; } }
        /// <summary>
        /// This is the dollar amount requested in the suit. If this field is populated, SUIT_DATE must contain a valid date.
        /// </summary>
        public new decimal? SUIT_AMT { get { return base.SUIT_AMT.Value; } set { base.SUIT_AMT.Value = value; } }
        /// <summary>
        /// Dictated by the credit contract up front, it is added to the principal from which the receiving attorney can take a commission.
        /// </summary>
        public new decimal? CONTRACT_FEE { get { return base.CONTRACT_FEE.Value; } set { base.CONTRACT_FEE.Value = value; } }
        /// <summary>
        /// This is the statutory fee awarded only to the attorney (not remitted on), determined by the debtor state.
        /// </summary>
        public new decimal? STAT_FEE { get { return base.STAT_FEE.Value; } set { base.STAT_FEE.Value = value; } }
        /// <summary>
        /// Initial number assigned to the suit.
        /// </summary>
        public new string DOCKET_NO { get { return base.DOCKET_NO.Value; } set { base.DOCKET_NO.Value = value; } }
        /// <summary>
        /// Depending on the state the suit is filed in, a new number may be assigned upon judgment.
        /// </summary>
        public new string JUDGMENT_NO { get { return base.JUDGMENT_NO.Value; } set { base.JUDGMENT_NO.Value = value; } }
        /// <summary>
        /// If the debtor has filed for bankruptcy, this is the court-issued number.
        /// </summary>
        public new string BKCY_NO { get { return base.BKCY_NO.Value; } set { base.BKCY_NO.Value = value; } }
        /// <summary>
        /// This is the date the suit was filed. If this field is populated, SUIT_AMT must be non-zero.
        /// </summary>
        public new DateTime? SUIT_DATE { get { return base.SUIT_DATE.Value; } set { base.SUIT_DATE.Value = value; } }
        /// <summary>
        /// This is the date the judgment was entered. If this field is populated, JDGMNT_AMT must be non-zero.
        /// </summary>
        public new DateTime? JUDGMENT_DATE { get { return base.JUDGMENT_DATE.Value; } set { base.JUDGMENT_DATE.Value = value; } }
        /// <summary>
        /// This is the dollar amount awarded in the judgment. If this field is populated, JDGMNT_DATE must contain a valid date.
        /// </summary>
        public new decimal? JUDGMENT_AMT { get { return base.JUDGMENT_AMT.Value; } set { base.JUDGMENT_AMT.Value = value; } }
        /// <summary>
        /// This is the principal amount of the judgment.
        /// </summary>
        public new decimal? JUDGMENT_PRIN { get { return base.JUDGMENT_PRIN.Value; } set { base.JUDGMENT_PRIN.Value = value; } }
        /// <summary>
        /// This is the dollar amount of the interest due before the judgment was rendered.
        /// </summary>
        public new decimal? PREJ_INT { get { return base.PREJ_INT.Value; } set { base.PREJ_INT.Value = value; } }
        /// <summary>
        /// This is the sum of the costs to the sender to carry the suit forward, such as attorney fees.
        /// </summary>
        public new decimal? JUDGMENT_COSTS { get { return base.JUDGMENT_COSTS.Value; } set { base.JUDGMENT_COSTS.Value = value; } }
        /// <summary>
        /// This is the difference between what was requested in the suit and the judgment amount.
        /// </summary>
        public new decimal? ADJUSTMENT { get { return base.ADJUSTMENT.Value; } set { base.ADJUSTMENT.Value = value; } }
        /// <summary>
        /// This is the standard code for the sheriff's country.
        /// </summary>
        public new string SHER_CNTRY { get { return base.SHER_CNTRY.Value; } set { base.SHER_CNTRY.Value = value; } }
        /// <summary>
        /// This is name of the court or clerk.
        /// </summary>
        public new string CRT_NAME { get { return base.CRT_NAME.Value; } set { base.CRT_NAME.Value = value; } }
        /// <summary>
        /// This is the date when the suit judgment expires. It is usually between 7 and 12 years from the filing date.
        /// </summary>
        public new DateTime? JDGMNT_EXP_DATE { get { return base.JDGMNT_EXP_DATE.Value; } set { base.JDGMNT_EXP_DATE.Value = value; } }
        /// <summary>
        /// This is a free-form text field explaining the reason for filing suit.
        /// </summary>
        public new string SUIT_REASON { get { return base.SUIT_REASON.Value; } set { base.SUIT_REASON.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the primary debtor from the latest Record 02, Record 31 or Record 51.
        /// </summary>
        public new bool JUDGMENTONDEBTOR1 { get { return base.JUDGMENTONDEBTOR1.Value; } set { base.JUDGMENTONDEBTOR1.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 2nd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR2 { get { return base.JUDGMENTONDEBTOR2.Value; } set { base.JUDGMENTONDEBTOR2.Value = value; } }
        /// <summary>
        /// (Y/N) This denotes if the judgment is for the 3rd debtor name in the latest Record 03, Record 33 or Record 53.
        /// </summary>
        public new bool JUDGMENTONDEBTOR3 { get { return base.JUDGMENTONDEBTOR3.Value; } set { base.JUDGMENTONDEBTOR3.Value = value; } }
        /// <summary>
        /// This is the date the judgment was recorded; this may be different than JDGMNT_DATE.
        /// </summary>
        public new DateTime? JUDGMENT_RECORDED_DATE { get { return base.JUDGMENT_RECORDED_DATE.Value; } set { base.JUDGMENT_RECORDED_DATE.Value = value; } }
        /// <summary>
        /// This is the date that the suit documents were generated and sent to court, as opposed to the date the suit was filed by the court.
        /// </summary>
        public new DateTime? SUIT_ISSUED_DATE { get { return base.SUIT_ISSUED_DATE.Value; } set { base.SUIT_ISSUED_DATE.Value = value; } }
        #endregion

        public RecordType57() { }
        public RecordType57(string RT57Entry) : base(RT57Entry) { }

        public override Type GetType() { return typeof(RecordType57); }
    }
    #endregion

    #region Record Type 58
    /// <summary>
    /// Record Type 58 - Export - Caption - Legal Names
    /// <para>This record is for entering the caption; i.e., the parties named in the suit. The Plaintiffs are typically the original creditors and the Defendants are the debtors.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 58.</para>
    /// </summary>
    public class RecordType58 : RecordTypes.YGC.RecordType58
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the combination of all plaintiff fields
        /// <para>(DO NOT USE - This field will not write to output file)</para>
        /// </summary>
        public new string PLAINTIFF_COMBINED { get { return base.PLAINTIFF_COMBINED.Value; } set { base.PLAINTIFF_COMBINED.Value = value; } }
        /// <summary>
        /// This is the first plaintiff, typically the debt owner or original creditor. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_1 { get { return base.PLAINTIFF_1.Value; } set { base.PLAINTIFF_1.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the second plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_2 { get { return base.PLAINTIFF_2.Value; } set { base.PLAINTIFF_2.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the third plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_3 { get { return base.PLAINTIFF_3.Value; } set { base.PLAINTIFF_3.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the fourth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_4 { get { return base.PLAINTIFF_4.Value; } set { base.PLAINTIFF_4.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the fifth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_5 { get { return base.PLAINTIFF_5.Value; } set { base.PLAINTIFF_5.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the sixth plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_6 { get { return base.PLAINTIFF_6.Value; } set { base.PLAINTIFF_6.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous plaintiff line or for the seventh plaintiff. There is no format restriction.
        /// </summary>
        public new string PLAINTIFF_7 { get { return base.PLAINTIFF_7.Value; } set { base.PLAINTIFF_7.Value = value; } }
        /// <summary>
        /// This is the full name of the first defendant, typically the debtor. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_1 { get { return base.DEFENDANT_1.Value; } set { base.DEFENDANT_1.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the second defendant. It can also be an alias of the primary defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_2 { get { return base.DEFENDANT_2.Value; } set { base.DEFENDANT_2.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the third defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_3 { get { return base.DEFENDANT_3.Value; } set { base.DEFENDANT_3.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the fourth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_4 { get { return base.DEFENDANT_4.Value; } set { base.DEFENDANT_4.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the fifth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_5 { get { return base.DEFENDANT_5.Value; } set { base.DEFENDANT_5.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the sixth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_6 { get { return base.DEFENDANT_6.Value; } set { base.DEFENDANT_6.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the seventh defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_7 { get { return base.DEFENDANT_7.Value; } set { base.DEFENDANT_7.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the eighth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_8 { get { return base.DEFENDANT_8.Value; } set { base.DEFENDANT_8.Value = value; } }
        /// <summary>
        /// This can be used as overflow for the previous defendant line or for the ninth defendant. There is no format restriction.
        /// </summary>
        public new string DEFENDANT_9 { get { return base.DEFENDANT_9.Value; } set { base.DEFENDANT_9.Value = value; } }
        #endregion

        public RecordType58() { }
        public RecordType58(string RT58Entry) : base(RT58Entry) { }

        public override Type GetType() { return typeof(RecordType58); }
    }
    #endregion

    #region Record Type 59
    /// <summary>
    /// Record Type 59 - Export - Message
    /// <para>Senders communicate status updates to their receivers with this record type. It should include the PCODE to clearly identify the update. Examples of status updates are direct payment, suit filed, account refused, judgment issued and account closed.This record type is sent by a receiver on the sender's behalf if the sender is unable to send the account to YGC. YouveGotDataDelivery uploads the records to YouveGotReports but does not send them to the receiver's inbox since the information is already in the receiver's system. Value = 59.</para>
    /// </summary>
    public class RecordType59 : RecordTypes.YGC.RecordType59
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
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the date on which the event reported occurred.
        /// </summary>
        public new DateTime? PDATE { get { return base.PDATE.Value; } set { base.PDATE.Value = value; } }
        /// <summary>
        /// This field indicates the type of status update. Some codes trigger changes to the account in YouveGotReports. See Status Codes tab for details; YGC highly recommends using a PCODE.
        /// </summary>
        public new string PCODE { get { return base.PCODE.Value; } set { base.PCODE.Value = value; } }
        /// <summary>
        /// This is a free-text comment or description to accompany the message. It should have a CRLF after the last character, which therefore means you don't have to right-pad the field with spaces to fill all 1024 characters.
        /// </summary>
        public new string PCMT { get { return base.PCMT.Value; } set { base.PCMT.Value = value; } }
        /// <summary>
        /// Note 2
        /// </summary>
        public new string NOTE02 { get { return base.NOTE02.Value; } set { base.NOTE02.Value = value; } }
        /// <summary>
        /// Note 3
        /// </summary>
        public new string NOTE03 { get { return base.NOTE03.Value; } set { base.NOTE03.Value = value; } }
        /// <summary>
        /// Note 4
        /// </summary>
        public new string NOTE04 { get { return base.NOTE04.Value; } set { base.NOTE04.Value = value; } }
        /// <summary>
        /// Note 5
        /// </summary>
        public new string NOTE05 { get { return base.NOTE05.Value; } set { base.NOTE05.Value = value; } }
        /// <summary>
        /// Note 6
        /// </summary>
        public new string NOTE06 { get { return base.NOTE06.Value; } set { base.NOTE06.Value = value; } }
        /// <summary>
        /// Note 7
        /// </summary>
        public new string NOTE07 { get { return base.NOTE07.Value; } set { base.NOTE07.Value = value; } }
        /// <summary>
        /// Note 8
        /// </summary>
        public new string NOTE08 { get { return base.NOTE08.Value; } set { base.NOTE08.Value = value; } }
        /// <summary>
        /// Note 9
        /// </summary>
        public new string NOTE09 { get { return base.NOTE09.Value; } set { base.NOTE09.Value = value; } }
        /// <summary>
        /// Note 10
        /// </summary>
        public new string NOTE10 { get { return base.NOTE10.Value; } set { base.NOTE10.Value = value; } }
        /// <summary>
        /// Note 11
        /// </summary>
        public new string NOTE11 { get { return base.NOTE11.Value; } set { base.NOTE11.Value = value; } }
        /// <summary>
        /// Note 12
        /// </summary>
        public new string NOTE12 { get { return base.NOTE12.Value; } set { base.NOTE12.Value = value; } }
        /// <summary>
        /// Note 13
        /// </summary>
        public new string NOTE13 { get { return base.NOTE13.Value; } set { base.NOTE13.Value = value; } }
        /// <summary>
        /// Note 14
        /// </summary>
        public new string NOTE14 { get { return base.NOTE14.Value; } set { base.NOTE14.Value = value; } }
        /// <summary>
        /// Note 15
        /// </summary>
        public new string NOTE15 { get { return base.NOTE15.Value; } set { base.NOTE15.Value = value; } }
        /// <summary>
        /// Note 16
        /// </summary>
        public new string NOTE16 { get { return base.NOTE16.Value; } set { base.NOTE16.Value = value; } }
        /// <summary>
        /// Note 17
        /// </summary>
        public new string NOTE17 { get { return base.NOTE17.Value; } set { base.NOTE17.Value = value; } }
        /// <summary>
        /// This is the time on which the event reported occurred. The format is HHMMSS on a 24-hour clock.
        /// </summary>
        public new DateTime? PTIME { get { return base.PTIME.Value; } set { base.PTIME.Value = value; } }
        /// <summary>
        /// Local time zone of the sender delivering the status update.Must be expressed in GMT format. For example:
        /// <para>GMT-5 = USA Eastern</para>
        /// <para>GMT-6 = USA Central</para>
        /// <para>GMT-7 = USA Mountain</para>
        /// <para>GMT-8 = USA Pacific</para>
        /// <para>GMT-9 = Alaska</para>
        /// <para>GMT-10 = Hawaii</para>
        /// </summary>
        public new string PTIME_ZONE { get { return base.PTIME_ZONE.Value; } set { base.PTIME_ZONE.Value = value; } }
        /// <summary>
        /// Phone number dialed to contact the debtor.
        /// </summary>
        public new string PHONE_NUMBER { get { return base.PHONE_NUMBER.Value; } set { base.PHONE_NUMBER.Value = value; } }
        /// <summary>
        /// Call Direction
        /// <para>Values: I/O. Incoming or outgoing.</para>
        /// </summary>
        public new CallDirection CALL_DIRECTION { get { return base.CALL_DIRECTION.Value; } set { base.CALL_DIRECTION.Value = value; } }
        /// <summary>
        /// Debtor
        /// <para>Values: 1 = Debtor 1, 2 = Debtor 2.</para>
        /// </summary>
        public new int? DBTR_TYPE { get { return base.DBTR_TYPE.Value; } set { base.DBTR_TYPE.Value = value; } }
        #endregion

        public RecordType59() { }
        public RecordType59(string RT59Entry) : base(RT59Entry) { }

        public override Type GetType() { return typeof(RecordType59); }
    }
    #endregion

    #region Record Type 70
    /// <summary>
    /// Record Type 70 - 
    /// </summary>
    public class RecordType70 : RecordTypes.YGC.RecordType70
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// Forwarder Number
        /// </summary>
        public new int? FORW_NO { get { return base.FORW_NO.Value; } set { base.FORW_NO.Value = value; } }
        /// <summary>
        /// Co-Counsel Number
        /// </summary>
        public new int? MASCO_NO { get { return base.MASCO_NO.Value; } set { base.MASCO_NO.Value = value; } }
        /// <summary>
        /// Adversary Attorney Number
        /// </summary>
        public new int? ADVA_NO { get { return base.ADVA_NO.Value; } set { base.ADVA_NO.Value = value; } }
        /// <summary>
        /// Our Secretary/Paralegal Number
        /// </summary>
        public new int? SECY_NO { get { return base.SECY_NO.Value; } set { base.SECY_NO.Value = value; } }
        /// <summary>
        /// Our Attorney Number
        /// </summary>
        public new int? ATTY_NO { get { return base.ATTY_NO.Value; } set { base.ATTY_NO.Value = value; } }
        /// <summary>
        /// Our Collector Number
        /// </summary>
        public new int? COLL_NO { get { return base.COLL_NO.Value; } set { base.COLL_NO.Value = value; } }
        /// <summary>
        /// Venue 1 Number
        /// </summary>
        public new int? VENUE1_NO { get { return base.VENUE1_NO.Value; } set { base.VENUE1_NO.Value = value; } }
        /// <summary>
        /// Venue 2 Number
        /// </summary>
        public new int? VENUE2_NO { get { return base.VENUE2_NO.Value; } set { base.VENUE2_NO.Value = value; } }
        /// <summary>
        /// Sheriff 1 Number
        /// </summary>
        public new int? SHERIFF1_NO { get { return base.SHERIFF1_NO.Value; } set { base.SHERIFF1_NO.Value = value; } }
        /// <summary>
        /// Sheriff 2 Number
        /// </summary>
        public new int? SHERIFF2_NO { get { return base.SHERIFF2_NO.Value; } set { base.SHERIFF2_NO.Value = value; } }
        /// <summary>
        /// Bank 1 Number
        /// </summary>
        public new int? BANK1_NO { get { return base.BANK1_NO.Value; } set { base.BANK1_NO.Value = value; } }
        /// <summary>
        /// Bank 2 Number
        /// </summary>
        public new int? BANK2_NO { get { return base.BANK2_NO.Value; } set { base.BANK2_NO.Value = value; } }
        /// <summary>
        /// Bank 3 Number
        /// </summary>
        public new int? BANK3_NO { get { return base.BANK3_NO.Value; } set { base.BANK3_NO.Value = value; } }
        /// <summary>
        /// Employer 1 Number
        /// </summary>
        public new int? EMPL1_NO { get { return base.EMPL1_NO.Value; } set { base.EMPL1_NO.Value = value; } }
        /// <summary>
        /// Employer 2 Number
        /// </summary>
        public new int? EMPL2_NO { get { return base.EMPL2_NO.Value; } set { base.EMPL2_NO.Value = value; } }
        /// <summary>
        /// Employer 3 Number
        /// </summary>
        public new int? EMPL3_NO { get { return base.EMPL3_NO.Value; } set { base.EMPL3_NO.Value = value; } }
        /// <summary>
        /// Status Code 1 (Date)
        /// </summary>
        public new DateTime? STATUS1_D { get { return base.STATUS1_D.Value; } set { base.STATUS1_D.Value = value; } }
        /// <summary>
        /// Status Code 1 (Code)
        /// </summary>
        public new int? STATUS1_CODE { get { return base.STATUS1_CODE.Value; } set { base.STATUS1_CODE.Value = value; } }
        /// <summary>
        /// Status Code 2 (Date)
        /// </summary>
        public new DateTime? STATUS2_D { get { return base.STATUS2_D.Value; } set { base.STATUS2_D.Value = value; } }
        /// <summary>
        /// Status Code 2 (Code)
        /// </summary>
        public new int? STATUS2_CODE { get { return base.STATUS2_CODE.Value; } set { base.STATUS2_CODE.Value = value; } }
        /// <summary>
        /// Internal Forwarder Reference Number
        /// </summary>
        public new string FREF { get { return base.FREF.Value; } set { base.FREF.Value = value; } }
        /// <summary>
        /// Co-Counsel Commission Rate
        /// </summary>
        public new decimal? CCOMM { get { return base.CCOMM.Value; } set { base.CCOMM.Value = value; } }
        /// <summary>
        /// Co-Counsel Suit Fee Rate
        /// </summary>
        public new decimal? CSFEE { get { return base.CSFEE.Value; } set { base.CSFEE.Value = value; } }
        /// <summary>
        /// Number of Debtors
        /// </summary>
        public new int? DNO { get { return base.DNO.Value; } set { base.DNO.Value = value; } }
        /// <summary>
        /// Adversary Attorney 2 Number
        /// </summary>
        public new int? ADVA_NO2 { get { return base.ADVA_NO2.Value; } set { base.ADVA_NO2.Value = value; } }
        /// <summary>
        /// Adversary Attorney 3 Number
        /// </summary>
        public new int? ADVA_NO3 { get { return base.ADVA_NO3.Value; } set { base.ADVA_NO3.Value = value; } }
        /// <summary>
        /// Sales Person
        /// </summary>
        public new int? SALES { get { return base.SALES.Value; } set { base.SALES.Value = value; } }
        #endregion

        public RecordType70() : base() { }
        public RecordType70(string RT70Entry) : base(RT70Entry) { }

        public override Type GetType() { return typeof(RecordType70); }
    }
    #endregion

    #region Record Type 71
    /// <summary>
    /// Record Type 71 - Account Card Values
    /// </summary>
    public class RecordType71 : RecordTypes.YGC.RecordType71
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Original Claim Amount
        /// </summary>
        public new decimal? ORIG_CLAIM { get { return base.ORIG_CLAIM.Value; } set { base.ORIG_CLAIM.Value = value; } }
        /// <summary>
        /// Suit Amount
        /// </summary>
        public new decimal? SUIT_AMT { get { return base.SUIT_AMT.Value; } set { base.SUIT_AMT.Value = value; } }
        /// <summary>
        /// Statutory Attorney Fees Accrued
        /// </summary>
        public new decimal? STAT_FEE { get { return base.STAT_FEE.Value; } set { base.STAT_FEE.Value = value; } }
        /// <summary>
        /// Judgment Amount
        /// </summary>
        public new decimal? JMT_AMT { get { return base.JMT_AMT.Value; } set { base.JMT_AMT.Value = value; } }
        /// <summary>
        /// Debtor Balance
        /// </summary>
        public new decimal? DEBT_BAL { get { return base.DEBT_BAL.Value; } set { base.DEBT_BAL.Value = value; } }
        /// <summary>
        /// Amount of Interest Accrued
        /// </summary>
        public new decimal? INT_DUE { get { return base.INT_DUE.Value; } set { base.INT_DUE.Value = value; } }
        /// <summary>
        /// Date interest was calculated
        /// </summary>
        public new DateTime? INT_DATE { get { return base.INT_DATE.Value; } set { base.INT_DATE.Value = value; } }
        /// <summary>
        /// Claim Adjusted Amount
        /// </summary>
        public new decimal? C_ADJUST { get { return base.C_ADJUST.Value; } set { base.C_ADJUST.Value = value; } }
        /// <summary>
        /// Contract Interest Fee
        /// </summary>
        public new decimal? CNTRACT_INT { get { return base.CNTRACT_INT.Value; } set { base.CNTRACT_INT.Value = value; } }
        /// <summary>
        /// Affidavit Amount
        /// </summary>
        public new decimal? AFFIDAVIT { get { return base.AFFIDAVIT.Value; } set { base.AFFIDAVIT.Value = value; } }
        /// <summary>
        /// Pre Judgment Interest
        /// </summary>
        public new decimal? ACCR_INT_BEF { get { return base.ACCR_INT_BEF.Value; } set { base.ACCR_INT_BEF.Value = value; } }
        /// <summary>
        /// Compromise Amount (Settlement)
        /// </summary>
        public new decimal? COMPR_AMT { get { return base.COMPR_AMT.Value; } set { base.COMPR_AMT.Value = value; } }
        /// <summary>
        /// Overpaid Amount
        /// </summary>
        public new decimal? OVERPAY_AMT { get { return base.OVERPAY_AMT.Value; } set { base.OVERPAY_AMT.Value = value; } }
        /// <summary>
        /// Collect & Hold
        /// </summary>
        public new decimal? COLL_HOLD { get { return base.COLL_HOLD.Value; } set { base.COLL_HOLD.Value = value; } }
        /// <summary>
        /// Principal Collected
        /// </summary>
        public new decimal? PRIN_COLL { get { return base.PRIN_COLL.Value; } set { base.PRIN_COLL.Value = value; } }
        /// <summary>
        /// Interest Collected
        /// </summary>
        public new decimal? INT_COLL { get { return base.INT_COLL.Value; } set { base.INT_COLL.Value = value; } }
        /// <summary>
        /// Commissions Received
        /// </summary>
        public new decimal? COMM_EARN { get { return base.COMM_EARN.Value; } set { base.COMM_EARN.Value = value; } }
        /// <summary>
        /// Suit Fees Received
        /// </summary>
        public new decimal? SFEE_EARN { get { return base.SFEE_EARN.Value; } set { base.SFEE_EARN.Value = value; } }
        /// <summary>
        /// Misc/Stat Fees Received
        /// </summary>
        public new decimal? STAT_EARN { get { return base.STAT_EARN.Value; } set { base.STAT_EARN.Value = value; } }
        /// <summary>
        /// Sales Tax Collected
        /// </summary>
        public new decimal? SALES_TAX { get { return base.SALES_TAX.Value; } set { base.SALES_TAX.Value = value; } }
        /// <summary>
        /// Merchandise Returned (Pre-Suit)
        /// </summary>
        public new decimal? MERCH_BEF { get { return base.MERCH_BEF.Value; } set { base.MERCH_BEF.Value = value; } }
        /// <summary>
        /// Merchandise Returned (Post-Suit)
        /// </summary>
        public new decimal? MERCH_POST { get { return base.MERCH_POST.Value; } set { base.MERCH_POST.Value = value; } }
        /// <summary>
        /// Merchandise Returned (Post-Judg)
        /// </summary>
        public new decimal? MERCH_POST_JUDG { get { return base.MERCH_POST_JUDG.Value; } set { base.MERCH_POST_JUDG.Value = value; } }
        /// <summary>
        /// Debtor Payments (Pre-Suit)
        /// </summary>
        public new decimal? CASH_BEF { get { return base.CASH_BEF.Value; } set { base.CASH_BEF.Value = value; } }
        /// <summary>
        /// Debtor Payments (Post-Suit)
        /// </summary>
        public new decimal? CASH_POST { get { return base.CASH_POST.Value; } set { base.CASH_POST.Value = value; } }
        /// <summary>
        /// Debtor Payments (Post-Judg)
        /// </summary>
        public new decimal? PAID_POST_JUDG { get { return base.PAID_POST_JUDG.Value; } set { base.PAID_POST_JUDG.Value = value; } }
        /// <summary>
        /// Direct Pay - No Fee due (Pre Suit)
        /// </summary>
        public new decimal? DP_PRE_SUIT_NF { get { return base.DP_PRE_SUIT_NF.Value; } set { base.DP_PRE_SUIT_NF.Value = value; } }
        /// <summary>
        /// Direct Pay - No Fee due (Post Suit)
        /// </summary>
        public new decimal? DP_POST_SUIT_NF { get { return base.DP_POST_SUIT_NF.Value; } set { base.DP_POST_SUIT_NF.Value = value; } }
        /// <summary>
        /// Direct Pay - No Fee due (Post Judg)
        /// </summary>
        public new decimal? DP_POST_JUDG_NF { get { return base.DP_POST_JUDG_NF.Value; } set { base.DP_POST_JUDG_NF.Value = value; } }
        /// <summary>
        /// Direct Pay - (All DP) (Pre Suit)
        /// </summary>
        public new decimal? DP_PRE_SUIT { get { return base.DP_PRE_SUIT.Value; } set { base.DP_PRE_SUIT.Value = value; } }
        /// <summary>
        /// Direct Pay - (All DP) (Post Suit)
        /// </summary>
        public new decimal? DP_POST_SUIT { get { return base.DP_POST_SUIT.Value; } set { base.DP_POST_SUIT.Value = value; } }
        /// <summary>
        /// Direct Pay - (All DP) (Post Judg)
        /// </summary>
        public new decimal? DP_POST_JUDG { get { return base.DP_POST_JUDG.Value; } set { base.DP_POST_JUDG.Value = value; } }
        /// <summary>
        /// Collected by Co-Co (Pre Suit)
        /// </summary>
        public new decimal? AGENT_BEFSCOLL { get { return base.AGENT_BEFSCOLL.Value; } set { base.AGENT_BEFSCOLL.Value = value; } }
        /// <summary>
        /// Collected by Co-Co (Post Suit)
        /// </summary>
        public new decimal? AGENT_PSCOLL { get { return base.AGENT_PSCOLL.Value; } set { base.AGENT_PSCOLL.Value = value; } }
        /// <summary>
        /// Collected by Co-Co (Post Judg)
        /// </summary>
        public new decimal? AGENT_PJCOLL { get { return base.AGENT_PJCOLL.Value; } set { base.AGENT_PJCOLL.Value = value; } }
        /// <summary>
        /// Costs Received (From Client)
        /// </summary>
        public new decimal? COST_RECEIVED { get { return base.COST_RECEIVED.Value; } set { base.COST_RECEIVED.Value = value; } }
        /// <summary>
        /// Costs Returned (To Client)
        /// </summary>
        public new decimal? COST_RET { get { return base.COST_RET.Value; } set { base.COST_RET.Value = value; } }
        /// <summary>
        /// Costs Balance (Still on Hand)
        /// </summary>
        public new decimal? COST_BAL { get { return base.COST_BAL.Value; } set { base.COST_BAL.Value = value; } }
        /// <summary>
        /// Non Recoverable Costs Expended
        /// </summary>
        public new decimal? NON_RCVRD_COST { get { return base.NON_RCVRD_COST.Value; } set { base.NON_RCVRD_COST.Value = value; } }
        /// <summary>
        /// Costs Expended Post Judgment
        /// </summary>
        public new decimal? COST_POST_JUDG { get { return base.COST_POST_JUDG.Value; } set { base.COST_POST_JUDG.Value = value; } }
        /// <summary>
        /// Costs Expended to Co-Co
        /// </summary>
        public new decimal? AGENT_COST { get { return base.AGENT_COST.Value; } set { base.AGENT_COST.Value = value; } }
        /// <summary>
        /// Costs Recovered Via Tax Rebate
        /// </summary>
        public new decimal? TAX_REBATE { get { return base.TAX_REBATE.Value; } set { base.TAX_REBATE.Value = value; } }
        /// <summary>
        /// Total Costs Expended
        /// </summary>
        public new decimal? TOT_EXP_COST { get { return base.TOT_EXP_COST.Value; } set { base.TOT_EXP_COST.Value = value; } }
        /// <summary>
        /// Recoverable Costs Expended
        /// </summary>
        public new decimal? COST_EXP { get { return base.COST_EXP.Value; } set { base.COST_EXP.Value = value; } }
        /// <summary>
        /// Costs Recovered
        /// </summary>
        public new decimal? COST_RECOVERED { get { return base.COST_RECOVERED.Value; } set { base.COST_RECOVERED.Value = value; } }
        /// <summary>
        /// Firm Costs Expended
        /// </summary>
        public new decimal? EXP_COST { get { return base.EXP_COST.Value; } set { base.EXP_COST.Value = value; } }
        #endregion

        public RecordType71() : base() { }
        public RecordType71(string RT71Entry) : base(RT71Entry) { }

        public override Type GetType() { return typeof(RecordType71); }

    }
    #endregion

    #region Record Type 95
    /// <summary>
    /// Record Type 95 - Diary Entry
    /// </summary>
    public class RecordType95 : RecordTypes.YGC.RecordType95
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
        public new string MASCO_FILE { get { return base.MASCO_FILE.Value; } set { base.MASCO_FILE.Value = value; } }
        /// <summary>
        /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FORW_ID { get { return base.FORW_ID.Value; } set { base.FORW_ID.Value = value; } }
        /// <summary>
        /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
        /// </summary>
        public new string FIRM_ID { get { return base.FIRM_ID.Value; } set { base.FIRM_ID.Value = value; } }
        /// <summary>
        /// Claim Diary Date
        /// </summary>
        public new DateTime? DIARY_DATE { get { return base.DIARY_DATE.Value; } set { base.DIARY_DATE.Value = value; } }
        /// <summary>
        /// Claim Diary Code
        /// </summary>
        public new int? DIARY_CODE { get { return base.DIARY_CODE.Value; } set { base.DIARY_CODE.Value = value; } }
        /// <summary>
        /// Claim Diary Comment
        /// </summary>
        public new string DIARY_CMT { get { return base.DIARY_CMT.Value; } set { base.DIARY_CMT.Value = value; } }
        /// <summary>
        /// Claim Diary Queue
        /// </summary>
        public new string DIARY_QUEUE { get { return base.DIARY_QUEUE.Value; } set { base.DIARY_QUEUE.Value = value; } }
        /// <summary>
        /// Claim Diary Time
        /// </summary>
        public new DateTime? DIARY_TIME { get { return base.DIARY_TIME.Value; } set { base.DIARY_TIME.Value = value; } }
        /// <summary>
        /// Claim Diary Priority
        /// </summary>
        public new int? DIARY_PRIORITY { get { return base.DIARY_PRIORITY.Value; } set { base.DIARY_PRIORITY.Value = value; } }
        #endregion

        public RecordType95()
            : base() { }
        public RecordType95(string RT95Entry)
            : base(RT95Entry) { }

        public override Type GetType() { return typeof(RecordType95); }
    }
    #endregion

}
