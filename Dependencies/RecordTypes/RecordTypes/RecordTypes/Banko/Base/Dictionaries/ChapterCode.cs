using System.Collections.Generic;

namespace RecordTypes.Banko.Base
{
    public static partial class Dictionaries
    {
        private static Dictionary<Enums.ChapterCode, string> _ChapterCodeNumber;
        public static Dictionary<Enums.ChapterCode, string> ChapterCodeNumber { get { if (_ChapterCodeNumber == null) CreateChapterCodeNumberDictionary(); return _ChapterCodeNumber; } }

        private static void CreateChapterCodeNumberDictionary()
        {
            _ChapterCodeNumber = new Dictionary<Enums.ChapterCode, string>()
            {
                { Enums.ChapterCode.Unknown, "" },
                { Enums.ChapterCode.Chapter7, "7" },
                { Enums.ChapterCode.Chapter9, "9" },
                { Enums.ChapterCode.Chapter11, "11" },
                { Enums.ChapterCode.Chapter12, "12" },
                { Enums.ChapterCode.Chapter13, "13" },
                { Enums.ChapterCode.Section304, "304" }
            };
        }
    }
}
