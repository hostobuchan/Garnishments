using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Data.Accounts
{
    [DataContract(Name = "AccountDebtor", Namespace = "")]
    class AccountDebtor
    {
        [DataMember(Name = "BIRTH_DATE")]
        public string BIRTH_DATE { get; private set; }
        [DataMember(Name = "CITY")]
        public string CITY { get; private set; }
        [DataMember(Name = "FAX")]
        public string FAX { get; private set; }
        [DataMember(Name = "NAME")]
        public string CLSNAME { get; private set; }
        [DataMember(Name = "SSN")]
        public string SSN { get; private set; }
        [DataMember(Name = "NUMBER")]
        public byte NUMBER { get; private set; }
        [DataMember(Name = "PHONE")]
        public string PHONE { get; private set; }
        [DataMember(Name = "PHONE2")]
        public string PHONE2 { get; private set; }
        [DataMember(Name = "ST")]
        public string STATE { get; private set; }
        [DataMember(Name = "STREET")]
        public string STREET { get; private set; }
        [DataMember(Name = "STREET2")]
        public string STREET2 { get; private set; }
        [DataMember(Name = "ZIP")]
        public string ZIP { get; private set; }
        [DataMember(Name = "BALANCE")]
        public decimal BALANCE { get; private set; }
        [DataMember(Name = "FILENO")]
        public string FILENO { get; private set; }
        [DataMember(Name = "OPENED_DATE")]
        public DateTime? OPENED_DATE { get; private set; }

        public string FirstName
        {
            get
            {
                try
                {
                    return this.Name[0];
                }
                catch { return string.Empty; }
            }
        }
        public string LastName
        {
            get
            {
                try
                {
                    return this.Name[2];
                }
                catch { return string.Empty; }
            }
        }
        public string[] Name
        {
            get
            {
                return EvaluationCriteria.Utilities.DebtorName(this.CLSNAME);
            }
        }
    }
}
