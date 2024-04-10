using System;
using System.Collections.Generic;
using System.IO;
using CLS = RecordTypes.CLS;
using OldFileReader = RecordTypes.FileReaders;

namespace RecordTypes2.FileReaders
{
    public class FileReader<T> : OldFileReader.FileReader, IDisposable where T : RecordTypes.EDI.EDIRecords.RecordType<T>
    {
        private RecordTypes.SupportedEDITypes EDIType;
        private RecordTypes.EDI.EDIRecords.RecordType<T> RecordType;

        //////////////////////////////////////
        /// 
        ///              NOT USEFULL 
        /// 
        //public static OldFileReader.FileReader GetFileReader(RecordTypes.SupportedEDITypes EDIType)
        //{
        //    Type rt = RecordTypes.EDITypeFinder.GetBaseRecord(EDIType);
        //    return typeof(FileReader<>).MakeGenericType(new[] { rt }).GetConstructor(new[] { typeof(RecordTypes.SupportedEDITypes) }).Invoke(new object[] { EDIType }) as OldFileReader.FileReader;
        //}
        //public static OldFileReader.FileReader GetFileReader(string FileLocation, RecordTypes.SupportedEDITypes EDIType)
        //{
        //    Type rt = RecordTypes.EDITypeFinder.GetBaseRecord(EDIType);
        //    return typeof(FileReader<>).MakeGenericType(new[] { rt }).GetConstructor(new[] { typeof(string), typeof(RecordTypes.SupportedEDITypes) }).Invoke(new object[] { FileLocation, EDIType }) as OldFileReader.FileReader;
        //}
        //public static OldFileReader.FileReader GetFileReader(Stream FileStream, RecordTypes.SupportedEDITypes EDIType)
        //{
        //    Type rt = RecordTypes.EDITypeFinder.GetBaseRecord(EDIType);
        //    return typeof(FileReader<>).MakeGenericType(new[] { rt }).GetConstructor(new[] { typeof(Stream), typeof(RecordTypes.SupportedEDITypes) }).Invoke(new object[] { FileStream, EDIType }) as OldFileReader.FileReader;
        //}

        public FileReader(RecordTypes.SupportedEDITypes EDIType)
        {
            this.EDIType = EDIType;
            this.RecordType = (new RecordInstance<T>(EDIType)).GetRecordInstance();
        }
        public FileReader(string FileLocation, RecordTypes.SupportedEDITypes EDIType) : this(EDIType)
        {
            this.FileLocation = FileLocation;
            this.FR = new StreamReader(this.FileLocation);
        }
        public FileReader(Stream FileStream, RecordTypes.SupportedEDITypes EDIType) : this(EDIType)
        {
            this.FileLocation = "";
            this.FR = new StreamReader(FileStream);
        }

        public T ReadRecord()
        {
            return this.RecordType.GetRecordType(this.FR.ReadLine());
        }

        public List<T> ReadFile()
        {
            List<T> file = new List<T>();
            try
            {
                this.FR.BaseStream.Seek(0, SeekOrigin.Begin);
                while (!this.EOF)
                {
                    OnFileReadProgress((int)(this.FR.BaseStream.Position * 100 / this.FR.BaseStream.Length));
                    file.Add(ReadRecord());
                }
                file.RemoveAll(el => el == null);
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAccountRecords(CLS.Account Account, List<T> Records)
        {
            return this.RecordType.GetAccountRecords(Account, Records);
        }

        public void BasicFileMaintenance(List<T> Records)
        {
            this.RecordType.BasicFileMaintenance(Records);
        }

        public void AddHeaders(List<T> BaseList, List<T> AddList)
        {
            this.RecordType.AddHeaders(BaseList, AddList);
        }

        public void WriteNewFile(string FileName, List<T> Records)
        {
            StreamWriter sw = new StreamWriter(FileName);
            int cnt = 0;
            int ttl = Records.Count;
            foreach (T record in Records)
            {
                cnt++;
                OnFileWriteProgress(cnt * 100 / ttl);
                sw.WriteLine(record);
            }
            sw.Flush();
            sw.Close();
        }

        public T PlacementRecord(List<T> Records, string AccountNumber)
        {
            return this.RecordType.PlacementRecord(Records, AccountNumber);
        }

        public int NewAccounts(List<T> Records)
        {
            return this.RecordType.NewAccounts(Records);
        }

        public IEnumerable<List<T>> UniqueAccountListing(List<T> Records)
        {
            return this.RecordType.UniqueAccountListing(Records);
        }

        public void Dispose()
        {
            this.FR?.Dispose();
        }
    }

    #region Disambiguate Abstract Classes

    class RecordInstance<T> where T : RecordTypes.EDI.EDIRecords.RecordType<T>
    {
        private RecordTypes.SupportedEDITypes EDIType;

        public RecordInstance(RecordTypes.SupportedEDITypes EDIType)
        {
            this.EDIType = EDIType;
        }

        public RecordTypes.EDI.EDIRecords.RecordType<T> GetRecordInstance()
        {
            switch (this.EDIType)
            {
                case RecordTypes.SupportedEDITypes.YGC:
                    return (RecordTypes.EDI.EDIRecords.RecordType<T>)new OldFileReader.YGCBaseRecord("01");
                case RecordTypes.SupportedEDITypes.Citi:
                    return (RecordTypes.EDI.EDIRecords.RecordType<T>)new OldFileReader.CitiAccount("00000000000000000000A00");
                case RecordTypes.SupportedEDITypes.Ford:
                    return (RecordTypes.EDI.EDIRecords.RecordType<T>)new OldFileReader.FordAccount("00000000000000000000A00");
                case RecordTypes.SupportedEDITypes.RMS:
                    return (RecordTypes.EDI.EDIRecords.RecordType<T>)new OldFileReader.RMSAccount("00000000000000000000A00");


                default:
                    throw new NotImplementedException();
            }
        }
    }

    #endregion
}
