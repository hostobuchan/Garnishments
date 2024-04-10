using EvaluationCriteria.Attributes;
using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria.Accounts
{
    [DataContract(Name = "EvaluateeAccount", IsReference = true)]
    public class EvaluateeAccount<T> where T : EvaluateeDebtor
    {
        #region Properties
        [DataMember(Name = "Manager", Order = 0)]
        protected object _DataHandler { get; private set; }
        protected virtual EvaluateeDataHandler Manager { get { return this._DataHandler as EvaluateeDataHandler; } }
        [DataMember(Name = "FileNo", Order = 1)]
        public string FileNo { get; set; }
        [DataMember(Name = "Adva", Order = 2)]
        [PropertyAssociation("ADVA", typeof(COptParam), Description = "Adversarial Attorney")]
        public int AdversarialAttorney { get; private set; }
        [DataMember(Name = "Paid", Order = 3)]
        [PropertyAssociation("AMT_PAID", typeof(CBasicParam), Description = "Amount Paid")]
        public double? AmountPaid { get; set; }
        [DataMember(Name = "Balance", Order = 4)]
        [PropertyAssociation("BALANCE", typeof(CBasicParam), Description = "Balance")]
        public double Balance { get; set; }
        [DataMember(Name = "BankNo", Order = 5)]
        [PropertyAssociation("BANK_NO", typeof(CBasicParam), Description = "Bank No.")]
        public int BankNo { get; set; }
        [DataMember(Name = "C_Priority", Order = 6)]
        [PropertyAssociation("C_PRIORITY", typeof(COptParam), Description = "C_Priority")]
        public int? C_Priority { get; private set; }
        [DataMember(Name = "Classification", Order = 7)]
        [PropertyAssociation("CLASSIFICATION", typeof(CClassParam), Description = "Classification")]
        public string Classification { get; private set; }
        [DataMember(Name = "ClientType", Order = 8)]
        [PropertyAssociation("CL_Type", typeof(CStringParam), Description = "Client Type")]
        public string ClientType { get; private set; }

        public string CLSName { get { return this.DebtorToUse?.CLSName; } }
        [DataMember(Name = "Collector", Order = 9)]
        [PropertyAssociation("COLLECTOR", typeof(CBoolParam), Description = "Collector")]
        public int Collector { get; private set; }
        [DataMember(Name = "Cost", Order = 10)]
        [PropertyAssociation("COST", typeof(CBasicParam), Description = "Cost")]
        public double Cost { get; private set; }
        [DataMember(Name = "County", Order = 11)]
        [PropertyAssociation("COUNTY", typeof(CStringParam), Description = "County")]
        public string County { get; set; }
        [DataMember(Name = "CourtDate", Order = 12)]
        [PropertyAssociation("COURT_DATE", typeof(CDateParam), Description = "Court Date")]
        public DateTime? CourtDate { get; set; }
        [DataMember(Name = "CreditorName", Order = 13)]
        [PropertyAssociation("CRED_NAME", typeof(CStringParam), Description = "Creditor Name")]
        public string CreditorName { get; set; }
        [DataMember(Name = "DateReceived", Order = 14)]
        [PropertyAssociation("DATE_RECD", typeof(CDateParam), Description = "Received Date")]
        public DateTime? DateReceived { get; set; }
        [DataMember(Name = "Diaries", Order = 15)]
        [PropertyAssociation("DIARY", typeof(CBoolParam), Description = "Diary Requirement")]
        public List<DiaryCode> Diaries { get; private set; }

        public string DisplayName { get { return this.DebtorToUse?.DisplayName + " (D" + Debtor.ToString() + ")"; } }
        [PropertyAssociation("DISPOSITION", typeof(CEnumParam<Enums.Disposition>), Description = "Disposition")]
        public Enums.Disposition Disposition
        {
            get
            {
                return this.JudgmentDate.HasValue ? Enums.Disposition.Judgment : this.SuitDate.HasValue || this.ServiceDate.HasValue ? Enums.Disposition.PostSuit : Enums.Disposition.PreSuit;
            }
        }
        [PropertyAssociation("DISPOSITION_DATE", typeof(CDateParam), Description = "Disposition Date")]
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
        [DataMember(Name = "Docket", Order = 16)]
        [PropertyAssociation("DOCKET", typeof(CSimpleParam), Description = "Docket Info")]
        public string Docket { get; set; }

        public bool? DocsNeeded { get; set; }
        [DataMember(Name = "EmployerNo", Order = 17)]
        [PropertyAssociation("EMP_NO", typeof(CBasicParam), Description = "Employer No.")]
        public int EmployerNo { get; set; }
        [DataMember(Name = "Forwarder", Order = 18)]
        [PropertyAssociation("FORW_NO", typeof(COptParam), Description = "Forwarder")]
        public int Forwarder { get; private set; }
        [DataMember(Name = "ForwarderFileNo", Order = 19)]
        public string ForwarderFileNo { get; private set; }
        [DataMember(Name = "ForwarderRefNo", Order = 20)]
        public string ForwarderRefNo { get; private set; }
        [DataMember(Name = "InterestCollected", Order = 21)]
        public double? InterestCollected { get; private set; }
        [DataMember(Name = "JudgmentAmount", Order = 22)]
        [PropertyAssociation("JMT_AMT", typeof(CBasicParam), Description = "Judgment Amount")]
        public double? JudgmentAmount { get; set; }
        [DataMember(Name = "JudgmentDate", Order = 23)]
        [PropertyAssociation("JMT_DATE", typeof(CDateParam), Description = "Judgment Date")]
        public DateTime? JudgmentDate { get; set; }
        [DataMember(Name = "LastLetter", Order = 24)]
        [PropertyAssociation("LAST_LETTER", typeof(CDateParam), Description = "Last Letter Date")]
        public DateTime? LastLetter { get; set; }
        [DataMember(Name = "LastPaymentAmount", Order = 25)]
        [PropertyAssociation("LAST_PAY_AMT", typeof(CBasicParam), Description = "Last Payment Amount")]
        public double? LastPaymentAmount { get; set; }
        [DataMember(Name = "LastPaymentDate", Order = 26)]
        [PropertyAssociation("LAST_PMT", typeof(CDateParam), Description = "Last Payment Date")]
        public DateTime? LastPaymentDate { get; set; }

        public DateTime? LastSkip { get; set; }
        //public MediaStatus MediaStatus { get; set; }
        [PropertyAssociation("MINSIFRATE", typeof(CBasicParam), Description = "Min Sif Rate")]
        public int MinSifRate { get { try { return SifRateFinder(this.MinSifRateString, this.Disposition); } catch { return 100; } } }
        [DataMember(Name = "MinSifRate", Order = 27)]
        public string MinSifRateString { get; set; }
        [DataMember(Name = "MinSuitBalance", Order = 28)]
        [PropertyAssociation("MIN_SUIT", typeof(CBasicParam), Description = "Minimum Suit Balance")]
        public double? MinSuitBalance { get; set; }

        public string NameFirst { get { return this.DebtorToUse?.NameFirst; } }
        public string NameMiddle { get { return this.DebtorToUse?.NameMiddle; } }
        public string NameLast { get { return this.DebtorToUse?.NameLast; } }
        public string NameSuffix { get { return this.DebtorToUse?.NameSuffix; } }

        [DataMember(Name = "NonSuitWorthy", Order = 29)]
        [PropertyAssociation("NSW", typeof(CSimpleParam), Description = "Non Suit-Worthy")]
        public bool? NonSuitWorthy { get; set; }
        [DataMember(Name = "OpenedDate", Order = 30)]
        [PropertyAssociation("OPENED_DATE", typeof(CDateParam), Description = "Opened Date")]
        public DateTime? OpenedDate { get; set; }
        [DataMember(Name = "OriginalClaim", Order = 31)]
        [PropertyAssociation("ORIG_CLAIM", typeof(CBasicParam), Description = "Original Claim Balance")]
        public double? OriginalClaim { get; set; }
        [DataMember(Name = "OriginalCreditor", Order = 32)]
        [PropertyAssociation("ORIG_CRED", typeof(CStringParam), Description = "Original Creditor")]
        public string OriginalCreditor { get; set; }
        [DataMember(Name = "Para", Order = 33)]
        [PropertyAssociation("PARA", typeof(COptParam), Description = "Para Number")]
        public int ParaNumber { get; set; }
        [PropertyAssociation("PER_PAID", typeof(CBasicParam), Description = "Percent Paid")]
        public double? PercentPaid { get { return this.AmountPaid / this.OriginalClaim; } }
        [DataMember(Name = "Plaintiff", Order = 34)]
        public string Plaintiff { get; set; }
        [DataMember(Name = "Plaintiff2", Order = 35)]
        public string Plaintiff2 { get; set; }
        [DataMember(Name = "PostJRate", Order = 36)]
        [PropertyAssociation("POST_RATE", typeof(CBasicParam), Description = "Interest Rate - Post Judgment")]
        public double? PostJudgmentInterestRate { get; set; }
        [DataMember(Name = "PreJRate", Order = 37)]
        [PropertyAssociation("PRE_RATE", typeof(CBasicParam), Description = "Interest Rate - Pre-Judgment")]
        public double? PreJudgmentInterestRate { get; set; }
        [DataMember(Name = "PrevLetter", Order = 38)]
        [PropertyAssociation("PREV_LETTER", typeof(CClassParam), Description = "Previous Letter")]
        public string PreviousLetter { get; set; }
        [DataMember(Name = "PrevLType", Order = 39)]
        [PropertyAssociation("PREV_LETTER_TYPE", typeof(CLetterParam), Description = "Previous Letter Type")]
        public int PreviousLetterType { get; set; }
        [DataMember(Name = "PrevLDate", Order = 40)]
        [PropertyAssociation("PREV_LETTER_TYPE", typeof(CLetterParam), Description = "Previous Letter Type", ParamNumber = 2)]
        public DateTime? PreviousLetterDate { get; set; }
        [DataMember(Name = "Sales", Order = 42)]
        [PropertyAssociation("SALES_NO", typeof(CBoolParam), Description = "Sales Number")]
        public int SalesNo { get; private set; }
        [DataMember(Name = "SherNo", Order = 43)]
        public int SheriffNo { get; set; }
        [PropertyAssociation("SIFRATE", typeof(CBasicParam), Description = "Sif Rate")]
        public int SifRate { get { try { return SifRateFinder(this.SifRateString, this.Disposition); } catch { return 100; } } }
        [DataMember(Name = "SifRate", Order = 44)]
        public string SifRateString { get; set; }
        [DataMember(Name = "Stat2Date", Order = 45)]
        [PropertyAssociation("STAT2_DATE", typeof(CDateParam), Description = "Stat2 Date")]
        public DateTime? Stat2Date { get; set; }
        [DataMember(Name = "Status", Order = 46)]
        [PropertyAssociation("STATUS", typeof(CBoolParam), Description = "Status Code")]
        public string Status { get; set; }
        [DataMember(Name = "StatusDate", Order = 47)]
        [PropertyAssociation("STATUS_DATE", typeof(CDateParam), Description = "Status Date")]
        public DateTime StatusDate { get; set; }
        [DataMember(Name = "StatuteDate", Order = 48)]
        [PropertyAssociation("STATUTE_DATE", typeof(CDateParam), Description = "Statute Date")]
        public DateTime? StatuteDate { get; set; }
        [DataMember(Name = "SuitDate", Order = 49)]
        [PropertyAssociation("SUIT_DATE", typeof(CDateParam), Description = "Suit Date")]
        public DateTime? SuitDate { get; set; }
        [DataMember(Name = "SuitScore", Order = 50)]
        [PropertyAssociation("SUIT_SCORE", typeof(CBasicParam), Description = "Suit Score")]
        public int? SuitScore { get; set; }
        [DataMember(Name = "SuitWorthy", Order = 51)]
        [PropertyAssociation("SW", typeof(CSimpleParam), Description = "Suit-Worthy")]
        public bool? SuitWorthy { get; set; }
        [DataMember(Name = "Trigger", Order = 52)]
        [PropertyAssociation("TRIGGER", typeof(CDateParam), Description = "Trigger Hit")]
        public DateTime? TriggerHit { get; set; }
        [DataMember(Name = "Venue", Order = 53)]
        [PropertyAssociation("VENUE_NO", typeof(CBasicParam), Description = "Venue No.")]
        public int Venue { get; set; }
        public bool? VerifiedAddress { get; set; }
        public bool? VerifiedPhone { get; set; }
        public bool? VerifiedPOE { get; set; }
        [DataMember(Name = "AcctRequest", Order = 54)]
        [PropertyAssociation("ACCT_REQ", typeof(CSimpleParam), Description = "Account Requested")]
        public bool AccountRequested { get; set; }
        #endregion

        #region Debtor Properties

        public int Debtor { get { return this.DebtorToUse?.Debtor ?? 0; } set { if (this.Debtors?.Count(el => el.Debtor == value) > 0) _DebtorToUse = value; } }
        [DataMember(Name = "Debtors", Order = 55)]
        public T[] Debtors { get; set; }

        private int _DebtorToUse { get; set; } = 1;
        public T DebtorToUse { get { return this.Debtors?.FirstOrDefault(el => el.Debtor == _DebtorToUse); } }

        public string Address { get { return this.DebtorToUse?.Address1; } }

        public string Address2 { get { return this.DebtorToUse?.Address2; } }
        [PropertyAssociation("AF_DATE", typeof(CDateParam), Description = "Answer Filed")]
        public DateTime? AnswerFiled { get { return this.DebtorToUse?.AnswerFiled; } }
        [PropertyAssociation("BK_CP", typeof(CBasicParam), Description = "Bankruptcy Chapter")]
        public string BankruptcyChapter { get { return this.DebtorToUse?.BankruptcyChapter; } }
        [PropertyAssociation("BK_DATE", typeof(CDateParam), Description = "Bankruptcy Date")]
        public DateTime? BankruptcyDate { get { return this.DebtorToUse?.BankruptcyDate; } }
        [PropertyAssociation("BK_FILE", typeof(CSimpleParam), Description = "Bankruptcy FileNo")]
        public string BankruptcyFileNo { get { return this.DebtorToUse?.BankruptcyFileNo; } }

        public string City { get { return this.DebtorToUse?.City; } }
        [PropertyAssociation("DEATH_DATE", typeof(CDateParam), Description = "Death Date")]
        public DateTime? DeathDate { get { return this.DebtorToUse?.DeathDate; } }
        [PropertyAssociation("DSMIS_DATE", typeof(CDateParam), Description = "Dismissal Date")]
        public DateTime? DismissalDate { get { return this.DebtorToUse?.DismissalDate; } }
        [PropertyAssociation("GARN_DATE", typeof(CDateParam), Description = "Garnishment Date")]
        public DateTime? GarnishmentDate { get { return this.DebtorToUse?.GarnishmentDate; } }
        [PropertyAssociation("HAS_PHONE", typeof(CSimpleParam), Description = "Has Phone")]
        public bool? HasPhone { get { return this.DebtorToUse?.HasPhone; } }
        [PropertyAssociation("HOME_OWN", typeof(CSimpleParam), Description = "Home Owner")]
        public bool? HomeOwner { get { return this.DebtorToUse?.HomeOwner; } }
        [PropertyAssociation("RET_MAIL", typeof(CSimpleParam), Description = "Return Mail")]
        public bool ReturnMail { get { return this.DebtorToUse?.ReturnMail ?? false; } }
        [PropertyAssociation("SERVICE_DATE", typeof(CDateParam), Description = "Service Date")]
        public DateTime? ServiceDate { get { return this.DebtorToUse?.ServiceDate; } }
        [PropertyAssociation("SSN", typeof(CSimpleParam), Description = "SSN")]
        public string SSN { get { return this.DebtorToUse?.SSN; } }
        [PropertyAssociation("STATE", typeof(CStringParam), Description = "State")]
        public string State { get { return this.DebtorToUse?.State; } }

        public string Zip { get { return this.DebtorToUse?.Zip; } }
        #endregion

        [OnDeserialized]
        void OnDeserialized(StreamingContext Context)
        {
            if (_DebtorToUse == 0) _DebtorToUse = 1;
            if (this.Debtors == null) this.Debtors = new T[] { };
            if (this.Diaries == null) this.Diaries = new List<DiaryCode>();
        }
    }
}
