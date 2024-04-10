using RecordTypes.EDI.EDIDataTypes;
using System.Linq;

namespace RecordTypes.Delimited.Base
{
    public abstract class TabDelimitedBaseRecord : Delimited.Base.DelimitedBaseRecord
    {
        public TabDelimitedBaseRecord(int Fields) : base('\t')
        {
            this.LineItems = new StringHolder[Fields];
            for (int i = 0; i < Fields; i++)
                this.LineItems[i] = "";
        }
        public TabDelimitedBaseRecord(string Record) : base('\t')
        {
            try { this.LineItems = Record.Split('\t').Cast<StringHolder>().ToArray(); }
            catch
            {
                string[] R = Record.Split('\t');
                this.LineItems = new StringHolder[R.Length];
                for (int i = 0; i < R.Length; i++)
                    this.LineItems[i] = R[i];
            }
        }
    }
}
