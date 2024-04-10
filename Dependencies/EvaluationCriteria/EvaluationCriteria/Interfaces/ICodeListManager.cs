using EvaluationCriteria.Enums;
using System.Collections.Generic;

namespace EvaluationCriteria.Interfaces
{
    public interface ICodeListManager
    {
        ICodeList FindCodeListById(int ID);
        void AddCodeList(ICodeList List);
        List<ICodeList> GetCodeLists();
        ICodeList NewCodeList(string Name, CodeListType ListType);
    }
}
