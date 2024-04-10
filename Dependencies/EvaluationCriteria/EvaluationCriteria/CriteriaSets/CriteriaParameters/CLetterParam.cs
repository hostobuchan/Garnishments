using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CLetterParam : CDateParam
    {
        public int LetterType { get; set; }

        public CLetterParam()
        {
            this.LetterType = 0;
        }
        public CLetterParam(string DataName)
        {
            this.LetterType = 0;
            this.DataName = DataName;
        }

        internal override void Load(IDataReader dr, ICriteriaLoader loader)
        {
            base.Load(dr, loader);
            // this.LetterType = ?
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader)
        {
            await base.LoadAsync(dr, loader);
            // this.LetterType = ?
        }
        internal override void Save(DataRow dr)
        {
            base.Save(dr);
            //dr["VALUE3"] = this.LetterType.ToString();
        }
        internal override async Task SaveAsync(DataRow dr)
        {
            await base.SaveAsync(dr);
            //dr["VALUE3"] = this.LetterType.ToString();
        }

        public override bool Evaluate(object DateGiven, object LetterTypeGiven)
        {
            bool Result = base.Evaluate(DateGiven);
            if (Result)
            {
                if (LetterTypeGiven is int)
                {
                    if ((int)LetterTypeGiven == LetterType || LetterType == 0) return ISNOT;
                    else return !ISNOT;
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (LetterTypeGiven?.GetType().ToString() ?? "NULL") + "\"");
                }
            }
            else
            {
                return !ISNOT;
            }
        }
        public override Eval EvaluateVerbose(object DateGiven, object LetterTypeGiven, object BaseValueGiven = null)
        {
            Eval info = base.EvaluateVerbose(DateGiven);
            if (info.Success)
            {
                if (LetterTypeGiven is int)
                {
                    if ((int)LetterTypeGiven == LetterType || LetterType == 0)
                    {
                        return info;
                    }
                    else
                    {
                        return new Eval(!ISNOT, DataName + " Not Required Type");
                    }
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (LetterTypeGiven?.GetType().ToString() ?? "NULL") + "\"");
                }
            }
            else
            {
                return info;
            }
        }

        public override string GetDescription()
        {
            return base.GetDescription();
        }
    }
}
