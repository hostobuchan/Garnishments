using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Data is associated with a specific list which requires a Dictionary
    /// </summary>
    /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
    public class RMSDict<T> : RecordTypes.EDI.EDIDataTypes.DataType
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

        public RMSDict(int DataLength, Dictionary<string, T> Dictionary, bool AlignLeft = true) : base(DataLength, AlignLeft)
        {
            this.Dictionary = Dictionary;
        }
    }
}
