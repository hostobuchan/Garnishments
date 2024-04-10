using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Data.Accounts
{
    [DataContract(Name = "DEBTOR", Namespace = "")]
    public class AccountDebtorStatus
    {
        [DataMember(Name = "DEBTOR")]
        public byte DebtorNo { get; private set; }
        [DataMember(Name = "ACTIVE_DUTY")]
        public bool? ActiveDuty { get; private set; }



        public override string ToString()
        {
            return $"[{DebtorNo}] {((ActiveDuty ?? false) ? "Active Duty" : "Not Active Duty")}";
        }
    }
}
