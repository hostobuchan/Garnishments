using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data
{
    public static class DataHandler
    {
        private static Enums.Status[] _FinalStatuses;
        public static Enums.Status[] FinalStatuses
        {
            get
            {
                if (_FinalStatuses == null)
                {
                    using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT [STATUS] \"Status\" FROM CompletedStatus FOR XML PATH(''), ROOT('ArrayOfStatus'), TYPE", conn))
                        {
                            conn.Open();
                            var xml = cmd.ExecuteXmlReader();
                            DataContractSerializer serializer = new DataContractSerializer(typeof(Enums.Status[]));
                            var obj = serializer.ReadObject(xml);
                            _FinalStatuses = obj as Enums.Status[];
                        }
                    }
                }
                return _FinalStatuses;
            }
        }

        #region Primitives

        #region Get ID

        public static async Task<int> GetAccountIDAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetID_FileNo", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileNo });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                }
            }
        }
        public static async Task<int> GetContactIDAsync(string contact)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetID_Contact", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar) { Value = contact });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                }
            }
        }
        public static async Task<int> GetLocationIDAsync(string address1, string address2, string city, string state, string zip)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetID_Location", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ADDRESS1", System.Data.SqlDbType.NVarChar) { Value = address1 });
                    cmd.Parameters.Add(new SqlParameter("@ADDRESS2", System.Data.SqlDbType.NVarChar) { Value = address2 });
                    cmd.Parameters.Add(new SqlParameter("@CITY", System.Data.SqlDbType.NVarChar) { Value = city });
                    cmd.Parameters.Add(new SqlParameter("@STATE", System.Data.SqlDbType.NVarChar) { Value = state });
                    cmd.Parameters.Add(new SqlParameter("@ZIP", System.Data.SqlDbType.NVarChar) { Value = zip });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                }
            }
        }

        #endregion

        #region Get Object

        /// <summary>
        /// Get List of Additional Attribute Types
        /// </summary>
        /// <returns>Array of Additional Attribute Type Info</returns>
        public static async Task<Assets.AdditionalInfoType[]> GetAdditionalInfoTypesAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAdditionalInfoTypes", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.AdditionalInfoType[]));
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.AdditionalInfoType[];
                }
            }
        }

        public static async Task<Assets.AssetInfo> GetAssetAsync(ulong id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get List of Result Possibilities
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="assetType">Asset Type</param>
        /// <returns>Array of Results</returns>
        public static async Task<Requests.Results.ResultCodeHandler> GetResultCodeHandlerAsync(Enums.Status status, Enums.AssetType assetType)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetResults_Status", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@STATUS", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(status) });
                    cmd.Parameters.Add(new SqlParameter("@ATID", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(assetType) });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.Results.ResultCodeHandler));
                    var obj = serializer.ReadObject(xml);
                    return obj as Requests.Results.ResultCodeHandler;
                }
            }
        }

        public static async Task<EvaluationCriteria.CriteriaSets.State[]> GetStatesAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("StatesXML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();
                    var reader = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(EvaluationCriteria.CriteriaSets.State[]));
                    var obj = serializer.ReadObject(reader);
                    return obj as EvaluationCriteria.CriteriaSets.State[];
                }
            }
        }

        public static async Task<Assets.Base.RegisteredAgent[]> GetRegisteredAgentsAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd =new SqlCommand("Get_RegisteredAgents", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.Base.RegisteredAgent[]), new Type[] { typeof(Assets.BankAgent), typeof(Assets.EmployerAgent) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Assets.Base.RegisteredAgent[]).Where(agent => !string.IsNullOrWhiteSpace(agent.Name) && !agent.Inactive).ToArray();
                }
            }
        }

        public static async Task<Accounts.Venue[]> GetVenuesAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Venues", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Accounts.Venue[]));
                    var obj = serializer.ReadObject(xml);
                    return obj as Accounts.Venue[];
                }
            }
        }

        public static async Task<Accounts.Counsel[]> GetCounselAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Counsel", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Accounts.Counsel[]));
                    var obj = serializer.ReadObject(xml);
                    return obj as Accounts.Counsel[];
                }
            }
        }

        public static async Task<CertifiedMail.FirmLetterhead> GetFirmLetterHeadAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("FirmLetterhead_GetCurrent", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(CertifiedMail.FirmLetterhead));
                    var obj = serializer.ReadObject(xml);
                    return obj as CertifiedMail.FirmLetterhead;
                }
            }
        }

        #endregion

        #region Add / Update / Delete

        public static async Task<Requests.Results.Result> AddResultAsync(Enums.AssetType assetType, Enums.Status status, string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Add_Result", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", System.Data.SqlDbType.NVarChar) { Value = name });
                    cmd.Parameters.Add(new SqlParameter("@ATID", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(assetType) });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(status) });

                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.Results.Result));
                    var obj = serializer.ReadObject(xml);
                    return obj as Requests.Results.Result;
                }
            }
        }

        public static async Task<Requests.Results.Result> UpdateResultAsync(Requests.Results.Result result, bool good, bool updateAsset, bool moneyExpected)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Result", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@UID", System.Data.SqlDbType.Int) { Value = result.ID });
                    cmd.Parameters.Add(new SqlParameter("@GOOD", System.Data.SqlDbType.Bit) { Value = good });
                    cmd.Parameters.Add(new SqlParameter("@UPDATE", System.Data.SqlDbType.Bit) { Value = updateAsset });
                    cmd.Parameters.Add(new SqlParameter("@MONEY", System.Data.SqlDbType.Bit) { Value = moneyExpected });

                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.Results.Result));
                    var obj = serializer.ReadObject(xml);
                    return obj as Requests.Results.Result;
                }
            }
        }

        public static async Task<Requests.Results.Codes.MergeCode> AddMergeCodeAsync(Requests.Results.Result result, byte salesNo, byte debtor, string xcode)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Add_ResultCode", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RESULT", System.Data.SqlDbType.SmallInt) { Value = result.ID });
                    cmd.Parameters.Add(new SqlParameter("@SALES", System.Data.SqlDbType.TinyInt) { Value = salesNo });
                    cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                    cmd.Parameters.Add(new SqlParameter("@XCODE", System.Data.SqlDbType.NVarChar) { Value = xcode });

                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.Results.Codes.MergeCode));
                    var obj = serializer.ReadObject(xml);
                    return obj as Requests.Results.Codes.MergeCode;
                }
            }
        }

        public static async Task DeleteMergeCodeAsync(Requests.Results.Codes.MergeCode code)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Delete_ResultCode", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@UID", System.Data.SqlDbType.Int) { Value = code.ID });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<Requests.Results.Codes.MergeCodeFieldValue> UpdateMergeCodeFieldValueAsync(Requests.Results.Codes.MergeCode code, Requests.Results.Codes.MergeCodeField field, Requests.Results.Codes.MergeCodeInfo value)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Update_ResultCodeValue", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RCID", System.Data.SqlDbType.Int) { Value = code.ID });
                    cmd.Parameters.Add(new SqlParameter("@RCFID", System.Data.SqlDbType.TinyInt) { Value = field.ID });
                    cmd.Parameters.Add(new SqlParameter("@RCIID", System.Data.SqlDbType.Int) { Value = value.ID });

                    await conn.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                    return new Requests.Results.Codes.MergeCodeFieldValue(field, value);
                }
            }
        }

        public static async Task DeleteMergeCodeFieldValueAsync(Requests.Results.Codes.MergeCode code, Requests.Results.Codes.MergeCodeField field, Requests.Results.Codes.MergeCodeInfo value)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Delete_ResultCodeValue", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RCID", System.Data.SqlDbType.Int) { Value = code.ID });
                    cmd.Parameters.Add(new SqlParameter("@RCFID", System.Data.SqlDbType.TinyInt) { Value = field.ID });
                    cmd.Parameters.Add(new SqlParameter("@RCIID", System.Data.SqlDbType.Int) { Value = value.ID });

                    await conn.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<Requests.Results.Codes.MergeCodeInfo> AddMergeCodeInfoAsync(string description, Enums.ResultInfoType infoType, string refObject, string refParam)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Add_ResultCodeInfo", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VALUE", System.Data.SqlDbType.NVarChar) { Value = description });
                    cmd.Parameters.Add(new SqlParameter("@RCITID", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(infoType) });
                    cmd.Parameters.Add(new SqlParameter("@REF1", System.Data.SqlDbType.NVarChar) { Value = refObject });
                    cmd.Parameters.Add(new SqlParameter("@REF2", System.Data.SqlDbType.NVarChar) { Value = refParam });

                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.Results.Codes.MergeCodeInfo));
                    var obj = serializer.ReadObject(xml);
                    return obj as Requests.Results.Codes.MergeCodeInfo;
                }
            }
        }

        public static async Task<CertifiedMail.FirmLetterhead> AddNewFirmLetterhead(System.IO.Stream stream)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("FirmLetterhead_Update", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    byte[] byteStream = new byte[stream.Length];
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    await stream.ReadAsync(byteStream, 0, Convert.ToInt32(stream.Length));
                    cmd.Parameters.Add(new SqlParameter("@STREAM", System.Data.SqlDbType.VarBinary) { Value = CertifiedMail.FirmLetterhead.CompressData(byteStream) });

                    await conn.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return await GetFirmLetterHeadAsync();
        }

        #endregion

        #endregion

        #region Requests

        #region Get

        /// <summary>
        /// Get Request By ID
        /// <para>**Asset Info Not Loaded***</para>
        /// </summary>
        /// <param name="requestId">Request ID</param>
        /// <returns>Garnishment Asset Request</returns>
        public static async Task<Requests.AssetRequest> GetAccountAssetRequestByIdAsync(int requestId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequest_Request", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int) { Value = requestId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AssetRequests), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AssetRequests)?.Requests?.FirstOrDefault();
                }
            }
        }
        /// <summary>
        /// Get Request By ID
        /// <para>**Asset Info Pre-Loaded***</para>
        /// </summary>
        /// <param name="requestId">Request ID</param>
        /// <returns>Garnishment Asset Request</returns>
        public static async Task<Requests.AssetRequest> GetAccountAssetRequestInfoByIdAsync(int requestId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequestInfo_Request", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int) { Value = requestId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AccountRequestInfos), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AccountRequestInfos)?.Requests?.FirstOrDefault();
                }
            }
        }
        /// <summary>
        /// Get Requests for Asset
        /// <para>**Asset Info Not Loaded***</para>
        /// </summary>
        /// <param name="asset">Account Asset Info</param>
        /// <returns>Array of Requests</returns>
        public static async Task<Requests.AssetRequest[]> GetAccountAssetRequestsAsync(Assets.AccountAssetInfo asset)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequests_AccountAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = asset.Account.FileNo });
                    cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = asset.Debtor });
                    cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = asset.Info.ID });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AssetRequests), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AssetRequests)?.Requests;
                }
            }
        }
        /// <summary>
        /// Get Requests for Account
        /// <para>**Asset Info Pre-Loaded***</para>
        /// </summary>
        /// <param name="fileNo">Internal File No.</param>
        /// <returns>Garnishment Asset Requests</returns>
        public static async Task<Requests.AssetRequest[]> GetAccountAssetRequestInfoByFileNoAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequestsInfo_Account", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileNo });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AccountRequestInfos), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AccountRequestInfos)?.Requests;
                }
            }
        }
        /// <summary>
        /// Get Requests in Status
        /// <para>**Asset Info Not Loaded***</para>
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>Array of Requests</returns>
        public static async Task<Requests.AssetRequest[]> GetAccountAssetRequestsInStatusAsync(Enums.Status status)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequests_Status_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@STATUS", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(status) });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AssetRequests), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AssetRequests)?.Requests;
                }
            }
        }
        /// <summary>
        /// Get Requests in Status
        /// <para>(Asset Info Pre-Loaded)</para>
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>Array of Requests</returns>
        public static async Task<Requests.AssetRequest[]> GetAccountAssetRequestsInfoInStatusAsync(Enums.Status status)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequestsInfo_Status_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@STATUS", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(status) });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AccountRequestInfos), new Type[] { typeof(Requests.AssetRequest) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AccountRequestInfos)?.Requests;
                }
            }
        }
        /// <summary>
        /// Get All Requests for Asset Info
        /// <para>(Asset Info Pre-Loaded)</para>
        /// </summary>
        /// <param name="assetInfoId">Asset Info ID</param>
        /// <returns>Array of Requests</returns>
        public static async Task<Requests.InfoRequest[]> GetRequestsForAssetInfoAsync(ulong assetInfoId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAssetRequestsInfo_AssetInfo_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = assetInfoId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AssetRequestInfos), new Type[] { typeof(Requests.InfoRequest) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AssetRequestInfos)?.Requests;
                }
            }
        }
        /// <summary>
        /// Get All Active Requests for User
        /// </summary>
        /// <param name="username">User Login</param>
        /// <returns>Array of Asset Requests</returns>
        public static async Task<Requests.AssetRequest[]> GetActiveRequestsForUserAsync(string username)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAssetRequests_User_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Requests.AssetRequests), new Type[] { typeof(Requests.AssetRequest), typeof(Assets.AccountAsset), typeof(Assets.AccountAssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return (obj as Requests.AssetRequests)?.Requests;
                }
            }
        }

        #endregion

        #region Add / Update

        /// <summary>
        /// Request Garnishment for Asset
        /// </summary>
        /// <param name="fileno">File No.</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="assetInfoId">Asset Info ID</param>
        /// <param name="username">Username</param>
        /// <param name="note">Status Note</param>
        /// <param name="overrideRestrictions">Force Request - Ignore Restrictions</param>
        /// <returns>Garnishment Request ID</returns>
        public static async Task<int> RequestNewGarnishmentAsync(string fileno, byte debtor, ulong assetInfoId, string username, string note = null, bool overrideRestrictions = false)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Create_Request", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileno });
                    cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                    cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = assetInfoId });
                    cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@NOTE", System.Data.SqlDbType.NVarChar) { Value = (string.IsNullOrWhiteSpace(note) ? null : note) ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@OVERRIDE", System.Data.SqlDbType.Bit) { Value = overrideRestrictions });
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    return Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                }
            }
        }
        /// <summary>
        /// Add/Change Garnishment Status
        /// </summary>
        /// <param name="requestId">Request ID</param>
        /// <param name="status">New Status</param>
        /// <param name="username">User Who Entered Status</param>
        /// <param name="date">Data Status Entered</param>
        /// <param name="result">Result ID (optional)</param>
        /// <param name="note">Notes (optional)</param>
        /// <returns>New Status ID</returns>
        public static async Task<ulong> AddStatusAsync(int requestId, Enums.Status status, string username, DateTime date, int? result = null, string note = null)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Add_Status", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int) { Value = requestId });
                    cmd.Parameters.Add(new SqlParameter("@STATUS", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(status) });
                    cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = date });
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@RESULT", System.Data.SqlDbType.Int) { Value = result ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@NOTE", System.Data.SqlDbType.NVarChar) { Value = (string.IsNullOrWhiteSpace(note) ? null : note) ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@RSID", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToUInt64(cmd.Parameters["@RSID"].Value);
                }
            }
        }
        /// <summary>
        /// Update Garnishment Status
        /// </summary>
        /// <param name="statusId">Request ID</param>
        /// <param name="username">User Who Entered Status</param>
        /// <param name="date">Data Status Entered</param>
        /// <param name="result">Result ID (optional)</param>
        /// <param name="note">Notes (optional)</param>
        /// <returns>New Status ID</returns>
        public static async Task UpdateStatusAsync(ulong statusId, string username, DateTime date, int? result = null, string note = null)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Status", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RSID", System.Data.SqlDbType.BigInt) { Value = statusId });
                    cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = date });
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });
                    cmd.Parameters.Add(new SqlParameter("@RESULT", System.Data.SqlDbType.Int) { Value = result ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@NOTE", System.Data.SqlDbType.NVarChar) { Value = (string.IsNullOrWhiteSpace(note) ? null : note) ?? (object)DBNull.Value });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateRegisteredAgentAsync(int requestId, int? agentId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Request_RegisteredAgent", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int) { Value = requestId });
                    cmd.Parameters.Add(new SqlParameter("@RAID", System.Data.SqlDbType.Int) { Value = agentId ?? (object)DBNull.Value });

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion

        #endregion

        #region Assets

        #region Get
        /// <summary>
        /// Get All Assets for Account
        /// </summary>
        /// <param name="fileNo">File No.</param>
        /// <returns>Account Assets</returns>
        public static async Task<Assets.AccountAssets> GetAccountAssetsAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAssetInfos_FileNo", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileNo });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.AccountAssets), new[] { typeof(Assets.AssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.AccountAssets;
                }
            }
        }
        /// <summary>
        /// Get All Unverified Assets for Account
        /// </summary>
        /// <param name="fileNo">File No.</param>
        /// <returns>Unverified Account Assets</returns>
        public static async Task<Assets.Unverified.UnverifiedAccountAssets> GetUnverifiedAccountAssetsAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetUnverifiedAssetInfos_FileNo", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileNo });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.Unverified.UnverifiedAccountAssets), new[] { typeof(Assets.AssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.Unverified.UnverifiedAccountAssets;
                }
            }
        }
        /// <summary>
        /// Get Account Asset
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="assetId">Asset ID</param>
        /// <returns>Account Assets</returns>
        public static async Task<Assets.AccountAssets> GetAccountAssetAsync(int accountId, byte debtor, int assetId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAccountAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ANID", System.Data.SqlDbType.Int) { Value = accountId });
                    cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                    cmd.Parameters.Add(new SqlParameter("@AID", System.Data.SqlDbType.BigInt) { Value = assetId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.AccountAssets), new[] { typeof(Assets.AssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.AccountAssets;
                }
            }
        }
        /// <summary>
        /// Get Account Asset
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="assetInfoId">Asset Info ID</param>
        /// <returns>Account Assets</returns>
        public static async Task<Assets.AccountAssets> GetAccountAssetAsync(int accountId, byte debtor, ulong assetInfoId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAssetInfo_AccountAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ANID", System.Data.SqlDbType.Int) { Value = accountId });
                    cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                    cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = assetInfoId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.AccountAssets), new[] { typeof(Assets.AssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.AccountAssets;
                }
            }
        }
        /// <summary>
        /// Get Account Asset
        /// </summary>
        /// <param name="rid">Request ID</param>
        /// <returns>Account Assets</returns>
        public static async Task<Assets.AccountAssets> GetAccountAssetAsync(int requestId)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("GetAssetInfo_Request", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@RID", System.Data.SqlDbType.Int) { Value = requestId });

                    await conn.OpenAsync();
                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.AccountAssets), new[] { typeof(Assets.AssetInfo) });
                    var obj = serializer.ReadObject(xml);
                    return obj as Assets.AccountAssets;
                }
            }
        }
        #endregion

        #region Add / Update

        /// <summary>
        /// Add Asset Infomation for Account Debtor
        /// </summary>
        /// <param name="fileno">File No.</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="type">Asset Type</param>
        /// <param name="name">Asset Name</param>
        /// <param name="contact">Contact Name</param>
        /// <param name="location">Asset Location</param>
        /// <param name="date">Date Found</param>
        /// <param name="userName">User Found</param>
        /// <param name="phones">Phones</param>
        /// <param name="attributes">Addtl. Attributes</param>
        /// <returns>Asset Info ID</returns>
        public static async Task<ulong> AddAccountAssetAsync(string fileno, byte debtor, Enums.AssetType type, string name, string contact, Assets.Base.Location location, DateTime date, string username, IEnumerable<Assets.Base.Phone> phones, Dictionary<int, string> attributes)
        {
            if (string.IsNullOrWhiteSpace(fileno)) throw new InvalidOperationException("FileNo was not provided");
            if (attributes == null) attributes = new Dictionary<int, string>();
            using (System.Data.DataTable attrTable = new System.Data.DataTable())
            using (System.Data.DataTable phoneTable = new System.Data.DataTable())
            {
                phoneTable.Columns.Add("TYPE", typeof(byte));
                phoneTable.Columns.Add("PHONE", typeof(string));
                foreach (var phone in phones)
                {
                    var dr = phoneTable.NewRow();
                    dr["TYPE"] = phone.Type;
                    dr["PHONE"] = phone.PhoneNumber;
                    phoneTable.Rows.Add(dr);
                }
                attrTable.Columns.Add("ITID", typeof(int));
                attrTable.Columns.Add("VALUE", typeof(string));
                foreach (var attr in attributes)
                {
                    var dr = attrTable.NewRow();
                    dr["ITID"] = attr.Key;
                    dr["VALUE"] = attr.Value;
                    attrTable.Rows.Add(dr);
                }

                int aid = 0;
                int acid = 0;
                int alid = 0;
                ulong aiid = 0;
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand("GetID_Asset", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar) { Value = name });
                            cmd.Parameters.Add(new SqlParameter("@ATID", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(type) });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            aid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("GetID_Contact", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar) { Value = contact ?? "" });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            acid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("GetID_Location", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", System.Data.SqlDbType.NVarChar) { Value = location.Address1 });
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", System.Data.SqlDbType.NVarChar) { Value = location.Address2 ?? "" });
                            cmd.Parameters.Add(new SqlParameter("@CITY", System.Data.SqlDbType.NVarChar) { Value = location.City });
                            cmd.Parameters.Add(new SqlParameter("@STATE", System.Data.SqlDbType.NVarChar) { Value = location.State });
                            cmd.Parameters.Add(new SqlParameter("@ZIP", System.Data.SqlDbType.NVarChar) { Value = location.Zip });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            alid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("Update_AccountAsset", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileno });
                            cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                            cmd.Parameters.Add(new SqlParameter("@AID", System.Data.SqlDbType.Int) { Value = aid });
                            cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = DBNull.Value });
                            cmd.Parameters.Add(new SqlParameter("@CONTACT", System.Data.SqlDbType.NVarChar) { Value = contact ?? "" });
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", System.Data.SqlDbType.NVarChar) { Value = location.Address1 });
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", System.Data.SqlDbType.NVarChar) { Value = location.Address2 ?? "" });
                            cmd.Parameters.Add(new SqlParameter("@CITY", System.Data.SqlDbType.NVarChar) { Value = location.City });
                            cmd.Parameters.Add(new SqlParameter("@STATE", System.Data.SqlDbType.NVarChar) { Value = location.State });
                            cmd.Parameters.Add(new SqlParameter("@ZIP", System.Data.SqlDbType.NVarChar) { Value = location.Zip });
                            cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = date });
                            cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });
                            cmd.Parameters.Add(new SqlParameter("@PHONES", System.Data.SqlDbType.Structured) { Value = phoneTable });
                            cmd.Parameters.Add(new SqlParameter("@ATTRS", System.Data.SqlDbType.Structured) { Value = attrTable });
                            cmd.Parameters.Add(new SqlParameter("@NEW_AIID", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output });

                            await cmd.ExecuteNonQueryAsync();
                            aiid = Convert.ToUInt64(cmd.Parameters["@NEW_AIID"].Value);
                        }

                        tran.Commit();
                        return aiid;
                    }
                }
            }
        }
        /// <summary>
        /// Add Unverified Asset Infomation for Account Debtor
        /// </summary>
        /// <param name="fileno">File No.</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="type">Asset Type</param>
        /// <param name="name">Asset Name</param>
        /// <param name="contact">Contact Name</param>
        /// <param name="location">Asset Location</param>
        /// <param name="date">Date Found</param>
        /// <param name="userName">User Found</param>
        /// <param name="phones">Phones</param>
        /// <param name="attributes">Addtl. Attributes</param>
        /// <returns>Asset Info ID</returns>
        public static async Task<ulong> AddUnverifiedAccountAssetAsync(string fileno, byte debtor, Enums.AssetType type, string name, string contact, Assets.Base.Location location, DateTime date, string username, int sourceId, IEnumerable<Assets.Base.Phone> phones, Dictionary<int, string> attributes)
        {
            if (string.IsNullOrWhiteSpace(fileno)) throw new InvalidOperationException("FileNo was not provided");
            if (attributes == null) attributes = new Dictionary<int, string>();
            using (System.Data.DataTable attrTable = new System.Data.DataTable())
            using (System.Data.DataTable phoneTable = new System.Data.DataTable())
            {
                phoneTable.Columns.Add("TYPE", typeof(byte));
                phoneTable.Columns.Add("PHONE", typeof(string));
                foreach (var phone in phones)
                {
                    var dr = phoneTable.NewRow();
                    dr["TYPE"] = phone.Type;
                    dr["PHONE"] = phone.PhoneNumber;
                    phoneTable.Rows.Add(dr);
                }
                attrTable.Columns.Add("ITID", typeof(int));
                attrTable.Columns.Add("VALUE", typeof(string));
                foreach (var attr in attributes)
                {
                    var dr = attrTable.NewRow();
                    dr["ITID"] = attr.Key;
                    dr["VALUE"] = attr.Value;
                    attrTable.Rows.Add(dr);
                }

                int aid = 0;
                int acid = 0;
                int alid = 0;
                ulong aiid = 0;
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand("GetID_Asset", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar) { Value = name });
                            cmd.Parameters.Add(new SqlParameter("@ATID", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(type) });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            aid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("GetID_Contact", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar) { Value = contact ?? "" });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            acid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("GetID_Location", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", System.Data.SqlDbType.NVarChar) { Value = location.Address1 });
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", System.Data.SqlDbType.NVarChar) { Value = location.Address2 ?? "" });
                            cmd.Parameters.Add(new SqlParameter("@CITY", System.Data.SqlDbType.NVarChar) { Value = location.City });
                            cmd.Parameters.Add(new SqlParameter("@STATE", System.Data.SqlDbType.NVarChar) { Value = location.State });
                            cmd.Parameters.Add(new SqlParameter("@ZIP", System.Data.SqlDbType.NVarChar) { Value = location.Zip });
                            cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });

                            await cmd.ExecuteNonQueryAsync();
                            alid = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                        }
                        using (SqlCommand cmd = new SqlCommand("Add_UnverifiedAccountAsset", conn, tran) { CommandType = System.Data.CommandType.StoredProcedure })
                        {
                            cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileno });
                            cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                            cmd.Parameters.Add(new SqlParameter("@AID", System.Data.SqlDbType.Int) { Value = aid });
                            cmd.Parameters.Add(new SqlParameter("@ACID", System.Data.SqlDbType.Int) { Value = acid });
                            cmd.Parameters.Add(new SqlParameter("@ALID", System.Data.SqlDbType.Int) { Value = alid });
                            cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = date });
                            cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = username });
                            cmd.Parameters.Add(new SqlParameter("@SOURCEID", System.Data.SqlDbType.Int) { Value = sourceId });
                            cmd.Parameters.Add(new SqlParameter("@PHONES", System.Data.SqlDbType.Structured) { Value = phoneTable });
                            cmd.Parameters.Add(new SqlParameter("@ATTRS", System.Data.SqlDbType.Structured) { Value = attrTable });
                            cmd.Parameters.Add(new SqlParameter("@NEW_AIID", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output });

                            await cmd.ExecuteNonQueryAsync();
                            aiid = Convert.ToUInt64(cmd.Parameters["@NEW_AIID"].Value);
                        }

                        tran.Commit();
                        return aiid;
                    }
                }
            }
        }

        /// <summary>
        /// Update Asset Infomation for Account Debtor
        /// </summary>
        /// <param name="fileno">File No.</param>
        /// <param name="debtor">Debtor No.</param>
        /// <param name="assetId">Asset ID</param>
        /// <param name="assetInfoId">Asset Info ID</param>
        /// <param name="contact">Contact Name</param>
        /// <param name="location">Asset Location</param>
        /// <param name="date">Date Found</param>
        /// <param name="userName">User Found</param>
        /// <param name="phones">Phones</param>
        /// <param name="attributes">Addtl. Attributes</param>
        /// <returns>Asset Info ID</returns>
        public static async Task<ulong> UpdateAccountAssetAsync(string fileno, byte debtor, int assetId, ulong assetInfoId, string contact, Assets.Base.Location location, DateTime date, string userName, IEnumerable<Assets.Base.Phone> phones, Dictionary<int, string> attributes, bool? good = null)
        {
            if (string.IsNullOrWhiteSpace(fileno)) throw new InvalidOperationException("FileNo was not provided");
            if (phones == null) phones = new HB.Garnishments.Data.Assets.Base.Phone[0];
            if (attributes == null) attributes = new Dictionary<int, string>();
            using (System.Data.DataTable attrTable = new System.Data.DataTable())
            using (System.Data.DataTable phoneTable = new System.Data.DataTable())
            {
                phoneTable.Columns.Add("TYPE", typeof(byte));
                phoneTable.Columns.Add("PHONE", typeof(string));
                foreach (var phone in phones)
                {
                    var dr = phoneTable.NewRow();
                    dr["TYPE"] = phone.Type;
                    dr["PHONE"] = phone.PhoneNumber;
                    phoneTable.Rows.Add(dr);
                }
                attrTable.Columns.Add("ITID", typeof(int));
                attrTable.Columns.Add("VALUE", typeof(string));
                foreach (var attr in attributes)
                {
                    var dr = attrTable.NewRow();
                    dr["ITID"] = attr.Key;
                    dr["VALUE"] = attr.Value;
                    attrTable.Rows.Add(dr);
                }
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                {
                    using (SqlCommand cmd = new SqlCommand("Update_AccountAsset", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileno });
                        cmd.Parameters.Add(new SqlParameter("@DEBTOR", System.Data.SqlDbType.TinyInt) { Value = debtor });
                        cmd.Parameters.Add(new SqlParameter("@AID", System.Data.SqlDbType.Int) { Value = assetId });
                        cmd.Parameters.Add(new SqlParameter("@AIID", System.Data.SqlDbType.BigInt) { Value = assetInfoId });
                        cmd.Parameters.Add(new SqlParameter("@CONTACT", System.Data.SqlDbType.NVarChar) { Value = contact ?? "" });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS1", System.Data.SqlDbType.NVarChar) { Value = location.Address1 });
                        cmd.Parameters.Add(new SqlParameter("@ADDRESS2", System.Data.SqlDbType.NVarChar) { Value = location.Address2 ?? "" });
                        cmd.Parameters.Add(new SqlParameter("@CITY", System.Data.SqlDbType.NVarChar) { Value = location.City });
                        cmd.Parameters.Add(new SqlParameter("@STATE", System.Data.SqlDbType.NVarChar) { Value = location.State });
                        cmd.Parameters.Add(new SqlParameter("@ZIP", System.Data.SqlDbType.NVarChar) { Value = location.Zip });
                        cmd.Parameters.Add(new SqlParameter("@DATE", System.Data.SqlDbType.DateTime) { Value = date });
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", System.Data.SqlDbType.NVarChar) { Value = userName });
                        cmd.Parameters.Add(new SqlParameter("@GOOD", System.Data.SqlDbType.Bit) { Value = good ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@PHONES", System.Data.SqlDbType.Structured) { Value = phoneTable });
                        cmd.Parameters.Add(new SqlParameter("@ATTRS", System.Data.SqlDbType.Structured) { Value = attrTable });
                        cmd.Parameters.Add(new SqlParameter("@NEW_AIID", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return Convert.ToUInt64(cmd.Parameters["@NEW_AIID"].Value);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Database Checks

        public static async Task<bool> CheckFileNoAsync(string fileNo)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(FILENO) FROM MASTER WHERE FILENO=@FILENO", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@FILENO", System.Data.SqlDbType.NVarChar) { Value = fileNo });

                    await conn.OpenAsync();
                    var count = await cmd.ExecuteScalarAsync();

                    return Convert.ToInt32(count) > 0;
                }
            }
        }

        #endregion
    }
}
