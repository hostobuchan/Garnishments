using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RecordTypes.Trak
{
    namespace Base
    {
        public class TrakList<T> : IEnumerable where T : RecordTypeBase
        {
            private List<RecordTypeBase> _Records = new List<RecordTypeBase>();
            public List<RecordTypeBase> TrakRecords { get { return this._Records; } }
            public T this[int index] { get { return (T)this._Records[index]; } }
            public void Add(T Record) { this._Records.Add(Record); }
            public void Remove(T Record) { this._Records.Remove(Record); }
            public int Count { get { return this._Records.Count; } }

            public IEnumerator GetEnumerator() { return new RecordTypeBase.TrakEnumerator(this._Records); }
        }

        /// <summary>
        /// Base Record For Trak Record Type
        /// </summary>
        public abstract class RecordTypeBase
        {
            protected string[] RecordSegments { get; private set; }

            public RecordTypeBase(string Record)
            {
                this.RecordSegments = Record.Split('|');
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (string Seg in this.RecordSegments)
                {
                    sb.Append(Seg + "|");
                }
                return sb.ToString().Substring(0, sb.Length - 1);
            }

            public class TrakEnumerator : IEnumerator
            {
                public List<RecordTypeBase> RecordList;
                private int position = -1;

                public TrakEnumerator(List<RecordTypeBase> RecordList)
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
        public abstract class DataType
        {
            private string[] RecordSegments { get; set; }
            private int SegmentIndex { get; set; }
            protected virtual string Value { get { return this.RecordSegments[this.SegmentIndex]; } set { this.RecordSegments[this.SegmentIndex] = value; } }

            public DataType(string[] RecordSegments, int SegmentIndex)
            {
                this.RecordSegments = RecordSegments;
                this.SegmentIndex = SegmentIndex;
            }
        }

        public class TrakString : DataType
        {
            public new string Value { get { return base.Value; } set { base.Value = value; } }

            public TrakString(string[] RecordSegments, int SegmentIndex) : base(RecordSegments, SegmentIndex) { }
        }

        public class TrakMaskedNumber : DataType
        {
            public new string Value { get { return base.Value.NumbersOnly(); } set { base.Value = "'" + value; } }

            public TrakMaskedNumber(string[] RecordSegments, int SegmentIndex) : base(RecordSegments, SegmentIndex) { }
        }

        public class TrakDate : DataType
        {
            public new DateTime? Value { get { try { return Convert.ToDateTime(base.Value); } catch { return null; } } set { base.Value = !value.HasValue ? "" : value.Value.ToString("MM/dd/yyyy"); } }

            public TrakDate(string[] RecordSegments, int SegmentIndex) : base(RecordSegments, SegmentIndex) { }
        }

        public class TrakDecimal : DataType
        {
            public new decimal? Value { get { try { return Convert.ToDecimal(base.Value); } catch { return null; } } set { base.Value = !value.HasValue ? "" : value.Value.ToString("0.00"); } }

            public TrakDecimal(string[] RecordSegments, int SegmentIndex) : base(RecordSegments, SegmentIndex) { }
        }
    }
}
