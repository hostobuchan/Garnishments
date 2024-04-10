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
    [DataContract(Name = "Routine")]
    public class Routine : IComparable<Routine>
    {
        public event System.Windows.Forms.Delegates.EventArgs.TextMultiProgressUpdatedEventHandler ProgressUpdated;
        protected void OnProgressUpdated(int Progress, string Description, string SubDescription, int Section, int TotalSections) { this.ProgressUpdated?.Invoke(Progress, Description, SubDescription, Section, TotalSections); }

        [DataMember(Name = "Manager")]
        protected CriteriaDataHandler Manager { get; set; }

        [DataMember(Name = "SALES")]
        public byte SalesNo { get; private set; }
        [DataMember(Name = "ATID")]
        public Enums.AssetType AssetType { get; private set; }
        [DataMember(Name = "STATUS")]
        public Enums.Status Status { get; private set; }
        [DataMember(Name = "EVALS")]
        public List<int> EvaluationSetIDs { get; private set; } = new List<int>();
        [IgnoreDataMember]
        public IEnumerable<EvaluationSet> EvaluationSets { get { return this.Manager.EvaluationSets.Where(el => this.EvaluationSetIDs.Contains(el.ID)); } }

        public Routine(CriteriaDataHandler manager, byte salesNo, Enums.AssetType assetType, Enums.Status status)
        {
            this.Manager = manager;
            this.SalesNo = salesNo;
            this.AssetType = assetType;
            this.Status = status;
        }

        public void LoadData()
        {
            this.EvaluationSetIDs = new List<int>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT EVALID FROM {Settings.Properties.SQLRoutines} WHERE [SALES]=@SALES AND [ATID]=@ATID AND [STATUS]=@STATUS", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@SALES", SqlDbType.TinyInt) { Value = this.SalesNo });
                    cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.TinyInt) { Value = Convert.ToByte(this.AssetType) });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.TinyInt) { Value = Convert.ToByte(this.AssetType) });

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.EvaluationSetIDs.Add(Convert.ToInt32(sdr["EVALID"]));
                    }
                    sdr.Close();
                }
            }
        }
        public async Task LoadDataAsync()
        {
            this.EvaluationSetIDs = new List<int>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT EVALID FROM {Settings.Properties.SQLRoutines} WHERE [SALES]=@SALES AND [ATID]=@ATID AND [STATUS]=@STATUS", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@SALES", SqlDbType.TinyInt) { Value = this.SalesNo });
                    cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.TinyInt) { Value = Convert.ToByte(this.AssetType) });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.TinyInt) { Value = Convert.ToByte(this.Status) });

                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        this.EvaluationSetIDs.Add(Convert.ToInt32(sdr["EVALID"]));
                    }
                    sdr.Close();
                }
            }
        }

        public void AddEvaluationSet(string description)
        {
            this.Manager.AddRoutineEvaluation(this, description);
        }
        public void RemoveEvaluationSet(EvaluationSet set)
        {
            this.Manager.RemoveRoutineEvaluation(set);
        }
        

        public override string ToString()
        {
            return $"Sales {this.SalesNo} - {this.AssetType} - {this.Status}";
        }

        public int CompareTo(Routine other)
        {
            throw new NotImplementedException();
        }
    }
}
