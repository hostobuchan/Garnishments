namespace System
{
    public static partial class StaticLists
    {
        private static StateName[] _StateNames;
        public static StateName[] StateNames { get { if (_StateNames == null) LoadStateNames(); return _StateNames; } }

        private static void LoadStateNames()
        {
            _StateNames = new StateName[]
            {
                new StateName() { Name ="Alabama", Abbreviation ="AL" },
                new StateName() { Name ="Alaska", Abbreviation ="AK" },
                new StateName() { Name ="Arizona", Abbreviation ="AZ" },
                new StateName() { Name ="Arkansas", Abbreviation ="AR" },
                new StateName() { Name ="California", Abbreviation ="CA" },
                new StateName() { Name ="Colorado", Abbreviation ="CO" },
                new StateName() { Name ="Connecticut", Abbreviation ="CT" },
                new StateName() { Name ="Delaware", Abbreviation ="DE" },
                new StateName() { Name ="Florida", Abbreviation ="FL" },
                new StateName() { Name ="Georgia", Abbreviation ="GA" },
                new StateName() { Name ="Hawaii", Abbreviation ="HI" },
                new StateName() { Name ="Idaho", Abbreviation ="ID" },
                new StateName() { Name ="Illinois", Abbreviation ="IL" },
                new StateName() { Name ="Indiana", Abbreviation ="IN" },
                new StateName() { Name ="Iowa", Abbreviation ="IA" },
                new StateName() { Name ="Kansas", Abbreviation ="KS" },
                new StateName() { Name ="Kentucky", Abbreviation ="KY" },
                new StateName() { Name ="Louisiana", Abbreviation ="LA" },
                new StateName() { Name ="Maine", Abbreviation ="ME" },
                new StateName() { Name ="Maryland", Abbreviation ="MD" },
                new StateName() { Name ="Massachusetts", Abbreviation ="MA" },
                new StateName() { Name ="Michigan", Abbreviation ="MI" },
                new StateName() { Name ="Minnesota", Abbreviation ="MN" },
                new StateName() { Name ="Mississippi", Abbreviation ="MS" },
                new StateName() { Name ="Missouri", Abbreviation ="MO" },
                new StateName() { Name ="Montana", Abbreviation ="MT" },
                new StateName() { Name ="Nebraska", Abbreviation ="NE" },
                new StateName() { Name ="Nevada", Abbreviation ="NV" },
                new StateName() { Name ="New Hampshire", Abbreviation ="NH" },
                new StateName() { Name ="New Jersey", Abbreviation ="NJ" },
                new StateName() { Name ="New Mexico", Abbreviation ="NM" },
                new StateName() { Name ="New York", Abbreviation ="NY" },
                new StateName() { Name ="North Carolina", Abbreviation ="NC" },
                new StateName() { Name ="North Dakota", Abbreviation ="ND" },
                new StateName() { Name ="Ohio", Abbreviation ="OH" },
                new StateName() { Name ="Oklahoma", Abbreviation ="OK" },
                new StateName() { Name ="Oregon", Abbreviation ="OR" },
                new StateName() { Name ="Pennsylvania", Abbreviation ="PA" },
                new StateName() { Name ="Rhode Island", Abbreviation ="RI" },
                new StateName() { Name ="South Carolina", Abbreviation ="SC" },
                new StateName() { Name ="South Dakota", Abbreviation ="SD" },
                new StateName() { Name ="Tennessee", Abbreviation ="TN" },
                new StateName() { Name ="Texas", Abbreviation ="TX" },
                new StateName() { Name ="Utah", Abbreviation ="UT" },
                new StateName() { Name ="Vermont", Abbreviation ="VT" },
                new StateName() { Name ="Virginia", Abbreviation ="VA" },
                new StateName() { Name ="Washington", Abbreviation ="WA" },
                new StateName() { Name ="West Virginia", Abbreviation ="WV" },
                new StateName() { Name ="Wisconsin", Abbreviation ="WI" },
                new StateName() { Name ="Wyoming", Abbreviation = "WY" }
            };
        }
    }

    public struct StateName
    {
        public string Name;
        public string Abbreviation;
    }
}
