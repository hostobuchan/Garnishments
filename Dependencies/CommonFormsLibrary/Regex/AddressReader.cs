namespace System.Windows.Forms.Regex
{
    public static class AddressReader
    {
        public static Address ReadCityStateZip(this string input)
        {
            var match = System.Text.RegularExpressions.Regex.Match(input, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
            if (match.Success)
            {
                return new Address()
                {
                    City = match.Groups["City"].Value,
                    State = match.Groups["State"].Value,
                    Zip = match.Groups["Zip"].Value
                };
            }
            else
            {
                return default(Address);
            }
        }
    }
}
