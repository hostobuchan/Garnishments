using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    public enum LastStatus : byte
    {
        Unknown = 0,
        Delivered = 1,
        ReturnedToSender = 2,
        Mailed = 3
    }
}
