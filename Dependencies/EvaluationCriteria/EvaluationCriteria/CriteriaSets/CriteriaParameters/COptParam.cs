using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using EvaluationCriteria.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class COptParam : CStringValuesParam
    {
        public virtual List<int> Values { get; set; }

        public COptParam()
        {
            this.Values = new List<int>();
            this.Value = "";
            this.Value2 = "";
        }
        public COptParam(string DataName)
        {
            this.Values = new List<int>();
            this.Value = "";
            this.Value2 = "";
            this.DataName = DataName;
        }

        internal override void Load(IDataReader dr, ICriteriaLoader loader = null)
        {
            base.Load(dr, loader);
            this.Values = loader.GetCodesForParam(Convert.ToInt32(this.Value));
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader = null)
        {
            await base.LoadAsync(dr, loader);
            this.Values = await loader.GetCodesForParamAsync(Convert.ToInt32(this.Value));
        }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            int Value = 0;
            if (ValueGiven == null)
            {
                switch (Param)
                {
                    case 0: // Do Not Use
                        return ISNOT;
                    case 1: // Does Not Exist (value==0)
                        return ISNOT;
                    case 4: // Not In List
                        return ISNOT;
                    default:
                        return !ISNOT;
                }
            }
            else if (ValueGiven is int || ValueGiven is double || ValueGiven is decimal)
            {
                if (ValueGiven is int) Value = (int)ValueGiven;
                else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                switch (Param)
                {
                    case 0: // Do Not Use
                        return ISNOT;
                    case 1: // Does Not Exist (value==0)
                        if (Value == 0) return ISNOT;
                        else return !ISNOT;
                    case 2: // Exists
                        if (Value > 0) return ISNOT;
                        else return !ISNOT;
                    case 3: // In List
                        foreach (int x in Values)
                        {
                            if (x == Value) return ISNOT;
                        }
                        return !ISNOT;
                    case 4: // Not In List
                        foreach (int x in Values)
                        {
                            if (x == Value) return !ISNOT;
                        }
                        return ISNOT;
                    default:
                        return true;
                }
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

            int Value = 0;
            if (ValueGiven is int) Value = (int)ValueGiven;
            else if (ValueGiven is double) Value = (int)(double)ValueGiven;
            else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
            if (ValueGiven is int || ValueGiven is double || ValueGiven is decimal)
            {
                switch (Param)
                {
                    case 0: // Do Not Use
                        Success = ISNOT;
                        break;
                    case 1: // Does Not Exist (value==0)
                        if (Value == 0)
                        {
                            Success = ISNOT;
                            Result = " Does Not Exist";
                        }
                        else
                        {
                            Success = !ISNOT;
                            Result = $" Exists ({(BaseValueGiven == null ? Value : BaseValueGiven)})";
                        }
                        break;
                    case 2: // Exists
                        if (Value > 0)
                        {
                            Success = ISNOT;
                            Result = $" Exists ({(BaseValueGiven == null ? Value : BaseValueGiven)})";
                        }
                        else
                        {
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                        }
                        break;
                    case 3: // In List
                        foreach (int x in Values)
                        {
                            if (x == Value)
                            {
                                Success = ISNOT;
                                Result = $" In List ({Value})";
                                break;
                            }
                        }
                        if (Result == "")
                        {
                            Success = !ISNOT;
                            Result = " Not In List";
                        }
                        break;
                    case 4: // Not In List
                        if (Value == 0)
                        {
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        }
                        foreach (int x in Values)
                        {
                            if (x == Value)
                            {
                                Success = !ISNOT;
                                Result = $" In List ({Value})";
                                break;
                            }
                        }
                        if (Result == "")
                        {
                            Success = ISNOT;
                            Result = " Not In List";
                        }
                        break;
                }
            }
            else if (ValueGiven is DBNull)
            {
                switch (Param)
                {
                    case 0: // Do Not Use
                        Success = ISNOT;
                        break;
                    case 1: // Does Not Exist (value==0)
                        Success = ISNOT;
                        Result = " Does Not Exist";
                        break;
                    case 4:
                        Success = !ISNOT;
                        Result = " Does Not Exist";
                        break;
                    default:
                        Success = !ISNOT;
                        Result = " Unknown Parameter";
                        break;
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
            switch (Param)
            {
                case 0: // Do Not Use
                    return string.Format("{0} - Not Used", this.DataName);
                case 1: // Does Not Exist (value==0)
                    return string.Format("{0} - Does Not Exist", this.DataName);
                case 2: // Exists
                    return string.Format("{0} - Exists", this.DataName);
                case 3: // In List
                    return string.Format("{0} - In List \"{1}\"", this.DataName, this.Value2);
                case 4: // Not In List
                    return string.Format("{0} - Not In List \"{1}\"", this.DataName, this.Value2);
                default:
                    return string.Format("{0} - Unknown Setting", this.DataName);
            }
        }
    }
}
