using System.Collections.Generic;

namespace RecordTypes2.CM
{
    public static partial class Dictionaries
    {
        private static Dictionary<string, Enums.Disposition> _Disposition;
        public static Dictionary<string, Enums.Disposition> Disposition { get { if (_Disposition == null) CreateDispositionDictionary(); return _Disposition; } }

        private static void CreateDispositionDictionary()
        {
            _Disposition = new Dictionary<string, Enums.Disposition>()
            {
                { "", Enums.Disposition.Unknown },
                { "B", Enums.Disposition.PreSuit },
                { "P", Enums.Disposition.PostSuit },
                { "J", Enums.Disposition.Judgment }
            };
        }
    }
}
