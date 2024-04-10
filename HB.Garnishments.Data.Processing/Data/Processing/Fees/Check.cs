using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing.Fees
{
    public struct Check
    {
        public string FileNo { get; private set; }
        public decimal Amount { get; private set; }
        public byte CostCode { get; private set; }
        public string Comment { get; private set; }

        public Check(string fileNo, decimal amount, byte code, string comment)
        {
            this.FileNo = fileNo;
            this.Amount = amount;
            this.CostCode = code;
            this.Comment = comment;
        }
        public Check(string fileNo, decimal amount, Enums.CostCode code, string comment)
        {
            this.FileNo = fileNo;
            this.Amount = amount;
            this.CostCode = Codes.CostCodes[code];
            this.Comment = comment;
        }

        public override string ToString()
        {
            return $"{this.FileNo},{this.Amount:C2},{this.Comment},{this.CostCode}";
        }
    }
}
