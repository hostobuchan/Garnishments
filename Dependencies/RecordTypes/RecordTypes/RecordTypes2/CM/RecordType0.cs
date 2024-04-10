using RecordTypes2.CM.Attributes;
using RecordTypes2.CM.DataTypes;

namespace RecordTypes2.CM
{
    public class RecordType0 : Base.CMEDIRecordBase
    {
        [CMEDIField("000", 0, true)]
        protected override CMEDINumber _Record { get; set; }
        [CMEDIField("Description", 4)]
        protected CMEDIString _Description { get; private set; }

        public string Description { get { return _Description.Value; } set { _Description.Value = value; } }

        public RecordType0() : base(0, 5)
        {
            _Description = new CMEDIString(LineItems[4]);
        }
        public RecordType0(string record) : base(0, record)
        {
            _Description = new CMEDIString(LineItems[4]);
        }
    }
}
