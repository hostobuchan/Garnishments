using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecordTypes.Delimited
{
    public static class FileReader
    {
        public static IEnumerable<Dictionary<string, object>> ReadCSVFile(string fileName)
        {
            return ReadCSVFileAsync(fileName).Result;
        }
        public static IEnumerable<Dictionary<string, object>> ReadTabFile(string fileName)
        {
            return ReadTabFileAsync(fileName).Result;
        }
        public static async Task<IEnumerable<Dictionary<string, object>>> ReadCSVFileAsync(string fileName)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fileName))
            {
                CSVRecord header = null;
                if (!reader.EndOfStream)
                {
                    header = new CSVRecord(await reader.ReadLineAsync());
                }
                string[] head = header.Entries;
                List<Dictionary<string, object>> dics = new List<Dictionary<string, object>>();
                while (!reader.EndOfStream)
                {
                    dics.Add(new CSVRecord(await reader.ReadLineAsync()).CreateDictionary(head));
                }
                return dics;
            }
        }
        public static async Task<IEnumerable<Dictionary<string, object>>> ReadTabFileAsync(string fileName)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fileName))
            {
                TabRecord header = null;
                if (!reader.EndOfStream)
                {
                    header = new TabRecord(await reader.ReadLineAsync());
                }
                string[] head = header.Entries;
                List<Dictionary<string, object>> dics = new List<Dictionary<string, object>>();
                while (!reader.EndOfStream)
                {
                    dics.Add(new TabRecord(await reader.ReadLineAsync()).CreateDictionary(head));
                }
                return dics;
            }
        }
    }

    class CSVRecord : Base.CSVBaseRecord
    {
        public CSVRecord(string record) : base(record) { }

        public string this[int index] { get { return base.LineItems[index]; } set { base.LineItems[index].Value = value; } }
        public string[] Entries
        {
            get
            {
                string[] e = new string[this.LineItems.Length];
                for (int i = 0; i < base.LineItems.Length; i++)
                {
                    e[i] = this.LineItems[i];
                }
                return e;
            }
        }
        public Dictionary<string, object> CreateDictionary(string[] header)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < base.LineItems.Length; i++)
            {
                dic.Add(header[i], this.LineItems[i]);
            }
            return dic;
        }
    }

    class TabRecord : Base.TabDelimitedBaseRecord
    {
        public TabRecord(string record) : base(record) { }

        public string this[int index] { get { return base.LineItems[index]; } set { base.LineItems[index].Value = value; } }
        public string[] Entries
        {
            get
            {
                string[] e = new string[this.LineItems.Length];
                for (int i = 0; i < base.LineItems.Length; i++)
                {
                    e[i] = this.LineItems[i];
                }
                return e;
            }
        }
        public Dictionary<string, object> CreateDictionary(string[] header)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < base.LineItems.Length; i++)
            {
                dic.Add(header[i], this.LineItems[i]);
            }
            return dic;
        }
    }
}
