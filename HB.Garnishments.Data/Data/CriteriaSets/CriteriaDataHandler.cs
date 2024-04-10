using EvaluationCriteria.CriteriaSets.SimpleSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CriteriaSets
{
    [DataContract(Name = "Manager", IsReference = true)]
    public class CriteriaDataHandler
    {
        [DataMember(Name = "SetManager")]
        public SimpleSetDataHandler<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> SimpleSetManager { get; private set; }
        [DataMember(Name = "EvalSets")]
        public BindingList<EvaluationSet> EvaluationSets { get; private set; }
        [DataMember(Name = "Routines")]
        public BindingList<Routine> Routines { get; private set; }

        private CriteriaDataHandler(SimpleSetDataHandler<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> simpleSetManager)
        {
            this.SimpleSetManager = simpleSetManager;
        }
        public CriteriaDataHandler()
        {
            this.SimpleSetManager = new SimpleSetDataHandler<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor>(new Dictionary<string, object>());
            this.SimpleSetManager.GetObjAllValues += CriteriaDataHandler.GetObjAllValues;
            // Load Routines and Evaluation Sets
            Load();
        }

        private void Load()
        {
            this.Routines = new BindingList<Routine>();
            this.EvaluationSets = new BindingList<EvaluationSet>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT EVALID, DESCRIPTION FROM {Settings.Properties.SQLRoutines}", conn))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        var evalSet = new EvaluationSet(this, sdr);
                        this.EvaluationSets.Add(evalSet);
                        evalSet.Load();
                    }
                    sdr.Close();
                }
                using (SqlCommand cmd = new SqlCommand($"SELECT SALES, ATID, STATUS FROM {Settings.Properties.SQLRoutines} GROUP BY TEAM, ROUTINE", conn))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        var routine = new Routine(this, Convert.ToByte(sdr["SALES"]), (Enums.AssetType)Enum.ToObject(typeof(Enums.AssetType), Convert.ToInt16(sdr["ATID"])), (Enums.Status)Enum.ToObject(typeof(Enums.Status), Convert.ToInt16(sdr["STATUS"])));
                        Routines.Add(routine);
                        routine.LoadData();
                    }
                    sdr.Close();
                }
            }
        }
        private async Task LoadAsync()
        {
            this.Routines = new BindingList<Routine>();
            this.EvaluationSets = new BindingList<EvaluationSet>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT EVALID, DESCRIPTION FROM {Settings.Properties.SQLRoutines}", conn))
                {
                    if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        var evalSet = new EvaluationSet(this, sdr);
                        this.EvaluationSets.Add(evalSet);
                        await evalSet.LoadAsync();
                    }
                    sdr.Close();
                }
                using (SqlCommand cmd = new SqlCommand($"SELECT SALES, ATID, STATUS FROM {Settings.Properties.SQLRoutines} GROUP BY SALES, ATID, STATUS", conn))
                {
                    if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        var routine = new Routine(this, Convert.ToByte(sdr["SALES"]), (Enums.AssetType)Enum.ToObject(typeof(Enums.AssetType), Convert.ToInt16(sdr["ATID"])), (Enums.Status)Enum.ToObject(typeof(Enums.Status), Convert.ToInt16(sdr["STATUS"])));
                        Routines.Add(routine);
                        await routine.LoadDataAsync();
                    }
                    sdr.Close();
                }
            }
        }

        public static async Task<CriteriaDataHandler> CreateCriteriaDataHandlerAsync()
        {
            var simpleSetDataHandler = await SimpleSetDataHandler<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor>.CreateSimpleSetDataHandlerAsync(new Dictionary<string, object>());
            simpleSetDataHandler.GetObjAllValues += GetObjAllValues;
            var criteriaDataHandler = new CriteriaDataHandler(simpleSetDataHandler);
            await criteriaDataHandler.LoadAsync();
            return criteriaDataHandler;
        }

        #region Routines
        public void RemoveRoutine(Routine routine)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Remove_Routine, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@SALES", SqlDbType.TinyInt) { Value = routine.SalesNo });
                    cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.TinyInt) { Value = routine.AssetType });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.TinyInt) { Value = Convert.ToByte(routine.Status) });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    this.Routines.Remove(routine);
                }
            }
        }

        public void AddRoutineEvaluation(Routine routine, string description)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Add_RoutineEval, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@SALES", SqlDbType.TinyInt) { Value = routine.SalesNo });
                    cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.TinyInt) { Value = routine.AssetType });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.TinyInt) { Value = Convert.ToByte(routine.Status) });
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar) { Value = description });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    EvaluationSet newEval = new EvaluationSet(this, Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value), description);
                    this.EvaluationSets.Add(newEval);
                    routine.EvaluationSetIDs.Add(newEval.ID);
                }
            }
        }
        public void RemoveRoutineEvaluation(EvaluationSet eval)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Remove_RoutineEval, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = eval.ID });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    this.EvaluationSets.Remove(eval);
                }
            }
        }
        public void UpdateRoutineEvaluation(EvaluationSet eval, string description)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Update_RoutineEval, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = eval.ID });
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar) { Value = description });

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    eval.Description = description;
                }
            }
        }
        #endregion

        #region Eval Sets
        public void AddEvaluationSet(EvaluationSet eval, SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> set)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Add_EvalSet, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = eval.ID });
                    cmd.Parameters.Add(new SqlParameter("@SETID", SqlDbType.Int) { Value = set.ID });

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    eval.SimpleSetIDs.Add(set.ID);
                }
            }
        }
        public void RemoveEvaluationSet(EvaluationSet eval, SimpleSet<Accounts.EvaluateeAccount, Accounts.EvaluateeDebtor> set)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand(Settings.StoredProcedures.Remove_EvalSet, conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@EVALID", SqlDbType.Int) { Value = eval.ID });
                    cmd.Parameters.Add(new SqlParameter("@SETID", SqlDbType.Int) { Value = set.ID });

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    eval.SimpleSetIDs.Remove(set.ID);
                }
            }
        }
        #endregion

        #region Object Values (Custom Numeric Lists) **CURRENTLY NON-FUNCTIONAL**
        public static List<object> GetObjAllValues(Type type)
        {
            if (type == typeof(EvaluationCriteria.CriteriaSets.State))
            {
                return DataHandler.GetStatesAsync().Result.Cast<object>().ToList();
            }
            else return null;
        }
        #endregion

        #region Evaluations

        public IEnumerable<EvaluationResult> Evaluate(Data.Requests.AssetRequest request, Accounts.EvaluateeAccount account)
        {
            var routine = this.Routines.FirstOrDefault(r => r.SalesNo == account.SalesNo && r.AssetType == request.Asset.Type && r.Status == request.CurrentStatus.Type);
            if (routine == null)
            {
                return new EvaluationResult[0]; // NEED TO CHANGE THIS TO AUTO APPROVE -- OR HAVE FUNCTION TO CALLBACK FOR APPROVAL
            }

            var results = routine.EvaluationSets.SelectMany(eval => eval.SimpleSets.Select(ss => new EvaluationResult()
            {
                Account = account,
                Request = request,
                Set = eval,
                Criteria = ss,
                Result = ss.Criteria.EvaluateVerbose(account)
            }));
            return results;
        }
        public IEnumerable<EvaluationResult> Evaluate(Assets.AccountAsset asset, Accounts.EvaluateeAccount account)
        {
            var routine = this.Routines.FirstOrDefault(r => r.SalesNo == account.SalesNo && r.AssetType == asset.Asset.Type && r.Status == Enums.Status.Requested);
            if (routine == null)
            {
                return new EvaluationResult[0]; // NEED TO CHANGE THIS TO AUTO APPROVE -- OR HAVE FUNCTION TO CALLBACK FOR APPROVAL
            }

            var results = routine.EvaluationSets.SelectMany(eval => eval.SimpleSets.Select(ss => new EvaluationResult()
            {
                Account = account,
                Set = eval,
                Criteria = ss,
                Result = ss.Criteria.EvaluateVerbose(account)
            }));
            return results;
        }

        #endregion
    }
}
