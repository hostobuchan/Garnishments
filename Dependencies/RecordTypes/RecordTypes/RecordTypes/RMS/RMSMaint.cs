using RecordTypes.RMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes.RMS.Maintenance
{
    public class MaintenanceInfo : RMS.Base.Record
    {
        #region Public Properties
        public RMSDateTime TransactionDate { get; private set; }
        public RMSString AccountNumber { get; private set; }
        public CitiTransEnum TransactionCode { get; private set; }
        #endregion

        public MaintenanceInfo(string Record)
        {
            this.TransactionDate = new RMSDateTime() { DataString = Record };
            this.AccountNumber = new RMSString(20) { DataString = Record.Substring(12) };
            this.TransactionCode = new CitiTransEnum(2) { DataString = Record.Substring(32) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}",
                this.TransactionDate,
                this.AccountNumber,
                this.TransactionCode);
        }

        #region EDI.EDIRecords.RecordType<Record>
        public override RMS.Base.Record PlacementRecord(List<RMS.Base.Record> Records, string AccountNumber)
        {
            return Records.OfType<DebtorRecord>().FirstOrDefault(el => el.AccountNumber.Value == AccountNumber);
        }
        public override void AddHeaders(List<RMS.Base.Record> BaseList, List<RMS.Base.Record> AddList)
        {
            throw new NotImplementedException();
        }
        public override RMS.Base.Record GetRecordType(string Record)
        {
            return FileReaders.RMSMaintenanceTypeIdentifier.GetMaintenanceRecord(Record);
        }
        #endregion
    }

    public class HeaderRecord : RMS.Base.Record
    {
        #region Public Properties
        public RMSDate TransactionDate { get; private set; }
        public RMSString Filler1 { get; private set; }
        public CitiTransEnum TransactionCode { get; private set; }
        public RMSZoneDecimal GrossBatchTotal { get; private set; }
        public RMSZoneDecimal NetBatchTransaction { get; private set; }
        public RMSNumber BatchTransactionCount { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString PartnerCode { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString Filler3 { get; private set; }
        #endregion

        internal HeaderRecord()
        {
            this.TransactionDate = new RMSDate();
            this.Filler1 = new RMSString(24);
            this.TransactionCode = new CitiTransEnum(2) { Value = Maintenance.TransactionCode.Header };
            this.GrossBatchTotal = new RMSZoneDecimal(10, 2);
            this.NetBatchTransaction = new RMSZoneDecimal(10, 2);
            this.BatchTransactionCount = new RMSNumber(5);
            this.RecovererCode = new RMSString(4);
            this.PartnerCode = new RMSString(4);
            this.Filler2 = new RMSString(28);
            this.ParentOrgCode = new RMSString(4);
            this.Filler3 = new RMSString(1);
        }
        public HeaderRecord(string Record)
        {
            this.TransactionDate = new RMSDate() { DataString = Record };
            this.Filler1 = new RMSString(24) { DataString = Record.Substring(8) };
            this.TransactionCode = new CitiTransEnum(2) { DataString = Record.Substring(32) };
            this.GrossBatchTotal = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(34) };
            this.NetBatchTransaction = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(44) };
            this.BatchTransactionCount = new RMSNumber(5) { DataString = Record.Substring(54) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(59) };
            this.PartnerCode = new RMSString(4) { DataString = Record.Substring(63) };
            this.Filler2 = new RMSString(28) { DataString = Record.Substring(67) };
            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(95) };
            this.Filler3 = new RMSString(1) { DataString = Record.Substring(99) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                this.TransactionDate,
                this.Filler1,
                this.TransactionCode,
                this.GrossBatchTotal,
                this.NetBatchTransaction,
                this.BatchTransactionCount,
                this.RecovererCode,
                this.PartnerCode,
                this.Filler2,
                this.ParentOrgCode,
                this.Filler3);
        }

        #region EDI.EDIRecords.RecordType<Record>
        public override RMS.Base.Record PlacementRecord(List<RMS.Base.Record> Records, string AccountNumber)
        {
            return Records.OfType<DebtorRecord>().FirstOrDefault(el => el.AccountNumber.Value == AccountNumber);
        }
        public override void AddHeaders(List<RMS.Base.Record> BaseList, List<RMS.Base.Record> AddList)
        {
            throw new NotImplementedException();
        }
        public override RMS.Base.Record GetRecordType(string Record)
        {
            return FileReaders.RMSMaintenanceTypeIdentifier.GetMaintenanceRecord(Record);
        }
        #endregion
    }

    public class CommentRecord : MaintenanceInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSString Comment { get; private set; }
        public RMSEnum<IOFlag, IOFlagValue> IOFlag { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString RecovererID { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString Filler3 { get; private set; }
        #endregion

        public CommentRecord(string Record) : base(Record)
        {
            this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(34) };
            this.Filler1 = new RMSString(4) { DataString = Record.Substring(36) };
            this.Comment = new RMSString(40) { DataString = Record.Substring(40) };
            this.IOFlag = new RMSEnum<IOFlag, IOFlagValue>(1) { DataString = Record.Substring(80) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(81) };
            this.RecovererID = new RMSString(8) { DataString = Record.Substring(85) };
            this.Filler2 = new RMSString(2) { DataString = Record.Substring(93) };
            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(95) };
            this.Filler3 = new RMSString(1) { DataString = Record.Substring(99) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                base.ToString(),
                this.SequenceNumber,
                this.Filler1,
                this.Comment,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.Filler2,
                this.ParentOrgCode,
                this.Filler3);
        }
    }

    public class FinanacialRecord : MaintenanceInfo
    {
        #region Public Properties
        public RMSZoneDecimal TransactionAmount { get; private set; }
        public RMSBool InterestFlag { get; private set; }
        public RMSBool SelfDirectedFlag { get; private set; }
        public RMSString TransactionDescription { get; private set; }
        public RMSZoneDecimal NetPaymentAmount { get; private set; }
        public RMSZoneDecimal CommissionPercent { get; private set; }
        public RMSEnum<IOFlag, IOFlagValue> IOFlag { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString RecovererID { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString Filler2 { get; private set; }
        #endregion

        public FinanacialRecord(string Record) : base(Record)
        {
            this.TransactionAmount = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(34) };
            this.InterestFlag = new RMSBool(1, "Y") { DataString = Record.Substring(44) };
            this.SelfDirectedFlag = new RMSBool(1, "Y") { DataString = Record.Substring(45) };
            this.TransactionDescription = new RMSString(20) { DataString = Record.Substring(46) };
            this.NetPaymentAmount = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(66) };
            this.CommissionPercent = new RMSZoneDecimal(4, 2) { DataString = Record.Substring(76) };
            this.IOFlag = new RMSEnum<IOFlag, IOFlagValue>(1) { DataString = Record.Substring(80) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(81) };
            this.RecovererID = new RMSString(8) { DataString = Record.Substring(85) };
            this.Filler1 = new RMSString(2) { DataString = Record.Substring(93) };
            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(95) };
            this.Filler2 = new RMSString(1) { DataString = Record.Substring(99) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                this.TransactionDate,
                this.AccountNumber,
                this.TransactionCode,
                this.TransactionAmount,
                this.InterestFlag,
                this.SelfDirectedFlag,
                this.TransactionDescription,
                this.NetPaymentAmount,
                this.CommissionPercent,
                this.IOFlag,
                this.RecovererCode,
                this.RecovererID,
                this.Filler1,
                this.ParentOrgCode,
                this.Filler2);
        }
    }

    public abstract class MaintenanceRecord : MaintenanceInfo
    {
        #region Public Properties
        public CitiMaintEnum<MaintenanceCode, MaintenanceCodeValue> FieldCode { get; private set; }
        public Transactions.Transaction FieldValue { get { return this.GetTransaction(); } }
        public RMSEnum<IOFlag, IOFlagValue> IOFlag { get; private set; }
        public RMSString RecovererCode { get; private set; }
        public RMSString RecovererID { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString Filler2 { get; private set; }
        #endregion

        public MaintenanceRecord(string Record) : base(Record)
        {
            this.FieldCode = new CitiMaintEnum<MaintenanceCode, MaintenanceCodeValue>(6) { DataString = Record.Substring(34) };
            this.IOFlag = new RMSEnum<IOFlag, IOFlagValue>(1) { DataString = Record.Substring(80) };
            this.RecovererCode = new RMSString(4) { DataString = Record.Substring(81) };
            this.RecovererID = new RMSString(8) { DataString = Record.Substring(85) };
            this.Filler1 = new RMSString(2) { DataString = Record.Substring(93) };
            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(95) };
            this.Filler2 = new RMSString(1) { DataString = Record.Substring(99) };
        }

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

        public abstract Transactions.Transaction GetTransaction();
    }

    public class MaintenanceRecord<T> : MaintenanceRecord where T : Transactions.Transaction
    {
        public new T FieldValue { get; private set; }

        public MaintenanceRecord(string Record) : base(Record)
        {
            FieldValue = (T)typeof(T).GetConstructor(new[] { typeof(string) }).Invoke(new[] { Record.Substring(40) });
        }

        public override Transactions.Transaction GetTransaction() { return this.FieldValue; }

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

    #region Modified Base Types
    public class CitiMaintEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value.Replace(@"$", "_")); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = value.ToString() == "Nothing" ? "".PadRight(base.DataLength, ' ') : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString().Replace("_", @"$"); } }

        public CitiMaintEnum(int DataLength) : base(DataLength) { }
    }
    public class CitiTransEnum : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new TransactionCode Value
        {
            get
            {
                try
                {
                    switch (base.Value)
                    {
                        case "HD":
                            return TransactionCode.Header;
                        case "MT":
                            return TransactionCode.Maintenance;
                        case "AR":
                            return TransactionCode.Recall;
                        default:
                            return (TransactionCode)Enum.ToObject(typeof(TransactionCode), Convert.ToInt32(base.Value));
                    }
                }
                catch
                {

                    return TransactionCode.Maintenance;
                }
            }
            set
            {
                switch (value)
                {
                    case TransactionCode.Header:
                        base.DataString = "HD";
                        break;
                    case TransactionCode.Maintenance:
                        base.DataString = "MT";
                        break;
                    case TransactionCode.Recall:
                        base.DataString = "AR";
                        break;
                    default:
                        base.DataString = Convert.ToInt32(value).ToString().PadLeft(base.DataLength, '0');
                        break;
                }
            }
        }

        public CitiTransEnum(int DataLength) : base(DataLength) { }

        public override string ToString()
        {
            return this.DataString;
        }
    }
    #endregion

    public enum TransactionCode
    {
        Header = 0,
        Maintenance = 1,
        Recall = 2,
        AccountAdjustment = 11,
        CycleAdjustment = 12,
        AccountAdjustment_Credit = 13,
        Fees = 14,
        MiscDebits = 15,
        BadCheck = 16,
        AssociatedExpenses = 18,
        LossInterest = 19,
        JudgmentAmount = 20,
        PostJudgmentInterest = 22,
        RemibursableLegalCostIncurred = 31,
        PaymentTransmission = 50,
        DirectPayment = 51,
        Echo_TransmittedPayment = 52,
        MiscCredits = 53,
        CCCS_Payments = 54,
        TransmittedNSF = 59,
        DirectNSF = 60,
        Echo_TransmittedNSF = 61,
        GeneralComment = 90,
        LegalComment = 94
    }
    public enum IOFlag
    {
        Internal,
        External,
        Upload
    }
    public enum IOFlagValue
    {
        I,
        O,
        X
    }
    public enum MaintenanceCode
    {
        Unknown,
        Co_Signer_First_Name,
        Co_Signer_Last_Name,
        Account_Number,
        Customer_Address_Line_1,
        Customer_Address_Line_2,
        AP_Account_Number,
        AP_Transaction_Date,
        Attorney_Code,
        Bankruptcy_User_Defined_Amt_1,
        Bankruptcy_User_Defined_Amt_2,
        Bankruptcy_User_Defined_Amt_3,
        Bankruptcy_User_Defined_A_1,
        Bankruptcy_User_Defined_A_2,
        Bankruptcy_User_Defined_A_3,
        Broken_Arrangement_Date,
        Collateral_Type,
        Proof_of_Claim_Number,
        Claim_Type,
        Bankruptcy_User_Defined_Date_1,
        Bankruptcy_User_Defined_Date_2,
        Bankruptcy_User_Defined_Date_3,
        Customer_Birthday,
        Bankruptcy_Chapter_Number,
        Ballot_Date,
        Billing_Option,
        Bankruptcy_Case_Number,
        Plan_Accepted_Date,
        Block_Payment_Flag,
        Bar_Date,
        Chapter_7_Discharge_Date,
        Credit_Bureau_Report_Flag,
        Comp_Call_Code,
        Contract_Date,
        Collateral_Flag,
        Contact_Frequency_Indicator,
        Contact_Frequency,
        Correspondence_Flag,
        Claim_Amount,
        Debtor_Corporate_Name,
        Customer_County,
        Commission,
        Charge_Off_Reason,
        Credit_Limit,
        Court_Code,
        Certificate_Of_Service_Date,
        Last_Cash_Date,
        Customer_City,
        Currency_Type,
        Court_Type,
        Defendant_Attorney_Code,
        Date_Assigned,
        Do_Not_Contact_Flag,
        Date_Charged_Off,
        Action_Code_Date,
        Dealer_Code,
        Dismissal_Date,
        Customer_Employer_Address,
        Employment_Status,
        Customer_Employer_Name,
        Execution_Date,
        Customer_First_Name,
        Financial_Recoverer,
        Date_Debt_Received,
        Garnishment_Date,
        Hot_Comments_Line_1,
        Hot_Comments_Line_2,
        Hot_Comments_Line_3,
        Customer_Home_Phone_Number,
        MASINT,
        Last_Payment_Amount,
        Initial_Delinquent_Date,
        Interest_Rate,
        Book,
        Judgment_Date,
        Excess_Judgment,
        Judgment_Type,
        Judgment_On,
        Page,
        Judgment_Recorded,
        Legal_User_Defined_A_N_10,
        Legal_User_Defined_A_N_1,
        Legal_User_Defined_A_N_2,
        Legal_User_Defined_A_N_3,
        Legal_User_Defined_A_N_4,
        Legal_User_Defined_A_N_5,
        Legal_User_Defined_A_N_6,
        Legal_User_Defined_A_N_7,
        Legal_User_Defined_A_N_8,
        Legal_User_Defined_A_N_9,
        Last_Billing_Date,
        Court_County,
        Last_Contact_Date,
        Case_Number,
        Court_Time,
        Legal_User_Defined_Date_1,
        Legal_User_Defined_Date_2,
        Legal_User_Defined_Date_3,
        Legal_User_Defined_Date_4,
        Legal_User_Defined_Date_5,
        Legal_User_Defined_Date_6,
        Court_Date,
        Last_Interest_Date,
        Lenders_Income_Rate,
        Legal_User_Defined_Number_1,
        Legal_User_Defined_Number_2,
        Legal_User_Defined_Number_3,
        Legal_User_Defined_Number_4,
        Legal_User_Defined_Number_5,
        Legal_User_Defined_Number_6,
        Legal_User_Defined_Number_7,
        Legal_User_Defined_Number_8,
        Customer_Last_Name,
        File_Location,
        Last_Payment_Amount2,
        Last_Payment_Date,
        Reason,
        Loan_Type,
        Memo_Balance_Only_Flag,
        MASMDU,
        Debtor_Month_Expense,
        Debtor_Month_Income,
        Memo_Date,
        Media_Queue_Date,
        Suit_Motion_Date,
        Next_Contact_Date,
        Next_Meeting_Date,
        Insufficient_Funds,
        Debtor_Other_Expense,
        Officer_Code,
        Debtor_Other_Income,
        Original_Loan_Amount,
        Old_Account_Number,
        Customer_Office_Phone,
        Customer_Own_Rent_Code,
        Proof_of_Claim_Amendment_Date,
        Payment_Apply_Code,
        Plan_Acceptance_Date,
        Plan_Approved_Date,
        Payment_Amount,
        Plan_Amendment_Number,
        Plan_Converted_Date,
        Priority_Claim_Flag,
        Previous_Case_Number,
        Plan_Convert_Number,
        Past_Due_Amount,
        Payment_Due_Date,
        Proof_of_Claim_Date,
        Payment_Frequency_Ind,
        Payment_Frequency,
        Post_Judgment_Arrangement_Date,
        Plan_Amount,
        Plan_Term,
        Debtor_Payment_Number,
        Pay_Outside_Claim,
        Plan_Received_Date,
        MASPRI,
        Not_Late_Until_Date,
        Payment_Schedule_Flag,
        Debtor_Payment_Term,
        Last_Purchase_Date,
        Reaffirmation_Date,
        Recoverer_Code,
        Status_Reason_Code,
        Recovery_Percentage,
        Satisfy_Date,
        Split_Claim_Amount,
        Secured_Claim_Amount,
        Unsecured_Percent,
        Credit_Recovery_Score,
        Suit_Costs,
        Suit_Filed_Date,
        Secured_Flag,
        Segment_ID,
        Suit_Interest,
        Stay_Lifted_Date,
        Split_Claim_Number,
        Split_Claim_Flag,
        Suit_Principal,
        Settlement_Status,
        Stat_Code,
        Customer_State,
        Stipulation_Date,
        Settlement_Code,
        Settlement_Duration_Period,
        Account_Status,
        Suit_Service_Date,
        MASTAB,
        Treatment_Tag,
        Customer_Tax_Identification_No,
        Trustee_Code,
        MASTDU,
        Tax_ID_Type,
        Debtor_Title,
        Customer_Type,
        Debtor_Time_Zone_Code,
        User_Defined_Field_Alpha_1,
        User_Defined_Field_Alpha_2,
        User_Defined_Field_Alpha_3,
        User_Defined_Field_Alpha_4,
        User_Defined_Field_Number_1,
        User_Defined_Field_Number_2,
        User_Defined_Field_Number_3,
        User_Defined_Field_Number_4,
        User_Defined_Field_Date_1,
        User_Defined_Field_Date_2,
        User_Defined_Field_Date_3,
        User_Defined_Field_Date_4,
        User_Defined_Field_Alpha_5,
        User_Defined_Field_Alpha_6,
        User_Defined_Field_Alpha_7,
        User_Defined_Field_Alpha_8,
        User_Defined_Field_Number_5,
        User_Defined_Field_Number_6,
        User_Defined_Field_Number_7,
        User_Defined_Field_Number_8,
        User_Defined_Field_Date_5,
        User_Defined_Field_Date_6,
        User_Defined_Field_Date_7,
        User_Defined_Field_Date_8,
        User_Defined_Field_1,
        User_Defined_Field_2,
        User_Defined_Field_3,
        User_Defined_Field_4,
        User_Defined_Field_5,
        User_Defined_Field_6,
        Variable_Rate_Code,
        Customer_Zip_Code
    }
    public enum MaintenanceCodeValue
    {
        Nothing,
        MAS2FN,
        MAS2LN,
        MASACT,
        MASAD1,
        MASAD2,
        MASAPA,
        MASAPD,
        MASATT,
        MASB_1,
        MASB_2,
        MASB_3,
        MASBA1,
        MASBA2,
        MASBA3,
        MASBAD,
        MASBCL,
        MASBCN,
        MASBCT,
        MASBD1,
        MASBD2,
        MASBD3,
        MASBDY,
        MASBKC,
        MASBLD,
        MASBLO,
        MASBNR,
        MASBPA,
        MASBPN,
        MASBRD,
        MASC7D,
        MASCBR,
        MASCCC,
        MASCDT,
        MASCFG,
        MASCFI,
        MASCFQ,
        MASCIF,
        MASCLM,
        MASCNM,
        MASCNT,
        MASCOM,
        MASCOR,
        MASCRD,
        MASCRT,
        MASCSD,
        MASCSH,
        MASCTY,
        MASCUR,
        MASCUT,
        MASDAC,
        MASDAS,
        MASDCF,
        MASDCO,
        MASDFD,
        MASDLR,
        MASDSD,
        MASEAD,
        MASEMP,
        MASENM,
        MASEXD,
        MASFNM,
        MASFRC,
        MASFRD,
        MASGDT,
        MASHC1,
        MASHC2,
        MASHC3,
        MASHPH,
        MASINT,
        MASIPA,
        MASIPD,
        MASIRC,
        MASJBK,
        MASJDT,
        MASJEX,
        MASJGT,
        MASJON,
        MASJPG,
        MASJRC,
        MASL10,
        MASLA1,
        MASLA2,
        MASLA3,
        MASLA4,
        MASLA5,
        MASLA6,
        MASLA7,
        MASLA8,
        MASLA9,
        MASLBD,
        MASLCC,
        MASLCD,
        MASLCN,
        MASLCT,
        MASLD1,
        MASLD2,
        MASLD3,
        MASLD4,
        MASLD5,
        MASLD6,
        MASLDT,
        MASLID,
        MASLIR,
        MASLN1,
        MASLN2,
        MASLN3,
        MASLN4,
        MASLN5,
        MASLN6,
        MASLN7,
        MASLN8,
        MASLNM,
        MASLOC,
        MASLPA,
        MASLPD,
        MASLRE,
        MASLTC,
        MASMBL,
        MASMDU,
        MASMEX,
        MASMIN,
        MASMMD,
        MASMQD,
        MASMTD,
        MASNCD,
        MASNMD,
        MASNSF,
        MASOEX,
        MASOFF,
        MASOIN,
        MASOLA,
        MASOLD,
        MASOPH,
        MASOWN,
        MASPAA,
        MASPAC,
        MASPAD,
        MASPAE,
        MASPAM,
        MASPAN,
        MASPCD,
        MASPCF,
        MASPCN,
        MASPCV,
        MASPDA,
        MASPDD,
        MASPFD,
        MASPFI,
        MASPFQ,
        MASPJR,
        MASPLM,
        MASPLT,
        MASPNO,
        MASPOT,
        MASPRD,
        MASPRI,
        MASPSD,
        MASPSF,
        MASPTM,
        MASPUR,
        MASRAD,
        MASRCV,
        MASRES,
        MASRPC,
        MASSAT,
        MASSCA,
        MASSCB,
        MASSCP,
        MASSCR,
        MASSCS,
        MASSDT,
        MASSFG,
        MASSID,
        MASSIN,
        MASSLD,
        MASSPC,
        MASSPF,
        MASSPN,
        MASSST,
        MASSTA,
        MASSTC,
        MASSTI,
        MASSTL,
        MASSTP,
        MASSTS,
        MASSVD,
        MASTAB,
        MASTAG,
        MASTAX,
        MASTCD,
        MASTDU,
        MASTIT,
        MASTTL,
        MASTYP,
        MASTZC,
        MASU01,
        MASU02,
        MASU03,
        MASU04,
        MASU05,
        MASU06,
        MASU07,
        MASU08,
        MASU09,
        MASU10,
        MASU11,
        MASU12,
        MASU13,
        MASU14,
        MASU15,
        MASU16,
        MASU17,
        MASU18,
        MASU19,
        MASU20,
        MASU21,
        MASU22,
        MASU23,
        MASU24,
        MASU25,
        MASU26,
        MASU27,
        MASU28,
        MASU29,
        MASU30,
        MASVRC,
        MASZIP
    }
}
