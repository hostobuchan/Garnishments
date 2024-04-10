using System.Reflection;

namespace RecordTypes.MergePops
{
    struct MPField
    {
        public PropertyInfo PropertyInfo { get; set; }
        public Attributes.MergePopFieldAttribute Attributes { get; set; }
    }
}
