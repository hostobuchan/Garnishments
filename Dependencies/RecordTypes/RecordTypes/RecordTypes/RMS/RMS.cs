using RecordTypes.RMS.Base;
using RecordTypes.RMS.DataTypes;
using RecordTypes.RMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes
{
    namespace RMS
    {

        public class HeaderRecord : Record
        {
            #region Public Properties
            public string HeaderConstant { get { return "RMSHDR"; } }
            public RMSString AgencyCode { get; private set; }
            public string HeaderSequence { get { return "00"; } }
            public string Filler { get { return "  "; } }
            public RMSDate Date { get; private set; }
            public string Filler2 { get { return "  "; } }
            public RMSNumber HeaderCount { get; private set; }
            public RMSZoneDecimal TotalBalance { get; private set; }
            public virtual string Filler3 { get { return "".PadRight(660, ' '); } }
            #endregion

            public HeaderRecord()
            {
                this.AgencyCode = new RMSString(4);
                this.Date = new RMSDate();
                this.HeaderCount = new RMSNumber(6);
                this.TotalBalance = new RMSZoneDecimal(10, 2);
            }
            public HeaderRecord(string Record)
            {
                this.AgencyCode = new RMSString(4) { DataString = Record.Substring(6, 4) };
                this.Date = new RMSDate() { DataString = Record.Substring(14, 8) };
                this.HeaderCount = new RMSNumber(6) { DataString = Record.Substring(24, 6) };
                this.TotalBalance = new RMSZoneDecimal(10, 2) { DataString = Record.Substring(30, 10) };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                    this.HeaderConstant,
                    this.AgencyCode,
                    this.HeaderSequence,
                    this.Filler,
                    this.Date,
                    this.Filler2,
                    this.HeaderCount,
                    this.TotalBalance,
                    this.Filler3);
            }

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
                return FileReaders.RMSRecordTypeIdentifier.GetRecordType(Record);
            }
            #endregion
        }

        public abstract class AccountInfo : Base.AccountInfo
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
                return FileReaders.RMSRecordTypeIdentifier.GetRecordType(Record);
            }
            #endregion
        }

        public abstract class AccountRecord : AccountInfo
        {
            #region Public Properties
            public RMSNumber SequenceNumber { get; private set; }
            public RMSString TaxNumber { get; private set; }
            public RMSEnum<CustomerTypes, CustomerTypeValues> CustomerType { get; private set; }
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
            public RMSDate DateOfBirth { get; private set; }
            public RMSString Employer { get; private set; }
            public RMSString Employer_Address { get; private set; }
            public string FormatCode { get { return "A"; } }
            public RMSString SSN { get { return this.TaxNumber; } }
            public RMSString CompanyName { get; private set; }
            #endregion

            public AccountRecord(string Record) : base(Record)
            {
                try
                {
                    this.SequenceNumber = new RMSNumber(2) { DataString = Record.Substring(21) };
                    this.TaxNumber = new RMSString(9) { DataString = Record.Substring(23) };
                    this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1) { DataString = Record.Substring(32) };
                    this.CompanyName = new RMSString(40) { DataString = Record.Substring(33) };
                    this.LastName = new RMSString(25) { DataString = Record.Substring(33) };
                    this.FirstName = new RMSString(15) { DataString = Record.Substring(58) };
                    this.Address1 = new RMSString(25) { DataString = Record.Substring(73) };
                    this.Address2 = new RMSString(25) { DataString = Record.Substring(98) };
                    this.City = new RMSString(20) { DataString = Record.Substring(123) };
                    this.County = new RMSString(15) { DataString = Record.Substring(143) };
                    this.State = new RMSString(2) { DataString = Record.Substring(158) };
                    this.Zip = new RMSString(10) { DataString = Record.Substring(160) };
                    this.Phone_Home = new RMSString(10) { DataString = Record.Substring(170) };
                    this.Phone_Work = new RMSString(10) { DataString = Record.Substring(180) };
                    this.DateOfBirth = new RMSDate() { DataString = Record.Substring(190) };
                    this.Employer = new RMSString(40) { DataString = Record.Substring(198) };
                    this.Employer_Address = new RMSString(40) { DataString = Record.Substring(238) };
                }
                catch
                {
                    if (this.SequenceNumber == null) this.SequenceNumber = new RMSNumber(2);
                    if (this.TaxNumber == null) this.TaxNumber = new RMSString(9);
                    if (this.CustomerType == null) this.CustomerType = new RMSEnum<CustomerTypes, CustomerTypeValues>(1);
                    if (this.CompanyName == null) this.CompanyName = new RMSString(40);
                    if (this.LastName == null) this.LastName = new RMSString(25);
                    if (this.FirstName == null) this.FirstName = new RMSString(15);
                    if (this.Address1 == null) this.Address1 = new RMSString(25);
                    if (this.Address2 == null) this.Address2 = new RMSString(25);
                    if (this.City == null) this.City = new RMSString(20);
                    if (this.County == null) this.County = new RMSString(15);
                    if (this.State == null) this.State = new RMSString(2);
                    if (this.Zip == null) this.Zip = new RMSString(10);
                    if (this.Phone_Home == null) this.Phone_Home = new RMSString(10);
                    if (this.Phone_Work == null) this.Phone_Work = new RMSString(10);
                    if (this.DateOfBirth == null) this.DateOfBirth = new RMSDate();
                    if (this.Employer == null) this.Employer = new RMSString(40);
                    if (this.Employer_Address == null) this.Employer_Address = new RMSString(40);
                }
            }
        }

        public class DebtorRecord : AccountRecord
        {
            #region Public Properties
            public RMSString LoanTypeCode { get; private set; }
            public RMSString LendingOfficerCode { get; private set; }
            public RMSString BankCode { get; private set; }
            public RMSString BranchCode { get; private set; }
            public RMSString CompCallCode { get; private set; }
            public RMSString AgencyCode { get; private set; }
            public RMSString DealerCode { get; private set; }
            public RMSString ChargeoffReason { get; private set; }
            public RMSString AccountStatus { get; private set; }
            public RMSZoneDecimal InterestRate { get; private set; }
            public RMSEnum<SourceCodes, SourceCodeValues> BalanceFieldType { get; private set; }
            public RMSDate DateReceived { get; private set; }
            public RMSDate ContractDate { get; private set; }
            public RMSDate ChargeoffDate { get; private set; }
            public RMSDate LastPaymentDate { get; private set; }
            public RMSDecimal ChargeoffAmount { get; private set; }
            public RMSDecimal AssociatedCosts { get; private set; }
            public RMSDecimal AccruedInterest { get; private set; }
            public RMSDecimal Balance { get; private set; }
            public RMSString LendingLevelData1 { get; private set; }
            public RMSString LendingLevelData2 { get; private set; }
            public RMSString LendingLevelData3 { get; private set; }
            public RMSString LendingLevelData4 { get; private set; }
            public RMSString LendingLevelData5 { get; private set; }
            public RMSString LendingLevelData6 { get; private set; }
            public RMSString LendingLevelData7 { get; private set; }
            public RMSString LendingLevelData8 { get; private set; }
            public RMSString LastCommentLine1 { get; private set; }
            public RMSString LastCommentLine2 { get; private set; }
            public RMSString LastCommentLine3 { get; private set; }
            public RMSDate LastCommentDate { get; private set; }
            public RMSString SecondLastName { get; private set; }
            public RMSString SecondFirstName { get; private set; }
            public RMSDecimal MonthlyIncome { get; private set; }
            public RMSDecimal OtherIncome { get; private set; }
            public RMSDecimal MonthlyPayment { get; private set; }
            public RMSDecimal OtherObligations { get; private set; }
            public RMSEnum<OwnRent, OwnRentValues> OwnRentCode { get; private set; }
            public RMSNumber RecoveryScore { get; private set; }
            public RMSDate NextPaymentDate { get; private set; }
            public RMSDate LastInterestDate { get; private set; }
            public RMSDate LastContactDate { get; private set; }
            public RMSZoneDecimal CommissionRate { get; private set; }
            public RMSString Filler1 { get; private set; }
            public RMSString CourtCaseNumber { get; private set; }
            public RMSString TaxNumberExtension { get; private set; }
            public RMSString Filler2 { get; private set; }
            public RMSString Filler3 { get; private set; }
            public RMSString Filler4 { get; private set; }
            #endregion

            public DebtorRecord(string Record) : base(Record)
            {
                try
                {
                    this.LoanTypeCode = new RMSString(4) { DataString = Record.Substring(278) };
                    this.LendingOfficerCode = new RMSString(6) { DataString = Record.Substring(282) };
                    this.BankCode = new RMSString(3) { DataString = Record.Substring(288) };
                    this.BranchCode = new RMSString(3) { DataString = Record.Substring(291) };
                    this.CompCallCode = new RMSString(4) { DataString = Record.Substring(294) };
                    this.AgencyCode = new RMSString(4) { DataString = Record.Substring(298) };
                    this.DealerCode = new RMSString(5) { DataString = Record.Substring(302) };
                    this.ChargeoffReason = new RMSString(4) { DataString = Record.Substring(307) };
                    this.AccountStatus = new RMSString(3) { DataString = Record.Substring(311) };
                    this.InterestRate = new RMSZoneDecimal(4, 2) { DataString = Record.Substring(314) };
                    this.BalanceFieldType = new RMSEnum<SourceCodes, SourceCodeValues>(1) { DataString = Record.Substring(318) };
                    this.DateReceived = new RMSDate() { DataString = Record.Substring(319) };
                    this.ContractDate = new RMSDate() { DataString = Record.Substring(327) };
                    this.ChargeoffDate = new RMSDate() { DataString = Record.Substring(335) };
                    this.LastPaymentDate = new RMSDate() { DataString = Record.Substring(343) };
                    this.ChargeoffAmount = new RMSDecimal(11, 2) { DataString = Record.Substring(351) };
                    this.AssociatedCosts = new RMSDecimal(10, 2) { DataString = Record.Substring(362) };
                    this.AccruedInterest = new RMSDecimal(10, 2) { DataString = Record.Substring(372) };
                    this.Balance = new RMSDecimal(10, 2) { DataString = Record.Substring(382) };
                    this.LendingLevelData1 = new RMSString(4) { DataString = Record.Substring(392) };
                    this.LendingLevelData2 = new RMSString(4) { DataString = Record.Substring(396) };
                    this.LendingLevelData3 = new RMSString(4) { DataString = Record.Substring(400) };
                    this.LendingLevelData4 = new RMSString(4) { DataString = Record.Substring(404) };
                    this.LendingLevelData5 = new RMSString(4) { DataString = Record.Substring(408) };
                    this.LendingLevelData6 = new RMSString(4) { DataString = Record.Substring(412) };
                    this.LendingLevelData7 = new RMSString(4) { DataString = Record.Substring(416) };
                    this.LendingLevelData8 = new RMSString(4) { DataString = Record.Substring(420) };
                    this.LastCommentLine1 = new RMSString(40) { DataString = Record.Substring(424) };
                    this.LastCommentLine2 = new RMSString(40) { DataString = Record.Substring(464) };
                    this.LastCommentLine3 = new RMSString(40) { DataString = Record.Substring(504) };
                    this.LastCommentDate = new RMSDate() { DataString = Record.Substring(544) };
                    this.SecondLastName = new RMSString(25) { DataString = Record.Substring(552) };
                    this.SecondFirstName = new RMSString(15) { DataString = Record.Substring(577) };
                    this.MonthlyIncome = new RMSDecimal(9, 2) { DataString = Record.Substring(592) };
                    this.OtherIncome = new RMSDecimal(9, 2) { DataString = Record.Substring(601) };
                    this.MonthlyPayment = new RMSDecimal(9, 2) { DataString = Record.Substring(610) };
                    this.OtherObligations = new RMSDecimal(9, 2) { DataString = Record.Substring(619) };
                    this.OwnRentCode = new RMSEnum<OwnRent, OwnRentValues>(1) { DataString = Record.Substring(628) };
                    this.RecoveryScore = new RMSNumber(4) { DataString = Record.Substring(629) };
                    this.NextPaymentDate = new RMSDate() { DataString = Record.Substring(633) };
                    this.LastInterestDate = new RMSDate() { DataString = Record.Substring(641) };
                    this.LastContactDate = new RMSDate() { DataString = Record.Substring(649) };
                    this.CommissionRate = new RMSZoneDecimal(4, 2) { DataString = Record.Substring(657) };
                    this.Filler1 = new RMSString(3) { DataString = Record.Substring(661) };
                    this.CourtCaseNumber = new RMSString(20) { DataString = Record.Substring(664) };
                    this.TaxNumberExtension = new RMSString(6) { DataString = Record.Substring(684) };
                    this.Filler2 = new RMSString(1) { DataString = Record.Substring(690) };
                    this.Filler3 = new RMSString(4) { DataString = Record.Substring(691) };
                    this.Filler4 = new RMSString(4) { DataString = Record.Substring(695) };
                }
                catch
                {
                    if (this.LoanTypeCode == null) this.LoanTypeCode = new RMSString(4);
                    if (this.LendingOfficerCode == null) this.LendingOfficerCode = new RMSString(6);
                    if (this.BankCode == null) this.BankCode = new RMSString(3);
                    if (this.BranchCode == null) this.BranchCode = new RMSString(3);
                    if (this.CompCallCode == null) this.CompCallCode = new RMSString(4);
                    if (this.AgencyCode == null) this.AgencyCode = new RMSString(4);
                    if (this.DealerCode == null) this.DealerCode = new RMSString(5);
                    if (this.ChargeoffReason == null) this.ChargeoffReason = new RMSString(4);
                    if (this.AccountStatus == null) this.AccountStatus = new RMSString(3);
                    if (this.InterestRate == null) this.InterestRate = new RMSZoneDecimal(4, 2);
                    if (this.BalanceFieldType == null) this.BalanceFieldType = new RMSEnum<SourceCodes, SourceCodeValues>(1);
                    if (this.DateReceived == null) this.DateReceived = new RMSDate();
                    if (this.ContractDate == null) this.ContractDate = new RMSDate();
                    if (this.ChargeoffDate == null) this.ChargeoffDate = new RMSDate();
                    if (this.LastPaymentDate == null) this.LastPaymentDate = new RMSDate();
                    if (this.ChargeoffAmount == null) this.ChargeoffAmount = new RMSDecimal(11, 2);
                    if (this.AssociatedCosts == null) this.AssociatedCosts = new RMSDecimal(10, 2);
                    if (this.AccruedInterest == null) this.AccruedInterest = new RMSDecimal(10, 2);
                    if (this.Balance == null) this.Balance = new RMSDecimal(10, 2);
                    if (this.LendingLevelData1 == null) this.LendingLevelData1 = new RMSString(4);
                    if (this.LendingLevelData2 == null) this.LendingLevelData2 = new RMSString(4);
                    if (this.LendingLevelData3 == null) this.LendingLevelData3 = new RMSString(4);
                    if (this.LendingLevelData4 == null) this.LendingLevelData4 = new RMSString(4);
                    if (this.LendingLevelData5 == null) this.LendingLevelData5 = new RMSString(4);
                    if (this.LendingLevelData6 == null) this.LendingLevelData6 = new RMSString(4);
                    if (this.LendingLevelData7 == null) this.LendingLevelData7 = new RMSString(4);
                    if (this.LendingLevelData8 == null) this.LendingLevelData8 = new RMSString(4);
                    if (this.LastCommentLine1 == null) this.LastCommentLine1 = new RMSString(40);
                    if (this.LastCommentLine2 == null) this.LastCommentLine2 = new RMSString(40);
                    if (this.LastCommentLine3 == null) this.LastCommentLine3 = new RMSString(40);
                    if (this.LastCommentDate == null) this.LastCommentDate = new RMSDate();
                    if (this.SecondLastName == null) this.SecondLastName = new RMSString(25);
                    if (this.SecondFirstName == null) this.SecondFirstName = new RMSString(15);
                    if (this.MonthlyIncome == null) this.MonthlyIncome = new RMSDecimal(9, 2);
                    if (this.OtherIncome == null) this.OtherIncome = new RMSDecimal(9, 2);
                    if (this.MonthlyPayment == null) this.MonthlyPayment = new RMSDecimal(9, 2);
                    if (this.OtherObligations == null) this.OtherObligations = new RMSDecimal(9, 2);
                    if (this.OwnRentCode == null) this.OwnRentCode = new RMSEnum<OwnRent, OwnRentValues>(1);
                    if (this.RecoveryScore == null) this.RecoveryScore = new RMSNumber(4);
                    if (this.NextPaymentDate == null) this.NextPaymentDate = new RMSDate();
                    if (this.LastInterestDate == null) this.LastInterestDate = new RMSDate();
                    if (this.LastContactDate == null) this.LastContactDate = new RMSDate();
                    if (this.CommissionRate == null) this.CommissionRate = new RMSZoneDecimal(4, 2);
                    if (this.Filler1 == null) this.Filler1 = new RMSString(3);
                    if (this.CourtCaseNumber == null) this.CourtCaseNumber = new RMSString(20);
                    if (this.TaxNumberExtension == null) this.TaxNumberExtension = new RMSString(6);
                    if (this.Filler2 == null) this.Filler2 = new RMSString(1);
                    if (this.Filler3 == null) this.Filler3 = new RMSString(4);
                    if (this.Filler4 == null) this.Filler4 = new RMSString(4);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}",
                    this.AccountNumber,
                    this.RecordType,
                    this.SequenceNumber,
                    this.TaxNumber,
                    this.CustomerType,
                    this.LastName,
                    this.FirstName,
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
                    this.Employer_Address,
                    this.LoanTypeCode,
                    this.LendingOfficerCode,
                    this.BankCode,
                    this.BranchCode,
                    this.CompCallCode,
                    this.AgencyCode,
                    this.DealerCode,
                    this.ChargeoffReason,
                    this.AccountStatus,
                    this.InterestRate,
                    this.BalanceFieldType,
                    this.DateReceived,
                    this.ContractDate,
                    this.ChargeoffDate,
                    this.LastPaymentDate,
                    this.ChargeoffAmount,
                    this.AssociatedCosts,
                    this.AccruedInterest,
                    this.Balance,
                    this.LendingLevelData1,
                    this.LendingLevelData2,
                    this.LendingLevelData3,
                    this.LendingLevelData4,
                    this.LendingLevelData5,
                    this.LendingLevelData6,
                    this.LendingLevelData7,
                    this.LendingLevelData8,
                    this.LastCommentLine1,
                    this.LastCommentLine2,
                    this.LastCommentLine3,
                    this.LastCommentDate,
                    this.SecondLastName,
                    this.SecondFirstName,
                    this.MonthlyIncome,
                    this.OtherIncome,
                    this.MonthlyPayment,
                    this.OtherObligations,
                    this.OwnRentCode,
                    this.RecoveryScore,
                    this.NextPaymentDate,
                    this.LastInterestDate,
                    this.LastContactDate,
                    this.CommissionRate,
                    this.Filler1,
                    this.CourtCaseNumber,
                    this.TaxNumberExtension,
                    this.Filler2,
                    this.Filler3,
                    this.Filler4,
                    this.FormatCode);
            }
        }

        public class CoMakerRecord : AccountRecord
        {
            #region Public Properties
            public RMSString Filler1 { get; private set; }
            public RMSString Filler2 { get; private set; }
            public RMSString Filler3 { get; private set; }
            public RMSString Filler4 { get; private set; }
            public RMSString Filler5 { get; private set; }
            public RMSString Filler6 { get; private set; }
            public RMSString Filler7 { get; private set; }
            public RMSString Filler8 { get; private set; }
            public RMSString Filler9 { get; private set; }
            public RMSString Filler10 { get; private set; }
            public RMSString Filler11 { get; private set; }
            public RMSString Filler12 { get; private set; }
            public RMSString Filler13 { get; private set; }
            #endregion

            public CoMakerRecord(string Record) : base(Record)
            {
                try
                {
                    this.Filler1 = new RMSString(36) { DataString = Record.Substring(278) };
                    this.Filler2 = new RMSString(4) { DataString = Record.Substring(314) };
                    this.Filler3 = new RMSString(33) { DataString = Record.Substring(318) };
                    this.Filler4 = new RMSString(41) { DataString = Record.Substring(351) };
                    this.Filler5 = new RMSString(200) { DataString = Record.Substring(392) };
                    this.Filler6 = new RMSString(9) { DataString = Record.Substring(592) };
                    this.Filler7 = new RMSString(9) { DataString = Record.Substring(601) };
                    this.Filler8 = new RMSString(9) { DataString = Record.Substring(610) };
                    this.Filler9 = new RMSString(9) { DataString = Record.Substring(619) };
                    this.Filler10 = new RMSString(1) { DataString = Record.Substring(628) };
                    this.Filler11 = new RMSString(28) { DataString = Record.Substring(629) };
                    this.Filler12 = new RMSString(4) { DataString = Record.Substring(657) };
                    this.Filler13 = new RMSString(38) { DataString = Record.Substring(661) };
                }
                catch
                {
                    if (this == null) Filler1 = new RMSString(36);
                    if (this == null) Filler2 = new RMSString(4);
                    if (this == null) Filler3 = new RMSString(33);
                    if (this == null) Filler4 = new RMSString(41);
                    if (this == null) Filler5 = new RMSString(200);
                    if (this == null) Filler6 = new RMSString(9);
                    if (this == null) Filler7 = new RMSString(9);
                    if (this == null) Filler8 = new RMSString(9);
                    if (this == null) Filler9 = new RMSString(9);
                    if (this == null) Filler10 = new RMSString(1);
                    if (this == null) Filler11 = new RMSString(28);
                    if (this == null) Filler12 = new RMSString(4);
                    if (this == null) Filler13 = new RMSString(38);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}",
                    this.AccountNumber,
                    this.RecordType,
                    this.SequenceNumber,
                    this.TaxNumber,
                    this.CustomerType,
                    this.LastName,
                    this.FirstName,
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
                    this.Employer_Address,
                    this.Filler1,
                    this.Filler2,
                    this.Filler3,
                    this.Filler4,
                    this.Filler5,
                    this.Filler6,
                    this.Filler7,
                    this.Filler8,
                    this.Filler9,
                    this.Filler10,
                    this.Filler11,
                    this.Filler12,
                    this.Filler13,
                    this.FormatCode);
            }
        }
    }
}
