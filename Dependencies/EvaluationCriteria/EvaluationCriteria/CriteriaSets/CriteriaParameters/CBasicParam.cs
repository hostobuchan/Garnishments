using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CBasicParam : CIntValueParam
    {
        public CBasicParam()
        {
            this.Value = 0;
        }
        public CBasicParam(string DataName)
        {
            this.Value = 0;
            this.DataName = DataName;
        }

        public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
        {
            double Value = 0;
            try
            {
                if (ValueGiven is DBNull || ValueGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            return true;
                        case 6:
                            return ISNOT;
                        default:
                            return !ISNOT;
                    }
                }
                else if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                {
                    if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                    else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                    else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                    else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                    switch (Param)
                    {
                        case 0: //Don't Care
                            return true;
                        case 1: //Equal
                            if (Value == this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        case 2: //Not Equal
                            if (Value != this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        case 3: //More Than
                            if (Value > this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        case 4: //Less Than
                            if (Value < this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        case 5: //Exists
                            return ISNOT;
                        case 6: //Does Not Exist
                            return !ISNOT;
                        case 7: //More Than or Equal To
                            if (Value >= this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        case 8: //Less Than or Equal To
                            if (Value <= this.Value) { return ISNOT; }
                            else { return !ISNOT; }
                        default:
                            return true;
                    }
                }
                else
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
            }
            catch (Exception ex) { throw new NotImplementedException(ex.Message); }
        }
        public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null, object BaseValueGiven = null)
        {
            bool Success = true;
            string Result = "";
            double Value = 0;
            try
            {
                if (ValueGiven is DBNull || ValueGiven == null)
                {
                    switch (Param)
                    {
                        case 0:
                            Success = true;
                            break;
                        case 6:
                            Success = ISNOT;
                            Result = " Does Not Exist";
                            break;
                        default:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                    }
                }
                else if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                {
                    if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                    else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                    else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                    else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                    switch (Param)
                    {
                        case 0: //Don't Care
                            Success = true;
                            break;
                        case 1: //Equal
                            if (Value == this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Not Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                        case 2: //Not Equal
                            if (Value != this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Not Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                        case 3: //More Than
                            if (Value > this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Greater Than {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Less Than {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                        case 4: //Less Than
                            if (Value < this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Less Than {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Greater Than {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                        case 5: //Exists
                            Success = ISNOT;
                            Result = $" Exists {{{ValueGiven}}}";
                            break;
                        case 6: //Does Not Exist
                            Success = !ISNOT;
                            Result = $" Exists {{{ValueGiven}}}";
                            break;
                        case 7: //More Than or Equal To
                            if (Value >= this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Greater Than or Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Less Than {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                        case 8: //Less Than or Equal To
                            if (Value <= this.Value)
                            {
                                Success = ISNOT;
                                Result = $" Less Than or Equal To {this.Value} {{{ValueGiven}}}";
                                break;
                            }
                            else
                            {
                                Success = !ISNOT;
                                Result = $" Greater Than {this.Value} {{{ValueGiven}}}";
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
                case 0: //Don't Care
                    return string.Format("{0} - Not Used", this.DataName);
                case 1: //Equal
                    return string.Format("{0} - Equals {1}", this.DataName, this.Value.ToString());
                case 2: //Not Equal
                    return string.Format("{0} - Does Not Equal {1}", this.DataName, this.Value.ToString());
                case 3: //More Than
                    return string.Format("{0} - More Than {1}", this.DataName, this.Value.ToString());
                case 4: //Less Than
                    return string.Format("{0} - Less Than {1}", this.DataName, this.Value.ToString());
                case 5: //Exists
                    return string.Format("{0} - Exists", this.DataName);
                case 6: //Does Not Exist
                    return string.Format("{0} - Does Not Exist", this.DataName);
                case 7: //More Than or Equal To
                    return string.Format("{0} - More Than or Equal to {1}", this.DataName, this.Value.ToString());
                case 8: //Less Than or Equal To
                    return string.Format("{0} - Less Than or Equal to {1}", this.DataName, this.Value.ToString());
                default:
                    return string.Format("{0} - Unknown Setting", this.DataName);
            }
        }
    }
}
