namespace RecordTypes.Banko.Base.Enums
{
    /// <summary>
    /// Selected Options for Processing
    /// </summary>
    public enum ProductCode
    {
        Unknown = 0,
        Bankruptcy = 1,
        Deceased = 2,
        BankruptcyDeceased = 4,
        Chapter13Only = 5,
        DeleteRecord = 99
    }
}
