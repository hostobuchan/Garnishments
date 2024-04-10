using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing
{
    public static class ReleaseOfGarnProcessor
    {
        public static IEnumerable<RecordTypes2.YGC.RecordType09> CreateNoteRecords(IEnumerable<Tuple<string, DateTime, int?>> accounts)
        {
            foreach (var acct in accounts)
            {
                yield return new RecordTypes2.YGC.RecordType09()
                {
                    MASCO_FILE = acct.Item1,
                    PCMT = $"FM ROG RCVD {acct.Item2:M/d/yyyy}"
                };
            }
        }
        public static IEnumerable<RecordTypes.MergePops.MergePop> CreateMergeRecords(IEnumerable<string> accounts)
        {
            foreach (var acct in accounts)
            {
                yield return new RecordTypes.MergePops.MergePop()
                {
                    FILENO = acct,
                    LLCODE = "XGREL"
                };
            }
        }
        public static async Task<Tuple<bool, int?>> GetGarnishmentRequestAsync(string fileNo, Func<IEnumerable<Data.Requests.AssetRequest>, Tuple<bool, int?>> selectRequest)
        {
            var requests = await DataHandler.GetAccountAssetRequestInfoByFileNoAsync(fileNo);
            var activeRequests = requests?.Where(req => req.IsActive).ToArray();
            if (activeRequests?.Length > 0)
            {
                if (activeRequests.Length == 1)
                {
                    // Only One in The Pipeline - Select It
                    return new Tuple<bool, int?>(true, activeRequests[0].ID);
                }
                else
                {
                    // Multiple - Now What?
                    return selectRequest?.Invoke(activeRequests);
                }
            }
            else
            {
                return new Tuple<bool, int?>(true, null);
            }
        }
        public static async Task RejectGarnsAsync(IEnumerable<int?> requests, string userName)
        {
            foreach (var req in requests.Where(r => r.HasValue)) {
                await DataHandler.AddStatusAsync(
                    req.Value,
                    Enums.Status.Rejected,
                    userName,
                    DateTime.Now,
                    note: "Release of Garn");
            }
        }

        public static async Task ProcessReleasesAsync(IEnumerable<Tuple<string, DateTime, int?>> accounts, string userName, Func<string> getSaveFile)
        {
            var notes = CreateNoteRecords(accounts).ToArray();
            var merges = CreateMergeRecords(accounts.Select(acct => acct.Item1)).ToArray();

            using (RecordTypes.MergePops.FileWriter writer = new RecordTypes.MergePops.FileWriter(getSaveFile()))
            {
                await writer.WriteFileAsync(merges);
            }
            RecordTypes.Output.Send_YGC_Imp(notes);

            await RejectGarnsAsync(accounts.Select(acct => acct.Item3), userName);
        }
    }
}
