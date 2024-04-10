using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Enums
{
    [Flags]
    public enum ImportSettings
    {
        Ignore = 0,
        ManuallyVerify = 1,
        Import = 2,
        AddAsset = 4,
        Garn = 8,

        ImportAndGarn = Import | AddAsset | Garn,
        ImportNoGarn = Import | AddAsset
    }
}
