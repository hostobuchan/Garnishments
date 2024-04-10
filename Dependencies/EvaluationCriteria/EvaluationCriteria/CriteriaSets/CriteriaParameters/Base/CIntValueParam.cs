using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters.Base
{
    [Serializable]
    public abstract class CIntValueParam : CParam
    {
        public int Value { get; set; }

        public CIntValueParam() : base() { }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            base.Load(dr);
            this.Value = dr["VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VALUE"]);
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader = null)
        {
            await base.LoadAsync(dr, loader);
            this.Value = dr["VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VALUE"]);
        }
        internal override void Save(DataRow dr)
        {
            base.Save(dr);
            dr["VALUE"] = this.Value.ToString();
        }
        internal override async Task SaveAsync(DataRow dr)
        {
            await base.SaveAsync(dr);
            dr["VALUE"] = this.Value.ToString();
        }
    }

    [Serializable]
    public abstract class CIntValuesParam : CIntValueParam
    {
        public int Value2 { get; set; }

        public CIntValuesParam() : base() { }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            base.Load(dr);
            this.Value2 = dr["VALUE2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VALUE2"]);
        }
        internal override void Save(DataRow dr)
        {
            base.Save(dr);
            dr["VALUE2"] = this.Value2.ToString();
        }
    }
}
