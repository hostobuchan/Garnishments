using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HB.Garnishments.Data.Communications;
using HB.Garnishments.Data.Enums;
using HB.Garnishments.Data.Requests;
using HB.Garnishments.Data.Requests.Results;
using Status = HB.Garnishments.Data.Enums.Status;

namespace HB.Garnishments.Data.Processing
{
    public abstract class NoNegativeResultProcessor : Processor
    {
        protected NoNegativeResultProcessor(SynchronizationContext sync, HubManager hubManager, Status process, string userName, byte? salesRestriction = null, params Status[] status) : base(sync, hubManager, process, userName, salesRestriction, status)
        {
        }

        public override void AddNegativeResult(AssetRequestAndAccount request, Func<IEnumerable<Result>, IEnumerable<Data.Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Result, Dictionary<string, object>>> selectResult)
        {
            throw new InvalidOperationException("Not Allowed");
        }
        public override Task AddNegativeResultAsync(AssetRequestAndAccount request, Func<IEnumerable<Result>, IEnumerable<Data.Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Result, Dictionary<string, object>>> selectResult)
        {
            throw new InvalidOperationException("Not Allowed");
        }
    }
}
