using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CertifiedMail
{
    [DataContract(Name = "FLH", Namespace = "")]
    public class FirmLetterhead
    {
        [DataMember(Name = "ID")]
        public int ID { get; private set; }
        [DataMember(Name = "DATE")]
        public DateTime Date { get; private set; }
        [DataMember(Name = "STREAM")]
        public byte[] EncodedStream { get; private set; }



        public System.IO.MemoryStream GetDataStream()
        {
            return new System.IO.MemoryStream(DecompressData(EncodedStream));
        }

        public static byte[] CompressData(byte[] data)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
                {
                    using (System.IO.MemoryStream ucStream = new System.IO.MemoryStream(data))
                    {
                        ucStream.CopyTo(zip);
                    }
                }

                ms.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] compData = new byte[ms.Length];
                ms.Read(compData, 0, (int)ms.Length);
                return compData;
            }
        }
        public static byte[] DecompressData(byte[] data)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
            {
                using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress, true))
                {
                    using (System.IO.MemoryStream ms2 = new System.IO.MemoryStream())
                    {
                        zip.CopyTo(ms2);

                        ms2.Seek(0, System.IO.SeekOrigin.Begin);
                        byte[] deCompData = new byte[ms2.Length];
                        ms2.Read(deCompData, 0, (int)ms2.Length);
                        return deCompData;
                    }
                }
            }
        }
    }
}
