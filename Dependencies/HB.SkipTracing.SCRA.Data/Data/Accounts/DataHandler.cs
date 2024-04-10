using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.SCRA.Data.Accounts
{
    static class DataHandler
    {
        public static async Task<IEnumerable<AccountDebtor>> Get_UploadAccounts_AddAsync()
        {
            return await Get_UploadAccounts_Async("SCRA_GetUploadAccounts_Add");
        }
        public static async Task<IEnumerable<AccountDebtor>> Get_UploadAccounts_RemoveAsync()
        {
            return await Get_UploadAccounts_Async("SCRA_GetUploadAccounts_Remove");
        }
        public static async Task<IEnumerable<AccountDebtor>> Get_UploadAccounts_AccountListAsync(IEnumerable<Tuple<string, byte>> accountDebtors)
        {
            return await Get_UploadAccounts_Async("SCRA_GetUploadAccounts_AccountList", accountDebtors);
        }
        static async Task<IEnumerable<AccountDebtor>> Get_UploadAccounts_Async(string command, IEnumerable<Tuple<string, byte>> accountDebtors = null)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand(command, conn) { CommandType = CommandType.StoredProcedure })
                {
                    if (accountDebtors != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCTS", SqlDbType.Structured) { TypeName = "FileNoDebtorTableType", Value = Data.DataHandler.CreateFileNoDataTable(accountDebtors.Distinct()) });
                    }
                    try
                    {
                        await conn.OpenAsync();
                        var xml = await cmd.ExecuteXmlReaderAsync();
                        DataContractSerializer serializer = new DataContractSerializer(typeof(AccountDebtor[]));
                        var obj = serializer.ReadObject(xml);
                        return obj as AccountDebtor[];
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public static async Task<List<string>> Get_AccountList_NeedingCodingAsync()
        {
            List<string> accounts = new List<string>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("SCRA_GetAccounts_NeedingCoding", conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        await conn.OpenAsync();

                        var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            accounts.Add(Convert.ToString(reader["FILENO"]));
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return accounts;
        }
    }
}
