using RecordTypes.Trak.Base;
using RecordTypes.Trak.DataTypes;

namespace RecordTypes.Trak
{
    public class TrakPlacement : RecordTypeBase
    {
        public TrakString ClientPurchaseCost { get; private set; }
        public TrakString ClientPurchaseRate { get; private set; }
        public TrakString PlacementRate { get; private set; }
        public TrakString Instruction { get; private set; }
        public TrakString SCORE { get; private set; }
        public TrakString ClientFILE { get; private set; }
        public Debtor Debtor { get; private set; }
        public TrakDate OpenDate { get; private set; }
        public TrakDate ChargeOffDate { get; private set; }
        public TrakDecimal ChargeOffAmount { get; private set; }
        public TrakDecimal CurrentBalance { get; private set; }
        public TrakDate PurchaseDate { get; private set; }
        public TrakDecimal LastChargeAmount { get; private set; }
        public TrakDate InterestDate { get; private set; }
        public TrakDecimal LastCashAdvanceAmount { get; private set; }
        public TrakString OriginalCreditor { get; private set; }
        public TrakString CardType { get; private set; }
        public TrakMaskedNumber OriginalAccountNumber { get; private set; }
        public TrakDate LastPaymentDate_OriginalCreditor { get; private set; }
        public TrakDecimal LastPaymentAmount_OriginalCreditor { get; private set; }
        public TrakDate StatuteDate { get; private set; }
        public TrakDate LastPaymentDate_Client { get; private set; }
        public TrakDecimal LastPaymentAmount_Client { get; private set; }
        public TrakDecimal TotalPayments_Client { get; private set; }
        public TrakString PreChargeOffAccountNumber { get; private set; }
        public TrakDate Judgment_Date { get; private set; }
        public TrakDecimal Judgment_Amount { get; private set; }
        public TrakDecimal Judgment_Cost { get; private set; }
        public TrakDecimal Judgment_Total { get; private set; }
        public TrakString Jurisdiction { get; private set; }
        public TrakString DocketNo { get; private set; }
        public CoDebtor CoDebtor { get; private set; }
        public SkipTracer LexisNexis { get; private set; }
        public SkipTracer Accurint { get; private set; }
        public TrakString Merchant { get; private set; }
        public TrakString Seller { get; private set; }
        public TrakMaskedNumber ForwarderAccountNumber { get; private set; }
        public TrakString ForwardedAgency { get; private set; }
        public TrakString ForwardedAttorney { get; private set; }
        public TrakDate RepoDate { get; private set; }
        public TrakDate PlacedDate { get; private set; }
        public TrakString CarInfo { get; private set; }
        public TrakString ContractState { get; private set; }
        public TrakString CurrentCreditor { get; private set; }

