using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing.Rejects
{
    public static class ReportGenerator
    {
        public static DataTable CreateRejectTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("File No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Debtor", typeof(byte)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Asset", typeof(string)));
            dt.Columns.Add(new DataColumn("Reject Reason", typeof(string)));
            return dt;
        }

        public static void AddRejectToTable(DataTable table, Tuple<Requests.AssetRequest, CriteriaSets.EvaluationResult[]> reject)
        {
            DataRow dr = table.NewRow();
            dr["File No."] = reject.Item1.Asset.Account.FileNo;
            dr["Debtor"] = reject.Item1.Asset.Debtor;
            dr["Type"] = reject.Item1.Asset.Type.ToString();
            dr["Asset"] = reject.Item1.Asset.Name;
            dr["Reject Reason"] = GetRejectDescriptionFlat(reject.Item2);
            table.Rows.Add(dr);
        }

        public static string GetRejectDescription(IEnumerable<CriteriaSets.EvaluationResult> results)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var set in results.Where(r => !r.Result.Success).GroupBy(r => r.Set.Description))
            {
                sb.AppendLine($"{set.Key}:");
                foreach (var result in results.Where(r => !r.Result.Success))
                {
                    sb.AppendLine($"     {result.Criteria.Name} - {result.Result.Info}");
                }
            }
            return sb.ToString();
        }
        public static string GetRejectDescriptionFlat(IEnumerable<CriteriaSets.EvaluationResult> results)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var set in results.Where(r => !r.Result.Success).GroupBy(r => r.Set.Description))
            {
                sb.Append($"[{set.Key}:");
                foreach (var result in results.Where(r => !r.Result.Success))
                {
                    sb.Append($" {result.Criteria.Name} - {result.Result.Info},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            return sb.ToString();
        }

    }
}
