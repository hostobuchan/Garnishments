using System;

namespace RecordTypes.MergePops.Attributes
{
    public class MergePopFieldAttribute : Attribute
    {
        public virtual bool Required { get; private set; }
        public string Header { get; private set; }
        public int Order { get; private set; }

        public MergePopFieldAttribute(string HeaderText, int Order, bool Required = false)
        {
            this.Required = Required;
            this.Header = HeaderText;
            this.Order = Order;
        }
    }
}
