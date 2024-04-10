using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.SimpleSets
{
    public class SimpleSet<T, K> : IComparable<SimpleSet<T, K>> where T : Accounts.EvaluateeAccount<K> where K : Accounts.EvaluateeDebtor
    {
        private string _Name { get; set; }
        public event Func<Type, List<object>> GetAllObjValues;

        public int ID { get; private set; }
        public string Name { get { return _Name; } set { UpdateName(value); } }
        public Criteria<T, K> Criteria { get; private set; }

        protected internal SimpleSet(Dictionary<string, object> SQLDistinguisher, int id, string name)
        {
            this.ID = id;
            this._Name = name;

            var newDistinguisher = new Dictionary<string, object>(SQLDistinguisher);
            newDistinguisher.Add("SET", this.ID);
            this.Criteria = new Criteria<T, K>(newDistinguisher);
            this.Criteria.GetAllObjValues += this.GetAllObjValues;
        }
        internal SimpleSet(Dictionary<string, object> SQLDistinguisher, IDataReader dr)
        {
            this.ID = Convert.ToInt32(dr["ID"]);
            this._Name = dr["NAME"].ToString();

            var newDistinguisher = new Dictionary<string, object>(SQLDistinguisher);
            newDistinguisher.Add("SET", this.ID);
            this.Criteria = new Criteria<T, K>(newDistinguisher);
            this.Criteria.GetAllObjValues += this.GetAllObjValues;
        }

        public void Save()
        {
            this.Criteria.Save();
        }
        public async Task SaveAsync()
        {
            await this.Criteria.SaveAsync();
        }

        public void Load()
        {
            this.Criteria.Load();
        }
        public async Task LoadAsync()
        {
            await this.Criteria.LoadAsync();
        }

        public void UpdateName(string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Update_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = this.ID });
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar, 50) { Value = name });

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        _Name = name;
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Unable to Update Name\n\n" + ex.Message);
                    }
                }
            }
        }
        public async Task UpdateNameAsync(string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Update_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = this.ID });
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar, 50) { Value = name });

                    try
                    {
                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        _Name = name;
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Unable to Update Name\n\n" + ex.Message);
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }

        public int CompareTo(SimpleSet<T, K> other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
