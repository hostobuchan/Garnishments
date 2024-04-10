using System.Runtime.Serialization;

namespace EvaluationCriteria.CriteriaSets
{
    [DataContract(Name = "State", Namespace = "")]
    public struct State : Interfaces.ICriteriaParam
    {
        [DataMember(Name = "SID")]
        public int ID { get; private set; }
        [DataMember(Name = "State")]
        public string Abbreviation { get; private set; }
        [DataMember(Name = "SALES")]
        public byte SalesNo { get; private set; }
        [DataMember(Name = "WEFILE")]
        public bool WeFile { get; private set; }
        [DataMember(Name = "STATUTE")]
        public byte Statute { get; private set; }
        [DataMember(Name = "STATUTE_JMT")]
        public byte? JudgmentStatute { get; private set; }

        [IgnoreDataMember]
        public string Display { get { return this.Abbreviation; } }


        public State(byte id, string abbrev, byte salesNo, bool weFile, byte statute, byte jmtStatute)
        {
            this.ID = id;
            this.Abbreviation = abbrev;
            this.SalesNo = salesNo;
            this.WeFile = weFile;
            this.Statute = statute;
            this.JudgmentStatute = jmtStatute;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
        public override bool Equals(object obj)
        {
            if (obj is State)
            {
                return this == (State)obj;
            }
            return false;
        }
        public static bool operator ==(State obj1, State obj2)
        {
            return obj1.ID == obj2.ID;
        }
        public static bool operator !=(State obj1, State obj2)
        {
            return obj1.ID != obj2.ID;
        }
    }
}
