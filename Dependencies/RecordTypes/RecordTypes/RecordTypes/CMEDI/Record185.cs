namespace RecordTypes.CMEDI
{
    /// <summary>
    /// Claim Tags Information
    /// </summary>
    class Record185 : Base.CMEDISenderRecord
    {
        /// <summary>
        /// Tag Code
        /// </summary>
        [MergePops.Attributes.MergePopField("CODE", 8, true)]
        public string CODE { get; set; }

        public Record185() : base("185") { }
    }
}
