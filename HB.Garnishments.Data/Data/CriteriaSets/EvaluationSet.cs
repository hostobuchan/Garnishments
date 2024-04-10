using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CriteriaSets
{
    [DataContract(Name = "EvalSet")]
    public class EvaluationSet : IComparable<EvaluationSet>
    {
        public event Action<int> ProgressUpdated;
        protected void UpdateProgress(int progress) { ProgressUpdated?.Invoke(progress); }

        [DataMember(Name = "Manager")]
        private CriteriaDataHandler Manager { get; set; }
        [DataMember(Name = "ID")]
        public int ID { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; protected internal set; }
        [IgnoreDataMember]
        public EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSetDataHandler<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> SimpleSetManager { get { return this.Manager.SimpleSetManager; } }
        [DataMember(Name = "SETS")]
        public List<int> SimpleSetIDs { get; private set; } = new List<int>();
        [IgnoreDataMember]
        public List<EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor>> SimpleSets { get { return this.SimpleSetManager.SimpleSets.FindAll(el => this.SimpleSetIDs.Contains(el.ID)); } }

        public EvaluationSet(CriteriaDataHandler manager, int id, string description)
        {
            this.Manager = manager;
            this.ID = id;
            this.Description = description;
        }
        internal EvaluationSet(CriteriaDataHandler manager, IDataReader dr) : this(manager, Convert.ToInt32(dr["EVALID"]), $"{dr["DESCRIPTION"] ?? ""}") { }

        public void Load()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [SET] FROM {Settings.Properties.SQLEvalSets} WHERE EVALID=@EVALID", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = this.ID });
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.SimpleSetIDs.Add(Convert.ToInt32(sdr["SET"]));
                    }
                    sdr.Close();
                }
            }
        }
        public async Task LoadAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [SET] FROM {Settings.Properties.SQLEvalSets} WHERE EVALID=@EVALID", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = this.ID });

                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        this.SimpleSetIDs.Add(Convert.ToInt32(sdr["SET"]));
                    }
                    sdr.Close();
                }
            }
        }

        public void AddCriteriaSet(EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> set)
        {
            this.Manager.AddEvaluationSet(this, set);
        }
        public void RemoveCriteriaSet(EvaluationCriteria.CriteriaSets.SimpleSets.SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> set)
        {
            this.Manager.RemoveEvaluationSet(this, set);
        }

        

        public override string ToString()
        {
            return $"{this.Description}";
        }

        public int CompareTo(EvaluationSet other)
        {
            return string.Compare(this.Description, other.Description, StringComparison.OrdinalIgnoreCase);
        }
    }
}
