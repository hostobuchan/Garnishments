namespace RecordTypes.MergePops
{
    public class MergePop
    {
        /// <summary>
        /// Claim File Number
        /// <para>F8</para>
        /// </summary>
        [Attributes.MergePopRequiredGroupField("FILENO", 0, 1)]
        public string FILENO { get { return _FILENO; } set { _FILENO = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _FILENO;
        /// <summary>
        /// Court Docket Number
        /// <para>F202</para>
        /// </summary>
        [Attributes.MergePopRequiredGroupField("DOCKET_NO", 1, 1)]
        public string DOCKET_NO { get { return _DOCKET_NO; } set { _DOCKET_NO = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _DOCKET_NO;
        /// <summary>
        /// Forwarder File Number
        /// <para>F6</para>
        /// </summary>
        [Attributes.MergePopRequiredGroupField("FORW_FILENO", 2, 1)]
        public string FORW_FILENO { get { return _FORW_FILENO; } set { _FORW_FILENO = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _FORW_FILENO;
        /// <summary>
        /// Co-Counsel File Number
        /// <para>F23 or F321</para>
        /// </summary>
        [Attributes.MergePopRequiredGroupField("COCO_FILENO", 3, 1)]
        public string COCO_FILENO { get { return _COCO_FILENO; } set { _COCO_FILENO = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _COCO_FILENO;
        /// <summary>
        /// Document Comment
        /// <para>F445 or F430 or F49 (1st half) and F50 (2nd half)</para>
        /// </summary>
        [Attributes.MergePopField("CMT", 4)]
        public string CMT { get { return _CMT; } set { _CMT = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _CMT;
        /// <summary>
        /// Document Name
        /// <para>F1</para>
        /// </summary>
        [Attributes.MergePopField("LLCODE", 5, true)]
        public string LLCODE { get { return _LLCODE; } set { _LLCODE = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _LLCODE;
        /// <summary>
        /// PS Comment - Line 1
        /// <para>F60</para>
        /// </summary>
        [Attributes.MergePopField("PS01", 6)]
        public string PS01 { get { return _PS01; } set { _PS01 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS01;
        /// <summary>
        /// PS Comment - Line 2
        /// <para>F61</para>
        /// </summary>
        [Attributes.MergePopField("PS02", 7)]
        public string PS02 { get { return _PS02; } set { _PS02 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS02;
        /// <summary>
        /// PS Comment - Line 3
        /// <para>F62</para>
        /// </summary>
        [Attributes.MergePopField("PS03", 8)]
        public string PS03 { get { return _PS03; } set { _PS03 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS03;
        /// <summary>
        /// PS Comment - Line 4
        /// <para>F63</para>
        /// </summary>
        [Attributes.MergePopField("PS04", 9)]
        public string PS04 { get { return _PS04; } set { _PS04 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS04;
        /// <summary>
        /// PS Comment - Line 5
        /// <para>F64</para>
        /// </summary>
        [Attributes.MergePopField("PS05", 10)]
        public string PS05 { get { return _PS05; } set { _PS05 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS05;
        /// <summary>
        /// PS Comment - Line 6
        /// <para>F65</para>
        /// </summary>
        [Attributes.MergePopField("PS06", 11)]
        public string PS06 { get { return _PS06; } set { _PS06 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS06;
        /// <summary>
        /// PS Comment - Line 7
        /// <para>F66</para>
        /// </summary>
        [Attributes.MergePopField("PS07", 12)]
        public string PS07 { get { return _PS07; } set { _PS07 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS07;
        /// <summary>
        /// PS Comment - Line 8
        /// <para>F67</para>
        /// </summary>
        [Attributes.MergePopField("PS08", 13)]
        public string PS08 { get { return _PS08; } set { _PS08 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS08;
        /// <summary>
        /// PS Comment - Line 9
        /// <para>F68</para>
        /// </summary>
        [Attributes.MergePopField("PS09", 14)]
        public string PS09 { get { return _PS09; } set { _PS09 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS09;
        /// <summary>
        /// PS Comment - Line 10
        /// <para>F69</para>
        /// </summary>
        [Attributes.MergePopField("PS10", 15)]
        public string PS10 { get { return _PS10; } set { _PS10 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS10;
        /// <summary>
        /// PS Comment - Line 11
        /// <para>F70</para>
        /// </summary>
        [Attributes.MergePopField("PS11", 16)]
        public string PS11 { get { return _PS11; } set { _PS11 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS11;
        /// <summary>
        /// PS Comment - Line 12
        /// <para>F71</para>
        /// </summary>
        [Attributes.MergePopField("PS12", 17)]
        public string PS12 { get { return _PS12; } set { _PS12 = value?.Replace("\n", "").Replace("\r", "").Replace("\t", "") ?? string.Empty; } }
        string _PS12;
        /// <summary>
        /// Extra Amount - 1
        /// <para>F150</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA1", 18)]
        public decimal? Extra1 { get; set; }
        /// <summary>
        /// Extra Amount - 2
        /// <para>F151</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA2", 19)]
        public decimal? Extra2 { get; set; }
        /// <summary>
        /// Extra Amount - 3
        /// <para>F152</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA3", 20)]
        public decimal? Extra3 { get; set; }
        /// <summary>
        /// Extra Amount - 4
        /// <para>F153</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA4", 21)]
        public decimal? Extra4 { get; set; }
        /// <summary>
        /// Extra Amount - 5
        /// <para>F154</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA5", 22)]
        public decimal? Extra5 { get; set; }
        /// <summary>
        /// Extra Amount - 6
        /// <para>F155</para>
        /// </summary>
        [Attributes.MergePopField("EXTRA6", 23)]
        public decimal? Extra6 { get; set; }

        internal string ToString(FileWriter writer)
        {
            return writer.WriteMergePop(this);
        }
    }
}
