using RecordTypes.PLX.Enums;
using RecordTypes.PLX2.DataTypes;

namespace RecordTypes.PLX2
{
    public class AdditionalInformationUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Merchant Name
        /// </summary>
        public PLXString Merchant { get; private set; }
        /// <summary>
        /// Status if Reportable to Credit Bureau
        /// </summary>
        public PLXBool CBReportable { get; private set; }
        /// <summary>
        /// Charge-Off Balance
        /// </summary>
        public PLXDecimal ChgOffBalance { get; private set; }
        /// <summary>
        /// Charge-Off Date
        /// </summary>
        public PLXDate ChgOffDate { get; private set; }
        /// <summary>
        /// Owner Name
        /// </summary>
        public PLXString OwnerName { get; private set; }
        /// <summary>
        /// Last Purchase Amount
        /// </summary>
        public PLXDecimal LastPurchAmt { get; private set; }
        /// <summary>
        /// Miscellaneous Data
        /// </summary>
        public PLXString Misc1 { get; private set; }
        /// <summary>
        /// Miscellaneous Data
        /// </summary>
        public PLXString Misc2 { get; private set; }
        /// <summary>
        /// Miscellaneous Data
        /// </summary>
        public PLXString Misc3 { get; private set; }
        /// <summary>
        /// Origination Date
        /// </summary>
        public PLXDate OriginationDate { get; private set; }
        /// <summary>
        /// Portfolio ID
        /// </summary>
        public PLXString PortfolioID { get; private set; }
        /// <summary>
        /// Seller Name
        /// </summary>
        public PLXString SellerName { get; private set; }
        /// <summary>
        /// Telecomm Phone Number
        /// </summary>
        public PLXString TelecommPhone { get; private set; }
        /// <summary>
        /// Trade Date
        /// </summary>
        public PLXDate PurchaseDate { get; private set; }
        /// <summary>
        /// Original Creditor Name
        /// </summary>
        public PLXString OriginalCreditor { get; private set; }
        /// <summary>
        /// Out of Statute Date
        /// </summary>
        public PLXDate OutOfStatuteDate { get; private set; }
        /// <summary>
        /// Status if a CBR Negative Notice is Required
        /// </summary>
        public PLXBool SendCBRNegNotice { get; private set; }
        /// <summary>
        /// Status if a OOS Notice is Required
        /// </summary>
        public PLXBool SendOOSNotice { get; private set; }
        /// <summary>
        /// Information concerning Original Creditor, Merchant, or Prior Owners
        /// </summary>
        public PLXString LegacyProductInfo { get; private set; }
        /// <summary>
        /// Paper Type of Account (Bankcard, Retail, Installment Loan, etc.)
        /// </summary>
        public PLXString AcctType { get; private set; }
        /// <summary>
        /// First Delinquency Date
        /// </summary>
        public PLXDate FirstDelinqDate { get; private set; }
        /// <summary>
        /// Charge-Off Creditor Address
        /// </summary>
        public PLXString COCAddress { get; private set; }
        /// <summary>
        /// Charge-Off Creditor Address2
        /// </summary>
        public PLXString COCAddress2 { get; private set; }
        /// <summary>
        /// Charge-Off Creditor City
        /// </summary>
        public PLXString COCCity { get; private set; }
        /// <summary>
        /// Charge-Off Creditor State
        /// </summary>
        public PLXString COCState { get; private set; }
        /// <summary>
        /// Charge-Off Creditor Zip
        /// </summary>
        public PLXString COCZip { get; private set; }
        /// <summary>
        /// Charge-Off Creditor Name
        /// </summary>
        public PLXString ChargeOffCreditorName { get; private set; }
        #endregion

