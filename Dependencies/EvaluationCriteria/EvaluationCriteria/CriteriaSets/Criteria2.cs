using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationCriteria.CriteriaSets
{
    public class Criteria2 : Criteria<Accounts.EvaluateeAccount<Accounts.EvaluateeDebtor>, Accounts.EvaluateeDebtor>
    {
        public Criteria2(Dictionary<string, object> SQLDistinguisher) : base(SQLDistinguisher) { }
    }
    public class Criteria<T, K> : Criteria, Interfaces.ICriteriaLoader where T : Accounts.EvaluateeAccount<K> where K : Accounts.EvaluateeDebtor
    {
        protected Dictionary<string, string> Value3 { get; set; }

        public event Func<Type, List<object>> GetAllObjValues;

        public Criteria(Dictionary<string, object> SQLDistinguisher) : base(SQLDistinguisher, true)
        {
            this.SQLDistinguisher = new Dictionary<string, object>(SQLDistinguisher);
            this.CriteriaItems = new Dictionary<string, CParam>();
            this.Value1 = new Dictionary<string, string>();
            this.Value2 = new Dictionary<string, string>();
            this.Value3 = new Dictionary<string, string>();

            foreach (var property in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                object[] attr = property.GetCustomAttributes(typeof(Attributes.PropertyAssociationAttribute), true);
                if (attr.Length > 0)
                {
                    var obj = attr.FirstOrDefault();
                    Attributes.PropertyAssociationAttribute PAA = null;
                    if (obj is Attributes.DerivedPropertyAssociationAttribute)
                    {
                        PAA = obj as Attributes.DerivedPropertyAssociationAttribute;
                    }
                    else if (obj is Attributes.PropertyAssociationAttribute)
                    {
                        PAA = obj as Attributes.PropertyAssociationAttribute;
                    }
                    if (PAA != null)
                    {
                        if (!CriteriaItems.ContainsKey(PAA.SqlAssociation))
                        {
                            CParam param = PAA.ParameterType.GetConstructor(new Type[] { typeof(string) }).Invoke(new[] { PAA.Description }) as CParam;
                            CriteriaItems.Add(PAA.SqlAssociation, param);
                        }
                        if (PAA.ParamNumber == 1)
                        {
                            if (!Value1.ContainsKey(PAA.SqlAssociation))
                                Value1.Add(PAA.SqlAssociation, property.Name);
                        }
                        else if (PAA.ParamNumber == 2)
                        {
                            if (!Value2.ContainsKey(PAA.SqlAssociation))
                                Value2.Add(PAA.SqlAssociation, property.Name);
                        }
                        if (PAA is Attributes.DerivedPropertyAssociationAttribute)
                        {
                            Value3.Add(PAA.SqlAssociation, (PAA as Attributes.DerivedPropertyAssociationAttribute).DerivedParameter);
                        }
                    }
                }
            }
        }

        public override void Load()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT [OPTION],[ISNOT],[COMPARE],[VALUE],[VALUE2] FROM {0} WHERE {1}", Settings.Properties.SQLOptions, this.SQLDistinguisherString), conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            CParam Param = this.CriteriaItems[sdr["OPTION"].ToString()];
                            Param.Load(sdr, this);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
        public async Task LoadAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT [OPTION],[ISNOT],[COMPARE],[VALUE],[VALUE2] FROM {0} WHERE {1}", Settings.Properties.SQLOptions, this.SQLDistinguisherString), conn))
                {
                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        try
                        {
                            CParam Param = this.CriteriaItems[sdr["OPTION"].ToString()];
                            await Param.LoadAsync(sdr, this);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        public override void Save()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter($"SELECT * FROM {Settings.Properties.SQLOptions} WHERE {this.SQLDistinguisherString}", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }

                    foreach (KeyValuePair<string, CParam> KVP in this.CriteriaItems)
                    {
                        if (KVP.Value.Param > 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["OPTION"] = KVP.Key;
                            foreach (KeyValuePair<string, object> KP in this.SQLDistinguisher)
                            {
                                dr[KP.Key] = KP.Value;
                            }
                            KVP.Value.Save(dr);

                            dt.Rows.Add(dr);
                        }
                    }

                    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                    sda.Update(dt);
                }
            }
        }
        public async Task SaveAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter($"SELECT * FROM {Settings.Properties.SQLOptions} WHERE {this.SQLDistinguisherString}", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }

                    foreach (KeyValuePair<string, CParam> KVP in this.CriteriaItems)
                    {
                        if (KVP.Value.Param > 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["OPTION"] = KVP.Key;
                            foreach (KeyValuePair<string, object> KP in this.SQLDistinguisher)
                            {
                                dr[KP.Key] = KP.Value;
                            }
                            await KVP.Value.SaveAsync(dr);

                            dt.Rows.Add(dr);
                        }
                    }

                    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                    sda.Update(dt);
                }
            }
        }

        public bool Evaluate(T Evaluatee)
        {
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    try
                    {
                        object Val1 = null;
                        object Val2 = null;
                        try { Val1 = typeof(T).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                        catch { }
                        if (Value2.ContainsKey(CP.Key) && !string.IsNullOrEmpty(Value2[CP.Key]))
                        {
                            if (Val1 is Evaluatees2.MediaStatus)
                                Val2 = Val1.GetType().GetProperty(Value2[CP.Key]).GetValue(Val1, null);
                            else
                            {
                                try { Val2 = typeof(T).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                                catch { }
                            }
                        }
                        if (!CP.Value.Evaluate(Val1, Val2)) return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public Eval EvaluateVerbose(T Evaluatee)
        {
            bool Success = true;
            StringBuilder Info = new StringBuilder();
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    try
                    {
                        object Val1 = null;
                        object Val2 = null;
                        object Val3 = null;
                        try { Val1 = typeof(T).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                        catch { }
                        if (Value2.ContainsKey(CP.Key) && !string.IsNullOrEmpty(Value2[CP.Key]))
                        {
                            if (Val1 is Evaluatees2.MediaStatus)
                                Val2 = Val1.GetType().GetProperty(Value2[CP.Key]).GetValue(Val1, null);
                            else
                            {
                                try { Val2 = typeof(T).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                                catch { }
                            }
                        }
                        if (Value3.ContainsKey(CP.Key) && !string.IsNullOrEmpty(Value3[CP.Key]))
                        {
                            Val3 = typeof(T).GetProperty(Value3[CP.Key]).GetValue(Evaluatee, null);
                        }

                        Eval Result = CP.Value.EvaluateVerbose(Val1, Val2, Val3);
                        Success &= Result.Success;

                        Info.Append(Result.Info + ", ");
                    }
                    catch (Exception ex)
                    {
                        return new Eval(false, $"[{CP.Key}] {ex.Message}");
                    }
                }
            }
            return new Eval(Success, Info.Length > 2 ? Info.ToString().Substring(0, Info.Length - 2) : "");
        }

        public CodeValue GetCodeValuesForParam(int id)
        {
            CodeValue value = new CodeValue();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE],[CONSTRAINT],[DAYS] FROM {Settings.Properties.SQLCodes} WHERE CID={id}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            value.Add(Convert.ToInt32(sdr["CODE"]),
                                sdr["CONSTRAINT"] == DBNull.Value ? 0 : Convert.ToByte(sdr["CONSTRAINT"]),
                                sdr["DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["DAYS"])
                                );
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return value;
        }
        public async Task<CodeValue> GetCodeValuesForParamAsync(int id)
        {
            CodeValue value = new CodeValue();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE],[CONSTRAINT],[DAYS] FROM {Settings.Properties.SQLCodes} WHERE CID={id}", conn))
                {
                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        try
                        {
                            value.Add(Convert.ToInt32(sdr["CODE"]),
                                sdr["CONSTRAINT"] == DBNull.Value ? 0 : Convert.ToByte(sdr["CONSTRAINT"]),
                                sdr["DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["DAYS"])
                                );
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return value;
        }
        public List<int> GetCodesForParam(int id)
        {
            List<int> codes = new List<int>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE],[CONSTRAINT],[DAYS] FROM {Settings.Properties.SQLCodes} WHERE CID={id}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            codes.Add(Convert.ToInt32(sdr["CODE"]));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return codes;
        }
        public async Task<List<int>> GetCodesForParamAsync(int id)
        {
            List<int> codes = new List<int>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE],[CONSTRAINT],[DAYS] FROM {Settings.Properties.SQLCodes} WHERE CID={id}", conn))
                {
                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        try
                        {
                            codes.Add(Convert.ToInt32(sdr["CODE"]));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return codes;
        }
        public List<string> GetStringsForParam(int id)
        {
            List<string> strings = new List<string>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE] FROM {Settings.Properties.SQLCodeStrings} WHERE CID={id}", conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            strings.Add(sdr["CODE"].ToString());
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return strings;
        }
        public async Task<List<string>> GetStringsForParamAsync(int id)
        {
            List<string> strings = new List<string>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [CODE] FROM {Settings.Properties.SQLCodeStrings} WHERE CID={id}", conn))
                {
                    await conn.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        try
                        {
                            strings.Add(sdr["CODE"].ToString());
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            return strings;
        }
        public List<object> GetObjValues(Type type)
        {
            return GetAllObjValues?.Invoke(type) as List<Object>;
        }
    }
}
