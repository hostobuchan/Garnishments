using System;
using System.Collections;
using System.Collections.Generic;

namespace RecordTypes
{
    namespace NCO
    {
        namespace Base
        {
            /// <summary>
            /// Strongly Typed Table of NCO Records
            /// </summary>
            /// <typeparam name="T">Type of NCO Record</typeparam>
            public class NCOList<T> : IEnumerable where T : Record
            {
                private List<Record> _Records = new List<Record>();
                public List<Record> NCORecords { get { return this._Records; } }
                public T this[int index] { get { return (T)this._Records[index]; } }
                public void Add(T Record) { this._Records.Add(Record); }
                public void Remove(T Record) { this._Records.Remove(Record); }
                public int Count { get { return this._Records.Count; } }

                public IEnumerator GetEnumerator() { return new Record.NCOEnumerator(this._Records); }
            }

            /// <summary>
            /// Base For Determining NCO Record Types
            /// </summary>
            public abstract class Record
            {
                /// <summary>
                /// Record Type
                /// </summary>
                public DataTypes.NCOEnum<Enums.RecordTypes> RecordType { get; private set; }

                public Record(Enums.RecordTypes Record)
                {
                    this.RecordType = new DataTypes.NCOEnum<Enums.RecordTypes>(2) { Value = Record };
                }
                public Record(string Record)
                {
                    this.RecordType = new DataTypes.NCOEnum<Enums.RecordTypes>(2) { DataString = Record.Substring(0, 2) };
                }

                public class NCOEnumerator : IEnumerator
                {
                    public List<Record> RecordList;
                    private int position = -1;

                    public NCOEnumerator(List<Record> RecordList)
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
            /// Base Record From Which All NCO Account Related Records Inherit
            /// </summary>
            public abstract class RecordBase : Record
            {
                /// <summary>
                /// NCO Account Number
                /// </summary>
                public DataTypes.NCOString AccountNumber { get; private set; }

                public RecordBase(Enums.RecordTypes Record, string AccountNumber) : base(Record)
                {
                    this.AccountNumber = new DataTypes.NCOString(20) { Value = AccountNumber };
                }
                public RecordBase(string Record) : base(Record)
                {
                    this.AccountNumber = new DataTypes.NCOString(20) { DataString = Record.Substring(2, 20) };
                }
            }

            public abstract class TransactionCode { }
        }

        namespace DataTypes
        {
            /// <summary>
            /// Basic Character String
            /// </summary>
            public class NCOString : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

                public NCOString(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Date Stored as YYYYMMDD
            /// </summary>
            public class NCODate : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMdd"); } }

                public NCODate() : base(8) { }
            }
            /// <summary>
            /// Date Stored as YYYYMMDDHH:mm:ss
            /// </summary>
            public class NCODateTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMddHH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMddHH:mm:ss"); } }

                public NCODateTime() : base(16) { }
            }
            /// <summary>
            /// Basic Number with no decimals
            /// </summary>
            public class NCONumber : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.DataString = value == null || value < 0 || value > 10 * (base.DataLength + 1) ? "" : value.ToString(); } }

