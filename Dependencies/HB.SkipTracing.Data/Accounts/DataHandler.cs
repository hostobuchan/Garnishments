using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HB.SkipTracing.Data.Accounts
{
    [DataContract(Name = "EvaluateeDatahandler", Namespace = "http://schemas.datacontract.org/2004/07/EvaluationCriteria.Accounts")]
    public class DataHandler : EvaluationCriteria.Accounts.EvaluateeDataHandler<EvaluateeAccount, EvaluateeDebtor>
    {
        public static new DataHandler GetDataHandler()
        {
            return GetDataHandler(typeof(DataHandler)) as DataHandler;
        }
        public static async new Task<DataHandler> GetDataHandlerAsync()
        {
            return (await GetDataHandlerAsync(typeof(DataHandler))) as DataHandler;
        }
        public static   DataHandler GetDataHandler(List<string> accounts)
        {
            return GetDataHandler(typeof(DataHandler), accounts) as DataHandler;
        }
        public static async  Task<DataHandler> GetDataHandlerAsync(List<string> accounts)
        {
            return (await GetDataHandlerAsync(typeof(DataHandler), accounts)) as DataHandler;
        }
        public static List<EvaluateeAccount> GetAccounts()
        {
            return GetDataHandler()?.Accounts;
        }
        public static async Task<List<EvaluateeAccount>> GetAccountsAsync()
        {
            return (await GetDataHandlerAsync())?.Accounts;
        }
        public static List<EvaluateeAccount> GetAccounts(List<string> accounts)
        {
            return GetDataHandler(accounts)?.Accounts;
        }
        public static async Task<List<EvaluateeAccount>> GetAccountsAsync(List<string> accounts)
        {
            return (await GetDataHandlerAsync(accounts))?.Accounts;
        }

        public static new DataHandler GetDataHandler(Type type)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccounts", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.CommandTimeout = 180;
                    conn.Open();

                    XmlReader xr = cmd.ExecuteXmlReader();
                    DataContractSerializer serializer = new DataContractSerializer(type);
                    return serializer.ReadObject(xr) as DataHandler;
                }
            }
        }
        protected static new async Task<DataHandler> GetDataHandlerAsync(Type type)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccounts", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.CommandTimeout = 180;
                    await conn.OpenAsync();

                    XmlReader xr = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(type);
                    return serializer.ReadObject(xr) as DataHandler;
                }
            }
        }
        protected static  DataHandler GetDataHandler(Type type, List<string> accounts)
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

                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccountsList", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCOUNTS", SqlDbType.Structured) { TypeName = "FileNoTableType", Value = dt });
                        cmd.CommandTimeout = 180;
                        conn.Open();

                        XmlReader xr = cmd.ExecuteXmlReader();
                        DataContractSerializer serializer = new DataContractSerializer(type);
                        return serializer.ReadObject(xr) as DataHandler;
                    }
                }
            }
        }
        protected static  async Task<DataHandler> GetDataHandlerAsync(Type type, List<string> accounts)
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

                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("GetEvaluateeAccountsList", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCOUNTS", SqlDbType.Structured) { TypeName = "FileNoTableType", Value = dt });
                        cmd.CommandTimeout = 180;
                        await conn.OpenAsync();

                        XmlReader xr = await cmd.ExecuteXmlReaderAsync();
                        DataContractSerializer serializer = new DataContractSerializer(type);
                        var obj = serializer.ReadObject(xr);
                        return obj as DataHandler;
                    }
                }
            }
        }
    }
}
