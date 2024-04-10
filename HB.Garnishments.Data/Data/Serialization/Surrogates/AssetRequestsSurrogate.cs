using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Serialization.Surrogates
{
    internal class AssetRequestsSurrogate
    {
        public RequestSurrogate[] _Requests { get; private set; }
        public RequestStatusSurrogate[] _Statuses { get; private set; }
        public Users.User[] _Users { get; private set; }
        public Requests.Results.Result[] _Results { get; private set; }
    }
}
