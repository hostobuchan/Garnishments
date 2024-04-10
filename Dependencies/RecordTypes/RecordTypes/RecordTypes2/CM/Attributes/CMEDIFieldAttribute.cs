using System;

namespace RecordTypes2.CM.Attributes
{
    public class CMEDIFieldAttribute : Attribute
    {
        public bool Required { get; private set; }
        public string Header { get; private set; }
        public int Order { get; private set; }

        public CMEDIFieldAttribute(string Header, int Order, bool Required = false)
        {
            this.Header = Header;
            this.Order = Order;
            this.Required = Required;
        }

        public void UpdateHeader(string header)
        {
            this.Header = header;
        }
    }
}
