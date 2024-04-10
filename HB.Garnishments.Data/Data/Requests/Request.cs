using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    [DataContract(Name = "Request", Namespace = "")]
    public abstract class Request
    {
        [DataMember(Name = "UID")]
        public int ID { get; protected set; }
        [DataMember(Name = "AIID")]
        public ulong AssetInfoID { get; protected set; }
        [DataMember(Name = "RAID")]
        public int? RegisteredAgent { get; set; }
        [DataMember(Name = "History")]
        public Status[] History { get; protected set; }
        [DataMember(Name = "Override")]
        public Users.User OverrideByUser { get; protected set; }

        #region Calculated Data Members
        [IgnoreDataMember]
        public Status this[Enums.Status status]
        {
            get
            {
                return this.History.GroupBy(s => s.Type)?.Where(g => g.Key == status)?.SelectMany(g => g)?.OrderBy(s => s.ID)?.LastOrDefault();
            }
        }
        [IgnoreDataMember]
        public Status CurrentStatus
        {
            get
            {
                return this.History.FirstOrDefault(s => s.ID == this.History.Max(s2 => s2.ID));
            }
        }
        [IgnoreDataMember]
        public bool IsActive
        {
            get
            {
                return !DataHandler.FinalStatuses.Contains(this.CurrentStatus.Type);
            }
        }
        #endregion

        public override string ToString()
        {
            return ToString("G");
        }
        public virtual string ToString(string format)
        {
            switch (format)
            {
                default:
                    return $"[{ID}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName}";
            }
        }
    }
}