        public AdditionalInformationUpdateRecord(string Record)
            : base(Record)
        {
            this.Merchant = new PLXString(this.LineItems[3]);
            this.CBReportable = new PLXBool(this.LineItems[4]);
            this.ChgOffBalance = new PLXDecimal(this.LineItems[5]);
            this.ChgOffDate = new PLXDate(this.LineItems[6]);
            this.OwnerName = new PLXString(this.LineItems[7]);
            this.LastPurchAmt = new PLXDecimal(this.LineItems[8]);
            this.Misc1 = new PLXString(this.LineItems[9]);
            this.Misc2 = new PLXString(this.LineItems[10]);
            this.Misc3 = new PLXString(this.LineItems[11]);
            this.OriginationDate = new PLXDate(this.LineItems[12]);
            this.PortfolioID = new PLXString(this.LineItems[13]);
            this.SellerName = new PLXString(this.LineItems[14]);
            this.TelecommPhone = new PLXString(this.LineItems[15]);
            this.PurchaseDate = new PLXDate(this.LineItems[16]);
            this.OriginalCreditor = new PLXString(this.LineItems[17]);
            this.OutOfStatuteDate = new PLXDate(this.LineItems[18]);
            this.SendCBRNegNotice = new PLXBool(this.LineItems[19]);
            this.SendOOSNotice = new PLXBool(this.LineItems[20]);
            this.LegacyProductInfo = new PLXString(this.LineItems[21]);
            this.AcctType = new PLXString(this.LineItems[22]);
            this.FirstDelinqDate = new PLXDate(this.LineItems[23]);
            this.COCAddress = new PLXString(this.LineItems[24]);
            this.COCAddress2 = new PLXString(this.LineItems[25]);
            this.COCCity = new PLXString(this.LineItems[26]);
            this.COCState = new PLXString(this.LineItems[27]);
            this.COCZip = new PLXString(this.LineItems[28]);
            this.ChargeOffCreditorName = new PLXString(this.LineItems[29]);
        }
    }

    public class AccountNumberUpdateRecord : Base.RecordBase
    {
        public AccountNumberUpdateRecord(string Record) : base(Record) { }
    }

    public class BorrowerAddressUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Borrower Record
        /// <para>Value will always be: 01 - Borrower</para>
        /// </summary>
        public PLXEnum<BorrowerType> BwrType { get; private set; }
        /// <summary>
        /// Borrower Address
        /// </summary>
        public PLXString Address { get; private set; }
        /// <summary>
        /// Borrower Address2
        /// </summary>
        public PLXString Address2 { get; private set; }
        /// <summary>
        /// Borrower City
        /// </summary>
        public PLXString City { get; private set; }
        /// <summary>
        /// Borrower State
        /// </summary>
        public PLXString State { get; private set; }
        /// <summary>
        /// Borrower Zip
        /// </summary>
        public PLXString Zip { get; private set; }
        #endregion

        public BorrowerAddressUpdateRecord(string Record)
            : base(Record)
        {
            this.BwrType = new PLXEnum<BorrowerType>(this.LineItems[3]);
            this.Address = new PLXString(this.LineItems[4]);
            this.Address2 = new PLXString(this.LineItems[5]);
            this.City = new PLXString(this.LineItems[6]);
            this.State = new PLXString(this.LineItems[7]);
            this.Zip = new PLXString(this.LineItems[8]);
        }
    }

    public class BankruptcyUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Bankruptcy Chapter
        /// <para>Values: 07, 11, 13</para>
        /// </summary>
        public PLXNumber Chapter { get; private set; }
        /// <summary>
        /// Banktuptcy Case Number
        /// </summary>
        public PLXString CaseNum { get; private set; }
        /// <summary>
        /// Bankruptcy Filed Date
        /// </summary>
        public PLXDate FileDate { get; private set; }
        /// <summary>
        /// Current Bankruptcy Status
        /// <para>02 - Filed</para>
        /// <para>15 - Dismissed</para>
        /// <para>20 - Discharged</para>
        /// <para>30 - Converted</para>
        /// </summary>
        public PLXEnum<BankruptcyStatus> BKStatus { get; private set; }
        /// <summary>
        /// Date of Current Banktuptcy Status
        /// </summary>
        public PLXDate StatusDate { get; private set; }
        #endregion

        public BankruptcyUpdateRecord(string Record)
            : base(Record)
        {
            this.Chapter = new PLXNumber(this.LineItems[3], 2, true);
            this.CaseNum = new PLXString(this.LineItems[4]);
            this.FileDate = new PLXDate(this.LineItems[5]);
            this.BKStatus = new PLXEnum<BankruptcyStatus>(this.LineItems[6]);
            this.StatusDate = new PLXDate(this.LineItems[7]);
        }
    }

    public class BorrowerPhoneUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Borrower Type
        /// <para>Value will always be: 01 - Borrower</para>
        /// </summary>
        public PLXEnum<BorrowerType> BwrType { get; private set; }
        /// <summary>
        /// Phone Type
        /// <para>01 - Home</para>
        /// <para>02 - Work</para>
        /// <para>03 - Other</para>
        /// <para>04 - Cell</para>
        /// </summary>
        public PLXEnum<BorrowerPhoneType> BrwrPhoneTypeID { get; private set; }
        /// <summary>
        /// Borrower Phone
        /// </summary>
        public PLXString Phone { get; private set; }
        #endregion

        public BorrowerPhoneUpdateRecord(string Record)
            : base(Record)
        {
            this.BwrType = new PLXEnum<BorrowerType>(this.LineItems[3]);
            this.BrwrPhoneTypeID = new PLXEnum<BorrowerPhoneType>(this.LineItems[4]);
            this.Phone = new PLXString(this.LineItems[5]);
        }
    }

    public class BorrowerAdditionalInfoUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Borrower Record
        /// <para>Value will always be:  01 - Borrower</para>
        /// </summary>
        public PLXEnum<BorrowerType> BwrType { get; private set; }
        /// <summary>
        /// Borrower Social Security Number
        /// </summary>
        public PLXString SSN { get; private set; }
        /// <summary>
        /// Borrower First Name
        /// </summary>
        public PLXString FirstName { get; private set; }
        /// <summary>
        /// Borrower Last Name
        /// </summary>
        public PLXString LastName { get; private set; }
        /// <summary>
        /// Borrower Date of Birth
        /// </summary>
        public PLXDate DateOfBirth { get; private set; }
        /// <summary>
        /// Borrower Primary Language
        /// <para>0 - English (default value)</para>
        /// <para>1 - Spanish</para>
        /// <para>3 - Other</para>
        /// </summary>
        public PLXEnum<LanguageCodes> LangCodeID { get; private set; }
        /// <summary>
        /// Borrower Employment Status
        /// <para>PT - Part Time</para>
        /// <para>FT - Full Time</para>
        /// <para>SE - Self Employed</para>
        /// <para>UE - Unemployed</para>
        /// <para>RET - Retired</para>
        /// <para>SSI - Social Security Income</para>
        /// </summary>
        public PLXEnum<Employment, EmploymentValues> Employment { get; private set; }
        /// <summary>
        /// Is the Borrower a Home Owner
        /// </summary>
        public PLXBool VerifiedHomeOwn { get; private set; }
        #endregion

        public BorrowerAdditionalInfoUpdateRecord(string Record)
            : base(Record)
        {
            this.BwrType = new PLXEnum<BorrowerType>(this.LineItems[3]);
            this.SSN = new PLXString(this.LineItems[4]);
            this.FirstName = new PLXString(this.LineItems[5]);
            this.LastName = new PLXString(this.LineItems[6]);
            this.DateOfBirth = new PLXDate(this.LineItems[7]);
            this.LangCodeID = new PLXEnum<LanguageCodes>(this.LineItems[8], 1);
            this.Employment = new PLXEnum<Employment, EmploymentValues>(this.LineItems[9]);
            this.VerifiedHomeOwn = new PLXBool(this.LineItems[10]);
        }
    }

    public class BorrowerDeceasedUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Borrower Record
        /// <para>Value will always be:  01 - Borrower</para>
        /// </summary>
        public PLXEnum<BorrowerType> BwrType { get; private set; }
        /// <summary>
        /// Date of Borrower's Death
        /// </summary>
        public PLXDate DateOfDeath { get; private set; }
        #endregion

        public BorrowerDeceasedUpdateRecord(string Record)
            : base(Record)
        {
            this.BwrType = new PLXEnum<BorrowerType>(this.LineItems[3]);
            this.DateOfDeath = new PLXDate(this.LineItems[4]);
        }
    }

    public class PlacementUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Commission Rate
        /// </summary>
        public PLXDecimal CommissionRate { get; private set; }
        /// <summary>
        /// Place Batch ID
        /// </summary>
        public PLXNumber PlaceBatchID { get; private set; }
        /// <summary>
        /// Trust Account ID
        /// </summary>
        public PLXString TrustAcctID { get; private set; }
        /// <summary>
        /// Putback Deadline
        /// </summary>
        public PLXDate PutbackDeadline { get; private set; }
        /// <summary>
        /// Settled in Full Rate
        /// </summary>
        public PLXDecimal SIF { get; private set; }
        #endregion

        public PlacementUpdateRecord(string Record)
            : base(Record)
        {
            this.CommissionRate = new PLXDecimal(this.LineItems[3], 4, 3);
            this.PlaceBatchID = new PLXNumber(this.LineItems[4]);
            this.TrustAcctID = new PLXString(this.LineItems[5]);
            this.PutbackDeadline = new PLXDate(this.LineItems[6]);
            this.SIF = new PLXDecimal(this.LineItems[7], 4, 3);
        }
    }

    public class AccountScoreUpdateRecord : Base.RecordBase
    {
        #region Properties
        /// <summary>
        /// Score Type
        /// <para>Acceptable Values: Prelegal Payer Score (Score values range from 1 to 30)</para>
        /// </summary>
        public PLXString ScoreType { get; private set; }
        public PLXString Score { get; private set; }
        public PLXDate ScoreDate { get; private set; }
        #endregion

        public AccountScoreUpdateRecord(string Record)
            : base(Record)
        {
            this.ScoreType = new PLXString(this.LineItems[3]);
            this.Score = new PLXString(this.LineItems[4]);
            this.ScoreDate = new PLXDate(this.LineItems[5]);
        }
    }



    public class RecallRecord : Base.RecordBase
    {
        public PLXString ResurgentAccountNo { get; private set; }
        public PLXString OriginalAccountNo { get; private set; }
        public PLXNumber DebtorNo { get; private set; }
        public PLXString FirstName { get; private set; }
        public PLXString LastName { get; private set; }
        public PLXString SSN { get; private set; }
        public PLXEnum<AccountStatus> AccountStatus { get; private set; }

        public RecallRecord(string Record) : base(Record)
        {
            this.ResurgentAccountNo = new DataTypes.PLXString(this.LineItems[1]);
            this.OriginalAccountNo = new DataTypes.PLXString(this.LineItems[2]);
            this.DebtorNo = new DataTypes.PLXNumber(this.LineItems[3]);
            this.FirstName = new DataTypes.PLXString(this.LineItems[4]);
            this.LastName = new DataTypes.PLXString(this.LineItems[5]);
            this.SSN = new DataTypes.PLXString(this.LineItems[6]);
            this.AccountStatus = new PLXEnum<AccountStatus>(this.LineItems[7]);
        }
    }

    public class BalanceItemizationUpdateRecord : Base.RecordBase
    {
        public PLXNumber PlaceBatchID { get; private set; }
        public PLXDictionary<ItemizationType> ItemizationType { get; private set; }
        public PLXString ItemizationCreditorName { get; private set; }
        public PLXDate ItemizationDate { get; private set; }
        public PLXDecimal ItemizationBalance { get; private set; }
        public PLXDecimal ItemizationPmtsCredits { get; private set; }
        public PLXDecimal ItemizationInterest { get; private set; }
        public PLXDecimal ItemizationFees { get; private set; }
        public PLXDecimal ItemizationCurrBalance { get; private set; }
        public PLXDecimal Interest { get; private set; }
        public PLXDecimal Cost { get; private set; }
        public PLXDecimal Fees { get; private set; }
        public PLXDecimal Payments { get; private set; }
        public PLXDecimal Credits { get; private set; }
        public PLXDecimal AdjustmentDecreaseBal { get; private set; }
        public PLXDecimal AdjustmentIncreaseBal { get; private set; }
        public PLXDate ItemizationLastUpdateDate { get; private set; }


        public BalanceItemizationUpdateRecord(string Record) : base(Record)
        {
            string[] S = Record.Split('\t');
            this.PlaceBatchID = new PLXNumber(LineItems[3]);
            this.ItemizationType = new PLXDictionary<ItemizationType>(LineItems[4], PLX.Dictionaries.ItemizationTypeDictionary);
            this.ItemizationCreditorName = new PLXString(LineItems[5]);
            this.ItemizationDate = new PLXDate(LineItems[6]);
            this.ItemizationBalance = new PLXDecimal(LineItems[7]);
            this.ItemizationPmtsCredits = new PLXDecimal(LineItems[8]);
            this.ItemizationInterest = new PLXDecimal(LineItems[9]);
            this.ItemizationFees = new PLXDecimal(LineItems[10]);
            this.ItemizationCurrBalance = new PLXDecimal(LineItems[11]);
            this.Interest = new PLXDecimal(LineItems[12]);
            this.Cost = new PLXDecimal(LineItems[13]);
            this.Fees = new PLXDecimal(LineItems[14]);
            this.Payments = new PLXDecimal(LineItems[15]);
            this.Credits = new PLXDecimal(LineItems[16]);
            this.AdjustmentDecreaseBal = new PLXDecimal(LineItems[17]);
            this.AdjustmentIncreaseBal = new PLXDecimal(LineItems[18]);
            this.ItemizationLastUpdateDate = new PLXDate(LineItems[19]);
        }
    }
}
