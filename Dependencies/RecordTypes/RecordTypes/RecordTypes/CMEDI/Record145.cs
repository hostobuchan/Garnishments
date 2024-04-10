namespace RecordTypes.CMEDI
{
    /// <summary>
    /// Bank Information (Infinity Override)
    /// </summary>
    public class Record145 : Base.CMEDISenderRecord
    {
        /// <summary>
        /// Debtor # for Bank (1-999)
        /// </summary>
        [MergePops.Attributes.MergePopField("NUMBER", 8, true)]
        public string NUMBER { get; set; }
        /// <summary>
        /// Bank Account # (Internal Backbone)
        /// </summary>
        [MergePops.Attributes.MergePopField("ACCT_NO", 9)]
        public string ACCT_NO { get; set; }
        /// <summary>
        /// Bank Name
        /// <para>INF.*XXXBnNm</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("BANK", 10)]
        public string BANK { get; set; }
        /// <summary>
        /// Bank Street Address
        /// <para>INF.*XXXBnAd</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("ADDR", 11)]
        public string ADDR { get; set; }
        /// <summary>
        /// Bank City, State Zip
        /// <para>INF.*XXXBnCS</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("CSZ", 12)]
        public string CSZ { get; set; }
        /// <summary>
        /// Bank Phone #
        /// <para>INF.*XXXBnP#</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("PHONE", 13)]
        public string PHONE { get; set; }
        /// <summary>
        /// Bank Phone Extension
        /// <para>INF.*XXXBnEx</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("PHONE_EXT", 14)]
        public string PHONE_EXT { get; set; }
        /// <summary>
        /// Bank Fax #
        /// <para>INF.*XXXBnF#</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("FAX", 15)]
        public string FAX { get; set; }
        /// <summary>
        /// Bank Attention Line
        /// <para>INF.*XXXBnAt</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("ATTN", 16)]
        public string ATTN { get; set; }
        /// <summary>
        /// Unique Key for Bank
        /// <para>INF.*XXXBnK#</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("BANK_KEY", 17)]
        public string BANK_KEY { get; set; }
        /// <summary>
        /// Bank E-Mail Address
        /// <para>INF.*XXXBnEm</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("EMAIL", 18)]
        public string EMAIL { get; set; }
        /// <summary>
        /// Bank Home Page
        /// <para>INF.*XXXBnHp</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("HOME_PAGE", 19)]
        public string HOME_PAGE { get; set; }
        /// <summary>
        /// Bank ABA #
        /// <para>INF.*XXXBnAB</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("ABA_NO", 20)]
        public string ABA_NO { get; set; }
        /// <summary>
        /// Bank Country
        /// <para>INF.* XXXBnCy</para>
        /// </summary>
        [MergePops.Attributes.MergePopField("CNTRY", 21)]
        public string CNTRY { get; set; }

        public Record145() : base("145") { }
    }
}
