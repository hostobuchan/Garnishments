using RecordTypes.PLX.Base;
using RecordTypes.PLX.Enums;
using System;
using System.Linq;

namespace RecordTypes
{
    // PLX is the primary record type used by Resurgent Capitol Services
    namespace PLX
    {
        public class FileHeader : RecordTypeBase
        {
            public int HeaderVersion { get; private set; }
            public int FileVersion { get; private set; }
            public string BusProcType { get; private set; }
            public int Source { get; private set; }
            public int Destination { get; private set; }
            public DateTime CreationDate { get; private set; }
            public string FileID { get; private set; }

            public FileHeader(FileHeader OriginalHeader) : base(Types.FileHeader)
            {
                this.HeaderVersion = OriginalHeader.HeaderVersion;
                this.FileVersion = OriginalHeader.FileVersion;
                this.BusProcType = OriginalHeader.BusProcType;
                this.Source = OriginalHeader.Source;
                this.Destination = OriginalHeader.Destination;
                this.CreationDate = DateTime.Now;
                this.FileID = OriginalHeader.FileID;
            }
            public FileHeader(string Record) : base(Record)
            {
                this.HeaderVersion = int.Parse(Record.Split('\t')[1]);
                this.FileVersion = int.Parse(Record.Split('\t')[2]);
                this.BusProcType = Record.Split('\t')[3];
                this.Source = int.Parse(Record.Split('\t')[4]);
                this.Destination = int.Parse(Record.Split('\t')[5]);
                this.CreationDate = DateTime.ParseExact(Record.Split('\t')[6], "MM/dd/yyyy HH.mm.ss", System.Globalization.CultureInfo.CurrentCulture);
                if (Record.Split('\t').GetUpperBound(0) > 6)
                    this.FileID = Record.Split('\t')[7];
                else
                    this.FileID = "";
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                    base.ToString(),
                    this.HeaderVersion,
                    this.FileVersion,
                    this.BusProcType,
                    this.Source,
                    this.Destination,
                    this.CreationDate,
                    this.FileID);
            }
        }

        public class RecordHeader : RecordTypeBase
        {
            public int HeaderVersion { get; private set; }
            public Types HeaderRecordType { get; private set; }
            public int RecordVersion { get; private set; }
            public int NumOfRecords { get; set; }

            public RecordHeader(string Record) : base(Record)
            {
                this.HeaderVersion = int.Parse(Record.Split('\t')[1]);
                this.HeaderRecordType = (Types)Enum.Parse(typeof(TypeValues), Record.Split('\t')[2]);
                this.RecordVersion = int.Parse(Record.Split('\t')[3]);
                this.NumOfRecords = int.Parse(Record.Split('\t')[4]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                    base.ToString(),
                    this.HeaderVersion,
                    this.HeaderRecordType,
                    this.RecordVersion,
                    this.NumOfRecords);
            }
        }

        public class BorrowerRecord : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string SSN { get; private set; }
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Address { get; private set; }
            public string Address2 { get; private set; }
            public string City { get; private set; }
            public string State { get; private set; }
            public string Zip { get; private set; }
            public DateTime? DateOfBirth { get; private set; }
            public string HomePhone { get; private set; }
            public string WorkPhone { get; private set; }
            public string OtherPhone { get; private set; }
            public LanguageCodes LangCode { get; private set; }
            public string BankName { get; private set; }
            public Employment Employed { get; private set; }
            public bool VerifiedHomeOwner { get; private set; }

