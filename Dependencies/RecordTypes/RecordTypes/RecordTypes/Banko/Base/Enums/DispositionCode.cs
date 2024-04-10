namespace RecordTypes.Banko.Base.Enums
{
    /// <summary>
    /// Bankruptcy Disposition Code
    /// </summary>
    public enum DispositionCode
    {
        Unknown = 0,
        Filed = 2,
        Dismissed = 15,
        Discharged = 20,
        Conversion = 30,
        Reinstated = 88,
        /// <summary>
        /// Closed, Filed in Error (case should not have been filed, the court is retracting the filing)
        /// </summary>
        Closed_Retracted = 89,
        /// <summary>
        /// Closed, Transferred Out (Case has been transferred OUT of this court)
        /// </summary>
        Closed_Transfered = 90,
        /// <summary>
        /// Closed, Discharge N/A (Case has been closed, but not dismissed or discharged, just closed without a final disposition)
        /// </summary>
        Closed_NoResult = 91,
        /// <summary>
        /// Unknown if Dismissed or Discharged
        /// </summary>
        Closed = 99
    }
}
