using RecordTypes.EDI.EDIDataTypes;
using System.Text;

namespace RecordTypes.Delimited.Base
{
    public abstract class DelimitedBaseRecord
    {
        protected StringHolder[] LineItems { get; set; }
        protected char DelimitationCharacter { get; private set; }

        public DelimitedBaseRecord(char delimitationCharacter)
        {
            this.DelimitationCharacter = delimitationCharacter;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LineItems.Length; i++)
            {
                sb.AppendFormat("{0}{1}", this.LineItems[i], this.DelimitationCharacter);
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }
}
