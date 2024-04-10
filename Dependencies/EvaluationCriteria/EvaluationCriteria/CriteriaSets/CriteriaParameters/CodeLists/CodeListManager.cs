using EvaluationCriteria.Enums;
using EvaluationCriteria.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters.CodeLists
{
    public class CodeListManager : ICodeListManager
    {
        public List<CodeList> CodeLists { get; private set; }

        public CodeListManager()
        {
            this.CodeLists = new List<CodeList>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {Settings.Properties.SQLCodeLists}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.CodeLists.Add(new CodeList(sdr));
                    }
                }
            }
        }

        public CodeList FindCodeListById(int CID)
        {
            try
            {
                return this.CodeLists.Find(el => el.ID.HasValue && el.ID.Value == CID);
            }
            catch
            {
                return null;
            }
        }

        public void AddCodeList(CodeList List)
        {
            if (List.ID == null)
            {
                List.Commit();
            }
            if (this.CodeLists.FindAll(el => el.ID.Value == List.ID.Value).Count == 0)
            {
                this.CodeLists.Add(List);
            }
        }

        #region ICodeListManager Interface

        public List<ICodeList> GetCodeLists()
        {
            return this.CodeLists.OfType<ICodeList>().ToList();
        }

        public ICodeList NewCodeList(string Name, CodeListType ListType)
        {
            var newList = new CodeList(Name, ListType);
            AddCodeList(newList);
            return newList;
        }

        ICodeList ICodeListManager.FindCodeListById(int ID)
        {
            return this.FindCodeListById(ID);
        }

        public void AddCodeList(ICodeList List)
        {
            this.AddCodeList(List as CodeList);
        }

        #endregion
    }
}
