using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.Enums;
using System.Collections.Generic;

namespace EvaluationCriteria.Interfaces
{
    public interface ICodeList
    {
        int? GetID();
        string GetName();
        CodeListType GetListType();
        CodeValue GetCodes();
        List<string> GetCodeStrings();
        void Commit();
        void SaveCodes();
    }
}
