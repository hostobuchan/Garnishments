using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CEnumParam<T> : CParam where T : struct
    {
        public CEnumParam() { }
        public CEnumParam(string DataName) { this.DataName = DataName; }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            if (ISNOT)
                return ((T)ConstraintGiven).Equals((T)Enum.ToObject(typeof(T), this.Param));
            else
                return !((T)ConstraintGiven).Equals((T)Enum.ToObject(typeof(T), this.Param));
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success;
            if (ISNOT)
                Success = ((T)ConstraintGiven).Equals((T)Enum.ToObject(typeof(T), this.Param));
            else
                Success = !((T)ConstraintGiven).Equals((T)Enum.ToObject(typeof(T), this.Param));
            return new Eval(Success, string.Format("{0} - \"{1}\",", DataName, ConstraintGiven));
        }

        public override string GetDescription()
        {
            try
            {
                return string.Format("{0} - {1}", this.DataName, (T)Enum.ToObject(typeof(T), this.Param));
            }
            catch
            {
                return string.Format("{0} - {1}", this.DataName, (T)Enum.ToObject(typeof(T), 0));
            }
        }
    }
}
