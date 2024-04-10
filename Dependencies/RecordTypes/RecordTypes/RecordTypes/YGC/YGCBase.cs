using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes
{
    namespace YGC
    {
        namespace Base
        {
            /// <summary>
            /// Strongly Typed Table of YGC Records
            /// </summary>
            /// <typeparam name="T">Type of YGC Record</typeparam>
            public class YGCList<T> : IEnumerable where T : YGCBase
            {
                private List<YGCBase> _Records = new List<YGCBase>();
                public List<YGCBase> YGCRecords { get { return this._Records; } }
                public T this[int index] { get { return (T)this._Records[index]; } }
                public void Add(T Record) { this._Records.Add(Record); }
                public void Remove(T Record) { this._Records.Remove(Record); }
                public int Count { get { return this._Records.Count; } }

                public IEnumerator GetEnumerator() { return new YGCBase.YGCEnumerator(this._Records); }
            }
            /// <summary>
            /// Base Data Type for all YGC Records
            /// </summary>
            public abstract class YGCBase : IComparable<YGCBase>, RecordTypes.EDI.EDIRecords.RecordType<YGCBase>
            {
                #region Public Properties
                public string RECORD { get; private set; }
                /// <summary>
                /// This is the internal file number of the sender (client) placing the account.
                /// </summary>
                public YGC.DataTypes.YGCString FILENO { get; private set; }
                /// <summary>
                /// This is the account number issued by the original creditor. In the retail card arena, this is typically the credit card number.
                /// </summary>
                public YGC.DataTypes.YGCString FORW_FILE { get; private set; }
                /// <summary>
                /// This is the internal file number of the receiver (agency/law firm) working the account. It can be blank in this record. The receiver will initially provide this field for later use by the sender in subsequent records.
                /// </summary>
                public YGC.DataTypes.YGCString MASCO_FILE { get; private set; }
                /// <summary>
                /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public abstract YGC.DataTypes.YGCString FORW_ID { get; protected set; }
                /// <summary>
                /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public abstract YGC.DataTypes.YGCString FIRM_ID { get; protected set; }
                #endregion

                public YGCBase(int RecordType)
                {
                    this.RECORD = RecordType.ToString().PadLeft(2, '0');
                    this.FILENO = new DataTypes.YGCString(10);
                    this.FORW_FILE = new DataTypes.YGCString(20);
                    this.MASCO_FILE = new DataTypes.YGCString(15);
                }
                public YGCBase(string YGCRecord)
                {
                    this.RECORD = YGCRecord.Substring(0, 2);
                    try
                    {
                        this.FILENO = new DataTypes.YGCString(10) { DataString = YGCRecord.Length > 2 ? YGCRecord.Substring(2) : "" };
                        this.FORW_FILE = new DataTypes.YGCString(20) { DataString = YGCRecord.Length > 12 ? YGCRecord.Substring(12) : "" };
                        this.MASCO_FILE = new DataTypes.YGCString(15) { DataString = YGCRecord.Length > 32 ? YGCRecord.Substring(32) : "" };
                    }
                    catch { }
                }

                public new abstract Type GetType();

                public override string ToString()
                {
                    return string.Format("{0}{1}{2}{3}",
                        this.RECORD,
                        this.FILENO,
                        this.FORW_FILE,
                        this.MASCO_FILE);
                }

                public static RecordTypes2.FileReaders.FileReader<YGCBase> GetFileReader(string FileName)
                {
                    return new RecordTypes2.FileReaders.FileReader<Base.YGCBase>(FileName, SupportedEDITypes.YGC);
                }

                public class YGCEnumerator : IEnumerator
                {
                    public List<YGCBase> RecordList;
                    private int position = -1;

                    public YGCEnumerator(List<YGCBase> RecordList)
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

                #region IComparable Members

                public int CompareTo(YGCBase obj)
                {
                    if (string.Compare(this.FORW_FILE.Value, obj.FORW_FILE.Value) == 0 &&
                        string.Compare(this.RECORD, obj.RECORD) == 0) return 0;
                    else if (string.Compare(this.FORW_FILE.Value, obj.FORW_FILE.Value) > 0) return 1;
                    else if (string.Compare(this.FORW_FILE.Value, obj.FORW_FILE.Value) == 0 &&
                        string.Compare(this.RECORD, obj.RECORD) > 0) return 1;
                    else return -1;
                }

                #endregion

                #region RecordType<YGCBase> Members

                public YGCBase GetRecordType(string Record)
                {
                    try
                    {
                        string myType = "RecordTypes2.YGC.RecordType" + (new RecordTypes.FileReaders.YGCBaseRecord(Record).RECORD).ToString();
                        Type T = Type.GetType(myType);
                        return (YGCBase)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

                public List<YGCBase> GetAccountRecords(CLS.Account Account, List<YGCBase> Records)
                {
                    return Records.Where(el => el.FORW_FILE.Value == Account.Forw_FileNo).ToList();
                }

                public void BasicFileMaintenance(List<YGCBase> Records)
                {
                    // Put Last Payment from Issuer into Last Payment Date, If No Last Payment Date
                    foreach (YGC.RecordType01 RT1 in Records.OfType<YGC.RecordType01>())
                    {
                        if (RT1.DATE_LPAY.Value == null && RT1.LPAY_ISS_D.Value != null)
                            RT1.DATE_LPAY.Value = RT1.LPAY_ISS_D.Value;
                    }
                }

                public void AddHeaders(List<YGCBase> BaseList, List<YGCBase> AddList)
                {
                    return;
                }

                public YGCBase PlacementRecord(List<YGCBase> Records, string AccountNumber)
                {
                    return Records.OfType<RecordTypes.YGC.RecordType01>().Where(el => el.FORW_FILE.Value == AccountNumber).FirstOrDefault();
                }

                public int NewAccounts(List<YGCBase> Records)
                {
                    return Records.OfType<RecordTypes.YGC.RecordType01>().GroupBy(el => el.FORW_FILE).Select(el => el.First()).Count();
                }

                public IEnumerable<List<YGCBase>> UniqueAccountListing(List<YGCBase> Records)
                {
                    foreach (RecordTypes.YGC.Base.YGCBase R in Records.GroupBy(el => el.FORW_FILE.Value).Select(el => el.First()))
                    {
                        yield return Records.FindAll(el => el.FORW_FILE.Value == R.FORW_FILE.Value && el.FILENO.Value == R.FILENO.Value && el.MASCO_FILE.Value == R.MASCO_FILE.Value).ToList();
                    }
                }

                #endregion
            }

            public abstract class YGCSender2ReceiverRecord : YGCBase
            {
                #region Public Properties
                /// <summary>
                /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public sealed override YGC.DataTypes.YGCString FORW_ID { get; protected set; }
                /// <summary>
                /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public sealed override YGC.DataTypes.YGCString FIRM_ID { get; protected set; }
                #endregion

                public YGCSender2ReceiverRecord(int RecordType) : base(RecordType)
                {
                    this.FORW_ID = new DataTypes.YGCString(10);
                    this.FIRM_ID = new DataTypes.YGCString(10);
                }
                public YGCSender2ReceiverRecord(string YGCRecord) : base(YGCRecord)
                {
                    this.FORW_ID = new DataTypes.YGCString(10) { DataString = YGCRecord.Substring(47, 10) };
                    this.FIRM_ID = new DataTypes.YGCString(10) { DataString = YGCRecord.Substring(57, 10) };
                }

                public override string ToString()
                {
                    return string.Format("{0}{1}{2}",
                        base.ToString(),
                        this.FORW_ID,
                        this.FIRM_ID);
                }
            }

            public abstract class YGCReceiver2SenderRecord : YGCBase
            {
                #region Public Properties
                /// <summary>
                /// This is the identification code of the receiver (agency/law firm) working the account. This ID code is pre-assigned to the receiver by ACC; however, the sender determines who is to receive the account. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public sealed override YGC.DataTypes.YGCString FIRM_ID { get; protected set; }
                /// <summary>
                /// This is the identification code of the sender (client) placing the account. This ID code is assigned by ACC. It is typically a 2 character state code followed by a 1-3 digit number (for example NJ750). It may also end with a subaccount extension to distinguish accounts by type or portfolio (for example, OZ12.MED and OZ12.AUTO).
                /// </summary>
                public sealed override YGC.DataTypes.YGCString FORW_ID { get; protected set; }
                #endregion

                public YGCReceiver2SenderRecord(int RecordType) : base(RecordType)
                {
                    this.FIRM_ID = new DataTypes.YGCString(10);
                    this.FORW_ID = new DataTypes.YGCString(10);
                }
                public YGCReceiver2SenderRecord(string YGCRecord) : base(YGCRecord)
                {
                    this.FIRM_ID = new DataTypes.YGCString(10) { DataString = YGCRecord.Length > 47 ? YGCRecord.Substring(47) : "" };
                    this.FORW_ID = new DataTypes.YGCString(10) { DataString = YGCRecord.Length > 57 ? YGCRecord.Substring(57) : "" };
                }

                public override string ToString()
                {
                    return string.Format("{0}{1}{2}",
                        base.ToString(),
                        this.FIRM_ID,
                        this.FORW_ID);
                }
            }
        }

        namespace DataTypes
        {
            /// <summary>
            /// Basic Character String
            /// </summary>
            public class YGCString : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new string Value { get { return base.DataString.Trim(); } set { base.Value = value; } }

                public YGCString(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Character string with reference to extra field if string is too long
            /// </summary>
            public class YGCExtString : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private YGCExtString Ext;

                public new string DataString
                {
                    get { return base.DataString; }
                    set
                    {
                        if (string.IsNullOrEmpty(value))
                            value = "";
                        if (value.Length > base.DataLength && Ext != null)
                        {
                            base.Value = value.Substring(0, value.Substring(0, base.DataLength).LastIndexOf(' '));
                            Ext.Value = value.Substring(value.Substring(0, base.DataLength).LastIndexOf(' ') + 1);
                        }
                        else
                        {
                            base.DataString = value.Length < DataLength ? value.PadRight(DataLength, ' ') : value.Substring(0, DataLength);
                        }
                    }
                }
                public new string Value { get { return base.Value; } set { this.DataString = value; } }

                public YGCExtString(int DataLength, YGCExtString Extension) : base(DataLength) { this.Ext = Extension; }

                public override string ToString()
                {
                    return this.DataString;
                }
            }
            /// <summary>
            /// Date Stored as YYYYMMDD
            /// </summary>
            public class YGCDate : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMdd"); } }

                public YGCDate() : base(8) { }
            }
            /// <summary>
            /// Time Stored as HHMMSS on a 24-Hour Clock
            /// </summary>
            public class YGCTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "HHmmss", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("HHmmss"); } }

                public YGCTime() : base(6) { }
            }
            /// <summary>
            /// Time Stored as HH:MM on a 24-Hour Clock
            /// </summary>
            public class YGCStupidTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "HH:mm", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("HH:mm"); } }

                public YGCStupidTime() : base(5) { }
            }
            /// <summary>
            /// Date & Time Stored as yyyyMMddHHMMSS on a 24-Hour Clock
            /// </summary>
            public class YGCDateTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value
                {
                    get
                    {
                        try
                        {
                            switch (base.Value.Length)
                            {
                                case 6:
                                    return DateTime.ParseExact(base.Value, "HHmmss", System.Globalization.CultureInfo.CurrentCulture);
                                case 8:
                                    return DateTime.ParseExact(base.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                default:
                                    return DateTime.ParseExact(base.Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                            }
                        }
                        catch { return null; }
                    }
                    set
                    {
                        if (value.HasValue)
                        {
                            if (value.Value.Hour == 0 && value.Value.Minute == 0 && value.Value.Second == 0)
                            {
                                base.DataString = value.Value.ToString("yyyyMMdd");
                            }
                            else
                            {
                                base.DataString = value.Value.ToString("yyyyMMddHHmmss");
                            }
                        }
                        else
                        {
                            base.DataString = "";
                        }
                    }
                }

                public YGCDateTime() : base(14) { }
            }
            /// <summary>
            /// Basic Number with no decimals
            /// </summary>
            public class YGCNumber : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public virtual new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.Value = value == null ? "" : value.ToString(); } }

                public YGCNumber(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Number with displayed decimal
            /// </summary>
            public class YGCDecimal : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private int Precision;
                private bool NoLeadingZero;
                public new decimal? Value { get { try { return decimal.Parse(base.Value); } catch { return null; } } set { string dataString = value == null ? "" : value.Value.ToString("F" + this.Precision.ToString()); if (this.NoLeadingZero) dataString = dataString.Substring(dataString.IndexOf('.')); base.Value = dataString; } }

                public YGCDecimal(int Scale, int Precision, bool NoLeadingZero = false)
                    : base(Scale, false)
                {
                    this.Precision = Precision;
                    this.NoLeadingZero = NoLeadingZero;
                }
            }
            /// <summary>
            /// Boolean data determined by the presence of char/string
            /// </summary>
            public class YGCBool : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private string Compare;
                private string Fail;
                public new bool Value { get { return base.Value == this.Compare; } set { base.Value = value ? this.Compare : this.Fail; } }

                public YGCBool(int DataLength, string CompareString, string FailString = "")
                    : base(DataLength)
                {
                    this.Compare = CompareString;
                    this.Fail = FailString;
                }
            }
            /// <summary>
            /// Data is associated with a specific list
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            /// <typeparam name="Q">Enumeration of List of Values</typeparam>
            public class YGCEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = Convert.ToInt32(value) == 0 ? "" : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

                public YGCEnum(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Data is associated with a specific list
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            public class YGCEnum<T> : RecordTypes.YGC.DataTypes.YGCNumber
            {
                public new T Value { get { try { return (T)Enum.ToObject(typeof(T), base.Value.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = Convert.ToInt32(value); } }

                public YGCEnum(int DataLength) : base(DataLength) { }
            }
            /// <summary>
            /// Data is associated with a specific list which requires a Dictionary
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            public class YGCDict<T> : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private Dictionary<string, T> Dictionary { get; set; }
                public new T Value
                {
                    get
                    {
                        try { return this.Dictionary[base.Value]; }
                        catch { return (T)Enum.ToObject(typeof(T), 0); }
                    }
                    set
                    {
                        try { base.Value = this.Dictionary.Where(el => Enum.Equals(el.Value, value)).Select(el => el.Key).First(); }
                        catch { base.Value = ""; }
                    }
                }

                public YGCDict(int DataLength, Dictionary<string, T> Dictionary, bool AlignLeft = true) : base(DataLength, AlignLeft)
                {
                    this.Dictionary = Dictionary;
                }
            }
        }

        namespace Enums
        {
            /// <summary>
            /// Enumeration of Byte-Code Salutation Field
            /// </summary>
            public enum Salutation
            {
                Nothing = 0,
                Mr = 1,
                Mrs = 2,
                Ms = 3,
                MrAndMrs = 4,
                Dr = 5,
                Capt = 6,
                Gentlemen = 7,
                Gentlemen_2 = 8,
                Gentlemen_3 = 9
            }
            /// <summary>
            /// Enumeration of Possible Income Frequency Descriptions
            /// </summary>
            public enum IncomeFrequency
            {
                Nothing,
                Hourly,
                Annual,
                Weekly,
                Monthly,
                BiMonthly,
                SemiMonthly
            }
            /// <summary>
            /// Enumeration of Possible Income Frequency Values
            /// </summary>
            public enum IncomeFrequencyValue
            {
                Nothing,
                H,
                A,
                W,
                M,
                B,
                S
            }
            /// <summary>
            /// Enumeration of Possible Phone Type Descriptions
            /// </summary>
            public enum PhoneType
            {
                Nothing,
                Home,
                SecondaryHome,
                Cell,
                SecondaryCell,
                Work,
                SecondaryWork,
                Fax
            }
            /// <summary>
            /// Enumeration of Possible Phone Type Values
            /// </summary>
            public enum PhoneTypeValue
            {
                Nothing,
                H,
                H2,
                C,
                C2,
                W,
                W2,
                F
            }
            /// <summary>
            /// Enumeration of Phone Source Descriptions
            /// </summary>
            public enum PhoneSource
            {
                Nothing,
                Issuer,
                Riskwise_LexisNexis,
                CollectionAgency,
                Acxiom,
                NSTN,
                Manual,
                FirstData,
                Transunion,
                USPostalService,
                Experian,
                Transunion2,
                NationalChangeOfAddress,
                YouveGotClaims_ACC,
                USPostalService1,
                USPostalService2,
                USPostalService3,
                FirstData2,
                NationalAttorneyNetwork
            }
            /// <summary>
            /// Enumeration of Phone Source Values
            /// </summary>
            public enum PhoneSourceValues
            {
                Nothing,
                ISS,
                RW,
                CAO,
                IA,
                NS,
                MAN,
                FD,
                WATCH,
                USPS,
                SCNL,
                TUSCOR,
                NCOA,
                YGC,
                USPS1,
                USPS2,
                USPS3,
                FRSDTA,
                NAN
            }
            /// <summary>
            /// Enumeration of Call Direction
            /// </summary>
            public enum CallDirection
            {
                Nothing,
                Incoming,
                Outgoing
            }
            /// <summary>
            /// Enumeration of Call Direction Values
            /// </summary>
            public enum CallDirectionValues
            {
                Nothing,
                I,
                O
            }
            /// <summary>
            /// Enumeration of Possible Verification Descriptions
            /// </summary>
            public enum Verification
            {
                Nothing,
                Verified,
                NotVerified,
                Bad
            }
            /// <summary>
            /// Enumeration of Possible Verification Values
            /// </summary>
            public enum VerificationValues
            {
                Nothing,
                V,
                N,
                B
            }
            /// <summary>
            /// Enumeration of Time Zones
            /// </summary>
            public enum TimeZone
            {
                Unknown,
                USA_Eastern,
                USA_Central,
                USA_Mountain,
                USA_Pacific,
                Alaska,
                Hawaii
            }
            /// <summary>
            /// Type of Service Enumeration
            /// </summary>
            public enum ServiceType
            {
                Nothing = 0,
                Personal,
                CertifiedMail,
                SubService,
                Posting_LeftAtDoor,
                FirstClassMail
            }
            /// <summary>
            /// Type of Service Value Enumeration
            /// </summary>
            public enum ServiceTypeValues
            {
                Nothing = 0,
                PER,
                CER,
                SUB,
                POS,
                FIR
            }
        }

        public static class Dictionaries
        {
            /// <summary>
            /// TimeZone Dictionay to Look Up TimeZone from YGC Format
            /// </summary>
            public static Dictionary<string, Enums.TimeZone> TimeZoneDictionary
            {
                get
                {
                    return new Dictionary<string, Enums.TimeZone>(){
                        { "GMT-5", Enums.TimeZone.USA_Eastern },
                        { "GMT-6", Enums.TimeZone.USA_Central },
                        { "GMT-7", Enums.TimeZone.USA_Mountain },
                        { "GMT-8", Enums.TimeZone.USA_Pacific },
                        { "GMT-9", Enums.TimeZone.Alaska },
                        { "GMT-10", Enums.TimeZone.Hawaii }
                    };
                }
            }
        }
    }
}
