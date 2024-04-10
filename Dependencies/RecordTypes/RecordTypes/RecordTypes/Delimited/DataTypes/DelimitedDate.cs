using RecordTypes.EDI.EDIDataTypes;
using System;

namespace RecordTypes.Delimited.DataTypes
{
    public class DelimitedDate : EncapsulatedDataType
    {
        private string DateFormat;
        public new DateTime? Value
        {
            get { try { return DateTime.ParseExact(base.Value, this.DateFormat, System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } }
            set { base.Value = value.HasValue ? value.Value.ToString(this.DateFormat) : ""; }
        }

        public DelimitedDate(StringHolder DataString, string DateFormat = "MM/dd/yyyy") : base(DataString)
        {
            this.DateFormat = DateFormat;
        }
    }

    public class DelimitedDateTime : EncapsulatedDataType
    {
        private string DateFormat;
        public new DateTime? Value
        {
            get { try { return DateTime.ParseExact(base.Value, this.DateFormat, System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } }
            set { base.Value = value.HasValue ? value.Value.ToString(this.DateFormat) : ""; }
        }

        public DelimitedDateTime(StringHolder DataString, string DateFormat = "MM/dd/yyyy HH:mm") : base(DataString)
        {
            this.DateFormat = DateFormat;
        }
    }
}
