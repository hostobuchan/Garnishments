using RecordTypes.PLX.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RecordTypes
{
    namespace PLX
    {
        namespace Base
        {
            public class PLXList<T> : IEnumerable where T : RecordTypeBase
            {
                private List<RecordTypeBase> _Records = new List<RecordTypeBase>();
                public List<RecordTypeBase> PLXRecords { get { return this._Records; } }
                public T this[int index] { get { return (T)this._Records[index]; } }
                public void Add(T Record) { this._Records.Add(Record); }
                public void Remove(T Record) { this._Records.Remove(Record); }
                public int Count { get { return this._Records.Count; } }

                public IEnumerator GetEnumerator() { return new RecordTypeBase.PLXEnumerator(this._Records); }
            }

            /// <summary>
            /// Base Record For Determining PLX Record Type
            /// </summary>
            public abstract class RecordTypeBase
            {
                public Types RecordType { get; private set; }

                public RecordTypeBase(Types RecordType)
                {
                    this.RecordType = RecordType;
                }
                public RecordTypeBase(string Record)
                {
                    this.RecordType = (Types)Enum.Parse(typeof(TypeValues), Record.Split('\t')[0]);
                }

                public override string ToString()
                {
                    return ((TypeValues)RecordType).ToString();
                }

                public class PLXEnumerator : IEnumerator
                {
                    public List<RecordTypeBase> RecordList;
                    private int position = -1;

                    public PLXEnumerator(List<RecordTypeBase> RecordList)
                    {
                        this.RecordList = RecordList;
                    }

                    private IEnumerator getEnumerator()
                    {
                        return (IEnumerator)this;
                    }

                    public bool MoveNext()
                    {
                        this.position++;
                        return (this.position < this.RecordList.Count);
                    }

                    public void Reset() { this.position = -1; }

                    public object Current { get { return this.RecordList[position]; } }
                }
            }

            /// <summary>
            /// Base Record From Which All PLX Account Related Records Inherit
            /// </summary>
            public abstract class RecordBase : RecordTypeBase
            {
                public int AccountID { get; private set; }
                public string AccountNumber { get; private set; }

                public RecordBase(string Record) : base(Record)
                {
                    this.AccountID = int.Parse(Record.Split('\t')[1]);
                    this.AccountNumber = Record.Split('\t')[2];
                }

                public override string ToString()
                {
                    return string.Format("{0}\t{1}\t{2}",
                        base.ToString(),
                        this.AccountID,
                        this.AccountNumber);
                }
            }
        }

        namespace Enums
        {
            /// <summary>
            /// Enumeration of PLX Record Type Descriptions
            /// </summary>
            public enum Types
            {
                Nothing,
                FileHeader,
                RecordHeader,
                BorrowerRecord,
                AccountBalanceRecord,
                PlacementRecord,
                PlaceBatchSummary,
                AdditionalInfoRecord,
                AlternateContactInfo,
                DeceasedInfo,
                LegalInfo,
                RelatedAsset_Bank,
                RelatedAsset_Employment,
                RelatedAsset_Other,
                RelatedAsset_RealEstate,
                RelatedAsset_Vehicle,
                AttorneyBondingInfo,
                BankruptcyInfo,
                JudgmentInfo,
                LawsuitInfo,
                InsufficientFundsInfo,
                StatusCodeRecordInfo,
                DormantJudgmentInfo,
                AccountScoreInfo,
                AdditionalInformationUpdateRecord,
                AccountNumberUpdateRecord,
                BankruptcyUpdateRecord,
                BorrowerAddressUpdateRecord,
                BorrowerPhoneUpdateRecord,
                BorrowerAdditionalInfoUpdateRecord,
                BorrowerDeceasedUpdateRecord,
                PlacementUpdateRecord,
                ScoreUpdateRecord,
                RecallRecord,
                BalanceItemizationRecord,
                BalanceItemizationUpdateRecord
            }
            /// <summary>
            /// Enumeration of PLX Record Type Values
            /// </summary>
            public enum TypeValues
            {
                Nothing,
                FHD,
                RHD,
                BWR,
                ABL,
                PLA,
                PBS,
                ADL,
                ALT,
                DEC,
                LGL,
                RAB,
                RAE,
                RAO,
                RAR,
                RAV,
                BND,
                BKY,
                JDG,
                SDI,
                NSF,
                SCD,
                DJG,
                SCR,
                ADU,
                ANU,
                BKU,
                BAU,
                BPU,
                BWU,
                DEU,
                PLU,
                SCU,
                RCX,
                BIZ,
                BIU
            }
            /// <summary>
            /// Enumeration of Language Selection
            /// </summary>
            public enum LanguageCodes
            {
                English = 0,
                Spanish = 1,
                Other = 3
            }
            /// <summary>
            /// Enumeration of Employment Descriptions
            /// </summary>
            public enum Employment
            {
                PartTime,
                FullTime,
                SelfEmployed,
                Unemployed,
                Retired,
                SocialSecurityIncome
            }
            /// <summary>
            /// Enumeration of Employment Values
            /// </summary>
            public enum EmploymentValues
            {
                PT,
                FT,
                SE,
                UE,
                RET,
                SSI
            }
            /// <summary>
            /// Enumeration of Borrower Types
            /// </summary>
            public enum BorrowerType
            {
                Borrower = 1,
                CoBorrower = 2
            }
            /// <summary>
            /// Enumeration of Borrower Phone Types
            /// </summary>
            public enum BorrowerPhoneType
            {
                Nothing = 0,
                Home = 1,
                Work = 2,
                Other = 3,
                Cell = 4
            }
            /// <summary>
            /// Enumeration of Method of Accrual
            /// </summary>
            public enum AccrualMethod
            {
                Accrue = 1,
                DoNotAccrue = 2,
                Suspended = 3
            }
            /// <summary>
            /// Enumeration of Alternative Contacts
            /// </summary>
            public enum AlternateContactType
            {
                Unknown = 0,
                Employer = 4,
                Attorney = 9,
                PowerOfAttorney = 16,
                AuthorizedRepresentative = 17,
                DebtSettlementAgency = 18,
                PowerOfAttorneyLimited = 19,
                ExecutorOfEstate = 20,
                Guardian = 21,
                ProbateCourt = 22
            }
            /// <summary>
            /// Enumeration of Judgment Types
            /// </summary>
            public enum JudgmentType
            {
                Unknown,
                Default,
                Contested,
                Stipulated
            }
            /// <summary>
            /// Enumeration of Judgment Type Values
            /// </summary>
            public enum JudgmentTypeValues
            {
                Unknown,
                D,
                C,
                S
            }
            /// <summary>
            /// Enumeration of Types of Service
            /// </summary>
            public enum ServiceType
            {
                Unknown,
                CertifiedMail,
                FirstClassMail,
                Personal,
                ServiceByPosting,
                ServiceByPublication,
                PrivateProcessServer,
                Sheriff,
                Other
            }
            /// <summary>
            /// Enumeration of Service Type Values
            /// </summary>
            public enum ServiceTypeValues
            {
                Unknown,
                CM,
                FCM,
                P,
                PG,
                PN,
                PS,
                S,
                U
            }
            /// <summary>
            /// Enumeration of Suit Outcomes
            /// </summary>
            public enum SuitOutcome
            {
                Unknown,
                JudgmentObtained,
                DismissedWithPrejudice,
                DismissedWithoutPrejudice,
                VacatedWithoutPrejudice,
                VacatedWithPrejudice
            }
            /// <summary>
            /// Enumeration of Suit Outcome Values
            /// </summary>
            public enum SuitOutcomeValues
            {
                Unknown,
                JO,
                DWP,
                DWO,
                VWO,
                VWP
            }
            /// <summary>
            /// Enumeration of Suit Dismissal Reasons
            /// </summary>
            public enum SuitDismissalReason
            {
                Unknown,
                UnableToServe,
                NoMedia,
                Paid,
                Bankrupt,
                Fraud,
                Incarcerated,
                Deceased,
                MilitaryActiveDuty,
                Welfare,
                PhysicalMentalIllness,
                NotCostEffective,
                ClosedPerClient
            }
            /// <summary>
            /// Enumeration of Suit Dismissal Values
            /// </summary>
            public enum SuitDismissalReasonValues
            {
                Unknown,
                NS,
                NM,
                PD,
                BK,
                FR,
                IN,
                DE,
                MA,
                WF,
                IL,
                NC,
                CC
            }
            /// <summary>
            /// Enumeration of Bankruptcy Status Descriptions
            /// </summary>
            public enum BankruptcyStatus
            {
                Unknown = 0,
                Filed = 2,
                Dismissed = 15,
                Discharged = 20,
                Converted = 30
            }

            /// <summary>
            /// Enumeration of Account Status IDs
            /// </summary>
            public enum AccountStatus
            {
                Unknown = 1,
                Collections = 10,
                BankruptcyPotential = 20,
                Legal = 30,
                Putback = 40,
                Closed = 60,
                Warehouse = 70,
                CCA = 80,
                PPA = 90,
                Sold = 100,
                Paying = 110,
                Dispute = 120,
                Deceased = 130,
                Fraud = 140,
                Suspense = 150,
                Settled = 160,
                ConfirmedBankruptcy = 170,
                PendingPaying = 180,
                ComplianceRecommendsNoPlacement = 280
            }

            /// <summary>
            /// Enumeration of Itemization Types
            /// </summary>
            public enum ItemizationType
            {
                Unknown = 0,
                ChargeOff,
                Judgment,
                LastPayment
            }

        }
        public static class Dictionaries
        {
            private static Dictionary<string, ItemizationType> _ItemizationTypeDict;
            public static Dictionary<string, ItemizationType> ItemizationTypeDictionary
            {
                get
                {
                    if (_ItemizationTypeDict == null)
                    {
                        _ItemizationTypeDict = new Dictionary<string, ItemizationType>()
                        {
                            { "", ItemizationType.Unknown },
                            { "judgment", ItemizationType.Judgment },
                            { "charge-off", ItemizationType.ChargeOff },
                            { "last payment", ItemizationType.LastPayment }
                        };
                    }
                    return _ItemizationTypeDict;
                }
            }
        }
    }
}
