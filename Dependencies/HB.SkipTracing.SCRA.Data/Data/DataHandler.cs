using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HB.SkipTracing.SCRA.Data
{
    public class DataHandler
    {


        public static async Task AddAccountsAsync(IEnumerable<Tuple<string, byte>> accounts)
        {
            using (var table = CreateFileNoDataTable(accounts))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using ( SqlCommand cmd = new SqlCommand("SCRA_AddAccount", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@FILENOS", SqlDbType.Structured) { TypeName = "FileNoDebtorTableType", Value = table });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public static async Task RemoveAccountsAsync(IEnumerable<Tuple<string, byte>> accounts)
        {
            using (var table = CreateFileNoDataTable(accounts))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("SCRA_RemoveAccount", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@FILENOS", SqlDbType.Structured) { TypeName = "FileNoDebtorTableType", Value = table });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public static async Task UpdateAccountsAsync(IEnumerable<SkipTracing.Data.RNN.Records.MilitaryRecord> accounts)
        {
            using (var table = CreateSCRAUpdateTable(accounts))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("SCRA_UpdateAccount", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@UPDATES", SqlDbType.Structured) { TypeName = "SCRATableType", Value = table });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public static async Task<IEnumerable<Accounts.AccountStatus>> GetAccountStatusAsync(IEnumerable<Tuple<string, byte>> accounts)
        {
            using (var table = CreateFileNoDataTable(accounts))
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("SCRA_GetAccountStatus_AccountList", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@ACCTS", SqlDbType.Structured) { TypeName = "FileNoDebtorTableType", Value = table });

                        await conn.OpenAsync();
                        var xml = await cmd.ExecuteXmlReaderAsync();
                        DataContractSerializer serializer = new DataContractSerializer(typeof(Accounts.AccountStatus[]));
                        var obj = serializer.ReadObject(xml);
                        return obj as Accounts.AccountStatus[];
                    }
                }
            }
        }

        public static System.Data.DataTable CreateFileNoDataTable(IEnumerable<Tuple<string,byte>> accounts)
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("FILENO", typeof(string));
            dt.Columns.Add("DEBTOR", typeof(byte));
            foreach (var debtor in accounts.Distinct())
            {
                var dr = dt.NewRow();
                dr["FILENO"] = debtor.Item1;
                dr["DEBTOR"] = debtor.Item2;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private static System.Data.DataTable CreateSCRAUpdateTable(IEnumerable<SkipTracing.Data.RNN.Records.MilitaryRecord> accounts)
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("FILENO", typeof(string));
            dt.Columns.Add("DEBTOR", typeof(byte));
            dt.Columns.Add("CLOSE_DATE", typeof(DateTime));
            dt.Columns.Add("ACTIVE_DUTY", typeof(bool));
            dt.Columns.Add("ACTIVE_DUTY_DATE", typeof(DateTime));
            dt.Columns.Add("ACTIVE_DUTY_ENDDATE", typeof(DateTime));
            foreach (var acct in accounts)
            {
                AddSCRAAccount(acct, dt);
            }
            return dt;
        }
        private static void AddSCRAAccount(SkipTracing.Data.RNN.Records.MilitaryRecord record, System.Data.DataTable table)
        {
            var dr = table.NewRow();
            dr["FILENO"] = record.FileNo;
            dr["DEBTOR"] = record.DebtorNo;
            dr["CLOSE_DATE"] = DBNull.Value;
            dr["ACTIVE_DUTY"] = record.ActiveDutyStatus;
            dr["ACTIVE_DUTY_DATE"] = record.ActiveDutyStartDate ?? (object)DBNull.Value;
            dr["ACTIVE_DUTY_ENDDATE"] = record.ActiveDutyEndDate ?? (object)DBNull.Value;
            table.Rows.Add(dr);
        }

        public static async Task SaveScrubFileToScan(string fileNo, string debtor, Zip.ZipArchive.ZipFileInfo file)
        {
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var saveCertFileName = string.Format(Settings.Properties.SaveLocation_AccountCertificate, fileNo, debtor, string.Empty);

            using (System.IO.Stream ms = file.GetStream())
            {
                var compareHash = hash.ComputeHash(ms);
                var b64CompareHash = Convert.ToBase64String(compareHash);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                (new System.IO.FileInfo(saveCertFileName)).Directory.CreateDirectoryStructure();
                int cnt = 0;
                while (System.IO.File.Exists(saveCertFileName))
                {
                    byte[] bytehash;
                    string b64Hash;
                    using (var fs = new System.IO.FileStream(saveCertFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        bytehash = hash.ComputeHash(fs);
                        b64Hash = Convert.ToBase64String(bytehash);
                    }

                    if (string.Equals(b64CompareHash, b64Hash))
                    {
                        // The Cert is Already in Scan
                        return;
                    }

                    cnt++;
                    saveCertFileName = string.Format(Settings.Properties.SaveLocation_AccountCertificate, fileNo, debtor, $" {cnt}");
                }

                using (System.IO.FileStream fs = new System.IO.FileStream(saveCertFileName, System.IO.FileMode.Create))
                {
                    await ms.CopyToAsync(fs);
                }
            }
        }
    }
}