        public TrakPlacement(string Record) : base(Record)
        {
            this.ClientPurchaseCost = new TrakString(base.RecordSegments, 0);
            this.ClientPurchaseRate = new TrakString(base.RecordSegments, 1);
            this.PlacementRate = new TrakString(base.RecordSegments, 2);
            this.Instruction = new TrakString(base.RecordSegments, 3);
            this.SCORE = new TrakString(base.RecordSegments, 4);
            this.ClientFILE = new TrakString(base.RecordSegments, 5);
            this.Debtor = new Debtor(base.RecordSegments, 6);
            this.OpenDate = new TrakDate(base.RecordSegments, 53);
            this.ChargeOffDate = new TrakDate(base.RecordSegments, 54);
            this.ChargeOffAmount = new TrakDecimal(base.RecordSegments, 55);
            this.CurrentBalance = new TrakDecimal(base.RecordSegments, 56);
            this.PurchaseDate = new TrakDate(base.RecordSegments, 57);
            this.LastChargeAmount = new TrakDecimal(base.RecordSegments, 58);
            this.InterestDate = new TrakDate(base.RecordSegments, 59);
            this.LastCashAdvanceAmount = new TrakDecimal(base.RecordSegments, 60);
            this.OriginalCreditor = new TrakString(base.RecordSegments, 61);
            this.CardType = new TrakString(base.RecordSegments, 62);
            this.OriginalAccountNumber = new TrakMaskedNumber(base.RecordSegments, 63);
            this.LastPaymentDate_OriginalCreditor = new TrakDate(base.RecordSegments, 64);
            this.LastPaymentAmount_OriginalCreditor = new TrakDecimal(base.RecordSegments, 65);
            this.StatuteDate = new TrakDate(base.RecordSegments, 66);
            this.LastPaymentDate_Client = new TrakDate(base.RecordSegments, 67);
            this.LastPaymentAmount_Client = new TrakDecimal(base.RecordSegments, 68);
            this.TotalPayments_Client = new TrakDecimal(base.RecordSegments, 69);
            this.PreChargeOffAccountNumber = new TrakString(base.RecordSegments, 70);
            this.Judgment_Date = new TrakDate(base.RecordSegments, 71);
            this.Judgment_Amount = new TrakDecimal(base.RecordSegments, 72);
            this.Judgment_Cost = new TrakDecimal(base.RecordSegments, 73);
            this.Judgment_Total = new TrakDecimal(base.RecordSegments, 74);
            this.Jurisdiction = new TrakString(base.RecordSegments, 75);
            this.DocketNo = new TrakString(base.RecordSegments, 76);
            this.CoDebtor = new CoDebtor(base.RecordSegments, 77);
            this.LexisNexis = new SkipTracer(base.RecordSegments, 123);
            this.Accurint = new SkipTracer(base.RecordSegments, 128);
            this.Merchant = new TrakString(base.RecordSegments, 133);
            this.Seller = new TrakString(base.RecordSegments, 134);
            this.ForwarderAccountNumber = new TrakMaskedNumber(base.RecordSegments, 135);
            this.ForwardedAgency = new TrakString(base.RecordSegments, 136);
            this.ForwardedAttorney = new TrakString(base.RecordSegments, 137);
            this.RepoDate = new TrakDate(base.RecordSegments, 138);
            this.PlacedDate = new TrakDate(base.RecordSegments, 139);
            this.CarInfo = new TrakString(base.RecordSegments, 140);
            this.ContractState = new TrakString(base.RecordSegments, 141);
            this.CurrentCreditor = new TrakString(base.RecordSegments, 142);
        }
    }

    public class Debtor
    {
        public TrakString LastName { get; private set; }
        public TrakString FirstName { get; private set; }
        public TrakString MiddleName { get; private set; }
        public TrakString Generation { get; private set; }
        public TrakString Alias { get; private set; }
        public TrakString Address { get; private set; }
        public TrakString City { get; private set; }
        public TrakString State { get; private set; }
        public TrakMaskedNumber Zip { get; private set; }
        public TrakDate DateOfBirth { get; private set; }
        public TrakString DriversLicense { get; private set; }
        public TrakString Phone { get; private set; }
        public TrakMaskedNumber SSN { get; private set; }
        public TrakString MilitaryInfo { get; private set; }
        public TrakMaskedNumber APR { get; private set; }
        public TrakString Employer { get; private set; }
        public TrakString Employer_Address { get; private set; }
        public TrakString Employer_City { get; private set; }
        public TrakString Employer_State { get; private set; }
        public TrakString Employer_Zip { get; private set; }
        public TrakString Employer_Phone { get; private set; }
        public TrakString SelfEmployed { get; private set; }
        public TrakDecimal WeeklySalary { get; private set; }
        public TrakString Bank { get; private set; }
        public TrakString Bank_Address { get; private set; }
        public TrakString Bank_City { get; private set; }
        public TrakString Bank_State { get; private set; }
        public TrakMaskedNumber Bank_Zip { get; private set; }
        public TrakString Bank_Phone { get; private set; }
        public TrakMaskedNumber Bank_Account { get; private set; }
        public TrakString Bank_Account_Type { get; private set; }
        public TrakString Bank_FundsVerified { get; private set; }
        public TrakString Attorney { get; private set; }
        public TrakString Attorney_Firm { get; private set; }
        public TrakString Attorney_Address { get; private set; }
        public TrakString Attorney_City { get; private set; }
        public TrakString Attorney_State { get; private set; }
        public TrakMaskedNumber Attorney_Zip { get; private set; }
        public TrakString Attorney_Phone { get; private set; }
        public TrakDate DateOfDeath { get; private set; }
        public TrakDate Bankruptcy_Filing_Date { get; private set; }
        public TrakString Bankruptcy_Filing_Number { get; private set; }
        public TrakString Bankruptcy_Filing_Chapter { get; private set; }
        public TrakString DebtDisputed { get; private set; }
        public TrakString Owns { get; private set; }
        public TrakString Rents { get; private set; }
        public TrakString ThirdPartyAuthorized { get; private set; }

