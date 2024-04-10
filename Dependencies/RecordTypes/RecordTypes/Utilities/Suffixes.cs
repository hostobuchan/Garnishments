namespace RecordTypes.Utilities
{
    public static class Suffixes
    {
        private static string[] _Suffixes;
        public static string[] Elements { get { if (_Suffixes == null) CreateSuffixesList(); return _Suffixes; } }

        private static void CreateSuffixesList()
        {
            _Suffixes = new string[]
            {
                "Sr",
                "Jr",
                "II",
                "III",
                "IV"
            };
        }
    }
}
