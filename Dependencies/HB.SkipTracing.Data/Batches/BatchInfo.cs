using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Batches
{
    public class BatchInfo
    {
        public int ID { get; private set; }
        public int Vendor { get; private set; }
        public string UploadFile { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public Enums.AssetType SearchType { get; private set; }

        public BatchInfo(IDataReader dr)
        {
            this.ID = Convert.ToInt32(dr["ID"]);
            this.Vendor = Convert.ToInt32(dr["VID"]);
            this.SearchType = (Enums.AssetType)Enum.ToObject(typeof(Enums.AssetType), Convert.ToByte(dr["PRODUCT"]));
            this.UploadFile = $"{dr["FILENAME"]}";
            this.CreationDate = dr["CREATE_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CREATE_DATE"]);
            this.ReturnDate = dr["RETURN_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RETURN_DATE"]);
        }

        public override string ToString()
        {
            return $"[{this.ID:0000}] {this.CreationDate:yyyy-MM-dd} -- {this.UploadFile}";
        }
    }
}
