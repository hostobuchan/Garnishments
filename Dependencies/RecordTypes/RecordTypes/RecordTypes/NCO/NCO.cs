using RecordTypes.NCO.Base;
using RecordTypes.NCO.DataTypes;
using RecordTypes.NCO.Enums;
using System;

namespace RecordTypes
{
    namespace NCO
    {
        /// <summary>
        /// Header Information
        /// <para>The Header Record reports information about the makeup of the transfer file and is always the first record in the file. There is one Header Record per file.</para>
        /// </summary>
        public class RecordType00 : Record
        {
            public NCODateTime CreateDateTime { get; private set; }
            public NCOString NCOID { get; private set; }
            public NCOString ReceiverID { get; private set; }

            public RecordType00() : base(Enums.RecordTypes.RecordType00)
            {
                this.CreateDateTime = new NCODateTime();
                this.NCOID = new NCOString(10);
                this.ReceiverID = new NCOString(8);
            }
            public RecordType00(string Record) : base(Record)
            {
                try
                {
                    this.CreateDateTime = new NCODateTime() { DataString = Record.Substring(2, 16) };
                    this.NCOID = new NCOString(10) { DataString = Record.Substring(18, 10) };
                    this.ReceiverID = new NCOString(8) { DataString = Record.Substring(28, 8) };
                }
                catch
                {
                    if (this.CreateDateTime == null) this.CreateDateTime = new NCODateTime();
                    if (this.NCOID == null) this.NCOID = new NCOString(10);
                    if (this.ReceiverID == null) this.ReceiverID = new NCOString(8);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}", this.RecordType, this.CreateDateTime, this.NCOID, this.ReceiverID);
            }
        }
        /// <summary>
        /// Account Related Data
        /// <para>The Account Related Data contains detailed information about the debt, which includes the amounts of all outstanding balances and the current account status (with the agency and credit bureaus). This record should always be accompanied by a Consumer Demographic (02) Record.</para>
        /// </summary>
        public class RecordType01 : RecordBase
        {
            #region Public Properties
            public NCOString CreditorGroup { get; private set; }
            public NCOString Creditor { get; private set; }
            public NCOString CreditorAccountNumber { get; private set; }
            public NCOString CreditorReferenceNumber { get; private set; }
            public NCOString OriginalCreditor { get; private set; }
            public NCOString OriginalCreditorAcct { get; private set; }
            public NCOString FormerCreditor { get; private set; }
            public NCOString FormerCreditorAcct { get; private set; }
            public NCODate PlacementDate { get; private set; }
            public NCODecimal TotalBalance { get; private set; }
            public NCODecimal PrincipalBalance { get; private set; }
            public NCODecimal InterestBalance { get; private set; }
            public NCODecimal CourtCostBalance { get; private set; }
            public NCODecimal AttorneyFeesBalance { get; private set; }
            public NCODecimal OtherFeesBalance { get; private set; }
            public NCODecimal NSFFeesBalance { get; private set; }
            public NCODecimal CollectionChargeBalance { get; private set; }
            public NCODecimal BalanceBucket8 { get; private set; }
            public NCODecimal BalanceBucket9 { get; private set; }
            public NCODecimal BalanceBucket10 { get; private set; }
            public NCOString DescriptionField { get; private set; }
            public NCODate SOLDate { get; private set; }
            public NCODate LastServiceDate { get; private set; }
            public NCOEnum<InterestCode> InterestCode { get; private set; }
            public NCODecimal InterestRate { get; private set; }
            public NCODate LastInterestCalculatedDate { get; private set; }
            public NCODecimal CommissionFeePercentage { get; private set; }
            public NCODecimal SuitFee { get; private set; }
            public NCOString ClaimType { get; private set; }
            public NCOString BatchNumber { get; private set; }
            public NCODate FirstDelinquencyDate { get; private set; }
            public NCODate LastPaymentDate_NCO { get; private set; }
            public NCODecimal LastPaymentAmount_NCO { get; private set; }
            public NCODate LastStatementDate { get; private set; }
            public NCODate ChargeOffDate { get; private set; }
            public NCODate AccountOpenedDate { get; private set; }
            public NCOString DealerName { get; private set; }
            public NCOString DescriptionOfMerchandise { get; private set; }
            public NCODate LastPurchaseDate { get; private set; }
            public NCOString LossCode { get; private set; }
            public NCONumber MonthsPastDue { get; private set; }
            public NCODate LastPaymentDate_Client { get; private set; }
            public NCODecimal LastPaymentAmount_Client { get; private set; }
            public NCODate BillingDate { get; private set; }
            public NCONumber BillingCycle { get; private set; }
            public NCODecimal ChargeOffAmount { get; private set; }
            public NCODecimal ChargeOffPrincipal { get; private set; }
            public NCODecimal ChargeOffInterest { get; private set; }
            public NCODecimal ChargeOffOther { get; private set; }
            public NCODate ReceiptDate { get; private set; }
            public NCODecimal LastPurchaseAmount { get; private set; }
            public NCODate NextPaymentDate { get; private set; }
            public NCONumber DaysDelinquent { get; private set; }
            public NCODecimal PrincipalPaid { get; private set; }
            public NCODecimal TotalPastDue { get; private set; }
            public NCODate PromissoryNoteDate { get; private set; }
            public NCODate SeparationDate { get; private set; }
            public NCODate MaturityDate { get; private set; }
            public NCODate LoadDefaultDate { get; private set; }
            public NCODecimal MinimumAmountDue { get; private set; }
            public NCOString ANINumber1 { get; private set; }
            public NCOString ANINumber2 { get; private set; }
            public NCOString ANINumber3 { get; private set; }
            public NCOString ANINumber4 { get; private set; }
            public NCOString ANINumber5 { get; private set; }
            public NCOString ANINumber6 { get; private set; }
            public NCOString ANINumber7 { get; private set; }
            public NCOString ANINumber8 { get; private set; }
            public NCOString ANINumber9 { get; private set; }
            public NCOString ANINumber10 { get; private set; }
            public NCOString Auto_VIN { get; private set; }
            public NCOString Auto_Make { get; private set; }
            public NCOString Auto_Model { get; private set; }
            public NCOString Auto_Year { get; private set; }
            public NCOString LawList { get; private set; }
            public NCOString LegalEntity1 { get; private set; }
            public NCOString LegalEntity2 { get; private set; }
            public NCOString LegalEntity3 { get; private set; }
            public NCODecimal EarlyOutBucket1 { get; private set; }
            public NCODecimal EarlyOutBucket2 { get; private set; }
            public NCODecimal EarlyOutBucket3 { get; private set; }
            public NCODecimal EarlyOutBucket4 { get; private set; }
            public NCODecimal EarlyOutBucket5 { get; private set; }
            public NCODecimal EarlyOutBucket6 { get; private set; }
            public NCODecimal EarlyOutBucket7 { get; private set; }
            public NCODecimal EarlyOutBucket8 { get; private set; }
            public NCODecimal EarlyOutBucket9 { get; private set; }
            public NCODecimal EarlyOutBucket10 { get; private set; }
            public NCOString SuitCaption { get; private set; }
            public NCOString SpecialInstructions { get; private set; }
            public NCOEnum<PaymentAllocation> PaymentAllocation { get; private set; }
            public NCOString Comments { get; private set; }
            #endregion

