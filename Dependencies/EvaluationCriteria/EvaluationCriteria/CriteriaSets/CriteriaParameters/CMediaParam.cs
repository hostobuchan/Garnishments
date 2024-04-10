using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CMediaParam : CParam
    {
        public CMediaParam() { }
        public CMediaParam(string DataName) { this.DataName = DataName; }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            if (ISNOT)
                return (Evaluatees2.MediaStatus.Status)ConstraintGiven == (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
            else
                return (Evaluatees2.MediaStatus.Status)ConstraintGiven != (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success;
            if (ISNOT)
                Success = (Evaluatees2.MediaStatus.Status)ConstraintGiven == (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
            else
                Success = (Evaluatees2.MediaStatus.Status)ConstraintGiven != (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
            return new Eval(Success, string.Format("{0} Media Status \"{1}\",", DataName, ConstraintGiven));
        }

        public override string GetDescription()
        {
            return string.Format("{0} - {1}", this.DataName, (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param));
        }
    }
}
