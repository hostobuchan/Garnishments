using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results.Codes
{
    [DataContract(Name = "Sales", Namespace = "")]
    public class SalesMergeCodes
    {
        [DataMember(Name = "SalesNo")]
        public byte SalesNo { get; private set; }
        [DataMember(Name = "Debtors")]
        public List<DebtorMergeCodes> DebtorCodes { get; private set; } = new List<DebtorMergeCodes>();

        [IgnoreDataMember]
        public DebtorMergeCodes this[int debtor]
        {
            get
            {
                return this.DebtorCodes.FirstOrDefault(d => d.Number == debtor);
            }
        }

        public SalesMergeCodes(byte salesNo)
        {
            this.SalesNo = salesNo;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (this.DebtorCodes == null) this.DebtorCodes = new List<DebtorMergeCodes>();
        }
    }
}
