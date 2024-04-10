using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets.SimpleSets
{
    public class SimpleSetDataHandler<T, K> where T : Accounts.EvaluateeAccount<K> where K : Accounts.EvaluateeDebtor
    {
        public event Func<Type, List<object>> GetObjAllValues;

        public Dictionary<string, object> SQLDistinguisher { get; private set; }
        public List<SimpleSet<T, K>> SimpleSets { get; private set; }

        public SimpleSetDataHandler(Dictionary<string, object> SQLDistinguisher)
        {
            this.SQLDistinguisher = new Dictionary<string, object>(SQLDistinguisher ?? new Dictionary<string, object>());
        }

        public void Load()
        {
            this.SimpleSets = new List<SimpleSet<T, K>>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {Settings.Properties.SQLSets}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        var set = new SimpleSet<T, K>(this.SQLDistinguisher, sdr);
                        set.GetAllObjValues += GetObjAllValues;
                        set.Load();
                        this.SimpleSets.Add(set);
                    }
                    sdr.Close();
                }
            }
        }
        public async Task LoadAsync()
        {
            this.SimpleSets = new List<SimpleSet<T, K>>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {Settings.Properties.SQLSets}", conn))
                {
                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        var set = new SimpleSet<T, K>(this.SQLDistinguisher, sdr);
                        set.GetAllObjValues += GetObjAllValues;
                        await set.LoadAsync();
                        this.SimpleSets.Add(set);
                    }
                    sdr.Close();
                }
            }
        }

        public static async Task<SimpleSetDataHandler<T, K>> CreateSimpleSetDataHandlerAsync(Dictionary<string, object> SQLDistinguisher)
        {
            var dataHandler = new SimpleSetDataHandler<T, K>(SQLDistinguisher);
            await dataHandler.LoadAsync();
            return dataHandler;
        }

        public SimpleSet<T, K> AddSet(string name)
        {
            if (!this.SimpleSets.Select(el => el.Name).Contains(name))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
                {
                    using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Add_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        foreach (var id in this.SQLDistinguisher)
                        {
                            cmd.Parameters.Add(new SqlParameter($"@{id.Key}", id.Value));
                        }
                        cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar, 50) { Value = name });
                        cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            var newSet = new SimpleSet<T, K>(this.SQLDistinguisher, Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value), name);
                            this.SimpleSets.Add(newSet);
                            return newSet;
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Unable to Add Set\n\n" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                throw new DuplicateNameException("A Set with That Name Already Exists!");
            }
        }
        public async Task<SimpleSet<T, K>> AddSetAsync(string name)
        {
            if (!this.SimpleSets.Select(el => el.Name).Contains(name))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
                {
                    using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Add_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        foreach (var id in this.SQLDistinguisher)
                        {
                            cmd.Parameters.Add(new SqlParameter($"@{id.Key}", id.Value));
                        }
                        cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar, 50) { Value = name });
                        cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                        try
                        {
                            await conn.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            var newSet = new SimpleSet<T, K>(this.SQLDistinguisher, Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value), name);
                            this.SimpleSets.Add(newSet);
                            return newSet;
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Unable to Add Set\n\n" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                throw new DuplicateNameException("A Set with That Name Already Exists!");
            }
        }

        public void RemoveSet(SimpleSet<T, K> set)
        {
            if (this.SimpleSets.Contains(set)) // Check that I own the set
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
                {
                    using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Remove_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = set.ID });

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            this.SimpleSets.Remove(set);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Unable to Remove Set\n\n" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to Remove Set - Not Found");
            }
        }
        public async Task RemoveSetAsync(SimpleSet<T, K> set)
        {
            if (this.SimpleSets.Contains(set)) // Check that I own the set
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
                {
                    using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Remove_SimpleSet, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = set.ID });

                        try
                        {
                            await conn.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            this.SimpleSets.Remove(set);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Unable to Remove Set\n\n" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to Remove Set - Not Found");
            }
        }

        public void UpdateSet(SimpleSet<T, K> set, string name)
        {
            if (!this.SimpleSets.Select(el => el.Name).Contains(name))
            {
                set.Name = name;
            }
            else
            {
                throw new DuplicateNameException("A Set with That Name Already Exists!");
            }
        }
    }
}
