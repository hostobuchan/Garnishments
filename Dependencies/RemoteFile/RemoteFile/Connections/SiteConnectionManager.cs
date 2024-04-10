using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RemoteFile.Connections
{
    public class SiteConnectionManager
    {
        public List<SiteLogin> SiteLogins { get; private set; }

        public SiteConnectionManager()
        {
            this.SiteLogins = new List<SiteLogin>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SFTP", conn))
                {
                    conn.Open();
                    var sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        var newSiteLogin = new SiteLogin(sdr);
                        this.SiteLogins.Add(newSiteLogin);
                    }
                }
            }
        }







        public static async Task<SiteLogin> FindLoginByNameAsync(string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SFTP WHERE SFTP_NAME Like @NAME", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = name });

                    await conn.OpenAsync();
                    var sdr = await cmd.ExecuteReaderAsync();
                    if (await sdr.ReadAsync())
                    {
                        return new SiteLogin(sdr);
                    }
                }
            }
            return null;
        }
    }
}
