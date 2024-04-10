using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Walz.Data
{
    [DataContract(Name = "Letter", Namespace = "")]
    public class Letter
    {
        [DataMember(Name = "ID")]
        public short ID { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; set; }
        [DataMember(Name = "WEIGHT")]
        public decimal Weight { get; set; }

        internal Letter(string Description, decimal Weight)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("LetterSize_Add", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 50).Value = Description;
                    cmd.Parameters.Add("@WEIGHT", SqlDbType.Decimal, 4).Value = Weight;
                    cmd.Parameters.Add("RETURN_VALUE", SqlDbType.SmallInt).Direction = ParameterDirection.ReturnValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    this.ID = Convert.ToInt16(cmd.Parameters["RETURN_VALUE"].Value);
                    this.Description = Description;
                    this.Weight = Weight;
                }
            }
        }
        internal Letter(IDataReader dr)
        {
            this.ID = Convert.ToInt16(dr["ID"]);
            this.Description = dr["DESCRIPTION"].ToString();
            this.Weight = Convert.ToDecimal(dr["WEIGHT"]);
        }

        public void Update(string Description, decimal Weight)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("LetterSize_Update", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@LID", SqlDbType.SmallInt).Value = this.ID;
                    cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 50).Value = Description;
                    cmd.Parameters.Add("@WEIGHT", SqlDbType.Decimal, 4).Value = Weight;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    this.Description = Description;
                    this.Weight = Weight;
                }
            }
        }
        internal void Delete()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("LetterSize_Remove", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@LID", SqlDbType.SmallInt).Value = this.ID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}",
                this.ID,
                this.Description);
        }
    }
}