            public BorrowerRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = (BorrowerType)int.Parse(S[3]);
                this.SSN = S[4];
                this.FirstName = S[5];
                this.LastName = S[6];
                this.Address = S[7];
                this.Address2 = S[8];
                this.City = S[9];
                this.State = S[10];
                this.Zip = S[11];
                try { this.DateOfBirth = DateTime.ParseExact(S[13], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture); }
                catch { this.DateOfBirth = null; }
                this.HomePhone = S[14];
                this.WorkPhone = S[16];
                this.OtherPhone = S[18];
                try { this.LangCode = (LanguageCodes)int.Parse(S[22]); }
                catch { this.LangCode = LanguageCodes.English; }
                this.BankName = S[23];
                try { this.Employed = (Employment)Enum.Parse(typeof(EmploymentValues), S[24]); }
                catch { this.Employed = Employment.Unemployed; }
                this.VerifiedHomeOwner = S[25].Trim() == "Y" ? true : false;
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}",
                    base.ToString(),
                    ((int)this.BorrowerType),
                    this.SSN,
                    this.FirstName,
                    this.LastName,
                    this.Address,
                    this.Address2,
                    this.City,
                    this.State,
                    this.Zip,
                    this.DateOfBirth.HasValue ? this.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                    this.HomePhone,
                    this.WorkPhone,
                    this.OtherPhone,
                    ((int)this.LangCode),
                    this.BankName,
                    (EmploymentValues)this.Employed,
                    this.VerifiedHomeOwner ? "Y" : "N");
            }
        }

        public class AccountBalanceRecord : RecordBase
        {
            public DateTime? EffectiveDate { get; private set; }
            public decimal CurrentBalance { get; private set; }
            public decimal PrincipalCollected { get; private set; }
            public decimal PrincipalOwing { get; private set; }
            public decimal PrincipalBalance { get; private set; }
            public decimal AttyFeeCollected { get; private set; }
            public decimal AttyFeeOwing { get; private set; }
            public decimal FeeBalance { get; private set; }
            public decimal InterestCollected { get; private set; }
            public decimal InterestOwing { get; private set; }
            public decimal InterestBalance { get; private set; }
            public decimal CostCollected { get; private set; }
            public decimal CostOwing { get; private set; }
            public decimal CostBalance { get; private set; }
            public decimal CurrentInterestRate { get; set; }
            public decimal DailyInterestAccrual { get; set; }
            public decimal SuspendedInterest { get; private set; }
            public DateTime? LastPaymentDate { get; private set; }
            public decimal LastPaymentAmount { get; private set; }
            public DateTime? LastNSFDate { get; private set; }
            public decimal LastNSFAmount { get; private set; }
            public AccrualMethod AccrualMethod { get; private set; }

            public AccountBalanceRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.EffectiveDate = string.IsNullOrEmpty(S[3]) ? null : (DateTime?)DateTime.ParseExact(S[3], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.CurrentBalance = string.IsNullOrEmpty(S[4]) ? 0 : decimal.Parse(S[4]);
                this.PrincipalCollected = string.IsNullOrEmpty(S[5]) ? 0 : decimal.Parse(S[5]);
                this.PrincipalOwing = string.IsNullOrEmpty(S[6]) ? 0 : decimal.Parse(S[6]);
                this.PrincipalBalance = string.IsNullOrEmpty(S[7]) ? 0 : decimal.Parse(S[7]);
                this.AttyFeeCollected = string.IsNullOrEmpty(S[8]) ? 0 : decimal.Parse(S[8]);
                this.AttyFeeOwing = string.IsNullOrEmpty(S[9]) ? 0 : decimal.Parse(S[9]);
                this.FeeBalance = string.IsNullOrEmpty(S[10]) ? 0 : decimal.Parse(S[10]);
                this.InterestCollected = string.IsNullOrEmpty(S[11]) ? 0 : decimal.Parse(S[11]);
                this.InterestOwing = string.IsNullOrEmpty(S[12]) ? 0 : decimal.Parse(S[12]);
                this.InterestBalance = string.IsNullOrEmpty(S[13]) ? 0 : decimal.Parse(S[13]);
                this.CostCollected = string.IsNullOrEmpty(S[14]) ? 0 : decimal.Parse(S[14]);
                this.CostOwing = string.IsNullOrEmpty(S[15]) ? 0 : decimal.Parse(S[15]);
                this.CostBalance = string.IsNullOrEmpty(S[16]) ? 0 : decimal.Parse(S[16]);
                this.CurrentInterestRate = string.IsNullOrEmpty(S[17]) ? 0 : decimal.Parse(S[17]);
                this.DailyInterestAccrual = string.IsNullOrEmpty(S[18]) ? 0 : decimal.Parse(S[18]);
                this.SuspendedInterest = string.IsNullOrEmpty(S[19]) ? 0 : decimal.Parse(S[19]);
                this.LastPaymentDate = string.IsNullOrEmpty(S[20]) ? null : (DateTime?)DateTime.ParseExact(S[20], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.LastPaymentAmount = string.IsNullOrEmpty(S[21]) ? 0 : decimal.Parse(S[21]);
                this.LastNSFDate = string.IsNullOrEmpty(S[22]) ? null : (DateTime?)DateTime.ParseExact(S[22], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.LastNSFAmount = string.IsNullOrEmpty(S[23]) ? 0 : decimal.Parse(S[23]);
                this.AccrualMethod = string.IsNullOrEmpty(S[24]) ? AccrualMethod.Suspended : (AccrualMethod)int.Parse(S[24]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}",
                    base.ToString(),
                    EffectiveDate.HasValue ? EffectiveDate.Value.ToString("MM/dd/yyyy") : "",
                    CurrentBalance,
                    PrincipalCollected,
                    PrincipalOwing,
                    PrincipalBalance,
                    AttyFeeCollected,
                    AttyFeeOwing,
                    FeeBalance,
                    InterestCollected,
                    InterestOwing,
                    InterestBalance,
                    CostCollected,
                    CostOwing,
                    CostBalance,
                    CurrentInterestRate,
                    DailyInterestAccrual,
                    SuspendedInterest,
                    LastPaymentDate.HasValue ? LastPaymentDate.Value.ToString("MM/dd/yyyy") : "",
                    LastPaymentAmount,
                    LastNSFDate.HasValue ? LastNSFDate.Value.ToString("MM/dd/yyyy") : "",
                    LastNSFAmount,
                    (int)AccrualMethod);
            }
        }

        public class PlacementRecord : RecordBase
        {
            public decimal CommissionRate { get; private set; }
            public int PlaceBatchID { get; private set; }
            public string TrustAccountID { get; private set; }
            public DateTime? PutbackDeadline { get; private set; }
            public decimal SIFRate { get; private set; }

            public PlacementRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.CommissionRate = string.IsNullOrEmpty(S[3]) ? 0 : decimal.Parse(S[3]);
                this.PlaceBatchID = string.IsNullOrEmpty(S[4]) ? 0 : int.Parse(S[4]);
                this.TrustAccountID = S[5];
                this.PutbackDeadline = string.IsNullOrEmpty(S[6]) ? null : (DateTime?)DateTime.ParseExact(S[6], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.SIFRate = string.IsNullOrEmpty(S[7]) ? 0 : decimal.Parse(S[7]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                    base.ToString(),
                    this.CommissionRate,
                    this.PlaceBatchID,
                    this.TrustAccountID,
                    this.PutbackDeadline.HasValue ? this.PutbackDeadline.Value.ToString("MM/dd/yyyy") : "",
                    this.SIFRate);
            }
        }

        public class PlaceBatchSummary : RecordTypeBase
        {
            public int PlaceBatchID { get; private set; }
            public int Accounts { get; private set; }
            public decimal PrincipalBalance { get; private set; }
            public decimal InterestBalance { get; private set; }
            public decimal OtherBalance { get; private set; }
            public decimal AttyBalance { get; private set; }
            public decimal TotalBalance { get; private set; }
            public string PlaceType { get; private set; }
            public string SpecServType { get; private set; }

            public PlaceBatchSummary(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.PlaceBatchID = string.IsNullOrEmpty(S[1]) ? 0 : int.Parse(S[1]);
                this.Accounts = string.IsNullOrEmpty(S[2]) ? 0 : int.Parse(S[2]);
                this.PrincipalBalance = string.IsNullOrEmpty(S[3]) ? 0 : decimal.Parse(S[3]);
                this.InterestBalance = string.IsNullOrEmpty(S[4]) ? 0 : decimal.Parse(S[4]);
                this.OtherBalance = string.IsNullOrEmpty(S[5]) ? 0 : decimal.Parse(S[5]);
                this.AttyBalance = string.IsNullOrEmpty(S[6]) ? 0 : decimal.Parse(S[6]);
                this.TotalBalance = string.IsNullOrEmpty(S[7]) ? 0 : decimal.Parse(S[7]);
                this.PlaceType = S[8];
                this.SpecServType = S[9];
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                    base.ToString(),
                    this.PlaceBatchID,
                    this.Accounts,
                    this.PrincipalBalance,
                    this.InterestBalance,
                    this.OtherBalance,
                    this.AttyBalance,
                    this.TotalBalance,
                    this.PlaceType,
                    this.SpecServType);
            }
        }

        public class AdditionalInfoRecord : RecordBase
        {
            public string Merchant { get; private set; }
            public bool CBReportable { get; private set; }
            public decimal ChgOffBalance { get; private set; }
            public DateTime? ChgOffDate { get; private set; }
            public string OwnerName { get; private set; }
            public decimal LastPurchaseAmount { get; private set; }
            public string Misc1 { get; private set; }
            public string Misc2 { get; private set; }
            public string Misc3 { get; private set; }
            public DateTime? OriginationDate { get; private set; }
            public string PortfolioID { get; private set; }
            public string SellerName { get; private set; }
            public string TelecommPhone { get; private set; }
            public DateTime? PurchaseDate { get; private set; }

            public string OriginalCreditor { get; private set; }
            public DateTime? OutOfStatuteDate { get; private set; }
            /// <summary>
            /// Status if a CBR Negative Notice is required
            /// </summary>
            public bool SendCBRNegNotice { get; private set; }
            /// <summary>
            /// Status if a OOS Notice is required
            /// </summary>
            public bool SendOOSNotice { get; private set; }
            /////////////////////////////
            // Version 4 - Starts Here //
            /////////////////////////////
            /// <summary>
            /// Paper Type of Account (Bankcard, Retail, Installment Loan, etc.)
            /// </summary>
            public string AcctType { get; private set; }
            /// <summary>
            /// Information concerning Original Creditor, Merchant, or Prior Owners
            /// </summary>
            public string LegacyProductInfo { get; private set; }
            /// <summary>
            /// Last Pmt Date from Seller prior to Resurgent's Purchase
            /// </summary>
            public DateTime? SellerLastPmtDate { get; private set; }
            /// <summary>
            /// Last Pmt Amount from Seller prior to Resurgent's Purchase
            /// </summary>
            public decimal? SellerLastPmtAmt { get; private set; }

            public AdditionalInfoRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.Merchant = S[3];
                this.CBReportable = S[4].Trim() == "Y";
                this.ChgOffBalance = string.IsNullOrEmpty(S[5]) ? 0 : decimal.Parse(S[5]);
                this.ChgOffDate = string.IsNullOrEmpty(S[6]) ? null : (DateTime?)DateTime.ParseExact(S[6], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.OwnerName = S[7];
                this.LastPurchaseAmount = string.IsNullOrEmpty(S[8]) ? 0 : decimal.Parse(S[8]);
                this.Misc1 = S[9];
                this.Misc2 = S[10];
                this.Misc3 = S[11];
                this.OriginationDate = string.IsNullOrEmpty(S[12]) ? null : (DateTime?)DateTime.ParseExact(S[12], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.PortfolioID = S[13];
                this.SellerName = S[14];
                this.TelecommPhone = S[15];
                this.PurchaseDate = string.IsNullOrEmpty(S[16]) ? null : (DateTime?)DateTime.ParseExact(S[16], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                if (S.GetUpperBound(0) > 16)
                    this.OriginalCreditor = S[17];
                if (S.GetUpperBound(0) > 17)
                    this.OutOfStatuteDate = string.IsNullOrEmpty(S[18]) ? null : (DateTime?)DateTime.ParseExact(S[18], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                if (S.GetUpperBound(0) > 18)
                    this.SendCBRNegNotice = string.IsNullOrEmpty(S[19]) ? false : S[19] == "Y";
                if (S.GetUpperBound(0) > 19)
                    this.SendOOSNotice = string.IsNullOrEmpty(S[20]) ? false : S[20] == "Y";
                if (S.GetUpperBound(0) > 20)
                    this.AcctType = S[21];
                if (S.GetUpperBound(0) > 21)
                    this.LegacyProductInfo = S[22];
                if (S.GetUpperBound(0) > 22)
                    this.SellerLastPmtDate = string.IsNullOrEmpty(S[23]) ? null : (DateTime?)DateTime.ParseExact(S[23], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                if (S.GetUpperBound(0) > 23)
                    this.SellerLastPmtAmt = string.IsNullOrEmpty(S[24]) ? null : (decimal?)decimal.Parse(S[24]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}",
                    base.ToString(),
                    this.Merchant,
                    this.CBReportable,
                    this.ChgOffBalance,
                    this.ChgOffDate.HasValue ? this.ChgOffDate.Value.ToString("MM/dd/yyyy") : "",
                    this.OwnerName,
                    this.LastPurchaseAmount,
                    this.Misc1,
                    this.Misc2,
                    this.Misc3,
                    this.OriginationDate.HasValue ? this.OriginationDate.Value.ToString("MM/dd/yyyy") : "",
                    this.PortfolioID,
                    this.SellerName,
                    this.TelecommPhone,
                    this.PurchaseDate.HasValue ? this.PurchaseDate.Value.ToString("MM/dd/yyyy") : "",
                    this.OriginalCreditor,
                    this.OutOfStatuteDate.HasValue ? this.OutOfStatuteDate.Value.ToString("MM/dd/yyyy") : "",
                    this.SendCBRNegNotice ? "Y" : "N",
                    this.SendOOSNotice ? "Y" : "N",
                    this.AcctType,
                    this.LegacyProductInfo,
                    this.SellerLastPmtDate.HasValue ? this.SellerLastPmtDate.Value.ToString("MM/dd/yyyy") : "",
                    this.SellerLastPmtAmt);
            }
        }

        public class AlternateContactInfo : RecordBase
        {
            public AlternateContactType AltContactType { get; private set; }
            public string Name { get; private set; }
            public string Address { get; private set; }
            public string Address2 { get; private set; }
            public string City { get; private set; }
            public string State { get; private set; }
            public string Zip { get; private set; }
            public string Phone { get; private set; }
            public DateTime? NotificationDate { get; private set; }

            public AlternateContactInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.AltContactType = string.IsNullOrEmpty(S[3]) ? AlternateContactType.Unknown : (AlternateContactType)int.Parse(S[3]);
                this.Name = S[4];
                this.Address = S[5];
                this.Address2 = S[6];
                this.City = S[7];
                this.State = S[8];
                this.Zip = S[9];
                this.Phone = S[11];
                this.NotificationDate = string.IsNullOrEmpty(S[13]) ? null : (DateTime?)DateTime.ParseExact(S[13], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                    base.ToString(),
                    (int)this.AltContactType,
                    this.Name,
                    this.Address,
                    this.Address2,
                    this.City,
                    this.State,
                    this.Zip,
                    this.Phone,
                    this.NotificationDate.HasValue ? this.NotificationDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class DeceasedRecord : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public DateTime? DateOfDeath { get; private set; }

            public DeceasedRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (BorrowerType)int.Parse(S[3]);
                this.DateOfDeath = string.IsNullOrEmpty(S[4]) ? null : (DateTime?)DateTime.ParseExact(S[4], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.DateOfDeath.HasValue ? this.DateOfDeath.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class LegalInfo : RecordBase
        {
            public DateTime? PendingCourtDate { get; private set; }
            public DateTime? LawsuitFileDate { get; private set; }
            public string DocketNumber { get; private set; }
            public DateTime? JudgmentDate { get; private set; }
            public decimal JudgmentAmount { get; private set; }
            public string JudgmentState { get; private set; }
            public decimal PreJudgmentInterestRate { get; private set; }
            public decimal PostJudgmentInterestRate { get; private set; }
            public string JudgmentCourt { get; private set; }
            public string JudgmentCaseNumber { get; private set; }

            public LegalInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.PendingCourtDate = string.IsNullOrEmpty(S[3]) ? null : (DateTime?)DateTime.ParseExact(S[3], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.LawsuitFileDate = string.IsNullOrEmpty(S[4]) ? null : (DateTime?)DateTime.ParseExact(S[4], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.DocketNumber = S[5];
                this.JudgmentDate = string.IsNullOrEmpty(S[6]) ? null : (DateTime?)DateTime.ParseExact(S[6], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.JudgmentAmount = string.IsNullOrEmpty(S[7]) ? 0 : decimal.Parse(S[7]);
                this.JudgmentState = S[8];
                this.PreJudgmentInterestRate = string.IsNullOrEmpty(S[9]) ? 0 : decimal.Parse(S[9]);
                this.PostJudgmentInterestRate = string.IsNullOrEmpty(S[10]) ? 0 : decimal.Parse(S[10]);
                this.JudgmentCourt = S[11];
                this.JudgmentCaseNumber = S[12];
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}",
                    base.ToString(),
                    this.PendingCourtDate.HasValue ? this.PendingCourtDate.Value.ToString("MM/dd/yyyy") : "",
                    this.LawsuitFileDate.HasValue ? this.LawsuitFileDate.Value.ToString("MM/dd/yyyy") : "",
                    this.DocketNumber,
                    this.JudgmentDate.HasValue ? this.JudgmentDate.Value.ToString("MM/dd/yyyy") : "",
                    this.JudgmentAmount,
                    this.JudgmentState,
                    this.PreJudgmentInterestRate,
                    this.PostJudgmentInterestRate,
                    this.JudgmentCourt,
                    this.JudgmentCaseNumber);
            }
        }

        public class RelatedAsset_Bank : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string BankName { get; private set; }
            public string BankABANumber { get; private set; }
            public string Note { get; private set; }
            public DateTime? GarnishmentDate { get; private set; }

            public RelatedAsset_Bank(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (Enums.BorrowerType)int.Parse(S[3]);
                this.BankName = S[4];
                this.BankABANumber = S[5];
                this.Note = S[6];
                this.GarnishmentDate = string.IsNullOrEmpty(S[7]) ? null : (DateTime?)DateTime.ParseExact(S[7], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.BankName,
                    this.BankABANumber,
                    this.Note,
                    this.GarnishmentDate.HasValue ? this.GarnishmentDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class RelatedAsset_Employment : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string Employer { get; private set; }
            public string Note { get; private set; }
            public DateTime? GarnishmentDate { get; private set; }

            public RelatedAsset_Employment(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (Enums.BorrowerType)int.Parse(S[3]);
                this.Employer = S[4];
                this.Note = S[5];
                this.GarnishmentDate = string.IsNullOrEmpty(S[6]) ? null : (DateTime?)DateTime.ParseExact(S[6], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.Employer,
                    this.Note,
                    this.GarnishmentDate.HasValue ? this.GarnishmentDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class RelatedAsset_Other : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string AssetType { get; private set; }
            public string Note { get; private set; }
            public DateTime? AttachmentDate { get; private set; }

            public RelatedAsset_Other(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (Enums.BorrowerType)int.Parse(S[3]);
                this.AssetType = S[4];
                this.Note = S[5];
                this.AttachmentDate = string.IsNullOrEmpty(S[6]) ? null : (DateTime?)DateTime.ParseExact(S[6], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.AssetType,
                    this.Note,
                    this.AttachmentDate.HasValue ? this.AttachmentDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class RelatedAsset_RealEstate : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string Address { get; private set; }
            public string City { get; private set; }
            public string State { get; private set; }
            public string Zip { get; private set; }
            public string County { get; private set; }
            public string Note { get; private set; }
            public DateTime? LienDate { get; private set; }

            public RelatedAsset_RealEstate(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (Enums.BorrowerType)int.Parse(S[3]);
                this.Address = S[4];
                this.City = S[5];
                this.State = S[6];
                this.Zip = S[7];
                this.County = S[8];
                this.Note = S[9];
                this.LienDate = string.IsNullOrEmpty(S[10]) ? null : (DateTime?)DateTime.ParseExact(S[10], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.Address,
                    this.City,
                    this.State,
                    this.Zip,
                    this.County,
                    this.Note,
                    this.LienDate.HasValue ? this.LienDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class RelatedAsset_Vehicle : RecordBase
        {
            public BorrowerType BorrowerType { get; private set; }
            public string Year { get; private set; }
            public string Make { get; private set; }
            public string Model { get; private set; }
            public string VIN { get; private set; }
            public string Note { get; private set; }
            public DateTime? AttachmentDate { get; private set; }

            public RelatedAsset_Vehicle(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.BorrowerType = string.IsNullOrEmpty(S[3]) ? BorrowerType.Borrower : (Enums.BorrowerType)int.Parse(S[3]);
                this.Year = S[4];
                this.Make = S[5];
                this.Model = S[6];
                this.VIN = S[7];
                this.Note = S[8];
                this.AttachmentDate = string.IsNullOrEmpty(S[9]) ? null : (DateTime?)DateTime.ParseExact(S[9], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                    base.ToString(),
                    (int)this.BorrowerType,
                    this.Year,
                    this.Make,
                    this.Model,
                    this.VIN,
                    this.Note,
                    this.AttachmentDate.HasValue ? this.AttachmentDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class AttorneyBondingInfo : RecordBase
        {
            public string AttorneyBondingFirm { get; private set; }

            public AttorneyBondingInfo(string Record) : base(Record)
            {
                this.AttorneyBondingFirm = Record.Split('\t')[3];
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}",
                    base.ToString(),
                    this.AttorneyBondingFirm);
            }
        }

        public class JudgmentInfo : RecordBase
        {
            public DateTime? JudgmentDate { get; private set; }
            public string JudgmentNumber { get; private set; }
            public JudgmentType JudgmentType { get; private set; }
            public decimal JudgmentAmountAwarded { get; private set; }
            public decimal JudgmentPrincipalAwarded { get; private set; }
            public decimal JudgmentInterestAwarded { get; private set; }
            public decimal JudgmentCostAwarded { get; private set; }
            public decimal JudgmentFeeAwarded { get; private set; }
            public decimal PostJudgmentInterestRate { get; private set; }

            public JudgmentInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.JudgmentDate = string.IsNullOrEmpty(S[3]) ? null : (DateTime?)DateTime.ParseExact(S[3], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.JudgmentNumber = S[4];
                this.JudgmentType = string.IsNullOrEmpty(S[5]) ? JudgmentType.Unknown : (Enums.JudgmentType)Enum.Parse(typeof(Enums.JudgmentTypeValues), S[5]);
                this.JudgmentAmountAwarded = string.IsNullOrEmpty(S[6]) ? 0 : decimal.Parse(S[6]);
                this.JudgmentPrincipalAwarded = string.IsNullOrEmpty(S[7]) ? 0 : decimal.Parse(S[7]);
                this.JudgmentInterestAwarded = string.IsNullOrEmpty(S[8]) ? 0 : decimal.Parse(S[8]);
                this.JudgmentCostAwarded = string.IsNullOrEmpty(S[9]) ? 0 : decimal.Parse(S[9]);
                this.JudgmentFeeAwarded = string.IsNullOrEmpty(S[10]) ? 0 : decimal.Parse(S[10]);
                this.PostJudgmentInterestRate = string.IsNullOrEmpty(S[11]) ? 0 : decimal.Parse(S[11]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                    base.ToString(),
                    this.JudgmentDate.HasValue ? this.JudgmentDate.Value.ToString("MM/dd/yyyy") : "",
                    this.JudgmentNumber,
                    (JudgmentTypeValues)this.JudgmentType,
                    this.JudgmentAmountAwarded,
                    this.JudgmentPrincipalAwarded,
                    this.JudgmentInterestAwarded,
                    this.JudgmentCostAwarded,
                    this.JudgmentFeeAwarded,
                    this.PostJudgmentInterestRate);
            }
        }

        public class LawsuitInfo : RecordBase
        {
            public DateTime? StipulationDate { get; private set; }
            public string CourtState { get; private set; }
            public string CourtCounty { get; private set; }
            public string Court { get; private set; }
            public DateTime? SuitFileDate { get; private set; }
            public string CaseNumber { get; private set; }
            public string DocketNumber { get; private set; }
            public DateTime? LegalServiceDate { get; private set; }
            public ServiceType ModeOfService { get; private set; }
            public string LawFirmOfRecord { get; private set; }
            public decimal SuitAmount { get; private set; }
            public decimal SuitPrincipalAmount { get; private set; }
            public decimal SuitInterestAmount { get; private set; }
            public decimal SuitCostAmount { get; private set; }
            public decimal SuitFeeAmount { get; private set; }
            public decimal SuitInterestRate { get; private set; }
            public SuitOutcome SuitOutcome { get; private set; }
            public SuitDismissalReason SuitDismissalReason { get; private set; }

            public LawsuitInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.StipulationDate = string.IsNullOrEmpty(S[3]) ? null : (DateTime?)DateTime.ParseExact(S[3], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.CourtState = S[4];
                this.CourtCounty = S[5];
                this.Court = S[6];
                this.SuitFileDate = string.IsNullOrEmpty(S[7]) ? null : (DateTime?)DateTime.ParseExact(S[7], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.CaseNumber = S[8];
                this.DocketNumber = S[9];
                this.LegalServiceDate = string.IsNullOrEmpty(S[10]) ? null : (DateTime?)DateTime.ParseExact(S[10], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.ModeOfService = string.IsNullOrEmpty(S[11]) ? ServiceType.Unknown : (ServiceType)Enum.Parse(typeof(ServiceTypeValues), S[11]);
                this.LawFirmOfRecord = S[12];
                this.SuitAmount = string.IsNullOrEmpty(S[13]) ? 0 : decimal.Parse(S[13]);
                this.SuitPrincipalAmount = string.IsNullOrEmpty(S[14]) ? 0 : decimal.Parse(S[14]);
                this.SuitInterestAmount = string.IsNullOrEmpty(S[15]) ? 0 : decimal.Parse(S[15]);
                this.SuitCostAmount = string.IsNullOrEmpty(S[16]) ? 0 : decimal.Parse(S[16]);
                this.SuitFeeAmount = string.IsNullOrEmpty(S[17]) ? 0 : decimal.Parse(S[17]);
                this.SuitInterestRate = string.IsNullOrEmpty(S[18]) ? 0 : decimal.Parse(S[18]);
                this.SuitOutcome = string.IsNullOrEmpty(S[19]) ? SuitOutcome.Unknown : (Enums.SuitOutcome)Enum.Parse(typeof(SuitOutcomeValues), S[19]);
                this.SuitDismissalReason = string.IsNullOrEmpty(S[20]) ? SuitDismissalReason.Unknown : (Enums.SuitDismissalReason)Enum.Parse(typeof(SuitDismissalReasonValues), S[20]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}",
                    base.ToString(),
                    this.StipulationDate.HasValue ? this.StipulationDate.Value.ToString("MM/dd/yyyy") : "",
                    this.CourtState,
                    this.CourtCounty,
                    this.Court,
                    this.SuitFileDate.HasValue ? this.SuitFileDate.Value.ToString("MM/dd/yyyy") : "",
                    this.CaseNumber,
                    this.DocketNumber,
                    this.LegalServiceDate.HasValue ? this.LegalServiceDate.Value.ToString("MM/dd/yyyy") : "",
                    (ServiceTypeValues)this.ModeOfService,
                    this.LawFirmOfRecord,
                    this.SuitAmount,
                    this.SuitPrincipalAmount,
                    this.SuitInterestAmount,
                    this.SuitCostAmount,
                    this.SuitFeeAmount,
                    this.SuitInterestRate,
                    (SuitOutcomeValues)this.SuitOutcome,
                    (SuitDismissalReasonValues)this.SuitDismissalReason);
            }
        }

        public class InsufficientFundsInfo : RecordBase
        {
            public decimal PreviousSIFRate { get; private set; }
            public string PreviousServicer { get; private set; }

            public InsufficientFundsInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.PreviousSIFRate = string.IsNullOrEmpty(S[3]) ? 0 : decimal.Parse(S[3]);
                this.PreviousServicer = S[4];
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}",
                    base.ToString(),
                    this.PreviousSIFRate,
                    this.PreviousServicer);
            }
        }

        public class StatusCodeRecordInfo : RecordBase
        {
            public string StatusCode { get; private set; }

            public StatusCodeRecordInfo(string Record) : base(Record)
            {
                this.StatusCode = Record.Split('\t')[3];
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}",
                    base.ToString(),
                    this.StatusCode);
            }
        }

        public class DormantJudgmentInfo : RecordBase
        {
            public string AttorneyOfRecord { get; private set; }
            public decimal SumPostJmtPayments { get; private set; }

            public DormantJudgmentInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.AttorneyOfRecord = S[3];
                this.SumPostJmtPayments = string.IsNullOrEmpty(S[4]) ? 0 : decimal.Parse(S[4]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}",
                    base.ToString(),
                    this.AttorneyOfRecord,
                    this.SumPostJmtPayments);
            }
        }

        public class AccountScoreInfo : RecordBase
        {
            public string ScoreType { get; private set; }
            public string Score { get; private set; }
            public DateTime? ScoreDate { get; private set; }

            public AccountScoreInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.ScoreType = S[3];
                this.Score = S[4];
                this.ScoreDate = string.IsNullOrEmpty(S[5]) ? null : (DateTime?)DateTime.ParseExact(S[5], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}",
                    base.ToString(),
                    this.ScoreType,
                    this.Score,
                    this.ScoreDate.HasValue ? this.ScoreDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class BankruptcyInfo : RecordBase
        {
            public string Chapter { get; private set; }
            public string CaseNumber { get; private set; }
            public DateTime? FileDate { get; private set; }
            public BankruptcyStatus BankruptcyStatus { get; private set; }
            public DateTime? BankruptcyStatusDate { get; private set; }

            public BankruptcyInfo(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.Chapter = S[3];
                this.CaseNumber = S[4];
                this.FileDate = string.IsNullOrEmpty(S[5]) ? null : (DateTime?)DateTime.ParseExact(S[5], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                this.BankruptcyStatus = string.IsNullOrEmpty(S[6]) ? BankruptcyStatus.Unknown : (Enums.BankruptcyStatus)int.Parse(S[6]);
                this.BankruptcyStatusDate = string.IsNullOrEmpty(S[7]) ? null : (DateTime?)DateTime.ParseExact(S[7], "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                    base.ToString(),
                    this.Chapter,
                    this.CaseNumber,
                    this.FileDate.HasValue ? this.FileDate.Value.ToString("MM/dd/yyyy") : "",
                    (int)this.BankruptcyStatus,
                    this.BankruptcyStatusDate.HasValue ? this.BankruptcyStatusDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }

        public class BalanceItemizationRecord : RecordBase
        {
            public int PlaceBatchID { get; private set; }
            public ItemizationType ItemizationType { get; private set; }
            public string ItemizationCreditorName { get; private set; }
            public DateTime? ItemizationDate { get; private set; }
            public decimal ItemizationBalance { get; private set; }
            public decimal ItemizationPmtsCredits { get; private set; }
            public decimal ItemizationInterest { get; private set; }
            public decimal ItemizationFees { get; private set; }
            public decimal ItemizationCurrBalance { get; private set; }
            public decimal Interest { get; private set; }
            public decimal Cost { get; private set; }
            public decimal Fees { get; private set; }
            public decimal Payments { get; private set; }
            public decimal Credits { get; private set; }
            public decimal AdjustmentDecreaseBal { get; private set; }
            public decimal AdjustmentIncreaseBal { get; private set; }
            public DateTime? ItemizationLastUpdateDate { get; private set; }


            public BalanceItemizationRecord(string Record) : base(Record)
            {
                string[] S = Record.Split('\t');
                this.PlaceBatchID = Convert.ToInt32(S[3]);
                this.ItemizationType = Dictionaries.ItemizationTypeDictionary.ContainsKey(S[4]) ? Dictionaries.ItemizationTypeDictionary[S[4]] : ItemizationType.Unknown;
                this.ItemizationCreditorName = S[5];
                this.ItemizationDate = Convert.ToDateTime(S[6]);
                this.ItemizationBalance = Convert.ToDecimal(S[7]);
                this.ItemizationPmtsCredits = Convert.ToDecimal(S[8]);
                this.ItemizationInterest = Convert.ToDecimal(S[9]);
                this.ItemizationFees = Convert.ToDecimal(S[10]);
                this.ItemizationCurrBalance = Convert.ToDecimal(S[11]);
                this.Interest = Convert.ToDecimal(S[12]);
                this.Cost = Convert.ToDecimal(S[13]);
                this.Fees = Convert.ToDecimal(S[14]);
                this.Payments = Convert.ToDecimal(S[15]);
                this.Credits = Convert.ToDecimal(S[16]);
                this.AdjustmentDecreaseBal = Convert.ToDecimal(S[17]);
                this.AdjustmentIncreaseBal = Convert.ToDecimal(S[18]);
                this.ItemizationLastUpdateDate = Convert.ToDateTime(S[19]);
            }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}",
                    base.ToString(),
                    this.PlaceBatchID,
                    Dictionaries.ItemizationTypeDictionary.FirstOrDefault(d => d.Value == this.ItemizationType).Key,
                    this.ItemizationCreditorName,
                    this.ItemizationDate.HasValue ? this.ItemizationDate.Value.ToString("MM/dd/yyyy") : "",
                    this.ItemizationBalance,
                    this.ItemizationPmtsCredits,
                    this.ItemizationInterest,
                    this.ItemizationFees,
                    this.ItemizationCurrBalance,
                    this.Interest,
                    this.Cost,
                    this.Fees,
                    this.Payments,
                    this.Credits,
                    this.AdjustmentDecreaseBal,
                    this.AdjustmentIncreaseBal,
                    this.ItemizationLastUpdateDate.HasValue ? this.ItemizationLastUpdateDate.Value.ToString("MM/dd/yyyy") : "");
            }
        }
    }
}
