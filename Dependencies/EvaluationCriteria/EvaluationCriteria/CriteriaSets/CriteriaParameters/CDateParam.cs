using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CDateParam : CIntValuesParam
    {
        public CDateParam()
        {
            this.Value = 0;
            this.Value2 = 0;
        }
        public CDateParam(string DataName)
        {
            this.DataName = DataName;
            this.ISNOT = true;
            this.Value = 0;
            this.Value2 = 0;
        }

        public override bool Evaluate(object DateGiven, object ConstraintGiven = null)
        {
            try
            {
                if (DateGiven is DateTime || DateGiven is DateTime?)
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            return true;
                        case 1: //Exists
                            return ISNOT;
                        case 2: //Does Not Exist
                            return !ISNOT;
                        case 3: //Greater Than _Value Days Ago
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                            {
                                return ISNOT;
                            }
                            else
                            {
                                return !ISNOT;
                            }
                        case 4: //Less Than _Value Days Ago
                            if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                            {
                                return ISNOT;
                            }
                            else
                            {
                                return !ISNOT;
                            }
                        case 5: //Between _Value and _Value2 Days Ago
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value2))
                            {
                                return ISNOT;
                            }
                            else
                            {
                                return !ISNOT;
                            }
                        case 6: //Exists More Than _Value Days Ago
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                            {
                                return ISNOT;
                            }
                            else
                            {
                                return !ISNOT;
                            }
                        default:
                            return true;
                    }
                }
                else if (DateGiven is DBNull || DateGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            return ISNOT;
                        case 1:
                            return !ISNOT;
                        case 2:
                            return ISNOT;
                        case 3:
                            return ISNOT;
                        case 4:
                            return !ISNOT;
                        case 5:
                            return !ISNOT;
                        case 6:
                            return !ISNOT;
                        default:
                            return ISNOT;
                    }
                }
                else
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (DateGiven?.GetType().ToString() ?? "NULL") + "\"");
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }
        }
        public override Eval EvaluateVerbose(object DateGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success = true;
            string Result = "";

            try
            {
                if (DateGiven is DateTime || DateGiven is DateTime?)
                {
                    switch (Param)
                    {
                        case 0: //Do Not Use
                            Success = true;
                            break;
                        case 1: //Exists
                            Success = ISNOT;
                            Result = $" Exists {{{(DateTime)DateGiven:MM/dd/yyyy}}}";
                            break;
                        case 2: //Does Not Exist
                            Success = !ISNOT;
                            Result = $" Exists {{{(DateTime)DateGiven:MM/dd/yyyy}}}";
                            break;
                        case 3: //Greater Than _Value Days Ago or Null
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                            {
                                Success = ISNOT;
                                Result = $" More Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Less Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                        case 4: //Less Than _Value Days Ago
                            if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                            {
                                Success = ISNOT;
                                Result = $" Less Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" More Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                        case 5: //Between _Value and _Value2 Days Ago
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value2))
                            {
                                Success = ISNOT;
                                Result = $" Between {Value} and {Value2} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                                    Result = $" Less Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                else
                                    Result = $" More Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                        case 6: //Exists More Than _Value Days Ago
                            if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                            {
                                Success = ISNOT;
                                Result = $" More Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Less Than {Value} Days Ago ({(DateTime)DateGiven:MM/dd/yyyy})";
                                break;
                            }
                        default:
                            Success = true;
                            break;
                    }
                }
                else if (DateGiven is DBNull || DateGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            Success = ISNOT;
                            break;
                        case 1:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 2:
                            Success = ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 3:
                            Success = ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 4:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 5:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 6:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        default:
                            Success = ISNOT;
                            break;
                    }
                }
                else
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (DateGiven?.GetType().ToString() ?? "NULL") + "\"");

                return new Eval(Success, DataName + Result);
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }
        }

        public override string GetDescription()
        {
            switch (this.Param)
            {
                case 0: //Do Not Use
                    return string.Format("{0} - Not Used", this.DataName);
                case 1: //Exists
                    return string.Format("{0} - Exists", this.DataName);
                case 2: //Does Not Exist
                    return string.Format("{0} - Does Not Exist", this.DataName);
                case 3: //Greater Than _Value Days Ago
                    return string.Format("{0} - Greater Than {1} Days Ago", this.DataName, this.Value.ToString());
                case 4: //Less Than _Value Days Ago
                    return string.Format("{0} - Less Than {1} Days Ago", this.DataName, this.Value.ToString());
                case 5: //Between _Value and _Value2 Days Ago
                    return string.Format("{0} - Between {1} and {2} Days Ago", this.DataName, this.Value.ToString(), this.Value2.ToString());
                case 6: //Exists More Than _Value Days Ago
                    return string.Format("{0} - Exists More Than {1} Days Ago", this.DataName, this.Value.ToString());
                default:
                    return string.Format("{0} - Unknown Setting", this.DataName);
            }
        }
    }
}
