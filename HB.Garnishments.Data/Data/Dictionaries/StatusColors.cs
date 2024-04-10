using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data
{
    public static partial class Dictionaries
    {
        private static Dictionary<Enums.Status, int[]> _StatusDaysColors;
        public static Dictionary<Enums.Status, int[]> StatusDaysColor { get { if (_StatusDaysColors == null) CreateStatusDaysColorDictionary(); return _StatusDaysColors; } }

        private static void CreateStatusDaysColorDictionary()
        {
            _StatusDaysColors = new Dictionary<Enums.Status, int[]>()
            {
                // int[] {  Green (OK),  Orange (Getting Late),  Red (Overdue) }
                { Enums.Status.Requested, new [] { 7, 30, 60 } },
                { Enums.Status.Canceled, new [] { 0, 0, 0 } },
                { Enums.Status.Rejected, new [] { 0, 0, 0 } },
                { Enums.Status.Processed, new [] { 7, 30, 60 } },
                { Enums.Status.Signed, new [] { 7, 30, 60 } },
                { Enums.Status.Filed, new [] { 7, 30, 60 } },
                { Enums.Status.CourtResponse, new [] { 7, 30, 60 } },
                { Enums.Status.GarnisheeResponse, new [] { 7, 30, 60 } },
                { Enums.Status.HearingRequested, new []{ 7, 30, 60} }
            };
        }
    }
}
