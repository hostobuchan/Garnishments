namespace System.Windows.Forms.Regex
{
    public struct Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Address)
                return this == (Address)obj;
            else
                return false;
        }
        public static bool operator ==(Address a1, Address a2)
        {
            return a1.Street.Equals(a2.Street, StringComparison.OrdinalIgnoreCase)
                && a1.City.Equals(a2.City, StringComparison.OrdinalIgnoreCase)
                && a1.State.Equals(a2.State, StringComparison.OrdinalIgnoreCase)
                && a1.Zip.NumbersOnly().Equals(a2.Zip.NumbersOnly(), StringComparison.OrdinalIgnoreCase);
        }
        public static bool operator !=(Address a1, Address a2)
        {
            return !a1.Street.Equals(a2.Street, StringComparison.OrdinalIgnoreCase)
                || !a1.City.Equals(a2.City, StringComparison.OrdinalIgnoreCase)
                || !a1.State.Equals(a2.State, StringComparison.OrdinalIgnoreCase)
                || !a1.Zip.NumbersOnly().Equals(a2.Zip.NumbersOnly(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
