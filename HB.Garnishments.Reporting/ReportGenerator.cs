using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Reporting
{
    public static class ReportGenerator
    {
        private static Data.Accounts.Venue[] Venues { get; set; }

        public static async Task CreateUnprocessedReportAsync(Data.Enums.Status status, System.Windows.Forms.Delegates.TextMultiProgressUpdatedEventHandler progress)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            var states = await Data.DataHandler.GetStatesAsync();
            Venues = await Data.DataHandler.GetVenuesAsync();
            var requests = await Data.DataHandler.GetAccountAssetRequestsInfoInStatusAsync(status);
            var accounts = await Data.Accounts.EvaluateeDataHandler.GetDataHandlerAsync(requests.Select(r => r.Asset.Account.FileNo).Distinct());
            progress?.Invoke(0, "Processing", "Associating Account Info", 1, 3);

            int ttl = requests.Count();
            int cnt = 0;
            List<Data.Requests.AssetRequestAndAccount> requestAccounts = new List<Data.Requests.AssetRequestAndAccount>();
            foreach (var request in requests)
            {
                progress?.Invoke(cnt * 100 / ttl, "Processing", status.ToString(), 2, 3);
                var account = accounts.Accounts.FirstOrDefault(acct => acct.FileNo.Equals(request.Asset.Account.FileNo, StringComparison.OrdinalIgnoreCase));
                if (account != default)
                {
                    requestAccounts.Add(new Data.Requests.AssetRequestAndAccount(request, account));
                }
            }


            var xlApp = new ExcelInterface.Application.Excel();
            progress?.Invoke(0, "Generating Report", "", 3, 3);
            foreach (var stateRequests in requestAccounts.GroupBy(req => states.FirstOrDefault(s => s.SalesNo == req.Account?.SalesNo).Abbreviation, StringComparer.OrdinalIgnoreCase).OrderBy(stateR => stateR.Key))
            {
                using (var dt = CreateDataTable())
                {
                    dt.TableName = stateRequests.Key?.ToUpper();
                    foreach (var request in stateRequests)
                    {
                        AddRecordToDataTable(dt, request);
                    }
                    xlApp.xlBook.AddWorksheetFromTable(dt);
                }
            }
            xlApp.ShowWorkBook();
        }

        private static DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FILENO", typeof(string));
            dt.Columns.Add("POE/BANK", typeof(string));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("RESULT", typeof(string));
            dt.Columns.Add("DATE", typeof(DateTime));
            dt.Columns.Add("VENUE", typeof(string));
            dt.Columns.Add("COUNTY", typeof(string));
            dt.Columns.Add("ASSET", typeof(string));
            dt.Columns.Add("INFO", typeof(string));
            return dt;
        }
        private static void AddRecordToDataTable(DataTable table, Data.Requests.AssetRequestAndAccount request)
        {
            var venue = Venues.FirstOrDefault(v => v.VenueNo == request.Account?.Venue);
            var newRow = table.NewRow();
            newRow["FILENO"] = request.Asset.Account?.FileNo;
            newRow["POE/BANK"] = request.Asset.Type.ToString();
            newRow["STATUS"] = request.CurrentStatus.Type.ToString();
            newRow["RESULT"] = request.CurrentStatus.Result?.Description;
            newRow["DATE"] = request.CurrentStatus.Date;
            newRow["VENUE"] = $"{venue?.VenueNo} - {venue?.Name}";
            newRow["COUNTY"] = venue?.County;
            newRow["ASSET"] = request.Asset.Name;
            newRow["INFO"] = request.CurrentStatus.Note;
            table.Rows.Add(newRow);
        }
    }
}
