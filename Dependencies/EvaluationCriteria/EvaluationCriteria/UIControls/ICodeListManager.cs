using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationCriteria.CriteriaSets.Controls
{
    public interface ICodeListManager
    {
        ICodeList FindCodeListById(int ID);
        void AddCodeList(ICodeList List);
        List<ICodeList> GetCodeLists();
        ICodeList NewCodeList(string Name, CodeListType ListType);
    }

    public interface ICodeList
    {
        int? GetID();
        string GetName();
        CodeListType GetListType();
        Criteria.CBoolParam.CodeValue GetCodes();
        List<string> GetCodeStrings();
        void Commit();
        void SaveCodes();
    }

    public enum CodeListType
    {
        Numeric,
        Text
    }
}
