using EvaluationCriteria.Accounts;
using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    [Serializable]
    public class CBoolParam : CStringValuesParam
    {
        public CodeValue Elements { get; set; }

        public CBoolParam()
        {
            this.Value = "";
            this.Value2 = "";
            this.Elements = new CodeValue();
        }
        public CBoolParam(string DataName)
        {
            this.Value = "";
            this.Value2 = "";
            this.Elements = new CodeValue();
            this.DataName = DataName;
        }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader)
        {
            base.Load(dr);
            this.Elements = loader.GetCodeValuesForParam(Convert.ToInt32(this.Value));
        }
        internal override async Task LoadAsync(IDataReader dr, Interfaces.ICriteriaLoader loader)
        {
            await base.LoadAsync(dr);
            this.Elements = await loader.GetCodeValuesForParamAsync(Convert.ToInt32(this.Value));
        }

        public override bool Evaluate(object ValueGiven, object DateGiven)
        {
            if (ValueGiven is List<DiaryCode>)
            {
                foreach (DiaryCode D in (List<DiaryCode>)ValueGiven)
                {
                    if (Elements.Values.Select(el => el.Code).Contains(D.Code))
                    {
                        if (this.Evaluate(D.Code, D.Date)) return ISNOT;
                    }
                }
                return !ISNOT;
            }
            else
            {
                try
                {
                    int Value = 0;
                    if (ValueGiven is int) Value = (int)ValueGiven;
                    else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                    else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                    else if (ValueGiven is string)
                    {
                        if (!int.TryParse(NumberCleanup((string)ValueGiven, typeof(int)), out Value))
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected String Value \"" + (string)ValueGiven + "\"");
                    }
                    if (ValueGiven is double || ValueGiven is decimal || ValueGiven is int || ValueGiven is string)
                    {
                        if (DateGiven is DateTime)
                        {
                            bool Found = false;
                            bool NotFound = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                switch (Elements[i].Param)
                                {
                                    case 0: //Exists
                                        if (Value == Elements[i].Code) Found = true;
                                        break;
                                    case 1: //In The Past
                                        if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) Found = true;
                                        break;
                                    case 2: //In The Future
                                        if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) Found = true;
                                        break;
                                    case 3: //Over Days Away
                                        if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) Found = true;
                                        break;
                                    case 4: //Under Days Away
                                        if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) Found = true;
                                        break;
                                    case 5: //Over Days Ago
                                        if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) Found = true;
                                        break;
                                    case 6: //Under Days Ago
                                        if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) Found = true;
                                        break;
                                    case 7: // Doesn't Exist
                                        if (Value == Elements[i].Code) NotFound = true;
                                        else Found = true;
                                        break;
                                }
                            }
                            if (Found && !NotFound) return ISNOT;
                            else return !ISNOT;
                        }
                        else if (DateGiven is DBNull || DateGiven == null)
                        {
                            bool Found = false;
                            bool NotFound = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                switch (Elements[i].Param)
                                {
                                    case 0: //Exists
                                        if (Value == Elements[i].Code) Found = true;
                                        break;
                                    case 7: //Doesn't Exist
                                        if (Value == Elements[i].Code) NotFound = true;
                                        else Found = true;
                                        break;
                                }
                            }
                            if (Found && !NotFound) return ISNOT;
                            else return !ISNOT;
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
        }
        public override Eval EvaluateVerbose(object ValueGiven, object DateGiven, object BaseValueGiven = null)
        {
            if (ValueGiven is List<DiaryCode>)
            {
                if (ISNOT)
                {
                    foreach (DiaryCode D in (List<DiaryCode>)ValueGiven)
                    {
                        if (this.Elements.Values.Select(el => el.Code).Contains(D.Code))
                        {
                            Eval Res = this.EvaluateVerbose(D.Code, D.Date);
                            if (Res.Success) return new Eval(true, Res.Info);
                        }
                    }
                    return new Eval(false, "No Required Diaries Found");
                }
                else
                {
                    bool Success = true;
                    StringBuilder Result = new StringBuilder(" Diaries Found (");
                    foreach (DiaryCode D in (List<DiaryCode>)ValueGiven)
                    {
                        if (this.Elements.Values.Select(el => el.Code).Contains(D.Code))
                        {
                            Eval Res = this.EvaluateVerbose(D.Code, D.Date);
                            if (!Res.Success)
                            {
                                Success = false;
                                Result.Append(D.Code.ToString() + ",");
                            }
                        }
                    }
                    if (Success)
                        return new Eval(true, "No Exclusionary Diaries Found");
                    else
                    {
                        return new Eval(false, Result.Length > 0 ? Result.ToString().Substring(0, Result.Length - 1) + ")" : "");
                    }
                }
            }
            else
            {
                bool Success = false;
                string Result = "";

                try
                {
                    int Value = 0;
                    if (ValueGiven is int) Value = (int)ValueGiven;
                    else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                    else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                    else if (ValueGiven is string)
                    {
                        if (!int.TryParse(NumberCleanup((string)ValueGiven, typeof(int)), out Value))
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected String Value \"" + (string)ValueGiven + "\"");
                    }
                    if (ValueGiven is double || ValueGiven is decimal || ValueGiven is int || ValueGiven is string)
                    {
                        if (DateGiven is DateTime)
                        {
                            bool Found = false;
                            bool NotFound = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                switch (Elements[i].Param)
                                {
                                    case 0: //Exists
                                        if (Value == Elements[i].Code) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                        break;
                                    case 1: //In The Past
                                        if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Past"; }
                                        break;
                                    case 2: //In The Future
                                        if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Future"; }
                                        break;
                                    case 3: //Over Days Away
                                        if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Away"; }
                                        break;
                                    case 4: //Under Days Away
                                        if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Away"; }
                                        break;
                                    case 5: //Over Days Ago
                                        if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                        break;
                                    case 6: //Under Days Ago
                                        if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                        break;
                                    case 7: // Doesn't Exist
                                        if (Value == Elements[i].Code) { NotFound = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                        else Found = true;
                                        break;
                                }
                            }
                            if (Found && !NotFound) Success = ISNOT;
                            else Success = !ISNOT;
                        }
                        else if (DateGiven is DBNull || DateGiven == null)
                        {
                            bool Found = false;
                            bool NotFound = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                switch (Elements[i].Param)
                                {
                                    case 0: //Exists
                                        if (Value == Elements[i].Code) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                        break;
                                    case 7: //Doesn't Exist
                                        if (Value == Elements[i].Code) { NotFound = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                        else Found = true;
                                        break;
                                    default:
                                        Success = false;
                                        Result = " No Date Existed";
                                        break;
                                }
                            }
                            if (Found && !NotFound) Success = ISNOT;
                            else Success = !ISNOT;
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + (ValueGiven?.GetType().ToString() ?? "NULL") + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                if (Success != ISNOT) Result = " Not Found";
                return new Eval(Success, DataName + Result);
            }
        }

        public override string GetDescription()
        {
            return string.Format("{0} List \"{1}\"", this.DataName, this.Value2);
        }
    }

    [Serializable]
    public class CodeValue
    {
        public List<CodeElements> Values { get; private set; }
        public CodeElements this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }
        public int Count
        {
            get { return Values.Count; }
        }
        public CodeValue()
        {
            this.Values = new List<CodeElements>();
        }
        public void Add(int Code, int Param, int Val)
        {
            if (this.Values.Where(el => el.Code == Code).Count() == 0)
            {
                CodeElements temp = new CodeElements()
                {
                    Code = Code,
                    Param = Param,
                    Value = Val
                };
                Values.Add(temp);
            }
        }
        public void Remove(CodeElements CodeElements)
        {
            this.Values.Remove(CodeElements);
        }
    }

    [Serializable]
    public class CodeElements
    {
        string[] Constraints = {"Exists",
                    "In The Past",
                    "In The Future",
                    "Over X Days Away",
                    "Under X Days Away",
                    "Over X Days Ago",
                    "Under X Days Ago",
                    "Doesn't Exist"};

        public int Code;
        public int Param;
        public int Value;

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Code, this.Constraints[Param].Replace("X", this.Value.ToString()));
        }
    }
}
