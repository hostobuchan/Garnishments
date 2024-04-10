using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public static class FileManager
    {
        public static List<FileType> GetFileTypes(System.IO.DirectoryInfo Directory)
        {
            return GetFileTypes(Directory.GetFiles());
        }
        public static List<FileType> GetFileTypes(IEnumerable<System.IO.FileInfo> Files)
        {
            return GetFileTypes(Files.Select(el => el.FullName));
        }
        public static List<FileType> GetFileTypes(IEnumerable<string> FileNames)
        {
            List<FileType> files = new List<FileType>();
            foreach (string file in FileNames)
            {
                files.Add(new FileType(file));
            }
            return files;
        }

        public static List<BaseFile> GetFiles(System.IO.DirectoryInfo Directory)
        {
            return GetFiles(GetFileTypes(Directory));
        }
        public static List<BaseFile> GetFiles(IEnumerable<System.IO.FileInfo> Files)
        {
            return GetFiles(GetFileTypes(Files));
        }
        public static List<BaseFile> GetFiles(IEnumerable<string> FileNames)
        {
            return GetFiles(GetFileTypes(FileNames));
        }
        public static List<BaseFile> GetFiles(IEnumerable<FileType> FileTypes)
        {
            List<BaseFile> Files = new List<BaseFile>();
            foreach (FileType file in FileTypes)
            {
                switch (file.Type)
                {
                    case Enums.FileType.ReceiptFile:
                        Files.Add(new Files.ReceiptFile(file));
                        break;
                    case Enums.FileType.BarcodeFile:
                        Files.Add(new Files.BarcodesFile(file));
                        break;
                    case Enums.FileType.ImagesFile:
                        Files.Add(new Files.ImagesFile(file));
                        break;
                }
            }
            return Files;
        }

        public static System.IO.MemoryStream CreateUploadFile(List<Records.UploadRecord> Records, Batches.BatchInfo Batch, Enums.FileVersion fileVersion = Enums.FileVersion.FileVersion3m)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ms);

            sw.WriteLine(GetHeaderLine(fileVersion));

            foreach (Records.UploadRecord record in Records)
            {
                record.BatchID = Batch.ID;
                sw.WriteLine(record.ToString(fileVersion));
            }

            sw.Flush();
            ms.Seek(0, System.IO.SeekOrigin.Begin);

            return ms;
        }

        public static string GetUploadFileName(string CompanyID, Batches.BatchInfo Batch, Enums.FileVersion fileVersion = Enums.FileVersion.FileVersion3m)
        {
            return string.Format("{0}_AL_{1:yyyyMMddHHmmss}ID{2}_{3}.txt",
                CompanyID,
                Batch.BatchDate,
                Batch.ID.ToString().PadLeft(4, '0'),
                Dictionaries.FileVersionStrings[fileVersion]
                );
        }

        private static string GetHeaderLine(Enums.FileVersion fileVersion)
        {
            switch (fileVersion)
            {
                case Enums.FileVersion.FileVersion3:
                case Enums.FileVersion.FileVersion3m:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}",
                        "ReferenceNumber",
                        "Name1",
                        "Name2",
                        "Address1",
                        "Address2",
                        "City",
                        "State",
                        "Zip",
                        "BatchID",
                        "ReturnReceiptType",
                        "RestrictedDelivery",
                        "FormType",
                        "Sender",
                        "ChargeAmount",
                        "ChargeTo",
                        "Weight",
                        "Flat"
                        );
                    break;
                case Enums.FileVersion.FileVersion3r:
                case Enums.FileVersion.FileVersion3rm:
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}",
                        "ReferenceNumber",
                        "Name1",
                        "Name2",
                        "Address1",
                        "Address2",
                        "City",
                        "State",
                        "Zip",
                        "BatchID",
                        "ReturnReceiptType",
                        "RestrictedDelivery",
                        "FormType",
                        "Sender",
                        "ChargeAmount",
                        "ChargeTo",
                        "ReturnName",
                        "ReturnAddress1",
                        "ReturnAddress2",
                        "ReturnCity",
                        "ReturnState",
                        "ReturnZip",
                        "Weight",
                        "Flat"
                        );
                    break;
                case Enums.FileVersion.FileVersion1:
                case Enums.FileVersion.FileVersion2:
                case Enums.FileVersion.FileVersion2m:
                case Enums.FileVersion.FileVersion2r:
                case Enums.FileVersion.FileVersion2rm:
                default:
                    throw new NotImplementedException(string.Format("Selected Format Not Supported \"{0}\"", fileVersion));
            }
        }
    }
}
