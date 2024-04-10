namespace RecordTypes.Utilities
{
    public static class YGCConverter
    {
        public static RecordTypes2.YGC.RecordType01 CreateRecordType01(this RecordTypes.YGC.RecordType51 record)
        {
            return new RecordTypes2.YGC.RecordType01("0" + record.ToString().Substring(1));
        }
    }
}