        public Debtor(string[] Segments, int StartSegment)
        {
            this.LastName = new TrakString(Segments, StartSegment);
            this.FirstName = new TrakString(Segments, StartSegment + 1);
            this.MiddleName = new TrakString(Segments, StartSegment + 2);
            this.Generation = new TrakString(Segments, StartSegment + 3);
            this.Alias = new TrakString(Segments, StartSegment + 4);
            this.Address = new TrakString(Segments, StartSegment + 5);
            this.City = new TrakString(Segments, StartSegment + 6);
            this.State = new TrakString(Segments, StartSegment + 7);
            this.Zip = new TrakMaskedNumber(Segments, StartSegment + 8);
            this.DateOfBirth = new TrakDate(Segments, StartSegment + 9);
            this.DriversLicense = new TrakString(Segments, StartSegment + 10);
            this.Phone = new TrakString(Segments, StartSegment + 11);
            this.SSN = new TrakMaskedNumber(Segments, StartSegment + 12);
            this.MilitaryInfo = new TrakString(Segments, StartSegment + 13);
            this.APR = new TrakMaskedNumber(Segments, StartSegment + 14);
            this.Employer = new TrakString(Segments, StartSegment + 15);
            this.Employer_Address = new TrakString(Segments, StartSegment + 16);
            this.Employer_City = new TrakString(Segments, StartSegment + 17);
            this.Employer_State = new TrakString(Segments, StartSegment + 18);
            this.Employer_Zip = new TrakString(Segments, StartSegment + 19);
            this.Employer_Phone = new TrakString(Segments, StartSegment + 20);
            this.SelfEmployed = new TrakString(Segments, StartSegment + 21);
            this.WeeklySalary = new TrakDecimal(Segments, StartSegment + 22);
            this.Bank = new TrakString(Segments, StartSegment + 23);
            this.Bank_Address = new TrakString(Segments, StartSegment + 24);
            this.Bank_City = new TrakString(Segments, StartSegment + 25);
            this.Bank_State = new TrakString(Segments, StartSegment + 26);
            this.Bank_Zip = new TrakMaskedNumber(Segments, StartSegment + 27);
            this.Bank_Phone = new TrakString(Segments, StartSegment + 28);
            this.Bank_Account = new TrakMaskedNumber(Segments, StartSegment + 29);
            this.Bank_Account_Type = new TrakString(Segments, StartSegment + 30);
            this.Bank_FundsVerified = new TrakString(Segments, StartSegment + 31);
            this.Attorney = new TrakString(Segments, StartSegment + 32);
            this.Attorney_Firm = new TrakString(Segments, StartSegment + 33);
            this.Attorney_Address = new TrakString(Segments, StartSegment + 34);
            this.Attorney_City = new TrakString(Segments, StartSegment + 35);
            this.Attorney_State = new TrakString(Segments, StartSegment + 36);
            this.Attorney_Zip = new TrakMaskedNumber(Segments, StartSegment + 37);
            this.Attorney_Phone = new TrakString(Segments, StartSegment + 38);
            this.DateOfDeath = new TrakDate(Segments, StartSegment + 39);
            this.Bankruptcy_Filing_Date = new TrakDate(Segments, StartSegment + 40);
            this.Bankruptcy_Filing_Number = new TrakString(Segments, StartSegment + 41);
            this.Bankruptcy_Filing_Chapter = new TrakString(Segments, StartSegment + 42);
            this.DebtDisputed = new TrakString(Segments, StartSegment + 43);
            this.Owns = new TrakString(Segments, StartSegment + 44);
            this.Rents = new TrakString(Segments, StartSegment + 45);
            this.ThirdPartyAuthorized = new TrakString(Segments, StartSegment + 46);
        }
    }

