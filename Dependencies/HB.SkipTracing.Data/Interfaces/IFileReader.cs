using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Interfaces
{
    public interface IFileReader<in T, K> where T : Records.UploadRecord where K : Records.DownloadRecord
    {
        void CreateUploadFile(System.IO.FileInfo file, IEnumerable<T> uploadRecords, Action<int> progress = null);
        Task CreateUploadFileAsync(System.IO.FileInfo file, IEnumerable<T> uploadRecords, Action<int> progress = null);

        IEnumerable<K> ReadReturnFile(System.IO.FileInfo file);
        Task<IEnumerable<K>> ReadReturnFileAsync(System.IO.FileInfo file);
    }

    public static partial class Extensions
    {
        public static void CreateUploadFile<T, K>(this IFileReader<T, K> reader, string fileName, IEnumerable<T> uploadRecords, Action<int> progress = null) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            reader.CreateUploadFile(new System.IO.FileInfo(fileName), uploadRecords, progress);
        }
        public static async Task CreateUploadFileAsync<T, K>(this IFileReader<T, K> reader, string fileName, IEnumerable<T> uploadRecords, Action<int> progress = null) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            await reader.CreateUploadFileAsync(new System.IO.FileInfo(fileName), uploadRecords, progress);
        }

        public static IEnumerable<K> ReadReturnFile<T, K>(this IFileReader<T, K> reader, string fileName) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            return reader.ReadReturnFile(new System.IO.FileInfo(fileName));
        }
        public static async Task<IEnumerable<K>> ReadReturnFileAsync<T, K>(this IFileReader<T, K> reader, string fileName) where T : Records.UploadRecord where K : Records.DownloadRecord
        {
            return await reader.ReadReturnFileAsync(new System.IO.FileInfo(fileName));
        }
    }
}
