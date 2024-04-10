using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Search
{
    public class SearchHandler
    {
        public static async Task<SearchNameResult[]> SearchAssetNameAsync(string name, Enums.AssetType type)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Search_AssetName", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = name });
                    cmd.Parameters.Add(new SqlParameter("@ATID", SqlDbType.TinyInt) { Value = Convert.ToByte(type) });

                    await conn.OpenAsync();
                    var xmlReader = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(SearchNameResult[]));
                    return serializer.ReadObject(xmlReader) as SearchNameResult[];
                }
            }
        }
        public static async Task<SearchNameResult[]> SearchAssetContactAsync(string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Search_AssetContact", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = name });

                    await conn.OpenAsync();
                    var xmlReader = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(SearchNameResult[]));
                    return serializer.ReadObject(xmlReader) as SearchNameResult[];
                }
            }
        }
        public static async Task<SearchNameResult[]> SearchAssetCityAsync(string name)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Search_AssetCity", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = name });

                    await conn.OpenAsync();
                    var xmlReader = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(SearchNameResult[]));
                    return serializer.ReadObject(xmlReader) as SearchNameResult[];
                }
            }
        }
        public static async Task<Assets.Base.Location[]> SearchAddressAsync(string add)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Search_AssetLocation", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ADDRESS", SqlDbType.NVarChar) { Value = add });

                    await conn.OpenAsync();
                    var xmlReader = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Assets.Base.Location[]));
                    return serializer.ReadObject(xmlReader) as Assets.Base.Location[];
                }
            }
        }
    }
}
