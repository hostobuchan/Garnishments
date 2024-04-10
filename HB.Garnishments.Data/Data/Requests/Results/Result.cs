using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results
{
    [DataContract(Name = "Result", Namespace = "")]
    public class Result : IFormattable, IFormatProvider
    {
        [DataMember(Name = "UID")]
        public short ID { get; private set; }
        [DataMember(Name = "ATID")]
        public Enums.AssetType Type { get; private set; }
        [DataMember(Name = "STATUS")]
        public Enums.Status Status { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; private set; }
        [DataMember(Name = "GOOD")]
        public bool Good { get; private set; }
        [DataMember(Name = "UPDATE")]
        public bool Update { get; private set; }
        [DataMember(Name = "MONEY")]
        public bool MoneyExpected { get; private set; }
        [DataMember(Name = "MergeCodes")]
        public List<Codes.SalesMergeCodes> MergeCodes { get; private set; } = new List<Codes.SalesMergeCodes>();

        [IgnoreDataMember]
        public Codes.SalesMergeCodes this[int salesNo]
        {
            get
            {
                return this.MergeCodes.FirstOrDefault(m => m.SalesNo == salesNo);
            }
        }

        #region IFormattable
        public override string ToString()
        {
            return ToString("");
        }
        public string ToString(string format)
        {
            return ToString("", this);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "N":
                    return this.Description;
                default:
                    return $"[{ID.ToString().PadLeft(3, '0')}] {Status} - {Description}";
            }
        }
        public object GetFormat(Type formatType)
        {
            return this;
        }
        #endregion

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (this.MergeCodes == null) this.MergeCodes = new List<Codes.SalesMergeCodes>();
        }
    }
}
