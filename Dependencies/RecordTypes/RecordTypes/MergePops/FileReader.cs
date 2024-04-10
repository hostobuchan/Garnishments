using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecordTypes.MergePops
{
    public static class FileReader
    {
        public static List<MergePop> ReadFile(string fileName, char delimiter = '\t')
        {
            return ReadFile(new System.IO.FileInfo(fileName), delimiter);
        }
        public static List<MergePop> ReadFile(System.IO.FileInfo file, char delimiter = '\t')
        {
            List<MergePop> merges = new List<MergePop>();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(file.FullName))
            {
                string[] header = reader.ReadLine().Split(delimiter);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    merges.Add(ReadMergePop(header, line.Split(delimiter)));
                }
            }
            return merges;
        }

        private static MergePop ReadMergePop(string[] head, string[] elements)
        {
            var props = typeof(MergePop).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var ediProps = props.Select(el => new MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.MergePopFieldAttribute), true).FirstOrDefault() as Attributes.MergePopFieldAttribute });

            MergePop mp = new MergePops.MergePop();
            try
            {
                for (int i = 0; i < head.Length; i++)
                {
                    string propName = head[i];
                    ediProps.FirstOrDefault(ep => string.Equals(ep.Attributes.Header, propName, StringComparison.Ordinal)).PropertyInfo.SetValue(mp, elements[i], null);
                }
            }
            catch (Exception ex)
            {
                mp = null;
            }
            return mp;
        }
    }
}
