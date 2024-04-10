using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Data.Records
{
    class UploadRecord : SkipTracing.Data.RNN.Records.UploadRecord
    {
        public UploadRecord(Accounts.AccountDebtor accountDebtor)
            : base(
                        accountDebtor.FILENO,
                        accountDebtor.NUMBER,
                        accountDebtor.SSN,
                        accountDebtor.FirstName,
                        accountDebtor.LastName,
                        accountDebtor.STREET,
                        accountDebtor.STREET2,
                        accountDebtor.CITY,
                        accountDebtor.STATE,
                        accountDebtor.ZIP,
                        accountDebtor.PHONE,
                        accountDebtor.PHONE2,
                        accountDebtor.FAX,
                        cf2: accountDebtor.BALANCE.ToString("N2"),
                        openDate: accountDebtor.OPENED_DATE
                  )
        { }
    }
}
