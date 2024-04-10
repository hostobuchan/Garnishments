using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Enums
{
    [Flags]
    public enum AssetType
    {
        Unknown = 0,
        Bank = 1,
        Employer = 2,
        SCRA = 4,

        Both = Bank | Employer
    }
}
