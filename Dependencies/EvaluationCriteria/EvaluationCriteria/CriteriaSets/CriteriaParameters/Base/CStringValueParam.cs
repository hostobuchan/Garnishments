using EvaluationCriteria.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters.Base
{
    [Serializable]
    public abstract class CStringValueParam : CParam
    {
        public string Value { get; set; }

        public CStringValueParam() : base() { }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            base.Load(dr);
            this.Value = $"{dr["VALUE"] ?? ""}";
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader = null)
        {
            await base.LoadAsync(dr, loader);
            this.Value = $"{dr["VALUE"] ?? ""}";
        }
        internal override void Save(DataRow dr)
        {
            base.Save(dr);
            dr["VALUE"] = this.Value;
        }
        internal override async Task SaveAsync(DataRow dr)
        {
            await base.SaveAsync(dr);
            dr["VALUE"] = this.Value;
        }
    }

    [Serializable]
    public abstract class CStringValuesParam : CStringValueParam
    {
        public string Value2 { get; set; }

        public CStringValuesParam() : base() { }

        internal override void Load(IDataReader dr, Interfaces.ICriteriaLoader loader = null)
        {
            base.Load(dr);
            this.Value2 = $"{dr["VALUE2"] ?? ""}";
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader = null)
        {
            await base.LoadAsync(dr, loader);
            this.Value2 = $"{dr["VALUE2"] ?? ""}";
        }
        internal override void Save(DataRow dr)
        {
            base.Save(dr);
            dr["VALUE2"] = this.Value2;
        }
    }
}
