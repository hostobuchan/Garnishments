using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Serialization
{
    public class Serializer
    {
        public static void Save(string location, Assets.AccountAssets assets)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(location, System.IO.FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Assets.AccountAssets));
                ser.WriteObject(fs, assets);
            }
        }
        public static Assets.AccountAssets Read(string location)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(location, System.IO.FileMode.Open))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Assets.AccountAssets));
                var obj = ser.ReadObject(fs);
                return obj as Assets.AccountAssets;
            }
        }
    }
}
