using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data
{
    public static partial class Dictionaries
    {
        private static Dictionary<Enums.Status, string> _GarnishmentStatusDescription;
        private static Dictionary<Enums.Status, string> _GarnishmentStatusAwaitingDescription;
        public static Dictionary<Enums.Status, string> StatusDescription { get { if (_GarnishmentStatusDescription == null) CreateGarnishmentStatusDescription(); return _GarnishmentStatusDescription; } }
        public static Dictionary<Enums.Status, string> StatusAwaitingDescription { get { if (_GarnishmentStatusAwaitingDescription == null) CreateGarnishmentStatusAwaitingDescription(); return _GarnishmentStatusAwaitingDescription; } }

        private static void CreateGarnishmentStatusDescription()
        {
            _GarnishmentStatusDescription = new Dictionary<Enums.Status, string>()
            {
                { Enums.Status.Requested, "Requested" },
                { Enums.Status.Processed, "Processed" },
                { Enums.Status.Signed, "Signed" },
                { Enums.Status.Filed, "Filed" },
                { Enums.Status.CourtResponse, "Received Court Answer" },
                { Enums.Status.GarnisheeResponse, "Received Garnishee Answer" },
                { Enums.Status.Canceled, "Canceled" },
                { Enums.Status.Rejected, "Rejected" },
                { Enums.Status.HearingRequested, "Hearing Requested" }
            };
        }
        private static void CreateGarnishmentStatusAwaitingDescription()
        {
            _GarnishmentStatusAwaitingDescription = new Dictionary<Enums.Status, string>()
            {
                { Enums.Status.Requested, "Awaiting Processing" },
                { Enums.Status.Processed, "Awaiting Attorney Review" },
                { Enums.Status.Signed, "Awaiting Filing" },
                { Enums.Status.Filed, "Awaiting Court Answer" },
                { Enums.Status.CourtResponse, "Awaiting Garnishee Answer" },
                { Enums.Status.GarnisheeResponse, "Complete" },
                { Enums.Status.Canceled, "Canceled" },
                { Enums.Status.Rejected, "Rejected" },
                { Enums.Status.HearingRequested, "Awaiting Hearing Result" }
            };
        }
    }
}
