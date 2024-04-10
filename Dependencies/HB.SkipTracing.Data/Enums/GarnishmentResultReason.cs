using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Enums
{
    public enum GarnishmentResultReason
    {
        Unknown = 0,
        PreviousGarnInPlace,
        PreviousGarnSuccessful,
        NoPreviousGarn,
        AccountIneligible,
        
        KnownBadInfo,
        UserOverride,
        Requested,
        Error
    }
}
