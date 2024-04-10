using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CSimpleParam : CParam
    {
        public CSimpleParam() { }
        public CSimpleParam(string DataName)
        {
            this.DataName = DataName;
        }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            try
            {
                string Value = "";
                if (ValueGiven is string) Value = (string)ValueGiven;
                if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                if (Value != "" && ValueGiven != null)
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            return true;
                        case 1: //Exists / True
                            return ISNOT;
                        case 2: //Does Not Exist / False
                            return !ISNOT;
                        default:
                            return !ISNOT;
                    }
                }
                else
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            return true;
                        case 1: //Exists / True
                            return !ISNOT;
                        case 2: //Does Not Exist / False
                            return ISNOT;
                        default:
                            return !ISNOT;
                    }
                }
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object baseValue = null)
        {
            bool Success = true;
            string Result = "";

            try
            {
                string Value = "";
                string DerivedValue = "";
                if (ValueGiven is string) Value = (string)ValueGiven;
                if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                if (baseValue != null)
                {
                    if (baseValue is string) DerivedValue = (string)baseValue;
                    if (baseValue is DateTime) DerivedValue = $"{baseValue:M/d/yyyy}";
                    else DerivedValue = baseValue.ToString();
                }
                if (Value != "" && ValueGiven != null)
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            Success = true;
                            break;
                        case 1: //Exists
                            Success = ISNOT;
                            Result = baseValue == null ? " Exists" : $" Exists ({DerivedValue})";
                            break;
                        case 2: //Does Not Exist
                            Success = !ISNOT;
                            Result = baseValue == null ? " Exists" : $" Exists ({DerivedValue})";
                            break;
                        default:
                            Success = !ISNOT;
                            break;
                    }
                }
                else
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            Success = true;
                            break;
                        case 1: //Exists
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 2: //Does Not Exist
                            Success = ISNOT;
                            Result = " Does Not Exist";
                            break;
                        default:
                            Success = !ISNOT;
                            break;
                    }
                }
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }

            return new Eval(Success, DataName + Result);
        }

        public override string GetDescription()
        {
            switch (Param)
            {
                case 0: //Do Not Use
                    return string.Format("{0} - Not Used", this.DataName);
                case 1: //Exists
                    return string.Format("{0} - Exists", this.DataName);
                case 2: //Does Not Exist
                    return string.Format("{0} - Does Not Exist", this.DataName);
                default:
                    return string.Format("{0} - Unknown Setting", this.DataName);
            }
        }
    }
}
