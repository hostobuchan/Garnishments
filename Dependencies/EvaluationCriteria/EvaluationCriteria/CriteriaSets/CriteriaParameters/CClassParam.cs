using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CClassParam : CStringValueParam
    {
        public CClassParam()
        {
            this.Value = "";
        }
        public CClassParam(string DataName)
        {
            this.Value = "";
            this.DataName = DataName;
        }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            try
            {
                string Value = "";
                if (ValueGiven is string) Value = (string)ValueGiven;
                else if (ValueGiven == null) Value = "";
                if (ValueGiven is string || ValueGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            return ISNOT;
                        case 1:
                            if (Value == this.Value) return ISNOT;
                            else return !ISNOT;
                        case 2:
                            if (Value != this.Value) return ISNOT;
                            else return !ISNOT;
                        default:
                            return true;
                    }
                }
                else
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success = true;
            string Result = "";

            try
            {
                string Value = "";
                if (ValueGiven is string) Value = (string)ValueGiven;
                else if (ValueGiven == null) Value = "";
                if (ValueGiven is string || ValueGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            Success = true;
                            break;
                        case 1:
                            if (Value == this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Equal To \"{this.Value}\" {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Not Equal To \"{this.Value}\" {{{ValueGiven}}}";
                                break;
                            }
                        case 2:
                            if (Value != this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Not Equal To \"{this.Value}\" {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Equal To \"{this.Value}\" {{{ValueGiven}}}";
                                break;
                            }
                        default:
                            Success = true;
                            break;
                    }
                }
                else
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }

            return new Eval(Success, DataName + Result);
        }

        public override string GetDescription()
        {
            switch (Param)
            {
                case 0:
                    return string.Format("{0} - Not Used", this.DataName);
                case 1:
                    return string.Format("{0} - Equal to \"{1}\"", this.DataName, this.Value);
                case 2:
                    return string.Format("{0} - Not Equal to \"{1}\"", this.DataName, this.Value);
                default:
                    return string.Format("{0} - Unknown Setting", this.DataName);
            }
        }
    }
}
