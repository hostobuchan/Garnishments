namespace RecordTypes.CMEDI.Base
{
    public class CMEDIDateTimeRecord : CMEDIBase
    {
        /// <summary>
        /// Date
        /// </summary>
        [MergePops.Attributes.MergePopField("Date", 2)]
        public string Date { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [MergePops.Attributes.MergePopField("Time", 3)]
        public string Time { get; set; }

        public CMEDIDateTimeRecord(string record) : base(record) { }
    }
}
