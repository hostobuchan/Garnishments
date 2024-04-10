using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Batches
{
    public class BatchInfo
    {
        public int ID { get; private set; }
        public DateTime BatchDate { get; private set; }
        public short Items { get; private set; }
        public decimal Cost { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        internal protected BatchInfo(int ID, DateTime BatchDate, short Items, decimal Cost, DateTime? ReturnDate = null)
        {
            this.ID = ID;
            this.BatchDate = BatchDate;
            this.Items = Items;
            this.Cost = Cost;
            this.ReturnDate = ReturnDate;
        }
        internal protected BatchInfo(IDataReader dr)
        {
            this.ID = Convert.ToInt32(dr["ID"]);
            this.BatchDate = Convert.ToDateTime(dr["BATCH_DATE"]);
            this.Items = Convert.ToInt16(dr["ITEMS"]);
            this.Cost = Convert.ToDecimal(dr["COST"]);
            this.ReturnDate = dr["RETURN_DATE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["RETURN_DATE"]);
        }
        internal protected BatchInfo(short Items, decimal Cost)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("Batch_Create", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@ITEMS", System.Data.SqlDbType.SmallInt).Value = Items;
                    cmd.Parameters.Add("@COST", System.Data.SqlDbType.Decimal, 5).Value = Cost;
                    cmd.Parameters.Add("RETURN_VALUE", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    cmd.ExecuteNonQuery();

                    this.ID = Convert.ToInt32(cmd.Parameters["RETURN_VALUE"].Value);
                    this.BatchDate = DateTime.Today;
                    this.Items = Items;
                    this.Cost = Cost;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1:yyyy-MM-dd}: {2:C}",
                this.ID,
                this.BatchDate,
                this.Cost);
        }
    }
}
