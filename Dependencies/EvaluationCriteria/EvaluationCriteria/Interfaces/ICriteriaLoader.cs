using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvaluationCriteria.Interfaces
{
    public interface ICriteriaLoader
    {
        CriteriaSets.CriteriaParameters.CodeValue GetCodeValuesForParam(int id);
        Task<CriteriaSets.CriteriaParameters.CodeValue> GetCodeValuesForParamAsync(int id);
        List<int> GetCodesForParam(int id);
        Task<List<int>> GetCodesForParamAsync(int id);
        List<string> GetStringsForParam(int id);
        Task<List<string>> GetStringsForParamAsync(int id);
        List<object> GetObjValues(Type type);
    }
}
