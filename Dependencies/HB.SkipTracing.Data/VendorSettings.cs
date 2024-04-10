using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data
{
    public interface IVendorSettings<out T, K, S> where T : Interfaces.IDataHandler<K, S> where K : Records.UploadRecord where S : Records.DownloadRecord
    {
        T DataHandler { get; }
        Batches.BatchHandler<K, S> BatchHandler { get; }
        Interfaces.IFileReader<K, S> FileReader { get; }
    }
    public class VendorSettings<T, K, S> : IVendorSettings<T, K, S> where K : Records.UploadRecord where S : Records.DownloadRecord where T : Interfaces.IDataHandler<K, S>
    {
        public T DataHandler { get; private set; }
        public Batches.BatchHandler<K, S> BatchHandler { get; private set; }
        public Interfaces.IFileReader<K, S> FileReader { get; private set; }

        public VendorSettings(T dataHandler, Batches.BatchHandler<K, S> batchHandler, Interfaces.IFileReader<K, S> fileReader)
        {
            this.DataHandler = dataHandler;
            this.BatchHandler = batchHandler;
            this.FileReader = fileReader;
        }
    }
}
