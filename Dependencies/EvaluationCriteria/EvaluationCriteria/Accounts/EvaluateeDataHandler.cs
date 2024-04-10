using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace EvaluationCriteria.Accounts
{
    public class EvaluateeDataHandler : EvaluateeDataHandler<EvaluateeAccount<EvaluateeDebtor>, EvaluateeDebtor>
    {
        public static new EvaluateeDataHandler GetDataHandler()
        {
            return GetDataHandler(typeof(EvaluateeDataHandler)) as EvaluateeDataHandler;
        }
        public static new async Task<EvaluateeDataHandler> GetDataHandlerAsync()
        {
            return (await GetDataHandlerAsync(typeof(EvaluateeDataHandler))) as EvaluateeDataHandler;
        }
        public static new EvaluateeDataHandler GetDataHandler(IEnumerable<string> accounts)
        {
            return GetDataHandler(typeof(EvaluateeDataHandler), accounts) as EvaluateeDataHandler;
        }
        public static new async Task<EvaluateeDataHandler> GetDataHandlerAsync(IEnumerable<string> accounts)
        {
            return (await GetDataHandlerAsync(typeof(EvaluateeDataHandler), accounts)) as EvaluateeDataHandler;
        }
    }
    [DataContract(Name = "EvaluateeDataHandler", IsReference = true)]
    public class EvaluateeDataHandler<T, K> where T : EvaluateeAccount<K> where K : EvaluateeDebtor
    {
        [DataMember(Name = "Accounts", Order = 0)]
        public List<T> Accounts { get; set; }

        protected static EvaluateeDataHandler<T, K> GetDataHandler()
        {
            return GetDataHandler(typeof(EvaluateeDataHandler<T, K>));
        }
        protected static async Task<EvaluateeDataHandler<T, K>> GetDataHandlerAsync()
        {
            return await GetDataHandlerAsync(typeof(EvaluateeDataHandler<T, K>));
        }

        protected static EvaluateeDataHandler<T, K> GetDataHandler(IEnumerable<string> accounts)
        {
            return GetDataHandler(typeof(EvaluateeDataHandler<T, K>), accounts);
        }
        protected static async Task<EvaluateeDataHandler<T, K>> GetDataHandlerAsync(IEnumerable<string> accounts)
        {
            return await GetDataHandlerAsync(typeof(EvaluateeDataHandler<T, K>), accounts);
        }
        protected static EvaluateeDataHandler<T, K> GetDataHandler(Type type)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccounts", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.CommandTimeout = 180;
                    conn.Open();

                    XmlReader xr = cmd.ExecuteXmlReader();
                    DataContractSerializer serializer = new DataContractSerializer(type);
                    return serializer.ReadObject(xr) as EvaluateeDataHandler<T, K>;
                }
            }
        }
        protected static async Task<EvaluateeDataHandler<T, K>> GetDataHandlerAsync(Type type)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccounts", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.CommandTimeout = 180;
                    await conn.OpenAsync();

                    XmlReader xr = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(type);
                    return serializer.ReadObject(xr) as EvaluateeDataHandler<T, K>;
                }
            }
        }
        protected static EvaluateeDataHandler<T, K> GetDataHandler(Type type, IEnumerable<string> accounts)
        {
            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("FILENO", typeof(string));
                foreach (var acct in accounts)
                {
                    var newRow = dt.NewRow();
                    newRow["FILENO"] = acct;
                    dt.Rows.Add(newRow);
                }

                using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
                {
                    using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccountsList", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCOUNTS", SqlDbType.Structured) { TypeName = "FileNoTableType", Value = dt });
                        cmd.CommandTimeout = 180;
                        conn.Open();

                        XmlReader xr = cmd.ExecuteXmlReader();
                        DataContractSerializer serializer = new DataContractSerializer(type);
                        var result = serializer.ReadObject(xr) as EvaluateeDataHandler<T, K>;
                        return result;
                    }
                }
            }
        }
        protected static async Task<EvaluateeDataHandler<T, K>> GetDataHandlerAsync(Type type, IEnumerable<string> accounts)
        {
            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("FILENO", typeof(string));
                await Task.Run(() =>
                {
                    foreach (var acct in accounts)
                    {
                        var newRow = dt.NewRow();
                        newRow["FILENO"] = acct;
                        dt.Rows.Add(newRow);
                    }
                });

                using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
                {
                    using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccountsList", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCOUNTS", SqlDbType.Structured) { TypeName = "FileNoTableType", Value = dt });
                        cmd.CommandTimeout = 180;
                        await conn.OpenAsync();

                        XmlReader xr = await cmd.ExecuteXmlReaderAsync();
                        DataContractSerializer serializer = new DataContractSerializer(type);
                        return serializer.ReadObject(xr) as EvaluateeDataHandler<T, K>;
                    }
                }
            }
        }

        public static T GetEvaluateeAccount(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccount", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar, 8) { Value = fileNo });
                        cmd.CommandTimeout = 180;
                        conn.Open();

                        XmlReader xr = cmd.ExecuteXmlReader();
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                        return serializer.ReadObject(xr) as T;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
        public static async Task<T> GetEvaluateeAccountAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccount", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        cmd.Parameters.Add(new SqlParameter("@FILENO", SqlDbType.NVarChar, 8) { Value = fileNo });
                        cmd.CommandTimeout = 180;
                        await conn.OpenAsync();

                        XmlReader xr = await cmd.ExecuteXmlReaderAsync();
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                        return serializer.ReadObject(xr) as T;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
    }
}
