namespace RecordTypes.CMEDI.Base
{
    public class CMEDISenderRecord : CMEDIAccountRecord
    {
        /// <summary>
        /// Sender ID Code
        /// </summary>
        [MergePops.Attributes.MergePopField("SENDER_ID", 7)]
        public string SENDER_ID { get; set; }

        public CMEDISenderRecord(string record) : base(record) { }
    }
}
