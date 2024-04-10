using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Enums
{
    [Flags]
    public enum AssetResultType
    {
        NotAdded = 0,
        Added = 1,
        Garnished = 2,
        Error = 4,

        AddedAndGarned = 3,
        NoAction = 0
    }
}
