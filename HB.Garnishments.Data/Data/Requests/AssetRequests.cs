using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    [DataContract(Name = "AssetRequests", Namespace = "")]
    class AssetRequests
    {
        [DataMember(Name = "Users", Order = 1)]
        protected Users.User[] _Users { get; private set; }
        [DataMember(Name = "Results", Order = 2)]
        protected Results.Result[] _Results { get; private set; }
        [DataMember(Name = "Requests", Order = 3)]
        public Requests.AssetRequest[] Requests { get; private set; }

        //internal AssetRequests(Serialization.Surrogates.AssetRequestsSurrogate surrogateHandler)
        //{
        //    this.Requests = surrogateHandler._Requests.Select(r => new Requests.AssetRequest(surrogateHandler, r)).ToArray();
        //}
    }
}
