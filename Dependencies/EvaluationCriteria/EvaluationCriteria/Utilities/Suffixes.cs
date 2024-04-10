namespace EvaluationCriteria
{
    public static partial class Utilities
    {
        private static string[] _Suffixes;
        public static string[] Suffixes { get { if (_Suffixes == null) CreateSuffixesList(); return _Suffixes; } }

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
