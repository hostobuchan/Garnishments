using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Enums
{

    public enum AssetResultReason
    {
        Unknown = 0,
        NewInfo,
        AlreadyExists,
        ExactMatchOnFile,
        PreviousGarnInPlace,
        PreviousInfoKnownGood,
        NoPreviousGarn,
        Error
    }
}