            public RecordType01(string Record) : base(Record)
            {
                #region Instantiate Properties
                this.CreditorGroup = new NCOString(100);
                this.Creditor = new NCOString(8);
                this.CreditorAccountNumber = new NCOString(40);
                this.CreditorReferenceNumber = new NCOString(40);
                this.OriginalCreditor = new NCOString(100);
                this.OriginalCreditorAcct = new NCOString(40);
                this.FormerCreditor = new NCOString(100);
                this.FormerCreditorAcct = new NCOString(40);
                this.PlacementDate = new NCODate();
                this.TotalBalance = new NCODecimal(12, 2);
                this.PrincipalBalance = new NCODecimal(12, 2);
                this.InterestBalance = new NCODecimal(12, 2);
                this.CourtCostBalance = new NCODecimal(12, 2);
                this.AttorneyFeesBalance = new NCODecimal(12, 2);
                this.OtherFeesBalance = new NCODecimal(12, 2);
                this.NSFFeesBalance = new NCODecimal(12, 2);
                this.CollectionChargeBalance = new NCODecimal(12, 2);
                this.BalanceBucket8 = new NCODecimal(12, 2);
                this.BalanceBucket9 = new NCODecimal(12, 2);
                this.BalanceBucket10 = new NCODecimal(12, 2);
                this.DescriptionField = new NCOString(60);
                this.SOLDate = new NCODate();
                this.LastServiceDate = new NCODate();
                this.InterestCode = new NCOEnum<InterestCode>(1);
                this.InterestRate = new NCODecimal(8, 4);
                this.LastInterestCalculatedDate = new NCODate();
                this.CommissionFeePercentage = new NCODecimal(8, 4);
                this.SuitFee = new NCODecimal(8, 4);
                this.ClaimType = new NCOString(20);
                this.BatchNumber = new NCOString(10);
                this.FirstDelinquencyDate = new NCODate();
                this.LastPaymentDate_NCO = new NCODate();
                this.LastPaymentAmount_NCO = new NCODecimal(12, 2);
                this.LastStatementDate = new NCODate();
                this.ChargeOffDate = new NCODate();
                this.AccountOpenedDate = new NCODate();
                this.DealerName = new NCOString(40);
                this.DescriptionOfMerchandise = new NCOString(40);
                this.LastPurchaseDate = new NCODate();
                this.LossCode = new NCOString(5);
                this.MonthsPastDue = new NCONumber(2);
                this.LastPaymentDate_Client = new NCODate();
                this.LastPaymentAmount_Client = new NCODecimal(12, 2);
                this.BillingDate = new NCODate();
                this.BillingCycle = new NCONumber(3);
                this.ChargeOffAmount = new NCODecimal(12, 2);
                this.ChargeOffPrincipal = new NCODecimal(12, 2);
                this.ChargeOffInterest = new NCODecimal(12, 2);
                this.ChargeOffOther = new NCODecimal(12, 2);
                this.ReceiptDate = new NCODate();
                this.LastPurchaseAmount = new NCODecimal(12, 2);
                this.NextPaymentDate = new NCODate();
                this.DaysDelinquent = new NCONumber(5);
                this.PrincipalPaid = new NCODecimal(12, 2);
                this.TotalPastDue = new NCODecimal(12, 2);
                this.PromissoryNoteDate = new NCODate();
                this.SeparationDate = new NCODate();
                this.MaturityDate = new NCODate();
                this.LoadDefaultDate = new NCODate();
                this.MinimumAmountDue = new NCODecimal(12, 2);
                this.ANINumber1 = new NCOString(10);
                this.ANINumber2 = new NCOString(10);
                this.ANINumber3 = new NCOString(10);
                this.ANINumber4 = new NCOString(10);
                this.ANINumber5 = new NCOString(10);
                this.ANINumber6 = new NCOString(10);
                this.ANINumber7 = new NCOString(10);
                this.ANINumber8 = new NCOString(10);
                this.ANINumber9 = new NCOString(10);
                this.ANINumber10 = new NCOString(10);
                this.Auto_VIN = new NCOString(20);
                this.Auto_Make = new NCOString(25);
                this.Auto_Model = new NCOString(25);
                this.Auto_Year = new NCOString(4);
                this.LawList = new NCOString(4);
                this.LegalEntity1 = new NCOString(100);
                this.LegalEntity2 = new NCOString(100);
                this.LegalEntity3 = new NCOString(100);
                this.EarlyOutBucket1 = new NCODecimal(12, 2);
                this.EarlyOutBucket2 = new NCODecimal(12, 2);
                this.EarlyOutBucket3 = new NCODecimal(12, 2);
                this.EarlyOutBucket4 = new NCODecimal(12, 2);
                this.EarlyOutBucket5 = new NCODecimal(12, 2);
                this.EarlyOutBucket6 = new NCODecimal(12, 2);
                this.EarlyOutBucket7 = new NCODecimal(12, 2);
                this.EarlyOutBucket8 = new NCODecimal(12, 2);
                this.EarlyOutBucket9 = new NCODecimal(12, 2);
                this.EarlyOutBucket10 = new NCODecimal(12, 2);
                this.SuitCaption = new NCOString(150);
                this.SpecialInstructions = new NCOString(200);
                this.PaymentAllocation = new NCOEnum<PaymentAllocation>(10);
                this.Comments = new NCOString(200);
                #endregion

                #region Assign Values
                try
                {
                    this.CreditorGroup.DataString = Record.Substring(22, 100);
                    this.Creditor.DataString = Record.Substring(122, 8);
                    this.CreditorAccountNumber.DataString = Record.Substring(130, 40);
                    this.CreditorReferenceNumber.DataString = Record.Substring(170, 40);
                    this.OriginalCreditor.DataString = Record.Substring(210, 100);
                    this.OriginalCreditorAcct.DataString = Record.Substring(310, 40);
                    this.FormerCreditor.DataString = Record.Substring(350, 100);
                    this.FormerCreditorAcct.DataString = Record.Substring(450, 40);
                    this.PlacementDate.DataString = Record.Substring(490, 8);
                    this.TotalBalance.DataString = Record.Substring(498, 12);
                    this.PrincipalBalance.DataString = Record.Substring(510, 12);
                    this.InterestBalance.DataString = Record.Substring(522, 12);
                    this.CourtCostBalance.DataString = Record.Substring(534, 12);
                    this.AttorneyFeesBalance.DataString = Record.Substring(546, 12);
                    this.OtherFeesBalance.DataString = Record.Substring(558, 12);
                    this.NSFFeesBalance.DataString = Record.Substring(570, 12);
                    this.CollectionChargeBalance.DataString = Record.Substring(582, 12);
                    this.BalanceBucket8.DataString = Record.Substring(594, 12);
                    this.BalanceBucket9.DataString = Record.Substring(606, 12);
                    this.BalanceBucket10.DataString = Record.Substring(618, 12);
                    this.DescriptionField.DataString = Record.Substring(630, 60);
                    this.SOLDate.DataString = Record.Substring(690, 8);
                    this.LastServiceDate.DataString = Record.Substring(698, 8);
                    this.InterestCode.DataString = Record.Substring(706, 1);
                    this.InterestRate.DataString = Record.Substring(707, 8);
                    this.LastInterestCalculatedDate.DataString = Record.Substring(715, 8);
                    this.CommissionFeePercentage.DataString = Record.Substring(723, 8);
                    this.SuitFee.DataString = Record.Substring(731, 8);
                    this.ClaimType.DataString = Record.Substring(739, 20);
                    this.BatchNumber.DataString = Record.Substring(759, 10);
                    this.FirstDelinquencyDate.DataString = Record.Substring(769, 8);
                    this.LastPaymentDate_NCO.DataString = Record.Substring(777, 8);
                    this.LastPaymentAmount_NCO.DataString = Record.Substring(785, 12);
                    this.LastStatementDate.DataString = Record.Substring(797, 8);
                    this.ChargeOffDate.DataString = Record.Substring(805, 8);
                    this.AccountOpenedDate.DataString = Record.Substring(813, 8);
                    this.DealerName.DataString = Record.Substring(821, 40);
                    this.DescriptionOfMerchandise.DataString = Record.Substring(861, 40);
                    this.LastPurchaseDate.DataString = Record.Substring(901, 8);
                    this.LossCode.DataString = Record.Substring(909, 5);
                    this.MonthsPastDue.DataString = Record.Substring(914, 2);
                    this.LastPaymentDate_Client.DataString = Record.Substring(916, 8);
                    this.LastPaymentAmount_Client.DataString = Record.Substring(924, 12);
                    this.BillingDate.DataString = Record.Substring(936, 8);
                    this.BillingCycle.DataString = Record.Substring(944, 3);
                    this.ChargeOffAmount.DataString = Record.Substring(947, 12);
                    this.ChargeOffPrincipal.DataString = Record.Substring(959, 12);
                    this.ChargeOffInterest.DataString = Record.Substring(971, 12);
                    this.ChargeOffOther.DataString = Record.Substring(983, 12);
                    this.ReceiptDate.DataString = Record.Substring(995, 8);
                    this.LastPurchaseAmount.DataString = Record.Substring(1003, 12);
                    this.NextPaymentDate.DataString = Record.Substring(1015, 8);
                    this.DaysDelinquent.DataString = Record.Substring(1023, 5);
                    this.PrincipalPaid.DataString = Record.Substring(1028, 12);
                    this.TotalPastDue.DataString = Record.Substring(1040, 12);
                    this.PromissoryNoteDate.DataString = Record.Substring(1052, 8);
                    this.SeparationDate.DataString = Record.Substring(1060, 8);
                    this.MaturityDate.DataString = Record.Substring(1068, 8);
                    this.LoadDefaultDate.DataString = Record.Substring(1076, 8);
                    this.MinimumAmountDue.DataString = Record.Substring(1084, 12);
                    this.ANINumber1.DataString = Record.Substring(1096, 10);
                    this.ANINumber2.DataString = Record.Substring(1106, 10);
                    this.ANINumber3.DataString = Record.Substring(1116, 10);
                    this.ANINumber4.DataString = Record.Substring(1126, 10);
                    this.ANINumber5.DataString = Record.Substring(1136, 10);
                    this.ANINumber6.DataString = Record.Substring(1146, 10);
                    this.ANINumber7.DataString = Record.Substring(1156, 10);
                    this.ANINumber8.DataString = Record.Substring(1166, 10);
                    this.ANINumber9.DataString = Record.Substring(1176, 10);
                    this.ANINumber10.DataString = Record.Substring(1186, 10);
                    this.Auto_VIN.DataString = Record.Substring(1196, 20);
                    this.Auto_Make.DataString = Record.Substring(1216, 25);
                    this.Auto_Model.DataString = Record.Substring(1241, 25);
                    this.Auto_Year.DataString = Record.Substring(1266, 4);
                    this.LawList.DataString = Record.Substring(1270, 4);
                    this.LegalEntity1.DataString = Record.Substring(1274, 100);
                    this.LegalEntity2.DataString = Record.Substring(1374, 100);
                    this.LegalEntity3.DataString = Record.Substring(1474, 100);
                    this.EarlyOutBucket1.DataString = Record.Substring(1574, 12);
                    this.EarlyOutBucket2.DataString = Record.Substring(1586, 12);
                    this.EarlyOutBucket3.DataString = Record.Substring(1598, 12);
                    this.EarlyOutBucket4.DataString = Record.Substring(1610, 12);
                    this.EarlyOutBucket5.DataString = Record.Substring(1622, 12);
                    this.EarlyOutBucket6.DataString = Record.Substring(1634, 12);
                    this.EarlyOutBucket7.DataString = Record.Substring(1646, 12);
                    this.EarlyOutBucket8.DataString = Record.Substring(1658, 12);
                    this.EarlyOutBucket9.DataString = Record.Substring(1670, 12);
                    this.EarlyOutBucket10.DataString = Record.Substring(1682, 12);
                    this.SuitCaption.DataString = Record.Substring(1694, 150);
                    this.SpecialInstructions.DataString = Record.Substring(1844, 200);
                    this.PaymentAllocation.DataString = Record.Substring(2044, 10);
                    this.Comments.DataString = Record.Substring(2054, 200);
                }
                catch { }
                #endregion
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}{68}{69}{70}{71}{72}{73}{74}{75}{76}{77}{78}{79}{80}{81}{82}{83}{84}{85}{86}{87}{88}{89}{90}{91}{92}{93}",
                    this.RecordType,
                    this.AccountNumber,
                    this.CreditorGroup,
                    this.Creditor,
                    this.CreditorAccountNumber,
                    this.CreditorReferenceNumber,
                    this.OriginalCreditor,
                    this.OriginalCreditorAcct,
                    this.FormerCreditor,
                    this.FormerCreditorAcct,
                    this.PlacementDate,
                    this.TotalBalance,
                    this.PrincipalBalance,
                    this.InterestBalance,
                    this.CourtCostBalance,
                    this.AttorneyFeesBalance,
                    this.OtherFeesBalance,
                    this.NSFFeesBalance,
                    this.CollectionChargeBalance,
                    this.BalanceBucket8,
                    this.BalanceBucket9,
                    this.BalanceBucket10,
                    this.DescriptionField,
                    this.SOLDate,
                    this.LastServiceDate,
                    this.InterestCode,
                    this.InterestRate,
                    this.LastInterestCalculatedDate,
                    this.CommissionFeePercentage,
                    this.SuitFee,
                    this.ClaimType,
                    this.BatchNumber,
                    this.FirstDelinquencyDate,
                    this.LastPaymentDate_NCO,
                    this.LastPaymentAmount_NCO,
                    this.LastStatementDate,
                    this.ChargeOffDate,
                    this.AccountOpenedDate,
                    this.DealerName,
                    this.DescriptionOfMerchandise,
                    this.LastPurchaseDate,
                    this.LossCode,
                    this.MonthsPastDue,
                    this.LastPaymentDate_Client,
                    this.LastPaymentAmount_Client,
                    this.BillingDate,
                    this.BillingCycle,
                    this.ChargeOffAmount,
                    this.ChargeOffPrincipal,
                    this.ChargeOffInterest,
                    this.ChargeOffOther,
                    this.ReceiptDate,
                    this.LastPurchaseAmount,
                    this.NextPaymentDate,
                    this.DaysDelinquent,
                    this.PrincipalPaid,
                    this.TotalPastDue,
                    this.PromissoryNoteDate,
                    this.SeparationDate,
                    this.MaturityDate,
                    this.LoadDefaultDate,
                    this.MinimumAmountDue,
                    this.ANINumber1,
                    this.ANINumber2,
                    this.ANINumber3,
                    this.ANINumber4,
                    this.ANINumber5,
                    this.ANINumber6,
                    this.ANINumber7,
                    this.ANINumber8,
                    this.ANINumber9,
                    this.ANINumber10,
                    this.Auto_VIN,
                    this.Auto_Make,
                    this.Auto_Model,
                    this.Auto_Year,
                    this.LawList,
                    this.LegalEntity1,
                    this.LegalEntity2,
                    this.LegalEntity3,
                    this.EarlyOutBucket1,
                    this.EarlyOutBucket2,
                    this.EarlyOutBucket3,
                    this.EarlyOutBucket4,
                    this.EarlyOutBucket5,
                    this.EarlyOutBucket6,
                    this.EarlyOutBucket7,
                    this.EarlyOutBucket8,
                    this.EarlyOutBucket9,
                    this.EarlyOutBucket10,
                    this.SuitCaption,
                    this.SpecialInstructions,
                    this.PaymentAllocation,
                    this.Comments);
            }
        }
        /// <summary>
        /// Consumer Demographic
        /// <para>The Consumer Demographic Record reports information pertinent to the person or entity responsible for the debt. This record should always come with an Account Related Data (01) Record.</para>
        /// </summary>
        public class RecordType02 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Unique identifier to the Consumer (used to tie merged accounts together)
            /// </summary>
            public NCOString ConsumerNumber { get; private set; }
            /// <summary>
            /// Last name of responsible party
            /// </summary>
            public NCOString LastName { get; private set; }
            /// <summary>
            /// First name of responsible party
            /// </summary>
            public NCOString FirstName { get; private set; }
            /// <summary>
            /// Middle name of responsible party
            /// </summary>
            public NCOString MiddleName { get; private set; }
            /// <summary>
            /// Prefix of responsible party
            /// </summary>
            public NCOString Prefix { get; private set; }
            /// <summary>
            /// Suffix of responsible party
            /// </summary>
            public NCOString Suffix { get; private set; }
            /// <summary>
            /// AKA of responsible party if available
            /// </summary>
            public NCOString ConsumerAlias { get; private set; }
            /// <summary>
            /// Street address of responsible party
            /// </summary>
            public NCOString StreetAddress1 { get; private set; }
            /// <summary>
            /// Secondary address of responsible party
            /// </summary>
            public NCOString StreetAddress2 { get; private set; }
            /// <summary>
            /// Third address of responsible party
            /// </summary>
            public NCOString StreetAddress3 { get; private set; }
            /// <summary>
            /// City of responsible party
            /// </summary>
            public NCOString City { get; private set; }
            /// <summary>
            /// Postal abbreviation for the state where responsible party resides
            /// </summary>
            public NCOString State { get; private set; }
            /// <summary>
            /// Standard five-digit or extended nine-digit (no hyphen) zip code, or alphanumeric zip code for foreign addresses
            /// </summary>
            public NCOString Zip { get; private set; }
            /// <summary>
            /// Responsible party’s social security number
            /// </summary>
            public NCOString SSN { get; private set; }
            /// <summary>
            /// Home phone of responsible party
            /// </summary>
            public NCOString Phone_Home { get; private set; }
            /// <summary>
            /// Cell phone of responsible party
            /// </summary>
            public NCOString Phone_Cell { get; private set; }
            /// <summary>
            /// Whether or not it is commercial (N= No, Y = Yes)
            /// </summary>
            public NCOBool CommercialConsumer { get; private set; }
            /// <summary>
            /// Commercial Name
            /// </summary>
            public NCOString CommericalName { get; private set; }
            /// <summary>
            /// Number used to rate the collectability of the responsible party.
            /// </summary>
            public NCOString BehavioralScore { get; private set; }
            /// <summary>
            /// Account/customer score #1
            /// </summary>
            public NCOString ClientScore1 { get; private set; }
            /// <summary>
            /// Account/customer score #2
            /// </summary>
            public NCOString ClientScore2 { get; private set; }
            /// <summary>
            /// Account/customer score #3
            /// </summary>
            public NCOString ClientScore3 { get; private set; }
            /// <summary>
            /// Date of last contact
            /// </summary>
            public NCODate LastContactDate { get; private set; }
            /// <summary>
            /// Date the responsible party was born
            /// </summary>
            public NCODate BirthDate { get; private set; }
            /// <summary>
            /// Email address of responsible party
            /// </summary>
            public NCOString Email { get; private set; }
            /// <summary>
            /// Drivers license number
            /// </summary>
            public NCOString DriversLicense { get; private set; }
            /// <summary>
            /// Consumer County
            /// </summary>
            public NCOString County { get; private set; }
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.AddressStatus for code list
            /// </summary>
            public NCOEnum<AddressStatus> AddressStatus { get; private set; }
            /// <summary>
            /// Consumer place of employment
            /// </summary>
            public NCOString Employer { get; private set; }
            /// <summary>
            /// Street address 1 of consumer employer
            /// </summary>
            public NCOString Employer_StreetAddress1 { get; private set; }
            /// <summary>
            /// Street address 2 of consumer employer
            /// </summary>
            public NCOString Employer_StreetAddress2 { get; private set; }
            /// <summary>
            /// Street address 3 of consumer employer
            /// </summary>
            public NCOString Employer_StreetAddress3 { get; private set; }
            /// <summary>
            /// City of consumer employer
            /// </summary>
            public NCOString Employer_City { get; private set; }
            /// <summary>
            /// State of consumer employer
            /// </summary>
            public NCOString Employer_State { get; private set; }
            /// <summary>
            /// Zip code of consumer employer
            /// </summary>
            public NCOString Employer_Zip { get; private set; }
            /// <summary>
            /// Phone number of consumer employer
            /// </summary>
            public NCOString Employer_Phone { get; private set; }
            /// <summary>
            /// Extension of consumer employer
            /// </summary>
            public NCOString Employer_PhoneExt { get; private set; }
            /// <summary>
            /// The attorney firm representing consumer
            /// </summary>
            public NCOString Attorney_Firm { get; private set; }
            /// <summary>
            /// Last name of attorney representing consumer
            /// </summary>
            public NCOString Attorney_LastName { get; private set; }
            /// <summary>
            /// First name of attorney representing consumer
            /// </summary>
            public NCOString Attorney_FirstName { get; private set; }
            /// <summary>
            /// Street address of attorney representing responsible party
            /// </summary>
            public NCOString Attorney_StreetAddress1 { get; private set; }
            /// <summary>
            /// Secondary address of attorney representing responsible party
            /// </summary>
            public NCOString Attorney_StreetAddress2 { get; private set; }
            /// <summary>
            /// Third address of attorney representing responsible party
            /// </summary>
            public NCOString Attorney_StreetAddress3 { get; private set; }
            /// <summary>
            /// City where attorney representing responsible party resides
            /// </summary>
            public NCOString Attorney_City { get; private set; }
            /// <summary>
            /// Postal abbreviation for the state where attorney representing responsible party resides
            /// </summary>
            public NCOString Attorney_State { get; private set; }
            /// <summary>
            /// Standard five-digit or extended nine-digit (no hyphen) zip code, or alphanumeric zip code for foreign addresses of person representing responsible party
            /// </summary>
            public NCOString Attorney_Zip { get; private set; }
            /// <summary>
            /// Phone number of attorney representing responsible party
            /// </summary>
            public NCOString Attorney_Phone { get; private set; }
            /// <summary>
            /// Fax number of attorney representing responsible party
            /// </summary>
            public NCOString Attorney_Fax { get; private set; }
            /// <summary>
            /// Responsible party attorney file number
            /// </summary>
            public NCOString Attorney_FileNumber { get; private set; }
            /// <summary>
            /// Date responsible party died
            /// </summary>
            public NCODate DeathDate { get; private set; }
            /// <summary>
            /// Bank account number of responsible party
            /// </summary>
            public NCOString Bank_AccountNumber { get; private set; }
            /// <summary>
            /// Name of bank responsible party uses
            /// </summary>
            public NCOString Bank { get; private set; }
            /// <summary>
            /// Street address of bank responsible party uses
            /// </summary>
            public NCOString Bank_StreetAddress1 { get; private set; }
            /// <summary>
            /// Secondary address of bank responsible party uses.
            /// </summary>
            public NCOString Bank_StreetAddress2 { get; private set; }
            /// <summary>
            /// Third address of bank responsible party uses
            /// </summary>
            public NCOString Bank_StreetAddress3 { get; private set; }
            /// <summary>
            /// Bank City
            /// </summary>
            public NCOString Bank_City { get; private set; }
            /// <summary>
            /// Bank State
            /// </summary>
            public NCOString Bank_State { get; private set; }
            /// <summary>
            /// Bank zip code
            /// </summary>
            public NCOString Bank_Zip { get; private set; }
            /// <summary>
            /// Consumers monthly income
            /// </summary>
            public NCODecimal MonthlyIncome { get; private set; }
            /// <summary>
            /// Consumers other income
            /// </summary>
            public NCODecimal OtherIncome { get; private set; }
            /// <summary>
            /// Consumers other obligations
            /// </summary>
            public NCODecimal OtherObligations { get; private set; }
            /// <summary>
            /// Date of the last credit limit adjustment
            /// </summary>
            public NCODate LastCreditChangeDate { get; private set; }
            /// <summary>
            /// Credit limit
            /// </summary>
            public NCODecimal CreditLimit { get; private set; }
            /// <summary>
            /// Miscellaneous Asset #1
            /// </summary>
            public NCOString MiscellaneousAsset1 { get; private set; }
            /// <summary>
            /// Miscellaneous Asset #2
            /// </summary>
            public NCOString MiscellaneousAsset2 { get; private set; }
            /// <summary>
            /// Miscellaneous Asset #3
            /// </summary>
            public NCOString MiscellaneousAsset3 { get; private set; }
            #endregion

            public RecordType02(string Record) : base(Record)
            {
                try
                {
                    this.ConsumerNumber = new NCOString(20) { DataString = Record.Substring(22, 20) };
                    this.LastName = new NCOString(40) { DataString = Record.Substring(42, 40) };
                    this.FirstName = new NCOString(40) { DataString = Record.Substring(82, 40) };
                    this.MiddleName = new NCOString(40) { DataString = Record.Substring(122, 40) };
                    this.Prefix = new NCOString(10) { DataString = Record.Substring(162, 10) };
                    this.Suffix = new NCOString(10) { DataString = Record.Substring(172, 10) };
                    this.ConsumerAlias = new NCOString(40) { DataString = Record.Substring(182, 40) };
                    this.StreetAddress1 = new NCOString(40) { DataString = Record.Substring(222, 40) };
                    this.StreetAddress2 = new NCOString(40) { DataString = Record.Substring(262, 40) };
                    this.StreetAddress3 = new NCOString(40) { DataString = Record.Substring(302, 40) };
                    this.City = new NCOString(40) { DataString = Record.Substring(342, 40) };
                    this.State = new NCOString(2) { DataString = Record.Substring(382, 2) };
                    this.Zip = new NCOString(9) { DataString = Record.Substring(384, 9) };
                    this.SSN = new NCOString(9) { DataString = Record.Substring(393, 9) };
                    this.Phone_Home = new NCOString(10) { DataString = Record.Substring(402, 10) };
                    this.Phone_Cell = new NCOString(10) { DataString = Record.Substring(412, 10) };
                    this.CommercialConsumer = new NCOBool(1, "Y", "N") { DataString = Record.Substring(422, 1) };
                    this.CommericalName = new NCOString(120) { DataString = Record.Substring(423, 120) };
                    this.BehavioralScore = new NCOString(10) { DataString = Record.Substring(543, 10) };
                    this.ClientScore1 = new NCOString(10) { DataString = Record.Substring(553, 10) };
                    this.ClientScore2 = new NCOString(10) { DataString = Record.Substring(563, 10) };
                    this.ClientScore3 = new NCOString(10) { DataString = Record.Substring(573, 10) };
                    this.LastContactDate = new NCODate() { DataString = Record.Substring(583, 8) };
                    this.BirthDate = new NCODate() { DataString = Record.Substring(591, 8) };
                    this.Email = new NCOString(120) { DataString = Record.Substring(599, 120) };
                    this.DriversLicense = new NCOString(20) { DataString = Record.Substring(719, 20) };
                    this.County = new NCOString(40) { DataString = Record.Substring(739, 40) };
                    this.AddressStatus = new NCOEnum<AddressStatus>(1) { DataString = Record.Substring(779, 1) };
                    this.Employer = new NCOString(80) { DataString = Record.Substring(780, 80) };
                    this.Employer_StreetAddress1 = new NCOString(40) { DataString = Record.Substring(860, 40) };
                    this.Employer_StreetAddress2 = new NCOString(40) { DataString = Record.Substring(900, 40) };
                    this.Employer_StreetAddress3 = new NCOString(40) { DataString = Record.Substring(940, 40) };
                    this.Employer_City = new NCOString(40) { DataString = Record.Substring(980, 40) };
                    this.Employer_State = new NCOString(2) { DataString = Record.Substring(1020, 2) };
                    this.Employer_Zip = new NCOString(9) { DataString = Record.Substring(1022, 9) };
                    this.Employer_Phone = new NCOString(10) { DataString = Record.Substring(1031, 10) };
                    this.Employer_PhoneExt = new NCOString(10) { DataString = Record.Substring(1041, 10) };
                    this.Attorney_Firm = new NCOString(80) { DataString = Record.Substring(1051, 80) };
                    this.Attorney_LastName = new NCOString(40) { DataString = Record.Substring(1131, 40) };
                    this.Attorney_FirstName = new NCOString(40) { DataString = Record.Substring(1171, 40) };
                    this.Attorney_StreetAddress1 = new NCOString(40) { DataString = Record.Substring(1211, 40) };
                    this.Attorney_StreetAddress2 = new NCOString(40) { DataString = Record.Substring(1251, 40) };
                    this.Attorney_StreetAddress3 = new NCOString(40) { DataString = Record.Substring(1291, 40) };
                    this.Attorney_City = new NCOString(40) { DataString = Record.Substring(1331, 40) };
                    this.Attorney_State = new NCOString(2) { DataString = Record.Substring(1371, 2) };
                    this.Attorney_Zip = new NCOString(9) { DataString = Record.Substring(1373, 9) };
                    this.Attorney_Phone = new NCOString(10) { DataString = Record.Substring(1382, 10) };
                    this.Attorney_Fax = new NCOString(10) { DataString = Record.Substring(1392, 10) };
                    this.Attorney_FileNumber = new NCOString(20) { DataString = Record.Substring(1402, 20) };
                    this.DeathDate = new NCODate() { DataString = Record.Substring(1422, 8) };
                    this.Bank_AccountNumber = new NCOString(40) { DataString = Record.Substring(1430, 40) };
                    this.Bank = new NCOString(80) { DataString = Record.Substring(1470, 80) };
                    this.Bank_StreetAddress1 = new NCOString(40) { DataString = Record.Substring(1550, 40) };
                    this.Bank_StreetAddress2 = new NCOString(40) { DataString = Record.Substring(1590, 40) };
                    this.Bank_StreetAddress3 = new NCOString(40) { DataString = Record.Substring(1630, 40) };
                    this.Bank_City = new NCOString(40) { DataString = Record.Substring(1670, 40) };
                    this.Bank_State = new NCOString(2) { DataString = Record.Substring(1710, 2) };
                    this.Bank_Zip = new NCOString(9) { DataString = Record.Substring(1712, 9) };
                    this.MonthlyIncome = new NCODecimal(12, 2) { DataString = Record.Substring(1721, 12) };
                    this.OtherIncome = new NCODecimal(12, 2) { DataString = Record.Substring(1733, 12) };
                    this.OtherObligations = new NCODecimal(12, 2) { DataString = Record.Substring(1745, 12) };
                    this.LastCreditChangeDate = new NCODate() { DataString = Record.Substring(1757, 8) };
                    this.CreditLimit = new NCODecimal(12, 2) { DataString = Record.Substring(1765, 12) };
                    this.MiscellaneousAsset1 = new NCOString(60) { DataString = Record.Substring(1777, 60) };
                    this.MiscellaneousAsset2 = new NCOString(60) { DataString = Record.Substring(1837, 60) };
                    this.MiscellaneousAsset3 = new NCOString(60) { DataString = Record.Substring(1897, 60) };
                }
                catch
                {
                    if (this.ConsumerNumber == null) this.ConsumerNumber = new NCOString(20);
                    if (this.LastName == null) this.LastName = new NCOString(40);
                    if (this.FirstName == null) this.FirstName = new NCOString(40);
                    if (this.MiddleName == null) this.MiddleName = new NCOString(40);
                    if (this.Prefix == null) this.Prefix = new NCOString(10);
                    if (this.Suffix == null) this.Suffix = new NCOString(10);
                    if (this.ConsumerAlias == null) this.ConsumerAlias = new NCOString(40);
                    if (this.StreetAddress1 == null) this.StreetAddress1 = new NCOString(40);
                    if (this.StreetAddress2 == null) this.StreetAddress2 = new NCOString(40);
                    if (this.StreetAddress3 == null) this.StreetAddress3 = new NCOString(40);
                    if (this.City == null) this.City = new NCOString(40);
                    if (this.State == null) this.State = new NCOString(2);
                    if (this.Zip == null) this.Zip = new NCOString(9);
                    if (this.SSN == null) this.SSN = new NCOString(9);
                    if (this.Phone_Home == null) this.Phone_Home = new NCOString(10);
                    if (this.Phone_Cell == null) this.Phone_Cell = new NCOString(10);
                    if (this.CommercialConsumer == null) this.CommercialConsumer = new NCOBool(1, "Y", "N");
                    if (this.CommericalName == null) this.CommericalName = new NCOString(120);
                    if (this.BehavioralScore == null) this.BehavioralScore = new NCOString(10);
                    if (this.ClientScore1 == null) this.ClientScore1 = new NCOString(10);
                    if (this.ClientScore2 == null) this.ClientScore2 = new NCOString(10);
                    if (this.ClientScore3 == null) this.ClientScore3 = new NCOString(10);
                    if (this.LastContactDate == null) this.LastContactDate = new NCODate();
                    if (this.BirthDate == null) this.BirthDate = new NCODate();
                    if (this.Email == null) this.Email = new NCOString(120);
                    if (this.DriversLicense == null) this.DriversLicense = new NCOString(20);
                    if (this.County == null) this.County = new NCOString(40);
                    if (this.AddressStatus == null) this.AddressStatus = new NCOEnum<AddressStatus>(1);
                    if (this.Employer == null) this.Employer = new NCOString(80);
                    if (this.Employer_StreetAddress1 == null) this.Employer_StreetAddress1 = new NCOString(40);
                    if (this.Employer_StreetAddress2 == null) this.Employer_StreetAddress2 = new NCOString(40);
                    if (this.Employer_StreetAddress3 == null) this.Employer_StreetAddress3 = new NCOString(40);
                    if (this.Employer_City == null) this.Employer_City = new NCOString(40);
                    if (this.Employer_State == null) this.Employer_State = new NCOString(2);
                    if (this.Employer_Zip == null) this.Employer_Zip = new NCOString(9);
                    if (this.Employer_Phone == null) this.Employer_Phone = new NCOString(10);
                    if (this.Employer_PhoneExt == null) this.Employer_PhoneExt = new NCOString(10);
                    if (this.Attorney_Firm == null) this.Attorney_Firm = new NCOString(80);
                    if (this.Attorney_LastName == null) this.Attorney_LastName = new NCOString(40);
                    if (this.Attorney_FirstName == null) this.Attorney_FirstName = new NCOString(40);
                    if (this.Attorney_StreetAddress1 == null) this.Attorney_StreetAddress1 = new NCOString(40);
                    if (this.Attorney_StreetAddress2 == null) this.Attorney_StreetAddress2 = new NCOString(40);
                    if (this.Attorney_StreetAddress3 == null) this.Attorney_StreetAddress3 = new NCOString(40);
                    if (this.Attorney_City == null) this.Attorney_City = new NCOString(40);
                    if (this.Attorney_State == null) this.Attorney_State = new NCOString(2);
                    if (this.Attorney_Zip == null) this.Attorney_Zip = new NCOString(9);
                    if (this.Attorney_Phone == null) this.Attorney_Phone = new NCOString(10);
                    if (this.Attorney_Fax == null) this.Attorney_Fax = new NCOString(10);
                    if (this.Attorney_FileNumber == null) this.Attorney_FileNumber = new NCOString(20);
                    if (this.DeathDate == null) this.DeathDate = new NCODate();
                    if (this.Bank_AccountNumber == null) this.Bank_AccountNumber = new NCOString(40);
                    if (this.Bank == null) this.Bank = new NCOString(80);
                    if (this.Bank_StreetAddress1 == null) this.Bank_StreetAddress1 = new NCOString(40);
                    if (this.Bank_StreetAddress2 == null) this.Bank_StreetAddress2 = new NCOString(40);
                    if (this.Bank_StreetAddress3 == null) this.Bank_StreetAddress3 = new NCOString(40);
                    if (this.Bank_City == null) this.Bank_City = new NCOString(40);
                    if (this.Bank_State == null) this.Bank_State = new NCOString(2);
                    if (this.Bank_Zip == null) this.Bank_Zip = new NCOString(9);
                    if (this.MonthlyIncome == null) this.MonthlyIncome = new NCODecimal(12, 2);
                    if (this.OtherIncome == null) this.OtherIncome = new NCODecimal(12, 2);
                    if (this.OtherObligations == null) this.OtherObligations = new NCODecimal(12, 2);
                    if (this.LastCreditChangeDate == null) this.LastCreditChangeDate = new NCODate();
                    if (this.CreditLimit == null) this.CreditLimit = new NCODecimal(12, 2);
                    if (this.MiscellaneousAsset1 == null) this.MiscellaneousAsset1 = new NCOString(60);
                    if (this.MiscellaneousAsset2 == null) this.MiscellaneousAsset2 = new NCOString(60);
                    if (this.MiscellaneousAsset3 == null) this.MiscellaneousAsset3 = new NCOString(60);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}",
                    this.RecordType,
                    this.AccountNumber,
                    this.ConsumerNumber,
                    this.LastName,
                    this.FirstName,
                    this.MiddleName,
                    this.Prefix,
                    this.Suffix,
                    this.ConsumerAlias,
                    this.StreetAddress1,
                    this.StreetAddress2,
                    this.StreetAddress3,
                    this.City,
                    this.State,
                    this.Zip,
                    this.SSN,
                    this.Phone_Home,
                    this.Phone_Cell,
                    this.CommercialConsumer,
                    this.CommericalName,
                    this.BehavioralScore,
                    this.ClientScore1,
                    this.ClientScore2,
                    this.ClientScore3,
                    this.LastContactDate,
                    this.BirthDate,
                    this.Email,
                    this.DriversLicense,
                    this.County,
                    this.AddressStatus,
                    this.Employer,
                    this.Employer_StreetAddress1,
                    this.Employer_StreetAddress2,
                    this.Employer_StreetAddress3,
                    this.Employer_City,
                    this.Employer_State,
                    this.Employer_Zip,
                    this.Employer_Phone,
                    this.Employer_PhoneExt,
                    this.Attorney_Firm,
                    this.Attorney_LastName,
                    this.Attorney_FirstName,
                    this.Attorney_StreetAddress1,
                    this.Attorney_StreetAddress2,
                    this.Attorney_StreetAddress3,
                    this.Attorney_City,
                    this.Attorney_State,
                    this.Attorney_Zip,
                    this.Attorney_Phone,
                    this.Attorney_Fax,
                    this.Attorney_FileNumber,
                    this.DeathDate,
                    this.Bank_AccountNumber,
                    this.Bank,
                    this.Bank_StreetAddress1,
                    this.Bank_StreetAddress2,
                    this.Bank_StreetAddress3,
                    this.Bank_City,
                    this.Bank_State,
                    this.Bank_Zip,
                    this.MonthlyIncome,
                    this.OtherIncome,
                    this.OtherObligations,
                    this.LastCreditChangeDate,
                    this.CreditLimit,
                    this.MiscellaneousAsset1,
                    this.MiscellaneousAsset2,
                    this.MiscellaneousAsset3);
            }
        }
        /// <summary>
        /// Related Person
        /// <para>The Related Person Record reports information pertinent to a spouse, co maker, or other person related to the responsible parties, or pertinent to the debt owed. It contains demographic as well as account specific information. There can be multiple Related Persons records per Account Related Data (01) Record.</para>
        /// </summary>
        public class RecordType03 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.RelationshipCode for code list.
            /// </summary>
            public NCOEnum<RelationshipCode> RelationshipCode { get; private set; }
            /// <summary>
            /// Last name of related person
            /// </summary>
            public NCOString LastName { get; private set; }
            /// <summary>
            /// First name of related person
            /// </summary>
            public NCOString FirstName { get; private set; }
            /// <summary>
            /// Middle name of related person
            /// </summary>
            public NCOString MiddleName { get; private set; }
            /// <summary>
            /// Prefix of related person
            /// </summary>
            public NCOString Prefix { get; private set; }
            /// <summary>
            /// Suffix of related person
            /// </summary>
            public NCOString Suffix { get; private set; }
            /// <summary>
            /// Street address of related person
            /// </summary>
            public NCOString StreetAddress1 { get; private set; }
            /// <summary>
            /// Secondary address of related person
            /// </summary>
            public NCOString StreetAddress2 { get; private set; }
            /// <summary>
            /// Third address of related person
            /// </summary>
            public NCOString StreetAddress3 { get; private set; }
            /// <summary>
            /// City of related person
            /// </summary>
            public NCOString City { get; private set; }
            /// <summary>
            /// Postal abbreviation for the state where related person resides
            /// </summary>
            public NCOString State { get; private set; }
            /// <summary>
            /// Postal Code of Related Person
            /// </summary>
            public NCOString Zip { get; private set; }
            /// <summary>
            /// County where related person resides
            /// </summary>
            public NCOString County { get; private set; }
            /// <summary>
            /// Social Security # of Related Person
            /// </summary>
            public NCOString SSN { get; private set; }
            /// <summary>
            /// Drivers license number
            /// </summary>
            public NCOString DriversLicense { get; private set; }
            /// <summary>
            /// Date the related person was born
            /// </summary>
            public NCODate BirthDate { get; private set; }
            /// <summary>
            /// Home phone of related person
            /// </summary>
            public NCOString Phone_Home { get; private set; }
            /// <summary>
            /// Cell phone of related person
            /// </summary>
            public NCOString Phone_Cell { get; private set; }
            /// <summary>
            /// Related person place of employment
            /// </summary>
            public NCOString Employer { get; private set; }
            /// <summary>
            /// Street address 1 of related person employer
            /// </summary>
            public NCOString Employer_StreetAddress1 { get; private set; }
            /// <summary>
            /// Street address 2 of related person employer
            /// </summary>
            public NCOString Employer_StreetAddress2 { get; private set; }
            /// <summary>
            /// Street address 3 of related person employer
            /// </summary>
            public NCOString Employer_StreetAddress3 { get; private set; }
            /// <summary>
            /// City of related person employer
            /// </summary>
            public NCOString Employer_City { get; private set; }
            /// <summary>
            /// State of related person employer
            /// </summary>
            public NCOString Employer_State { get; private set; }
            /// <summary>
            /// Zip code of related person employer
            /// </summary>
            public NCOString Employer_Zip { get; private set; }
            /// <summary>
            /// Phone number of related person employer
            /// </summary>
            public NCOString Employer_Phone { get; private set; }
            /// <summary>
            /// Extension of related person employer
            /// </summary>
            public NCOString Employer_PhoneExt { get; private set; }
            /// <summary>
            /// Relationship to consumer
            /// </summary>
            public NCOString RelationshipToConsumer { get; private set; }
            #endregion

            public RecordType03(string Record) : base(Record)
            {
                try
                {
                    this.RelationshipCode = new NCOEnum<Enums.RelationshipCode>(3) { DataString = Record.Substring(22, 3) };
                    this.LastName = new NCOString(40) { DataString = Record.Substring(25, 40) };
                    this.FirstName = new NCOString(40) { DataString = Record.Substring(65, 40) };
                    this.MiddleName = new NCOString(40) { DataString = Record.Substring(105, 40) };
                    this.Prefix = new NCOString(10) { DataString = Record.Substring(145, 10) };
                    this.Suffix = new NCOString(10) { DataString = Record.Substring(155, 10) };
                    this.StreetAddress1 = new NCOString(40) { DataString = Record.Substring(165, 40) };
                    this.StreetAddress2 = new NCOString(40) { DataString = Record.Substring(205, 40) };
                    this.StreetAddress3 = new NCOString(40) { DataString = Record.Substring(245, 40) };
                    this.City = new NCOString(40) { DataString = Record.Substring(285, 40) };
                    this.State = new NCOString(2) { DataString = Record.Substring(325, 2) };
                    this.Zip = new NCOString(9) { DataString = Record.Substring(327, 9) };
                    this.County = new NCOString(40) { DataString = Record.Substring(336, 9) };
                    this.SSN = new NCOString(9) { DataString = Record.Substring(376, 9) };
                    this.DriversLicense = new NCOString(20) { DataString = Record.Substring(385, 20) };
                    this.BirthDate = new NCODate() { DataString = Record.Substring(405, 8) };
                    this.Phone_Home = new NCOString(10) { DataString = Record.Substring(413, 10) };
                    this.Phone_Cell = new NCOString(10) { DataString = Record.Substring(423, 10) };
                    this.Employer = new NCOString(80) { DataString = Record.Substring(433, 80) };
                    this.Employer_StreetAddress1 = new NCOString(40) { DataString = Record.Substring(513, 40) };
                    this.Employer_StreetAddress2 = new NCOString(40) { DataString = Record.Substring(553, 40) };
                    this.Employer_StreetAddress3 = new NCOString(40) { DataString = Record.Substring(593, 40) };
                    this.Employer_City = new NCOString(40) { DataString = Record.Substring(633, 40) };
                    this.Employer_State = new NCOString(2) { DataString = Record.Substring(673, 2) };
                    this.Employer_Zip = new NCOString(9) { DataString = Record.Substring(675, 9) };
                    this.Employer_Phone = new NCOString(10) { DataString = Record.Substring(684, 10) };
                    this.Employer_PhoneExt = new NCOString(10) { DataString = Record.Substring(694, 10) };
                    this.RelationshipToConsumer = new NCOString(40) { DataString = Record.Substring(704, 40) };
                }
                catch
                {
                    if (this.RelationshipCode == null) this.RelationshipCode = new NCOEnum<RelationshipCode>(3);
                    if (this.LastName == null) this.LastName = new NCOString(40);
                    if (this.FirstName == null) this.FirstName = new NCOString(40);
                    if (this.MiddleName == null) this.MiddleName = new NCOString(40);
                    if (this.Prefix == null) this.Prefix = new NCOString(10);
                    if (this.Suffix == null) this.Suffix = new NCOString(10);
                    if (this.StreetAddress1 == null) this.StreetAddress1 = new NCOString(40);
                    if (this.StreetAddress2 == null) this.StreetAddress2 = new NCOString(40);
                    if (this.StreetAddress3 == null) this.StreetAddress3 = new NCOString(40);
                    if (this.City == null) this.City = new NCOString(40);
                    if (this.State == null) this.State = new NCOString(2);
                    if (this.Zip == null) this.Zip = new NCOString(9);
                    if (this.County == null) this.County = new NCOString(40);
                    if (this.SSN == null) this.SSN = new NCOString(9);
                    if (this.DriversLicense == null) this.DriversLicense = new NCOString(20);
                    if (this.BirthDate == null) this.BirthDate = new NCODate();
                    if (this.Phone_Home == null) this.Phone_Home = new NCOString(10);
                    if (this.Phone_Cell == null) this.Phone_Cell = new NCOString(10);
                    if (this.Employer == null) this.Employer = new NCOString(80);
                    if (this.Employer_StreetAddress1 == null) this.Employer_StreetAddress1 = new NCOString(40);
                    if (this.Employer_StreetAddress2 == null) this.Employer_StreetAddress2 = new NCOString(40);
                    if (this.Employer_StreetAddress3 == null) this.Employer_StreetAddress3 = new NCOString(40);
                    if (this.Employer_City == null) this.Employer_City = new NCOString(40);
                    if (this.Employer_State == null) this.Employer_State = new NCOString(2);
                    if (this.Employer_Zip == null) this.Employer_Zip = new NCOString(9);
                    if (this.Employer_Phone == null) this.Employer_Phone = new NCOString(10);
                    if (this.Employer_PhoneExt == null) this.Employer_PhoneExt = new NCOString(10);
                    if (this.RelationshipToConsumer == null) this.RelationshipToConsumer = new NCOString(40);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}",
                    this.RecordType,
                    this.AccountNumber,
                    this.RelationshipCode,
                    this.LastName,
                    this.FirstName,
                    this.MiddleName,
                    this.Prefix,
                    this.Suffix,
                    this.StreetAddress1,
                    this.StreetAddress2,
                    this.StreetAddress3,
                    this.City,
                    this.State,
                    this.Zip,
                    this.County,
                    this.SSN,
                    this.DriversLicense,
                    this.BirthDate,
                    this.Phone_Home,
                    this.Phone_Cell,
                    this.Employer,
                    this.Employer_StreetAddress1,
                    this.Employer_StreetAddress2,
                    this.Employer_StreetAddress3,
                    this.Employer_City,
                    this.Employer_State,
                    this.Employer_Zip,
                    this.Employer_Phone,
                    this.Employer_PhoneExt,
                    this.RelationshipToConsumer);
            }
        }
        /// <summary>
        /// Legal / Judgment
        /// <para>The Legal / Judgment Record is used to pass information about legal and judgment actions against the party responsible for the debt.</para>
        /// </summary>
        public class RecordType04 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Date suit filed
            /// </summary>
            public NCODate SuitFiledDate { get; private set; }
            /// <summary>
            /// Suit case number
            /// </summary>
            public NCOString CaseNumber_Suit { get; private set; }
            /// <summary>
            /// Amount suit requested at time of suit
            /// </summary>
            public NCODecimal SuitAmount { get; private set; }
            /// <summary>
            /// Date the service was effectuated
            /// </summary>
            public NCODate ServiceDate { get; private set; }
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.ServiceType for code list
            /// </summary>
            public NCOEnum<ServiceType> ServiceType { get; private set; }
            /// <summary>
            /// Last name of the defendant served in the suit referenced
            /// </summary>
            public NCOString ServedLastName { get; private set; }
            /// <summary>
            /// First name of the defendant served in the suit referenced
            /// </summary>
            public NCOString ServedFirstName { get; private set; }
            /// <summary>
            /// Docket number assigned to judgment
            /// </summary>
            public NCOString DocketNumber { get; private set; }
            /// <summary>
            /// Date of judgment
            /// </summary>
            public NCODate JudgmentDate { get; private set; }
            /// <summary>
            /// Expiration date of judgment
            /// </summary>
            public NCODate JudgmentExpirationDate { get; private set; }
            /// <summary>
            /// Total Judgment awarded amount
            /// </summary>
            public NCODecimal AwardedAmount { get; private set; }
            /// <summary>
            /// Judgment principal awarded amount
            /// </summary>
            public NCODecimal AwardedPrincipal { get; private set; }
            /// <summary>
            /// Judgment interest awarded amount
            /// </summary>
            public NCODecimal AwardedInterest { get; private set; }
            /// <summary>
            /// Judgment fees awarded amount
            /// </summary>
            public NCODecimal AwardedFee { get; private set; }
            /// <summary>
            /// Judgment cost awarded amount
            /// </summary>
            public NCODecimal AwardedCost { get; private set; }
            /// <summary>
            /// Amount of judgment balance on which interest can be charged
            /// </summary>
            public NCODecimal InterestChargingBalance { get; private set; }
            /// <summary>
            /// Amount of judgment balance on which interest cannot be charged
            /// </summary>
            public NCODecimal NonInterestChargingBalance { get; private set; }
            /// <summary>
            /// Judgment awarded interest rate
            /// </summary>
            public NCODecimal AwardedInterestPercentage { get; private set; }
            /// <summary>
            /// Pre-judgment interest rate
            /// </summary>
            public NCODecimal PreJudgmentPercentage { get; private set; }
            /// <summary>
            /// Case number of Garnishment
            /// </summary>
            public NCOString CaseNumber_Garnishment { get; private set; }
            /// <summary>
            /// Date bank garnishment was filed
            /// </summary>
            public NCODate GarnishmentDate_Bank { get; private set; }
            /// <summary>
            /// Date wage garnishment was filed
            /// </summary>
            public NCODate GarnishmentDate_Wage { get; private set; }
            /// <summary>
            /// Date lien was filed
            /// </summary>
            public NCODate LienDate { get; private set; }
            /// <summary>
            /// Name of the jurisdiction / county where suit was filed
            /// </summary>
            public NCOString CourtJurisdiction { get; private set; }
            /// <summary>
            /// Name of the court where suit was filed
            /// </summary>
            public NCOString Court { get; private set; }
            /// <summary>
            /// The date this case will go to courts
            /// </summary>
            public NCODate CourtDate { get; private set; }
            /// <summary>
            /// Last name of consumer's attorney for this suit
            /// </summary>
            public NCOString TrialAttorney_LastName { get; private set; }
            /// <summary>
            /// First name of consumer's attorney for this suit
            /// </summary>
            public NCOString TrialAttorney_FirstName { get; private set; }
            /// <summary>
            /// The date of the trial
            /// </summary>
            public NCODate TrialDate { get; private set; }
            /// <summary>
            /// Last name of the sheriff responsible for service
            /// </summary>
            public NCOString Sheriff_LastName { get; private set; }
            /// <summary>
            /// First name of the sheriff responsible for service
            /// </summary>
            public NCOString Sheriff_FirstName { get; private set; }
            /// <summary>
            /// Sheriff's Office address line 1
            /// </summary>
            public NCOString Sheriff_Address1 { get; private set; }
            /// <summary>
            /// Sheriff's Office address line 2
            /// </summary>
            public NCOString Sheriff_Address2 { get; private set; }
            /// <summary>
            /// Sheriff's Office address line 3
            /// </summary>
            public NCOString Sheriff_Address3 { get; private set; }
            /// <summary>
            /// Sheriff's Office city
            /// </summary>
            public NCOString Sheriff_City { get; private set; }
            /// <summary>
            /// Sheriff's Office state
            /// </summary>
            public NCOString Sheriff_State { get; private set; }
            /// <summary>
            /// Sheriff's Office zip code
            /// </summary>
            public NCOString Sheriff_Zip { get; private set; }
            /// <summary>
            /// Additional comments associated with this suit
            /// </summary>
            public NCOString Comments { get; private set; }
            #endregion

            public RecordType04(string Record) : base(Record)
            {
                try
                {
                    this.SuitFiledDate = new NCODate() { DataString = Record.Substring(22, 8) };
                    this.CaseNumber_Suit = new NCOString(30) { DataString = Record.Substring(30, 30) };
                    this.SuitAmount = new NCODecimal(12, 2) { DataString = Record.Substring(60, 12) };
                    this.ServiceDate = new NCODate() { DataString = Record.Substring(72, 8) };
                    this.ServiceType = new NCOEnum<ServiceType>(2) { DataString = Record.Substring(80, 2) };
                    this.ServedLastName = new NCOString(40) { DataString = Record.Substring(82, 40) };
                    this.ServedFirstName = new NCOString(40) { DataString = Record.Substring(122, 40) };
                    this.DocketNumber = new NCOString(30) { DataString = Record.Substring(162, 30) };
                    this.JudgmentDate = new NCODate() { DataString = Record.Substring(192, 8) };
                    this.JudgmentExpirationDate = new NCODate() { DataString = Record.Substring(200, 8) };
                    this.AwardedAmount = new NCODecimal(12, 2) { DataString = Record.Substring(208, 12) };
                    this.AwardedPrincipal = new NCODecimal(12, 2) { DataString = Record.Substring(220, 12) };
                    this.AwardedInterest = new NCODecimal(12, 2) { DataString = Record.Substring(232, 12) };
                    this.AwardedFee = new NCODecimal(12, 2) { DataString = Record.Substring(244, 12) };
                    this.AwardedCost = new NCODecimal(12, 2) { DataString = Record.Substring(256, 12) };
                    this.InterestChargingBalance = new NCODecimal(12, 2) { DataString = Record.Substring(268, 12) };
                    this.NonInterestChargingBalance = new NCODecimal(12, 2) { DataString = Record.Substring(280, 12) };
                    this.AwardedInterestPercentage = new NCODecimal(8, 4) { DataString = Record.Substring(292, 8) };
                    this.PreJudgmentPercentage = new NCODecimal(8, 4) { DataString = Record.Substring(300, 8) };
                    this.CaseNumber_Garnishment = new NCOString(30) { DataString = Record.Substring(308, 30) };
                    this.GarnishmentDate_Bank = new NCODate() { DataString = Record.Substring(338, 8) };
                    this.GarnishmentDate_Wage = new NCODate() { DataString = Record.Substring(346, 8) };
                    this.LienDate = new NCODate() { DataString = Record.Substring(354, 8) };
                    this.CourtJurisdiction = new NCOString(80) { DataString = Record.Substring(362, 80) };
                    this.Court = new NCOString(80) { DataString = Record.Substring(442, 80) };
                    this.CourtDate = new NCODate() { DataString = Record.Substring(522, 8) };
                    this.TrialAttorney_LastName = new NCOString(40) { DataString = Record.Substring(530, 40) };
                    this.TrialAttorney_FirstName = new NCOString(40) { DataString = Record.Substring(570, 40) };
                    this.TrialDate = new NCODate() { DataString = Record.Substring(610, 8) };
                    this.Sheriff_LastName = new NCOString(40) { DataString = Record.Substring(618, 40) };
                    this.Sheriff_FirstName = new NCOString(40) { DataString = Record.Substring(658, 40) };
                    this.Sheriff_Address1 = new NCOString(40) { DataString = Record.Substring(698, 40) };
                    this.Sheriff_Address2 = new NCOString(40) { DataString = Record.Substring(738, 40) };
                    this.Sheriff_Address3 = new NCOString(40) { DataString = Record.Substring(778, 40) };
                    this.Sheriff_City = new NCOString(40) { DataString = Record.Substring(818, 40) };
                    this.Sheriff_State = new NCOString(2) { DataString = Record.Substring(858, 2) };
                    this.Sheriff_Zip = new NCOString(9) { DataString = Record.Substring(860, 9) };
                    this.Comments = new NCOString(100) { DataString = Record.Substring(869, 100) };
                }
                catch
                {
                    if (this.SuitFiledDate == null) this.SuitFiledDate = new NCODate();
                    if (this.CaseNumber_Suit == null) this.CaseNumber_Suit = new NCOString(30);
                    if (this.SuitAmount == null) this.SuitAmount = new NCODecimal(12, 2);
                    if (this.ServiceDate == null) this.ServiceDate = new NCODate();
                    if (this.ServiceType == null) this.ServiceType = new NCOEnum<ServiceType>(2);
                    if (this.ServedLastName == null) this.ServedLastName = new NCOString(40);
                    if (this.ServedFirstName == null) this.ServedFirstName = new NCOString(40);
                    if (this.DocketNumber == null) this.DocketNumber = new NCOString(30);
                    if (this.JudgmentDate == null) this.JudgmentDate = new NCODate();
                    if (this.JudgmentExpirationDate == null) this.JudgmentExpirationDate = new NCODate();
                    if (this.AwardedAmount == null) this.AwardedAmount = new NCODecimal(12, 2);
                    if (this.AwardedPrincipal == null) this.AwardedPrincipal = new NCODecimal(12, 2);
                    if (this.AwardedInterest == null) this.AwardedInterest = new NCODecimal(12, 2);
                    if (this.AwardedFee == null) this.AwardedFee = new NCODecimal(12, 2);
                    if (this.AwardedCost == null) this.AwardedCost = new NCODecimal(12, 2);
                    if (this.InterestChargingBalance == null) this.InterestChargingBalance = new NCODecimal(12, 2);
                    if (this.NonInterestChargingBalance == null) this.NonInterestChargingBalance = new NCODecimal(12, 2);
                    if (this.AwardedInterestPercentage == null) this.AwardedInterestPercentage = new NCODecimal(8, 4);
                    if (this.PreJudgmentPercentage == null) this.PreJudgmentPercentage = new NCODecimal(8, 4);
                    if (this.CaseNumber_Garnishment == null) this.CaseNumber_Garnishment = new NCOString(30);
                    if (this.GarnishmentDate_Bank == null) this.GarnishmentDate_Bank = new NCODate();
                    if (this.GarnishmentDate_Wage == null) this.GarnishmentDate_Wage = new NCODate();
                    if (this.LienDate == null) this.LienDate = new NCODate();
                    if (this.CourtJurisdiction == null) this.CourtJurisdiction = new NCOString(80);
                    if (this.Court == null) this.Court = new NCOString(80);
                    if (this.CourtDate == null) this.CourtDate = new NCODate();
                    if (this.TrialAttorney_LastName == null) this.TrialAttorney_LastName = new NCOString(40);
                    if (this.TrialAttorney_FirstName == null) this.TrialAttorney_FirstName = new NCOString(40);
                    if (this.TrialDate == null) this.TrialDate = new NCODate();
                    if (this.Sheriff_LastName == null) this.Sheriff_LastName = new NCOString(40);
                    if (this.Sheriff_FirstName == null) this.Sheriff_FirstName = new NCOString(40);
                    if (this.Sheriff_Address1 == null) this.Sheriff_Address1 = new NCOString(40);
                    if (this.Sheriff_Address2 == null) this.Sheriff_Address2 = new NCOString(40);
                    if (this.Sheriff_Address3 == null) this.Sheriff_Address3 = new NCOString(40);
                    if (this.Sheriff_City == null) this.Sheriff_City = new NCOString(40);
                    if (this.Sheriff_State == null) this.Sheriff_State = new NCOString(2);
                    if (this.Sheriff_Zip == null) this.Sheriff_Zip = new NCOString(9);
                    if (this.Comments == null) this.Comments = new NCOString(100);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}",
                    this.RecordType,
                    this.AccountNumber,
                    this.SuitFiledDate,
                    this.CaseNumber_Suit,
                    this.SuitAmount,
                    this.ServiceDate,
                    this.ServiceType,
                    this.ServedLastName,
                    this.ServedFirstName,
                    this.DocketNumber,
                    this.JudgmentDate,
                    this.JudgmentExpirationDate,
                    this.AwardedAmount,
                    this.AwardedPrincipal,
                    this.AwardedInterest,
                    this.AwardedFee,
                    this.AwardedCost,
                    this.InterestChargingBalance,
                    this.NonInterestChargingBalance,
                    this.AwardedInterestPercentage,
                    this.PreJudgmentPercentage,
                    this.CaseNumber_Garnishment,
                    this.GarnishmentDate_Bank,
                    this.GarnishmentDate_Wage,
                    this.LienDate,
                    this.CourtJurisdiction,
                    this.Court,
                    this.CourtDate,
                    this.TrialAttorney_LastName,
                    this.TrialAttorney_FirstName,
                    this.TrialDate,
                    this.Sheriff_LastName,
                    this.Sheriff_FirstName,
                    this.Sheriff_Address1,
                    this.Sheriff_Address2,
                    this.Sheriff_Address3,
                    this.Sheriff_City,
                    this.Sheriff_State,
                    this.Sheriff_Zip,
                    this.Comments);
            }
        }
        /// <summary>
        /// Bankruptcy
        /// <para>The Bankruptcy Record is used to store information about bankruptcy actions against the party responsible for the debt or related parties.</para>
        /// </summary>
        public class RecordType05 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Relationship to primary debtor
            /// </summary>
            public NCOEnum<RelationshipCode> RelationshipCode { get; private set; }
            /// <summary>
            /// Used if multiple bankruptcies (1-999)
            /// </summary>
            public NCONumber BankruptcyCounter { get; private set; }
            /// <summary>
            /// (N=No, Y=Yes) Identifies the bankruptcy that is active currently
            /// </summary>
            public NCOBool ActiveBankruptcy { get; private set; }
            /// <summary>
            /// Bankruptcy Chapter
            /// </summary>
            public NCOEnum<ChapterCode> BankruptcyChapter { get; private set; }
            /// <summary>
            /// The name of the court where the bankruptcy was filed
            /// </summary>
            public NCOString Court { get; private set; }
            /// <summary>
            /// The district of the court where the bankruptcy was filed
            /// </summary>
            public NCOString Court_District { get; private set; }
            /// <summary>
            /// The city of the court where the bankruptcy was filed
            /// </summary>
            public NCOString Court_City { get; private set; }
            /// <summary>
            /// The state of the court where the bankruptcy was filed
            /// </summary>
            public NCOString Court_State { get; private set; }
            /// <summary>
            /// The initials of the judge assigned to the bankruptcy
            /// </summary>
            public NCOString JudgeInitials { get; private set; }
            /// <summary>
            /// Status of bankruptcy
            /// </summary>
            public NCOEnum<DispositionCode> DispositionCode { get; private set; }
            /// <summary>
            /// Date bankruptcy was discharged
            /// </summary>
            public NCODate DischargeDate { get; private set; }
            /// <summary>
            /// Date bankruptcy was dismissed
            /// </summary>
            public NCODate DismissalDate { get; private set; }
            /// <summary>
            /// Docket number assigned to this bankruptcy
            /// </summary>
            public NCOString DocketNumber { get; private set; }
            /// <summary>
            /// Date the bankruptcy was filed
            /// </summary>
            public NCODate FilingDate { get; private set; }
            /// <summary>
            /// Jurisdiction where bankruptcy was filed
            /// </summary>
            public NCOString Jurisdiction { get; private set; }
            /// <summary>
            /// Phone number of bankruptcy court
            /// </summary>
            public NCOString Court_Phone { get; private set; }
            /// <summary>
            /// Proof Date
            /// </summary>
            public NCODate ProofDate { get; private set; }
            /// <summary>
            /// The last name of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_LastName { get; private set; }
            /// <summary>
            /// The first name of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_FirstName { get; private set; }
            /// <summary>
            /// The address (line1) of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_Address1 { get; private set; }
            /// <summary>
            /// The address (line2) of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_Address2 { get; private set; }
            /// <summary>
            /// The address (line3) of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_Address3 { get; private set; }
            /// <summary>
            /// The city of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_City { get; private set; }
            /// <summary>
            /// The state of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_State { get; private set; }
            /// <summary>
            /// The postal code of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_Zip { get; private set; }
            /// <summary>
            /// The phone number of the person who is listed as the trustee for the consumer
            /// </summary>
            public NCOString Trustee_Phone { get; private set; }
            /// <summary>
            /// The amount of money the courts hand down for reaffirmation from the consumer
            /// </summary>
            public NCODecimal ReaffirmationAmount { get; private set; }
            /// <summary>
            /// The amount of money to be paid by the consumer to the attorney for the reaffirmation proceedings
            /// </summary>
            public NCODecimal ReaffirmationPayment { get; private set; }
            /// <summary>
            /// The date the reaffirmation was signed by the consumer
            /// </summary>
            public NCODate ReaffirmationSignedDate { get; private set; }
            /// <summary>
            /// The date that the reaffirmation was filed with the courts by the attorney handling the case
            /// </summary>
            public NCODate ReaffirmationFiledDate { get; private set; }
            /// <summary>
            /// The date the attorney handling the case received confirmation of the case going to court
            /// </summary>
            public NCODate ConfirmationDate { get; private set; }
            /// <summary>
            /// The date that the 341 meeting was attended by both the attorney and the consumer
            /// </summary>
            public NCODateTime MeetingDate_341 { get; private set; }
            /// <summary>
            /// ECOA
            /// <para>J= Joint, S= Single</para>
            /// <para>True = Joint</para>
            /// <para>False = Single (Default)</para>
            /// </summary>
            public NCOBool ECOA_Joint { get; private set; }
            /// <summary>
            /// Bar Date
            /// </summary>
            public NCODate BarDate { get; private set; }
            /// <summary>
            /// The date that the bankruptcy chapter was converted
            /// </summary>
            public NCODate ConversionDate { get; private set; }
            /// <summary>
            /// Status Date
            /// </summary>
            public NCODate StatusDate { get; private set; }
            /// <summary>
            /// The previous chapter of the bankruptcy after it was converted
            /// </summary>
            public NCOEnum<ChapterCode> PreviousChapter { get; private set; }
            /// <summary>
            /// Assets Available
            /// <para>T= True, F = False</para>
            /// </summary>
            public NCOBool AssetsAvailable { get; private set; }
            /// <summary>
            /// Debt Secured
            /// <para>S = Secured, U = Unsecured</para>
            /// </summary>
            public NCOBool DebtSecured { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias1_Filer_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias1_Filer_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias2_Filer_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias2_Filer_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias3_Filer_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias3_Filer_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias4_Filer_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias4_Filer_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias1_CoFiler_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias1_CoFiler_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias2_CoFiler_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias2_CoFiler_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias3_CoFiler_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias3_CoFiler_FirstName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias4_CoFiler_LastName { get; private set; }
            /// <summary>
            /// This will hold any other names a person may be known as so that the courts can alert all interested parties.
            /// </summary>
            public NCOString Alias4_CoFiler_FirstName { get; private set; }
            #endregion

            public RecordType05(string Record) : base(Record)
            {
                try
                {
                    this.RelationshipCode = new NCOEnum<RelationshipCode>(3) { DataString = Record.Substring(22) };
                    this.BankruptcyCounter = new NCONumber(3) { DataString = Record.Substring(25) };
                    this.ActiveBankruptcy = new NCOBool(2, "Y", "N") { DataString = Record.Substring(28) };
                    this.BankruptcyChapter = new NCOEnum<ChapterCode>(1) { DataString = Record.Substring(30) };
                    this.Court = new NCOString(80) { DataString = Record.Substring(31) };
                    this.Court_District = new NCOString(40) { DataString = Record.Substring(111) };
                    this.Court_City = new NCOString(40) { DataString = Record.Substring(151) };
                    this.Court_State = new NCOString(2) { DataString = Record.Substring(191) };
                    this.JudgeInitials = new NCOString(4) { DataString = Record.Substring(193) };
                    this.DispositionCode = new NCOEnum<DispositionCode>(1) { DataString = Record.Substring(197) };
                    this.DischargeDate = new NCODate() { DataString = Record.Substring(198) };
                    this.DismissalDate = new NCODate() { DataString = Record.Substring(206) };
                    this.DocketNumber = new NCOString(30) { DataString = Record.Substring(214) };
                    this.FilingDate = new NCODate() { DataString = Record.Substring(244) };
                    this.Jurisdiction = new NCOString(80) { DataString = Record.Substring(252) };
                    this.Court_Phone = new NCOString(10) { DataString = Record.Substring(332) };
                    this.ProofDate = new NCODate() { DataString = Record.Substring(342) };
                    this.Trustee_LastName = new NCOString(40) { DataString = Record.Substring(350) };
                    this.Trustee_FirstName = new NCOString(40) { DataString = Record.Substring(390) };
                    this.Trustee_Address1 = new NCOString(40) { DataString = Record.Substring(430) };
                    this.Trustee_Address2 = new NCOString(40) { DataString = Record.Substring(470) };
                    this.Trustee_Address3 = new NCOString(40) { DataString = Record.Substring(510) };
                    this.Trustee_City = new NCOString(40) { DataString = Record.Substring(550) };
                    this.Trustee_State = new NCOString(2) { DataString = Record.Substring(590) };
                    this.Trustee_Zip = new NCOString(9) { DataString = Record.Substring(592) };
                    this.Trustee_Phone = new NCOString(10) { DataString = Record.Substring(601) };
                    this.ReaffirmationAmount = new NCODecimal(12, 2) { DataString = Record.Substring(611) };
                    this.ReaffirmationPayment = new NCODecimal(12, 2) { DataString = Record.Substring(623) };
                    this.ReaffirmationSignedDate = new NCODate() { DataString = Record.Substring(635) };
                    this.ReaffirmationFiledDate = new NCODate() { DataString = Record.Substring(643) };
                    this.ConfirmationDate = new NCODate() { DataString = Record.Substring(651) };
                    this.MeetingDate_341 = new NCODateTime() { DataString = Record.Substring(659) };
                    this.ECOA_Joint = new NCOBool(1, "J", "S") { DataString = Record.Substring(675) };
                    this.BarDate = new NCODate() { DataString = Record.Substring(676) };
                    this.ConversionDate = new NCODate() { DataString = Record.Substring(684) };
                    this.StatusDate = new NCODate() { DataString = Record.Substring(692) };
                    this.PreviousChapter = new NCOEnum<ChapterCode>(1) { DataString = Record.Substring(700) };
                    this.AssetsAvailable = new NCOBool(1, "T", "F") { DataString = Record.Substring(701) };
                    this.DebtSecured = new NCOBool(1, "S", "U") { DataString = Record.Substring(702) };
                    this.Alias1_Filer_LastName = new NCOString(40) { DataString = Record.Substring(703) };
                    this.Alias1_Filer_FirstName = new NCOString(40) { DataString = Record.Substring(743) };
                    this.Alias2_Filer_LastName = new NCOString(40) { DataString = Record.Substring(783) };
                    this.Alias2_Filer_FirstName = new NCOString(40) { DataString = Record.Substring(823) };
                    this.Alias3_Filer_LastName = new NCOString(40) { DataString = Record.Substring(863) };
                    this.Alias3_Filer_FirstName = new NCOString(40) { DataString = Record.Substring(903) };
                    this.Alias4_Filer_LastName = new NCOString(40) { DataString = Record.Substring(943) };
                    this.Alias4_Filer_FirstName = new NCOString(40) { DataString = Record.Substring(983) };
                    this.Alias1_CoFiler_LastName = new NCOString(40) { DataString = Record.Substring(1023) };
                    this.Alias1_CoFiler_FirstName = new NCOString(40) { DataString = Record.Substring(1063) };
                    this.Alias2_CoFiler_LastName = new NCOString(40) { DataString = Record.Substring(1103) };
                    this.Alias2_CoFiler_FirstName = new NCOString(40) { DataString = Record.Substring(1143) };
                    this.Alias3_CoFiler_LastName = new NCOString(40) { DataString = Record.Substring(1183) };
                    this.Alias3_CoFiler_FirstName = new NCOString(40) { DataString = Record.Substring(1223) };
                    this.Alias4_CoFiler_LastName = new NCOString(40) { DataString = Record.Substring(1263) };
                    this.Alias4_CoFiler_FirstName = new NCOString(40) { DataString = Record.Substring(1303) };
                }
                catch
                {
                    if (this.RelationshipCode == null) this.RelationshipCode = new NCOEnum<RelationshipCode>(3);
                    if (this.BankruptcyCounter == null) this.BankruptcyCounter = new NCONumber(3);
                    if (this.ActiveBankruptcy == null) this.ActiveBankruptcy = new NCOBool(2, "Y", "N");
                    if (this.BankruptcyChapter == null) this.BankruptcyChapter = new NCOEnum<ChapterCode>(1);
                    if (this.Court == null) this.Court = new NCOString(80);
                    if (this.Court_District == null) this.Court_District = new NCOString(40);
                    if (this.Court_City == null) this.Court_City = new NCOString(40);
                    if (this.Court_State == null) this.Court_State = new NCOString(2);
                    if (this.JudgeInitials == null) this.JudgeInitials = new NCOString(4);
                    if (this.DispositionCode == null) this.DispositionCode = new NCOEnum<DispositionCode>(1);
                    if (this.DischargeDate == null) this.DischargeDate = new NCODate();
                    if (this.DismissalDate == null) this.DismissalDate = new NCODate();
                    if (this.DocketNumber == null) this.DocketNumber = new NCOString(30);
                    if (this.FilingDate == null) this.FilingDate = new NCODate();
                    if (this.Jurisdiction == null) this.Jurisdiction = new NCOString(80);
                    if (this.Court_Phone == null) this.Court_Phone = new NCOString(10);
                    if (this.ProofDate == null) this.ProofDate = new NCODate();
                    if (this.Trustee_LastName == null) this.Trustee_LastName = new NCOString(40);
                    if (this.Trustee_FirstName == null) this.Trustee_FirstName = new NCOString(40);
                    if (this.Trustee_Address1 == null) this.Trustee_Address1 = new NCOString(40);
                    if (this.Trustee_Address2 == null) this.Trustee_Address2 = new NCOString(40);
                    if (this.Trustee_Address3 == null) this.Trustee_Address3 = new NCOString(40);
                    if (this.Trustee_City == null) this.Trustee_City = new NCOString(40);
                    if (this.Trustee_State == null) this.Trustee_State = new NCOString(2);
                    if (this.Trustee_Zip == null) this.Trustee_Zip = new NCOString(9);
                    if (this.Trustee_Phone == null) this.Trustee_Phone = new NCOString(10);
                    if (this.ReaffirmationAmount == null) this.ReaffirmationAmount = new NCODecimal(12, 2);
                    if (this.ReaffirmationPayment == null) this.ReaffirmationPayment = new NCODecimal(12, 2);
                    if (this.ReaffirmationSignedDate == null) this.ReaffirmationSignedDate = new NCODate();
                    if (this.ReaffirmationFiledDate == null) this.ReaffirmationFiledDate = new NCODate();
                    if (this.ConfirmationDate == null) this.ConfirmationDate = new NCODate();
                    if (this.MeetingDate_341 == null) this.MeetingDate_341 = new NCODateTime();
                    if (this.ECOA_Joint == null) this.ECOA_Joint = new NCOBool(1, "J", "S");
                    if (this.BarDate == null) this.BarDate = new NCODate();
                    if (this.ConversionDate == null) this.ConversionDate = new NCODate();
                    if (this.StatusDate == null) this.StatusDate = new NCODate();
                    if (this.PreviousChapter == null) this.PreviousChapter = new NCOEnum<ChapterCode>(1);
                    if (this.AssetsAvailable == null) this.AssetsAvailable = new NCOBool(1, "T", "F");
                    if (this.DebtSecured == null) this.DebtSecured = new NCOBool(1, "S", "U");
                    if (this.Alias1_Filer_LastName == null) this.Alias1_Filer_LastName = new NCOString(40);
                    if (this.Alias1_Filer_FirstName == null) this.Alias1_Filer_FirstName = new NCOString(40);
                    if (this.Alias2_Filer_LastName == null) this.Alias2_Filer_LastName = new NCOString(40);
                    if (this.Alias2_Filer_FirstName == null) this.Alias2_Filer_FirstName = new NCOString(40);
                    if (this.Alias3_Filer_LastName == null) this.Alias3_Filer_LastName = new NCOString(40);
                    if (this.Alias3_Filer_FirstName == null) this.Alias3_Filer_FirstName = new NCOString(40);
                    if (this.Alias4_Filer_LastName == null) this.Alias4_Filer_LastName = new NCOString(40);
                    if (this.Alias4_Filer_FirstName == null) this.Alias4_Filer_FirstName = new NCOString(40);
                    if (this.Alias1_CoFiler_LastName == null) this.Alias1_CoFiler_LastName = new NCOString(40);
                    if (this.Alias1_CoFiler_FirstName == null) this.Alias1_CoFiler_FirstName = new NCOString(40);
                    if (this.Alias2_CoFiler_LastName == null) this.Alias2_CoFiler_LastName = new NCOString(40);
                    if (this.Alias2_CoFiler_FirstName == null) this.Alias2_CoFiler_FirstName = new NCOString(40);
                    if (this.Alias3_CoFiler_LastName == null) this.Alias3_CoFiler_LastName = new NCOString(40);
                    if (this.Alias3_CoFiler_FirstName == null) this.Alias3_CoFiler_FirstName = new NCOString(40);
                    if (this.Alias4_CoFiler_LastName == null) this.Alias4_CoFiler_LastName = new NCOString(40);
                    if (this.Alias4_CoFiler_FirstName == null) this.Alias4_CoFiler_FirstName = new NCOString(40);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}",
                    this.RecordType,
                    this.AccountNumber,
                    this.RelationshipCode,
                    this.BankruptcyCounter,
                    this.ActiveBankruptcy,
                    this.BankruptcyChapter,
                    this.Court,
                    this.Court_District,
                    this.Court_City,
                    this.Court_State,
                    this.JudgeInitials,
                    this.DispositionCode,
                    this.DischargeDate,
                    this.DismissalDate,
                    this.DocketNumber,
                    this.FilingDate,
                    this.Jurisdiction,
                    this.Court_Phone,
                    this.ProofDate,
                    this.Trustee_LastName,
                    this.Trustee_FirstName,
                    this.Trustee_Address1,
                    this.Trustee_Address2,
                    this.Trustee_Address3,
                    this.Trustee_City,
                    this.Trustee_State,
                    this.Trustee_Zip,
                    this.Trustee_Phone,
                    this.ReaffirmationAmount,
                    this.ReaffirmationPayment,
                    this.ReaffirmationSignedDate,
                    this.ReaffirmationFiledDate,
                    this.ConfirmationDate,
                    this.MeetingDate_341,
                    this.ECOA_Joint,
                    this.BarDate,
                    this.ConversionDate,
                    this.StatusDate,
                    this.PreviousChapter,
                    this.AssetsAvailable,
                    this.DebtSecured,
                    this.Alias1_Filer_LastName,
                    this.Alias1_Filer_FirstName,
                    this.Alias2_Filer_LastName,
                    this.Alias2_Filer_FirstName,
                    this.Alias3_Filer_LastName,
                    this.Alias3_Filer_FirstName,
                    this.Alias4_Filer_LastName,
                    this.Alias4_Filer_FirstName,
                    this.Alias1_CoFiler_LastName,
                    this.Alias1_CoFiler_FirstName,
                    this.Alias2_CoFiler_LastName,
                    this.Alias2_CoFiler_FirstName,
                    this.Alias3_CoFiler_LastName,
                    this.Alias3_CoFiler_FirstName,
                    this.Alias4_CoFiler_LastName,
                    this.Alias4_CoFiler_FirstName);
            }
        }
        /// <summary>
        /// Additional Notes
        /// <para>The Additional Notes Record is used to pass comments at the time of placement from the client. The individual note fields can either be treated as one consecutive note, or separate notes. This is dependant on the client.</para>
        /// </summary>
        public class RecordType06 : RecordBase
        {
            #region Public Properties
            public NCOString Note1 { get; private set; }
            public NCOString Note2 { get; private set; }
            public NCOString Note3 { get; private set; }
            public NCOString Note4 { get; private set; }
            public NCOString Note5 { get; private set; }
            public NCOString Note6 { get; private set; }
            public NCOString Note7 { get; private set; }
            public NCOString Note8 { get; private set; }
            public NCOString Note9 { get; private set; }
            #endregion

            public RecordType06(string Record) : base(Record)
            {
                try
                {
                    this.Note1 = new NCOString(100) { DataString = Record.Substring(22) };
                    this.Note2 = new NCOString(100) { DataString = Record.Substring(122) };
                    this.Note3 = new NCOString(100) { DataString = Record.Substring(222) };
                    this.Note4 = new NCOString(100) { DataString = Record.Substring(322) };
                    this.Note5 = new NCOString(100) { DataString = Record.Substring(422) };
                    this.Note6 = new NCOString(100) { DataString = Record.Substring(522) };
                    this.Note7 = new NCOString(100) { DataString = Record.Substring(622) };
                    this.Note8 = new NCOString(100) { DataString = Record.Substring(722) };
                    this.Note9 = new NCOString(100) { DataString = Record.Substring(822) };
                }
                catch
                {
                    if (this.Note1 == null) this.Note1 = new NCOString(100);
                    if (this.Note2 == null) this.Note2 = new NCOString(100);
                    if (this.Note3 == null) this.Note3 = new NCOString(100);
                    if (this.Note4 == null) this.Note4 = new NCOString(100);
                    if (this.Note5 == null) this.Note5 = new NCOString(100);
                    if (this.Note6 == null) this.Note6 = new NCOString(100);
                    if (this.Note7 == null) this.Note7 = new NCOString(100);
                    if (this.Note8 == null) this.Note8 = new NCOString(100);
                    if (this.Note9 == null) this.Note9 = new NCOString(100);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                    this.RecordType,
                    this.AccountNumber,
                    this.Note1,
                    this.Note2,
                    this.Note3,
                    this.Note4,
                    this.Note5,
                    this.Note6,
                    this.Note7,
                    this.Note8,
                    this.Note9);
            }
        }
        /// <summary>
        /// Financial Transactions
        /// <para>The Financial Transaction Record is used to pass financial transactions both from NCO to Receiver and Receiver to NCO.</para>
        /// </summary>
        public class RecordType07 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Client name
            /// </summary>
            public NCOString CreditorGroup { get; private set; }
            /// <summary>
            /// Identifies client this account belongs to
            /// </summary>
            public NCOString Creditor { get; private set; }
            /// <summary>
            /// The batch number associated with this payment, this will be associated with the Unique Financial ID
            /// </summary>
            public NCOString BatchNumber { get; private set; }
            /// <summary>
            /// The invoice number the receiver uses to track this financial transaction
            /// </summary>
            public NCOString ReceiverInvoiceNumber { get; private set; }
            /// <summary>
            /// Date Payment, adjustment or cost is made and posted in the system. (If not supplied the current date is assumed)
            /// </summary>
            public NCODate FinancialPostedDate { get; private set; }
            /// <summary>
            /// Total Amount of Payment, adjustment or cost. This field must be filled in and be a positive number
            /// </summary>
            public NCODecimal FinancialAmount { get; private set; }
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.FinancialType for code list
            /// </summary>
            public NCOEnum<FinancialType> FinancialType { get; private set; }
            /// <summary>
            /// Unique identifier for payment. IF NSF please put Unique identifier from original payment here
            /// </summary>
            public NCOString FinancialUniqueID { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards Principal
            /// </summary>
            public NCODecimal PrincipalAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards Interest
            /// </summary>
            public NCODecimal InterestAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards Court Cost
            /// </summary>
            public NCODecimal CourtCostAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards Attorney Fees
            /// </summary>
            public NCODecimal AttoneyFeesAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards this Other Fee Amount
            /// </summary>
            public NCODecimal OtherFeeAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards NSF Fees
            /// </summary>
            public NCODecimal NSFFeeAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards Collection Charge Amount
            /// </summary>
            public NCODecimal CollectionChargeAmount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards this bucket.
            /// </summary>
            public NCODecimal Bucket8Amount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards this bucket.
            /// </summary>
            public NCODecimal Bucket9Amount { get; private set; }
            /// <summary>
            /// Amount from the payment credited towards this bucket.
            /// </summary>
            public NCODecimal Bucket10Amount { get; private set; }
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.CourtCostType for code list
            /// </summary>
            public NCOEnum<CourtCostType> CourtCostType { get; private set; }
            /// <summary>
            /// Total Commission Amount for this payment.
            /// </summary>
            public NCODecimal CommissionAmount { get; private set; }
            /// <summary>
            /// Commission percentage for this payment.
            /// </summary>
            public NCODecimal CommissionPercent { get; private set; }
            /// <summary>
            /// The total amount of the debt remaining after this financial transaction has been applied
            /// </summary>
            public NCODecimal TotalRemainingBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Principal after payment is posted
            /// </summary>
            public NCODecimal PrincipalBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Interest after payment is posted
            /// </summary>
            public NCODecimal InterestBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Court Cost after payment is posted
            /// </summary>
            public NCODecimal CourtCostBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Attorney Fees after payment is posted
            /// </summary>
            public NCODecimal AttorneyFeesBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Other Fee after payment is posted
            /// </summary>
            public NCODecimal OtherFeeBalance { get; private set; }
            /// <summary>
            /// Balance remaining in NSF Fees after payment is posted
            /// </summary>
            public NCODecimal NSFFeeBalance { get; private set; }
            /// <summary>
            /// Balance remaining in Collection Charge Amount after payment is posted
            /// </summary>
            public NCODecimal CollectionChargeBalance { get; private set; }
            /// <summary>
            /// Balance remaining in this bucket after payment is posted
            /// </summary>
            public NCODecimal Bucket8Balance { get; private set; }
            /// <summary>
            /// Balance remaining in this bucket after payment is posted
            /// </summary>
            public NCODecimal Bucket9Balance { get; private set; }
            /// <summary>
            /// Balance remaining in this bucket after payment is posted
            /// </summary>
            public NCODecimal Bucket10Balance { get; private set; }
            /// <summary>
            /// Amount Paid to Date after Payment is Applied
            /// </summary>
            public NCODecimal TotalPaid { get; private set; }
            /// <summary>
            /// Interest Accrued to Date after Payment is Applied
            /// </summary>
            public NCODecimal TotalInterest { get; private set; }
            /// <summary>
            /// Interest rate on account at time of payment
            /// </summary>
            public NCODecimal CurrentInterestRate { get; private set; }
            /// <summary>
            /// Court Cost Spent to Date
            /// </summary>
            public NCODecimal TotalCourtCosts { get; private set; }
            #endregion

            public RecordType07(string Record) : base(Record)
            {
                try
                {
                    this.CreditorGroup = new NCOString(100) { DataString = Record.Substring(22) };
                    this.Creditor = new NCOString(8) { DataString = Record.Substring(122) };
                    this.BatchNumber = new NCOString(10) { DataString = Record.Substring(130) };
                    this.ReceiverInvoiceNumber = new NCOString(20) { DataString = Record.Substring(140) };
                    this.FinancialPostedDate = new NCODate() { DataString = Record.Substring(160) };
                    this.FinancialAmount = new NCODecimal(12, 2) { DataString = Record.Substring(168) };
                    this.FinancialType = new NCOEnum<FinancialType>(2) { DataString = Record.Substring(180) };
                    this.FinancialUniqueID = new NCOString(16) { DataString = Record.Substring(182) };
                    this.PrincipalAmount = new NCODecimal(12, 2) { DataString = Record.Substring(198) };
                    this.InterestAmount = new NCODecimal(12, 2) { DataString = Record.Substring(210) };
                    this.CourtCostAmount = new NCODecimal(12, 2) { DataString = Record.Substring(222) };
                    this.AttoneyFeesAmount = new NCODecimal(12, 2) { DataString = Record.Substring(234) };
                    this.OtherFeeAmount = new NCODecimal(12, 2) { DataString = Record.Substring(246) };
                    this.NSFFeeAmount = new NCODecimal(12, 2) { DataString = Record.Substring(258) };
                    this.CollectionChargeAmount = new NCODecimal(12, 2) { DataString = Record.Substring(270) };
                    this.Bucket8Amount = new NCODecimal(12, 2) { DataString = Record.Substring(282) };
                    this.Bucket9Amount = new NCODecimal(12, 2) { DataString = Record.Substring(294) };
                    this.Bucket10Amount = new NCODecimal(12, 2) { DataString = Record.Substring(306) };
                    this.CourtCostType = new NCOEnum<CourtCostType>(3) { DataString = Record.Substring(318) };
                    this.CommissionAmount = new NCODecimal(12, 2) { DataString = Record.Substring(321) };
                    this.CommissionPercent = new NCODecimal(8, 4) { DataString = Record.Substring(333) };
                    this.TotalRemainingBalance = new NCODecimal(12, 2) { DataString = Record.Substring(341) };
                    this.PrincipalBalance = new NCODecimal(12, 2) { DataString = Record.Substring(353) };
                    this.InterestBalance = new NCODecimal(12, 2) { DataString = Record.Substring(365) };
                    this.CourtCostBalance = new NCODecimal(12, 2) { DataString = Record.Substring(377) };
                    this.AttorneyFeesBalance = new NCODecimal(12, 2) { DataString = Record.Substring(389) };
                    this.OtherFeeBalance = new NCODecimal(12, 2) { DataString = Record.Substring(401) };
                    this.NSFFeeBalance = new NCODecimal(12, 2) { DataString = Record.Substring(413) };
                    this.CollectionChargeBalance = new NCODecimal(12, 2) { DataString = Record.Substring(425) };
                    this.Bucket8Balance = new NCODecimal(12, 2) { DataString = Record.Substring(437) };
                    this.Bucket9Balance = new NCODecimal(12, 2) { DataString = Record.Substring(449) };
                    this.Bucket10Balance = new NCODecimal(12, 2) { DataString = Record.Substring(461) };
                    this.TotalPaid = new NCODecimal(12, 2) { DataString = Record.Substring(473) };
                    this.TotalInterest = new NCODecimal(12, 2) { DataString = Record.Substring(485) };
                    this.CurrentInterestRate = new NCODecimal(8, 4) { DataString = Record.Substring(497) };
                    this.TotalCourtCosts = new NCODecimal(12, 2) { DataString = Record.Substring(505) };
                }
                catch
                {
                    if (this.CreditorGroup == null) this.CreditorGroup = new NCOString(100);
                    if (this.Creditor == null) this.Creditor = new NCOString(8);
                    if (this.BatchNumber == null) this.BatchNumber = new NCOString(10);
                    if (this.ReceiverInvoiceNumber == null) this.ReceiverInvoiceNumber = new NCOString(20);
                    if (this.FinancialPostedDate == null) this.FinancialPostedDate = new NCODate();
                    if (this.FinancialAmount == null) this.FinancialAmount = new NCODecimal(12, 2);
                    if (this.FinancialType == null) this.FinancialType = new NCOEnum<FinancialType>(2);
                    if (this.FinancialUniqueID == null) this.FinancialUniqueID = new NCOString(16);
                    if (this.PrincipalAmount == null) this.PrincipalAmount = new NCODecimal(12, 2);
                    if (this.InterestAmount == null) this.InterestAmount = new NCODecimal(12, 2);
                    if (this.CourtCostAmount == null) this.CourtCostAmount = new NCODecimal(12, 2);
                    if (this.AttoneyFeesAmount == null) this.AttoneyFeesAmount = new NCODecimal(12, 2);
                    if (this.OtherFeeAmount == null) this.OtherFeeAmount = new NCODecimal(12, 2);
                    if (this.NSFFeeAmount == null) this.NSFFeeAmount = new NCODecimal(12, 2);
                    if (this.CollectionChargeAmount == null) this.CollectionChargeAmount = new NCODecimal(12, 2);
                    if (this.Bucket8Amount == null) this.Bucket8Amount = new NCODecimal(12, 2);
                    if (this.Bucket9Amount == null) this.Bucket9Amount = new NCODecimal(12, 2);
                    if (this.Bucket10Amount == null) this.Bucket10Amount = new NCODecimal(12, 2);
                    if (this.CourtCostType == null) this.CourtCostType = new NCOEnum<CourtCostType>(3);
                    if (this.CommissionAmount == null) this.CommissionAmount = new NCODecimal(12, 2);
                    if (this.CommissionPercent == null) this.CommissionPercent = new NCODecimal(8, 4);
                    if (this.TotalRemainingBalance == null) this.TotalRemainingBalance = new NCODecimal(12, 2);
                    if (this.PrincipalBalance == null) this.PrincipalBalance = new NCODecimal(12, 2);
                    if (this.InterestBalance == null) this.InterestBalance = new NCODecimal(12, 2);
                    if (this.CourtCostBalance == null) this.CourtCostBalance = new NCODecimal(12, 2);
                    if (this.AttorneyFeesBalance == null) this.AttorneyFeesBalance = new NCODecimal(12, 2);
                    if (this.OtherFeeBalance == null) this.OtherFeeBalance = new NCODecimal(12, 2);
                    if (this.NSFFeeBalance == null) this.NSFFeeBalance = new NCODecimal(12, 2);
                    if (this.CollectionChargeBalance == null) this.CollectionChargeBalance = new NCODecimal(12, 2);
                    if (this.Bucket8Balance == null) this.Bucket8Balance = new NCODecimal(12, 2);
                    if (this.Bucket9Balance == null) this.Bucket9Balance = new NCODecimal(12, 2);
                    if (this.Bucket10Balance == null) this.Bucket10Balance = new NCODecimal(12, 2);
                    if (this.TotalPaid == null) this.TotalPaid = new NCODecimal(12, 2);
                    if (this.TotalInterest == null) this.TotalInterest = new NCODecimal(12, 2);
                    if (this.CurrentInterestRate == null) this.CurrentInterestRate = new NCODecimal(8, 4);
                    if (this.TotalCourtCosts == null) this.TotalCourtCosts = new NCODecimal(12, 2);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}",
                    this.RecordType,
                    this.AccountNumber,
                    this.CreditorGroup,
                    this.Creditor,
                    this.BatchNumber,
                    this.ReceiverInvoiceNumber,
                    this.FinancialPostedDate,
                    this.FinancialAmount,
                    this.FinancialType,
                    this.FinancialUniqueID,
                    this.PrincipalAmount,
                    this.InterestAmount,
                    this.CourtCostAmount,
                    this.AttoneyFeesAmount,
                    this.OtherFeeAmount,
                    this.NSFFeeAmount,
                    this.CollectionChargeAmount,
                    this.Bucket8Amount,
                    this.Bucket9Amount,
                    this.Bucket10Amount,
                    this.CourtCostType,
                    this.CommissionAmount,
                    this.CommissionPercent,
                    this.TotalRemainingBalance,
                    this.PrincipalBalance,
                    this.InterestBalance,
                    this.CourtCostBalance,
                    this.AttorneyFeesBalance,
                    this.OtherFeeBalance,
                    this.NSFFeeBalance,
                    this.CollectionChargeBalance,
                    this.Bucket8Balance,
                    this.Bucket9Balance,
                    this.Bucket10Balance,
                    this.TotalPaid,
                    this.TotalInterest,
                    this.CurrentInterestRate,
                    this.TotalCourtCosts);
            }
        }
        /// <summary>
        /// Maintenance Record
        /// <para>The Maintenance Record serves as the vehicle for communicating all transaction codes to NCO. The makeup of the Sub Transaction Area will be defined differently for each transaction code as referenced in the Transaction Codes section of this document.</para>
        /// </summary>
        public class RecordType08 : RecordBase
        {
            #region Public Properties
            public NCOEnum<TransactionCodes, TransactionCodes> TransactionCode { get; private set; }
            public NCODateTime TransactionDateTime { get; private set; }
            public NCOEnum<RelationshipCode> RelationshipCode { get; private set; }
            public NCOString Filler { get; private set; }
            public NCOString SubTransactionArea { get; private set; }
            #endregion

            public RecordType08(string Record) : base(Record)
            {
                try
                {
                    this.TransactionCode = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(22, 5) };
                    this.TransactionDateTime = new NCODateTime() { DataString = Record.Substring(27, 16) };
                    this.RelationshipCode = new NCOEnum<RelationshipCode>(3) { DataString = Record.Substring(43, 3) };
                    this.Filler = new NCOString(4) { DataString = Record.Substring(46, 4) };
                    this.SubTransactionArea = new NCOString(500) { DataString = Record.Substring(50, 500) };
                }
                catch
                {
                    if (this.TransactionCode == null) this.TransactionCode = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.TransactionDateTime == null) this.TransactionDateTime = new NCODateTime();
                    if (this.RelationshipCode == null) this.RelationshipCode = new NCOEnum<RelationshipCode>(3);
                    if (this.Filler == null) this.Filler = new NCOString(4);
                    if (this.SubTransactionArea == null) this.SubTransactionArea = new NCOString(500);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    this.RecordType,
                    this.AccountNumber,
                    this.TransactionCode,
                    this.TransactionDateTime,
                    this.RelationshipCode,
                    this.Filler,
                    this.SubTransactionArea);
            }
        }
        /// <summary>
        /// Maintenance Record
        /// <para>The Maintenance Record serves as the vehicle for communicating all transaction codes to NCO. The makeup of the Sub Transaction Area will be defined differently for each transaction code as referenced in the Transaction Codes section of this document.</para>
        /// </summary>
        /// <typeparam name="T">Transaction Code Type</typeparam>
        public class RecordType08<T> : RecordBase where T : TransactionCode
        {
            #region Public Properties
            public NCOEnum<TransactionCodes, TransactionCodes> TransactionCode { get; private set; }
            public NCODateTime TransactionDateTime { get; private set; }
            public NCOEnum<RelationshipCode> RelationshipCode { get; private set; }
            public NCOString Filler { get; private set; }
            public T TransactionInfo { get; private set; }
            #endregion

            public RecordType08(string AccountNumber, DateTime TransactionDate, RelationshipCode Relationship, T TransactionInfo) : base(Enums.RecordTypes.RecordType08, AccountNumber)
            {
                this.TransactionCode = new NCOEnum<Enums.TransactionCodes, Enums.TransactionCodes>(5) { DataString = typeof(T).Name };
                this.TransactionDateTime = new NCODateTime() { Value = TransactionDate };
                this.RelationshipCode = new NCOEnum<RelationshipCode>(3) { Value = Relationship };
                this.Filler = new NCOString(4);
                this.TransactionInfo = TransactionInfo;
            }
            public RecordType08(string Record) : base(Record)
            {
                try
                {
                    this.TransactionCode = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(22, 5) };
                    this.TransactionDateTime = new NCODateTime() { DataString = Record.Substring(27, 16) };
                    this.RelationshipCode = new NCOEnum<RelationshipCode>(3) { DataString = Record.Substring(43, 3) };
                    this.Filler = new NCOString(4) { DataString = Record.Substring(46, 4) };
                    Type t = typeof(T);
                    this.TransactionInfo = (T)t.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record.Substring(50) });
                }
                catch
                {
                    if (this.TransactionCode == null) this.TransactionCode = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.TransactionDateTime == null) this.TransactionDateTime = new NCODateTime();
                    if (this.RelationshipCode == null) this.RelationshipCode = new NCOEnum<RelationshipCode>(3);
                    if (this.Filler == null) this.Filler = new NCOString(4);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    this.RecordType,
                    this.AccountNumber,
                    this.TransactionCode,
                    this.TransactionDateTime,
                    this.RelationshipCode,
                    this.Filler,
                    this.TransactionInfo);
            }
        }
        /// <summary>
        /// Reconciliation Record
        /// <para>The Reconciliation Record contains account level details that the receiver sends to NCO to reconcile with their system.</para>
        /// </summary>
        public class RecordType09 : RecordBase
        {
            #region Public Properties
            /// <summary>
            /// Creditor ID
            /// </summary>
            public NCOString Creditor { get; private set; }
            /// <summary>
            /// Sum of all outstanding balance buckets owed
            /// </summary>
            public NCODecimal TotalBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding Principal balance owed
            /// </summary>
            public NCODecimal PrincipalBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding Interest balance owed
            /// </summary>
            public NCODecimal InterestBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding Court Cost owed
            /// </summary>
            public NCODecimal CourtCostBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding Attorney Fees owed
            /// </summary>
            public NCODecimal AttorneyFeesBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding Other Balance owed
            /// </summary>
            public NCODecimal OtherBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding NSF Fees owed
            /// </summary>
            public NCODecimal NSFBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding collection charges owed
            /// </summary>
            public NCODecimal CollectionChargeBalance { get; private set; }
            /// <summary>
            /// Sum of current outstanding bucket 8 owed
            /// </summary>
            public NCODecimal Bucket8Balance { get; private set; }
            /// <summary>
            /// Sum of current outstanding bucket 9 owed
            /// </summary>
            public NCODecimal Bucket9Balance { get; private set; }
            /// <summary>
            /// Sum of current outstanding bucket 10 owed
            /// </summary>
            public NCODecimal Bucket10Balance { get; private set; }
            /// <summary>
            /// Amount of the last payment received by the receiver
            /// </summary>
            public NCODecimal LastPaymentAmount { get; private set; }
            /// <summary>
            /// Date of the last payment received by the receiver
            /// </summary>
            public NCODate LastPaymentDate { get; private set; }
            /// <summary>
            /// Date suit filed
            /// </summary>
            public NCODate SuitFiledDate { get; private set; }
            /// <summary>
            /// Suit case number
            /// </summary>
            public NCOString CaseNumber { get; private set; }
            /// <summary>
            /// Name of jurisdiction suit was filed in
            /// </summary>
            public NCOString Jurisdiction { get; private set; }
            /// <summary>
            /// Name of the court suit was filed in
            /// </summary>
            public NCOString Court { get; private set; }
            /// <summary>
            /// Date of judgment
            /// </summary>
            public NCODate JudgmentDate { get; private set; }
            /// <summary>
            /// Docket number assigned to judgment
            /// </summary>
            public NCOString DocketNumber { get; private set; }
            /// <summary>
            /// Expiration date of judgment
            /// </summary>
            public NCODate JudgmentExpirationDate { get; private set; }
            /// <summary>
            /// Total Judgment awarded amount
            /// </summary>
            public NCODecimal AwardedAmount { get; private set; }
            /// <summary>
            /// Judgment principal awarded amount
            /// </summary>
            public NCODecimal AwardedPrincipal { get; private set; }
            /// <summary>
            /// Judgment interest awarded amount
            /// </summary>
            public NCODecimal AwardedInterest { get; private set; }
            /// <summary>
            /// Judgment fees awarded amount
            /// </summary>
            public NCODecimal AwardedFee { get; private set; }
            /// <summary>
            /// Judgment cost awarded amount
            /// </summary>
            public NCODecimal AwardedCost { get; private set; }
            /// <summary>
            /// Amount of judgment balance on which interest can be charged
            /// </summary>
            public NCODecimal InterestChargingBalance { get; private set; }
            /// <summary>
            /// Amount of judgment balance on which interest cannot be charged
            /// </summary>
            public NCODecimal NonInterestChargingBalance { get; private set; }
            /// <summary>
            /// Date the most recent bank garnishment was filed
            /// </summary>
            public NCODate BankGarnishmentDate { get; private set; }
            /// <summary>
            /// Date the most recent wage garnishment was filed
            /// </summary>
            public NCODate WageGarnishmentDate { get; private set; }
            /// <summary>
            /// Interest rate charged on consumer account
            /// </summary>
            public NCODecimal InterestRate { get; private set; }
            /// <summary>
            /// Date the receiver last updated interest amount
            /// </summary>
            public NCODate LastInterestUpdate { get; private set; }
            /// <summary>
            /// Please refer to RecordTypes.NCO.Enums.PaymentAllocation for code list
            /// </summary>
            public NCOEnum<PaymentAllocation> PaymentAllocation { get; private set; }
            #endregion

            public RecordType09(string Record) : base(Record)
            {
                try
                {
                    this.Creditor = new NCOString(8) { DataString = Record.Substring(22) };
                    this.TotalBalance = new NCODecimal(12, 2) { DataString = Record.Substring(30) };
                    this.PrincipalBalance = new NCODecimal(12, 2) { DataString = Record.Substring(42) };
                    this.InterestBalance = new NCODecimal(12, 2) { DataString = Record.Substring(54) };
                    this.CourtCostBalance = new NCODecimal(12, 2) { DataString = Record.Substring(66) };
                    this.AttorneyFeesBalance = new NCODecimal(12, 2) { DataString = Record.Substring(78) };
                    this.OtherBalance = new NCODecimal(12, 2) { DataString = Record.Substring(90) };
                    this.NSFBalance = new NCODecimal(12, 2) { DataString = Record.Substring(102) };
                    this.CollectionChargeBalance = new NCODecimal(12, 2) { DataString = Record.Substring(114) };
                    this.Bucket8Balance = new NCODecimal(12, 2) { DataString = Record.Substring(126) };
                    this.Bucket9Balance = new NCODecimal(12, 2) { DataString = Record.Substring(138) };
                    this.Bucket10Balance = new NCODecimal(12, 2) { DataString = Record.Substring(150) };
                    this.LastPaymentAmount = new NCODecimal(12, 2) { DataString = Record.Substring(162) };
                    this.LastPaymentDate = new NCODate() { DataString = Record.Substring(174) };
                    this.SuitFiledDate = new NCODate() { DataString = Record.Substring(182) };
                    this.CaseNumber = new NCOString(30) { DataString = Record.Substring(190) };
                    this.Jurisdiction = new NCOString(80) { DataString = Record.Substring(220) };
                    this.Court = new NCOString(80) { DataString = Record.Substring(300) };
                    this.JudgmentDate = new NCODate() { DataString = Record.Substring(380) };
                    this.DocketNumber = new NCOString(30) { DataString = Record.Substring(388) };
                    this.JudgmentExpirationDate = new NCODate() { DataString = Record.Substring(418) };
                    this.AwardedAmount = new NCODecimal(12, 2) { DataString = Record.Substring(426) };
                    this.AwardedPrincipal = new NCODecimal(12, 2) { DataString = Record.Substring(438) };
                    this.AwardedInterest = new NCODecimal(12, 2) { DataString = Record.Substring(450) };
                    this.AwardedFee = new NCODecimal(12, 2) { DataString = Record.Substring(462) };
                    this.AwardedCost = new NCODecimal(12, 2) { DataString = Record.Substring(474) };
                    this.InterestChargingBalance = new NCODecimal(12, 2) { DataString = Record.Substring(486) };
                    this.NonInterestChargingBalance = new NCODecimal(12, 2) { DataString = Record.Substring(498) };
                    this.BankGarnishmentDate = new NCODate() { DataString = Record.Substring(510) };
                    this.WageGarnishmentDate = new NCODate() { DataString = Record.Substring(518) };
                    this.InterestRate = new NCODecimal(8, 4) { DataString = Record.Substring(526) };
                    this.LastInterestUpdate = new NCODate() { DataString = Record.Substring(534) };
                    this.PaymentAllocation = new NCOEnum<PaymentAllocation>(10) { DataString = Record.Substring(542) };
                }
                catch
                {
                    if (this.Creditor == null) this.Creditor = new NCOString(8);
                    if (this.TotalBalance == null) this.TotalBalance = new NCODecimal(12, 2);
                    if (this.PrincipalBalance == null) this.PrincipalBalance = new NCODecimal(12, 2);
                    if (this.InterestBalance == null) this.InterestBalance = new NCODecimal(12, 2);
                    if (this.CourtCostBalance == null) this.CourtCostBalance = new NCODecimal(12, 2);
                    if (this.AttorneyFeesBalance == null) this.AttorneyFeesBalance = new NCODecimal(12, 2);
                    if (this.OtherBalance == null) this.OtherBalance = new NCODecimal(12, 2);
                    if (this.NSFBalance == null) this.NSFBalance = new NCODecimal(12, 2);
                    if (this.CollectionChargeBalance == null) this.CollectionChargeBalance = new NCODecimal(12, 2);
                    if (this.Bucket8Balance == null) this.Bucket8Balance = new NCODecimal(12, 2);
                    if (this.Bucket9Balance == null) this.Bucket9Balance = new NCODecimal(12, 2);
                    if (this.Bucket10Balance == null) this.Bucket10Balance = new NCODecimal(12, 2);
                    if (this.LastPaymentAmount == null) this.LastPaymentAmount = new NCODecimal(12, 2);
                    if (this.LastPaymentDate == null) this.LastPaymentDate = new NCODate();
                    if (this.SuitFiledDate == null) this.SuitFiledDate = new NCODate();
                    if (this.CaseNumber == null) this.CaseNumber = new NCOString(30);
                    if (this.Jurisdiction == null) this.Jurisdiction = new NCOString(80);
                    if (this.Court == null) this.Court = new NCOString(80);
                    if (this.JudgmentDate == null) this.JudgmentDate = new NCODate();
                    if (this.DocketNumber == null) this.DocketNumber = new NCOString(30);
                    if (this.JudgmentExpirationDate == null) this.JudgmentExpirationDate = new NCODate();
                    if (this.AwardedAmount == null) this.AwardedAmount = new NCODecimal(12, 2);
                    if (this.AwardedPrincipal == null) this.AwardedPrincipal = new NCODecimal(12, 2);
                    if (this.AwardedInterest == null) this.AwardedInterest = new NCODecimal(12, 2);
                    if (this.AwardedFee == null) this.AwardedFee = new NCODecimal(12, 2);
                    if (this.AwardedCost == null) this.AwardedCost = new NCODecimal(12, 2);
                    if (this.InterestChargingBalance == null) this.InterestChargingBalance = new NCODecimal(12, 2);
                    if (this.NonInterestChargingBalance == null) this.NonInterestChargingBalance = new NCODecimal(12, 2);
                    if (this.BankGarnishmentDate == null) this.BankGarnishmentDate = new NCODate();
                    if (this.WageGarnishmentDate == null) this.WageGarnishmentDate = new NCODate();
                    if (this.InterestRate == null) this.InterestRate = new NCODecimal(8, 4);
                    if (this.LastInterestUpdate == null) this.LastInterestUpdate = new NCODate();
                    if (this.PaymentAllocation == null) this.PaymentAllocation = new NCOEnum<PaymentAllocation>(10);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}",
                    this.RecordType,
                    this.AccountNumber,
                    this.Creditor,
                    this.TotalBalance,
                    this.PrincipalBalance,
                    this.InterestBalance,
                    this.CourtCostBalance,
                    this.AttorneyFeesBalance,
                    this.OtherBalance,
                    this.NSFBalance,
                    this.CollectionChargeBalance,
                    this.Bucket8Balance,
                    this.Bucket9Balance,
                    this.Bucket10Balance,
                    this.LastPaymentAmount,
                    this.LastPaymentDate,
                    this.SuitFiledDate,
                    this.CaseNumber,
                    this.Jurisdiction,
                    this.Court,
                    this.JudgmentDate,
                    this.DocketNumber,
                    this.JudgmentExpirationDate,
                    this.AwardedAmount,
                    this.AwardedPrincipal,
                    this.AwardedInterest,
                    this.AwardedFee,
                    this.AwardedCost,
                    this.InterestChargingBalance,
                    this.NonInterestChargingBalance,
                    this.BankGarnishmentDate,
                    this.WageGarnishmentDate,
                    this.InterestRate,
                    this.LastInterestUpdate,
                    this.PaymentAllocation);
            }
        }
        /// <summary>
        /// Payment / Maintenance Reject Record
        /// <para>The Payment / Maintenance Reject Record details all the particular reject reasons for a single record. All rejects contained will refer to the “Receiver Sent Information” field. Each reject maintenance / payment record will have its own Record 10.</para>
        /// </summary>
        public class RecordType10 : Record
        {
            #region Public Properties
            /// <summary>
            /// This will be the exact format of the pmt/mnt record we received
            /// </summary>
            public NCOString RecieverSentInformation { get; private set; }
            /// <summary>
            /// File name information was in
            /// </summary>
            public NCOString OriginalFileName { get; private set; }
            /// <summary>
            /// The code for the first reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode01 { get; private set; }
            /// <summary>
            /// The reason for the first reject on this transaction
            /// </summary>
            public NCOString RejectReason01 { get; private set; }
            /// <summary>
            /// The code for the second reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode02 { get; private set; }
            /// <summary>
            /// The reason for the second reject on this transaction
            /// </summary>
            public NCOString RejectReason02 { get; private set; }
            /// <summary>
            /// The code for the third reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode03 { get; private set; }
            /// <summary>
            /// The reason for the third reject on this transaction
            /// </summary>
            public NCOString RejectReason03 { get; private set; }
            /// <summary>
            /// The code for the fourth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode04 { get; private set; }
            /// <summary>
            /// The reason for the fourth reject on this transaction
            /// </summary>
            public NCOString RejectReason04 { get; private set; }
            /// <summary>
            /// The code for the fifth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode05 { get; private set; }
            /// <summary>
            /// The reason for the fifth reject on this transaction
            /// </summary>
            public NCOString RejectReason05 { get; private set; }
            /// <summary>
            /// The code for the sixth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode06 { get; private set; }
            /// <summary>
            /// The reason for the sixth reject on this transaction
            /// </summary>
            public NCOString RejectReason06 { get; private set; }
            /// <summary>
            /// The code for the seventh reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode07 { get; private set; }
            /// <summary>
            /// The reason for the seventh reject on this transaction
            /// </summary>
            public NCOString RejectReason07 { get; private set; }
            /// <summary>
            /// The code for the eighth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode08 { get; private set; }
            /// <summary>
            /// The reason for the eighth reject on this transaction
            /// </summary>
            public NCOString RejectReason08 { get; private set; }
            /// <summary>
            /// The code for the ninth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode09 { get; private set; }
            /// <summary>
            /// The reason for the ninth reject on this transaction
            /// </summary>
            public NCOString RejectReason09 { get; private set; }
            /// <summary>
            /// The code for the tenth reject on this transaction
            /// </summary>
            public NCOEnum<TransactionCodes, TransactionCodes> RejectCode10 { get; private set; }
            /// <summary>
            /// The reason for the tenth reject on this transaction
            /// </summary>
            public NCOString RejectReason10 { get; private set; }
            #endregion

            public RecordType10(string Record) : base(Record)
            {
                try
                {
                    this.RecieverSentInformation = new NCOString(600) { DataString = Record.Substring(2, 600) };
                    this.OriginalFileName = new NCOString(26) { DataString = Record.Substring(602) };
                    this.RejectCode01 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(628) };
                    this.RejectReason01 = new NCOString(100) { DataString = Record.Substring(633) };
                    this.RejectCode02 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(733) };
                    this.RejectReason02 = new NCOString(100) { DataString = Record.Substring(738) };
                    this.RejectCode03 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(838) };
                    this.RejectReason03 = new NCOString(100) { DataString = Record.Substring(843) };
                    this.RejectCode04 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(943) };
                    this.RejectReason04 = new NCOString(100) { DataString = Record.Substring(948) };
                    this.RejectCode05 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1048) };
                    this.RejectReason05 = new NCOString(100) { DataString = Record.Substring(1053) };
                    this.RejectCode06 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1153) };
                    this.RejectReason06 = new NCOString(100) { DataString = Record.Substring(1158) };
                    this.RejectCode07 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1258) };
                    this.RejectReason07 = new NCOString(100) { DataString = Record.Substring(1263) };
                    this.RejectCode08 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1363) };
                    this.RejectReason08 = new NCOString(100) { DataString = Record.Substring(1368) };
                    this.RejectCode09 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1468) };
                    this.RejectReason09 = new NCOString(100) { DataString = Record.Substring(1473) };
                    this.RejectCode10 = new NCOEnum<TransactionCodes, TransactionCodes>(5) { DataString = Record.Substring(1573) };
                    this.RejectReason10 = new NCOString(100) { DataString = Record.Substring(1578) };
                }
                catch
                {
                    if (this.RecieverSentInformation == null) this.RecieverSentInformation = new NCOString(600);
                    if (this.OriginalFileName == null) this.OriginalFileName = new NCOString(26);
                    if (this.RejectCode01 == null) this.RejectCode01 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason01 == null) this.RejectReason01 = new NCOString(100);
                    if (this.RejectCode02 == null) this.RejectCode02 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason02 == null) this.RejectReason02 = new NCOString(100);
                    if (this.RejectCode03 == null) this.RejectCode03 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason03 == null) this.RejectReason03 = new NCOString(100);
                    if (this.RejectCode04 == null) this.RejectCode04 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason04 == null) this.RejectReason04 = new NCOString(100);
                    if (this.RejectCode05 == null) this.RejectCode05 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason05 == null) this.RejectReason05 = new NCOString(100);
                    if (this.RejectCode06 == null) this.RejectCode06 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason06 == null) this.RejectReason06 = new NCOString(100);
                    if (this.RejectCode07 == null) this.RejectCode07 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason07 == null) this.RejectReason07 = new NCOString(100);
                    if (this.RejectCode08 == null) this.RejectCode08 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason08 == null) this.RejectReason08 = new NCOString(100);
                    if (this.RejectCode09 == null) this.RejectCode09 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason09 == null) this.RejectReason09 = new NCOString(100);
                    if (this.RejectCode10 == null) this.RejectCode10 = new NCOEnum<TransactionCodes, TransactionCodes>(5);
                    if (this.RejectReason10 == null) this.RejectReason10 = new NCOString(100);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}",
                    this.RecordType,
                    this.RecieverSentInformation,
                    this.OriginalFileName,
                    this.RejectCode01,
                    this.RejectReason01,
                    this.RejectCode02,
                    this.RejectReason02,
                    this.RejectCode03,
                    this.RejectReason03,
                    this.RejectCode04,
                    this.RejectReason04,
                    this.RejectCode05,
                    this.RejectReason05,
                    this.RejectCode06,
                    this.RejectReason06,
                    this.RejectCode07,
                    this.RejectReason07,
                    this.RejectCode08,
                    this.RejectReason08,
                    this.RejectCode09,
                    this.RejectReason09,
                    this.RejectCode10,
                    this.RejectReason10);
            }
        }
        /// <summary>
        /// Trailer
        /// <para>The Trailer Record acts as the end record in any series of records</para>
        /// </summary>
        public class RecordType99 : Record
        {
            #region Public Properties
            public NCODecimal TotalAmountOfAccounts { get; private set; }
            public NCONumber TotalRecordType01 { get; private set; }
            public NCONumber TotalRecordType02 { get; private set; }
            public NCONumber TotalRecordType03 { get; private set; }
            public NCONumber TotalRecordType04 { get; private set; }
            public NCONumber TotalRecordType05 { get; private set; }
            public NCONumber TotalRecordType06 { get; private set; }
            public NCONumber TotalRecordType07 { get; private set; }
            public NCONumber TotalRecordType08 { get; private set; }
            public NCONumber TotalRecordType09 { get; private set; }
            public NCONumber TotalRecordType10 { get; private set; }
            #endregion

            public RecordType99() : base(Enums.RecordTypes.RecordType99)
            {
                this.TotalAmountOfAccounts = new NCODecimal(12, 2);
                this.TotalRecordType01 = new NCONumber(10);
                this.TotalRecordType02 = new NCONumber(10);
                this.TotalRecordType03 = new NCONumber(10);
                this.TotalRecordType04 = new NCONumber(10);
                this.TotalRecordType05 = new NCONumber(10);
                this.TotalRecordType06 = new NCONumber(10);
                this.TotalRecordType07 = new NCONumber(10);
                this.TotalRecordType08 = new NCONumber(10);
                this.TotalRecordType09 = new NCONumber(10);
                this.TotalRecordType10 = new NCONumber(10);
            }
            public RecordType99(string Record) : base(Record)
            {
                this.TotalAmountOfAccounts = new NCODecimal(12, 2) { DataString = Record.Substring(2, 12) };
                this.TotalRecordType01 = new NCONumber(10) { DataString = Record.Substring(14, 10) };
                this.TotalRecordType02 = new NCONumber(10) { DataString = Record.Substring(24, 10) };
                this.TotalRecordType03 = new NCONumber(10) { DataString = Record.Substring(34, 10) };
                this.TotalRecordType04 = new NCONumber(10) { DataString = Record.Substring(44, 10) };
                this.TotalRecordType05 = new NCONumber(10) { DataString = Record.Substring(54, 10) };
                this.TotalRecordType06 = new NCONumber(10) { DataString = Record.Substring(64, 10) };
                this.TotalRecordType07 = new NCONumber(10) { DataString = Record.Substring(74, 10) };
                this.TotalRecordType08 = new NCONumber(10) { DataString = Record.Substring(84, 10) };
                this.TotalRecordType09 = new NCONumber(10) { DataString = Record.Substring(94, 10) };
                this.TotalRecordType10 = new NCONumber(10) { DataString = Record.Substring(104, 10) };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
                    this.RecordType,
                    this.TotalAmountOfAccounts,
                    this.TotalRecordType01,
                    this.TotalRecordType02,
                    this.TotalRecordType03,
                    this.TotalRecordType04,
                    this.TotalRecordType05,
                    this.TotalRecordType06,
                    this.TotalRecordType07,
                    this.TotalRecordType08,
                    this.TotalRecordType09,
                    this.TotalRecordType10);
            }
        }
    }
}
