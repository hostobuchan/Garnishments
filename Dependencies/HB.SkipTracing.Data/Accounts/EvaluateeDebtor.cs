using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Accounts
{
    [DataContract(Name = "EvaluateeDebtor", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class EvaluateeDebtor : EvaluationCriteria.Accounts.EvaluateeDebtor
    {
        [DataMember(Name = "Phone", Order = 20)]
        public string Phone { get; set; }
        [DataMember(Name = "Phone2", Order = 21)]
        public string Phone2 { get; set; }
        [DataMember(Name = "Fax", Order = 22)]
        public string Fax { get; set; }
        [DataMember(Name = "Cell", Order = 23)]
        public string Mobile { get; set; }
        [DataMember(Name = "DOB", Order = 24)]
        public DateTime? DOB { get; set; }
    }
}
