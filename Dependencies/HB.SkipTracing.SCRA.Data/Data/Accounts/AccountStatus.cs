using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Data.Accounts
{
    [DataContract(Name = "Account", Namespace = "")]
    public class AccountStatus
    {
        [DataMember(Name = "FILENO")]
        public string FileNo { get; private set; }
        [DataMember(Name = "DEBTORS")]
        public AccountDebtorStatus[] Debtors { get; private set; }
        
        [IgnoreDataMember]
        public bool ActiveDuty { get { return this.Debtors.Count(d => (d.ActiveDuty ?? false)) > 0; } }



        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            if (Debtors == null) Debtors = new AccountDebtorStatus[0];
        }
    }
}
