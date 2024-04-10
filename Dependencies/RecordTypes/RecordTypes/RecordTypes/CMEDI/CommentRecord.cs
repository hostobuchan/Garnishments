namespace RecordTypes.CMEDI
{
    /// <summary>
    /// Comment Record
    /// </summary>
    public class CommentRecord : Base.CMEDIDateTimeRecord
    {
        /// <summary>
        /// 
        /// </summary>
        [MergePops.Attributes.MergePopField("Description", 4, true)]
        public string Description { get; set; }
        public override bool NeedsHeader { get { return false; } }

        public CommentRecord() : base("0") { }
    }
}
