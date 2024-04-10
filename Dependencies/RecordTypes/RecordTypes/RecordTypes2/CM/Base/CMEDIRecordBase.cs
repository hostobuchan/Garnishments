using RecordTypes2.CM.Attributes;
using RecordTypes2.CM.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RecordTypes2.CM.Base
{
    public abstract class CMEDIRecordBase : RecordTypes.Delimited.Base.TabDelimitedBaseRecord
    {
        #region Protected Properties
        [CMEDIField("", 0, true)]
        protected abstract CMEDINumber _Record { get; set; }
        [CMEDIField("H", 1, true)]
        protected CMEDIString _RecordType { get; private set; }
        [CMEDIField("Date", 2, true)]
        protected CMEDIDate _Date { get; private set; }
        [CMEDIField("Time", 3, true)]
        protected CMEDITime _Time { get; private set; }
        #endregion

        #region Public Properties
        public int? Record { get { return _Record.Value; } }
        public string RecordType { get { return _RecordType.Value; } }
        public DateTime? Date { get { return _Date.Value; } set { _Date.Value = value; } }
        public DateTime? Time { get { return _Time.Value; } set { _Time.Value = value; } }
        #endregion

        public CMEDIRecordBase(int recordType, string data) : base(data.Substring(0, data.LastIndexOf("\t#")))
        {
            Initialize(recordType);
        }
        public CMEDIRecordBase(int recordType, int numFields) : base(numFields)
        {
            Initialize(recordType);
        }

        private void Initialize(int recordType)
        {
            _Record = new CMEDINumber(this.LineItems[0], 3, true, '0') { Value = recordType };
            _RecordType = new CMEDIString(this.LineItems[1]) { Value = "D" };
            _Date = new CMEDIDate(this.LineItems[2]);
            _Time = new CMEDITime(this.LineItems[3]);
        }

        public string HeaderLine()
        {
            StringBuilder sb = new StringBuilder();
            var props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            var usedProps = props.Where(el => typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).IsAssignableFrom(el.PropertyType));
            var ediProps = usedProps.Select(el => new CMEDIField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.CMEDIFieldAttribute), true).FirstOrDefault() as Attributes.CMEDIFieldAttribute });
            var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
            foreach (var field in orderedProps)
            {
                sb.Append($"{field.Attributes.Header}\t");
            }
            sb.Append("#");
            return sb.ToString();
        }
        public string HeaderLine(IEnumerable<CMEDIRecordBase> records)
        {
            Type myType = this.GetType();
            CMEDIRecordBase[] likeRecords = records.Where(el => el.GetType() == myType).ToArray();
            StringBuilder sb = new StringBuilder();
            var props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            var usedProps = props.Where(el => typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).IsAssignableFrom(el.PropertyType));
            var ediProps = usedProps.Select(el => new CMEDIField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.CMEDIFieldAttribute), true).FirstOrDefault() as Attributes.CMEDIFieldAttribute });
            var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
            foreach (var field in orderedProps)
            {
                if (field.Attributes.Required || likeRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                    sb.Append($"{field.Attributes.Header}\t");
            }
            sb.Append("#");
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            var usedProps = props.Where(el => typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).IsAssignableFrom(el.PropertyType));
            var ediProps = usedProps.Select(el => new CMEDIField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.CMEDIFieldAttribute), true).FirstOrDefault() as Attributes.CMEDIFieldAttribute });
            var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
            foreach (var field in orderedProps)
            {
                var prop = typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).GetProperty("DataString", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                sb.Append($"{prop.GetValue(field.PropertyInfo.GetValue(this, null), null)}\t");
            }
            sb.Append("#");
            return sb.ToString();
        }
        public string ToString(IEnumerable<CMEDIRecordBase> records)
        {
            Type myType = this.GetType();
            CMEDIRecordBase[] likeRecords = records.Where(el => el.GetType() == myType).ToArray();
            StringBuilder sb = new StringBuilder();
            var props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            var usedProps = props.Where(el => typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).IsAssignableFrom(el.PropertyType));
            var ediProps = usedProps.Select(el => new CMEDIField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.CMEDIFieldAttribute), true).FirstOrDefault() as Attributes.CMEDIFieldAttribute });
            var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
            foreach (var field in orderedProps)
            {
                if (field.Attributes.Required || likeRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                    sb.Append($"{typeof(RecordTypes.EDI.EDIDataTypes.EncapsulatedDataType).GetProperty("DataString", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).GetValue(field.PropertyInfo.GetValue(this, null), null)}\t");
            }
            sb.Append("#");
            return sb.ToString();
        }
    }

    struct CMEDIField
    {
        public PropertyInfo PropertyInfo;
        public CMEDIFieldAttribute Attributes;
    }
}
