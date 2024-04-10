using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeInterface
{
    public class PullSubscriber
    {
        private delegate void BlankEventHandler();
        public delegate void WatermarkUpdatedEventHandler(string Watermark);
        public delegate void EventsOccurredEventHandler(GetEventsResults EventsResults);
        public event WatermarkUpdatedEventHandler WatermarkUpdated;
        public event EventsOccurredEventHandler EventsOccurred;
        private void OnWatermarkUpdated(string Watermark) { if (this.WatermarkUpdated != null) this.WatermarkUpdated(Watermark); }
        private void OnEventsOccurred(GetEventsResults EventsResults) { if (this.EventsOccurred != null) this.EventsOccurred(EventsResults); }
        
        private string _Watermark { get; set; }
        private ExchangeService Service { get; set; }
        private PullSubscription PullSubscription { get; set; }
        private System.Threading.Timer NextPull { get; set; }

        public string Watermark { get { return this._Watermark; } set { this._Watermark = value; this.OnWatermarkUpdated(value); } }


        internal PullSubscriber(ExchangeService Service, IEnumerable<FolderId> FolderIds, int Timeout, string Watermark, params EventType[] EventTypes)
        {
            this.Service = Service;
            this.PullSubscription = Service.SubscribeToPullNotifications(FolderIds, Timeout, Watermark, EventTypes);
        }

        /// <summary>
        /// Start Getting Events for Subscription
        /// </summary>
        /// <param name="Frequency">Time in milliseconds between event requests</param>
        public void StartPullEvents(int Frequency)
        {
            this.NextPull = new System.Threading.Timer(new System.Threading.TimerCallback(delegate(object o) { PullEvents(); }), null, Frequency, Frequency);
        }


        public void StopPullEvents()
        {
            this.NextPull.Dispose();
            this.NextPull = null;
            this.PullSubscription.Unsubscribe();
        }

        private void PullEvents()
        {
            GetEventsResults results = this.PullSubscription.GetEvents();
            this.Watermark = this.PullSubscription.Watermark;
            if (results.AllEvents.Count > 0)
                OnEventsOccurred(results);
        }
    }
}
