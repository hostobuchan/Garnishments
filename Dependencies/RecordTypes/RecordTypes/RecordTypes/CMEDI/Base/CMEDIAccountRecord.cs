namespace RecordTypes.CMEDI.Base
{
    public class CMEDIAccountRecord : CMEDIDateTimeRecord
    {
        /// <summary>
        /// Forwarder or Senders internal File #
        /// </summary>
        [MergePops.Attributes.MergePopRequiredGroupField("FORW_REFNO", 4, 1)]
        public string FORW_REFNO { get; set; }
        /// <summary>
        /// Forwarders file #( Credit Card #)
        /// </summary>
        [MergePops.Attributes.MergePopRequiredGroupField("FORW_FILENO", 5, 1)]
        public string FORW_FILENO { get; set; }
        /// <summary>
        /// Receivers File #
        /// </summary>
        [MergePops.Attributes.MergePopRequiredGroupField("FIRM_FILENO", 6, 1)]
        public string FIRM_FILENO { get; set; }

        public CMEDIAccountRecord(string record) : base(record) { }
    }
}
