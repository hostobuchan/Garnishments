using System.Collections;
using System.Collections.Generic;

namespace RecordTypes.RMS.Base
{
    /// <summary>
    /// Strongly Typed Table of RMS Records
    /// </summary>
    /// <typeparam name="T">Type of RMS Record</typeparam>
    public class RMSList<T> : IEnumerable where T : Record
    {
        private List<Record> _Records = new List<Record>();
        public List<Record> RMSRecords { get { return this._Records; } }
        public T this[int index] { get { return (T)this._Records[index]; } }
        public void Add(T Record) { this._Records.Add(Record); }
        public void Remove(T Record) { this._Records.Remove(Record); }
        public int Count { get { return this._Records.Count; } }

        public IEnumerator GetEnumerator() { return new Record.RMSEnumerator(this._Records); }
    }
}
