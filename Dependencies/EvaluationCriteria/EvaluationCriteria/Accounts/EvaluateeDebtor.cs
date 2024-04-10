using EvaluationCriteria.Attributes;
using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using System;
using System.Runtime.Serialization;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria.Accounts
{
    [DataContract]
    public class EvaluateeDebtor
    {
        [DataMember(Name = "Account", Order = 0)]
        public object Account { get; private set; }
        [DataMember(Name = "Address", Order = 1)]
        public string Address1 { get; set; }
        [DataMember(Name = "Address2", Order = 2)]
        public string Address2 { get; set; }
        [DataMember(Name = "AnswerFiled", Order = 3)]
        [PropertyAssociation("AF_DATE", typeof(CDateParam), Description = "Answer Filed")]
        public DateTime? AnswerFiled { get; private set; }
        [DataMember(Name = "BankruptcyChapter", Order = 4)]
        [PropertyAssociation("BK_CP", typeof(CBasicParam), Description = "Bankruptcy Chapter")]
        public string BankruptcyChapter { get; set; }
        [DataMember(Name = "BankruptcyDate", Order = 5)]
        [PropertyAssociation("BK_DATE", typeof(CDateParam), Description = "Bankruptcy Date")]
        public DateTime? BankruptcyDate { get; set; }
        [DataMember(Name = "BankruptcyFileNo", Order = 6)]
        [PropertyAssociation("BK_FILE", typeof(CSimpleParam), Description = "Bankruptcy FileNo")]
        public string BankruptcyFileNo { get; set; }
        [DataMember(Name = "City", Order = 7)]
        public string City { get; set; }
        [DataMember(Name = "Name", Order = 8)]
        public string CLSName { get; private set; }
        [DataMember(Name = "DeathDate", Order = 9)]
        [PropertyAssociation("DEATH_DATE", typeof(CDateParam), Description = "Death Date")]
        public DateTime? DeathDate { get; set; }
        [DataMember(Name = "Debtor", Order = 10)]
        public int Debtor { get; set; }
        [DataMember(Name = "DismissalDate", Order = 11)]
        [PropertyAssociation("DSMIS_DATE", typeof(CDateParam), Description = "Dismissal Date")]
        public DateTime? DismissalDate { get; set; }

        public string DisplayName { get { return $"{Name[0]} {Name[1]} {Name[2]}{(string.IsNullOrWhiteSpace(Name[3]) ? "" : $" {Name[3]}")}"; } }
        [DataMember(Name = "GarnishmentDate", Order = 12)]
        [PropertyAssociation("GARN_DATE", typeof(CDateParam), Description = "Garnishment Date")]
        public DateTime? GarnishmentDate { get; set; }
        [DataMember(Name = "HasPhone", Order = 13)]
        [PropertyAssociation("HAS_PHONE", typeof(CSimpleParam), Description = "Has Phone")]
        public bool? HasPhone { get; set; }
        [DataMember(Name = "HomeOwner", Order = 14)]
        [PropertyAssociation("HOME_OWN", typeof(CSimpleParam), Description = "Home Owner")]
        public bool? HomeOwner { get; set; }

        public string[] Name { get { return DebtorName(this.CLSName); } }
        public string NameFirst { get { return this.Name[0]; } }
        public string NameMiddle { get { return this.Name[1]; } }
        public string NameLast { get { return this.Name[2]; } }
        public string NameSuffix { get { return this.Name[3]; } }

        [DataMember(Name = "ReturnMail", Order = 15)]
        [PropertyAssociation("RET_MAIL", typeof(CSimpleParam), Description = "Return Mail")]
        public bool ReturnMail { get; set; }
        [DataMember(Name = "ServiceDate", Order = 16)]
        [PropertyAssociation("SERVICE_DATE", typeof(CDateParam), Description = "Service Date")]
        public DateTime? ServiceDate { get; set; }
        [DataMember(Name = "SSN", Order = 17)]
        [PropertyAssociation("SSN", typeof(CSimpleParam), Description = "SSN")]
        public string SSN { get; set; }
        [DataMember(Name = "State", Order = 18)]
        [PropertyAssociation("STATE", typeof(CStringParam), Description = "State")]
        public string State { get; set; }
        [DataMember(Name = "Zip", Order = 19)]
        public string Zip { get; set; }

        public override string ToString()
        {
            return $"{this.DisplayName}";
        }
    }
}
