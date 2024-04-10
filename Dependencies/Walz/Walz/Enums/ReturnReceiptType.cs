using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    public enum ReturnReceiptType : byte
    {
        Unknown = 0,
        None = 1,
        ReturnReceiptHardCopy = 2,
        ReturnReceiptElectronic = 3,
    }
}
