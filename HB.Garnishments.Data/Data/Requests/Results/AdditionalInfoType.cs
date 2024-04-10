using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results
{
    public struct AdditionalInfoType
    {
        public string Key { get; private set; }
        public Enums.ResultInfoType InfoType { get; private set; }

        public AdditionalInfoType(string key, Enums.ResultInfoType infoType)
        {
            this.Key = key;
            this.InfoType = infoType;
        }
    }
}
