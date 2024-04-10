using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing
{
    public interface IProcessor : IDisposable
    {
        event EventHandler<Exception> CommunicationProblem;
        event Processor.GarnProblem GarnishmentProblem;
        event ProgressChangedEventHandler ProcessingUpdate;

        SynchronizationContext Sync { get; }
        Enums.Status ProcessingStatus { get; }
        Enums.Status[] ItemsInStatus { get; }
        BindingList<Requests.AssetRequest> ItemsPending { get; }
        BindingList<Requests.Results.AssetRequestProcessingResult> PositiveItemsPending { get; }
        BindingList<Requests.Results.AssetRequestProcessingResult> NegativeItemsPending { get; }
        BindingList<Tuple<Requests.AssetRequest, CriteriaSets.EvaluationResult[]>> FailedItemsPending { get; }

        Task LoadAsync();
        Task EvaluateExclusionsAsync();

        void AddPositiveResult(Requests.AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult);
        void AddNegativeResult(Requests.AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult);
        void RemovePositiveResult(Requests.Results.AssetRequestProcessingResult request, bool permanent = false);
        void RemoveNegativeResult(Requests.Results.AssetRequestProcessingResult request, bool permanent = false);


        Task AddPositiveResultAsync(Requests.AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult);
        Task AddNegativeResultAsync(Requests.AssetRequestAndAccount request, Func<IEnumerable<Requests.Results.Result>, IEnumerable<Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Requests.Results.Result, Dictionary<string, object>>> selectResult);
        Task RemovePositiveResultAsync(Requests.Results.AssetRequestProcessingResult request, bool permanent = false);
        Task RemoveNegativeResultAsync(Requests.Results.AssetRequestProcessingResult request, bool permanent = false);

        Task ProcessResultsAsync();
        Task CancelResultsAsync();
    }
}
