using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HB.Garnishments.Data.Search
{
    [DataContract(Name = "SNResult", Namespace = "")]
    public class SearchNameResult
    {
        [DataMember(Name = "UID")]
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; private set; }
        [DataMember(Name = "NAME")]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; private set; }
    }
}
