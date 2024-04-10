using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes
{
    namespace NAN
    {
        namespace Base
        {
            /// <summary>
            /// Strongly Typed Table of NAN Records
            /// </summary>
            /// <typeparam name="T">Type of NAN Record</typeparam>
            public class NANList<T> : IEnumerable where T : Record
            {
                private List<T> _Records = new List<T>();
                public List<T> NANRecords { get { return this._Records; } }
                public T this[int index] { get { return (T)this._Records[index]; } }
                public void Add(T Record) { this._Records.Add(Record); }
                public void Remove(T Record) { this._Records.Remove(Record); }
                public int Count { get { return this._Records.Count; } }

                public IEnumerator GetEnumerator() { return new Record.NANEnumerator<T>(this._Records); }
            }

            public abstract class Record
            {
                public class NANEnumerator<T> : IEnumerator where T : Record
                {
                    public List<T> RecordList;
                    private int position = -1;

                    public NANEnumerator(List<T> RecordList)
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
        }

        namespace DataTypes
        {
            /// <summary>
            /// Basic Character String
            /// </summary>
            public class NANString : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

                public NANString(int DataLength, bool LeftAlign = true) : base(DataLength, LeftAlign) { }
            }
            /// <summary>
            /// Date Stored as YYYYMMDD
            /// </summary>
            public class NANDate : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMdd"); } }

                public NANDate() : base(8) { }
            }
            /// <summary>
            /// Time Stored as HH:mm
            /// </summary>
            public class NANTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "HH:mm", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("HH:mm"); } }

                public NANTime() : base(5) { }
            }
            /// <summary>
            /// Date Stored as YYYYMMDDHH:mm:ss
            /// </summary>
            public class NANDateTime : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyyyMMddHH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyyyMMddHH:mm:ss"); } }

                public NANDateTime() : base(16) { }
            }
            /// <summary>
            /// Basic Number with no decimals
            /// </summary>
            public class NANNumber : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private int? PadLength { get; set; }
                public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.Value = value == null || value < 0 || value > 10 * (base.DataLength + 1) ? "" : value.ToString().PadLeft(this.PadLength.HasValue ? this.PadLength.Value : base.DataLength, '0'); } }

                public NANNumber(int DataLength, int? PadLength = null) : base(DataLength) { this.PadLength = PadLength; }
            }
            /// <summary>
            /// Number with displayed decimal
            /// </summary>
            public class NANDecimal : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private int Precision;
                public new decimal? Value { get { try { if (base.Value.Contains('.')) { return decimal.Parse(base.Value); } else { return decimal.Parse(base.Value) / (decimal)(Math.Pow(10, Precision)); } } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("F" + this.Precision.ToString()).PadLeft(base.DataLength, ' ').Substring(value.Value.ToString("F" + this.Precision.ToString()).PadLeft(base.DataLength, ' ').Length - base.DataLength); } }

                public NANDecimal(int Scale, int Precision, bool AlignLeft = true)
                    : base(Scale, AlignLeft)
                {
                    this.Precision = Precision;
                }
            }
            /// <summary>
            /// Boolean data determined by the presence of char/string
            /// </summary>
            public class NANBool : RecordTypes.EDI.EDIDataTypes.DataType
            {
                private string Compare;
                private string Fail;
                public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

                public NANBool(int DataLength, string CompareString, string FailString = "")
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
            public class NANEnum<T> : NANNumber
            {
                public new T Value { get { try { return (T)Enum.ToObject(typeof(T), base.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = Convert.ToInt32(value); } }

                public NANEnum(int DataLength, int? PadLength = null) : base(DataLength, PadLength) { }
            }
            /// <summary>
            /// Data is associated with a specific list
            /// </summary>
            /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
            /// <typeparam name="Q">Enumeration of List of Values</typeparam>
            public class NANEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
            {
                public new T Value
                {
                    get
                    {
                        try
                        {
                            if (new System.Text.RegularExpressions.Regex("\\d").Match(base.Value.Substring(0, 1)).Success)
                            {
                                return (T)Enum.Parse(typeof(Q), "_" + base.Value);
                            }
                            else
                            {
                                return (T)Enum.Parse(typeof(Q), base.Value);
                            }
                        }
                        catch { return (T)Enum.ToObject(typeof(T), 0); }
                    }
                    set { base.Value = Convert.ToInt32(value) == 0 ? "" : Enum.ToObject(typeof(Q), Convert.ToInt32(value)).ToString().Substring(0, 1) == "_" ? Enum.ToObject(typeof(Q), Convert.ToInt32(value)).ToString().Substring(0, 1).Substring(1) : Enum.ToObject(typeof(Q), Convert.ToInt32(value)).ToString(); }
                }

                public NANEnum(int DataLength) : base(DataLength) { }
            }
        }

        namespace Enums
        {

        }
    }
}
