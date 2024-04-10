using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RecordTypes.CMEDI
{
    public class FileWriter : IDisposable
    {
        internal event Func<Base.CMEDIBase, string> WriteFormattedMergePop;

        private System.IO.FileInfo File { get; set; }

        public FileWriter(string fileName) : this(new System.IO.FileInfo(fileName)) { }
        public FileWriter(System.IO.FileInfo file)
        {
            this.File = file;
        }

        public void WriteFile(IEnumerable<Base.CMEDIBase> records, Action<int> progress = null)
        {
            List<Exception> exceptions = new List<Exception>();

            int p = 0;
            int ttl = records.Count();
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(this.File.OpenWrite()))
            {
                foreach (var recordGroup in records.GroupBy(rec => rec.GetType()).OrderBy(rg => rg.First().Record, StringComparer.OrdinalIgnoreCase))
                {
                    Func<Base.CMEDIBase, string> mWriter = (m) => { return WriteRecord(m, recordGroup); };
                    this.WriteFormattedMergePop = mWriter;

                    bool NeedsHeader = Convert.ToBoolean(recordGroup.Key.GetProperty("NeedsHeader").GetValue(recordGroup.First(), null));
                    if (NeedsHeader)
                    {
                        writer.WriteLine(WriteHeader(recordGroup));
                    }
                    foreach (var record in recordGroup)
                    {
                        p++;
                        progress?.Invoke(p * 100 / ttl);
                        if (record != null)
                        {
                            try
                            {
                                var line = this.WriteFormattedMergePop?.Invoke(record);
                                if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                                {
                                    writer.WriteLine(line);
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Add("Record", record);
                                exceptions.Add(ex);
                            }
                        }
                    }
                    writer.Flush();
                }

                this.WriteFormattedMergePop = null;
            }

            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        internal string WriteMergePop(Base.CMEDIBase record)
        {
            return WriteFormattedMergePop?.Invoke(record);
        }

        private string WriteHeader(IGrouping<Type, Base.CMEDIBase> allRecords)
        {
            if (allRecords.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                var props = allRecords.Key.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var ediProps = props.Select(el => new MergePops.MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(MergePops.Attributes.MergePopFieldAttribute), true).FirstOrDefault() as MergePops.Attributes.MergePopFieldAttribute });
                var rGroupProps = props.Select(el => new MergePops.MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(MergePops.Attributes.MergePopRequiredGroupFieldAttribute), true).FirstOrDefault() as MergePops.Attributes.MergePopRequiredGroupFieldAttribute }).Where(el => el.Attributes != null);
                var orderedProps = ediProps.Where(ep => ep.Attributes != null).OrderBy(el => el.Attributes?.Order);
                foreach (var group in rGroupProps.GroupBy(el => (el.Attributes as MergePops.Attributes.MergePopRequiredGroupFieldAttribute).GroupNumber))
                {
                    if (group.Count(el => allRecords.Select(rec => el.PropertyInfo.GetValue(rec, null)).Count(val => !string.IsNullOrEmpty(val?.ToString().Trim())) > 0) == 0)
                    {
                        throw new InvalidOperationException($"All Records Missing Required Information\n\n{string.Join(" or ", group.Select(el => el.Attributes.Header).ToArray())}");
                    }
                }
                foreach (var field in orderedProps)
                {
                    if (field.Attributes.Required || allRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                    {
                        if (string.IsNullOrEmpty(field.Attributes.Header))
                        {
                            sb.Append($"{field.PropertyInfo.GetValue(allRecords.First(), null)}\t");
                        }
                        else
                        {
                            sb.Append($"{field.Attributes.Header}\t");
                        }
                    }
                }
                sb.Append("\t#");
                return sb.ToString();
            }
            return null;
        }
        private string WriteRecord(Base.CMEDIBase record, IGrouping<Type, Base.CMEDIBase> allRecords)
        {
            if (record != null && allRecords.Contains(record))
            {
                StringBuilder sb = new StringBuilder();
                var props = allRecords.Key.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                var ediProps = props.Select(el => new MergePops.MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(MergePops.Attributes.MergePopFieldAttribute), true).FirstOrDefault() as MergePops.Attributes.MergePopFieldAttribute });
                var rGroupProps = props.Select(el => new MergePops.MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(MergePops.Attributes.MergePopRequiredGroupFieldAttribute), true).FirstOrDefault() as MergePops.Attributes.MergePopRequiredGroupFieldAttribute }).Where(el => el.Attributes != null);
                var orderedProps = ediProps.Where(ep => ep.Attributes != null).OrderBy(el => el.Attributes?.Order);
                foreach (var group in rGroupProps.GroupBy(el => (el.Attributes as MergePops.Attributes.MergePopRequiredGroupFieldAttribute).GroupNumber))
                {
                    if (group.Count(el => !string.IsNullOrEmpty(el.PropertyInfo.GetValue(record, null)?.ToString().Trim())) == 0)
                    {
                        throw new InvalidOperationException($"Record Missing Required Information\n\n{string.Join(" or ", group.Select(el => el.Attributes.Header).ToArray())}");
                    }
                }
                foreach (var field in orderedProps)
                {
                    if (field.Attributes.Required || allRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                        sb.Append($"{field.PropertyInfo.GetValue(record, null)}\t");
                }
                sb.Append("\t#");
                return sb.ToString();
            }
            return null;
        }

        public void Dispose()
        {
            this.WriteFormattedMergePop = null;
            this.File = null;
        }
    }
}
