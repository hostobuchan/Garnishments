namespace RecordTypes.CMEDI.Base
{
    public class CMEDISenderReceiverRecord : CMEDIAccountRecord
    {
        /// <summary>
        /// Receiver ID Code
        /// </summary>
        [MergePops.Attributes.MergePopField("RECEIVER_ID", 8)]
        public string RECEIVER_ID { get; set; }

        public CMEDISenderReceiverRecord(string record) : base(record) { }
    }
}
