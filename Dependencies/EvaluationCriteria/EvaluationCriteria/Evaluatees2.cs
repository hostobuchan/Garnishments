using EvaluationCriteria.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria
{
    public class Evaluatees2
    {
        private List<Evaluatee2> _EvalList = new List<Evaluatee2>();

        public Evaluatees2(DataTable MASTER, DataTable Diary)
        {
            foreach (DataRow dr in MASTER.Rows)
            {
                if (Diary.Rows.Count > 0 && Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").Length > 0)
                    _EvalList.Add(new Evaluatee2(dr, Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable()));
                else
                    _EvalList.Add(new Evaluatee2(dr, new DataTable()));
            }
        }
        public Evaluatees2(SqlDataReader Reader, DataTable Diary)
        {
            DataTable dt = new DataTable();
            foreach (DataRow d in Reader.GetSchemaTable().Rows)
            {
                dt.Columns.Add(new DataColumn(d["ColumnName"].ToString(), (Type)d["DataType"]));
            }
            DataRow dr = dt.NewRow();
            while (Reader.Read())
            {
                for (int e = 0; e < Reader.FieldCount; e++)
                {
                    dr[e] = Reader[e];
                }
                if (Diary.Select("FILENO='" + Reader["FILENO"].ToString() + "'").Length > 0)
                    _EvalList.Add(new Evaluatee2(dr, Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable()));
                else
                    _EvalList.Add(new Evaluatee2(dr, new DataTable()));
            }
        }
        public Evaluatees2(SqlDataReader Reader, DataTable Diary, DataTable CostCodes)
        {
            DataTable dt = new DataTable();
            foreach (DataRow d in Reader.GetSchemaTable().Rows)
            {
                dt.Columns.Add(new DataColumn(d["ColumnName"].ToString(), (Type)d["DataType"]));
            }
            DataRow dr = dt.NewRow();
            while (Reader.Read())
            {
                for (int e = 0; e < Reader.FieldCount; e++)
                {
                    dr[e] = Reader[e];
                }

                DataTable diaries = Diary.Select("FILENO='" + Reader["FILENO"].ToString() + "'").Length > 0 ? Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable() : new DataTable();
                DataTable costs = CostCodes.Select("FILENO='" + Reader["FILENO"].ToString() + "'").Length > 0 ? CostCodes.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable() : new DataTable();
                _EvalList.Add(new Evaluatee2(dr, diaries, costs));
            }
        }
        public Evaluatees2(IDataReader Reader)
        {
            DataTable dt = new DataTable();
            foreach (DataRow d in Reader.GetSchemaTable().Rows)
            {
                dt.Columns.Add(new DataColumn(d["ColumnName"].ToString(), (Type)d["DataType"]));
            }
            DataRow dr = dt.NewRow();
            while (Reader.Read())
            {
                for (int e = 0; e < Reader.FieldCount; e++)
                {
                    dr[e] = Reader[e];
                }
                _EvalList.Add(new Evaluatee2(dr, new DataTable()));
            }
        }

        public List<Evaluatee2> EvaluateeList { get { return _EvalList; } set { _EvalList = value; } }
        public List<Evaluatee2> EvaluateeListUnique { get { return _EvalList.GroupBy(el => el.FileNo).Select(el => el.First()).ToList(); } }

        public class Evaluatee2
        {
            #region Properties
            public string FileNo { get; set; }
            public string Address { get; set; }
            public string Address2 { get; set; }
            public int AdversarialAttorney { get; private set; }
            public double? AmountPaid { get; set; }
            public DateTime? AnswerFiled { get; private set; }
            public double Balance { get; set; }
            public int BankNo { get; set; }
            public string BankruptcyChapter { get; set; }
            public DateTime? BankruptcyDate { get; set; }
            public string BankruptcyFileNo { get; set; }
            public int? C_Priority { get; private set; }
            public string City { get; set; }
            public string Classification { get; private set; }
            public string ClientType { get; private set; }
            public string CLSName { get; private set; }
            public int Collector { get; private set; }
            public double Cost { get; private set; }
            public List<DiaryCode> CostCodes { get; private set; }
            public string County { get; set; }
            public DateTime CourtDate { get; set; }
            public string CreditorName { get; set; }
            public DateTime? DateReceived { get; set; }
            public DateTime? DeathDate { get; set; }
            public int Debtor { get; set; }
            public List<DiaryCode> Diaries { get; private set; }
            public string DisplayName { get { return Name[0] + " " + Name[1] + " " + Name[2] + " (D" + Debtor.ToString() + ")"; } }
            public DateTime DispositionDate
            {
                get
                {
                    if (JudgmentDate != null)
                        return JudgmentDate.Value;
                    else
                    {
                        if (ServiceDate != null)
                            return ServiceDate.Value;
                        else
                        {
                            if (SuitDate != null)
                                return SuitDate.Value;
                            else
                            {
                                if (DateReceived != null)
                                    return DateReceived.Value;
                                else
                                    return DateTime.MinValue;
                            }
                        }
                    }
                }
            }
            public string Docket { get; set; }
            public bool? DocsNeeded { get; set; }
            public DateTime? DismissalDate { get; set; }
            public int EmployerNo { get; set; }
            public int Forwarder { get; private set; }
            public string ForwarderFileNo { get; private set; }
            public string ForwarderRefNo { get; private set; }
            public DateTime? GarnishmentDate { get; set; }
            public bool? HasPhone { get; set; }
            public bool? HomeOwner { get; set; }
            public double? InterestCollected { get; private set; }
            public double? JudgmentAmount { get; set; }
            public DateTime? JudgmentDate { get; set; }
            public DateTime? LastLetter { get; set; }
            public DateTime? LastPaymentDate { get; set; }
            public DateTime? LastSkip { get; set; }
            public MediaStatus MediaStatus { get; set; }
            public int MinSifRate { get { try { return SifRateFinder(this.MinSifRateString, this); } catch { return 100; } } }
            public string MinSifRateString { get; set; }
            public double? MinSuitBalance { get; set; }
            public string[] Name { get { return DebtorName(this.CLSName); } }
            public string NameFirst { get { return this.Name[0]; } }
            public string NameMiddle { get { return this.Name[1]; } }
            public string NameLast { get { return this.Name[2]; } }
            public string NameSuffix { get { return this.Name[3]; } }
            public bool? NonSuitWorthy { get; set; }
            public DateTime? OpenedDate { get; set; }
            public double? OriginalClaim { get; set; }
            public string OriginalCreditor { get; set; }
            public int ParaNumber { get; set; }
            public double PercentPaid { get; set; }
            public string Plaintiff { get; set; }
            public string Plaintiff2 { get; set; }
            public double? PostJudgmentInterestRate { get; set; }
            public double? PreJudgmentInterestRate { get; set; }
            public string PreviousLetter { get; set; }
            public int PreviousLetterType { get; set; }
            public DateTime? PreviousLetterDate { get; set; }
            public double? LastPaymentAmount { get; set; }
            public bool ReturnMail { get; set; }
            public int SalesNo { get; private set; }
            public DateTime? ServiceDate { get; set; }
            public int SheriffNo { get; set; }
            public int SifRate { get { try { return SifRateFinder(this.SifRateString, this); } catch { return 100; } } }
            public string SifRateString { get; set; }
            public string SSN { get; set; }
            public DateTime? Stat2Date { get; set; }
            public string State { get; set; }
            public int Status { get; set; }
            public DateTime StatusDate { get; set; }
            public DateTime? StatuteDate { get; set; }
            public DateTime? SuitDate { get; set; }
            public int? SuitScore { get; set; }
            public bool? SuitWorthy { get; set; }
            public DateTime? TriggerHit { get; set; }
            public int Venue { get; set; }
            public bool? VerifiedAddress { get; set; }
            public bool? VerifiedPhone { get; set; }
            public bool? VerifiedPOE { get; set; }
            public string Zip { get; set; }
            #endregion

            public Evaluatee2(DataRow FileData, DataTable Diaries, DataTable CostCodes) : this(FileData, Diaries)
            {
                foreach (DataRow dr in CostCodes.Rows)
                {
                    this.CostCodes.Add(new DiaryCode(dr));
                }
            }
            public Evaluatee2(DataRow FileData, DataTable Diaries)
            {
                this.CostCodes = new List<DiaryCode>();
                this.Diaries = new List<DiaryCode>();
                this.FileNo = GetDataFromTable(FileData, "FILENO").ToString();
                this.Address = GetDataFromTable(FileData, "STREET") == null ? "" : GetDataFromTable(FileData, "STREET").ToString();
                this.Address2 = GetDataFromTable(FileData, "STREET2") == null ? "" : GetDataFromTable(FileData, "STREET2").ToString();
                try { this.AdversarialAttorney = Convert.ToInt32(GetDataFromTable(FileData, "ADVA")); } catch { this.AdversarialAttorney = 0; }
                try { this.AmountPaid = Convert.ToDouble(GetDataFromTable(FileData, "AMT_PAID")); } catch { this.AmountPaid = null; }
                try { this.AnswerFiled = Convert.ToDateTime(GetDataFromTable(FileData, "AF_DATE")); } catch { this.AnswerFiled = null; }
                try { this.Balance = Convert.ToDouble(GetDataFromTable(FileData, "BALANCE")); } catch { this.Balance = 0; }
                try { this.BankNo = Convert.ToInt32(GetDataFromTable(FileData, "BANK_NO")); } catch { this.BankNo = 0; }
                this.BankruptcyChapter = GetDataFromTable(FileData, "BKCY_CHAPTER") == null ? "" : GetDataFromTable(FileData, "BKCY_CHAPTER").ToString();
                try { this.BankruptcyDate = Convert.ToDateTime(GetDataFromTable(FileData, "BKCY_FILED_DATE")); } catch { this.BankruptcyDate = null; }
                this.BankruptcyFileNo = GetDataFromTable(FileData, "BKCY_FILENO") == null ? "" : GetDataFromTable(FileData, "BKCY_FILENO").ToString();
                try { this.C_Priority = Convert.ToInt32(GetDataFromTable(FileData, "C_PRIORITY")); } catch { this.C_Priority = null; }
                this.City = GetDataFromTable(FileData, "CITY") == null ? "" : GetDataFromTable(FileData, "CITY").ToString();
                this.Classification = GetDataFromTable(FileData, "CLASSIFICATION") == null ? "" : GetDataFromTable(FileData, "CLASSIFICATION").ToString();
                this.ClientType = GetDataFromTable(FileData, "CL_TYPE") == null ? "" : GetDataFromTable(FileData, "CL_TYPE").ToString();
                this.CLSName = GetDataFromTable(FileData, "NAME") == null ? "" : GetDataFromTable(FileData, "NAME").ToString();
                try { this.Collector = Convert.ToInt32(GetDataFromTable(FileData, "COLLECTOR")); } catch { this.Collector = 0; }
                try { this.Cost = Convert.ToDouble(GetDataFromTable(FileData, "COST_EXP")); } catch { this.Cost = 0; }
                this.County = GetDataFromTable(FileData, "COUNTY_NAME") == null ? "" : GetDataFromTable(FileData, "COUNTY_NAME").ToString();
                try { this.CourtDate = Convert.ToDateTime(GetDataFromTable(FileData, new[] { "COURT_DATE", "COURT_DATE2", "SUIT_DATE" })); } catch { this.CourtDate = new DateTime(2007, 1, 1); }
                this.CreditorName = GetDataFromTable(FileData, "CRED_NAME") == null ? "" : GetDataFromTable(FileData, "CRED_NAME").ToString();
                try { this.DateReceived = Convert.ToDateTime(GetDataFromTable(FileData, "DATE_RECD")); } catch { this.DateReceived = null; }
                try { this.DeathDate = Convert.ToDateTime(GetDataFromTable(FileData, "DEATH_DATE")); } catch { this.DeathDate = null; }
                try { this.Debtor = Convert.ToInt32(GetDataFromTable(FileData, "NUMBER")); } catch { this.Debtor = 1; }
                this.Docket = GetDataFromTable(FileData, "DOCKET_NO") == null ? "" : GetDataFromTable(FileData, "DOCKET_NO").ToString();
                try { this.DocsNeeded = Convert.ToBoolean(GetDataFromTable(FileData, "Docs Needed")); } catch { this.DocsNeeded = null; }
                try { this.DismissalDate = Convert.ToDateTime(GetDataFromTable(FileData, "DSMIS_DATE")); } catch { this.DismissalDate = null; }
                try { this.EmployerNo = Convert.ToInt32(GetDataFromTable(FileData, "EMP_NO")); } catch { this.EmployerNo = 0; }
                this.Forwarder = Convert.ToInt32(GetDataFromTable(FileData, "FORW_NO"));
                this.ForwarderFileNo = GetDataFromTable(FileData, "FORW_FILENO") == null ? "" : GetDataFromTable(FileData, "FORW_FILENO").ToString();
                this.ForwarderRefNo = GetDataFromTable(FileData, "FORW_REFNO") == null ? "" : GetDataFromTable(FileData, "FORW_REFNO").ToString();
                try { this.GarnishmentDate = Convert.ToDateTime(GetDataFromTable(FileData, "GARN_DATE")); } catch { this.GarnishmentDate = null; }
                try { this.HasPhone = Convert.ToBoolean(GetDataFromTable(FileData, "HAS_PHONE")); } catch { this.HasPhone = null; }
                try { this.HomeOwner = Convert.ToBoolean(GetDataFromTable(FileData, "HOME_OWNER")); } catch { this.HomeOwner = null; }
                try { this.InterestCollected = Convert.ToDouble(GetDataFromTable(FileData, "INT_COLL")); } catch { this.InterestCollected = null; }
                try { this.JudgmentAmount = Convert.ToDouble(GetDataFromTable(FileData, "JMT_AMT")); } catch { this.JudgmentAmount = null; }
                try { this.JudgmentDate = Convert.ToDateTime(GetDataFromTable(FileData, "JMT_DATE")); } catch { this.JudgmentDate = null; }
                try { this.LastLetter = Convert.ToDateTime(GetDataFromTable(FileData, "LASTLETTER")); } catch { this.LastLetter = null; }
                try { this.LastPaymentAmount = Convert.ToDouble(GetDataFromTable(FileData, "PMT_AMT")); } catch { this.LastPaymentAmount = null; }
                try { this.LastPaymentDate = Convert.ToDateTime(GetDataFromTable(FileData, "LPAYMNT_DATE")); } catch { this.LastPaymentDate = null; }
                try { this.LastSkip = Convert.ToDateTime(GetDataFromTable(FileData, "LASTSKIP")); } catch { this.LastSkip = null; }
                this.MinSifRateString = GetDataFromTable(FileData, "MINSIFRATE") == null ? "" : GetDataFromTable(FileData, "MINSIFRATE").ToString();
                try { this.MinSuitBalance = Convert.ToDouble(GetDataFromTable(FileData, "Minimum Suit Balance")); } catch { this.MinSuitBalance = null; }
                try { this.NonSuitWorthy = Convert.ToBoolean(GetDataFromTable(FileData, "Marked_172")); } catch { this.NonSuitWorthy = null; }
                try { this.OpenedDate = Convert.ToDateTime(GetDataFromTable(FileData, "OPENED_DATE")); } catch { this.OpenedDate = null; }
                try { this.OriginalClaim = Convert.ToDouble(GetDataFromTable(FileData, "ORIG_CLAIM")); } catch { this.OriginalClaim = null; }
                this.OriginalCreditor = GetDataFromTable(FileData, "ORIG_CRED") == null ? "" : GetDataFromTable(FileData, "ORIG_CRED").ToString();
                try { this.ParaNumber = Convert.ToInt32(GetDataFromTable(FileData, "PARA")); } catch { this.ParaNumber = 0; }
                try { this.PercentPaid = Convert.ToDouble(GetDataFromTable(FileData, "PERCENT_PAID")); } catch { this.PercentPaid = 0; }
                this.Plaintiff = GetDataFromTable(FileData, "PLAINTIFF_1") == null ? "" : GetDataFromTable(FileData, "PLAINTIFF_1").ToString();
                this.Plaintiff2 = GetDataFromTable(FileData, "PLAINTIFF_2") == null ? "" : GetDataFromTable(FileData, "PLAINTIFF_2").ToString();
                try { this.PostJudgmentInterestRate = Convert.ToDouble(GetDataFromTable(FileData, "POST_J_RATE")); } catch { this.PostJudgmentInterestRate = null; }
                try { this.PreJudgmentInterestRate = Convert.ToDouble(GetDataFromTable(FileData, "PRE_J_RATE")); } catch { this.PreJudgmentInterestRate = null; }
                this.PreviousLetter = GetDataFromTable(FileData, "PREV_LETTER") == null ? "" : GetDataFromTable(FileData, "PREV_LETTER").ToString();
                try { this.PreviousLetterType = Convert.ToInt32(GetDataFromTable(FileData, "PREV_LETTER_TYPE")); } catch { this.PreviousLetterType = 0; }
                try { this.PreviousLetterDate = Convert.ToDateTime(GetDataFromTable(FileData, "PREV_LETTER_DATE")); } catch { this.PreviousLetterDate = null; }
                try { this.ReturnMail = Convert.ToBoolean(GetDataFromTable(FileData, "RET_MAIL")); } catch { this.ReturnMail = false; }
                try { this.SalesNo = Convert.ToInt32(GetDataFromTable(FileData, "SALES_NO")); } catch { this.SalesNo = 0; }
                try { this.ServiceDate = Convert.ToDateTime(GetDataFromTable(FileData, "SERVICE_DATE")); } catch { this.ServiceDate = null; }
                try { this.SheriffNo = Convert.ToInt32(GetDataFromTable(FileData, "SHER_NO")); } catch { this.SheriffNo = 0; }
                this.SifRateString = GetDataFromTable(FileData, "SIFRATE") == null ? "" : GetDataFromTable(FileData, "SIFRATE").ToString();
                this.SSN = GetDataFromTable(FileData, "SSN") == null ? "" : GetDataFromTable(FileData, "SSN").ToString();
                try { this.Stat2Date = Convert.ToDateTime(GetDataFromTable(FileData, "STAT2_DATE")); } catch { this.Stat2Date = null; }
                this.State = GetDataFromTable(FileData, new[] { "ST", "STATE" }) == null ? "" : GetDataFromTable(FileData, new[] { "ST", "STATE" }).ToString();
                try { this.Status = Convert.ToInt32(GetDataFromTable(FileData, "STAT1_CODE")); } catch { this.Status = 0; }
                try { this.StatusDate = Convert.ToDateTime(GetDataFromTable(FileData, "STAT1_DATE")); } catch { this.StatusDate = DateTime.MinValue; }
                this.StatuteDate = null;
                try { this.SuitDate = Convert.ToDateTime(GetDataFromTable(FileData, "SUIT_DATE")); } catch { this.SuitDate = null; }
                try { this.SuitWorthy = Convert.ToBoolean(GetDataFromTable(FileData, "Marked_173")); } catch { this.SuitWorthy = null; }
                try { this.SuitScore = Convert.ToInt32(GetDataFromTable(FileData, "SUIT_SCORE")); } catch { this.SuitScore = null; }
                try { this.TriggerHit = Convert.ToDateTime(GetDataFromTable(FileData, "TRIGGER")); } catch { this.TriggerHit = null; }
                try { this.Venue = Convert.ToInt32(GetDataFromTable(FileData, "VENUE1_NO")); } catch { this.Venue = 0; }
                try { this.VerifiedAddress = Convert.ToBoolean(GetDataFromTable(FileData, "VERIFIED_ADDRESS")); } catch { this.VerifiedAddress = null; }
                try { this.VerifiedPhone = Convert.ToBoolean(GetDataFromTable(FileData, "VERIFIED_PHONE")); } catch { this.VerifiedPhone = null; }
                try { this.VerifiedPOE = Convert.ToBoolean(GetDataFromTable(FileData, "VERIFIED_POE")); } catch { this.VerifiedPOE = null; }
                this.Zip = GetDataFromTable(FileData, "ZIP") == null ? "" : Criteria.NumberCleanup(GetDataFromTable(FileData, "ZIP").ToString(), typeof(int));
                foreach (DataRow dr in Diaries.Rows)
                {
                    this.Diaries.Add(new DiaryCode(dr));
                }
                // Media Status
                this.MediaStatus = new MediaStatus()
                {
                    PaymentHistory = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "PAY_HISTORY_STATUS")),
                    ChainOfTitle = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "CHAIN_OF_TITLE_STATUS")),
                    TermsAndConds = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "TERMS_AND_CONDS_STATUS")),
                    ChargeOff = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "CHARGEOFF_STATUS")),
                    Statement = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "STATEMENT_STATUS")),
                    LoanDocs = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "LOAN_DOCS_STATUS")),
                    Application = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "APPLICATION_STATUS")),
                    BillOfSale = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "BILL_OF_SALE_STATUS")),
                    Other = MediaStatus.DecodeStatus(GetDataFromTable(FileData, "OTHER_STATUS"))
                };
            }

            object GetDataFromTable(DataRow FileData, string[] PossibleAttributes)
            {
                object Result = null;
                foreach (string s in PossibleAttributes)
                {
                    Result = GetDataFromTable(FileData, s);
                    if (Result != null) return Result;
                }
                return Result;
            }
            object GetDataFromTable(DataRow FileData, string Attribute)
            {
                if (FileData.Table.Columns.IndexOf(Attribute) > -1)
                {
                    return (object)FileData[Attribute];
                }
                else
                { return null; }
            }


        }

        public class MediaStatus
        {
            public Status PaymentHistory { get; set; }
            public Status ChainOfTitle { get; set; }
            public Status TermsAndConds { get; set; }
            public Status ChargeOff { get; set; }
            public Status Statement { get; set; }
            public Status LoanDocs { get; set; }
            public Status Application { get; set; }
            public Status BillOfSale { get; set; }
            public Status Other { get; set; }
            public Status AllMedia
            {
                get
                {
                    byte Composite = 0;
                    Composite |= StatusBit(this.PaymentHistory);
                    Composite |= StatusBit(this.ChainOfTitle);
                    Composite |= StatusBit(this.TermsAndConds);
                    Composite |= StatusBit(this.ChargeOff);
                    Composite |= StatusBit(this.Statement);
                    Composite |= StatusBit(this.LoanDocs);
                    Composite |= StatusBit(this.Application);
                    Composite |= StatusBit(this.BillOfSale);
                    Composite |= StatusBit(this.Other);
                    return BitStatus(Composite);
                }
            }

            public static Status DecodeStatus(object Status)
            {
                if (Status == null) return MediaStatus.Status.None;
                else
                {
                    switch (Status.ToString())
                    {
                        case "REQUESTED":
                            return MediaStatus.Status.Requested;
                        case "ORDERED":
                            return MediaStatus.Status.Ordered;
                        case "RECEIVED":
                            return MediaStatus.Status.Received;
                        case "NONE":
                        default:
                            return MediaStatus.Status.None;
                    }
                }
            }

            private static byte StatusBit(Status curStatus)
            {
                switch (curStatus)
                {
                    case Status.Received:
                        return 8;
                    case Status.Ordered:
                        return 4;
                    case Status.Requested:
                        return 2;
                    case Status.None:
                    default:
                        return 1;
                }
            }
            private static Status BitStatus(byte Composite)
            {
                if ((Composite & 2) == 2) return Status.Requested;
                if ((Composite & 4) == 4) return Status.Ordered;
                if ((Composite & 8) == 8) return Status.Received;
                return Status.None;
            }

            public enum Status : byte
            {
                NotUsed = 0,
                None = 1,
                Requested = 2,
                Ordered = 3,
                Received = 4
            }
        }
    }
}
