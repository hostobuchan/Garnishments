using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Zips
{
    [DataContract(Name = "Zip", Namespace = "")]
    public struct Zip
    {
        [DataMember(Name = "ZIP_CODE")]
        public string ZipCode { get; private set; }
        [DataMember(Name = "PREF_CITY_STATE_NAME")]
        public string PrefCityStateName { get; private set; }
        [DataMember(Name = "STATE_ABBREV")]
        public string StateAbrev { get; private set; }
        [DataMember(Name = "STATE")]
        public string State { get; private set; }
        [DataMember(Name = "STATE_FIPS")]
        public int? StateFIPS { get; private set; }
        [DataMember(Name = "COUNTY_NAME")]
        public string County { get; private set; }
        [DataMember(Name = "COUNTY_FIPS")]
        public string CountyFIPS { get; private set; }
        [DataMember(Name = "ZIP_CLASS_CODE")]
        public string ClassCode { get; private set; }
        [DataMember(Name = "CARRIER_ROUTE_RATE")]
        public string CarrierRate { get; private set; }


        public static async Task<Zip[]> GetAllZipsAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Zips_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Zip[]));
                    var obj = serializer.ReadObject(xml);
                    return obj as Zip[];
                }
            }
        }

        public static bool operator ==(Zip obj1, Zip obj2)
        {
            return string.Equals(obj1.ZipCode?.Trim(), obj2.ZipCode?.Trim())
                && string.Equals(obj1.PrefCityStateName?.Trim(), obj2.PrefCityStateName?.Trim())
                && string.Equals(obj1.StateAbrev?.Trim(), obj2.StateAbrev?.Trim())
                && string.Equals(obj1.State?.Trim(), obj2.State?.Trim())
                && obj1.StateFIPS == obj2.StateFIPS
                && string.Equals(obj1.County?.Trim(), obj2.County?.Trim())
                && string.Equals(obj1.CountyFIPS?.Trim(), obj2.CountyFIPS?.Trim())
                && string.Equals(obj1.ClassCode?.Trim(), obj2.ClassCode?.Trim())
                && string.Equals(obj1.CarrierRate?.Trim(), obj2.CarrierRate?.Trim());
        }
        public static bool operator !=(Zip obj1, Zip obj2)
        {
            return !string.Equals(obj1.ZipCode?.Trim(), obj2.ZipCode?.Trim())
                || !string.Equals(obj1.PrefCityStateName?.Trim(), obj2.PrefCityStateName?.Trim())
                || !string.Equals(obj1.StateAbrev?.Trim(), obj2.StateAbrev?.Trim())
                || !string.Equals(obj1.State?.Trim(), obj2.State?.Trim())
                || obj1.StateFIPS != obj2.StateFIPS
                || !string.Equals(obj1.County?.Trim(), obj2.County?.Trim())
                || !string.Equals(obj1.CountyFIPS?.Trim(), obj2.CountyFIPS?.Trim())
                || !string.Equals(obj1.ClassCode?.Trim(), obj2.ClassCode?.Trim())
                || !string.Equals(obj1.CarrierRate?.Trim(), obj2.CarrierRate?.Trim());
        }
    }
}
