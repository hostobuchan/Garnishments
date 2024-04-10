using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Reporting
{
    public static class ReportDataHandler<T> where T : Records.DownloadRecord
    {
        public static async Task GenerateReportAsync(IEnumerable<AssetResult<T>> results)
        {
            if (results.Count(r => r.Result != Enums.AssetResultType.AddedAndGarned) > 0)
            {
                DataTable dt = CreateDataTable();
                dt.TableName = "Failed";
                foreach (var result in results.Where(res => res.Result != Enums.AssetResultType.AddedAndGarned))
                {
                    AddResultToTable(dt, result);
                }

                ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();
                xlApp.xlBook.AddWorksheetFromTable(dt);
                xlApp.ShowWorkBook();
            }
        }

        private static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FILENO", typeof(string));
            dt.Columns.Add("DEBTOR", typeof(byte));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("ADDED", typeof(bool));
            dt.Columns.Add("GARNED", typeof(bool));
            dt.Columns.Add("GARN ID", typeof(int));
            dt.Columns.Add("ADD REASON", typeof(string));
            dt.Columns.Add("GARN REASON", typeof(string));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("ASSET PREV ON FILE", typeof(bool));
            dt.Columns.Add("ASSET PREV GARNED", typeof(bool));
            dt.Columns.Add("PREV ID", typeof(ulong));
            dt.Columns.Add("PREV INFO", typeof(string));
            dt.Columns.Add("NEW ID", typeof(ulong));
            dt.Columns.Add("NEW INFO", typeof(string));
            return dt;
        }
        private static void AddResultToTable(DataTable dt, AssetResult<T> result)
        {
            DataRow dr = dt.NewRow();
            dr["FILENO"] = result.DownloadRecord.FileNo;
            dr["DEBTOR"] = result.DownloadRecord.DebtorNo;
            dr["TYPE"] = result.DownloadRecord.AssetType.ToString();
            dr["ADDED"] = result.Result.HasFlag(Enums.AssetResultType.Added);
            dr["GARNED"] = result.Result.HasFlag(Enums.AssetResultType.Garnished);
            dr["GARN ID"] = result.GarnishmentID;
            dr["ADD REASON"] = result.AssetResultReason.ToString();
            dr["GARN REASON"] = result.GarnishmentResultReason.ToString();
            dr["DESCRIPTION"] = result.Description;
            dr["ASSET PREV ON FILE"] = !result.AssetInfo?.IsNew;
            dr["ASSET PREV GARNED"] = result.AssetInfo?.HasGarn;
            dr["PREV ID"] = result.AssetInfo?.MatchingAsset?.CurrentInfo?.Info?.ID ?? (object)DBNull.Value;
            dr["PREV INFO"] = result.AssetInfo?.MatchingAsset?.ToString() ?? "";
            dr["NEW ID"] = result.NewAsset?.Info?.ID ?? (object)DBNull.Value;
            dr["NEW INFO"] = result.NewAsset?.ToString() ?? "";
            dt.Rows.Add(dr);
        }
    }
}
