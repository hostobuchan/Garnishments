using RecordTypes.RMS.Base;
using RecordTypes.RMS.DataTypes;
using RecordTypes.RMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes.Citi
{
    public class HeaderRecord : RMS.HeaderRecord
    {
        public HeaderRecord() : base() { }
        public HeaderRecord(string Record) : base(Record) { }
    }

    public abstract class AccountInfo : RMS.Base.AccountInfo
    {
        public AccountInfo(string Record) : base(Record) { }

        #region EDI.EDIRecords.RecordType<Record>
        public override Record PlacementRecord(List<Record> Records, string AccountNumber)
        {
            return Records.OfType<DebtorRecord>().FirstOrDefault(el => el.AccountNumber.Value == AccountNumber);
        }
        public override void AddHeaders(List<Record> BaseList, List<Record> AddList)
        {
            throw new NotImplementedException();
        }
        public override Record GetRecordType(string Record)
        {
            return FileReaders.CitiRMSRecordTypeIdentifier.GetRecordType(Record);
        }
        #endregion
    }

    public abstract class AccountRecord : AccountInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public RMSString Title { get; private set; }
        public RMSEnum<CustomerTypes, CustomerTypeValues> CustomerType { get; private set; }
        public RMSString LastName { get; private set; }
        public RMSString FirstName { get; private set; }
        public RMSString Address1 { get; private set; }
        public RMSString Address2 { get; private set; }
        public RMSString City { get; private set; }
        /// <summary>
        /// For BANK only will be cell phone number field
        /// </summary>
        public RMSString County { get; private set; }
        public RMSString State { get; private set; }
        public RMSString Zip { get; private set; }
        public RMSString Phone_Home { get; private set; }
        public RMSString Phone_Work { get; private set; }
        public RMSDate DateOfBirth { get; private set; }
        public RMSString Employer { get; private set; }
        public RMSString Employer_Address { get; private set; }
        public RMSString CompanyName { get; private set; }
        #endregion

        public AccountRecord(string Record)
            : base(Record)
        {
            try
            {
                this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21, 2) };
                this.Title = new RMSString(4) { DataString = Record.Substring(23, 4) };
                this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1) { DataString = Record.Substring(27, 1) };
                this.CompanyName = new RMSString(40) { DataString = Record.Substring(28, 40) };
                this.LastName = new RMSString(25) { DataString = Record.Substring(28, 25) };
                this.FirstName = new RMSString(15) { DataString = Record.Substring(53, 15) };
                this.Address1 = new RMSString(25) { DataString = Record.Substring(68, 25) };
                this.Address2 = new RMSString(25) { DataString = Record.Substring(93, 25) };
                this.City = new RMSString(20) { DataString = Record.Substring(118, 20) };
                this.County = new RMSString(15) { DataString = Record.Substring(138, 15) };
                this.State = new RMSString(2) { DataString = Record.Substring(153, 2) };
                this.Zip = new RMSString(10) { DataString = Record.Substring(155, 10) };
                this.Phone_Home = new RMSString(16) { DataString = Record.Substring(165, 16) };
                this.Phone_Work = new RMSString(16) { DataString = Record.Substring(181, 16) };
                this.DateOfBirth = new RMSDate() { DataString = Record.Substring(197, 8) };
                this.Employer = new RMSString(40) { DataString = Record.Substring(205, 40) };
                this.Employer_Address = new RMSString(40) { DataString = Record.Substring(245, 40) };
            }
            catch
            {
                if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
                if (this.Title == null) this.Title = new RMSString(4);
                if (this.CustomerType == null) this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1);
                if (this.LastName == null) this.LastName = new RMSString(25);
                if (this.FirstName == null) this.FirstName = new RMSString(15);
                if (this.Address1 == null) this.Address1 = new RMSString(25);
                if (this.Address2 == null) this.Address2 = new RMSString(25);
                if (this.City == null) this.City = new RMSString(20);
                if (this.County == null) this.County = new RMSString(15);
                if (this.State == null) this.State = new RMSString(2);
                if (this.Zip == null) this.Zip = new RMSString(10);
                if (this.Phone_Home == null) this.Phone_Home = new RMSString(16);
                if (this.Phone_Work == null) this.Phone_Work = new RMSString(16);
                if (this.DateOfBirth == null) this.DateOfBirth = new RMSDate();
                if (this.Employer == null) this.Employer = new RMSString(40);
                if (this.Employer_Address == null) this.Employer_Address = new RMSString(40);
                if (this.CompanyName == null) this.CompanyName = new RMSString(40);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
                base.ToString(),
                this.SequenceNumber,
                this.Title,
                this.CustomerType,
                this.CustomerType.Value == CustomerTypes.Individual ? this.LastName.DataString + this.FirstName.DataString : this.CompanyName.DataString,
                this.Address1,
                this.Address2,
                this.City,
                this.County,
                this.State,
                this.Zip,
                this.Phone_Home,
                this.Phone_Work,
                this.DateOfBirth,
                this.Employer,
                this.Employer_Address);
        }
    }

    public class DebtorRecord : AccountRecord
    {
        #region Public Properties
        public RMSString LoanTypeCode { get; private set; }
        public RMSNumber LendingOfficerCode { get; private set; }
        public RMSString AttorneyCode { get; private set; }
        public RMSString DealerCode { get; private set; }
        public RMSString ChargeoffReason { get; private set; }
        public RMSString AccountStatus { get; private set; }
        public RMSDecimal InterestRate { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSDate DateReceived { get; private set; }
        public RMSDate ContractDate { get; private set; }
        public RMSDate ChargeoffDate { get; private set; }
        public RMSDate LastPaymentDate { get; private set; }
        public RMSZoneDecimal ChargeoffAmount { get; private set; }
        public RMSZoneDecimal AssociatedCosts { get; private set; }
        public RMSZoneDecimal AccruedInterest { get; private set; }
        public RMSZoneDecimal Balance { get; private set; }
        public RMSZoneDecimal NetPrincipal { get; private set; }
        public RMSZoneDecimal NetAssociatedCosts { get; private set; }
        public RMSZoneDecimal NetInterest { get; private set; }
        public RMSDate LastPurchaseDate { get; private set; }
        public RMSZoneDecimal LastPaymentAmount { get; private set; }
        public RMSString FileLocation { get; private set; }
        public RMSString StatusReason { get; private set; }
        public RMSString TTAG { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString OldAccountNumber { get; private set; }
        /// <summary>
        /// CFNA Specific
        /// </summary>
        public RMSString SecurityCode { get; private set; }
        /// <summary>
        /// For CFNA, State of original loan booking code
        /// </summary>
        public RMSString PrincipalCode { get; private set; }
        /// <summary>
        /// For CFNA, Branch of original loan booking code
        /// </summary>
        public RMSString AgentCode { get; private set; }
        /// <summary>
        /// For CFNA, Class Code For BANK only will be cell phone indicator field (Y/N)
        /// </summary>
        public RMSString OccupationCode { get; private set; }
        public RMSString Filler3 { get; private set; }
        public RMSEnum<LanguagePreference, LanguagePreferenceValues> LanguageIndicator_WrittenRequest { get; private set; }
        public RMSEnum<LanguagePreference, LanguagePreferenceValues> LanguageIndicator_VerbalRequest { get; private set; }
        public RMSNumber BillingCycle { get; private set; }
        public RMSZoneDecimal TotalFutureDue { get; private set; }
        public RMSZoneDecimal TotalCurrentDue { get; private set; }
        public RMSZoneDecimal PastDue_005 { get; private set; }
        public RMSZoneDecimal PastDue_030 { get; private set; }
        public RMSZoneDecimal PastDue_060 { get; private set; }
        public RMSZoneDecimal PastDue_090 { get; private set; }
        public RMSZoneDecimal PastDue_120 { get; private set; }
        public RMSZoneDecimal PastDue_150 { get; private set; }
        public RMSZoneDecimal PastDue_180 { get; private set; }
        public RMSDate LastCommentDate { get; private set; }
        public RMSString SecondLastName { get; private set; }
        public RMSString SecondFirstName { get; private set; }
        public RMSZoneDecimal MonthlyIncome { get; private set; }
        public RMSString Filler4 { get; private set; }
        public RMSEnum<OwnRent, OwnRentValues> OwnRentCode { get; private set; }
        public RMSNumber RecoveryScore { get; private set; }
        public RMSDate NextPaymentDate { get; private set; }
        public RMSDate LastInterestDate { get; private set; }
        public RMSDate LastContactDate { get; private set; }
        public RMSDecimal CommissionRate { get; private set; }
        public RMSBool Phone_Home_Flag { get; private set; }
        public RMSBool Phone_Work_Flag { get; private set; }
        public RMSBool Address_Flag { get; private set; }
        public RMSString CustomerID { get; private set; }
        public RMSString Filler5 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        public RMSString SSN { get { return this.CustomerID; } }
        public RMSString TaxID { get { return this.CustomerID; } }
        #endregion

        public DebtorRecord(string Record)
            : base(Record)
        {
            try
            {
                this.LoanTypeCode = new RMSString(4) { DataString = Record.Substring(285) };
                this.LendingOfficerCode = new RMSNumber(6) { DataString = Record.Substring(289) };
                this.AttorneyCode = new RMSString(4) { DataString = Record.Substring(295) };
                this.DealerCode = new RMSString(5) { DataString = Record.Substring(299) };
                this.ChargeoffReason = new RMSString(4) { DataString = Record.Substring(304) };
                this.AccountStatus = new RMSString(3) { DataString = Record.Substring(308) };
                this.InterestRate = new RMSDecimal(4, 2) { DataString = Record.Substring(311) };
                this.Filler1 = new RMSString(1) { DataString = Record.Substring(315) };
                this.DateReceived = new RMSDate() { DataString = Record.Substring(316) };
                this.ContractDate = new RMSDate() { DataString = Record.Substring(324) };
                this.ChargeoffDate = new RMSDate() { DataString = Record.Substring(332) };
                this.LastPaymentDate = new RMSDate() { DataString = Record.Substring(340) };
                this.ChargeoffAmount = new RMSZoneDecimal(11, 2) { DataString = Record.Substring(348) };
                this.AssociatedCosts = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(359) };
                this.AccruedInterest = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(369) };
                this.Balance = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(379) };
                this.NetPrincipal = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(389) };
                this.NetAssociatedCosts = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(398) };
                this.NetInterest = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(407) };
                this.LastPurchaseDate = new RMSDate() { DataString = Record.Substring(416) };
                this.LastPaymentAmount = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(424) };
                this.FileLocation = new RMSString(15) { DataString = Record.Substring(433) };
                this.StatusReason = new RMSString(3) { DataString = Record.Substring(448) };
                this.TTAG = new RMSString(3) { DataString = Record.Substring(451) };
                this.Filler2 = new RMSString(2) { DataString = Record.Substring(454) };
                this.OldAccountNumber = new RMSString(20) { DataString = Record.Substring(456) };
                this.SecurityCode = new RMSString(5) { DataString = Record.Substring(476) };
                this.PrincipalCode = new RMSString(4) { DataString = Record.Substring(481) };
                this.AgentCode = new RMSString(4) { DataString = Record.Substring(485) };
                this.OccupationCode = new RMSString(2) { DataString = Record.Substring(489) };
                this.Filler3 = new RMSString(1) { DataString = Record.Substring(491) };
                this.LanguageIndicator_WrittenRequest = new RMSEnum<LanguagePreference, LanguagePreferenceValues>(2) { DataString = Record.Substring(492) };
                this.LanguageIndicator_VerbalRequest = new RMSEnum<LanguagePreference, LanguagePreferenceValues>(2) { DataString = Record.Substring(494) };
                this.BillingCycle = new RMSNumber(2) { DataString = Record.Substring(496) };
                this.TotalFutureDue = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(298) };
                this.TotalCurrentDue = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(507) };
                this.PastDue_005 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(516) };
                this.PastDue_030 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(525) };
                this.PastDue_060 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(534) };
                this.PastDue_090 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(543) };
                this.PastDue_120 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(552) };
                this.PastDue_150 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(561) };
                this.PastDue_180 = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(570) };
                this.LastCommentDate = new RMSDate() { DataString = Record.Substring(579) };
                this.SecondLastName = new RMSString(25) { DataString = Record.Substring(587) };
                this.SecondFirstName = new RMSString(15) { DataString = Record.Substring(612) };
                this.MonthlyIncome = new RMSZoneDecimal(9, 2) { DataString = Record.Substring(627) };
                this.Filler4 = new RMSString(2) { DataString = Record.Substring(636) };
                this.OwnRentCode = new RMSEnum<OwnRent, OwnRentValues>(1) { DataString = Record.Substring(638) };
                this.RecoveryScore = new RMSNumber(4) { DataString = Record.Substring(639) };
                this.NextPaymentDate = new RMSDate() { DataString = Record.Substring(643) };
                this.LastInterestDate = new RMSDate() { DataString = Record.Substring(651) };
                this.LastContactDate = new RMSDate() { DataString = Record.Substring(659) };
                this.CommissionRate = new RMSDecimal(4, 2) { DataString = Record.Substring(667) };
                this.Phone_Home_Flag = new RMSBool(1, "Y", "N") { DataString = Record.Substring(671) };
                this.Phone_Work_Flag = new RMSBool(1, "Y", "N") { DataString = Record.Substring(672) };
                this.Address_Flag = new RMSBool(1, "Y", "N") { DataString = Record.Substring(673) };
                this.CustomerID = new RMSString(16) { DataString = Record.Substring(674) };
                this.Filler5 = new RMSString(1) { DataString = Record.Substring(690) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.LoanTypeCode == null) this.LoanTypeCode = new RMSString(4);
                if (this.LendingOfficerCode == null) this.LendingOfficerCode = new RMSNumber(6);
                if (this.AttorneyCode == null) this.AttorneyCode = new RMSString(4);
                if (this.DealerCode == null) this.DealerCode = new RMSString(5);
                if (this.ChargeoffReason == null) this.ChargeoffReason = new RMSString(4);
                if (this.AccountStatus == null) this.AccountStatus = new RMSString(3);
                if (this.InterestRate == null) this.InterestRate = new RMSDecimal(4, 2);
                if (this.Filler1 == null) this.Filler1 = new RMSString(1);
                if (this.DateReceived == null) this.DateReceived = new RMSDate();
                if (this.ContractDate == null) this.ContractDate = new RMSDate();
                if (this.ChargeoffDate == null) this.ChargeoffDate = new RMSDate();
                if (this.LastPaymentDate == null) this.LastPaymentDate = new RMSDate();
                if (this.ChargeoffAmount == null) this.ChargeoffAmount = new RMSZoneDecimal(11, 2);
                if (this.AssociatedCosts == null) this.AssociatedCosts = new RMSZoneDecimal(10, 2);
                if (this.AccruedInterest == null) this.AccruedInterest = new RMSZoneDecimal(10, 2);
                if (this.Balance == null) this.Balance = new RMSZoneDecimal(10, 2);
                if (this.NetPrincipal == null) this.NetPrincipal = new RMSZoneDecimal(9, 2);
                if (this.NetAssociatedCosts == null) this.NetAssociatedCosts = new RMSZoneDecimal(9, 2);
                if (this.NetInterest == null) this.NetInterest = new RMSZoneDecimal(9, 2);
                if (this.LastPurchaseDate == null) this.LastPurchaseDate = new RMSDate();
                if (this.LastPaymentAmount == null) this.LastPaymentAmount = new RMSZoneDecimal(9, 2);
                if (this.FileLocation == null) this.FileLocation = new RMSString(15);
                if (this.StatusReason == null) this.StatusReason = new RMSString(3);
                if (this.TTAG == null) this.TTAG = new RMSString(3);
                if (this.Filler2 == null) this.Filler1 = new RMSString(1);
                if (this.OldAccountNumber == null) this.OldAccountNumber = new RMSString(20);
                if (this.SecurityCode == null) this.SecurityCode = new RMSString(5);
                if (this.PrincipalCode == null) this.PrincipalCode = new RMSString(4);
                if (this.AgentCode == null) this.AgentCode = new RMSString(4);
                if (this.OccupationCode == null) this.OccupationCode = new RMSString(2);
                if (this.Filler3 == null) this.Filler2 = new RMSString(1);
                if (this.LanguageIndicator_WrittenRequest == null) this.LanguageIndicator_WrittenRequest = new RMSEnum<LanguagePreference, LanguagePreferenceValues>(2);
                if (this.LanguageIndicator_WrittenRequest == null) this.LanguageIndicator_WrittenRequest = new RMSEnum<LanguagePreference, LanguagePreferenceValues>(2);
                if (this.BillingCycle == null) this.BillingCycle = new RMSNumber(2);
                if (this.TotalFutureDue == null) this.TotalFutureDue = new RMSZoneDecimal(9, 2);
                if (this.TotalCurrentDue == null) this.TotalCurrentDue = new RMSZoneDecimal(9, 2);
                if (this.PastDue_005 == null) this.PastDue_005 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_030 == null) this.PastDue_030 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_060 == null) this.PastDue_060 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_090 == null) this.PastDue_090 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_120 == null) this.PastDue_120 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_150 == null) this.PastDue_150 = new RMSZoneDecimal(9, 2);
                if (this.PastDue_180 == null) this.PastDue_180 = new RMSZoneDecimal(9, 2);
                if (this.LastCommentDate == null) this.LastCommentDate = new RMSDate();
                if (this.SecondLastName == null) this.SecondLastName = new RMSString(25);
                if (this.SecondFirstName == null) this.SecondFirstName = new RMSString(15);
                if (this.MonthlyIncome == null) this.MonthlyIncome = new RMSZoneDecimal(9, 2);
                if (this.Filler4 == null) this.Filler3 = new RMSString(2);
                if (this.OwnRentCode == null) this.OwnRentCode = new RMSEnum<OwnRent, OwnRentValues>(1);
                if (this.RecoveryScore == null) this.RecoveryScore = new RMSNumber(4);
                if (this.NextPaymentDate == null) this.NextPaymentDate = new RMSDate();
                if (this.LastInterestDate == null) this.LastInterestDate = new RMSDate();
                if (this.LastContactDate == null) this.LastContactDate = new RMSDate();
                if (this.CommissionRate == null) this.CommissionRate = new RMSDecimal(4, 2);
                if (this.Phone_Home_Flag == null) this.Phone_Home_Flag = new RMSBool(1, "Y", "N");
                if (this.Phone_Work_Flag == null) this.Phone_Work_Flag = new RMSBool(1, "Y", "N");
                if (this.Address_Flag == null) this.Address_Flag = new RMSBool(1, "Y", "N");
                if (this.CustomerID == null) this.CustomerID = new RMSString(16);
                if (this.Filler5 == null) this.Filler5 = new RMSString(1);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}",
                base.ToString(),
                this.LoanTypeCode,
                this.LendingOfficerCode,
                this.AttorneyCode,
                this.DealerCode,
                this.ChargeoffReason,
                this.AccountStatus,
                this.InterestRate,
                this.Filler1,
                this.DateReceived,
                this.ContractDate,
                this.ChargeoffDate,
                this.LastPaymentDate,
                this.ChargeoffAmount,
                this.AssociatedCosts,
                this.AccruedInterest,
                this.Balance,
                this.NetPrincipal,
                this.NetAssociatedCosts,
                this.NetInterest,
                this.LastPurchaseDate,
                this.LastPaymentAmount,
                this.FileLocation,
                this.StatusReason,
                this.TTAG,
                this.Filler2,
                this.OldAccountNumber,
                this.SecurityCode,
                this.PrincipalCode,
                this.AgentCode,
                this.OccupationCode,
                this.Filler3,
                this.LanguageIndicator_WrittenRequest,
                this.LanguageIndicator_VerbalRequest,
                this.BillingCycle,
                this.TotalFutureDue,
                this.TotalCurrentDue,
                this.PastDue_005,
                this.PastDue_030,
                this.PastDue_060,
                this.PastDue_090,
                this.PastDue_120,
                this.PastDue_150,
                this.PastDue_180,
                this.LastCommentDate,
                this.SecondLastName,
                this.SecondFirstName,
                this.MonthlyIncome,
                this.Filler4,
                this.OwnRentCode,
                this.RecoveryScore,
                this.NextPaymentDate,
                this.LastInterestDate,
                this.LastContactDate,
                this.CommissionRate,
                this.Phone_Home_Flag,
                this.Phone_Work_Flag,
                this.Address_Flag,
                this.CustomerID,
                this.Filler5,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }
    }

    public class CoMakerRecord : AccountRecord
    {
        #region Public Properties
        /// <summary>
        /// 01 - 99 = Comaker Record
        /// <para>**For Sequence Number 07 and 08 only - see matrix</para>
        /// </summary>
        public new RMSNumber SequenceNumber { get { return base.SequenceNumber; } }
        public RMSString Filler1 { get; private set; }
        public RMSString CustomerID { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        public RMSString SSN { get { return this.CustomerID; } }
        public RMSString TaxID { get { return this.CustomerID; } }
        #endregion

        public CoMakerRecord(string Record)
            : base(Record)
        {
            try
            {
                this.Filler1 = new RMSString(389) { DataString = Record.Substring(285) };
                this.CustomerID = new RMSString(16) { DataString = Record.Substring(674) };
                this.Filler2 = new RMSString(1) { DataString = Record.Substring(690) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.Filler1 == null) this.Filler1 = new RMSString(389);
                if (this.CustomerID == null) this.CustomerID = new RMSString(16);
                if (this.Filler2 == null) this.Filler2 = new RMSString(1);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
            FixName();
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                base.ToString(),
                this.Filler1,
                this.CustomerID,
                this.Filler2,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }

        private void FixName()
        {
            if (this.CustomerType.Value == CustomerTypes.Unknown)
            {
                if (this.LastName.Value.Contains("  "))
                {
                    string First = "";
                    string Last = "";
                    int index = this.CompanyName.Value.IndexOf("  ");
                    First = this.CompanyName.Value.Substring(0, index);
                    Last = this.CompanyName.Value.Substring(index).Trim();
                    this.LastName.Value = Last;
                    this.FirstName.Value = First;
                    this.CustomerType.Value = CustomerTypes.Individual;
                }
            }
        }
    }

    public class CoMakerRecord07 : AccountInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public RMSString Title { get; private set; }
        public RMSEnum<CustomerTypes, CustomerTypeValues> CustomerType { get; private set; }
        public RMSString BankRoutingNumber { get; private set; }
        public RMSString CompanyName { get { return this.BankRoutingNumber; } }
        public RMSString LastName { get; private set; }
        public RMSString FirstName { get; private set; }
        public RMSString Address1 { get; private set; }
        public RMSString Address2 { get; private set; }
        public RMSString City { get; private set; }
        public RMSString County { get; private set; }
        public RMSString State { get; private set; }
        public RMSString Zip { get; private set; }
        public RMSString Phone_Home { get; private set; }
        public RMSString Phone_Work { get; private set; }
        public RMSString EmployerVerificationCode { get; private set; }
        public RMSString Employer { get; private set; }
        public RMSString Employer_Address { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSBool OwnsHomeIndicator { get; private set; }
        public RMSString CustomerID { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        public RMSString SSN { get { return this.CustomerID; } }
        public RMSString TaxID { get { return this.CustomerID; } }
        #endregion

        public CoMakerRecord07(string Record)
            : base(Record)
        {
            try
            {
                this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21, 2) };
                this.Title = new RMSString(4) { DataString = Record.Substring(23, 4) };
                this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1) { DataString = Record.Substring(27, 1) };
                this.BankRoutingNumber = new RMSString(40) { DataString = Record.Substring(28, 40) };
                this.LastName = new RMSString(25) { DataString = Record.Substring(28, 25) };
                this.FirstName = new RMSString(15) { DataString = Record.Substring(53, 15) };
                this.Address1 = new RMSString(25) { DataString = Record.Substring(68, 25) };
                this.Address2 = new RMSString(25) { DataString = Record.Substring(93, 25) };
                this.City = new RMSString(20) { DataString = Record.Substring(118, 20) };
                this.County = new RMSString(15) { DataString = Record.Substring(138, 15) };
                this.State = new RMSString(2) { DataString = Record.Substring(153, 2) };
                this.Zip = new RMSString(10) { DataString = Record.Substring(155, 10) };
                this.Phone_Home = new RMSString(16) { DataString = Record.Substring(165, 16) };
                this.Phone_Work = new RMSString(16) { DataString = Record.Substring(181, 16) };
                this.EmployerVerificationCode = new RMSString(8) { DataString = Record.Substring(197, 8) };
                this.Employer = new RMSString(40) { DataString = Record.Substring(205, 40) };
                this.Employer_Address = new RMSString(40) { DataString = Record.Substring(245, 40) };
                this.Filler1 = new RMSString(388) { DataString = Record.Substring(285) };
                this.OwnsHomeIndicator = new RMSBool(1, "Y") { DataString = Record.Substring(673) };
                this.CustomerID = new RMSString(16) { DataString = Record.Substring(674) };
                this.Filler2 = new RMSString(1) { DataString = Record.Substring(690) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
                if (this.Title == null) this.Title = new RMSString(4);
                if (this.CustomerType == null) this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1);
                if (this.BankRoutingNumber == null) this.BankRoutingNumber = new RMSString(40);
                if (this.LastName == null) this.LastName = new RMSString(25);
                if (this.FirstName == null) this.FirstName = new RMSString(15);
                if (this.Address1 == null) this.Address1 = new RMSString(25);
                if (this.Address2 == null) this.Address2 = new RMSString(25);
                if (this.City == null) this.City = new RMSString(20);
                if (this.County == null) this.County = new RMSString(15);
                if (this.State == null) this.State = new RMSString(2);
                if (this.Zip == null) this.Zip = new RMSString(10);
                if (this.Phone_Home == null) this.Phone_Home = new RMSString(16);
                if (this.Phone_Work == null) this.Phone_Work = new RMSString(16);
                if (this.EmployerVerificationCode == null) this.EmployerVerificationCode = new RMSString(8);
                if (this.Employer == null) this.Employer = new RMSString(40);
                if (this.Employer_Address == null) this.Employer_Address = new RMSString(40);
                if (this.Filler1 == null) this.Filler1 = new RMSString(388);
                if (this.OwnsHomeIndicator == null) this.OwnsHomeIndicator = new RMSBool(1, "Y");
                if (this.CustomerID == null) this.CustomerID = new RMSString(16);
                if (this.Filler2 == null) this.Filler2 = new RMSString(1);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}",
                base.ToString(),
                this.SequenceNumber,
                this.Title,
                this.CustomerType,
                this.CustomerType.Value == CustomerTypes.Individual ? this.LastName.DataString + this.FirstName.DataString : this.CompanyName.DataString,
                this.Address1,
                this.Address2,
                this.City,
                this.County,
                this.State,
                this.Zip,
                this.Phone_Home,
                this.Phone_Work,
                this.EmployerVerificationCode,
                this.Employer,
                this.Employer_Address,
                this.Filler1,
                this.OwnsHomeIndicator,
                this.CustomerID,
                this.Filler2,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }
    }

    public class CoMakerRecord08 : AccountRecord
    {
        #region Public Properties
        public RMSString Filler1 { get; private set; }
        public RMSString CustomerID { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        public RMSString SSN { get { return this.CustomerID; } }
        public RMSString TaxID { get { return this.CustomerID; } }
        #endregion

        public CoMakerRecord08(string Record)
            : base(Record)
        {
            try
            {
                this.Filler1 = new RMSString(389) { DataString = Record.Substring(285) };
                this.CustomerID = new RMSString(16) { DataString = Record.Substring(674) };
                this.Filler2 = new RMSString(1) { DataString = Record.Substring(690) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.Filler1 == null) this.Filler1 = new RMSString(389);
                if (this.CustomerID == null) this.CustomerID = new RMSString(16);
                if (this.Filler2 == null) this.Filler2 = new RMSString(1);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                base.ToString(),
                this.Filler1,
                this.CustomerID,
                this.Filler2,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }
    }

    public class NotesRecord : AccountInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public NoteEntry Entry01 { get; private set; }
        public NoteEntry Entry02 { get; private set; }
        public NoteEntry Entry03 { get; private set; }
        public NoteEntry Entry04 { get; private set; }
        public NoteEntry Entry05 { get; private set; }
        public NoteEntry Entry06 { get; private set; }
        public NoteEntry Entry07 { get; private set; }
        public NoteEntry Entry08 { get; private set; }
        public NoteEntry Entry09 { get; private set; }
        public NoteEntry Entry10 { get; private set; }
        public NoteEntry Entry11 { get; private set; }
        public NoteEntry Entry12 { get; private set; }
        public NoteEntry Entry13 { get; private set; }
        public RMSString Filler { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        #endregion

        public NotesRecord(string Record)
            : base(Record)
        {
            try
            {
                this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
                this.Entry01 = new NoteEntry(Record.Substring(23));
                this.Entry02 = new NoteEntry(Record.Substring(71));
                this.Entry03 = new NoteEntry(Record.Substring(119));
                this.Entry04 = new NoteEntry(Record.Substring(167));
                this.Entry05 = new NoteEntry(Record.Substring(215));
                this.Entry06 = new NoteEntry(Record.Substring(263));
                this.Entry07 = new NoteEntry(Record.Substring(311));
                this.Entry08 = new NoteEntry(Record.Substring(359));
                this.Entry09 = new NoteEntry(Record.Substring(407));
                this.Entry10 = new NoteEntry(Record.Substring(455));
                this.Entry11 = new NoteEntry(Record.Substring(503));
                this.Entry12 = new NoteEntry(Record.Substring(551));
                this.Entry13 = new NoteEntry(Record.Substring(599));
                this.Filler = new RMSString(44) { DataString = Record.Substring(647) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
                if (this.Entry01 == null) this.Entry01 = new NoteEntry("");
                if (this.Entry02 == null) this.Entry02 = new NoteEntry("");
                if (this.Entry03 == null) this.Entry03 = new NoteEntry("");
                if (this.Entry04 == null) this.Entry04 = new NoteEntry("");
                if (this.Entry05 == null) this.Entry05 = new NoteEntry("");
                if (this.Entry06 == null) this.Entry06 = new NoteEntry("");
                if (this.Entry07 == null) this.Entry07 = new NoteEntry("");
                if (this.Entry08 == null) this.Entry08 = new NoteEntry("");
                if (this.Entry09 == null) this.Entry09 = new NoteEntry("");
                if (this.Entry10 == null) this.Entry10 = new NoteEntry("");
                if (this.Entry11 == null) this.Entry11 = new NoteEntry("");
                if (this.Entry12 == null) this.Entry12 = new NoteEntry("");
                if (this.Entry13 == null) this.Entry13 = new NoteEntry("");
                if (this.Filler == null) this.Filler = new RMSString(44);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}",
                base.ToString(),
                this.SequenceNumber,
                this.Entry01,
                this.Entry02,
                this.Entry03,
                this.Entry04,
                this.Entry05,
                this.Entry06,
                this.Entry07,
                this.Entry08,
                this.Entry09,
                this.Entry10,
                this.Entry11,
                this.Entry12,
                this.Entry13,
                this.Filler,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }

        public class NoteEntry
        {
            public RMSDate TransactionDate { get; private set; }
            public RMSString NoteLine { get; private set; }

            public NoteEntry(string Entry)
            {
                try
                {
                    this.TransactionDate = new RMSDate() { DataString = Entry };
                    this.NoteLine = new RMSString(40) { DataString = Entry.Substring(8) };
                }
                catch
                {
                    if (this.TransactionDate == null) this.TransactionDate = new RMSDate();
                    if (this.NoteLine == null) this.NoteLine = new RMSString(40);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}", this.TransactionDate, this.NoteLine);
            }
        }
    }

    //public class CollateralRecord : AccountInfo
    //{
    //    #region Public Properties
    //    public RMSNumber SequenceNumber { get; private set; }
    //    public Collateral Collateral1 { get; private set; }
    //    public Collateral Collateral2 { get; private set; }
    //    public Collateral Collateral3 { get; private set; }
    //    public Collateral Collateral4 { get; private set; }
    //    public Collateral Collateral5 { get; private set; }
    //    public RMSString Filler { get; private set; }
    //    public RMSString ParentOrgCode { get; private set; }
    //    public RMSString AgencyCode { get; private set; }
    //    public string FormatCode { get { return "B"; } }
    //    #endregion

    //    public CollateralRecord(string Record)
    //        : base(Record)
    //    {
    //        try
    //        {
    //            this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
    //            this.Collateral1 = new Collateral(Record.Substring(23));
    //            this.Collateral2 = new Collateral(Record.Substring(100));
    //            this.Collateral3 = new Collateral(Record.Substring(126));
    //            this.Collateral4 = new Collateral(Record.Substring(254));
    //            this.Collateral5 = new Collateral(Record.Substring(331));
    //            this.Filler = new RMSString(283) { DataString = Record.Substring(408) };
    //            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
    //            this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
    //        }
    //        catch
    //        {
    //            if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
    //            if (this.Collateral1 == null) this.Collateral1 = new Collateral("");
    //            if (this.Collateral2 == null) this.Collateral2 = new Collateral("");
    //            if (this.Collateral3 == null) this.Collateral3 = new Collateral("");
    //            if (this.Collateral4 == null) this.Collateral4 = new Collateral("");
    //            if (this.Collateral5 == null) this.Collateral5 = new Collateral("");
    //            if (this.Filler == null) this.Filler = new RMSString(283);
    //            if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
    //            if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
    //            base.ToString(),
    //            this.SequenceNumber,
    //            this.Collateral1,
    //            this.Collateral2,
    //            this.Collateral3,
    //            this.Collateral4,
    //            this.Collateral5,
    //            this.Filler,
    //            this.ParentOrgCode,
    //            this.AgencyCode,
    //            FormatCode);
    //    }

    //    public class Collateral
    //    {
    //        #region Public Properties
    //        public RMSString CollateralID { get; private set; }
    //        public RMSNumber CollateralYear { get; private set; }
    //        public RMSString CollateralMake { get; private set; }
    //        public RMSDate RepossessionDate { get; private set; }
    //        public RMSDate SaleDate { get; private set; }
    //        public RMSDecimal SalePrice { get; private set; }
    //        #endregion

    //        public Collateral(string Coll)
    //        {
    //            try
    //            {
    //                this.CollateralID = new RMSString(20) { DataString = Coll };
    //                this.CollateralYear = new RMSNumber(2) { DataString = Coll.Substring(20) };
    //                this.CollateralMake = new RMSString(28) { DataString = Coll.Substring(22) };
    //                this.RepossessionDate = new RMSDate() { DataString = Coll.Substring(50) };
    //                this.SaleDate = new RMSDate() { DataString = Coll.Substring(58) };
    //                this.SalePrice = new RMSDecimal(11, 2) { DataString = Coll.Substring(66) };
    //            }
    //            catch
    //            {
    //                if (this.CollateralID == null) this.CollateralID = new RMSString(20);
    //                if (this.CollateralYear == null) this.CollateralYear = new RMSNumber(2);
    //                if (this.CollateralMake == null) this.CollateralMake = new RMSString(28);
    //                if (this.RepossessionDate == null) this.RepossessionDate = new RMSDate();
    //                if (this.SaleDate == null) this.SaleDate = new RMSDate();
    //                if (this.SalePrice == null) this.SalePrice = new RMSDecimal(11, 2);
    //            }
    //        }

    //        public override string ToString()
    //        {
    //            return string.Format("{0}{1}{2}{3}{4}{5}",
    //                this.CollateralID,
    //                this.CollateralYear,
    //                this.CollateralMake,
    //                this.RepossessionDate,
    //                this.SaleDate,
    //                this.SalePrice);
    //        }
    //    }
    //}

    public class LegalRecord : AccountInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public RMSString Suit_Reason { get; private set; }
        public RMSDate Suit_FiledDate { get; private set; }
        public RMSString Suit_CaseNumber { get; private set; }
        public RMSString CourtName { get; private set; }
        public RMSDate Judgment_RecordedDate { get; private set; }
        public RMSString Judgment_Book { get; private set; }
        public RMSString Judgment_Page { get; private set; }
        public RMSDate Judgment_ExpirationDate { get; private set; }
        public RMSString CreditorMeetingLocation { get; private set; }
        public RMSDate CreditorMeetingDate { get; private set; }
        public RMSString Bankruptcy_CaseNumber { get; private set; }
        public RMSDate Bankruptcy_FiledDate { get; private set; }
        public RMSDate BarDate { get; private set; }
        public RMSDate DismissDate { get; private set; }
        public RMSDate DischargeDate { get; private set; }
        public RMSDate ReaffirmDate { get; private set; }
        public RMSDate StayLiftedDate { get; private set; }
        public RMSString Defendant_Attorney_Name { get; private set; }
        public RMSString Defendant_Attorney_Address1 { get; private set; }
        public RMSString Defendant_Attorney_Address2 { get; private set; }
        public RMSString Defendant_Attorney_City { get; private set; }
        public RMSString Defendant_Attorney_State { get; private set; }
        public RMSString Defendant_Attorney_Zip { get; private set; }
        public RMSString Defendant_Attorney_Phone { get; private set; }
        public RMSDate Judgment_Date { get; private set; }
        public RMSBool BankAssetIndicator { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSZoneDecimal POC_PrincipalBalance { get; private set; }
        public RMSZoneDecimal POC_InterestBalance { get; private set; }
        public RMSZoneDecimal POC_FeeBalance { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        #endregion

        public LegalRecord(string Record)
            : base(Record)
        {
            try
            {
                this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
                this.Suit_Reason = new RMSString(30) { DataString = Record.Substring(23) };
                this.Suit_FiledDate = new RMSDate() { DataString = Record.Substring(53) };
                this.Suit_CaseNumber = new RMSString(20) { DataString = Record.Substring(61) };
                this.CourtName = new RMSString(30) { DataString = Record.Substring(81) };
                this.Judgment_RecordedDate = new RMSDate() { DataString = Record.Substring(111) };
                this.Judgment_Book = new RMSString(8) { DataString = Record.Substring(119) };
                this.Judgment_Page = new RMSString(8) { DataString = Record.Substring(127) };
                this.Judgment_ExpirationDate = new RMSDate() { DataString = Record.Substring(135) };
                this.CreditorMeetingLocation = new RMSString(15) { DataString = Record.Substring(143) };
                this.CreditorMeetingDate = new RMSDate() { DataString = Record.Substring(158) };
                this.Bankruptcy_CaseNumber = new RMSString(20) { DataString = Record.Substring(166) };
                this.Bankruptcy_FiledDate = new RMSDate() { DataString = Record.Substring(186) };
                this.BarDate = new RMSDate() { DataString = Record.Substring(194) };
                this.DismissDate = new RMSDate() { DataString = Record.Substring(202) };
                this.DischargeDate = new RMSDate() { DataString = Record.Substring(210) };
                this.ReaffirmDate = new RMSDate() { DataString = Record.Substring(218) };
                this.StayLiftedDate = new RMSDate() { DataString = Record.Substring(226) };
                this.Defendant_Attorney_Name = new RMSString(40) { DataString = Record.Substring(234) };
                this.Defendant_Attorney_Address1 = new RMSString(40) { DataString = Record.Substring(274) };
                this.Defendant_Attorney_Address2 = new RMSString(40) { DataString = Record.Substring(314) };
                this.Defendant_Attorney_City = new RMSString(20) { DataString = Record.Substring(354) };
                this.Defendant_Attorney_State = new RMSString(2) { DataString = Record.Substring(374) };
                this.Defendant_Attorney_Zip = new RMSString(10) { DataString = Record.Substring(376) };
                this.Defendant_Attorney_Phone = new RMSString(16) { DataString = Record.Substring(386) };
                this.Judgment_Date = new RMSDate() { DataString = Record.Substring(402) };
                this.BankAssetIndicator = new RMSBool(1, "Y") { DataString = Record.Substring(410) };
                this.Filler1 = new RMSString(247) { DataString = Record.Substring(411) };
                this.POC_PrincipalBalance = new RMSZoneDecimal(11, 2) { DataString = Record.Substring(658) };
                this.POC_InterestBalance = new RMSZoneDecimal(11, 2) { DataString = Record.Substring(669) };
                this.POC_FeeBalance = new RMSZoneDecimal(11, 2) { DataString = Record.Substring(680) };
                this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
                if (this.Suit_Reason == null) this.Suit_Reason = new RMSString(30);
                if (this.Suit_FiledDate == null) this.Suit_FiledDate = new RMSDate();
                if (this.Suit_CaseNumber == null) this.Suit_CaseNumber = new RMSString(20);
                if (this.CourtName == null) this.CourtName = new RMSString(30);
                if (this.Judgment_RecordedDate == null) this.Judgment_RecordedDate = new RMSDate();
                if (this.Judgment_Book == null) this.Judgment_Book = new RMSString(8);
                if (this.Judgment_Page == null) this.Judgment_Page = new RMSString(8);
                if (this.Judgment_ExpirationDate == null) this.Judgment_ExpirationDate = new RMSDate();
                if (this.CreditorMeetingLocation == null) this.CreditorMeetingLocation = new RMSString(15);
                if (this.CreditorMeetingDate == null) this.CreditorMeetingDate = new RMSDate();
                if (this.Bankruptcy_CaseNumber == null) this.Bankruptcy_CaseNumber = new RMSString(20);
                if (this.Bankruptcy_FiledDate == null) this.Bankruptcy_FiledDate = new RMSDate();
                if (this.BarDate == null) this.BarDate = new RMSDate();
                if (this.DismissDate == null) this.DismissDate = new RMSDate();
                if (this.DischargeDate == null) this.DischargeDate = new RMSDate();
                if (this.ReaffirmDate == null) this.ReaffirmDate = new RMSDate();
                if (this.StayLiftedDate == null) this.StayLiftedDate = new RMSDate();
                if (this.Defendant_Attorney_Name == null) this.Defendant_Attorney_Name = new RMSString(40);
                if (this.Defendant_Attorney_Address1 == null) this.Defendant_Attorney_Address1 = new RMSString(40);
                if (this.Defendant_Attorney_Address2 == null) this.Defendant_Attorney_Address2 = new RMSString(40);
                if (this.Defendant_Attorney_City == null) this.Defendant_Attorney_City = new RMSString(20);
                if (this.Defendant_Attorney_State == null) this.Defendant_Attorney_State = new RMSString(2);
                if (this.Defendant_Attorney_Zip == null) this.Defendant_Attorney_Zip = new RMSString(10);
                if (this.Defendant_Attorney_Phone == null) this.Defendant_Attorney_Phone = new RMSString(16);
                if (this.Judgment_Date == null) this.Judgment_Date = new RMSDate();
                if (this.BankAssetIndicator == null) this.BankAssetIndicator = new RMSBool(1, "Y");
                if (this.Filler1 == null) this.Filler1 = new RMSString(247);
                if (this.POC_PrincipalBalance == null) this.POC_PrincipalBalance = new RMSZoneDecimal(11, 2);
                if (this.POC_InterestBalance == null) this.POC_InterestBalance = new RMSZoneDecimal(11, 2);
                if (this.POC_FeeBalance == null) this.POC_FeeBalance = new RMSZoneDecimal(11, 2);
                if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
                if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}",
                base.ToString(),
                this.SequenceNumber,
                this.Suit_Reason,
                this.Suit_FiledDate,
                this.Suit_CaseNumber,
                this.CourtName,
                this.Judgment_RecordedDate,
                this.Judgment_Book,
                this.Judgment_Page,
                this.Judgment_ExpirationDate,
                this.CreditorMeetingLocation,
                this.CreditorMeetingDate,
                this.Bankruptcy_CaseNumber,
                this.Bankruptcy_FiledDate,
                this.BarDate,
                this.DismissDate,
                this.DischargeDate,
                this.ReaffirmDate,
                this.StayLiftedDate,
                this.Defendant_Attorney_Name,
                this.Defendant_Attorney_Address1,
                this.Defendant_Attorney_Address2,
                this.Defendant_Attorney_City,
                this.Defendant_Attorney_State,
                this.Defendant_Attorney_Zip,
                this.Defendant_Attorney_Phone,
                this.Judgment_Date,
                this.BankAssetIndicator,
                this.Filler1,
                this.POC_PrincipalBalance,
                this.POC_InterestBalance,
                this.POC_FeeBalance,
                this.ParentOrgCode,
                this.AgencyCode,
                this.FormatCode);
        }
    }

    //public class CreditRecord : AccountInfo
    //{
    //    #region Public Properties
    //    public RMSNumber SequenceNumber { get; private set; }
    //    public RMSDate LastCashAdvanceDate { get; private set; }
    //    public RMSDate LastPurchaseDate { get; private set; }
    //    public RMSNumber Cycle01_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle02_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle03_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle04_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle05_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle06_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle07_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle08_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle09_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle10_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle11_NumberOfPayments { get; private set; }
    //    public RMSNumber Cycle12_NumberOfPayments { get; private set; }
    //    public RMSDecimal Cycle01_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle02_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle03_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle04_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle05_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle06_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle07_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle08_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle09_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle10_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle11_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle12_AmountOfPayments { get; private set; }
    //    public RMSDecimal Cycle01_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle02_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle03_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle04_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle05_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle06_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle07_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle08_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle09_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle10_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle11_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle12_AmountOfPurchases { get; private set; }
    //    public RMSDecimal Cycle01_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle02_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle03_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle04_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle05_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle06_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle07_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle08_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle09_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle10_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle11_AmountOfCashAdvance { get; private set; }
    //    public RMSDecimal Cycle12_AmountOfCashAdvance { get; private set; }
    //    public RMSNumber NumberNSFChecks { get; private set; }
    //    public RMSDecimal CreditLimit { get; private set; }
    //    public RMSNumber TimesPastDue_030Days { get; private set; }
    //    public RMSNumber TimesPastDue_060Days { get; private set; }
    //    public RMSNumber TimesPastDue_090Days { get; private set; }
    //    public RMSNumber TimesPastDue_120Days { get; private set; }
    //    public RMSNumber TimesPastDue_180Days { get; private set; }
    //    public RMSDecimal AmountPastDue_30Days { get; private set; }
    //    public RMSDecimal AmountPastDue_60Days { get; private set; }
    //    public RMSDecimal AmountPastDue_90Days { get; private set; }
    //    public RMSDecimal AmountPastDue_91Days { get; private set; }
    //    public RMSDecimal AmountPaymentsThisCycle { get; private set; }
    //    public RMSDecimal HighCreditAmount { get; private set; }
    //    public RMSDecimal PayoffAmount { get; private set; }
    //    public RMSDate MaturationDate { get; private set; }
    //    public RMSDate RegPaymentDate { get; private set; }
    //    public RMSNumber NumberMonthsExtended { get; private set; }
    //    public RMSDecimal LateChargesDue { get; private set; }
    //    public RMSDate LastExtensionDate { get; private set; }
    //    public RMSString Filler1 { get; private set; }
    //    public RMSString ParentOrgCode { get; private set; }
    //    public RMSString AgencyCode { get; private set; }
    //    public string FormatCode { get { return "B"; } }
    //    #endregion

    //    public CreditRecord(string Record)
    //        : base(Record)
    //    {
    //        try
    //        {
    //            this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
    //            this.LastCashAdvanceDate = new RMSDate() { DataString = Record.Substring(23) };
    //            this.LastPurchaseDate = new RMSDate() { DataString = Record.Substring(31) };
    //            this.Cycle01_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(39) };
    //            this.Cycle02_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(41) };
    //            this.Cycle03_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(43) };
    //            this.Cycle04_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(45) };
    //            this.Cycle05_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(47) };
    //            this.Cycle06_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(49) };
    //            this.Cycle07_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(51) };
    //            this.Cycle08_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(53) };
    //            this.Cycle09_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(55) };
    //            this.Cycle10_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(57) };
    //            this.Cycle11_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(59) };
    //            this.Cycle12_NumberOfPayments = new RMSNumber(2) { DataString = Record.Substring(61) };
    //            this.Cycle01_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(63) };
    //            this.Cycle02_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(76) };
    //            this.Cycle03_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(89) };
    //            this.Cycle04_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(102) };
    //            this.Cycle05_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(115) };
    //            this.Cycle06_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(128) };
    //            this.Cycle07_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(141) };
    //            this.Cycle08_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(154) };
    //            this.Cycle09_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(167) };
    //            this.Cycle10_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(180) };
    //            this.Cycle11_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(193) };
    //            this.Cycle12_AmountOfPayments = new RMSDecimal(13, 2) { DataString = Record.Substring(206) };
    //            this.Cycle01_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(219) };
    //            this.Cycle02_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(232) };
    //            this.Cycle03_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(245) };
    //            this.Cycle04_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(258) };
    //            this.Cycle05_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(271) };
    //            this.Cycle06_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(284) };
    //            this.Cycle07_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(297) };
    //            this.Cycle08_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(310) };
    //            this.Cycle09_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(323) };
    //            this.Cycle10_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(336) };
    //            this.Cycle11_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(349) };
    //            this.Cycle12_AmountOfPurchases = new RMSDecimal(13, 2) { DataString = Record.Substring(362) };
    //            this.Cycle01_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(375) };
    //            this.Cycle02_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(388) };
    //            this.Cycle03_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(401) };
    //            this.Cycle04_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(414) };
    //            this.Cycle05_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(427) };
    //            this.Cycle06_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(440) };
    //            this.Cycle07_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(453) };
    //            this.Cycle08_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(466) };
    //            this.Cycle09_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(479) };
    //            this.Cycle10_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(492) };
    //            this.Cycle11_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(505) };
    //            this.Cycle12_AmountOfCashAdvance = new RMSDecimal(13, 2) { DataString = Record.Substring(518) };
    //            this.NumberNSFChecks = new RMSNumber(2) { DataString = Record.Substring(531) };
    //            this.CreditLimit = new RMSDecimal(13, 2) { DataString = Record.Substring(533) };
    //            this.TimesPastDue_030Days = new RMSNumber(2) { DataString = Record.Substring(546) };
    //            this.TimesPastDue_060Days = new RMSNumber(2) { DataString = Record.Substring(548) };
    //            this.TimesPastDue_090Days = new RMSNumber(2) { DataString = Record.Substring(550) };
    //            this.TimesPastDue_120Days = new RMSNumber(2) { DataString = Record.Substring(552) };
    //            this.TimesPastDue_180Days = new RMSNumber(2) { DataString = Record.Substring(554) };
    //            this.AmountPastDue_30Days = new RMSDecimal(13, 2) { DataString = Record.Substring(556) };
    //            this.AmountPastDue_60Days = new RMSDecimal(13, 2) { DataString = Record.Substring(569) };
    //            this.AmountPastDue_90Days = new RMSDecimal(13, 2) { DataString = Record.Substring(582) };
    //            this.AmountPastDue_91Days = new RMSDecimal(13, 2) { DataString = Record.Substring(595) };
    //            this.AmountPaymentsThisCycle = new RMSDecimal(13, 2) { DataString = Record.Substring(608) };
    //            this.HighCreditAmount = new RMSDecimal(11, 2) { DataString = Record.Substring(621) };
    //            this.PayoffAmount = new RMSDecimal(11, 2) { DataString = Record.Substring(632) };
    //            this.MaturationDate = new RMSDate() { DataString = Record.Substring(643) };
    //            this.RegPaymentDate = new RMSDate() { DataString = Record.Substring(651) };
    //            this.NumberMonthsExtended = new RMSNumber(3) { DataString = Record.Substring(659) };
    //            this.LateChargesDue = new RMSDecimal(11, 2) { DataString = Record.Substring(662) };
    //            this.LastExtensionDate = new RMSDate() { DataString = Record.Substring(673) };
    //            this.Filler1 = new RMSString(10) { DataString = Record.Substring(681) };
    //            this.ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
    //            this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
    //        }
    //        catch
    //        {
    //            if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
    //            if (this.LastCashAdvanceDate == null) this.LastCashAdvanceDate = new RMSDate();
    //            if (this.LastPurchaseDate == null) this.LastPurchaseDate = new RMSDate();
    //            if (this.Cycle01_NumberOfPayments == null) this.Cycle01_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle02_NumberOfPayments == null) this.Cycle02_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle03_NumberOfPayments == null) this.Cycle03_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle04_NumberOfPayments == null) this.Cycle04_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle05_NumberOfPayments == null) this.Cycle05_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle06_NumberOfPayments == null) this.Cycle06_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle07_NumberOfPayments == null) this.Cycle07_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle08_NumberOfPayments == null) this.Cycle08_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle09_NumberOfPayments == null) this.Cycle09_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle10_NumberOfPayments == null) this.Cycle10_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle11_NumberOfPayments == null) this.Cycle11_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle12_NumberOfPayments == null) this.Cycle12_NumberOfPayments = new RMSNumber(2);
    //            if (this.Cycle01_AmountOfPayments == null) this.Cycle01_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle02_AmountOfPayments == null) this.Cycle02_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle03_AmountOfPayments == null) this.Cycle03_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle04_AmountOfPayments == null) this.Cycle04_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle05_AmountOfPayments == null) this.Cycle05_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle06_AmountOfPayments == null) this.Cycle06_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle07_AmountOfPayments == null) this.Cycle07_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle08_AmountOfPayments == null) this.Cycle08_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle09_AmountOfPayments == null) this.Cycle09_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle10_AmountOfPayments == null) this.Cycle10_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle11_AmountOfPayments == null) this.Cycle11_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle12_AmountOfPayments == null) this.Cycle12_AmountOfPayments = new RMSDecimal(13, 2);
    //            if (this.Cycle01_AmountOfPurchases == null) this.Cycle01_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle02_AmountOfPurchases == null) this.Cycle02_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle03_AmountOfPurchases == null) this.Cycle03_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle04_AmountOfPurchases == null) this.Cycle04_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle05_AmountOfPurchases == null) this.Cycle05_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle06_AmountOfPurchases == null) this.Cycle06_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle07_AmountOfPurchases == null) this.Cycle07_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle08_AmountOfPurchases == null) this.Cycle08_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle09_AmountOfPurchases == null) this.Cycle09_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle10_AmountOfPurchases == null) this.Cycle10_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle11_AmountOfPurchases == null) this.Cycle11_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle12_AmountOfPurchases == null) this.Cycle12_AmountOfPurchases = new RMSDecimal(13, 2);
    //            if (this.Cycle01_AmountOfCashAdvance == null) this.Cycle01_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle02_AmountOfCashAdvance == null) this.Cycle02_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle03_AmountOfCashAdvance == null) this.Cycle03_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle04_AmountOfCashAdvance == null) this.Cycle04_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle05_AmountOfCashAdvance == null) this.Cycle05_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle06_AmountOfCashAdvance == null) this.Cycle06_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle07_AmountOfCashAdvance == null) this.Cycle07_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle08_AmountOfCashAdvance == null) this.Cycle08_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle09_AmountOfCashAdvance == null) this.Cycle09_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle10_AmountOfCashAdvance == null) this.Cycle10_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle11_AmountOfCashAdvance == null) this.Cycle11_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.Cycle12_AmountOfCashAdvance == null) this.Cycle12_AmountOfCashAdvance = new RMSDecimal(13, 2);
    //            if (this.NumberNSFChecks == null) this.NumberNSFChecks = new RMSNumber(2);
    //            if (this.CreditLimit == null) this.CreditLimit = new RMSDecimal(13, 2);
    //            if (this.TimesPastDue_030Days == null) this.TimesPastDue_030Days = new RMSNumber(2);
    //            if (this.TimesPastDue_060Days == null) this.TimesPastDue_060Days = new RMSNumber(2);
    //            if (this.TimesPastDue_090Days == null) this.TimesPastDue_090Days = new RMSNumber(2);
    //            if (this.TimesPastDue_120Days == null) this.TimesPastDue_120Days = new RMSNumber(2);
    //            if (this.TimesPastDue_180Days == null) this.TimesPastDue_180Days = new RMSNumber(2);
    //            if (this.AmountPastDue_30Days == null) this.AmountPastDue_30Days = new RMSDecimal(13, 2);
    //            if (this.AmountPastDue_60Days == null) this.AmountPastDue_60Days = new RMSDecimal(13, 2);
    //            if (this.AmountPastDue_90Days == null) this.AmountPastDue_90Days = new RMSDecimal(13, 2);
    //            if (this.AmountPastDue_91Days == null) this.AmountPastDue_91Days = new RMSDecimal(13, 2);
    //            if (this.AmountPaymentsThisCycle == null) this.AmountPaymentsThisCycle = new RMSDecimal(13, 2);
    //            if (this.HighCreditAmount == null) this.HighCreditAmount = new RMSDecimal(11, 2);
    //            if (this.PayoffAmount == null) this.PayoffAmount = new RMSDecimal(11, 2);
    //            if (this.MaturationDate == null) this.MaturationDate = new RMSDate();
    //            if (this.RegPaymentDate == null) this.RegPaymentDate = new RMSDate();
    //            if (this.NumberMonthsExtended == null) this.NumberMonthsExtended = new RMSNumber(3);
    //            if (this.LateChargesDue == null) this.LateChargesDue = new RMSDecimal(11, 2);
    //            if (this.LastExtensionDate == null) this.LastExtensionDate = new RMSDate();
    //            if (this.Filler1 == null) this.Filler1 = new RMSString(10);
    //            if (this.ParentOrgCode == null) this.ParentOrgCode = new RMSString(4);
    //            if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}{68}{69}{70}{71}{72}{73}{74}",
    //            base.ToString(),
    //            this.SequenceNumber,
    //            this.LastCashAdvanceDate,
    //            this.LastPurchaseDate,
    //            this.Cycle01_NumberOfPayments,
    //            this.Cycle02_NumberOfPayments,
    //            this.Cycle03_NumberOfPayments,
    //            this.Cycle04_NumberOfPayments,
    //            this.Cycle05_NumberOfPayments,
    //            this.Cycle06_NumberOfPayments,
    //            this.Cycle07_NumberOfPayments,
    //            this.Cycle08_NumberOfPayments,
    //            this.Cycle09_NumberOfPayments,
    //            this.Cycle10_NumberOfPayments,
    //            this.Cycle11_NumberOfPayments,
    //            this.Cycle12_NumberOfPayments,
    //            this.Cycle01_AmountOfPayments,
    //            this.Cycle02_AmountOfPayments,
    //            this.Cycle03_AmountOfPayments,
    //            this.Cycle04_AmountOfPayments,
    //            this.Cycle05_AmountOfPayments,
    //            this.Cycle06_AmountOfPayments,
    //            this.Cycle07_AmountOfPayments,
    //            this.Cycle08_AmountOfPayments,
    //            this.Cycle09_AmountOfPayments,
    //            this.Cycle10_AmountOfPayments,
    //            this.Cycle11_AmountOfPayments,
    //            this.Cycle12_AmountOfPayments,
    //            this.Cycle01_AmountOfPurchases,
    //            this.Cycle02_AmountOfPurchases,
    //            this.Cycle03_AmountOfPurchases,
    //            this.Cycle04_AmountOfPurchases,
    //            this.Cycle05_AmountOfPurchases,
    //            this.Cycle06_AmountOfPurchases,
    //            this.Cycle07_AmountOfPurchases,
    //            this.Cycle08_AmountOfPurchases,
    //            this.Cycle09_AmountOfPurchases,
    //            this.Cycle10_AmountOfPurchases,
    //            this.Cycle11_AmountOfPurchases,
    //            this.Cycle12_AmountOfPurchases,
    //            this.Cycle01_AmountOfCashAdvance,
    //            this.Cycle02_AmountOfCashAdvance,
    //            this.Cycle03_AmountOfCashAdvance,
    //            this.Cycle04_AmountOfCashAdvance,
    //            this.Cycle05_AmountOfCashAdvance,
    //            this.Cycle06_AmountOfCashAdvance,
    //            this.Cycle07_AmountOfCashAdvance,
    //            this.Cycle08_AmountOfCashAdvance,
    //            this.Cycle09_AmountOfCashAdvance,
    //            this.Cycle10_AmountOfCashAdvance,
    //            this.Cycle11_AmountOfCashAdvance,
    //            this.Cycle12_AmountOfCashAdvance,
    //            this.NumberNSFChecks,
    //            this.CreditLimit,
    //            this.TimesPastDue_030Days,
    //            this.TimesPastDue_060Days,
    //            this.TimesPastDue_090Days,
    //            this.TimesPastDue_120Days,
    //            this.TimesPastDue_180Days,
    //            this.AmountPastDue_30Days,
    //            this.AmountPastDue_60Days,
    //            this.AmountPastDue_90Days,
    //            this.AmountPastDue_91Days,
    //            this.AmountPaymentsThisCycle,
    //            this.HighCreditAmount,
    //            this.PayoffAmount,
    //            this.MaturationDate,
    //            this.RegPaymentDate,
    //            this.NumberMonthsExtended,
    //            this.LateChargesDue,
    //            this.LastExtensionDate,
    //            this.Filler1,
    //            this.ParentOrgCode,
    //            this.AgencyCode,
    //            this.FormatCode);
    //    }
    //}

    //public class UserRecord : AccountInfo
    //{
    //    #region Public Properties
    //    public RMSNumber SequenceNumber { get; private set; }
    //    public RMSString USER_VALUE_1 { get; private set; }
    //    public RMSString USER_VALUE_2 { get; private set; }
    //    public RMSString USER_VALUE_3 { get; private set; }
    //    public RMSString USER_VALUE_4 { get; private set; }
    //    public RMSString USER_VALUE_5 { get; private set; }
    //    public RMSString USER_VALUE_6 { get; private set; }
    //    public RMSString USER_VALUE_7 { get; private set; }
    //    public RMSString USER_VALUE_8 { get; private set; }
    //    public RMSDecimal USER_VAR_1 { get; private set; }
    //    public RMSDecimal USER_VAR_2 { get; private set; }
    //    public RMSDecimal USER_VAR_3 { get; private set; }
    //    public RMSDecimal USER_VAR_4 { get; private set; }
    //    public RMSDecimal USER_VAR_5 { get; private set; }
    //    public RMSDecimal USER_VAR_6 { get; private set; }
    //    public RMSDecimal USER_VAR_7 { get; private set; }
    //    public RMSDecimal USER_VAR_8 { get; private set; }
    //    public RMSDate USER_DATE_1 { get; private set; }
    //    public RMSDate USER_DATE_2 { get; private set; }
    //    public RMSDate USER_DATE_3 { get; private set; }
    //    public RMSDate USER_DATE_4 { get; private set; }
    //    public RMSDate USER_DATE_5 { get; private set; }
    //    public RMSDate USER_DATE_6 { get; private set; }
    //    public RMSDate USER_DATE_7 { get; private set; }
    //    public RMSDate USER_DATE_8 { get; private set; }
    //    public RMSString Filler1 { get; private set; }
    //    public RMSString Filler2 { get; private set; }
    //    public RMSString AgencyCode { get; private set; }
    //    public string FormatCode { get { return "B"; } }
    //    #endregion

    //    public UserRecord(string Record)
    //        : base(Record)
    //    {
    //        try
    //        {
    //            this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
    //            this.USER_VALUE_1 = new RMSString(40) { DataString = Record.Substring(23) };
    //            this.USER_VALUE_2 = new RMSString(40) { DataString = Record.Substring(63) };
    //            this.USER_VALUE_3 = new RMSString(40) { DataString = Record.Substring(103) };
    //            this.USER_VALUE_4 = new RMSString(40) { DataString = Record.Substring(143) };
    //            this.USER_VALUE_5 = new RMSString(40) { DataString = Record.Substring(183) };
    //            this.USER_VALUE_6 = new RMSString(40) { DataString = Record.Substring(223) };
    //            this.USER_VALUE_7 = new RMSString(40) { DataString = Record.Substring(263) };
    //            this.USER_VALUE_8 = new RMSString(40) { DataString = Record.Substring(303) };
    //            this.USER_VAR_1 = new RMSDecimal(10, 2) { DataString = Record.Substring(343) };
    //            this.USER_VAR_2 = new RMSDecimal(10, 2) { DataString = Record.Substring(353) };
    //            this.USER_VAR_3 = new RMSDecimal(10, 2) { DataString = Record.Substring(363) };
    //            this.USER_VAR_4 = new RMSDecimal(10, 2) { DataString = Record.Substring(373) };
    //            this.USER_VAR_5 = new RMSDecimal(10, 2) { DataString = Record.Substring(383) };
    //            this.USER_VAR_6 = new RMSDecimal(10, 2) { DataString = Record.Substring(393) };
    //            this.USER_VAR_7 = new RMSDecimal(10, 2) { DataString = Record.Substring(403) };
    //            this.USER_VAR_8 = new RMSDecimal(10, 2) { DataString = Record.Substring(413) };
    //            this.USER_DATE_1 = new RMSDate() { DataString = Record.Substring(423) };
    //            this.USER_DATE_2 = new RMSDate() { DataString = Record.Substring(431) };
    //            this.USER_DATE_3 = new RMSDate() { DataString = Record.Substring(439) };
    //            this.USER_DATE_4 = new RMSDate() { DataString = Record.Substring(447) };
    //            this.USER_DATE_5 = new RMSDate() { DataString = Record.Substring(455) };
    //            this.USER_DATE_6 = new RMSDate() { DataString = Record.Substring(463) };
    //            this.USER_DATE_7 = new RMSDate() { DataString = Record.Substring(471) };
    //            this.USER_DATE_8 = new RMSDate() { DataString = Record.Substring(479) };
    //            this.Filler1 = new RMSString(204) { DataString = Record.Substring(487) };
    //            this.Filler2 = new RMSString(4) { DataString = Record.Substring(691) };
    //            this.AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
    //        }
    //        catch
    //        {
    //            if (this == null) this.SequenceNumber = new RMSNumber(2);
    //            if (this == null) this.USER_VALUE_1 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_2 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_3 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_4 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_5 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_6 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_7 = new RMSString(40);
    //            if (this == null) this.USER_VALUE_8 = new RMSString(40);
    //            if (this == null) this.USER_VAR_1 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_2 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_3 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_4 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_5 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_6 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_7 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_VAR_8 = new RMSDecimal(10, 2);
    //            if (this == null) this.USER_DATE_1 = new RMSDate();
    //            if (this == null) this.USER_DATE_2 = new RMSDate();
    //            if (this == null) this.USER_DATE_3 = new RMSDate();
    //            if (this == null) this.USER_DATE_4 = new RMSDate();
    //            if (this == null) this.USER_DATE_5 = new RMSDate();
    //            if (this == null) this.USER_DATE_6 = new RMSDate();
    //            if (this == null) this.USER_DATE_7 = new RMSDate();
    //            if (this == null) this.USER_DATE_8 = new RMSDate();
    //            if (this == null) this.Filler1 = new RMSString(204);
    //            if (this == null) this.Filler2 = new RMSString(4);
    //            if (this == null) this.AgencyCode = new RMSString(4);
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}",
    //            base.ToString(),
    //            this.SequenceNumber,
    //            this.USER_VALUE_1,
    //            this.USER_VALUE_2,
    //            this.USER_VALUE_3,
    //            this.USER_VALUE_4,
    //            this.USER_VALUE_5,
    //            this.USER_VALUE_6,
    //            this.USER_VALUE_7,
    //            this.USER_VALUE_8,
    //            this.USER_VAR_1,
    //            this.USER_VAR_2,
    //            this.USER_VAR_3,
    //            this.USER_VAR_4,
    //            this.USER_VAR_5,
    //            this.USER_VAR_6,
    //            this.USER_VAR_7,
    //            this.USER_VAR_8,
    //            this.USER_DATE_1,
    //            this.USER_DATE_2,
    //            this.USER_DATE_3,
    //            this.USER_DATE_4,
    //            this.USER_DATE_5,
    //            this.USER_DATE_6,
    //            this.USER_DATE_7,
    //            this.USER_DATE_8,
    //            this.Filler1,
    //            this.Filler2,
    //            this.AgencyCode,
    //            this.FormatCode);
    //    }
    //}

    public class ExtraRecord : AccountInfo
    {
        #region Public Properties
        public RMSNumber SequenceNumber { get; private set; }
        public RMSString UserDefinedCode { get; private set; }
        public RMSNumber DaysDelinquent { get; private set; }
        public RMSString DelinquencyBucket { get; private set; }
        public RMSString DelinquencyHistory { get; private set; }
        public RMSString TreatmentTag { get; private set; }
        public RMSString ForbearanceIndicator { get; private set; }
        public RMSString CellPhoneIndicator { get; private set; }
        public RMSString CellPhone { get; private set; }
        public RMSString Filler1 { get; private set; }
        public RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues> HomePhoneType { get; private set; }
        public RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues> OfficePhoneType { get; private set; }
        public RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues> CellPhoneType { get; private set; }
        public RMSString Filler2 { get; private set; }
        public RMSString UserDefined_Alpha03 { get; private set; }
        public RMSString UserDefined_Alpha04 { get; private set; }
        public RMSString UserDefined_Alpha05 { get; private set; }
        public RMSString UserDefined_Alpha06 { get; private set; }
        public RMSString UserDefined_Alpha07 { get; private set; }
        public RMSString UserDefined_Alpha08 { get; private set; }
        public RMSString UserDefined_Alpha09 { get; private set; }
        public RMSString UserDefined_Alpha10 { get; private set; }
        public RMSZoneDecimal POC_PrincipalBalance { get; private set; }
        public RMSZoneDecimal POC_InterestBalance { get; private set; }
        public RMSZoneDecimal POC_FeeBalance { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric04 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric05 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric06 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric07 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric08 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric09 { get; private set; }
        public RMSZoneDecimal UserDefined_Numeric10 { get; private set; }
        public RMSDate UserDefined_Date01 { get; private set; }
        public RMSDate UserDefined_Date02 { get; private set; }
        public RMSDate UserDefined_Date03 { get; private set; }
        public RMSDate UserDefined_Date04 { get; private set; }
        public RMSDate UserDefined_Date05 { get; private set; }
        public RMSDate UserDefined_Date06 { get; private set; }
        public RMSDate UserDefined_Date07 { get; private set; }
        public RMSDate UserDefined_Date08 { get; private set; }
        public RMSDate UserDefined_Date09 { get; private set; }
        public RMSDate UserDefined_Date10 { get; private set; }
        public RMSString Filler3 { get; private set; }
        public RMSString ParentOrgCode { get; private set; }
        public RMSString AgencyCode { get; private set; }
        public string FormatCode { get { return "B"; } }
        #endregion

        public ExtraRecord(string Record)
            : base(Record)
        {
            try
            {
                SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
                UserDefinedCode = new RMSString(2) { DataString = Record.Substring(23) };
                DaysDelinquent = new RMSNumber(3) { DataString = Record.Substring(25) };
                DelinquencyBucket = new RMSString(1) { DataString = Record.Substring(28) };
                DelinquencyHistory = new RMSString(12) { DataString = Record.Substring(29) };
                TreatmentTag = new RMSString(4) { DataString = Record.Substring(41) };
                ForbearanceIndicator = new RMSString(1) { DataString = Record.Substring(45) };
                CellPhoneIndicator = new RMSString(1) { DataString = Record.Substring(46) };
                CellPhone = new RMSString(16) { DataString = Record.Substring(47) };
                Filler1 = new RMSString(2) { DataString = Record.Substring(63) };
                HomePhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1) { DataString = Record.Substring(65) };
                OfficePhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1) { DataString = Record.Substring(66) };
                CellPhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1) { DataString = Record.Substring(67) };
                Filler2 = new RMSString(37) { DataString = Record.Substring(68) };
                UserDefined_Alpha03 = new RMSString(40) { DataString = Record.Substring(105) };
                UserDefined_Alpha04 = new RMSString(40) { DataString = Record.Substring(145) };
                UserDefined_Alpha05 = new RMSString(40) { DataString = Record.Substring(185) };
                UserDefined_Alpha06 = new RMSString(40) { DataString = Record.Substring(225) };
                UserDefined_Alpha07 = new RMSString(40) { DataString = Record.Substring(265) };
                UserDefined_Alpha08 = new RMSString(40) { DataString = Record.Substring(305) };
                UserDefined_Alpha09 = new RMSString(40) { DataString = Record.Substring(345) };
                UserDefined_Alpha10 = new RMSString(40) { DataString = Record.Substring(385) };
                POC_PrincipalBalance = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(425) };
                POC_InterestBalance = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(438) };
                POC_FeeBalance = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(451) };
                UserDefined_Numeric04 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(464) };
                UserDefined_Numeric05 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(477) };
                UserDefined_Numeric06 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(490) };
                UserDefined_Numeric07 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(503) };
                UserDefined_Numeric08 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(516) };
                UserDefined_Numeric09 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(529) };
                UserDefined_Numeric10 = new RMSZoneDecimal(13, 2) { DataString = Record.Substring(542) };
                UserDefined_Date01 = new RMSDate() { DataString = Record.Substring(555) };
                UserDefined_Date02 = new RMSDate() { DataString = Record.Substring(563) };
                UserDefined_Date03 = new RMSDate() { DataString = Record.Substring(571) };
                UserDefined_Date04 = new RMSDate() { DataString = Record.Substring(579) };
                UserDefined_Date05 = new RMSDate() { DataString = Record.Substring(587) };
                UserDefined_Date06 = new RMSDate() { DataString = Record.Substring(595) };
                UserDefined_Date07 = new RMSDate() { DataString = Record.Substring(603) };
                UserDefined_Date08 = new RMSDate() { DataString = Record.Substring(611) };
                UserDefined_Date09 = new RMSDate() { DataString = Record.Substring(619) };
                UserDefined_Date10 = new RMSDate() { DataString = Record.Substring(627) };
                Filler3 = new RMSString(56) { DataString = Record.Substring(635) };
                ParentOrgCode = new RMSString(4) { DataString = Record.Substring(691) };
                AgencyCode = new RMSString(4) { DataString = Record.Substring(695) };
            }
            catch
            {
                if (SequenceNumber == null) SequenceNumber = new RMSNumber(2);
                if (UserDefinedCode == null) UserDefinedCode = new RMSString(2);
                if (DaysDelinquent == null) DaysDelinquent = new RMSNumber(3);
                if (DelinquencyBucket == null) DelinquencyBucket = new RMSString(1);
                if (DelinquencyHistory == null) DelinquencyHistory = new RMSString(12);
                if (TreatmentTag == null) TreatmentTag = new RMSString(4);
                if (ForbearanceIndicator == null) ForbearanceIndicator = new RMSString(1);
                if (CellPhoneIndicator == null) CellPhoneIndicator = new RMSString(1);
                if (CellPhone == null) CellPhone = new RMSString(16);
                if (Filler1 == null) Filler1 = new RMSString(2);
                if (HomePhoneType == null) HomePhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1);
                if (OfficePhoneType == null) OfficePhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1);
                if (CellPhoneType == null) CellPhoneType = new RMSEnum<Enums.PhoneType, Enums.PhoneTypeValues>(1);
                if (Filler2 == null) Filler2 = new RMSString(37);
                if (UserDefined_Alpha03 == null) UserDefined_Alpha03 = new RMSString(40);
                if (UserDefined_Alpha04 == null) UserDefined_Alpha04 = new RMSString(40);
                if (UserDefined_Alpha05 == null) UserDefined_Alpha05 = new RMSString(40);
                if (UserDefined_Alpha06 == null) UserDefined_Alpha06 = new RMSString(40);
                if (UserDefined_Alpha07 == null) UserDefined_Alpha07 = new RMSString(40);
                if (UserDefined_Alpha08 == null) UserDefined_Alpha08 = new RMSString(40);
                if (UserDefined_Alpha09 == null) UserDefined_Alpha09 = new RMSString(40);
                if (UserDefined_Alpha10 == null) UserDefined_Alpha10 = new RMSString(40);
                if (POC_PrincipalBalance == null) POC_PrincipalBalance = new RMSZoneDecimal(13, 2);
                if (POC_InterestBalance == null) POC_InterestBalance = new RMSZoneDecimal(13, 2);
                if (POC_FeeBalance == null) POC_FeeBalance = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric04 == null) UserDefined_Numeric04 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric05 == null) UserDefined_Numeric05 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric06 == null) UserDefined_Numeric06 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric07 == null) UserDefined_Numeric07 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric08 == null) UserDefined_Numeric08 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric09 == null) UserDefined_Numeric09 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Numeric10 == null) UserDefined_Numeric10 = new RMSZoneDecimal(13, 2);
                if (UserDefined_Date01 == null) UserDefined_Date01 = new RMSDate();
                if (UserDefined_Date02 == null) UserDefined_Date02 = new RMSDate();
                if (UserDefined_Date03 == null) UserDefined_Date03 = new RMSDate();
                if (UserDefined_Date04 == null) UserDefined_Date04 = new RMSDate();
                if (UserDefined_Date05 == null) UserDefined_Date05 = new RMSDate();
                if (UserDefined_Date06 == null) UserDefined_Date06 = new RMSDate();
                if (UserDefined_Date07 == null) UserDefined_Date07 = new RMSDate();
                if (UserDefined_Date08 == null) UserDefined_Date08 = new RMSDate();
                if (UserDefined_Date09 == null) UserDefined_Date09 = new RMSDate();
                if (UserDefined_Date10 == null) UserDefined_Date10 = new RMSDate();
                if (Filler3 == null) Filler3 = new RMSString(56);
                if (ParentOrgCode == null) ParentOrgCode = new RMSString(4);
                if (AgencyCode == null) AgencyCode = new RMSString(4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}",
                base.ToString(),
                SequenceNumber,
                UserDefinedCode,
                DaysDelinquent,
                DelinquencyBucket,
                DelinquencyHistory,
                TreatmentTag,
                ForbearanceIndicator,
                CellPhoneIndicator,
                CellPhone,
                Filler1,
                HomePhoneType,
                OfficePhoneType,
                CellPhoneType,
                Filler2,
                UserDefined_Alpha03,
                UserDefined_Alpha04,
                UserDefined_Alpha05,
                UserDefined_Alpha06,
                UserDefined_Alpha07,
                UserDefined_Alpha08,
                UserDefined_Alpha09,
                UserDefined_Alpha10,
                POC_PrincipalBalance,
                POC_InterestBalance,
                POC_FeeBalance,
                UserDefined_Numeric04,
                UserDefined_Numeric05,
                UserDefined_Numeric06,
                UserDefined_Numeric07,
                UserDefined_Numeric08,
                UserDefined_Numeric09,
                UserDefined_Numeric10,
                UserDefined_Date01,
                UserDefined_Date02,
                UserDefined_Date03,
                UserDefined_Date04,
                UserDefined_Date05,
                UserDefined_Date06,
                UserDefined_Date07,
                UserDefined_Date08,
                UserDefined_Date09,
                UserDefined_Date10,
                Filler3,
                ParentOrgCode,
                AgencyCode);

        }
    }

    namespace Enums
    {
        public enum PhoneType
        {
            Unknown2 = 0,
            Landline,
            Mobile,
            Unknown,
            Invalid
        }
        public enum PhoneTypeValues
        {
            Nothing = 0,
            L,
            W,
            U,
            I
        }
    }
}
