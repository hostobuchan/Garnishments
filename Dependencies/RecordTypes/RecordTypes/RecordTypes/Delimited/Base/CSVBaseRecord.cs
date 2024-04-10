using RecordTypes.EDI.EDIDataTypes;
using System.Linq;
using System.Text;

namespace RecordTypes.Delimited.Base
{
    public abstract class CSVBaseRecord : DelimitedBaseRecord
    {
        public CSVBaseRecord(int fields) : base(',')
        {
            LineItems = new StringHolder[fields];
        }
        public CSVBaseRecord(string dataString) : base(',')
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)"); // "(?<=\\\")(?<match>[^\\\"]*)(?=\\\")|[^\\\"](?<match>[^\\,]*)");
            var matches = regex.Matches(dataString);
            LineItems = matches.OfType<System.Text.RegularExpressions.Match>().Select(m => new StringHolder(m.Groups[2].Value)).ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.LineItems.Length; i++)
            {
                sb.AppendFormat("\"{0}\"{1}", this.LineItems[i], this.DelimitationCharacter);
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }
}
