using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HB.Garnishments.Data.CertifiedMail
{
    public class SSILetter
    {
        public Accounts.EvaluateeAccount Account { get; private set; }
        public WalzMailEntry[] Recipients { get; private set; }

        public SSILetter(Accounts.EvaluateeAccount account, IEnumerable<WalzMailEntry> recipients)
        {
            this.Account = account;
            this.Recipients = recipients.ToArray();
        }
    }
}
