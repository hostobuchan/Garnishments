using RecordTypes.EDI.EDIDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedDictionary<T> : EncapsulatedDataType where T : struct
    {
        private Dictionary<string, T> _Lookup { get; set; }
        public new T Value { get { try { return _Lookup[base.Value]; } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = _Lookup.Where(el => el.Value.Equals(value)).First().Key; } }

        public DelimitedDictionary(StringHolder DataString, Dictionary<string, T> Lookup) : base(DataString)
        {
            this._Lookup = Lookup;
        }
    }
}
