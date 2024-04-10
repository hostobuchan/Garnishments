using System;
using System.Data;
using System.Data.SqlClient;

namespace RemoteFile.Connections
{
    public class SiteLogin
    {
        #region Public Properties
        public int UID { get; private set; }
        public string FtpName { get; private set; }
        public string FtpSite { get; private set; }
        public int? FtpPort { get; private set; }
        public string FtpDirectory { get; private set; }
        public string FtpUserName { get; private set; }
        public string FtpPassword { get; private set; }
        public string OperationUserName { get; private set; }
        public string OperationPassword { get; private set; }
        public Enums.ConnectionMethod ProtocolUsed { get; private set; }
        #endregion

        // Loads Instance of Login Info for Specified Entry
        public SiteLogin(int ftpID)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SFTP WHERE SFTP_ID=@SFTPID", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@SFTPID", SqlDbType.Int) { Value = ftpID });

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        UID = Convert.ToInt32(sdr["SFTP_ID"]);
                        FtpName = $"{sdr["SFTP_NAME"] ?? ""}";
                        FtpSite = $"{sdr["SFTP_SITE"] ?? ""}";
                        FtpPort = sdr["SFTP_PORT"] == DBNull.Value ? (int?)null : Convert.ToInt32(sdr["SFTP_PORT"]);
                        FtpDirectory = $"{sdr["SFTP_DIR"] ?? ""}";
                        FtpUserName = $"{sdr["SFTP_USER"] ?? ""}";
                        FtpPassword = $"{sdr["SFTP_PASS"] ?? ""}";
                        OperationUserName = $"{sdr["UPLOAD_USER"] ?? ""}";
                        OperationPassword = $"{sdr["UPLOAD_PASS"] ?? ""}";
                        ProtocolUsed = (Enums.ConnectionMethod)Enum.ToObject(typeof(Enums.ConnectionMethod), Convert.ToByte(sdr["PROTOCOL"]));
                    }
                    else
                    {
                        throw new InvalidOperationException($"No Entry Exists for UID \"{ftpID}\"");
                    }
                }
            }
        }
        // Loads Instance of Login From SQLDataReader
        public SiteLogin(SqlDataReader sdr)
        {
            UID = Convert.ToInt32(sdr["SFTP_ID"]);
            FtpName = $"{sdr["SFTP_NAME"] ?? ""}";
            FtpSite = $"{sdr["SFTP_SITE"] ?? ""}";
            FtpPort = sdr["SFTP_PORT"] == DBNull.Value ? (int?)null : Convert.ToInt32(sdr["SFTP_PORT"]);
            FtpDirectory = $"{sdr["SFTP_DIR"] ?? ""}";
            FtpUserName = $"{sdr["SFTP_USER"] ?? ""}";
            FtpPassword = $"{sdr["SFTP_PASS"] ?? ""}";
            OperationUserName = $"{sdr["UPLOAD_USER"] ?? ""}";
            OperationPassword = $"{sdr["UPLOAD_PASS"] ?? ""}";
            ProtocolUsed = (Enums.ConnectionMethod)Enum.ToObject(typeof(Enums.ConnectionMethod), Convert.ToByte(sdr["PROTOCOL"]));
        }
        // Creates New Instance of Login for Given Information and Creates Database Entry
        public SiteLogin(string ftpName, string ftpSite, int? port, string ftpDirectory, string ftpUser, string ftpPassword, string operationUser, string operationPass, Enums.ConnectionMethod protocol = Enums.ConnectionMethod.SFTP)
        {
            this.FtpName = ftpName;
            this.FtpSite = ftpSite;
            this.FtpPort = port;
            this.FtpDirectory = ftpDirectory;
            this.FtpUserName = ftpUser;
            this.FtpPassword = ftpPassword;
            this.OperationUserName = operationUser;
            this.OperationPassword = operationPass;
            this.ProtocolUsed = protocol;
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.SFTP_Add_Site", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("RETURN_VALUE", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue });
                    cmd.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar, 20) { Value = this.FtpName });
                    cmd.Parameters.Add(new SqlParameter("@SITE", System.Data.SqlDbType.NVarChar, 50) { Value = this.FtpSite });
                    cmd.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.Int) { Value = this.FtpPort });
                    cmd.Parameters.Add(new SqlParameter("@DIR", System.Data.SqlDbType.NVarChar, 30) { Value = this.FtpDirectory });
                    cmd.Parameters.Add(new SqlParameter("@USER", System.Data.SqlDbType.NVarChar, 30) { Value = this.FtpUserName });
                    cmd.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar, 16) { Value = this.FtpPassword });
                    cmd.Parameters.Add(new SqlParameter("@UPUSER", System.Data.SqlDbType.NVarChar, 30) { Value = this.OperationUserName });
                    cmd.Parameters.Add(new SqlParameter("@UPPASS", System.Data.SqlDbType.NVarChar, 16) { Value = this.OperationPassword });
                    cmd.Parameters.Add(new SqlParameter("@PROT", System.Data.SqlDbType.TinyInt) { Value = this.ProtocolUsed });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    this.UID = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                }
            }
        }
        // Creates Instance of Login with Given Information // Database is Unaltered
        public SiteLogin(int uid, string ftpName, string ftpSite, int? port, string ftpDirectory, string ftpUser, string ftpPassword, string operationUser, string operationPass, Enums.ConnectionMethod protocol = Enums.ConnectionMethod.SFTP)
        {
            this.UID = uid;
            this.FtpName = ftpName;
            this.FtpSite = ftpSite;
            this.FtpPort = port;
            this.FtpDirectory = ftpDirectory;
            this.FtpUserName = ftpUser;
            this.FtpPassword = ftpPassword;
            this.OperationUserName = operationUser;
            this.OperationPassword = operationPass;
            this.ProtocolUsed = protocol;
        }

        public override string ToString()
        {
            return this.FtpName;
        }

        public void SaveChanges()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE SFTP SET SFTP_NAME=@NAME, SFTP_SITE=@SITE, SFTP_PORT=@PORT, SFTP_DIR=@DIR, SFTP_USER=@USER, SFTP_PASS=@PASS, UPLOAD_USER=@UPUSER, UPLOAD_PASS=@UPPASS, PROTOCOL=@PROT WHERE SFTP_ID=@UID", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UID", SqlDbType.Int) { Value = this.UID });
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = this.FtpName });
                    cmd.Parameters.Add(new SqlParameter("@SITE", System.Data.SqlDbType.NVarChar, 50) { Value = this.FtpSite });
                    cmd.Parameters.Add(new SqlParameter("@PORT", System.Data.SqlDbType.Int) { Value = this.FtpPort });
                    cmd.Parameters.Add(new SqlParameter("@DIR", System.Data.SqlDbType.NVarChar, 30) { Value = this.FtpDirectory });
                    cmd.Parameters.Add(new SqlParameter("@USER", System.Data.SqlDbType.NVarChar, 30) { Value = this.FtpUserName });
                    cmd.Parameters.Add(new SqlParameter("@PASS", System.Data.SqlDbType.NVarChar, 16) { Value = this.FtpPassword });
                    cmd.Parameters.Add(new SqlParameter("@UPUSER", System.Data.SqlDbType.NVarChar, 30) { Value = this.OperationUserName });
                    cmd.Parameters.Add(new SqlParameter("@UPPASS", System.Data.SqlDbType.NVarChar, 16) { Value = this.OperationPassword });
                    cmd.Parameters.Add(new SqlParameter("@PROT", System.Data.SqlDbType.TinyInt) { Value = this.ProtocolUsed });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Interfaces.SiteConnectionInterface GetClient()
        {
            switch (this.ProtocolUsed)
            {
                case Enums.ConnectionMethod.SFTP:
                    return new Methods.SFTP.SFTP(this);
                case Enums.ConnectionMethod.FTP:
                    return new Methods.FTP.FTP(this);
                case Enums.ConnectionMethod.FTPS:
                case Enums.ConnectionMethod.FTPS_Explicit:
                    return new Methods.FTPS.FTPS(this);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

