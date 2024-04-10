using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data
{
    [DataContract(Name = "Modifier", Namespace = "")]
    public class Modifier : IComparable<Modifier>
    {
        [DataMember(Name = "ENVELOPE")]
        public Envelope Envelope { get; set; }
        [DataMember(Name = "WEIGHT")]
        public decimal Weight { get; set; }
        [DataMember(Name = "MODIFIER")]
        public Enums.CostModifier CostModifier { get; set; }
        [DataMember(Name = "AMOUNT")]
        public decimal Amount { get; set; }

        public Modifier() { }
        public Modifier(Envelope envelope)
        {
            this.Envelope = envelope;
        }
        public Modifier(Envelope envelope, IDataReader dr)
        {
            this.Envelope = envelope;
            this.Weight = Convert.ToDecimal(dr["WEIGHT"]);
            this.CostModifier = (Enums.CostModifier)Enum.ToObject(typeof(Enums.CostModifier), Convert.ToByte(dr["MODIFIER"]));
            this.Amount = Convert.ToDecimal(dr["AMOUNT"]);
        }

        public override string ToString()
        {
            return string.Format("{0}: @{1} {2} {3:C}",
                this.Envelope,
                this.Weight,
                Dictionaries.CostModifiers[this.CostModifier],
                this.Amount
                );
        }

        #region Equality Overrides
        public override bool Equals(object obj)
        {
            if (obj is Modifier)
                return (this == (obj as Modifier));
            else
                return false;
        }
        public static bool operator ==(Modifier m1, Modifier m2)
        {
            return (m1.Envelope?.ID == m2.Envelope?.ID && m1.Weight == m2.Weight);
        }
        public static bool operator !=(Modifier m1, Modifier m2)
        {
            return !(m1.Envelope?.ID == m2.Envelope?.ID && m1.Weight == m2.Weight);
        }
        #endregion

        #region IComparable<Modifier> Members

        public int CompareTo(Modifier other)
        {
            if (this.Envelope.ID > other.Envelope.ID) return 1;
            else if (this.Envelope.ID < other.Envelope.ID) return -1;
            else if (this.Weight > other.Weight) return 1;
            else if (this.Weight < other.Weight) return -1;
            else return 0;
        }

        #endregion
    }
}
