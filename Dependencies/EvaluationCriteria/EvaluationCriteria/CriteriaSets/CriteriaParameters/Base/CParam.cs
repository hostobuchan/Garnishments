using System;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters.Base
{
    [Serializable]
    public abstract class CParam
    {
        public int Param { get; set; }
        public bool ISNOT { get; set; }
        public string DataName { get; set; }

        public CParam()
        {
            this.Param = 0;
            this.ISNOT = true;
            this.DataName = "";
        }

        internal virtual void Load(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            this.Param = Convert.ToByte(dr["COMPARE"]);
            this.ISNOT = Convert.ToBoolean(dr["ISNOT"]);
        }
        internal virtual async Task LoadAsync(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            await Task.Run(() =>
            {
                this.Param = Convert.ToByte(dr["COMPARE"]);
                this.ISNOT = Convert.ToBoolean(dr["ISNOT"]);
            });
        }
        internal virtual void Save(DataRow dr)
        {
            dr["COMPARE"] = this.Param;
            dr["ISNOT"] = this.ISNOT;
        }
        internal virtual async Task SaveAsync(DataRow dr)
        {
            dr["COMPARE"] = this.Param;
            dr["ISNOT"] = this.ISNOT;
        }

        public abstract bool Evaluate(object ValueGiven, object ConstraintGiven = null);
        public abstract Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null);
        public override string ToString()
        {
            return GetDescription();
        }
        public abstract string GetDescription();
    }

    public class Eval
    {
        public bool Success { get; private set; }
        public string Info { get; private set; }

        public Eval(bool Success, string Info)
        {
            this.Success = Success;
            this.Info = Info;
        }
    }
}
