using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    [DataContract(Name = "Status", Namespace = "")]
    public class Status
    {
        [DataMember(Name = "UID")]
        public ulong ID { get; private set; }
        [DataMember(Name = "STATUS")]
        public Enums.Status Type { get; private set; }
        [DataMember(Name = "DATE")]
        public DateTime Date { get; private set; }
        [DataMember(Name = "User")]
        public Users.User User { get; private set; }
        [DataMember(Name = "Result")]
        public Results.Result Result { get; private set; }
        [DataMember(Name = "NOTE")]
        public string Note { get; private set; }

        internal Status(Serialization.Surrogates.AssetRequestsSurrogate surrogateHandler, Serialization.Surrogates.RequestStatusSurrogate status)
        {
            this.ID = status.ID;
            this.Type = status.Type;
            this.Date = status.Date;
            this.User = surrogateHandler._Users.FirstOrDefault(u => u.ID == status.UserID);
            this.Result = surrogateHandler._Results.FirstOrDefault(r => r.ID == status.ResultID);
            this.Note = status.Note;
        }

        public override string ToString()
        {
            if (Result == null)
                return $"[{ID.ToString().PadLeft(5, '0')}] {Date:yyyyMMdd} {Type} {User.DisplayName}";
            else
                return $"[{ID.ToString().PadLeft(5, '0')}] {Date:yyyyMMdd} {User.DisplayName} Result: {Result}";
        }
    }
}
