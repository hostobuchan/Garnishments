using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CStringParam : CStringValuesParam
    {
        public List<string> Values { get; set; }

        public CStringParam()
        {
            this.Values = new List<string>();
            this.Value = "";
            this.Value2 = "";
        }
        public CStringParam(string DataName)
        {
            this.Values = new List<string>();
            this.Value = "";
            this.Value2 = "";
            this.DataName = DataName;
        }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader)
        {
            base.Load(dr);
            this.Values = loader.GetStringsForParam(Convert.ToInt32(this.Value));
        }
        internal override async Task LoadAsync(IDataReader dr, Interfaces.ICriteriaLoader loader)
        {
            base.Load(dr);
            this.Values = await loader.GetStringsForParamAsync(Convert.ToInt32(this.Value));
        }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            if (ValueGiven == null)
            {
                return !ISNOT;
            }
            else if (ValueGiven is string)
            {
                if (this.Values.Contains(ValueGiven.ToString()))
                    return ISNOT;
                else
                    return !ISNOT;
            }
            else
            {
                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
            }
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success = false;
            string Result = "";

            if (ValueGiven == null)
            {
                Success = ISNOT;
                Result = " Not In List";
            }
            else if (ValueGiven is string)
            {
                if (this.Values.Contains(ValueGiven.ToString()))
                {
                    Success = ISNOT;
                    Result = " In List";
                }
                else
                {
                    Success = !ISNOT;
                    Result = " Not In List";
                }
            }
            else
            {
                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
            }

            return new Eval(Success, DataName + Result);
        }

        public override string GetDescription()
        {
            return string.Format("{0} - In List \"{1}\"", this.DataName, this.Value2);
        }
    }
}
