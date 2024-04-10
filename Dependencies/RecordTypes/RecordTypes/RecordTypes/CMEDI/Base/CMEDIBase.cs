namespace RecordTypes.CMEDI.Base
{
    public class CMEDIBase
    {
        /// <summary>
        /// Record Number
        /// </summary>
        [MergePops.Attributes.MergePopField(null, 0, true)]
        public string Record { get; private set; }
        /// <summary>
        /// Denotes Header (H) or Data (D)
        /// </summary>
        [MergePops.Attributes.MergePopField("H", 1, true)]
        public string Data { get { return "D"; } }
        public virtual bool NeedsHeader { get { return true; } }

        protected CMEDIBase(string record)
        {
            this.Record = record;
        }
    }
}
