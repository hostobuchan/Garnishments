using System.Collections.Generic;

namespace RecordTypes.Utilities
{
    public static class FileReader
    {
        public static IEnumerable<string> ReadSingleAccountPerLine(string fileName)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                foreach (var acct in ReadSingleAccountPerLine(fs))
                {
                    yield return acct;
                }
            }
        }
        public static IEnumerable<string> ReadSingleAccountPerLine(System.IO.Stream stream)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        public static IEnumerable<string> ReadTabDelimitedAccountPerLine(string fileName)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                foreach (var acct in ReadTabDelimitedAccountPerLine(fs))
                {
                    yield return acct;
                }
            }
        }
        public static IEnumerable<string> ReadTabDelimitedAccountPerLine(System.IO.Stream stream)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string[] Entries = reader.ReadLine().Split('\t');
                    yield return Entries[0];
                }
            }
        }

        public static IEnumerable<string> ReadCSVAccountPerLine(string fileName)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                foreach (var acct in ReadCSVAccountPerLine(fs))
                {
                    yield return acct;
                }
            }
        }
        public static IEnumerable<string> ReadCSVAccountPerLine(System.IO.Stream stream)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string[] Entries = reader.ReadLine().Split(',');
                    yield return Entries[0];
                }
            }
        }
    }
}
