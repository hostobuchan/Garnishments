using System;
using System.Data;
using System.Runtime.Serialization;

namespace EvaluationCriteria.Accounts
{
    [DataContract]
    public class DiaryCode
    {
        [DataMember(Name = "Code", Order = 0)]
        public int Code { get; private set; }
        [DataMember(Name = "Date", Order = 1)]
        public DateTime Date { get; private set; }

        public DiaryCode(DataRow dr)
        {
            this.Code = dr["CODE"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CODE"]);
            this.Date = dr["DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["DATE"]);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1:yyyy-MM-dd})", this.Code, this.Date);
        }
    }
}