    public class CoDebtor
    {
        public TrakString LastName { get; private set; }
        public TrakString FirstName { get; private set; }
        public TrakString MiddleName { get; private set; }
        public TrakString Generation { get; private set; }
        public TrakString Alias { get; private set; }
        public TrakString Address { get; private set; }
        public TrakString City { get; private set; }
        public TrakString State { get; private set; }
        public TrakMaskedNumber Zip { get; private set; }
        public TrakDate DateOfBirth { get; private set; }
        public TrakString DriversLicense { get; private set; }
        public TrakString Phone { get; private set; }
        public TrakMaskedNumber SSN { get; private set; }
        public TrakString MilitaryInfo { get; private set; }
        public TrakString Employer { get; private set; }
        public TrakString Employer_Address { get; private set; }
        public TrakString Employer_City { get; private set; }
        public TrakString Employer_State { get; private set; }
        public TrakMaskedNumber Employer_Zip { get; private set; }
        public TrakString Employer_Phone { get; private set; }
        public TrakString SelfEmployed { get; private set; }
        public TrakDecimal WeeklySalary { get; private set; }
        public TrakString Bank { get; private set; }
        public TrakString Bank_Address { get; private set; }
        public TrakString Bank_City { get; private set; }
        public TrakString Bank_State { get; private set; }
        public TrakMaskedNumber Bank_Zip { get; private set; }
        public TrakString Bank_Phone { get; private set; }
        public TrakMaskedNumber Bank_Account { get; private set; }
        public TrakString Bank_Account_Type { get; private set; }
        public TrakString Bank_FundsVerified { get; private set; }
        public TrakString Attorney { get; private set; }
        public TrakString Attorney_Firm { get; private set; }
        public TrakString Attorney_Address { get; private set; }
        public TrakString Attorney_City { get; private set; }
        public TrakString Attorney_State { get; private set; }
        public TrakMaskedNumber Attorney_Zip { get; private set; }
        public TrakString Attorney_Phone { get; private set; }
        public TrakDate DateOfDeath { get; private set; }
        public TrakDate Bankruptcy_Filing_Date { get; private set; }
        public TrakString Bankruptcy_Filing_Number { get; private set; }
        public TrakString Bankruptcy_Filing_Chapter { get; private set; }
        public TrakString DebtDisputed { get; private set; }
        public TrakString Owns { get; private set; }
        public TrakString Rents { get; private set; }
        public TrakString ThirdPartyAuthorized { get; private set; }

