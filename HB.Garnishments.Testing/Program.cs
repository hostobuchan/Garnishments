using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountAssets = Data.DataHandler.GetAccountAssetsAsync("311958").Result;

            Data.Serialization.Serializer.Save(@"C:\Users\bmoorjani\Documents\XMLMyFile.xml", accountAssets);

            Console.WriteLine("writing an xml file in C:\\Users\\bmoorjani\\Documents\\XMLMyFile.xml  for file 311958");
            var results = Data.Serialization.Serializer.Read(@"C:\Users\bmoorjani\Documents\XMLMyFile.xml");
        }
    }
}
