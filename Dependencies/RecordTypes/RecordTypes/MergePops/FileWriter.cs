using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecordTypes.MergePops
{
    public class FileWriter : IDisposable
    {
        internal event Func<MergePop, string> WriteFormattedMergePop;

        private System.IO.FileInfo File { get; set; }

        public FileWriter(string fileName) : this(new System.IO.FileInfo(fileName)) { }
        public FileWriter(System.IO.FileInfo file)
        {
            this.File = file;
        }

        public void WriteFile(IEnumerable<MergePop> mergePops, Action<int> progress = null)
        {
            List<Exception> exceptions = new List<Exception>();
            Func<MergePop, string> mWriter = (m) => { return WriteMergePop(m, mergePops); };
            this.WriteFormattedMergePop = mWriter;

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(this.File.OpenWrite()))
            {
                writer.WriteLine(WriteHeader(mergePops));
                int p = 0;
                int ttl = mergePops.Count();
                foreach (var mergePop in mergePops)
                {
                    p++;
                    progress?.Invoke(p * 100 / ttl);
                    if (mergePop != null)
                    {
                        try
                        {
                            var line = this.WriteFormattedMergePop?.Invoke(mergePop);
                            if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                            {
                                writer.WriteLine(line);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Data.Add("MergePop", mergePop);
                            exceptions.Add(ex);
                        }
                    }
                }
                writer.Flush();
            }

            this.WriteFormattedMergePop = null;
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        public async Task WriteFileAsync(IEnumerable<MergePop> mergePops, Action<int> progress = null)
        {
            List<Exception> exceptions = new List<Exception>();
            Func<MergePop, string> mWriter = (m) => { return WriteMergePop(m, mergePops); };
            this.WriteFormattedMergePop = mWriter;

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(this.File.OpenWrite()))
            {
                await writer.WriteLineAsync(WriteHeader(mergePops));
                int p = 0;
                int ttl = mergePops.Count();
                foreach (var mergePop in mergePops)
                {
                    p++;
                    progress?.Invoke(p * 100 / ttl);
                    if (mergePop != null)
                    {
                        try
                        {
                            var line = this.WriteFormattedMergePop?.Invoke(mergePop);
                            if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                            {
                                await writer.WriteLineAsync(line);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Data.Add("MergePop", mergePop);
                            exceptions.Add(ex);
                        }
                    }
                }
                await writer.FlushAsync();
            }

            this.WriteFormattedMergePop = null;
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
        internal string WriteMergePop(MergePop mergePop)
        {
            return WriteFormattedMergePop?.Invoke(mergePop);
        }

        private string WriteHeader(IEnumerable<MergePop> allRecords)
        {
            if (allRecords.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                var props = typeof(MergePop).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                var ediProps = props.Select(el => new MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.MergePopFieldAttribute), true).FirstOrDefault() as Attributes.MergePopFieldAttribute });
                var rGroupProps = props.Select(el => new MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.MergePopRequiredGroupFieldAttribute), true).FirstOrDefault() as Attributes.MergePopRequiredGroupFieldAttribute }).Where(el => el.Attributes != null);
                var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
                foreach (var group in rGroupProps.GroupBy(el => (el.Attributes as Attributes.MergePopRequiredGroupFieldAttribute).GroupNumber))
                {
                    if (group.Count(el => allRecords.Select(rec => el.PropertyInfo.GetValue(rec, null)).Count(val => !string.IsNullOrEmpty(val?.ToString().Trim())) > 0) == 0)
                    {
                        throw new InvalidOperationException($"All Records Missing Required Information\n\n{string.Join(" or ", group.Select(el => el.Attributes.Header).ToArray())}");
                    }
                }
                foreach (var field in orderedProps)
                {
                    if (field.Attributes.Required || allRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                        sb.Append($"{field.Attributes.Header}\t");
                }
                return sb.ToString();
            }
            return null;
        }
        private string WriteMergePop(MergePop mergePop, IEnumerable<MergePop> allRecords)
        {
            if (mergePop != null && allRecords.Contains(mergePop))
            {
                StringBuilder sb = new StringBuilder();
                var props = typeof(MergePop).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                var ediProps = props.Select(el => new MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.MergePopFieldAttribute), true).FirstOrDefault() as Attributes.MergePopFieldAttribute });
                var rGroupProps = props.Select(el => new MPField() { PropertyInfo = el, Attributes = el.GetCustomAttributes(typeof(Attributes.MergePopRequiredGroupFieldAttribute), true).FirstOrDefault() as Attributes.MergePopRequiredGroupFieldAttribute }).Where(el => el.Attributes != null);
                var orderedProps = ediProps.OrderBy(el => el.Attributes?.Order);
                foreach (var group in rGroupProps.GroupBy(el => (el.Attributes as Attributes.MergePopRequiredGroupFieldAttribute).GroupNumber))
                {
                    if (group.Count(el => !string.IsNullOrEmpty(el.PropertyInfo.GetValue(mergePop, null)?.ToString().Trim())) == 0)
                    {
                        throw new InvalidOperationException($"Record Missing Required Information\n\n{string.Join(" or ", group.Select(el => el.Attributes.Header).ToArray())}");
                    }
                }
                foreach (var field in orderedProps)
                {
                    if (field.Attributes.Required || allRecords.Select(el => field.PropertyInfo.GetValue(el, null)).Count(el => !string.IsNullOrEmpty(el?.ToString())) > 0)
                        sb.Append($"{field.PropertyInfo.GetValue(mergePop, null)}\t");
                }
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