        public CoDebtor(string[] Segments, int StartSegment)
        {
            this.LastName = new TrakString(Segments, StartSegment);
            this.FirstName = new TrakString(Segments, StartSegment + 1);
            this.MiddleName = new TrakString(Segments, StartSegment + 2);
            this.Generation = new TrakString(Segments, StartSegment + 3);
            this.Alias = new TrakString(Segments, StartSegment + 4);
            this.Address = new TrakString(Segments, StartSegment + 5);
            this.City = new TrakString(Segments, StartSegment + 6);
            this.State = new TrakString(Segments, StartSegment + 7);
            this.Zip = new TrakMaskedNumber(Segments, StartSegment + 8);
            this.DateOfBirth = new TrakDate(Segments, StartSegment + 9);
            this.DriversLicense = new TrakString(Segments, StartSegment + 10);
            this.Phone = new TrakString(Segments, StartSegment + 11);
            this.SSN = new TrakMaskedNumber(Segments, StartSegment + 12);
            this.MilitaryInfo = new TrakString(Segments, StartSegment + 13);
            this.Employer = new TrakString(Segments, StartSegment + 14);
            this.Employer_Address = new TrakString(Segments, StartSegment + 15);
            this.Employer_City = new TrakString(Segments, StartSegment + 16);
            this.Employer_State = new TrakString(Segments, StartSegment + 17);
            this.Employer_Zip = new TrakMaskedNumber(Segments, StartSegment + 18);
            this.Employer_Phone = new TrakString(Segments, StartSegment + 19);
            this.SelfEmployed = new TrakString(Segments, StartSegment + 20);
            this.WeeklySalary = new TrakDecimal(Segments, StartSegment + 21);
            this.Bank = new TrakString(Segments, StartSegment + 22);
            this.Bank_Address = new TrakString(Segments, StartSegment + 23);
            this.Bank_City = new TrakString(Segments, StartSegment + 24);
            this.Bank_State = new TrakString(Segments, StartSegment + 25);
            this.Bank_Zip = new TrakMaskedNumber(Segments, StartSegment + 26);
            this.Bank_Phone = new TrakString(Segments, StartSegment + 27);
            this.Bank_Account = new TrakMaskedNumber(Segments, StartSegment + 28);
            this.Bank_Account_Type = new TrakString(Segments, StartSegment + 29);
            this.Bank_FundsVerified = new TrakString(Segments, StartSegment + 30);
            this.Attorney = new TrakString(Segments, StartSegment + 31);
            this.Attorney_Firm = new TrakString(Segments, StartSegment + 32);
            this.Attorney_Address = new TrakString(Segments, StartSegment + 33);
            this.Attorney_City = new TrakString(Segments, StartSegment + 34);
            this.Attorney_State = new TrakString(Segments, StartSegment + 35);
            this.Attorney_Zip = new TrakMaskedNumber(Segments, StartSegment + 36);
            this.Attorney_Phone = new TrakString(Segments, StartSegment + 37);
            this.DateOfDeath = new TrakDate(Segments, StartSegment + 38);
            this.Bankruptcy_Filing_Date = new TrakDate(Segments, StartSegment + 39);
            this.Bankruptcy_Filing_Number = new TrakString(Segments, StartSegment + 40);
            this.Bankruptcy_Filing_Chapter = new TrakString(Segments, StartSegment + 41);
            this.DebtDisputed = new TrakString(Segments, StartSegment + 42);
            this.Owns = new TrakString(Segments, StartSegment + 43);
            this.Rents = new TrakString(Segments, StartSegment + 44);
            this.ThirdPartyAuthorized = new TrakString(Segments, StartSegment + 45);
        }
    }

    public class SkipTracer
    {
        public TrakString Property { get; private set; }
        public TrakString Address { get; private set; }
        public TrakString Phone { get; private set; }
        public TrakString BankruptcyInfo { get; private set; }
        public TrakString DeceasedInfo { get; private set; }

        public SkipTracer(string[] Segments, int StartSegment)
        {
            this.Property = new TrakString(Segments, StartSegment);
            this.Address = new TrakString(Segments, StartSegment + 1);
            this.Phone = new TrakString(Segments, StartSegment + 2);
            this.BankruptcyInfo = new TrakString(Segments, StartSegment + 3);
            this.DeceasedInfo = new TrakString(Segments, StartSegment + 4);
        }
    }
}