                public NCONumber(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Number with displayed decimal
            /// </summary>
            public class NCODecimal : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private int Precision;
                public new decimal? Value { get { try { return decimal.Parse(base.Value); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("F" + this.Precision.ToString()).PadRight(base.DataLength, ' ').Substring(value.Value.ToString("F" + this.Precision.ToString()).PadRight(base.DataLength, ' ').Length - base.DataLength); } }

                public NCODecimal(int Scale, int Precision) : base(Scale)
                {
                    this.Precision = Precision;
                }
            }
            /// <summary>
            /// Boolean data determined by the presence of char/string
            /// </summary>
            public class NCOBool : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private string Compare;
                private string Fail;
                public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

                public NCOBool(int DataLength, string CompareString, string FailString = "") : base(DataLength)
                {
                    this.Compare = CompareString;
                    this.Fail = FailString;
                }
            }
            /// <summary>
            /// Data is associated with a specific list
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            public class NCOEnum<T> : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new T Value { get { try { return (T)Enum.Parse(typeof(T), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = Convert.ToInt32(value).ToString().PadLeft(base.DataLength, '0'); } }

                public NCOEnum(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Data is associated with a specific list
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            /// <typeparam name="Q">Enumeration of List of Values</typeparam>
            public class NCOEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = value.ToString() == "Nothing" ? "".PadRight(base.DataLength, ' ') : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

                public NCOEnum(int DataLength) : base(DataLength) { }
            }
        }

        namespace Enums
        {
            /// <summary>
            /// Enumeration of NG Record Types
            /// </summary>
            public enum RecordTypes
            {
                /// <summary>
                /// Header
                /// </summary>
                RecordType00 = 0,
                /// <summary>
                /// Account Related Data
                /// </summary>
                RecordType01 = 1,
                /// <summary>
                /// Consumer Demographic
                /// </summary>
                RecordType02 = 2,
                /// <summary>
                /// Related Person
                /// </summary>
                RecordType03 = 3,
                /// <summary>
                /// Legal / Judgment
                /// </summary>
                RecordType04 = 4,
                /// <summary>
                /// Bankruptcy
                /// </summary>
                RecordType05 = 5,
                /// <summary>
                /// Additional Notes
                /// </summary>
                RecordType06 = 6,
                /// <summary>
                /// Financial Transactions
                /// </summary>
                RecordType07 = 7,
                /// <summary>
                /// Maintenance Record
                /// </summary>
                RecordType08 = 8,
                /// <summary>
                /// Reconciliation Record
                /// </summary>
                RecordType09 = 9,
                /// <summary>
                /// Payment / Maintenance Record
                /// </summary>
                RecordType10 = 10,
                /// <summary>
                /// Trailer
                /// </summary>
                RecordType99 = 99
            }
            /// <summary>
            /// Enumeration of Interest Codes
            /// </summary>
            public enum InterestCode
            {
                Unknown = 0,
                None = 1,
                Simple = 2,
                Compound = 3,
                Periodic = 4
            }

            public enum AddressStatus
            {
                Empty = 0,
                Valid = 1,
                Invalid = 2,
                New = 3
            }

            public enum RelationshipCode
            {
                Consumer = 0,
                CoMaker1 = 1,
                CoMaker2 = 2,
                CoMaker3 = 3,
                CoMaker4 = 4,
                CoMaker5 = 5,
                JointOwner = 10,
                CoOwner = 20,
                AllConsumers = 99
            }

            public enum ServiceType
            {
                Other = 0,
                Mail = 1,
                SPS = 2,
                Sheriff = 3,
                Posting = 4,
                Publication = 5
            }

            public enum ChapterCode
            {
                Unknown = 0,
                Chapter7 = 1,
                Chapter11 = 2,
                Chapter12 = 3,
                Chapter13 = 4
            }

            public enum DispositionCode
            {
                Unknown = 0,
                Open = 1,
                Discharged = 2,
                Dismissed = 3
            }

            public enum FinancialType
            {
                Unknown = 0,
                Payment = 1,
                PaymentNSF = 2,
                PaymentReversal = 3,
                CourtCostSpent = 4,
                CourtCostSpentReversal = 5,
                CourtCostSpent_NonBalanceImpacting = 6,
                CourtCostSpent_NonBalanceImpactingReversal = 7,
                Adjustment_Positive = 8,
                Adjustment_Negative = 9,
                ClientPayment = 10,
                ClientPaymentReversal = 11,
                ClientAdjustment_Positive = 12,
                ClientAdjustment_Negative = 13,
                BalanceInFullPayment = 14,
                SettlementInFullPayment = 15
            }

            public enum PaymentAllocation
            {
                Unknown = 0,
                Principal = 1,
                Interest = 2,
                CourtCost = 3,
                AttorneyFees = 4,
                OtherFees = 5,
                NSFFees = 6,
                CollectionCharge = 7,
                Bucket8 = 8,
                Bucket9 = 9,
                Bucket10 = 10
            }

            public enum CourtCostType
            {
                Unknown = 0,
                SuitFilingFee = 1,
                Dismissal = 2,
                ArbitrationFee = 3,
                SubstitutionOfCounsel = 4,
                SuitOther = 5,
                EFile = 6,
                LexisNexisFiling = 7,
                ArbitrationFilingFee = 8,
                ExemplifiedJudgment = 9,
                Motion_SummaryJudgment = 10,
                JudgmentRenewal = 11,
                JudgmentFiling = 12,
                JudgmentSatisfaction = 13,
                JudgmentCopy = 14,
                TransferJudgment = 15,
                DocketJudgmentFee = 16,
                CertificateOfJudgment = 17,
                TranscriptFee = 18,
                FilingIndex = 20,
                SheriffFee = 21,
                ProcessServerFilingFee = 22,
                ReissueService = 23,
                ServiceOfProcess = 24,
                ServiceOfPublication = 25,
                PostJudgmentService = 27,
                ServingSummons = 28,
                BankGarnishmentFilingFee = 29,
                WageGarnishmentFilingFee = 30,
                LienFilingFee = 31,
                DebtorExaminationFiled = 32,
                AssetLocation = 33,
                FIFA = 34,
                PropertyExecution = 35,
                LevyFilingFee = 36,
                PostJudgmentOtherFee = 37,
                Interrogatories = 38,
                LienReleased = 39,
                AliasFee = 40,
                Citation = 41,
                SubpoenaFee = 42,
                Capias_DebtorExam = 43,
                Depositions = 44,
                ExParte = 45,
                Mediation = 46,
                NoticeOfTrial = 47,
                Petition = 48,
                StipFilingFee = 49,
                AttorneyFee = 50,
                BadAddress = 51,
                CertifiedMail = 52
            }

            //Incomplete - Not Implemented
            public enum TransactionCodes
            {
                B341A,
                B341S,
                BA341,
                BACTR,
                BAHRS,
                BCAPR,
                BKAND,
                BKATY,
                BKBLA,
                BKBLF,
                BKCNV,
                BKCON,
                BKDIS,
                BKDSC,
                BKERR,
                BKFIL,
                BKPCA,
                BKPCF,
                BKPCM,
                BKPCO,
                BKPCW,
                BKPCX,
                BKPEN,
                BKPTR,
                BKUNS,
                BLNSV,
                BN341,
                BPOCM,
                BPOCR,
                BRADQ,
                BRANA,
                BRAOB,
                BRDEM,
                BRDSG,
                BREFM,
                BREFS,
                BRFFL,
                BRFMC,
                BRFRA,
                BRFRN,
                BRFTN,
                BRFTO,
                BRHRS,
                BROMA,
                BRORA,
                BRSGN,
                CADIR,
                CICAT,
                CICDS,
                CICNR,
                CICPR,
                COANA,
                COAPR,
                COALM,
                COHCT,
                COHDC,
                COHLM,
                COHNA,
                COHPR,
                COHRF,
                CORLM,
                COWCT,
                COWLM,
                COWNA,
                COWPR,
                IADDU,
                IAHIT,
                IAUTV,
                IBNKV,
                ICCSR,
                ICCSS,
                ICELL,
                IDATY,
                IDEAD,
                IDIRA,
                IDISA,
                IDNLE,
                IDSTS,
                IENIB,
                IHITN,
                IHITY,
                IIRSL,
                IJAIL,
                ILPAM,
                ILPDT,
                IMRCH,
                IOPEN,
                IOVPR,
                IOVPY,
                IPHNV,
                IPOES,
                IPOEV,
                IPOSC,
                IPRPV,
                IRETM,
                ISCRB,
                ISEXH,
                ISKBA,
                ISKIP,
                ISLOC,
                ISSAA,
                ISSTR,
                ITRLF,
                ITRUP,
                LADVD,
                LADVP,
                LANCC,
                LANNF,
                LANSR,
                LARBF,
                LARBR,
                LASUM,
                LBARD,
                LBOCS,
                LCCLV,
                LCHAT,
                LCHSC,
                LCNCL,
                LCNER,
                LCONS,
                LCONT,
                LCTST,
                LDATE,
                LDBEX,
                LDEPO,
                LDEPT,
                LDJRQ,
                LDSCR,
                LFIFA,
                LGANS,
                LGDEF,
                LGDEX,
                LGDIS,
                LGFDC,
                LGFIL,
                LGJDG,
                LGLBN,
                LGLIN,
                LGMAX,
                LGSVD,
                LGTRV,
                LGUNS,
                LINTD,
                LINTP,
                LINTR,
                LIGER,
                LIGEX,
                LIGHL,
                LIGRJ,
                LISAT,
                LJUDG,
                LLEVY,
                LLIEN,
                LMDMS,
                LMLST,
                LMOTC,
                LMOTR,
                LMPJM,
                LMRDN,
                LMRGR,
                LMRHR,
                LOBJC,
                LPEPD,
                LPJDC,
                LPJDS,
                LPJIN,
                LPRES,
                LRCOM,
                LRINT,
                LRQAR,
                LRQAD,
                LRQPL,
                LSDPR,
                LSISS,
                LSMJQ,
                LSNDR,
                LSRVC,
                LSTAU,
                LSTDW,
                LSTER,
                LSTNA,
                LSTND,
                LSTNR,
                LSTRC,
                LSTRQ,
                LSUBC,
                LSUIT,
                LSUPP,
                LSVCR,
                LTRDT,
                LTRWN,
                LUTSV,
                LVACJ,
                LVENA,
                LVEND,
                LWEXJ,
                LWEXQ,
                LWITC,
                LWITQ,
                LWTNA,
                MAFAD,
                MAFAF,
                MAFAQ,
                MAFAR,
                MAFAS,
                MAFAY,
                MAFAZ,
                MDOCD,
                MDOCF,
                MDOCQ,
                MDOCR,
                MDOCS,
                MDOCX,
                MDOCY,
                MDOCZ,
                MCOPD,
                MCOPF,
                MCOPN,
                MCOPQ,
                MCOPR,
                MCOPS,
                MCOPX,
                MCOPZ,
                S6SKP,
                SACKN,
                SAORI,
                SATFU,
                SAUDT,
                SCACT,
                SCALL,
                SCALN,
                SCAND,
                SCBUQ,
                SCCMR,
                SCCNM,
                SCDUS,
                SCLOS,
                SCNDH,
                SCNUP,
                SCOAA,
                SCOMM,
                SCORE,
                SCORV,
                SCRGR,
                SCRQS,
                SCRTR,
                SCSTP,
                SDEFC,
                SDISP,
                SDISR,
                SDLNC,
                SDLUP,
                SDSCA,
                SDSCR,
                SDSCX,
                SFRAD,
                SHOLD,
                SINPR,
                SIRTQ,
                SIRTR,
                SIRTS,
                SIRTV,
                SKEEP,
                SLETD,
                SLETQ,
                SLETS,
                SLETR,
                SMGRR,
                SMRCH,
                SNOAS,
                SNPRD,
                SNPRP,
                SPALM,
                SPALU,
                SPCDD,
                SPDCR,
                SPDCV,
                SPPAD,
                SPPDT,
                SPPLA,
                SPROM,
                SPSIF,
                SRCAL,
                SRCLE,
                SRCYC,
                SREOP,
                SREPO,
                SRPLV,
                SRSTR,
                SSIFX,
                SSIFA,
                SSIFD,
                SSIFQ,
                SSOLE,
                SSRCL,
                STEMP,
                STMEX,
                STRDA,
                STRFU,
                SVOLR,
                ZCAMT,
                ZCONS,
                ZFLIP,
                ZFWDA,
                ZIDNT,
                ZILOC,
                ZINIU,
                ZMASS,
                ZRTRN,
                ZTRTU,
                ZVNDR,
                ZVNDS
            }
        }
    }
}
