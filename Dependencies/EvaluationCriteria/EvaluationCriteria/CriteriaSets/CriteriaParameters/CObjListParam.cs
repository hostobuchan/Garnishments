using EvaluationCriteria.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.CriteriaParameters
{
    public class CObjListParam<T> : COptParam where T : Interfaces.ICriteriaParam
    {
        public List<T> AllValues { get; private set; } = new List<T>();
        public List<T> ObjValues { get { return this.AllValues.Where(t => this.Values.Contains(t.ID)).ToList(); } set { this.Values = value.Select(t => t.ID).ToList(); } }

        public CObjListParam() : base() { }
        public CObjListParam(string dataName) : base(dataName) { }

        internal override void Load(IDataReader dr, ICriteriaLoader loader = null)
        {
            base.Load(dr, loader);
            this.Values = loader.GetCodesForParam(Convert.ToInt32(this.Value));
            this.AllValues = loader.GetObjValues(typeof(T)) as List<T>;
        }
        internal override async Task LoadAsync(IDataReader dr, ICriteriaLoader loader = null)
        {
            await base.LoadAsync(dr, loader);
            this.Values = await loader.GetCodesForParamAsync(Convert.ToInt32(this.Value));
            this.AllValues = loader.GetObjValues(typeof(T)) as List<T>;
        }
    }
}
