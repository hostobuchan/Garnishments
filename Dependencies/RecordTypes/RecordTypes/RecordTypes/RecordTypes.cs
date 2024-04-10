using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RecordTypes
{
    #region Record Type 01
    public class RT01Table
    {
        private List<RT01> _Rows = new List<RT01>();
        public List<RT01> RT01Records { get { return _Rows; } }
        public RT01 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT01
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _DATE_FORW = "".PadRight(8, ' ');
            private string _LAW_LIST = "".PadRight(4, ' ');
            private string _COMM = "".PadRight(4, ' ');
            private string _SFEE = "".PadRight(4, ' ');
            private string _ORIG_CLAIM = "".PadRight(14, ' ');
            private string _ORIG_INT = "".PadRight(14, ' ');
            private string _ORIG_INT_D = "".PadRight(8, ' ');
            private string _RATES_PRE = "".PadRight(5, ' ');
            private string _RATES_POST = "".PadRight(5, ' ');
            private string _CRED_NAME = "".PadRight(25, ' ');
            private string _CRED_NAME2 = "".PadRight(25, ' ');
            private string _CRED_STREET = "".PadRight(25, ' ');
            private string _CRED_CS = "".PadRight(15, ' ');
            private string _CRED_ZIP = "".PadRight(9, ' ');
            private string _DEBT_BAL = "".PadRight(14, ' ');
            private string _CTYPE = "".PadRight(4, ' ');
            private string _DATE_LPAY = "".PadRight(8, ' ');
            private string _AMT_LPAY = "".PadRight(14, ' ');
            private string _DATE_OPEN = "".PadRight(8, ' ');
            private string _CHRG_OFF_D = "".PadRight(8, ' ');
            private string _CHRG_OFF_A = "".PadRight(14, ' ');
            private string _PURCHASE_D = "".PadRight(8, ' ');
            private string _ORIG_CRED = "".PadRight(30, ' ');
            private string _ORIG_CRED2 = "".PadRight(30, ' ');
            private string _PORT_ID = "".PadRight(20, ' ');
            private string _CRED_CNTRY = "".PadRight(3, ' ');
            private string _LPAY_ISS_D = "".PadRight(8, ' ');
            private string _LPAY_ISS_AMT = "".PadRight(14, ' ');
            private string _MEDIA = "".PadRight(1, ' ');
            private string _DELINQ_D = "".PadRight(8, ' ');
            private string _ACCEL_D = "".PadRight(8, ' ');
            private string _REPO_D = "".PadRight(8, ' ');
            private string _SALE_D = "".PadRight(8, ' ');
            private string _MATUR_D = "".PadRight(8, ' ');
            private string _SOL_START_D = "".PadRight(8, ' ');
            private string _SOL_END_D = "".PadRight(8, ' ');
            private string _LATE_FEE = "".PadRight(14, ' ');
            private string _HIST_CRED1 = "".PadRight(50, ' ');
            private string _HIST_CRED2 = "".PadRight(50, ' ');
            private string _HIST_CRED3 = "".PadRight(50, ' ');
            private string _HIST_CRED4 = "".PadRight(50, ' ');
            private string _HIST_CRED5 = "".PadRight(50, ' ');
            #endregion

            #region Public Properties
            /// <summary>
            /// This record is required to create a new account. It identifies the creditor and current owner of the debt and establishes the current financial state of the debt. Value = 01.
            /// </summary>
            public string RECORD { get { return "01"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the date the account was first sent to the receiver to work, regardless of when it was uploaded to YouveGotClaims®. If this field is left blank, YouveGotReports will automatically fill it with the date this record is processed.
            /// </summary>
            public DateTime? DATE_FORW { get { try { return DateTime.ParseExact(this._DATE_FORW, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._DATE_FORW = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
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
            public string LAW_LIST { get { return this._LAW_LIST.Trim(); } set { this._LAW_LIST = value.Length < 4 ? value.PadRight(4, ' ') : value.Substring(0, 4); } }
            /// <summary>
            /// This is the commission percentage the receiver earns as determined by the sender. There is no set numeric format.
            /// </summary>
            public string COMM { get { return this._COMM.Trim(); } set { this._COMM = value.Length < 4 ? value.PadRight(4, ' ') : value.Substring(0, 4); } }
            /// <summary>
            /// This is the percentage the receiver earns if a suit is filed. There is no set numeric format.
            /// </summary>
            public string SFEE { get { return this._SFEE.Trim(); } set { this._SFEE = value.Length < 4 ? value.PadRight(4, ' ') : value.Substring(0, 4); } }
            /// <summary>
            /// This is the dollar amount of the original principal due at time of placement.
            /// </summary>
            public decimal ORIG_CLAIM { get { try { return decimal.Parse(this._ORIG_CLAIM); } catch { return 0; } } set { this._ORIG_CLAIM = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// This is the dollar amount of accrued interest.
            /// </summary>
            public decimal ORIG_INT { get { try { return decimal.Parse(this._ORIG_INT); } catch { return 0; } } set { this._ORIG_INT = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// This is the last day of the accrual period.
            /// </summary>
            public DateTime? ORIG_INT_D { get { try { return DateTime.ParseExact(this._ORIG_INT_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._ORIG_INT_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the interest rate defined by the contract between the creditor and debtor. It takes the decimal form:
            /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
            /// </summary>
            public decimal RATES_PRE { get { try { return decimal.Parse(this._RATES_PRE); } catch { return 0; } } set { this._RATES_PRE = value < 1 ? value.ToString(".0000") : ".0000"; } }
            /// <summary>
            /// Upon judgment, this is the interest rate applied as dictated by the debtor state.  It takes the decimal form:
            /// <para>Ex: .1950 is the value of this field if the interest rate is 19.5%.</para>
            /// </summary>
            public decimal RATES_POST { get { try { return decimal.Parse(this._RATES_POST); } catch { return 0; } } set { this._RATES_POST = value < 1 ? value.ToString(".0000") : ".0000"; } }
            /// <summary>
            /// This is the company name of either the original creditor or debt purchaser/collection agency.
            /// </summary>
            public string CRED_NAME { get { return this._CRED_NAME.Trim(); } set { this._CRED_NAME = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is an overflow field for CRED_NAME.
            /// </summary>
            public string CRED_NAME2 { get { return this._CRED_NAME2.Trim(); } set { this._CRED_NAME2 = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// Street address of company in CRED_NAME.
            /// </summary>
            public string CRED_STREET { get { return this._CRED_STREET.Trim(); } set { this._CRED_STREET = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// City and state of company in CRED_NAME. Format this field as City ST or City,ST.
            /// <para>(Example: Linden NJ or Linden,NJ)</para>
            /// </summary>
            public string CRED_CS { get { return this._CRED_CS.Trim(); } set { this._CRED_CS = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the creditor's zip code. It can accommodate the four-digit extension if you do not include the hyphen.
            /// </summary>
            public string CRED_ZIP { get { return this._CRED_ZIP.Trim(); } set { this._CRED_ZIP = value.Length < 9 ? value.PadRight(9, ' ') : value.Substring(0, 9); } }
            /// <summary>
            /// This field represents everything the debtor owes at time of placement, excluding interest. It equals ORIG_CLAIM + LATE_FEE + CNTRCT_FEE + STAT_FEE + JDG_COSTS, where the last three fields are from a previous legal action defined in Record 07 and LATE_FEE is from this record.
            /// </summary>
            public decimal DEBT_BAL { get { try { return decimal.Parse(this._DEBT_BAL); } catch { return 0; } } set { this._DEBT_BAL = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// User-defined. Suggested Codes:  ARB = Arbitration, MED = Medical, CARD = Credit Card, LOAN = loan
            /// </summary>
            public string CTYPE { get { return this._CTYPE.Trim(); } set { this._CTYPE = value.Length < 4 ? value.PadRight(4, ' ') : value.Substring(0, 4); } }
            /// <summary>
            /// This is the date the last payment was received by the creditor.
            /// </summary>
            public DateTime? DATE_LPAY { get { try { return DateTime.ParseExact(this._DATE_LPAY, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._DATE_LPAY = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the dollar amount of the last payment made to the creditor.
            /// </summary>
            public decimal AMT_LPAY { get { try { return decimal.Parse(this._AMT_LPAY); } catch { return 0; } } set { this._AMT_LPAY = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// Typically, the date the credit card account was opened.
            /// </summary>
            public DateTime? DATE_OPEN { get { try { return DateTime.ParseExact(this._DATE_OPEN, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._DATE_OPEN = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date the creditor has determined the debt will not be paid through normal channels.
            /// </summary>
            public DateTime? CHRG_OFF_D { get { try { return DateTime.ParseExact(this._CHRG_OFF_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._CHRG_OFF_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is typically the same as ORIG_CLAIM.
            /// </summary>
            public decimal CHRG_OFF_A { get { try { return decimal.Parse(this._CHRG_OFF_A); } catch { return 0; } } set { this._CHRG_OFF_A = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// If this debt was purchased, this field holds the date of purchase.
            /// </summary>
            public DateTime? PURCHASE_D { get { try { return DateTime.ParseExact(this._PURCHASE_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._PURCHASE_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is only filled out if this debt was purchased or if the sender is a collection agency. It holds the name of the original creditor company.
            /// </summary>
            public string ORIG_CRED { get { return this._ORIG_CRED.Trim(); } set { this._ORIG_CRED = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is an overflow field for ORIG_CRED.
            /// </summary>
            public string ORIG_CRED2 { get { return this._ORIG_CRED2.Trim(); } set { this._ORIG_CRED2 = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// Typically pertains to purchased debt, and holds the number assigned to the batch of accounts purchased. Some reports in YouveGotReports can be filtered against this code.
            /// </summary>
            public string PORT_ID { get { return this._PORT_ID.Trim(); } set { this._PORT_ID = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the standard code for the creditor's country.
            /// </summary>
            public string CRED_CNTRY { get { return this._CRED_CNTRY.Trim(); } set { this._CRED_CNTRY = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 3); } }
            /// <summary>
            /// This is the date the last payment was received by the issuer.
            /// </summary>
            public DateTime? LPAY_ISS_D { get { try { return DateTime.ParseExact(this._LPAY_ISS_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._LPAY_ISS_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the amount of the last payment received by the issuer.
            /// </summary>
            public decimal LPAY_ISS_AMT { get { try { return decimal.Parse(this._LPAY_ISS_AMT); } catch { return 0; } } set { this._LPAY_ISS_AMT = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// This flag indicates whether the sender has media to support a lawsuit. It is up to the receiver to determine what kind of media will ultimately be required, at which time they will send a request. Use values Y or N.
            /// </summary>
            public string MEDIA { get { return this._MEDIA.Trim(); } set { this._MEDIA = value.Length < 1 ? value.PadRight(1, ' ') : value.Substring(0, 1); } }
            /// <summary>
            /// Date of Delinquency
            /// </summary>
            public DateTime? DELINQ_D { get { try { return DateTime.ParseExact(this._DELINQ_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._DELINQ_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Date of Acceleration
            /// </summary>
            public DateTime? ACCEL_D { get { try { return DateTime.ParseExact(this._ACCEL_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._ACCEL_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Date of Repossession
            /// </summary>
            public DateTime? REPO_D { get { try { return DateTime.ParseExact(this._REPO_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._REPO_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Sale Date
            /// </summary>
            public DateTime? SALE_D { get { try { return DateTime.ParseExact(this._SALE_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._SALE_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Maturity Date
            /// </summary>
            public DateTime? MATUR_D { get { try { return DateTime.ParseExact(this._MATUR_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._MATUR_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// The Statute of Limitations Start Date is calculated by the sender.
            /// </summary>
            public DateTime? SOL_START_D { get { try { return DateTime.ParseExact(this._SOL_START_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._SOL_START_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// The Statute of Limitations End Date is calculated by the sender. You can also refer to the Documents tab, Business Reference category for the current statutes for each state.
            /// </summary>
            public DateTime? SOL_END_D { get { try { return DateTime.ParseExact(this._SOL_END_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._SOL_END_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the accumulation of late fees for non-payment before placement. This should be included in the calculation of DEBT_BAL.
            /// </summary>
            public decimal LATE_FEE { get { try { return decimal.Parse(this._LATE_FEE); } catch { return 0; } } set { this._LATE_FEE = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// This represents the name of the creditor for the account before the current CRED_NAME or ORIG_CRED if present. If ORIG_CRED is not present, HIST_CRED1 should NOT be used in place of ORIG_CRED.
            /// </summary>
            public string HIST_CRED1 { get { return this._HIST_CRED1.Trim(); } set { this._HIST_CRED1 = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED1.
            /// </summary>
            public string HIST_CRED2 { get { return this._HIST_CRED2.Trim(); } set { this._HIST_CRED2 = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED2.
            /// </summary>
            public string HIST_CRED3 { get { return this._HIST_CRED3.Trim(); } set { this._HIST_CRED3 = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED3.
            /// </summary>
            public string HIST_CRED4 { get { return this._HIST_CRED4.Trim(); } set { this._HIST_CRED4 = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This represents the name of the creditor for the account before HIST_CRED4.
            /// </summary>
            public string HIST_CRED5 { get { return this._HIST_CRED5.Trim(); } set { this._HIST_CRED5 = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            #endregion

            public RT01(string RT01Entry)
            {
                try
                {
                    this._FILENO = RT01Entry.Substring(2, 10);
                    this._FORW_FILE = RT01Entry.Substring(12, 20);
                    this._MASCO_FILE = RT01Entry.Substring(32, 15);
                    this._FORW_ID = RT01Entry.Substring(47, 10);
                    this._FIRM_ID = RT01Entry.Substring(57, 10);
                    this._DATE_FORW = RT01Entry.Substring(67, 8);
                    this._LAW_LIST = RT01Entry.Substring(75, 4);
                    this._COMM = RT01Entry.Substring(79, 4);
                    this._SFEE = RT01Entry.Substring(83, 4);
                    this._ORIG_CLAIM = RT01Entry.Substring(87, 14);
                    this._ORIG_INT = RT01Entry.Substring(101, 14);
                    this._ORIG_INT_D = RT01Entry.Substring(115, 8);
                    this._RATES_PRE = RT01Entry.Substring(123, 5);
                    this._RATES_POST = RT01Entry.Substring(128, 5);
                    this._CRED_NAME = RT01Entry.Substring(133, 25);
                    this._CRED_NAME2 = RT01Entry.Substring(158, 25);
                    this._CRED_STREET = RT01Entry.Substring(183, 25);
                    this._CRED_CS = RT01Entry.Substring(208, 15);
                    this._CRED_ZIP = RT01Entry.Substring(223, 9);
                    this._DEBT_BAL = RT01Entry.Substring(232, 14);
                    this._CTYPE = RT01Entry.Substring(246, 4);
                    this._DATE_LPAY = RT01Entry.Substring(250, 8);
                    this._AMT_LPAY = RT01Entry.Substring(258, 14);
                    this._DATE_OPEN = RT01Entry.Substring(272, 8);
                    this._CHRG_OFF_D = RT01Entry.Substring(280, 8);
                    this._CHRG_OFF_A = RT01Entry.Substring(288, 14);
                    this._PURCHASE_D = RT01Entry.Substring(302, 8);
                    this._ORIG_CRED = RT01Entry.Substring(310, 30);
                    this._ORIG_CRED2 = RT01Entry.Substring(340, 30);
                    this._PORT_ID = RT01Entry.Substring(370, 20);
                    this._CRED_CNTRY = RT01Entry.Substring(390, 3);
                    this._LPAY_ISS_D = RT01Entry.Substring(393, 8);
                    this._LPAY_ISS_AMT = RT01Entry.Substring(401, 14);
                    this._MEDIA = RT01Entry.Substring(415, 1);
                    this._DELINQ_D = RT01Entry.Substring(416, 8);
                    this._ACCEL_D = RT01Entry.Substring(424, 8);
                    this._REPO_D = RT01Entry.Substring(432, 8);
                    this._SALE_D = RT01Entry.Substring(440, 8);
                    this._MATUR_D = RT01Entry.Substring(448, 8);
                    this._SOL_START_D = RT01Entry.Substring(456, 8);
                    this._SOL_END_D = RT01Entry.Substring(464, 8);
                    this._LATE_FEE = RT01Entry.Substring(472, 14);
                    this._HIST_CRED1 = RT01Entry.Substring(486, 50);
                    this._HIST_CRED2 = RT01Entry.Substring(536, 50);
                    this._HIST_CRED3 = RT01Entry.Substring(586, 50);
                    this._HIST_CRED4 = RT01Entry.Substring(636, 50);
                    this._HIST_CRED5 = RT01Entry.Substring(686, 50);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47}",
                    this.RECORD,
                    this._FILENO,
                    this._FORW_FILE,
                    this._MASCO_FILE,
                    this._FORW_ID,
                    this._FIRM_ID,
                    this._DATE_FORW,
                    this._LAW_LIST,
                    this._COMM,
                    this._SFEE,
                    this._ORIG_CLAIM,
                    this._ORIG_INT,
                    this._ORIG_INT_D,
                    this._RATES_PRE,
                    this._RATES_POST,
                    this._CRED_NAME,
                    this._CRED_NAME2,
                    this._CRED_STREET,
                    this._CRED_CS,
                    this._CRED_ZIP,
                    this._DEBT_BAL,
                    this._CTYPE,
                    this._DATE_LPAY,
                    this._AMT_LPAY,
                    this._DATE_OPEN,
                    this._CHRG_OFF_D,
                    this._CHRG_OFF_A,
                    this._PURCHASE_D,
                    this._ORIG_CRED,
                    this._ORIG_CRED2,
                    this._PORT_ID,
                    this._CRED_CNTRY,
                    this._LPAY_ISS_D,
                    this._LPAY_ISS_AMT,
                    this._MEDIA,
                    this._DELINQ_D,
                    this._ACCEL_D,
                    this._REPO_D,
                    this._SALE_D,
                    this._MATUR_D,
                    this._SOL_START_D,
                    this._SOL_END_D,
                    this._LATE_FEE,
                    this._HIST_CRED1,
                    this._HIST_CRED2,
                    this._HIST_CRED3,
                    this._HIST_CRED4,
                    this._HIST_CRED5);
            }
        }
    }
    #endregion

    #region Record Type 02
    public class RT02Table
    {
        private List<RT02> _Rows = new List<RT02>();
        public List<RT02> RT02Records { get { return _Rows; } }
        public RT02 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT02
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _D1_NAME = "".PadRight(30, ' ');
            private string _D1_SALUT = "".PadRight(1, ' ');
            private string _D1_ALIAS = "".PadRight(25, ' ');
            private string _D1_STREET = "".PadRight(25, ' ');
            private string _D1_CS = "".PadRight(23, ' ');
            private string _D1_ZIP = "".PadRight(9, ' ');
            private string _D1_PHONE = "".PadRight(12, ' ');
            private string _D1_FAX = "".PadRight(12, ' ');
            private string _D1_SSN = "".PadRight(15, ' ');
            private string _RFILE = "".PadRight(8, ' ');
            private string _D1_DOB = "".PadRight(8, ' ');
            private string _D1_DL = "".PadRight(17, ' ');
            private string _D1_STATE = "".PadRight(3, ' ');
            private string _D1_MAIL = "".PadRight(1, ' ');
            private string _SERVICE_D = "".PadRight(8, ' ');
            private string _ANSWER_DUE_D = "".PadRight(8, ' ');
            private string _ANSWER_FILE_D = "".PadRight(8, ' ');
            private string _DEFAULT_D = "".PadRight(8, ' ');
            private string _TRIAL_D = "".PadRight(8, ' ');
            private string _HEARING_D = "".PadRight(8, ' ');
            private string _LIEN_D = "".PadRight(8, ' ');
            private string _GARN_D = "".PadRight(8, ' ');
            private string _SERVICE_TYPE = "".PadRight(4, ' ');
            private string _D1_STREET2 = "".PadRight(25, ' ');
            private string _D1_CITY = "".PadRight(30, ' ');
            private string _D1_CELL = "".PadRight(12, ' ');
            private string _SCORE_FICO = "".PadRight(3, ' ');
            private string _SCORE_COLLECT = "".PadRight(3, ' ');
            private string _SCORE_OTHER = "".PadRight(3, ' ');
            private string _D1_CNTRY = "".PadRight(3, ' ');
            private string _D1_STREET_LONG = "".PadRight(50, ' ');
            private string _D1_STREET2_LONG = "".PadRight(50, ' ');
            #endregion

            #region Public Properties
            /// <summary>
            /// This record identifies the primary debtor and significant legal events. Record type value = 02. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 02, 31 or 52, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.
            /// </summary>
            public string RECORD { get { return "02"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the primary debtor's name. The format is Lastname/Firstname.
            /// </summary>
            public string D1_NAME { get { return this._D1_NAME.Trim(); } set { this._D1_NAME = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// "This single-byte field is a code for the salutation. The valid codes are: 
            /// <para>1 - Mr.</para>
            /// <para>2 - Mrs.</para>
            /// <para>3 - Ms.</para>
            /// <para>4 - Mr. & Mrs.</para>
            /// <para>5 - Dr.</para>
            /// <para>6 - Capt.</para>
            /// <para>7-9 - Gentlemen</para>
            /// </summary>
            public byte D1_SALUT { get { try { return byte.Parse(this._D1_SALUT.Trim()); } catch { return 0; } } set { this._D1_SALUT = value < 10 && value > 0 ? value.ToString() : " "; } }
            /// <summary>
            /// This is the primary debtor's alias name. The format is Lastname/Firstname.
            /// </summary>
            public string D1_ALIAS { get { return this._D1_ALIAS.Trim(); } set { this._D1_ALIAS = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the primary debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page. If there is a D1_STREET field but no D1_STRT2 field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public string D1_STREET { get { return this._D1_STREET.Trim(); } set { this._D1_STREET = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
            /// <para>If there are no D1_CITY and D1_STATE values in this record, this field will populate the city and state fields in the account detail page in YouveGotReports. If there is a D1_CS field but no D1_ZIP or D1_CNTRY fields populated in this record, the zip code and country will become empty on YouveGotReports.
            /// </para></summary>
            public string D1_CS { get { return this._D1_CS.Trim(); } set { this._D1_CS = value.Length < 23 ? value.PadRight(23, ' ') : value.Substring(0, 23); } }
            /// <summary>
            /// This is the primary debtor's zip code. It can accommodate the four-digit extension if you do not include the hyphen. If there is a D1_ZIP but no D1_CITY, D1_STATE and D1_CNTRY or D1_CS and D1_CNTRY fields populated in this record, the city, state and country will become empty on YouveGotReports.
            /// </summary>
            public string D1_ZIP { get { return this._D1_ZIP.Trim(); } set { this._D1_ZIP = value.Length < 9 ? value.PadRight(9, ' ') : value.Substring(0, 9); } }
            /// <summary>
            /// This is the primary debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string D1_PHONE { get { return this._D1_PHONE.Trim(); } set { this._D1_PHONE = value.Length < 12 ? value.PadRight(12, ' ') : value.Substring(0, 12); } }
            /// <summary>
            /// This is the primary debtor's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string D1_FAX { get { return this._D1_FAX.Trim(); } set { this._D1_FAX = value.Length < 12 ? value.PadRight(12, ' ') : value.Substring(0, 12); } }
            /// <summary>
            /// This is the primary debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public string D1_SSN { get { return this._D1_SSN.Trim(); } set { this._D1_SSN = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This can hold a code common to a set of accounts for the same debtor, such as a student with multiple semester loans. This will allow you to work a parent account instead of each individual account.
            /// </summary>
            public string RFILE { get { return this._RFILE.Trim(); } set { this._RFILE = value.Length < 8 ? value.PadRight(8, ' ') : value.Substring(0, 8); } }
            /// <summary>
            /// This is the primary debtor's date of birth.
            /// </summary>
            public DateTime? D1_DOB { get { try { return DateTime.ParseExact(this._D1_DOB, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._D1_DOB = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the primary debtor's driver's license number.
            /// </summary>
            public string D1_DL { get { return this._D1_DL.Trim(); } set { this._D1_DL = value.Length < 17 ? value.PadRight(17, ' ') : value.Substring(0, 17); } }
            /// <summary>
            /// This is the primary debtor's state abbreviation. If there is a D1_STATE but no D1_CITY, D1_ZIP or D1_CNTRY fields populated in this record, the city, zip or country fields will become empty on YouveGotReports.
            /// </summary>
            public string D1_STATE { get { return this._D1_STATE.Trim(); } set { this._D1_STATE = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 3); } }
            /// <summary>
            /// Set this field to Y if the served papers were returned.
            /// </summary>
            public bool D1_MAIL { get { return this._D1_MAIL.Trim() == "Y"; } set { this._D1_MAIL = value ? "Y" : " "; } }
            /// <summary>
            /// This is the date the suit was served.
            /// </summary>
            public DateTime? SERVICE_D { get { try { return DateTime.ParseExact(this._SERVICE_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._SERVICE_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Typically 30 days after SERVICE_D, it is the date the debtor's response to the suit is due.
            /// </summary>
            public DateTime? ANSWER_DUE_D { get { try { return DateTime.ParseExact(this._ANSWER_DUE_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._ANSWER_DUE_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date the debtor's response was filed.
            /// </summary>
            public DateTime? ANSWER_FILE_D { get { try { return DateTime.ParseExact(this._ANSWER_FILE_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._ANSWER_FILE_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date the creditor requests a default judgment to be entered if the debtor does not appear in court by ANSWER_DUE_D
            /// </summary>
            public DateTime? DEFAULT_D { get { try { return DateTime.ParseExact(this._DEFAULT_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._DEFAULT_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the court-assigned date for the debtor to stand trial. In the case of a small-claims court, this will be the same as ANSWER_DUE_D.
            /// </summary>
            public DateTime? TRIAL_D { get { try { return DateTime.ParseExact(this._TRIAL_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._TRIAL_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date of the latest hearing on a motion filed by either party.
            /// </summary>
            public DateTime? HEARING_D { get { try { return DateTime.ParseExact(this._HEARING_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._HEARING_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date a lien was filed against a debtor's property.
            /// </summary>
            public DateTime? LIEN_D { get { try { return DateTime.ParseExact(this._LIEN_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._LIEN_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the date garnishment against the debtor's wages was established.
            /// </summary>
            public DateTime? GARN_D { get { try { return DateTime.ParseExact(this._GARN_D, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._GARN_D = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            ///<summary>
            ///This is the method used to serve the papers. 
            ///<para>The valid codes are: </para>
            ///<para>PER = Personal</para>
            ///<para>CER = Certified Mail</para>
            ///<para>SUB = Sub-service</para>
            ///<para>POS = Posting (left at front door)</para>
            ///<para>FIR = First Class Mail</para>
            ///</summary>
            public string SERVICE_TYPE { get { return this._SERVICE_TYPE.Trim(); } set { this._SERVICE_TYPE = value.Length < 4 ? value.PadRight(4, ' ') : value.Substring(0, 4); } }
            /// <summary>
            /// This is an overflow field for the debtor's street address. If there is no D1_STREET_LONG value in this record, this field will populate the overflow address field in the YouveGotReports account details page. If there is a D1_STRT2 FIELD but no D1_STREET field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public string D1_STREET2 { get { return this._D1_STREET2.Trim(); } set { this._D1_STREET2 = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the primary debtor's city. If this field is populated but D1_STATE, D1_ZIP or D1_CNTRY is not populated in this record, the state, zip code or country will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public string D1_CITY { get { return this._D1_CITY.Trim(); } set { this._D1_CITY = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the primary debtor's cell phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string D1_CELL { get { return this._D1_CELL.Trim(); } set { this._D1_CELL = value.Length < 12 ? value.PadRight(12, ' ') : value.Substring(0, 12); } }
            /// <summary>
            /// Fair Isaac credit score
            /// </summary>
            public Int16 SCORE_FICO { get { try { return Int16.Parse(this._SCORE_FICO.Trim()); } catch { return 0; } } set { this._SCORE_FICO = value >= 0 && value < 1000 ? value.ToString().PadLeft(3, ' ') : "   "; } }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public Int16 SCORE_COLLECT { get { try { return Int16.Parse(this._SCORE_COLLECT.Trim()); } catch { return 0; } } set { this._SCORE_COLLECT = value >= 0 && value < 1000 ? value.ToString().PadLeft(3, ' ') : "   "; } }
            /// <summary>
            /// Creditor-calculated score
            /// </summary>
            public Int16 SCORE_OTHER { get { try { return Int16.Parse(this._SCORE_OTHER.Trim()); } catch { return 0; } } set { this._SCORE_OTHER = value >= 0 && value < 1000 ? value.ToString().PadLeft(3, ' ') : "   "; } }
            /// <summary>
            /// This is the standard code for the debtor's country. If this field is populated but D1_CITY, D1_STATE, or D1_ZIP is not populated in this record, the city, state, or zip code will become empty in the account detail page in YouveGotReports.
            /// </summary>
            public string D1_CNTRY { get { return this._D1_CNTRY.Trim(); } set { this._D1_CNTRY = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 3); } }
            /// <summary>
            /// This field serves to deliver the entire primary debtor street address to systems that can hold longer values. It should be the same value as D1_STREET + D1_STRT2. Use this IN ADDITION TO D1_STREET in case your receivers cannot yet accept this newer field. If there is a D1_STREET_LONG field but no D1_STRT2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public string D1_STREET_LONG { get { return this._D1_STREET_LONG.Trim(); } set { this._D1_STREET_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This is an overflow field for D1_STREET_LONG. If there is a D1_STREET2_LONG field but no D1_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public string D1_STREET2_LONG { get { return this._D1_STREET2_LONG.Trim(); } set { this._D1_STREET2_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            #endregion

            public RT02(string RT02Entry)
            {
                try
                {
                    this._FILENO = RT02Entry.Substring(2, 10);
                    this._FORW_FILE = RT02Entry.Substring(12, 20);
                    this._MASCO_FILE = RT02Entry.Substring(32, 15);
                    this._FORW_ID = RT02Entry.Substring(47, 10);
                    this._FIRM_ID = RT02Entry.Substring(57, 10);
                    this._D1_NAME = RT02Entry.Substring(67, 30);
                    this._D1_SALUT = RT02Entry.Substring(97, 1);
                    this._D1_ALIAS = RT02Entry.Substring(98, 25);
                    this._D1_STREET = RT02Entry.Substring(123, 25);
                    this._D1_CS = RT02Entry.Substring(148, 23);
                    this._D1_ZIP = RT02Entry.Substring(171, 9);
                    this._D1_PHONE = RT02Entry.Substring(180, 12);
                    this._D1_FAX = RT02Entry.Substring(192, 12);
                    this._D1_SSN = RT02Entry.Substring(204, 15);
                    this._RFILE = RT02Entry.Substring(219, 8);
                    this._D1_DOB = RT02Entry.Substring(227, 8);
                    this._D1_DL = RT02Entry.Substring(235, 17);
                    this._D1_STATE = RT02Entry.Substring(252, 3);
                    this._D1_MAIL = RT02Entry.Substring(255, 1);
                    this._SERVICE_D = RT02Entry.Substring(256, 8);
                    this._ANSWER_DUE_D = RT02Entry.Substring(264, 8);
                    this._ANSWER_FILE_D = RT02Entry.Substring(272, 8);
                    this._DEFAULT_D = RT02Entry.Substring(280, 8);
                    this._TRIAL_D = RT02Entry.Substring(288, 8);
                    this._HEARING_D = RT02Entry.Substring(296, 8);
                    this._LIEN_D = RT02Entry.Substring(304, 8);
                    this._GARN_D = RT02Entry.Substring(312, 8);
                    this._SERVICE_TYPE = RT02Entry.Substring(320, 4);
                    this._D1_STREET2 = RT02Entry.Substring(324, 25);
                    this._D1_CITY = RT02Entry.Substring(349, 30);
                    this._D1_CELL = RT02Entry.Substring(379, 12);
                    this._SCORE_FICO = RT02Entry.Substring(391, 3);
                    this._SCORE_COLLECT = RT02Entry.Substring(394, 3);
                    this._SCORE_OTHER = RT02Entry.Substring(397, 3);
                    this._D1_CNTRY = RT02Entry.Substring(400, 3);
                    this._D1_STREET_LONG = RT02Entry.Substring(403, 50);
                    this._D1_STREET2_LONG = RT02Entry.Substring(453, 50);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37}",
                    this.RECORD,
                    this._FILENO,
                    this._FORW_FILE,
                    this._MASCO_FILE,
                    this._FORW_ID,
                    this._FIRM_ID,
                    this._D1_NAME,
                    this._D1_SALUT,
                    this._D1_ALIAS,
                    this._D1_STREET,
                    this._D1_CS,
                    this._D1_ZIP,
                    this._D1_PHONE,
                    this._D1_FAX,
                    this._D1_SSN,
                    this._RFILE,
                    this._D1_DOB,
                    this._D1_DL,
                    this._D1_STATE,
                    this._D1_MAIL,
                    this._SERVICE_D,
                    this._ANSWER_DUE_D,
                    this._ANSWER_FILE_D,
                    this._DEFAULT_D,
                    this._TRIAL_D,
                    this._HEARING_D,
                    this._LIEN_D,
                    this._GARN_D,
                    this._SERVICE_TYPE,
                    this._D1_STREET2,
                    this._D1_CITY,
                    this._D1_CELL,
                    this._SCORE_FICO,
                    this._SCORE_COLLECT,
                    this._SCORE_OTHER,
                    this._D1_CNTRY,
                    this._D1_STREET_LONG,
                    this._D1_STREET2_LONG);
            }
        }
    }
    #endregion

    #region Record Type 03
    public class RT03Table
    {
        private List<RT03> _Rows = new List<RT03>();
        public List<RT03> RT03Records { get { return _Rows; } }
        public RT03 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT03
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _D2_NAME = "".PadRight(25, ' ');
            private string _D2_STREET = "".PadRight(25, ' ');
            private string _D2_CSZ = "".PadRight(25, ' ');
            private string _D2_PHONE = "".PadRight(15, ' ');
            private string _D2_SSN = "".PadRight(15, ' ');
            private string _D3_NAME = "".PadRight(25, ' ');
            private string _D3_STREET = "".PadRight(25, ' ');
            private string _D3_CSZ = "".PadRight(25, ' ');
            private string _D3_PHONE = "".PadRight(15, ' ');
            private string _D3_SSN = "".PadRight(15, ' ');
            private string _D2_DOB = "".PadRight(8, ' ');
            private string _D3_DOB = "".PadRight(8, ' ');
            private string _D2_DL = "".PadRight(17, ' ');
            private string _D3_DL = "".PadRight(17, ' ');
            private string _D2_CNTRY = "".PadRight(3, ' ');
            private string _D3_CNTRY = "".PadRight(3, ' ');
            private string _D2_STREET_LONG = "".PadRight(50, ' ');
            private string _D2_STREET2_LONG = "".PadRight(50, ' ');
            private string _D3_STREET_LONG = "".PadRight(50, ' ');
            private string _D3_STREET2_LONG = "".PadRight(50, ' ');
            #endregion

            #region Public Properties
            /// <summary>
            /// If more than one name appears on the original contract between creditor and debtor, they are identified here. This can also be used for a co-signer. Record type value = 03. YouveGotReports treats the street address fields as one block and the city/state/zip fields as another block, and displays only the latest blocks received from a record 03, 33 or 53, in the address section of the details page. Therefore it is important that when you update any part of the address, send all fields in that block as indicated in the descriptions below.
            /// </summary>
            public string RECORD { get { return "03"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the name of the second debtor or co-signer. The format is Lastname/Firstname.
            /// </summary>
            public string D2_NAME { get { return this._D2_NAME.Trim(); } set { this._D2_NAME = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the second debtor's street address. If there is no D2_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
            /// </summary>
            public string D2_STREET { get { return this._D2_STREET.Trim(); } set { this._D2_STREET = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the second debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D2_CSZ field but no D2_CNTRY field populated in this record, the country will become empty on YouveGotReports.
            /// </summary>
            public string D2_CSZ { get { return this._D2_CSZ.Trim(); } set { this._D2_CSZ = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the second debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string D2_PHONE { get { return this._D2_PHONE.Trim(); } set { this._D2_PHONE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the second debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public string D2_SSN { get { return this._D2_SSN.Trim(); } set { this._D2_SSN = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the name of the third debtor or co-signer. The format is Lastname/Firstname.
            /// </summary>
            public string D3_NAME { get { return this._D3_NAME.Trim(); } set { this._D3_NAME = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the third debtor's street address. If there is no D3_STREET_LONG value in this record, this field will populate the address field in the YouveGotReports account details page.
            /// </summary>
            public string D3_STREET { get { return this._D3_STREET.Trim(); } set { this._D3_STREET = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the third debtor's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036) If there is a D3_CSZ field but no D3_CNTRY field populated in this record, the country will become empty on YouveGotReports.
            /// </summary>
            public string D3_CSZ { get { return this._D3_CSZ.Trim(); } set { this._D3_CSZ = value.Length < 25 ? value.PadRight(25, ' ') : value.Substring(0, 25); } }
            /// <summary>
            /// This is the third debtor's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string D3_PHONE { get { return this._D3_PHONE.Trim(); } set { this._D3_PHONE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the third debtor's social security number. It can accommodate hyphens.
            /// </summary>
            public string D3_SSN { get { return this._D3_SSN.Trim(); } set { this._D3_SSN = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the second debtor's date of birth.
            /// </summary>
            public DateTime? D2_DOB { get { try { return DateTime.ParseExact(this._D2_DOB, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._D2_DOB = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the third debtor's date of birth.
            /// </summary>
            public DateTime? D3_DOB { get { try { return DateTime.ParseExact(this._D3_DOB, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._D3_DOB = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// This is the second debtor's driver's license number.
            /// </summary>
            public string D2_DL { get { return this._D2_DL.Trim(); } set { this._D2_DL = value.Length < 17 ? value.PadRight(17, ' ') : value.Substring(0, 17); } }
            /// <summary>
            /// This is the third debtor's driver's license number.
            /// </summary>
            public string D3_DL { get { return this._D3_DL.Trim(); } set { this._D3_DL = value.Length < 17 ? value.PadRight(17, ' ') : value.Substring(0, 17); } }
            /// <summary>
            /// This is the standard code for the second debtor's country. If there is a D2_CNTRY field but no D2_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
            /// </summary>
            public string D2_CNTRY { get { return this._D2_CNTRY.Trim(); } set { this._D2_CNTRY = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 3); } }
            /// <summary>
            /// This is the standard code for the third debtor's country. If there is a D3_CNTRY field but no D3_CSZ field populated in this record, the city, state and zip code will become empty on YouveGotReports.
            /// </summary>
            public string D3_CNTRY { get { return this._D3_CNTRY.Trim(); } set { this._D3_CNTRY = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 103); } }
            /// <summary>
            /// This field serves to deliver the entire 2nd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D2_STREET in case your receivers cannot yet accept this newer field. If there is a D2_STREET_LONG field but no D2_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public string D2_STREET_LONG { get { return this._D2_STREET_LONG.Trim(); } set { this._D2_STREET_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This is an overflow field for D2_STREET_LONG. If there is a D2_STREET2_LONG field but no D2_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public string D2_STREET2_LONG { get { return this._D2_STREET2_LONG.Trim(); } set { this._D2_STREET2_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This field serves to deliver the entire 3rd debtor street address to systems that can hold longer values. Use this IN ADDITION TO D3_STREET in case your receivers cannot yet accept this newer field. If there is a D3_STREET_LONG field but no D3_STREET2_LONG field populated in this record, the second address line will become empty on YouveGotReports.
            /// </summary>
            public string D3_STREET_LONG { get { return this._D3_STREET_LONG.Trim(); } set { this._D3_STREET_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            /// <summary>
            /// This is an overflow field for D3_STREET_LONG. If there is a D3_STREET2_LONG field but no D3_STREET_LONG field populated in this record, the first address line will become empty on YouveGotReports.
            /// </summary>
            public string D3_STREET2_LONG { get { return this._D3_STREET2_LONG.Trim(); } set { this._D3_STREET2_LONG = value.Length < 50 ? value.PadRight(50, ' ') : value.Substring(0, 50); } }
            #endregion

            public RT03(string RT03Entry)
            {
                try
                {
                    this._FILENO = RT03Entry.Substring(2, 10);
                    this._FORW_FILE = RT03Entry.Substring(12, 20);
                    this._MASCO_FILE = RT03Entry.Substring(32, 15);
                    this._FORW_ID = RT03Entry.Substring(47, 10);
                    this._FIRM_ID = RT03Entry.Substring(57, 10);
                    this._D2_NAME = RT03Entry.Substring(67, 25);
                    this._D2_STREET = RT03Entry.Substring(92, 25);
                    this._D2_CSZ = RT03Entry.Substring(117, 25);
                    this._D2_PHONE = RT03Entry.Substring(142, 15);
                    this._D2_SSN = RT03Entry.Substring(157, 15);
                    this._D3_NAME = RT03Entry.Substring(172, 25);
                    this._D3_STREET = RT03Entry.Substring(197, 25);
                    this._D3_CSZ = RT03Entry.Substring(222, 25);
                    this._D3_PHONE = RT03Entry.Substring(247, 15);
                    this._D3_SSN = RT03Entry.Substring(262, 15);
                    this._D2_DOB = RT03Entry.Substring(277, 8);
                    this._D3_DOB = RT03Entry.Substring(285, 8);
                    this._D2_DL = RT03Entry.Substring(293, 17);
                    this._D3_DL = RT03Entry.Substring(310, 17);
                    this._D2_CNTRY = RT03Entry.Substring(327, 3);
                    this._D3_CNTRY = RT03Entry.Substring(330, 3);
                    this._D2_STREET_LONG = RT03Entry.Substring(333, 50);
                    this._D2_STREET2_LONG = RT03Entry.Substring(383, 50);
                    this._D3_STREET_LONG = RT03Entry.Substring(433, 50);
                    this._D3_STREET2_LONG = RT03Entry.Substring(483, 50);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25}",
                    this.RECORD,
                    this._FILENO,
                    this._FORW_FILE,
                    this._MASCO_FILE,
                    this._FORW_ID,
                    this._FIRM_ID,
                    this._D2_NAME,
                    this._D2_STREET,
                    this._D2_CSZ,
                    this._D2_PHONE,
                    this._D2_SSN,
                    this._D3_NAME,
                    this._D3_STREET,
                    this._D3_CSZ,
                    this._D3_PHONE,
                    this._D3_SSN,
                    this._D2_DOB,
                    this._D3_DOB,
                    this._D2_DL,
                    this._D3_DL,
                    this._D2_CNTRY,
                    this._D3_CNTRY,
                    this._D2_STREET_LONG,
                    this._D2_STREET2_LONG,
                    this._D3_STREET_LONG,
                    this._D3_STREET2_LONG);
            }
        }
    }
    #endregion

    #region Record Type 04
    public class RT04Table
    {
        private List<RT04> _Rows = new List<RT04>();
        public List<RT04> RT04Records { get { return _Rows; } }
        public RT04 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT04
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _Emp_Name = "".PadRight(40, ' ');
            private string _Emp_Street = "".PadRight(40, ' ');
            private string _Emp_PO = "".PadRight(40, ' ');
            private string _Emp_CS = "".PadRight(30, ' ');
            private string _Emp_ZO = "".PadRight(10, ' ');
            private string _Emp_Phone = "".PadRight(15, ' ');
            private string _Emp_Fax = "".PadRight(15, ' ');
            private string _Emp_Attn = "".PadRight(40, ' '); //Correspondence Personnel for Employer
            private string _Emp_Payr = "".PadRight(40, ' '); //Payroll Contact
            private string _Emp_No = "".PadRight(3, ' '); //Debtor Number
            private string _Employee = "".PadRight(30, ' '); //Employee Name
            private string _Emp_Income = "".PadRight(14, ' '); //Income Per each period indicated by frequency
            private string _Emp_Freq = "".PadRight(1, ' '); //Pay Frequency (H,A,W,M,B,S) (Hourly, Annually, Weekly, Monthly, Bi-monthly, Semi-monthly)
            private string _Emp_Pos = "".PadRight(20, ' '); //Title at Company
            private string _Emp_Tenure = "".PadRight(3, ' '); //Length in Months of Employment
            private string _Emp_Cntry = "".PadRight(3, ' '); //Country Code
            #endregion

            #region Public Properties
            /// <summary>
            /// This record holds the debtors' employment information. You can submit a record for each of 3 distinct debtors for the same account. Value = 04.
            /// </summary>
            public string RECORD { get { return "04"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// Company name of debtor's employer.
            /// </summary>
            public string Employer_Name { get { return _Emp_Name.Trim(); } set { _Emp_Name = value.Length > 40 ? value.Substring(0, 40) : value.PadRight(40, ' '); } }
            /// <summary>
            /// This is the employer's street address.
            /// </summary>
            public string Employer_Street { get { return _Emp_Street.Trim(); } set { _Emp_Street = value.Length > 40 ? value.Substring(0, 40) : value.PadRight(40, ' '); } }
            /// <summary>
            /// This is the employer's PO Box number.
            /// </summary>
            public string Employer_PO_Box { get { return _Emp_PO.Trim(); } set { _Emp_PO = value.Length > 40 ? value.Substring(0, 40) : value.PadRight(40, ' '); } }
            /// <summary>
            /// Format this field as City ST or City,ST. (Example: Linden NJ or Linden,NJ)
            /// </summary>
            public string Employer_City_State { get { return _Emp_CS.Trim(); } set { _Emp_CS = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// This is the employer's zip code. It can accommodate the four-digit extension with the hyphen.
            /// </summary>
            public string Employer_Zip { get { return _Emp_ZO.Trim(); } set { _Emp_ZO = value.Length > 10 ? value.Substring(0, 10) : value.PadRight(10, ' '); } }
            /// <summary>
            /// This is the employer's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string Employer_Phone { get { return _Emp_Phone.Trim(); } set { _Emp_Phone = value.Length > 15 ? value.Substring(0, 15) : value.PadRight(15, ' '); } }
            /// <summary>
            /// This is the employer's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string Employer_Fax { get { return _Emp_Fax.Trim(); } set { _Emp_Fax = value.Length > 15 ? value.Substring(0, 15) : value.PadRight(15, ' '); } }
            /// <summary>
            /// Department or personnel for correspondence to employer.
            /// </summary>
            public string Employer_ATTN { get { return _Emp_Attn.Trim(); } set { _Emp_Attn = value.Length > 40 ? value.Substring(0, 40) : value.PadRight(40, ' '); } }
            /// <summary>
            /// This is the contact person at the payroll department.
            /// </summary>
            public string Employer_Payroll { get { return _Emp_Payr.Trim(); } set { _Emp_Payr = value.Length > 40 ? value.Substring(0, 40) : value.PadRight(40, ' '); } }
            /// <summary>
            /// This is used to distinguish up to 3 employment records. Values are 1, 2 or 3. 
            /// <para>(Ex: The record 04 for the primary debtor can have EMP_NO = 1 and the record 04 for the co-signer can have EMP_NO = 2.)</para>
            /// <para>If a record 04 is sent with the same EMP_NO as a previous record 04 for the same account, the second record will overwrite the first.</para>
            /// </summary>
            public int Debtor_Number { get { try { return int.Parse(_Emp_No.Trim()); } catch { return 1; } } set { _Emp_No = value > 999 || value < 1 ? "1  " : value.ToString().PadRight(3, ' '); } }
            /// <summary>
            /// This is the debtor name. There is no format restriction.
            /// </summary>
            public string Employee_Name { get { return _Employee.Trim(); } set { _Employee = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// Income Earned Per Pay Frequency Period
            /// </summary>
            public decimal Employee_Income { get { try { return decimal.Parse(_Emp_Income); } catch { return 0; } } set { _Emp_Income = value >= (10 * 12) || value < 0 ? "0.00".PadLeft(14, ' ') : value.ToString("0.00").PadLeft(14, ' '); } }
            /// <summary>
            /// Freqeuncy with Which the Listed Income is Distributed
            /// </summary>
            public PaymentFrequency Employee_Frequency
            {
                get
                {
                    switch (_Emp_Freq.Trim())
                    {
                        case "H":
                            return PaymentFrequency.Hourly;
                        case "A":
                            return PaymentFrequency.Annual;
                        case "W":
                            return PaymentFrequency.Weekly;
                        case "M":
                            return PaymentFrequency.Monthly;
                        case "B":
                            return PaymentFrequency.BiMonthly;
                        case "S":
                            return PaymentFrequency.SemiMonthly;
                        default:
                            return PaymentFrequency.Annual;
                    }
                }
                set
                {
                    switch (value)
                    {
                        case PaymentFrequency.Hourly:
                            _Emp_Freq = "H";
                            break;
                        case PaymentFrequency.Annual:
                            _Emp_Freq = "A";
                            break;
                        case PaymentFrequency.Weekly:
                            _Emp_Freq = "W";
                            break;
                        case PaymentFrequency.Monthly:
                            _Emp_Freq = "M";
                            break;
                        case PaymentFrequency.BiMonthly:
                            _Emp_Freq = "B";
                            break;
                        case PaymentFrequency.SemiMonthly:
                            _Emp_Freq = "S";
                            break;
                    }
                }
            }
            /// <summary>
            /// This is the employee's title at the given employer.
            /// </summary>
            public string Employee_Position { get { return _Emp_Pos.Trim(); } set { _Emp_Pos = value.Length > 20 ? value.Substring(0, 20) : value.PadRight(20, ' '); } }
            /// <summary>
            /// Number of Months with Employer
            /// </summary>
            public int Employee_Tenure { get { try { return int.Parse(_Emp_Tenure); } catch { return 0; } } set { _Emp_Tenure = value > 999 ? "999" : value < 0 ? _Emp_Tenure = "  0" : value.ToString().PadLeft(3, ' '); } }
            /// <summary>
            /// This is the standard code for the employee's country.
            /// </summary>
            public string Employer_Country { get { return _Emp_Cntry.Trim(); } set { _Emp_Cntry = value.Length > 3 ? value.Substring(0, 3) : value.PadRight(3, ' '); } }
            #endregion

            public RT04(string RT04Entry)
            {
                try
                {
                    _FILENO = RT04Entry.Substring(2, 10);
                    _FORW_FILE = RT04Entry.Substring(12, 20);
                    _MASCO_FILE = RT04Entry.Substring(32, 15);
                    _FORW_ID = RT04Entry.Substring(47, 10);
                    _FIRM_ID = RT04Entry.Substring(57, 10);
                    _Emp_Name = RT04Entry.Substring(67, 40);
                    _Emp_Street = RT04Entry.Substring(107, 40);
                    _Emp_PO = RT04Entry.Substring(147, 40);
                    _Emp_CS = RT04Entry.Substring(187, 30);
                    _Emp_ZO = RT04Entry.Substring(217, 10);
                    _Emp_Phone = RT04Entry.Substring(227, 15);
                    _Emp_Fax = RT04Entry.Substring(242, 15);
                    _Emp_Attn = RT04Entry.Substring(257, 40);
                    _Emp_Payr = RT04Entry.Substring(297, 40);
                    _Emp_No = RT04Entry.Substring(337, 3);
                    _Employee = RT04Entry.Substring(340, 30);
                    _Emp_Income = RT04Entry.Substring(370, 14);
                    _Emp_Freq = RT04Entry.Substring(384, 1);
                    _Emp_Pos = RT04Entry.Substring(385, 20);
                    _Emp_Tenure = RT04Entry.Substring(405, 3);
                    _Emp_Cntry = RT04Entry.Substring(408, 3);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}",
                    RECORD,
                    _FILENO,
                    _FORW_FILE,
                    _MASCO_FILE,
                    _FORW_ID,
                    _FIRM_ID,
                    _Emp_Name,
                    _Emp_Street,
                    _Emp_PO,
                    _Emp_CS,
                    _Emp_ZO,
                    _Emp_Phone,
                    _Emp_Fax,
                    _Emp_Attn,
                    _Emp_Payr,
                    _Emp_No,
                    _Emp_Name,
                    _Emp_Income,
                    _Emp_Freq,
                    _Emp_Pos,
                    _Emp_Tenure,
                    _Emp_Cntry);
            }
        }
    }

    public enum PaymentFrequency
    {
        Hourly,
        Annual,
        Weekly,
        Monthly,
        BiMonthly,
        SemiMonthly
    }
    #endregion

    #region Record Type 05
    public class RT05Table
    {
        private List<RT05> _Rows = new List<RT05>();
        public List<RT05> RT05Records { get { return _Rows; } }
        public RT05 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT05
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _FILLER = "".PadRight(20, ' ');
            private string _BANK_NAME = "".PadRight(30, ' ');
            private string _BANK_STREET = "".PadRight(30, ' ');
            private string _BANK_CSZ = "".PadRight(30, ' ');
            private string _BANK_ATTN = "".PadRight(30, ' ');
            private string _BANK_PHONE = "".PadRight(15, ' ');
            private string _BANK_FAX = "".PadRight(15, ' ');
            private string _BANK_ACCT = "".PadRight(17, ' ');
            private string _MISC_ASSET1 = "".PadRight(25, ' ');
            private string _MISC_ASSET2 = "".PadRight(25, ' ');
            private string _MISC_ASSET3 = "".PadRight(25, ' ');
            private string _MISC_PHONE = "".PadRight(15, ' ');
            private string _BANK_NO = "".PadRight(3, ' ');
            private string _BANK_CNTRY = "".PadRight(3, ' ');
            #endregion

            #region Public Properties
            /// <summary>
            /// This record holds any bank account information and non-auto or non-real estate asset information for the debtor. You can submit a record for each of 3 distinct bank accounts for the same debtor. Value = 05.
            /// </summary>
            public string RECORD { get { return "05"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the name of the debtor's bank.
            /// </summary>
            public string Bank_Name { get { return _BANK_NAME.Trim(); } set { _BANK_NAME = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// This is the debtor's bank street address.
            /// </summary>
            public string Bank_Street { get { return _BANK_STREET.Trim(); } set { _BANK_STREET = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// This is the bank's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
            /// </summary>
            public string Bank_City_State_Zip { get { return _BANK_CSZ.Trim(); } set { _BANK_CSZ = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// This is the contact name for the bank.
            /// </summary>
            public string Bank_ATTN { get { return _BANK_ATTN.Trim(); } set { _BANK_ATTN = value.Length > 30 ? value.Substring(0, 30) : value.PadRight(30, ' '); } }
            /// <summary>
            /// This is the bank's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string Bank_Phone { get { return _BANK_PHONE.Trim(); } set { _BANK_PHONE = value.Length > 15 ? value.Substring(0, 15) : value.PadRight(15, ' '); } }
            /// <summary>
            /// This is the bank's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string Bank_Fax { get { return _BANK_FAX.Trim(); } set { _BANK_FAX = value.Length > 15 ? value.Substring(0, 15) : value.PadRight(15, ' '); } }
            /// <summary>
            /// This is the debtor's bank account number.
            /// </summary>
            public string Bank_Account { get { return _BANK_ACCT.Trim(); } set { _BANK_ACCT = value.Length > 17 ? value.Substring(0, 17) : value.PadRight(17, ' '); } }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public string Miscellaneous_Asset1 { get { return _MISC_ASSET1.Trim(); } set { _MISC_ASSET1 = value.Length > 25 ? value.Substring(0, 25) : value.PadRight(25, ' '); } }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public string Miscellaneous_Asset2 { get { return _MISC_ASSET2.Trim(); } set { _MISC_ASSET2 = value.Length > 25 ? value.Substring(0, 25) : value.PadRight(25, ' '); } }
            /// <summary>
            /// Use this field for any non-land or non-automobile asset like a stock portfolio. (A receiver can supply physical asset information by sending a record 46.) If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public string Miscellaneous_Asset3 { get { return _MISC_ASSET3.Trim(); } set { _MISC_ASSET3 = value.Length > 25 ? value.Substring(0, 25) : value.PadRight(25, ' '); } }
            /// <summary>
            /// This field holds a relevant phone number relating to the miscellaneous asset. If this field is used, you MUST set BANK_NO to 1.
            /// </summary>
            public string Miscellaneous_Phone { get { return _MISC_PHONE.Trim(); } set { _MISC_PHONE = value.Length > 15 ? value.Substring(0, 15) : value.PadRight(15, ' '); } }
            /// <summary>
            /// This is used to distinguish up to 3 bank records. Values are 1, 2 or 3. 
            /// <para>(Ex: The record 05 for the debtor's checking account can have BANK_NO = 1 and the record 05 for the debtor's savings account can have BANK_NO = 2.)</para>
            /// <para>If a record 05 is sent with the same BANK_NO as a previous record 05 for the same account, the second record will overwrite the first. If you have a miscellaneous asset described in this record, this value must be 1.</para>
            /// </summary>
            public int Debtor_Number { get { try { return int.Parse(_BANK_NO.Trim()); } catch { return 1; } } set { _BANK_NO = value > 999 || value < 1 ? "1  " : value.ToString().PadRight(3, ' '); } }
            /// <summary>
            /// This is the standard code for the bank's country.
            /// </summary>
            public string Bank_Country { get { return _BANK_CNTRY.Trim(); } set { _BANK_CNTRY = value.Length > 3 ? value.Substring(0, 3) : value.PadRight(3, ' '); } }
            #endregion

            public RT05(string RT05Entry)
            {
                _FILENO = RT05Entry.Substring(2, 10);
                _FORW_FILE = RT05Entry.Substring(12, 20);
                _MASCO_FILE = RT05Entry.Substring(32, 15);
                _FORW_ID = RT05Entry.Substring(47, 10);
                _FIRM_ID = RT05Entry.Substring(57, 10);
                _FILLER = RT05Entry.Substring(67, 20);
                _BANK_NAME = RT05Entry.Substring(87, 30);
                _BANK_STREET = RT05Entry.Substring(117, 30);
                _BANK_CSZ = RT05Entry.Substring(147, 30);
                _BANK_ATTN = RT05Entry.Substring(177, 30);
                _BANK_PHONE = RT05Entry.Substring(207, 15);
                _BANK_FAX = RT05Entry.Substring(222, 15);
                _BANK_ACCT = RT05Entry.Substring(237, 17);
                _MISC_ASSET1 = RT05Entry.Substring(254, 25);
                _MISC_ASSET2 = RT05Entry.Substring(279, 25);
                _MISC_ASSET3 = RT05Entry.Substring(304, 25);
                _MISC_PHONE = RT05Entry.Substring(329, 15);
                _BANK_NO = RT05Entry.Substring(344, 3);
                _BANK_CNTRY = RT05Entry.Substring(347, 3);
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}",
                    RECORD,
                    _FILENO,
                    _FORW_FILE,
                    _MASCO_FILE,
                    _FORW_ID,
                    _FIRM_ID,
                    _FILLER,
                    _BANK_NAME,
                    _BANK_STREET,
                    _BANK_CSZ,
                    _BANK_ATTN,
                    _BANK_PHONE,
                    _BANK_FAX,
                    _BANK_ACCT,
                    _MISC_ASSET1,
                    _MISC_ASSET2,
                    _MISC_ASSET3,
                    _MISC_PHONE,
                    _BANK_NO,
                    _BANK_CNTRY);
            }
        }
    }

    #endregion

    #region Record Type 06
    public class RT06Table
    {
        private List<RT06> _Rows = new List<RT06>();
        public List<RT06> RT06Records { get { return _Rows; } }
        public RT06 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public class RT06
        {
            #region Private Properties
            private string _FILENO = "".PadRight(10, ' ');
            private string _FORW_FILE = "".PadRight(20, ' ');
            private string _MASCO_FILE = "".PadRight(15, ' ');
            private string _FORW_ID = "".PadRight(10, ' ');
            private string _FIRM_ID = "".PadRight(10, ' ');
            private string _ADVA_NAME = "".PadRight(30, ' ');
            private string _ADVA_FIRM = "".PadRight(30, ' ');
            private string _ADVA_FIRM2 = "".PadRight(30, ' ');
            private string _ADVA_STREET = "".PadRight(30, ' ');
            private string _ADVA_CSZ = "".PadRight(30, ' ');
            private string _ADVA_SALUT = "".PadRight(30, ' ');
            private string _ADVA_PHONE = "".PadRight(30, ' ');
            private string _ADVA_FAX = "".PadRight(15, ' ');
            private string _ADVA_FILENO = "".PadRight(12, ' ');
            private string _MISC_DATE1 = "".PadRight(8, ' ');
            private string _MISC_DATE2 = "".PadRight(8, ' ');
            private string _MISC_AMT1 = "".PadRight(14, ' ');
            private string _MISC_AMT2 = "".PadRight(14, ' ');
            private string _MISC_COMM1 = "".PadRight(15, ' ');
            private string _MISC_COMM2 = "".PadRight(15, ' ');
            private string _MISC_COMM3 = "".PadRight(15, ' ');
            private string _MISC_COMM4 = "".PadRight(15, ' ');
            private string _ADVA_NUM = "".PadRight(3, ' ');
            private string _ADVA_CNTRY = "".PadRight(3, ' ');
            #endregion

            #region Public Properties
            /// <summary>
            /// This record is for debtor attorney information and any miscellaneous information that no other record in the DataStandard accommodates. You can submit a record for each of 3 debtor attorneys for the same account. Value = 06.
            /// </summary>
            public string RECORD { get { return "06"; } }
            /// <summary>
            /// This is the internal file number of the sender (client) placing the account.
            /// </summary>
            public string FILENO { get { return this._FILENO.Trim(); } set { this._FILENO = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
            /// </summary>
            public string FORW_FILE { get { return this._FORW_FILE.Trim(); } set { this._FORW_FILE = value.Length < 20 ? value.PadRight(20, ' ') : value.Substring(0, 20); } }
            /// <summary>
            /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
            /// </summary>
            public string MASCO_FILE { get { return this._MASCO_FILE.Trim(); } set { this._MASCO_FILE = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FORW_ID { get { return this._FORW_ID.Trim(); } set { this._FORW_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
            /// </summary>
            public string FIRM_ID { get { return this._FIRM_ID.Trim(); } set { this._FIRM_ID = value.Length < 10 ? value.PadRight(10, ' ') : value.Substring(0, 10); } }
            /// <summary>
            /// This is for the name of the debtor's attorney. There is no format restriction.
            /// </summary>
            public string ADVA_NAME { get { return this._ADVA_NAME.Trim(); } set { this._ADVA_NAME = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the name of the debtor's attorney's firm.
            /// </summary>
            public string ADVA_FIRM { get { return this._ADVA_FIRM.Trim(); } set { this._ADVA_FIRM = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is an overflow field for ADVA_FIRM.
            /// </summary>
            public string ADVA_FIRM2 { get { return this._ADVA_FIRM2.Trim(); } set { this._ADVA_FIRM2 = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the attorney's street address.
            /// </summary>
            public string ADVA_STREET { get { return this._ADVA_STREET.Trim(); } set { this._ADVA_STREET = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the attorney's city, state and zip code. Format this field as City ST Zip or City,ST Zip. (Example: Linden NJ 07036 or Linden,NJ 07036)
            /// </summary>
            public string ADVA_CSZ { get { return this._ADVA_CSZ.Trim(); } set { this._ADVA_CSZ = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the attorney's preferred salutation in correspondence, such as "Dear Attorney Lewis:"
            /// </summary>
            public string ADVA_SALUT { get { return this._ADVA_SALUT.Trim(); } set { this._ADVA_SALUT = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the attorney's phone number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string ADVA_PHONE { get { return this._ADVA_PHONE.Trim(); } set { this._ADVA_PHONE = value.Length < 30 ? value.PadRight(30, ' ') : value.Substring(0, 30); } }
            /// <summary>
            /// This is the attorney's fax number. It can accommodate separators for the area code and exchange.
            /// </summary>
            public string ADVA_FAX { get { return this._ADVA_FAX.Trim(); } set { this._ADVA_FAX = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is the internal file number at the debtor's attorney's firm for this account.
            /// </summary>
            public string ADVA_FILENO { get { return this._ADVA_FILENO.Trim(); } set { this._ADVA_FILENO = value.Length < 12 ? value.PadRight(12, ' ') : value.Substring(0, 12); } }
            /// <summary>
            /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public DateTime? MISC_DATE1 { get { try { return DateTime.ParseExact(this._MISC_DATE1, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._MISC_DATE1 = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Use this field for a miscellaneous event regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public DateTime? MISC_DATE2 { get { try { return DateTime.ParseExact(this._MISC_DATE2, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this._MISC_DATE2 = value == null ? "        " : value.Value.ToString("yyyyMMdd"); } }
            /// <summary>
            /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public decimal MISC_AMT1 { get { try { return decimal.Parse(this._MISC_AMT1); } catch { return 0; } } set { this._MISC_AMT1 = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// Use this field for a miscellaneous dollar amount regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public decimal MISC_AMT2 { get { try { return decimal.Parse(this._MISC_AMT2); } catch { return 0; } } set { this._MISC_AMT2 = value.ToString("0.00").PadLeft(14, ' ').Substring(value.ToString("0.00").PadLeft(14, ' ').Length - 14, 14); } }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public string MISC_COMM1 { get { return this._MISC_COMM1.Trim(); } set { this._MISC_COMM1 = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public string MISC_COMM2 { get { return this._MISC_COMM2.Trim(); } set { this._MISC_COMM2 = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public string MISC_COMM3 { get { return this._MISC_COMM3.Trim(); } set { this._MISC_COMM3 = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// Use this field for a miscellaneous comment regarding the account. If this field is used, you MUST set ADVA_NUM to 1.
            /// </summary>
            public string MISC_COMM4 { get { return this._MISC_COMM4.Trim(); } set { this._MISC_COMM4 = value.Length < 15 ? value.PadRight(15, ' ') : value.Substring(0, 15); } }
            /// <summary>
            /// This is used to distinguish up to 3 debtor attorney records. Values are 1, 2 or 3.
            /// <para>(Ex: The record 06 for the debtor's primary attorney can have ADVA_NUM = 1 and the record 06 for the debtor's secondary attorney can have ADVA_NUM = 2.)</para>
            /// <para>If a record 06 is sent with the same ADVA_NUM as a previous record 06 for the same account, the second record will overwrite the first. If you have miscellaneous information described in this record, this value must be 1.</para>
            /// </summary>
            public int ADVA_NUM { get { try { return int.Parse(this._ADVA_NUM.Trim()); } catch { return 1; } } set { this._ADVA_NUM = value < 0 || value > 999 ? "1  " : value.ToString().PadRight(3, ' '); } }
            /// <summary>
            /// This is the standard code for the debtor attorney's country.
            /// </summary>
            public string ADVA_CNTRY { get { return this._ADVA_CNTRY.Trim(); } set { this._ADVA_CNTRY = value.Length < 3 ? value.PadRight(3, ' ') : value.Substring(0, 3); } }
            #endregion

            public RT06(string RT06Entry)
            {
                try
                {
                    this._FILENO = RT06Entry.Substring(2, 10);
                    this._FORW_FILE = RT06Entry.Substring(12, 20);
                    this._MASCO_FILE = RT06Entry.Substring(32, 15);
                    this._FORW_ID = RT06Entry.Substring(47, 10);
                    this._FIRM_ID = RT06Entry.Substring(57, 10);
                    this._ADVA_NAME = RT06Entry.Substring(67, 30);
                    this._ADVA_FIRM = RT06Entry.Substring(97, 30);
                    this._ADVA_FIRM2 = RT06Entry.Substring(127, 30);
                    this._ADVA_STREET = RT06Entry.Substring(157, 30);
                    this._ADVA_CSZ = RT06Entry.Substring(187, 30);
                    this._ADVA_SALUT = RT06Entry.Substring(217, 30);
                    this._ADVA_PHONE = RT06Entry.Substring(247, 30);
                    this._ADVA_FAX = RT06Entry.Substring(277, 15);
                    this._ADVA_FILENO = RT06Entry.Substring(292, 12);
                    this._MISC_DATE1 = RT06Entry.Substring(304, 8);
                    this._MISC_DATE2 = RT06Entry.Substring(312, 8);
                    this._MISC_AMT1 = RT06Entry.Substring(320, 14);
                    this._MISC_AMT2 = RT06Entry.Substring(334, 14);
                    this._MISC_COMM1 = RT06Entry.Substring(348, 15);
                    this._MISC_COMM2 = RT06Entry.Substring(363, 15);
                    this._MISC_COMM3 = RT06Entry.Substring(378, 15);
                    this._MISC_COMM4 = RT06Entry.Substring(393, 15);
                    this._ADVA_NUM = RT06Entry.Substring(408, 3);
                    this._ADVA_CNTRY = RT06Entry.Substring(411, 3);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}",
                    this.RECORD,
                    this._FILENO,
                    this._FORW_FILE,
                    this._MASCO_FILE,
                    this._FORW_ID,
                    this._FIRM_ID,
                    this._ADVA_NAME,
                    this._ADVA_FIRM,
                    this._ADVA_FIRM2,
                    this._ADVA_STREET,
                    this._ADVA_CSZ,
                    this._ADVA_SALUT,
                    this._ADVA_PHONE,
                    this._ADVA_FAX, 15,
                    this._ADVA_FILENO,
                    this._MISC_DATE1,
                    this._MISC_DATE2,
                    this._MISC_AMT1,
                    this._MISC_AMT2,
                    this._MISC_COMM1,
                    this._MISC_COMM2,
                    this._MISC_COMM3,
                    this._MISC_COMM4,
                    this._ADVA_NUM,
                    this._ADVA_CNTRY);
            }
        }
    }
    #endregion

    #region Record Type 09
    public class RT09Table
    {
        private List<RT09> _Rows = new List<RT09>();
        public List<RT09> RT09Records { get { return _Rows; } }
        public RT09 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            _Rows.Add(Temp);
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT, string NOTE2)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            Temp.NOTE2 = NOTE2;
            _Rows.Add(Temp);
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT, string NOTE2, string NOTE3)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            Temp.NOTE2 = NOTE2;
            Temp.NOTE3 = NOTE3;
            _Rows.Add(Temp);
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT, string NOTE2, string NOTE3, string NOTE4)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            Temp.NOTE2 = NOTE2;
            Temp.NOTE3 = NOTE3;
            Temp.NOTE4 = NOTE4;
            _Rows.Add(Temp);
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT, string NOTE2, string NOTE3, string NOTE4, string NOTE5)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            Temp.NOTE2 = NOTE2;
            Temp.NOTE3 = NOTE3;
            Temp.NOTE4 = NOTE4;
            Temp.NOTE5 = NOTE5;
            _Rows.Add(Temp);
        }

        public void Add(string MASCO_FILE, string PDATE, string PCODE, string PCMT, string NOTE2, string NOTE3, string NOTE4, string NOTE5, string NOTE6)
        {
            RT09 Temp = new RT09();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.PDATE = PDATE;
            Temp.PCODE = PCODE;
            Temp.PCMT = PCMT;
            Temp.NOTE2 = NOTE2;
            Temp.NOTE3 = NOTE3;
            Temp.NOTE4 = NOTE4;
            Temp.NOTE5 = NOTE5;
            Temp.NOTE6 = NOTE6;
            _Rows.Add(Temp);
        }

        public class RT09
        {
            private string _FILENO = "";
            private string _FORW_FILE = "";
            private string _MASCO_FILE = "";
            private string _FORW_ID = "";
            private string _FIRM_ID = "";
            private string _PDATE = "";
            private string _PCODE = "";
            private string _PCMT = "";
            private string _NOTE2 = "";
            private string _NOTE3 = "";
            private string _NOTE4 = "";
            private string _NOTE5 = "";
            private string _NOTE6 = "";
            private string _NOTE7 = "";
            private string _NOTE8 = "";
            private string _NOTE9 = "";
            private string _NOTE10 = "";
            private string _NOTE11 = "";
            private string _NOTE12 = "";
            private string _NOTE13 = "";
            private string _NOTE14 = "";
            private string _NOTE15 = "";
            private string _NOTE16 = "";
            private string _NOTE17 = "";
            public override string ToString()
            {
                _FILENO = _FILENO.PadRight(10, ' ');
                _FIRM_ID = _FIRM_ID.PadRight(10, ' ');
                _FORW_FILE = _FORW_FILE.PadRight(20, ' ');
                _FORW_ID = _FORW_ID.PadRight(10, ' ');
                _MASCO_FILE = _MASCO_FILE.PadRight(15, ' ');
                _PCMT = _PCMT.PadRight(48, ' ');
                _PCODE = _PCODE.PadRight(8, ' ');
                _PDATE = _PDATE.PadRight(8, ' ');
                _NOTE2 = _NOTE2.PadRight(57, ' ');
                _NOTE3 = _NOTE3.PadRight(57, ' ');
                _NOTE4 = _NOTE4.PadRight(57, ' ');
                _NOTE5 = _NOTE5.PadRight(57, ' ');
                _NOTE6 = _NOTE6.PadRight(57, ' ');
                _NOTE7 = _NOTE7.PadRight(57, ' ');
                _NOTE8 = _NOTE8.PadRight(57, ' ');
                _NOTE9 = _NOTE9.PadRight(57, ' ');
                _NOTE10 = _NOTE10.PadRight(57, ' ');
                _NOTE11 = _NOTE11.PadRight(57, ' ');
                _NOTE12 = _NOTE12.PadRight(57, ' ');
                _NOTE13 = _NOTE13.PadRight(57, ' ');
                _NOTE14 = _NOTE14.PadRight(57, ' ');
                _NOTE15 = _NOTE15.PadRight(57, ' ');
                _NOTE16 = _NOTE16.PadRight(57, ' ');
                _NOTE17 = _NOTE17.PadRight(57, ' ');
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}",
                    RECORD,
                    _FILENO,
                    _FORW_FILE,
                    _MASCO_FILE,
                    _FORW_ID,
                    _FIRM_ID,
                    _PDATE,
                    _PCODE,
                    _PCMT,
                    _NOTE2,
                    _NOTE3,
                    _NOTE4,
                    _NOTE5,
                    _NOTE6,
                    _NOTE7,
                    _NOTE8,
                    _NOTE9,
                    _NOTE10,
                    _NOTE11,
                    _NOTE12,
                    _NOTE13,
                    _NOTE14,
                    _NOTE15,
                    _NOTE16,
                    _NOTE17);
            }
            public string RECORD { get { return "09"; } }
            public string FILENO
            {
                get { return _FILENO; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FILENO = value.Substring(0, 10);
                    }
                    else
                    {
                        _FILENO = value.PadRight(10, ' ');
                    }
                }
            }
            public string FORW_FILE
            {
                get { return _FORW_FILE; }
                set
                {
                    if (value.Length > 20)
                    {
                        _FORW_FILE = value.Substring(0, 20);
                    }
                    else
                    {
                        _FORW_FILE = value.PadRight(20, ' ');
                    }
                }
            }
            public string MASCO_FILE
            {
                get { return _MASCO_FILE; }
                set
                {
                    if (value.Length > 15)
                    {
                        _MASCO_FILE = value.Substring(0, 15);
                    }
                    else
                    {
                        _MASCO_FILE = value.PadRight(15, ' ');
                    }
                }
            }
            public string FORW_ID
            {
                get { return _FORW_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FORW_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FORW_ID = value.PadRight(10, ' ');
                    }
                }
            }
            public string FIRM_ID
            {
                get { return _FIRM_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FIRM_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FIRM_ID = value.PadRight(10, ' ');
                    }
                }
            }
            public string PDATE
            {
                get { return _PDATE; }
                set
                {
                    if (value.Length > 8)
                    {
                        _PDATE = value.Substring(0, 8);
                    }
                    else
                    {
                        _PDATE = value.PadRight(8, ' ');
                    }
                }
            }
            public string PCODE
            {
                get { return _PCODE; }
                set
                {
                    if (value.Length > 8)
                    {
                        _PCODE = value.Substring(0, 8);
                    }
                    else
                    {
                        _PCODE = value.PadRight(8, ' ');
                    }
                }
            }
            public string PCMT
            {
                get { return _PCMT; }
                set
                {
                    if (value.Length > 48)
                    {
                        _PCMT = value.Substring(0, value.Substring(0, 48).LastIndexOf(' '));
                        NOTE2 = value.Substring(value.Substring(0, 48).LastIndexOf(' '));
                    }
                    else
                    {
                        _PCMT = value.PadRight(48, ' ');
                    }
                }
            }
            public string NOTE2
            {
                get { return _NOTE2; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE2 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE3 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE2 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE3
            {
                get { return _NOTE3; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE3 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE4 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE3 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE4
            {
                get { return _NOTE4; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE4 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE5 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE4 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE5
            {
                get { return _NOTE5; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE5 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE6 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE5 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE6
            {
                get { return _NOTE6; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE6 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE7 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE6 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE7
            {
                get { return _NOTE7; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE7 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE8 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE7 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE8
            {
                get { return _NOTE8; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE8 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE9 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE8 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE9
            {
                get { return _NOTE9; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE9 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE10 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE9 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE10
            {
                get { return _NOTE10; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE10 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE11 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE10 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE11
            {
                get { return _NOTE11; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE11 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE12 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE11 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE12
            {
                get { return _NOTE12; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE12 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE13 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE12 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE13
            {
                get { return _NOTE13; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE13 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE14 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE13 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE14
            {
                get { return _NOTE14; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE14 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE15 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE14 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE15
            {
                get { return _NOTE15; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE15 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE16 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE15 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE16
            {
                get { return _NOTE16; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE16 = value.Substring(0, value.Substring(0, 57).LastIndexOf(' '));
                        NOTE17 = value.Substring(value.Substring(0, 57).LastIndexOf(' '));
                    }
                    else
                    {
                        _NOTE16 = value.PadRight(57, ' ');
                    }
                }
            }
            public string NOTE17
            {
                get { return _NOTE17; }
                set
                {
                    if (value.Length > 57)
                    {
                        _NOTE17 = value.Substring(0, 57);
                    }
                    else
                    {
                        _NOTE17 = value.PadRight(57, ' ');
                    }
                }
            }
        }
    }
    #endregion

    #region Record Type 70
    public class RT70Table
    {
        private List<RT70> _Rows = new List<RT70>();
        public List<RT70> RT70Records { get { return _Rows; } }
        public RT70 this[int index] { get { return _Rows[index]; } set { _Rows[index] = value; } }

        public int Count
        {
            get { return _Rows.Count; }
        }

        /// <summary>
        /// YGC Record Type 70
        /// </summary>
        public class RT70
        {
            #region Private Variables
            private string _FILENO = "";
            private string _FORW_FILE = "";
            private string _MASCO_FILE = "";
            private string _FORW_ID = "";
            private string _FIRM_ID = "";
            private string _FORW_NO = "";
            private string _MASCO_NO = "";
            private string _ADVA_NO = "";
            private string _SECY_NO = "";
            private string _ATTY_NO = "";
            private string _COLL_NO = "";
            private string _VENUE1_NO = "";
            private string _VENUE2_NO = "";
            private string _SHERIFF1_NO = "";
            private string _SHERIFF2_NO = "";
            private string _BANK1_NO = "";
            private string _BANK2_NO = "";
            private string _BANK3_NO = "";
            private string _EMPL1_NO = "";
            private string _EMPL2_NO = "";
            private string _EMPL3_NO = "";
            private string _STATUS1_D = "";
            private string _STATUS1_C = "";
            private string _STATUS2_D = "";
            private string _STATUS2_C = "";
            private string _FREF = "";
            private string _CCOMM = "";
            private string _CSFEE = "";
            private string _DNO = "";
            private string _ADVA_NO2 = "";
            private string _ADVA_NO3 = "";
            private string _SALES = "";
            #endregion

            public override string ToString()
            {
                _FILENO = _FILENO.PadRight(10, ' ');
                _FORW_FILE = _FORW_FILE.PadRight(20, ' ');
                _MASCO_FILE = _MASCO_FILE.PadRight(15, ' ');
                _FORW_ID = _FORW_ID.PadRight(10, ' ');
                _FIRM_ID = _FIRM_ID.PadRight(10, ' ');
                _FORW_NO = _FORW_NO.PadRight(5, ' ');
                _MASCO_NO = _MASCO_NO.PadRight(5, ' ');
                _ADVA_NO = _ADVA_NO.PadRight(5, ' ');
                _SECY_NO = _SECY_NO.PadRight(5, ' ');
                _ATTY_NO = _ATTY_NO.PadRight(5, ' ');
                _COLL_NO = _COLL_NO.PadRight(5, ' ');
                _VENUE1_NO = _VENUE1_NO.PadRight(5, ' ');
                _VENUE2_NO = _VENUE2_NO.PadRight(5, ' ');
                _SHERIFF1_NO = _SHERIFF1_NO.PadRight(5, ' ');
                _SHERIFF2_NO = _SHERIFF2_NO.PadRight(5, ' ');
                _BANK1_NO = _BANK1_NO.PadRight(5, ' ');
                _BANK2_NO = _BANK2_NO.PadRight(5, ' ');
                _BANK3_NO = _BANK3_NO.PadRight(5, ' ');
                _EMPL1_NO = _EMPL1_NO.PadRight(5, ' ');
                _EMPL2_NO = _EMPL2_NO.PadRight(5, ' ');
                _EMPL3_NO = _EMPL3_NO.PadRight(5, ' ');
                _STATUS1_D = _STATUS1_D.PadRight(8, ' ');
                _STATUS1_C = _STATUS1_C.PadRight(3, ' ');
                _STATUS2_D = _STATUS2_D.PadRight(8, ' ');
                _STATUS2_C = _STATUS2_C.PadRight(3, ' ');
                _FREF = _FREF.PadRight(10, ' ');
                _CCOMM = _CCOMM.PadRight(4, ' ');
                _CSFEE = _CSFEE.PadRight(4, ' ');
                _DNO = _DNO.PadRight(1, ' ');
                _ADVA_NO2 = _ADVA_NO2.PadRight(5, ' ');
                _ADVA_NO3 = _ADVA_NO3.PadRight(5, ' ');
                _SALES = _SALES.PadRight(5, ' ');
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}",
                    RECORD,
                    _FILENO,
                    _FORW_FILE,
                    _MASCO_FILE,
                    _FORW_ID,
                    _FIRM_ID,
                    _FORW_NO,
                    _MASCO_NO,
                    _ADVA_NO,
                    _SECY_NO,
                    _ATTY_NO,
                    _COLL_NO,
                    _VENUE1_NO,
                    _VENUE2_NO,
                    _SHERIFF1_NO,
                    _SHERIFF2_NO,
                    _BANK1_NO,
                    _BANK2_NO,
                    _BANK3_NO,
                    _EMPL1_NO,
                    _EMPL2_NO,
                    _EMPL3_NO,
                    _STATUS1_D,
                    _STATUS1_C,
                    _STATUS2_D,
                    _STATUS2_C,
                    _FREF,
                    _CCOMM,
                    _CSFEE,
                    _DNO,
                    _ADVA_NO2,
                    _ADVA_NO3,
                    _SALES);
            }

            #region Public Variables
            /// <summary>
            /// YGC Record Type 70
            /// </summary>
            public string RECORD { get { return "70"; } }
            /// <summary>
            /// Sender Internal File Number
            /// </summary>
            public string FILENO
            {
                get { return _FILENO; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FILENO = value.Substring(0, 10);
                    }
                    else
                    {
                        _FILENO = value.PadRight(10, ' ');
                    }
                }
            }
            /// <summary>
            /// Forwarder File Number
            /// </summary>
            public string FORW_FILE
            {
                get { return _FORW_FILE; }
                set
                {
                    if (value.Length > 20)
                    {
                        _FORW_FILE = value.Substring(0, 20);
                    }
                    else
                    {
                        _FORW_FILE = value.PadRight(20, ' ');
                    }
                }
            }
            /// <summary>
            /// Co-Counsel File Number
            /// </summary>
            public string MASCO_FILE
            {
                get { return _MASCO_FILE; }
                set
                {
                    if (value.Length > 15)
                    {
                        _MASCO_FILE = value.Substring(0, 15);
                    }
                    else
                    {
                        _MASCO_FILE = value.PadRight(15, ' ');
                    }
                }
            }
            /// <summary>
            /// Forwarder YGC ID
            /// </summary>
            public string FORW_ID
            {
                get { return _FORW_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FORW_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FORW_ID = value.PadRight(10, ' ');
                    }
                }
            }
            /// <summary>
            /// Firm YGC ID
            /// </summary>
            public string FIRM_ID
            {
                get { return _FIRM_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FIRM_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FIRM_ID = value.PadRight(10, ' ');
                    }
                }
            }
            /// <summary>
            /// Forwarder Number
            /// </summary>
            public int FORW_NO
            {
                get { try { return int.Parse(_FORW_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _FORW_NO = "0".PadRight(5, ' ');
                    else _FORW_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Co-Counsel Number
            /// </summary>
            public int MASCO_NO
            {
                get { try { return int.Parse(_MASCO_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _MASCO_NO = "0".PadRight(5, ' ');
                    else _MASCO_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Adversary Attorney Number
            /// </summary>
            public int ADVA_NO
            {
                get { try { return int.Parse(_ADVA_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _ADVA_NO = "0".PadRight(5, ' ');
                    else _ADVA_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Our Secretary/Paralegal Number
            /// </summary>
            public int SECY_NO
            {
                get { try { return int.Parse(_SECY_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _SECY_NO = "0".PadRight(5, ' ');
                    else _SECY_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Our Attorney Number
            /// </summary>
            public int ATTY_NO
            {
                get { try { return int.Parse(_ATTY_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _ATTY_NO = "0".PadRight(5, ' ');
                    else _ATTY_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Our Collector Number
            /// </summary>
            public int COLL_NO
            {
                get { try { return int.Parse(_COLL_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _COLL_NO = "0".PadRight(5, ' ');
                    else _COLL_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Venue Debtor 1
            /// </summary>
            public int VENUE1_NO
            {
                get { try { return int.Parse(_VENUE1_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _VENUE1_NO = "0".PadRight(5, ' ');
                    else _VENUE1_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Venue Debtor 2
            /// </summary>
            public int VENUE2_NO
            {
                get { try { return int.Parse(_VENUE2_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _VENUE2_NO = "0".PadRight(5, ' ');
                    else _VENUE2_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Sheriff Debtor 1
            /// </summary>
            public int SHERIFF1_NO
            {
                get { try { return int.Parse(_SHERIFF1_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _SHERIFF1_NO = "0".PadRight(5, ' ');
                    else _SHERIFF1_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Sheriff Debtor 2
            /// </summary>
            public int SHERIFF2_NO
            {
                get { try { return int.Parse(_SHERIFF2_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _SHERIFF2_NO = "0".PadRight(5, ' ');
                    else _SHERIFF2_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Bank Debtor 1
            /// </summary>
            public int BANK1_NO
            {
                get { try { return int.Parse(_BANK1_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _BANK1_NO = "0".PadRight(5, ' ');
                    else _BANK1_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Bank Debtor 2
            /// </summary>
            public int BANK2_NO
            {
                get { try { return int.Parse(_BANK2_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _BANK2_NO = "0".PadRight(5, ' ');
                    else _BANK2_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Bank Debtor 3
            /// </summary>
            public int BANK3_NO
            {
                get { try { return int.Parse(_BANK3_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _BANK3_NO = "0".PadRight(5, ' ');
                    else _BANK3_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Employer Debtor 1
            /// </summary>
            public int EMPL1_NO
            {
                get { try { return int.Parse(_EMPL1_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _EMPL1_NO = "0".PadRight(5, ' ');
                    else _EMPL1_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Employer Debtor 2
            /// </summary>
            public int EMPL2_NO
            {
                get { try { return int.Parse(_EMPL2_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _EMPL2_NO = "0".PadRight(5, ' ');
                    else _EMPL2_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Employer Debtor 3
            /// </summary>
            public int EMPL3_NO
            {
                get { try { return int.Parse(_EMPL3_NO.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _EMPL3_NO = "0".PadRight(5, ' ');
                    else _EMPL3_NO = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Status Code Date
            /// </summary>
            public DateTime STATUS1_DATE
            {
                get { try { return DateTime.ParseExact(_STATUS1_D.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return DateTime.MinValue; } }
                set
                {
                    _STATUS1_D = value.ToString("yyyyMMdd");
                }
            }
            /// <summary>
            /// Status Code
            /// </summary>
            public int STATUS1_CODE
            {
                get { try { return int.Parse(_STATUS1_C.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 999 || value < 0) _STATUS1_C = "0".PadRight(3, ' ');
                    else _STATUS1_C = value.ToString().PadRight(3, ' ');
                }
            }
            /// <summary>
            /// Status 2 Code Date
            /// </summary>
            public DateTime STATUS2_DATE
            {
                get { try { return DateTime.ParseExact(_STATUS2_D.Trim(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return DateTime.MinValue; } }
                set
                {
                    _STATUS2_D = value.ToString("yyyyMMdd");
                }
            }
            /// <summary>
            /// Status 2 Code
            /// </summary>
            public int STATUS2_CODE
            {
                get { try { return int.Parse(_STATUS2_C.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 999 || value < 0) _STATUS2_C = "0".PadRight(3, ' ');
                    else _STATUS2_C = value.ToString().PadRight(3, ' ');
                }
            }
            /// <summary>
            /// Internal Forwarder Reference Number
            /// </summary>
            public int FREF
            {
                get { try { return int.Parse(_FREF.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 999999999 || value < 0) _FREF = "".PadRight(10, ' ');
                    else _FREF = value.ToString().PadRight(10, ' ');
                }
            }
            /// <summary>
            /// Adversary Attorney Number Debtor 2
            /// </summary>
            public int ADVA_NO2
            {
                get { try { return int.Parse(_ADVA_NO2.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _ADVA_NO2 = "0".PadRight(5, ' ');
                    else _ADVA_NO2 = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Adversary Attorney Number Debtor 3
            /// </summary>
            public int ADVA_NO3
            {
                get { try { return int.Parse(_ADVA_NO3.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _ADVA_NO3 = "0".PadRight(5, ' ');
                    else _ADVA_NO3 = value.ToString().PadRight(5, ' ');
                }
            }
            /// <summary>
            /// Sales Person
            /// </summary>
            public int SALES
            {
                get { try { return int.Parse(_SALES.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 99999 || value < 0) _SALES = "0".PadRight(5, ' ');
                    else _SALES = value.ToString().PadRight(5, ' ');
                }
            }
            #endregion
        }
    }
    #endregion

    #region Record Type 95
    public class RT95Table
    {
        private List<RT95> _Rows = new List<RT95>();
        public List<RT95> RT95Records { get { return _Rows; } }
        public RT95 this[int index]
        {
            get
            {
                return _Rows[index];
            }
            set
            {
                _Rows[index] = value;
            }
        }

        public int Count
        {
            get { return _Rows.Count; }
        }

        public void Add(string MASCO_FILE, string DIARY_DATE, string DIARY_CODE, string DIARY_CMT, string DIARY_QUEUE)
        {
            RT95 Temp = new RT95();
            Temp.MASCO_FILE = MASCO_FILE;
            Temp.DIARY_DATE = DIARY_DATE;
            Temp.DIARY_CODE = DIARY_CODE;
            Temp.DIARY_CMT = DIARY_CMT;
            Temp.DIARY_QUEUE = DIARY_QUEUE;
            _Rows.Add(Temp);
        }

        public class RT95
        {
            private string _FILENO = "";
            private string _FORW_FILE = "";
            private string _MASCO_FILE = "";
            private string _FORW_ID = "";
            private string _FIRM_ID = "";
            private string _DIARY_DATE = "";
            private string _DIARY_CODE = "";
            private string _DIARY_CMT = "";
            private string _DIARY_QUEUE = "";
            public override string ToString()
            {
                _DIARY_CMT = _DIARY_CMT.PadRight(20, ' ');
                _DIARY_CODE = _DIARY_CODE.PadRight(8, ' ');
                _DIARY_DATE = _DIARY_DATE.PadRight(8, ' ');
                _DIARY_QUEUE = _DIARY_QUEUE.PadRight(8, ' ');
                _FILENO = _FILENO.PadRight(10, ' ');
                _FIRM_ID = _FIRM_ID.PadRight(10, ' ');
                _FORW_FILE = _FORW_FILE.PadRight(20, ' ');
                _FORW_ID = _FORW_ID.PadRight(10, ' ');
                _MASCO_FILE = _MASCO_FILE.PadRight(15, ' ');
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                    RECORD,
                    _FILENO,
                    _FORW_FILE,
                    _MASCO_FILE,
                    _FORW_ID,
                    _FIRM_ID,
                    _DIARY_DATE,
                    _DIARY_CODE,
                    _DIARY_CMT,
                    _DIARY_QUEUE);
            }
            public string RECORD { get { return "95"; } }
            public string FILENO
            {
                get { return _FILENO; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FILENO = value.Substring(0, 10);
                    }
                    else
                    {
                        _FILENO = value.PadRight(10, ' ');
                    }
                }
            }
            public string FORW_FILE
            {
                get { return _FORW_FILE; }
                set
                {
                    if (value.Length > 20)
                    {
                        _FORW_FILE = value.Substring(0, 20);
                    }
                    else
                    {
                        _FORW_FILE = value.PadRight(20, ' ');
                    }
                }
            }
            public string MASCO_FILE
            {
                get { return _MASCO_FILE; }
                set
                {
                    if (value.Length > 15)
                    {
                        _MASCO_FILE = value.Substring(0, 15);
                    }
                    else
                    {
                        _MASCO_FILE = value.PadRight(15, ' ');
                    }
                }
            }
            public string FORW_ID
            {
                get { return _FORW_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FORW_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FORW_ID = value.PadRight(10, ' ');
                    }
                }
            }
            public string FIRM_ID
            {
                get { return _FIRM_ID; }
                set
                {
                    if (value.Length > 10)
                    {
                        _FIRM_ID = value.Substring(0, 10);
                    }
                    else
                    {
                        _FIRM_ID = value.PadRight(10, ' ');
                    }
                }
            }
            public string DIARY_DATE
            {
                get { return _DIARY_DATE; }
                set
                {
                    if (value.Length > 8)
                    {
                        _DIARY_DATE = value.Substring(0, 8);
                    }
                    else
                    {
                        _DIARY_DATE = value.PadRight(8, ' ');
                    }
                }
            }
            public string DIARY_CODE
            {
                get { return _DIARY_CODE; }
                set
                {
                    if (value.Length > 8)
                    {
                        _DIARY_CODE = value.Substring(0, 8);
                    }
                    else
                    {
                        _DIARY_CODE = value.PadRight(8, ' ');
                    }
                }
            }
            public string DIARY_CMT
            {
                get { return _DIARY_CMT; }
                set
                {
                    if (value.Length > 20)
                    {
                        _DIARY_CMT = value.Substring(0, 20);
                    }
                    else
                    {
                        _DIARY_CMT = value.PadRight(20, ' ');
                    }
                }
            }
            public string DIARY_QUEUE
            {
                get { return _DIARY_QUEUE; }
                set
                {
                    if (value.Length > 8)
                    {
                        _DIARY_QUEUE = value.Substring(0, 8);
                    }
                    else
                    {
                        _DIARY_QUEUE = value.PadRight(8, ' ');
                    }
                }
            }
        }
    }
    #endregion

    #region ORCC Upload Record Type
    public class ORCCRecordTable
    {
        private List<ORCCRecord> _Records = new List<ORCCRecord>();
        public List<ORCCRecord> ORCCRecords { get { return _Records; } }

        public ORCCRecordTable(System.Data.SqlClient.SqlDataReader sdr)
        {
            while (sdr.Read())
            {
                ORCCRecord newRec = new ORCCRecord(sdr);
                if (newRec.TrustAccount != "" && newRec.TotalDue != 0) _Records.Add(newRec);
            }
        }

        public void CreateUploadFile(string FileLocation)
        {
            System.IO.FileStream fs = new System.IO.FileStream(FileLocation, System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

            sw.WriteLine(string.Format("HDR      {0}", DateTime.Now.ToString("yyyyMMddHH:mm:ss")).PadRight(800, ' '));
            foreach (ORCCRecord R in _Records)
            {
                sw.WriteLine(R.ToString());
            }
            sw.WriteLine(string.Format("TRL{0}", _Records.Count.ToString()).PadRight(800, ' '));
            sw.Flush();
            sw.Close();
        }

        public class ORCCRecord
        {
            #region Private Variables
            private string _Fileno = "";
            private string _TotalDue = "";
            private string _LPayAmt = "";
            private string _LPayDate = "";
            private string _Street = "";
            private string _Street2 = "";
            private string _City = "";
            private string _St = "";
            private string _Zip = "";
            private string _Phone = "";
            private string _Phone2 = "";
            private string _Fax = "";
            private string _SSN = "";
            private string _RandomDigits = "00";
            private string _PayDue = "00000000";
            private string _CredLimit = "000000000000000";
            private string _MinPayDue = "00000000000";
            private string _DelinquentAmt = "00000000000";
            private string _NumCyclesDelinq = "000";
            private string _CycleCode = "  ";
            private string _DaysDelinq = "000";
            private string _BirthDate = "";
            private string _FName = "";
            private string _LName = "";
            private string _Email = "";
            private string _DateLastReaged = "00000000";
            private string _OpenedDate = "";
            private string _PrevReageDate = "00000000";
            private string _NextReageDate = "00000000";
            private string _LastContactDate = "00000000";
            private string _ProgramStatus = "          ";
            private string _TrustAcct = "";
            private string _SiteKey = "     ";
            private string _Fileno2 = "";
            private string _AcctStatus = "   ";
            private string _Filler = "".PadRight(71, ' ');
            private string _ForwNo = "";
            private string _OrigCred = "";
            private string _Settlement = "";
            private string _Plaintiff = "";
            private string _ForwFileno = "";
            private string _UserDefined7 = "".PadRight(25, ' ');
            private string _UserDefined8 = "".PadRight(25, ' ');
            private string _UserDefined9 = "".PadRight(25, ' ');
            private string _UserDefined10 = "".PadRight(25, ' ');
            #endregion

            public ORCCRecord(System.Data.SqlClient.SqlDataReader sdr)
            {
                this.FileNo = sdr["FILENO"].ToString().RemoveSpecialCharacters();
                this.TotalDue = (double)sdr["BALANCE"];
                try { this.LastPaymentAmount = (double)sdr["LPAYMNT_AMT"]; } catch { }
                try { this.LastPaymentDate = (DateTime)sdr["LPAYMNT_DATE"]; } catch { }
                this.Street = sdr["STREET"].ToString().RemoveSpecialCharacters();
                this.Street2 = sdr["STREET2"].ToString().RemoveSpecialCharacters();
                this.City = sdr["CITY"].ToString().RemoveSpecialCharacters();
                this.State = sdr["ST"].ToString().RemoveSpecialCharacters();
                this.Zip = sdr["ZIP"].ToString();
                this.Phone = sdr["PHONE"].ToString();
                this.Phone2 = sdr["PHONE2"].ToString();
                this.Fax = sdr["FAX"].ToString();
                this.SSN = sdr["SSN"].ToString();
                try { this.BirthDate = (DateTime)sdr["BIRTH_DATE"]; } catch { }
                this.DebtorName = sdr["NAME"].ToString();
                this.Email = sdr["EMAIL"].ToString();
                try { this.OpenedDate = (DateTime)sdr["OPENED_DATE"]; } catch { }
                if (sdr["UPDATEDACCOUNT"] != DBNull.Value) this.TrustAccount = sdr["UPDATEDACCOUNT"].ToString();
                else this.TrustAccount = sdr["ACCOUNT"].ToString();
                this.ForwNo = sdr["FORW_NO"].ToString();
                if (sdr["ORIG_CRED"] != DBNull.Value) this.OriginalCreditor = sdr["ORIG_CRED"].ToString().RemoveSpecialCharacters();
                else this.OriginalCreditor = "N/A";
                if (sdr["ZERORATE"] == DBNull.Value)
                {
                    if (sdr["SETTLEMENT"] != DBNull.Value) { this.Settlement = (int)sdr["SETTLEMENT"]; }
                    else
                    {
                        this.Settlement = SifRateFinder(sdr["SIFRate"].ToString(), sdr["JMT_DATE"], sdr["SUIT_DATE"], sdr["SERVICE_DATE"]);
                        if (sdr["MOD"] != DBNull.Value && this.Settlement != 0)
                            this.Settlement += (int)sdr["MOD"];
                    }
                }
                else
                {
                    this.Settlement = 0;
                }
                this.Plaintiff = sdr["PLAINTIFF_1"].ToString().RemoveSpecialCharacters() + " " + sdr["PLAINTIFF_2"].ToString().RemoveSpecialCharacters();
                this.ForwFileNo = sdr["FORW_FILENO"].ToString().RemoveSpecialCharacters();
            }

            public override string ToString()
            {
                CheckValues();
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}",
                    _Fileno,
                    _TotalDue,
                    _LPayAmt,
                    _LPayDate,
                    _Street,
                    _Street2,
                    _City,
                    _St,
                    _Zip,
                    _Phone,
                    _Phone2,
                    _Fax,
                    _SSN,
                    _RandomDigits,
                    _PayDue,
                    _CredLimit,
                    _MinPayDue,
                    _DelinquentAmt,
                    _NumCyclesDelinq,
                    _CycleCode,
                    _DaysDelinq,
                    _BirthDate,
                    _FName,
                    _LName,
                    _Email,
                    _DateLastReaged,
                    _OpenedDate,
                    _PrevReageDate,
                    _NextReageDate,
                    _LastContactDate,
                    _ProgramStatus,
                    _TrustAcct,
                    _SiteKey,
                    _Fileno2,
                    _AcctStatus,
                    _Filler,
                    _TrustAcct,
                    _ForwNo,
                    _OrigCred,
                    _Settlement,
                    _Plaintiff,
                    _ForwFileno,
                    _UserDefined7,
                    _UserDefined8,
                    _UserDefined9,
                    _UserDefined10);
            }

            #region Public Variables
            public string FileNo
            {
                get
                {
                    return _Fileno.Trim();
                }
                set
                {
                    if (value.Length > 30) _Fileno = value.Substring(0, 30);
                    else _Fileno = value.PadRight(30, ' ');
                    if (value.Length > 40) _Fileno2 = value.Substring(0, 40);
                    else _Fileno2 = value.PadRight(40, ' ');
                }
            }
            public double TotalDue
            {
                get { try { return double.Parse(_TotalDue) / 100; } catch { return 0; } }
                set
                {
                    if (NumberCleanup(value.ToString("0.00")).Length > 11) _TotalDue = "99999999999";
                    else _TotalDue = NumberCleanup(value.ToString("0.00")).PadLeft(11, '0');
                }
            }
            public double LastPaymentAmount
            {
                get { try { return double.Parse(_LPayAmt) / 100; } catch { return 0; } }
                set
                {
                    if (NumberCleanup(value.ToString("0.00")).Length > 11) _LPayAmt = "99999999999";
                    else _LPayAmt = NumberCleanup(value.ToString("0.00")).PadLeft(11, '0');
                }
            }
            public DateTime LastPaymentDate
            {
                get { try { return DateTime.ParseExact(_LPayDate, "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture); } catch { return DateTime.MinValue; } }
                set
                {
                    _LPayDate = "00000000";
                    try { _LPayDate = value.ToString("MMddyyyy"); }
                    catch { _LPayDate = "00000000"; }
                }
            }
            public string Street
            {
                get { return _Street.Trim(); }
                set
                {
                    if (value.Length > 26) _Street = value.Substring(0, 26);
                    else _Street = value.PadRight(26, ' ');
                }
            }
            public string Street2
            {
                get { return _Street2.Trim(); }
                set
                {
                    if (value.Length > 26) _Street2 = value.Substring(0, 26);
                    else _Street2 = value.PadRight(26, ' ');
                }
            }
            public string City
            {
                get { return _City.Trim(); }
                set
                {
                    if (value.Length > 20) _City = value.Substring(0, 20);
                    else _City = value.PadRight(20, ' ');
                }
            }
            public string State
            {
                get { return _St.Trim(); }
                set
                {
                    if (value.Length > 2) _St = value.Substring(0, 2);
                    else _St = value.PadRight(2, ' ');
                }
            }
            public string Zip
            {
                get { return _Zip.Trim(); }
                set
                {
                    if (NumberCleanup(value).Length > 9) _Zip = NumberCleanup(value).Substring(0, 9);
                    else _Zip = NumberCleanup(value).PadRight(9, '0');
                }
            }
            public string Phone
            {
                get { return _Phone.Trim(); }
                set
                {
                    if (NumberCleanup(value).Length != 10) _Phone = "0000000000";
                    else _Phone = NumberCleanup(value);
                }
            }
            public string Phone2
            {
                get { return _Phone2.Trim(); }
                set
                {
                    if (NumberCleanup(value).Length != 10) _Phone2 = "0000000000";
                    else _Phone2 = NumberCleanup(value);
                }
            }
            public string Fax
            {
                get { return _Fax.Trim(); }
                set
                {
                    if (NumberCleanup(value).Length != 10) _Fax = "0000000000";
                    else _Fax = NumberCleanup(value);
                }
            }
            public string SSN
            {
                get { return _SSN.Trim(); }
                set
                {
                    if (NumberCleanup(value).Length > 9) _SSN = "000000000";
                    else _SSN = NumberCleanup(value).PadRight(9, '0');
                }
            }
            public DateTime BirthDate
            {
                get { try { return DateTime.ParseExact(_BirthDate, "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture); } catch { return DateTime.MinValue; } }
                set
                {
                    try { _BirthDate = value.ToString("MMddyyyy"); }
                    catch { _BirthDate = "00000000"; }
                }
            }
            public string FirstName
            {
                get { return _FName.Trim(); }
                set
                {
                    if (value.Length > 25) _FName = value.Substring(0, 25);
                    else _FName = value.PadRight(25, ' ');
                }
            }
            public string LastName
            {
                get { return _LName.Trim(); }
                set
                {
                    if (value.Length > 26) _LName = value.Substring(0, 26);
                    else _LName = value.PadRight(26, ' ');
                }
            }
            public string DebtorName
            {
                get { return _FName + " " + _LName; }
                set
                {
                    if (value.Contains('/'))
                    {
                        if (value.Split('/')[1].Contains(' '))
                        {
                            FirstName = value.Split('/')[1].Substring(0, value.Split('/')[1].IndexOf(' '));
                            LastName = value.Split('/')[0];
                        }
                        else
                        {
                            FirstName = value.Split('/')[1];
                            LastName = value.Split('/')[0];
                        }
                    }
                    else
                    {
                        if (value.Contains(' '))
                        {
                            FirstName = value.Split(' ')[0];
                            LastName = value.Split(' ')[1];
                        }
                        else
                        {
                            FirstName = value;
                            LastName = value;
                        }
                    }
                }
            }
            public string Email
            {
                get { return _Email.Trim(); }
                set
                {
                    if (value.Contains('@') && !value.Contains(' '))
                    {
                        if (value.Length > 60) _Email = value.Substring(0, 60);
                        else _Email = value.PadRight(60, ' ');
                    }
                    else
                    {
                        _Email = "".PadRight(60, ' ');
                    }
                }
            }
            public DateTime OpenedDate
            {
                get { try { return DateTime.ParseExact(_OpenedDate, "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture); } catch { return DateTime.MinValue; } }
                set
                {
                    try { _OpenedDate = value.ToString("MMddyyyy"); }
                    catch { _OpenedDate = "00000000"; }
                }
            }
            public string TrustAccount
            {
                get { return _TrustAcct.Trim(); }
                set
                {
                    if (value.Length > 25) _TrustAcct = value.Substring(0, 25);
                    else _TrustAcct = value.PadRight(25, ' ');
                }
            }
            public string ForwNo
            {
                get { return _ForwNo.Trim(); }
                set
                {
                    if (value.Length > 25) _ForwNo = value.Substring(0, 25);
                    else _ForwNo = value.PadRight(25, ' ');
                }
            }
            public string OriginalCreditor
            {
                get { return _OrigCred.Trim(); }
                set
                {
                    value = value.Trim();
                    if (value.Length > 25)
                    {
                        if (value.Substring(25, 1) == " ") value = value.Substring(0, 25);
                        else if (value.Substring(0, 25).Contains(' ')) value = value.Substring(0, 25).Substring(0, value.Substring(0, 25).LastIndexOf(' '));
                        else value = "N/A";
                    }
                    if (value.Length > 0)
                    { if (value.Substring(value.Length - 1, 1) == "," || value.Substring(value.Length - 1, 1) == "&" || value.Substring(value.Length - 1, 1) == "-") value = value.Substring(0, value.Length - 1); }
                    if (value.Length > 1)
                    { if (value.Substring(value.Length - 2, 2) == "AS" || value.Substring(value.Length - 2, 2) == "OF") value = value.Substring(0, value.Length - 2); }
                    if (value.Length > 2)
                    { if (value.Substring(value.Length - 3, 3) == "DBA") value = value.Substring(0, value.Length - 3); }
                    _OrigCred = value.PadRight(25, ' ');
                }
            }
            public int Settlement
            {
                get { try { return int.Parse(_Settlement.Trim()); } catch { return 0; } }
                set
                {
                    if (value > 100 || value < 0) _Settlement = "0".PadRight(25, ' ');
                    else _Settlement = value.ToString().PadRight(25, ' ');
                }
            }
            public string Plaintiff
            {
                get { return _Plaintiff.Trim(); }
                set
                {
                    value = value.Trim();
                    if (value.Length > 25)
                    {
                        if (value.Substring(25, 1) == " ") value = value.Substring(0, 25);
                        else if (value.Substring(0, 25).Contains(' ')) value = value.Substring(0, 25).Substring(0, value.Substring(0, 25).LastIndexOf(' '));
                        else value = "";
                    }
                    if (value.Length > 0)
                    { if (value.Substring(value.Length - 1, 1) == "," || value.Substring(value.Length - 1, 1) == "&" || value.Substring(value.Length - 1, 1) == "-") value = value.Substring(0, value.Length - 1); }
                    if (value.Length > 1)
                    { if (value.Substring(value.Length - 2, 2) == "AS" || value.Substring(value.Length - 2, 2) == "OF") value = value.Substring(0, value.Length - 2); }
                    if (value.Length > 2)
                    { if (value.Substring(value.Length - 3, 3) == "DBA") value = value.Substring(0, value.Length - 3); }
                    _Plaintiff = value.PadRight(25, ' ');
                }
            }
            public string ForwFileNo
            {
                get { return _ForwFileno.Trim(); }
                set
                {
                    if (value.Length > 25) _ForwFileno = value.Substring(0, 25);
                    else _ForwFileno = value.PadRight(25, ' ');
                }
            }
            #endregion

            #region Private Methods
            private void CheckValues()
            {
                if (_Fileno.Length > 30) this.FileNo = _Fileno;
                else { _Fileno.PadRight(30, ' '); _Fileno2.PadRight(40, ' '); }
                if (_TotalDue.Length > 11) this.TotalDue = double.Parse(_TotalDue) / 100;
                else { _TotalDue.PadLeft(11, '0'); }
                if (_LPayAmt.Length > 11) this.LastPaymentAmount = double.Parse(_LPayAmt) / 100;
                else { _LPayAmt.PadLeft(11, '0'); }
                if (_LPayDate.Length > 8 || _LPayDate == "") _LPayDate = "00000000";
                else _LPayDate.PadRight(8, '0');
                if (_Street.Length > 26) this.Street = _Street;
                else _Street.PadRight(26, ' ');
                if (_Street2.Length > 25) this.Street2 = _Street2;
                else _Street2.PadRight(25, ' ');
                if (_City.Length > 20) this.City = _City;
                else _City.PadRight(20, ' ');
                if (_St.Length > 2) _St = "  ";
                else _St.PadRight(2, ' ');
                if (_Zip.Length > 9) this.Zip = _Zip;
                else _Zip.PadRight(9, '0');
                if (_Phone.Length > 10) this.Phone = _Phone;
                else _Phone.PadRight(10, '0');
                if (_Phone2.Length > 10) this.Phone2 = _Phone2;
                else _Phone2.PadRight(10, '0');
                if (_Fax.Length > 10) this.Fax = _Fax;
                else _Fax.PadRight(10, '0');
                if (_SSN.Length > 9 || _SSN == "") _SSN = "000000000";
                else _SSN.PadRight(9, '0');
                if (_BirthDate.Length > 8 || _BirthDate == "") _BirthDate = "00000000";
                else _BirthDate.PadRight(8, '0');
                if (_FName.Length > 25) this.FirstName = _FName;
                else _FName.PadRight(25, ' ');
                if (_LName.Length > 26) this.LastName = _LName;
                else _LName.PadRight(26, ' ');
                if (_Email.Length > 60) this.Email = _Email;
                else _Email.PadRight(60, ' ');
                if (_OpenedDate.Length > 8 || _OpenedDate == "") _OpenedDate = "00000000";
                else _OpenedDate.PadRight(8, '0');
                if (_TrustAcct.Length > 25) this.TrustAccount = _TrustAcct;
                else _TrustAcct.PadRight(25, ' ');
                if (_ForwNo.Length > 25) this.ForwNo = _ForwNo;
                else _ForwNo.PadRight(25, ' ');
                if (_OrigCred.Length > 25) this.OriginalCreditor = _OrigCred;
                else _OrigCred.PadRight(25, ' ');
                int temp;
                if (int.TryParse(_Settlement.Trim(), out temp))
                {
                    if (_Settlement.Length > 25) this.Settlement = 0;
                    else _Settlement.PadRight(25, ' ');
                }
                else
                {
                    this.Settlement = 0;
                }
                if (_Plaintiff.Length > 25) this.Plaintiff = _Plaintiff;
                else _Plaintiff.PadRight(25, ' ');
                if (_ForwFileno.Length > 25) this.ForwFileNo = _ForwFileno;
                else _ForwFileno.PadRight(25, ' ');
            }
            private string NumberCleanup(string theString)
            {
                Regex regex = new Regex(@"\d");
                MatchCollection match = regex.Matches(theString);
                StringBuilder sb = new StringBuilder();
                foreach (Match m in match)
                {
                    if (m.Success)
                    {
                        sb.Append(m.Value);
                    }
                }
                return sb.ToString();
            }
            private int SifRateFinder(string SifString, object JudgmentDate, object SuitDate, object ServiceDate)
            {
                if (SifString == "CONTACT PORTFOLIO ANALYST")
                {
                    return 0;
                }
                else if (SifString.Contains('+'))
                {
                    return int.Parse(NumberCleanup(SifString));
                }
                else if (SifString.Contains(';'))
                {
                    string[] Rates = SifString.Split(';');
                    if (JudgmentDate != null && JudgmentDate != DBNull.Value)
                        return int.Parse(NumberCleanup(Rates[2]));
                    else if ((SuitDate != null && SuitDate != DBNull.Value) || (ServiceDate != null && ServiceDate != DBNull.Value))
                        return int.Parse(NumberCleanup(Rates[1]));
                    else
                        return int.Parse(NumberCleanup(Rates[0]));
                }
                else
                    return 100;
            }
            private double CalcDue(DateTime INT_DATE, double STAT_FEE, double COST_EXP, double COST_RECOVERED, double STAT_EARN, double BALANCE, double PRE_J_RATE, double POST_J_RATE, double STORED_INT, double JMT_AMT, double INT_COLL)
            {
                // Predefined variables which store the values to calculate the accrued interest
                DateTime Today = DateTime.Now;
                int NumDays;
                double PreJBase;
                double PreJPerDiem;
                double PostJBase;
                double PostJPerDiem;
                double AccruPre;
                double AccruPost;
                double DuePre;
                double DuePost;
                bool Calc_Int_On_PreJudgment_Costs;
                bool Calc_Int_On_PostJudgment_Stat_Fees;
                bool Calc_Int_On_PostJudgment_Costs;
                bool Calc_Int_On_PreJudgment_Stat_Fees;
                int IntBase_PreJCost;
                int IntBase_PostJCost;
                int IntBase_PreJStat;
                int IntBase_PostJStat;
                // Get todays date and store it in Today
                // Interest Calculation Setup
                // Change Y/N values as needed to
                // match setting in 1-S-4 setup
                // for interest math
                Calc_Int_On_PreJudgment_Costs = false;
                Calc_Int_On_PostJudgment_Costs = true;
                Calc_Int_On_PreJudgment_Stat_Fees = false;
                Calc_Int_On_PostJudgment_Stat_Fees = true;
                //  Int calc'ed always on prin
                if ((!Calc_Int_On_PreJudgment_Costs))
                {
                    IntBase_PreJCost = 1;
                }
                else
                {
                    IntBase_PreJCost = 0;
                }
                if ((Calc_Int_On_PostJudgment_Costs))
                {
                    IntBase_PostJCost = 1;
                }
                else
                {
                    IntBase_PostJCost = 0;
                }
                if ((!Calc_Int_On_PreJudgment_Stat_Fees))
                {
                    IntBase_PreJStat = 1;
                }
                else
                {
                    IntBase_PreJStat = 0;
                }
                if ((Calc_Int_On_PostJudgment_Stat_Fees))
                {
                    IntBase_PostJStat = 1;
                }
                else
                {
                    IntBase_PostJStat = 0;
                }
                // now that we have all the field values we can compute the accrued interest
                NumDays = (Today - INT_DATE).Days;
                if (NumDays < 0) NumDays *= -1;
                PreJBase = (BALANCE - (((STAT_FEE - STAT_EARN) * IntBase_PreJStat) + ((COST_EXP - COST_RECOVERED) * IntBase_PreJCost)));
                PostJBase = (BALANCE - (((STAT_FEE - STAT_EARN) * IntBase_PostJStat) + ((COST_EXP - COST_RECOVERED) * IntBase_PostJCost)));
                PreJPerDiem = ((PRE_J_RATE / 100) * (PreJBase / 365));
                PostJPerDiem = ((POST_J_RATE / 100) * (PostJBase / 365));
                if ((JMT_AMT > 0))
                {
                    AccruPost = ((NumDays * PostJPerDiem) + STORED_INT);
                    // Accrued-Post
                    DuePost = (AccruPost - INT_COLL);
                    // Due-Post
                    return Math.Round(DuePost, 2);
                }
                else
                {
                    AccruPre = ((NumDays * PreJPerDiem) + STORED_INT);
                    // Accrued-Pre
                    DuePre = (AccruPre - INT_COLL);
                    //  Due-Pre
                    return Math.Round(DuePre, 2);
                }
            }
            #endregion
        }
    }
    #endregion

    #region ORCC Action Log Record Type
    public class ORCCActionLogs
    {
        #region Private Properties
        private List<ORCCActionLogAction> _Actions = new List<ORCCActionLogAction>();
        private int _BatchNum;
        private int _Records;
        private DateTime _BatchDate;
        #endregion

        #region Public Properties
        public List<ORCCActionLogAction> Actions { get { return _Actions; } }
        public int BatchNumber { get { return _BatchNum; } }
        public int Records { get { return _Records; } }
        public DateTime BatchDate { get { return _BatchDate; } }
        public string FileNos
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                List<string> filenos = new List<string>();
                foreach (ORCCActionLogAction ALA in _Actions)
                {
                    if (!filenos.Contains(ALA.AccountNumber))
                        filenos.Add(ALA.AccountNumber);
                }
                foreach (string S in filenos)
                {
                    sb.Append("'" + S + "',");
                }
                if (sb.Length == 0)
                    return "''";
                else
                    return sb.ToString().Substring(0, sb.Length - 1);
            }
        }
        #endregion

        public ORCCActionLogs(System.IO.StreamReader FileReader)
        {
            while (!FileReader.EndOfStream)
            {
                string LineRead = FileReader.ReadLine();
                if (LineRead.Substring(0, 3) == "HDR")
                {
                    _BatchNum = int.Parse(LineRead.Substring(3, 6).Trim());
                    _BatchDate = DateTime.ParseExact(LineRead.Substring(9, 16).Trim(), "yyyyMMddHH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                }
                else if (LineRead.Substring(0, 3) == "TRL")
                {
                    _Records = int.Parse(LineRead.Substring(3, 6).Trim());
                }
                else
                {
                    ORCCActionLogAction OALA = new ORCCActionLogAction(LineRead);
                    switch (OALA.ActionType)
                    {
                        case "CancelSrvReq":
                            _Actions.Add(new ORCCCancelAction(OALA));
                            break;
                        case "EditAddressInfo":
                        case "EditAddressNumberInfoCW":
                            _Actions.Add(new ORCCEditAddressAction(OALA));
                            break;
                        case "EditPhoneNumberInfo":
                        case "EditPhoneNumberInfoCW":
                            _Actions.Add(new ORCCEditPhoneAction(OALA));
                            break;
                        case "EditEmailContactInfo":
                        case "EditEmailContactInfoCW":
                            _Actions.Add(new ORCCEditEmailAction(OALA));
                            break;
                        case "FailLogin":
                            _Actions.Add(new ORCCFailedLoginAction(OALA));
                            break;
                        case "Login":
                            _Actions.Add(new ORCCLoginAction(OALA));
                            break;
                        case "PmtIntermediate":
                        case "PmtIntermediateCW":
                        case "PmtMin":
                        case "PmtMinCW":
                        case "PmtOverTotal":
                        case "PmtOverTotalCW":
                        case "PmtPaidInFull":
                        case "PmtPaidInFullCW":
                        case "PmtSpecificAmt":
                        case "PmtSubMin":
                        case "PmtSubMinCW":
                        case "PmtTotal":
                        case "PmtTotalCW":
                        case "SubmitBillPayment":
                        case "SubmitBillPaymentCW":
                            _Actions.Add(new ORCCPaymentAction(OALA));
                            break;
                        case "MultiplePmt":
                        case "MultiplePmtCW":
                            _Actions.Add(new ORCCMultiplePaymentAction(OALA));
                            break;
                        case "Prg_Accepted":
                        case "Prg_AcceptedCW":
                            _Actions.Add(new ORCCProgramAcceptedAction(OALA));
                            break;
                        case "Prg_Applied":
                            _Actions.Add(new ORCCProgramAppliedAction(OALA));
                            break;
                        case "Prg_Offered":
                        case "Prg_OfferedCW":
                            _Actions.Add(new ORCCProgramOfferedAction(OALA));
                            break;
                        case "Prg_NotOffered":
                        case "Prg_NotOfferedCW":
                            _Actions.Add(new ORCCProgramNotOfferedAction(OALA));
                            break;
                        case "Prg_PmtSubmitted":
                        case "Prg_PmtSubmittedCW":
                            _Actions.Add(new ORCCProgramPaymentAction(OALA));
                            break;
                        case "PromiseToPay":
                        case "PromiseToPayCW":
                        case "PromiseToPayStandAlone":
                            _Actions.Add(new ORCCPromiseToPayAction(OALA));
                            break;
                        case "RequestAdvisorAppt":
                            _Actions.Add(new ORCCRequestAdvisorAction(OALA));
                            break;
                        case "UserEnrolled":
                            _Actions.Add(new ORCCUserEnrolledAction(OALA));
                            break;
                        default:
                            _Actions.Add(OALA);
                            break;
                    }
                }
            }
        }

        public class ORCCActionLogAction
        {
            #region Private Properties
            private string _ActionType;
            private int _DelinqStage;
            private string _Account;
            private string _AccountID;
            private string _Data1;
            private string _Data2;
            private string _Data3;
            private string _Data4;
            private string _Data5;
            private string _Data6;
            private string _Data7;
            private string _Data8;
            private DateTime _CreateDate;
            private string _Line;
            #endregion

            #region Public Properties
            public string ActionType { get { return _ActionType.Trim(); } }
            public int DelinquencyStage { get { return _DelinqStage; } }
            public string AccountNumber { get { return _Account.Trim(); } }
            public string AccountID { get { return _AccountID.Trim(); } }
            protected virtual string Data1 { get { return _Data1.Trim(); } }
            protected virtual string Data2 { get { return _Data2.Trim(); } }
            protected virtual string Data3 { get { return _Data3.Trim(); } }
            protected virtual string Data4 { get { return _Data4.Trim(); } }
            protected virtual string Data5 { get { return _Data5.Trim(); } }
            protected virtual string Data6 { get { return _Data6.Trim(); } }
            protected virtual string Data7 { get { return _Data7.Trim(); } }
            protected virtual string Data8 { get { return _Data8.Trim(); } }
            public DateTime CreateDate { get { return _CreateDate; } }
            #endregion

            public ORCCActionLogAction(string LineEntry)
            {
                if (LineEntry.Length == 560)
                {
                    _Line = LineEntry;
                    _ActionType = LineEntry.Substring(0, 50);
                    _DelinqStage = int.Parse(LineEntry.Substring(50, 5).Trim());
                    _Account = LineEntry.Substring(55, 40);
                    _AccountID = LineEntry.Substring(95, 40);
                    _Data1 = LineEntry.Substring(135, 50);
                    _Data2 = LineEntry.Substring(185, 50);
                    _Data3 = LineEntry.Substring(235, 50);
                    _Data4 = LineEntry.Substring(285, 50);
                    _Data5 = LineEntry.Substring(335, 50);
                    _Data6 = LineEntry.Substring(385, 50);
                    _Data7 = LineEntry.Substring(435, 50);
                    _Data8 = LineEntry.Substring(485, 50);
                    _CreateDate = DateTime.ParseExact(LineEntry.Substring(535, 25).Trim(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                }
            }

            protected internal ORCCActionLogAction(ORCCActionLogAction Set)
            {
                _Line = Set.ToString();
                _ActionType = Set.ActionType;
                _DelinqStage = Set.DelinquencyStage;
                _Account = Set.AccountNumber;
                _AccountID = Set.AccountID;
                _Data1 = Set.Data1;
                _Data2 = Set.Data2;
                _Data3 = Set.Data3;
                _Data4 = Set.Data4;
                _Data5 = Set.Data5;
                _Data6 = Set.Data6;
                _Data7 = Set.Data7;
                _Data8 = Set.Data8;
                _CreateDate = Set.CreateDate;
            }

            public override string ToString()
            {
                return _Line;
            }
        }

        public class ORCCCancelAction : ORCCActionLogAction
        {
            public ORCCCancelAction(ORCCActionLogAction Action) : base(Action) { }

            public string CaseNumber { get { return base.Data2; } }
        }

        public class ORCCPaymentAction : ORCCCancelAction
        {
            public ORCCPaymentAction(ORCCActionLogAction Action) : base(Action) { }

            public decimal Amount { get { return decimal.Parse(base.Data1); } }
            public string IP { get { return base.Data3; } }
            public decimal? RushFee { get { return decimal.Parse(base.Data4 != "" ? base.Data4 : "0"); } }
            public DateTime PaymentDate { get { return DateTime.Parse(base.Data5); } }
            public decimal PMBFee { get { return decimal.Parse(base.Data6); } }
            public string CWUserName { get { return base.Data7; } }
            public string ConfirmationNumber { get { return base.Data8; } }
        }

        public class ORCCMultiplePaymentAction : ORCCActionLogAction
        {
            public ORCCMultiplePaymentAction(ORCCActionLogAction Action) : base(Action) { }

            public decimal Amount { get { return decimal.Parse(base.Data1); } }
            public decimal PMBFee { get { return decimal.Parse(base.Data2); } }
            public int NumberOfPayments { get { return int.Parse(base.Data3); } }
            public string CWUserName { get { return base.Data4; } }
        }

        public class ORCCEditAddressAction : ORCCActionLogAction
        {
            public ORCCEditAddressAction(ORCCActionLogAction Action) : base(Action) { }

            public string Street { get { return base.Data1; } }
            public string Street2 { get { return base.Data2; } }
            public string City { get { return base.Data3; } }
            public string State { get { return base.Data4; } }
            public string Zip { get { return base.Data5; } }
        }

        public class ORCCEditPhoneAction : ORCCActionLogAction
        {
            public ORCCEditPhoneAction(ORCCActionLogAction Action) : base(Action) { }

            public string HomePhone { get { return base.Data1; } }
            public string BusinessPhone { get { return base.Data2; } }
            public string CellPhone { get { return base.Data3; } }
        }

        public class ORCCEditEmailAction : ORCCActionLogAction
        {
            public ORCCEditEmailAction(ORCCActionLogAction Action) : base(Action) { }

            public string Email { get { return base.Data1; } }
        }

        public class ORCCFailedLoginAction : ORCCActionLogAction
        {
            public ORCCFailedLoginAction(ORCCActionLogAction Action) : base(Action) { }

            public string Reason { get { return base.Data1; } }
        }

        public class ORCCLoginAction : ORCCActionLogAction
        {
            public ORCCLoginAction(ORCCActionLogAction Action) : base(Action) { }

            public string UserID { get { return base.Data1; } }
        }

        public class ORCCProgramOfferedAction : ORCCActionLogAction
        {
            public ORCCProgramOfferedAction(ORCCActionLogAction Action) : base(Action) { }

            public string ProgramName { get { return base.Data1; } }
            public string CWUserName { get { return base.Data7; } }
        }

        public class ORCCProgramNotOfferedAction : ORCCProgramOfferedAction
        {
            public ORCCProgramNotOfferedAction(ORCCActionLogAction Action) : base(Action) { }

            public string Reason { get { return base.Data2; } }
        }

        public class ORCCProgramAcceptedAction : ORCCProgramOfferedAction
        {
            public ORCCProgramAcceptedAction(ORCCActionLogAction Action) : base(Action) { }

            public decimal ProgramAmount { get { return decimal.Parse(base.Data2); } }
            public decimal BalanceAtAcceptance { get { return decimal.Parse(base.Data3); } }
        }

        public class ORCCProgramPaymentAction : ORCCProgramAcceptedAction
        {
            public ORCCProgramPaymentAction(ORCCActionLogAction Action) : base(Action) { }

            public int TotalProgramPayments { get { return int.Parse(base.Data4); } }
            public int SuccessfullPayments { get { return int.Parse(base.Data5); } }
        }

        public class ORCCProgramAppliedAction : ORCCActionLogAction
        {
            public ORCCProgramAppliedAction(ORCCActionLogAction Action) : base(Action) { }

            public string Answer1 { get { return base.Data1; } }
            public string Answer2 { get { return base.Data2; } }
            public string Answer3 { get { return base.Data3; } }
            public string Answer4 { get { return base.Data4; } }
            public string Answer5 { get { return base.Data5; } }
            public string Answer6 { get { return base.Data6; } }
            public string Answer7 { get { return base.Data7; } }
        }

        public class ORCCUserEnrolledAction : ORCCActionLogAction
        {
            public ORCCUserEnrolledAction(ORCCActionLogAction Action) : base(Action) { }

            public string UserID { get { return base.Data1; } }
        }

        public class ORCCPromiseToPayAction : ORCCActionLogAction
        {
            public ORCCPromiseToPayAction(ORCCActionLogAction Action) : base(Action) { }

            public decimal PromiseAmount { get { return decimal.Parse(base.Data1); } }
            public string CaseNumber { get { return base.Data2; } }
            public string PromiseMethod { get { return base.Data4; } }
            public DateTime PromiseDate { get { return DateTime.Parse(base.Data5); } }
        }

        public class ORCCRequestAdvisorAction : ORCCActionLogAction
        {
            public ORCCRequestAdvisorAction(ORCCActionLogAction Action) : base(Action) { }

            public DateTime Appointment
            {
                get
                {
                    try
                    {
                        return DateTime.Parse(base.Data1 + " " + base.Data2);
                    }
                    catch { return DateTime.Parse(base.Data1); }
                }
            }
            public string Phone { get { return base.Data3; } }
            public string Email { get { return base.Data4; } }
            public string TimeZone { get { return base.Data5; } }
        }
    }
    #endregion

}
