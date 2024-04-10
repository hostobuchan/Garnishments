using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.SkipTracing.Data.Accounts;
using HB.SkipTracing.Data.Records;

namespace HB.SkipTracing.Data.Batches
{
    public abstract class BatchHandler<T, K> : Interfaces.IBatchHandler<T, K> where T : Records.UploadRecord where K : Records.DownloadRecord
    {
        public int Vendor { get; private set; }
        public int? CurrentID { get; protected set; }
        public abstract string UploadFileName { get; protected set; }
        public DateTime? CreationDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public Enums.AssetType SearchType { get; private set; }

        public BatchHandler(int vendor, Enums.AssetType searchType)
        {
            this.Vendor = vendor;
            this.SearchType = searchType;
        }

        public int GetBatchID()
        {
            if (CurrentID.HasValue) return this.CurrentID.Value;
            else
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("AddBatch", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.TinyInt) { Value = this.Vendor });
                        cmd.Parameters.Add(new SqlParameter("@PRODUCT", SqlDbType.TinyInt) { Value = this.SearchType });
                        cmd.Parameters.Add(new SqlParameter("@FILENAME", SqlDbType.NVarChar) { Value = this.UploadFileName });
                        cmd.Parameters.Add(new SqlParameter("@C_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("@R_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        this.CurrentID = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        this.CreationDate = Convert.ToDateTime(cmd.Parameters["@C_DT"].Value);
                        this.ReturnDate = cmd.Parameters["@R_DT"].Value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(cmd.Parameters["@R_DT"].Value);
                        return this.CurrentID.Value;
                    }
                }
            }
        }
        public async Task<int> GetBatchIDAsync()
        {
            if (CurrentID.HasValue) return this.CurrentID.Value;
            else
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
                {
                    using (SqlCommand cmd = new SqlCommand("AddBatch", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.TinyInt) { Value = this.Vendor });
                        cmd.Parameters.Add(new SqlParameter("@PRODUCT", SqlDbType.TinyInt) { Value = this.SearchType });
                        cmd.Parameters.Add(new SqlParameter("@FILENAME", SqlDbType.NVarChar) { Value = this.UploadFileName });
                        cmd.Parameters.Add(new SqlParameter("@C_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("@R_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        this.CurrentID = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        this.CreationDate = Convert.ToDateTime(cmd.Parameters["@C_DT"].Value);
                        this.ReturnDate = cmd.Parameters["@R_DT"].Value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(cmd.Parameters["@R_DT"].Value);
                        return this.CurrentID.Value;
                    }
                }
            }
        }

        public abstract IEnumerable<T> GenerateBatchUploadRecords(IEnumerable<EvaluateeAccount> accounts);
        public abstract Task<IEnumerable<T>> GenerateBatchUploadRecordsAsync(IEnumerable<EvaluateeAccount> accounts);


        public bool LoadBatchInfo(System.IO.FileInfo file)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetBatchInfo", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.Int) { Value = this.Vendor });
                    cmd.Parameters.Add(new SqlParameter("@FILENAME", SqlDbType.NVarChar) { Value = file.Name });
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT", SqlDbType.TinyInt) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@C_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@R_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@UPLOAD_FILE", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (Convert.ToBoolean(cmd.Parameters["RETURN_VALUE"].Value))
                    {
                        this.UploadFileName = cmd.Parameters["@UPLOAD_FILE"].Value.ToString();
                        this.CurrentID = Convert.ToInt32(cmd.Parameters["@ID"].Value);
                        this.SearchType = (Enums.AssetType)Enum.ToObject(typeof(Enums.AssetType), Convert.ToByte(cmd.Parameters["@PRODUCT"].Value));
                        this.CreationDate = Convert.ToDateTime(cmd.Parameters["@C_DT"].Value);
                        this.ReturnDate = cmd.Parameters["@R_DT"].Value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(cmd.Parameters["@R_DT"]);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<bool> LoadBatchInfoAsync(System.IO.FileInfo file)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetBatchInfo", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.Int) { Value = this.Vendor });
                    cmd.Parameters.Add(new SqlParameter("@FILENAME", SqlDbType.NVarChar) { Value = file.Name });
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@PRODUCT", SqlDbType.TinyInt) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@C_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@R_DT", SqlDbType.Date) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("@UPLOAD_FILE", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    if (Convert.ToBoolean(cmd.Parameters["RETURN_VALUE"].Value))
                    {
                        this.UploadFileName = cmd.Parameters["@UPLOAD_FILE"].Value.ToString();
                        this.CurrentID = Convert.ToInt32(cmd.Parameters["@ID"].Value);
                        this.SearchType = (Enums.AssetType)Enum.ToObject(typeof(Enums.AssetType), Convert.ToByte(cmd.Parameters["@PRODUCT"].Value));
                        this.CreationDate = Convert.ToDateTime(cmd.Parameters["@C_DT"].Value);
                        this.ReturnDate = cmd.Parameters["@R_DT"].Value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(cmd.Parameters["@R_DT"]);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static List<BatchInfo> GetRecentBatchInfo(int vendorId)
        {
            List<BatchInfo> batches = new List<BatchInfo>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetRecentBatchInfoByVendor", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.Int) { Value = vendorId });

                    conn.Open();
                    var sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        batches.Add(new BatchInfo(sdr));
                    }
                }
            }
            return batches;
        }
        public static async Task<List<BatchInfo>> GetRecentBatchInfoAsync(int vendorId)
        {
            List<BatchInfo> batches = new List<BatchInfo>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.STDB))
            {
                using (SqlCommand cmd = new SqlCommand("GetRecentBatchInfoByVendor", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VID", SqlDbType.Int) { Value = vendorId });

                    await conn.OpenAsync();
                    var sdr = await cmd.ExecuteReaderAsync();
                    while (await sdr.ReadAsync())
                    {
                        batches.Add(new BatchInfo(sdr));
                    }
                }
            }
            return batches;
        }
    }
}
