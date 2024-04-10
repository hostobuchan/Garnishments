using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NANBase = RecordTypes.NAN.Base;
using NCOBase = RecordTypes.NCO.Base;
using PLXBase = RecordTypes.PLX.Base;
using RMSBase = RecordTypes.RMS.Base;
using YGCBase = RecordTypes.YGC.Base;

namespace RecordTypes
{
    namespace FileReaders
    {
        #region File Readers

        public abstract class FileReader
        {
            public delegate void FileReadProgressHandler(int Progress);
            public delegate void FileWriteProgressHandler(int Progress);
            public event FileReadProgressHandler FileReadProgress;
            public event FileWriteProgressHandler FileWriteProgress;
            protected void OnFileReadProgress(int Progress)
            {
                if (FileReadProgress != null) FileReadProgress(Progress);
            }
            protected void OnFileWriteProgress(int Progress)
            {
                if (FileWriteProgress != null) FileWriteProgress(Progress);
            }

            protected string FileLocation { get; set; }
            protected StreamReader FR { get; set; }

            public bool EOF { get { return this.FR.EndOfStream; } }
        }
        public class FileReader<T> : FileReader
        {
            private SupportedEDITypes EDIType;

            public FileReader(SupportedEDITypes EDIType)
            {
                this.EDIType = EDIType;
            }
            public FileReader(string FileLocation, SupportedEDITypes EDIType)
            {
                this.FileLocation = FileLocation;
                this.EDIType = EDIType;
                this.FR = new StreamReader(this.FileLocation);
            }

            public object ReadRecord()
            {
                Type t = RecordTypeIDFinder.GetRecordTypeID(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("GetRecordType", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                return mi.Invoke(null, new object[] { this.FR.ReadLine() });
            }

            private void AddRecord(List<T> List, T Record)
            {
                List.Add(Record);
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
                        System.Reflection.MethodInfo mi = this.GetType().GetMethod("AddRecord", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        mi.Invoke(this, new object[] { file, ReadRecord() });
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
                Type t = RecordTypeIDFinder.GetFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("GetAccountRecords", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                return (List<T>)mi.Invoke(null, new object[] { Account, Records });
            }

            public void BasicFileMaintenance(List<T> Records)
            {
                Type t = RecordTypeIDFinder.GetFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("BasicFileMaintenance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(null, new object[] { Records });
            }

            public void AddHeaders(List<T> BaseList, List<T> AddList)
            {
                Type t = RecordTypeIDFinder.GetFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("AddHeaders", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(null, new object[] { BaseList, AddList });
            }

            public void WriteNewFile(string FileName, List<T> Records)
            {
                Type t = RecordTypeIDFinder.GetFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("WriteNewFile", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(null, new object[] { FileName, Records });
            }

            public object PlacementRecord(List<T> Records, string AccountNumber)
            {
                switch (EDIType)
                {
                    case SupportedEDITypes.Citi:
                        return Records.OfType<RecordTypes.Citi.DebtorRecord>().ToList().FindAll(el => el.AccountNumber.Value == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.Ford:
                        return Records.OfType<RecordTypes.Ford.DebtorRecord>().ToList().FindAll(el => el.AccountNumber.Value == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.NAN:
                        return Records.OfType<RecordTypes.NAN.NewBusinessRecord>().ToList().FindAll(el => el.ClientAccountNumber.Value == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.NCO:
                        return Records.OfType<RecordTypes.NCO.RecordType01>().ToList().FindAll(el => el.AccountNumber.Value == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.PLX:
                        return Records.OfType<RecordTypes.PLX.PlacementRecord>().ToList().FindAll(el => el.AccountNumber == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.RMS:
                        return Records.OfType<RecordTypes.RMS.DebtorRecord>().ToList().FindAll(el => el.AccountNumber.Value == AccountNumber).FirstOrDefault();
                    case SupportedEDITypes.YGC:
                        return Records.OfType<RecordTypes.YGC.RecordType01>().ToList().FindAll(el => el.FORW_FILE.Value == AccountNumber).FirstOrDefault();
                    default:
                        return null;
                }
            }

            public int NewAccounts(List<T> Records)
            {
                switch (EDIType)
                {
                    case SupportedEDITypes.Citi:
                        return Records.OfType<RecordTypes.Citi.DebtorRecord>().GroupBy(el => el.AccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.Ford:
                        return Records.OfType<RecordTypes.Ford.DebtorRecord>().GroupBy(el => el.AccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.NAN:
                        return Records.OfType<RecordTypes.NAN.NewBusinessRecord>().GroupBy(el => el.NetworkAccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.NCO:
                        return Records.OfType<RecordTypes.NCO.Base.RecordBase>().GroupBy(el => el.AccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.PLX:
                        return Records.OfType<RecordTypes.PLX.Base.RecordBase>().GroupBy(el => el.AccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.RMS:
                        return Records.OfType<RecordTypes.RMS.DebtorRecord>().GroupBy(el => el.AccountNumber).Select(el => el.First()).Count();
                    case SupportedEDITypes.YGC:
                        return Records.OfType<RecordTypes.YGC.Base.YGCBase>().GroupBy(el => el.FORW_FILE).Select(el => el.First()).Count();
                    default:
                        return 0;
                }
            }

            public IEnumerable<List<T>> UniqueAccountListing(List<T> Records)
            {
                switch (EDIType)
                {
                    case SupportedEDITypes.Citi:
                        foreach (RecordTypes.Citi.DebtorRecord R in Records.OfType<RecordTypes.Citi.DebtorRecord>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.Citi.AccountRecord>().ToList().FindAll(el => el.AccountNumber.Value == R.AccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.Ford:
                        foreach (RecordTypes.Ford.DebtorRecord R in Records.OfType<RecordTypes.Ford.DebtorRecord>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.RMS.Base.AccountInfo>().ToList().FindAll(el => el.AccountNumber.Value == R.AccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.NAN:
                        foreach (RecordTypes.NAN.NewBusinessRecord R in Records.OfType<RecordTypes.NAN.NewBusinessRecord>().GroupBy(el => el.NetworkAccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.NAN.NewBusinessRecord>().ToList().FindAll(el => el.NetworkAccountNumber.Value == R.NetworkAccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.NCO:
                        foreach (RecordTypes.NCO.Base.RecordBase R in Records.OfType<RecordTypes.NCO.Base.RecordBase>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.NCO.Base.RecordBase>().ToList().FindAll(el => el.AccountNumber.Value == R.AccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.PLX:
                        foreach (RecordTypes.PLX.Base.RecordBase R in Records.OfType<RecordTypes.PLX.Base.RecordBase>().GroupBy(el => el.AccountNumber).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.PLX.Base.RecordBase>().ToList().FindAll(el => el.AccountNumber == R.AccountNumber).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.RMS:
                        foreach (RecordTypes.RMS.DebtorRecord R in Records.OfType<RecordTypes.RMS.DebtorRecord>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.RMS.Base.AccountInfo>().ToList().FindAll(el => el.AccountNumber.Value == R.AccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.YGC:
                        foreach (RecordTypes.YGC.RecordType01 R in Records.OfType<RecordTypes.YGC.RecordType01>().GroupBy(el => el.FORW_FILE.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.YGC.Base.YGCBase>().ToList().FindAll(el => el.FORW_FILE.Value == R.FORW_FILE.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.Trak:
                        foreach (RecordTypes.Trak.TrakPlacement R in Records.OfType<RecordTypes.Trak.TrakPlacement>().GroupBy(el => el.OriginalAccountNumber).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.Trak.TrakPlacement>().ToList().FindAll(el => el.OriginalAccountNumber == R.OriginalAccountNumber).OfType<T>().ToList();
                        }
                        break;
                    default:
                        yield return null;
                        break;
                }
            }
        }

        public class YGCFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public YGCFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public YGCBase.YGCBase ReadRecord()
            {
                return YGCRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public YGCBase.YGCList<YGCBase.YGCBase> ReadFile()
            {
                YGCBase.YGCList<YGCBase.YGCBase> List = new YGCBase.YGCList<YGCBase.YGCBase>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<YGCBase.YGCBase> GetAccountRecords(CLS.Account Account, List<YGCBase.YGCBase> Records)
            {
                return Records.OfType<YGCBase.YGCBase>().Where(el => el.FORW_FILE.Value == Account.Forw_FileNo).ToList();
            }

            public static void BasicFileMaintenance(List<YGCBase.YGCBase> Records)
            {
                // Put Last Payment from Issuer into Last Payment Date, If No Last Payment Date
                foreach (YGC.RecordType01 RT1 in Records.OfType<YGC.RecordType01>())
                {
                    if (RT1.DATE_LPAY.Value == null && RT1.LPAY_ISS_D.Value != null)
                        RT1.DATE_LPAY.Value = RT1.LPAY_ISS_D.Value;
                }
            }

            public static void AddHeaders(List<YGCBase.YGCBase> BaseList, List<YGCBase.YGCBase> AddList) { }

            public static void WriteNewFile(string FileName, List<YGCBase.YGCBase> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (YGCBase.YGCBase R in Records.OrderBy(el => el))
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class PLXFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public PLXFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public PLXBase.RecordTypeBase ReadRecord()
            {
                return PLXRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public PLXBase.PLXList<PLXBase.RecordTypeBase> ReadFile()
            {
                PLXBase.PLXList<PLXBase.RecordTypeBase> List = new PLXBase.PLXList<PLXBase.RecordTypeBase>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<PLXBase.RecordTypeBase> GetAccountRecords(CLS.Account Account, List<PLXBase.RecordTypeBase> Records)
            {
                List<PLXBase.RecordBase> AccountRecords = Records.OfType<PLXBase.RecordBase>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber == Account.Forw_FileNo).OfType<PLXBase.RecordTypeBase>().ToList();
            }

            public static void BasicFileMaintenance(List<PLXBase.RecordTypeBase> Records) { }

            public static void AddHeaders(List<PLXBase.RecordTypeBase> BaseList, List<PLXBase.RecordTypeBase> AddList)
            {
                AddList.AddRange(BaseList.FindAll(el => el.RecordType == PLX.Enums.Types.FileHeader || el.RecordType == PLX.Enums.Types.RecordHeader));
            }

            public static void WriteNewFile(string FileName, List<PLXBase.RecordTypeBase> Records)
            {
                PLX.FileHeader OHead = Records.OfType<PLX.FileHeader>().FirstOrDefault();
                List<PLX.RecordHeader> RHeads = Records.OfType<PLX.RecordHeader>().GroupBy(el => el.HeaderRecordType).Select(el => el.First()).ToList();
                Records.RemoveAll(el => el is PLX.FileHeader);
                Records.RemoveAll(el => el is PLX.RecordHeader);
                PLX.FileHeader HR = new PLX.FileHeader(OHead);
                foreach (PLX.RecordHeader RH in RHeads)
                {
                    RH.NumOfRecords = Records.Count(el => el.RecordType == RH.HeaderRecordType);
                }
                Records.Insert(0, HR);
                Records.InsertRange(1, RHeads.OfType<PLXBase.RecordTypeBase>().OrderBy(el => el.RecordType));
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (PLXBase.RecordTypeBase R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckPLXFile(PLXBase.PLXList<PLXBase.RecordTypeBase> FileRecords)
            {
                bool Error = false;
                StringBuilder sb = new StringBuilder();
                sb.Append("Record Errors:\r\n\r\n");
                foreach (PLX.RecordHeader RH in FileRecords.OfType<PLX.RecordHeader>())
                {
                    Type RType = Type.GetType("RecordTypes.PLX." + RH.HeaderRecordType.ToString());
                    int Total = RH.NumOfRecords;
                    int Count = FileRecords.PLXRecords.FindAll(el => el.GetType() == RType).Count;

                    if (Total != Count)
                    {
                        Error = true;
                        sb.Append(string.Format("{0} ({1}) - Expected {2} Records, Found {3}\r\n", RH.HeaderRecordType.ToString(), ((PLX.Enums.TypeValues)RH.HeaderRecordType).ToString(), Total, Count));
                    }
                }

                if (!Error) return new RecordErrors(false, "Succeeded!");
                else return new RecordErrors(true, sb.ToString());
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class NCOFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public NCOFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public NCOBase.Record ReadRecord()
            {
                return NCORecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public NCOBase.NCOList<NCOBase.Record> ReadFile()
            {
                NCOBase.NCOList<NCOBase.Record> List = new NCOBase.NCOList<NCOBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<NCOBase.Record> GetAccountRecords(CLS.Account Account, List<NCOBase.Record> Records)
            {
                List<NCOBase.RecordBase> AccountRecords = Records.OfType<NCOBase.RecordBase>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_RefNo).OfType<NCOBase.Record>().ToList();
            }

            public static void BasicFileMaintenance(List<NCOBase.Record> Records) { }

            public static void AddHeaders(List<NCOBase.Record> BaseList, List<NCOBase.Record> AddList)
            {
                AddList.AddRange(BaseList.FindAll(el => el is NCO.RecordType00));
            }

            public static void WriteNewFile(string FileName, List<NCOBase.Record> Records)
            {
                NCO.RecordType00 OHead = Records.OfType<NCO.RecordType00>().FirstOrDefault();
                if (OHead == null) throw new NotImplementedException("No PLX Header (Record Type 00) Could Be Found in Placement File");
                Records.RemoveAll(el => el is NCO.RecordType00);
                Records.RemoveAll(el => el is NCO.RecordType99);
                NCO.RecordType00 HR = new NCO.RecordType00();
                NCO.RecordType99 TR = new NCO.RecordType99();
                HR.CreateDateTime.Value = DateTime.Now;
                HR.NCOID.Value = OHead.NCOID.Value;
                HR.ReceiverID.Value = OHead.ReceiverID.Value;
                Records.Insert(0, HR);
                try
                {
                    TR.TotalAmountOfAccounts.Value = Records.OfType<NCO.RecordType01>().Sum(el => el.TotalBalance.Value);
                }
                catch { TR.TotalAmountOfAccounts.Value = 0; }
                TR.TotalRecordType01.Value = Records.OfType<NCO.RecordType01>().Count();
                TR.TotalRecordType02.Value = Records.OfType<NCO.RecordType02>().Count();
                TR.TotalRecordType03.Value = Records.OfType<NCO.RecordType03>().Count();
                TR.TotalRecordType04.Value = Records.OfType<NCO.RecordType04>().Count();
                TR.TotalRecordType05.Value = Records.OfType<NCO.RecordType05>().Count();
                TR.TotalRecordType06.Value = Records.OfType<NCO.RecordType06>().Count();
                TR.TotalRecordType07.Value = Records.OfType<NCO.RecordType07>().Count();
                TR.TotalRecordType08.Value = Records.OfType<NCO.RecordType08>().Count();
                TR.TotalRecordType09.Value = Records.OfType<NCO.RecordType09>().Count();
                TR.TotalRecordType10.Value = Records.OfType<NCO.RecordType10>().Count();
                Records.Add(TR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (NCOBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckNCOFile(NCOBase.NCOList<NCOBase.Record> FileRecords)
            {
                bool Error = false;
                StringBuilder sb = new StringBuilder();
                sb.Append("Record Errors:\r\n\r\n");
                foreach (NCO.RecordType99 Trail in FileRecords.OfType<NCO.RecordType99>())
                {
                    decimal PlacementAmount = Trail.TotalAmountOfAccounts.Value.Value;
                    decimal CalculatedAmount = FileRecords.NCORecords.OfType<NCO.RecordType01>().Sum(el => el.TotalBalance.Value.Value);
                    int[,] Checks = new int[2, 10];
                    string[,] Names = new string[,] { { "Accounts", "Demographics", "Related Persons", "Legal", "Bankruptcy", "Notes", "Financial", "Maintenance", "Reconciliation", "Rejects" }, { "RecType01", "RecType02", "RecType03", "RecType04", "RecType05", "RecType06", "RecType07", "RecType08", "RecType09", "RecType10" } };
                    Checks[0, 0] = Trail.TotalRecordType01.Value.Value;
                    Checks[1, 0] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType01).Count;
                    Checks[0, 1] = Trail.TotalRecordType02.Value.Value;
                    Checks[1, 1] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType02).Count;
                    Checks[0, 2] = Trail.TotalRecordType03.Value.Value;
                    Checks[1, 2] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType03).Count;
                    Checks[0, 3] = Trail.TotalRecordType04.Value.Value;
                    Checks[1, 3] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType04).Count;
                    Checks[0, 4] = Trail.TotalRecordType05.Value.Value;
                    Checks[1, 4] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType05).Count;
                    Checks[0, 5] = Trail.TotalRecordType06.Value.Value;
                    Checks[1, 5] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType06).Count;
                    Checks[0, 6] = Trail.TotalRecordType07.Value.Value;
                    Checks[1, 6] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType07).Count;
                    Checks[0, 7] = Trail.TotalRecordType08.Value.Value;
                    Checks[1, 7] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType08).Count;
                    Checks[0, 8] = Trail.TotalRecordType09.Value.Value;
                    Checks[1, 8] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType09).Count;
                    Checks[0, 9] = Trail.TotalRecordType10.Value.Value;
                    Checks[1, 9] = FileRecords.NCORecords.FindAll(el => el.RecordType.Value == NCO.Enums.RecordTypes.RecordType10).Count;

                    if (PlacementAmount != CalculatedAmount)
                    {
                        Error = true;
                        sb.Append(string.Format("Placement Balance - Expected {0:C}, Calculated {1:C}\r\n", PlacementAmount, CalculatedAmount));
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        if (Checks[0, i] != Checks[1, i])
                        {
                            Error = true;
                            sb.Append(string.Format("{0} ({1}) - Expected {2} Records, Found {3}\r\n", Names[0, i], Names[1, i], Checks[0, i], Checks[1, i]));
                        }
                    }
                }

                if (!Error) return new RecordErrors(false, "Succeeded!");
                else return new RecordErrors(true, sb.ToString());
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class RMSFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public RMSFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public RMSBase.Record ReadRecord()
            {
                return RMSRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public RMSBase.RMSList<RMS.Base.Record> ReadFile()
            {
                RMSBase.RMSList<RMSBase.Record> List = new RMSBase.RMSList<RMSBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<RMSBase.Record> GetAccountRecords(CLS.Account Account, List<RMSBase.Record> Records)
            {
                List<RMSBase.AccountInfo> AccountRecords = Records.OfType<RMSBase.AccountInfo>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_FileNo).OfType<RMSBase.Record>().ToList();
            }

            public static void BasicFileMaintenance(List<RMSBase.Record> Records)
            {
                // Remove Empty Debtors and Property
                Records.RemoveAll(el1 => Records.OfType<RMS.CoMakerRecord>().Where(el => el.CompanyName.Value == "" || el.CompanyName.Value.ToUpper().Contains("PROPERTY")).OfType<RMSBase.Record>().Contains(el1));
            }

            public static void AddHeaders(List<RMSBase.Record> BaseList, List<RMSBase.Record> AddList)
            {
                AddList.AddRange(BaseList.FindAll(el => el is RMS.HeaderRecord));
            }

            public static void WriteNewFile(string FileName, List<RMSBase.Record> Records)
            {
                RMS.HeaderRecord OHead = Records.OfType<RMS.HeaderRecord>().FirstOrDefault();
                Records.RemoveAll(el => el is RMS.HeaderRecord);
                RMS.HeaderRecord HR = new RMS.HeaderRecord();
                HR.AgencyCode.Value = OHead.AgencyCode.Value;
                HR.Date.Value = DateTime.Now;
                HR.HeaderCount.Value = Records.Count;
                HR.TotalBalance.Value = Records.OfType<RMS.DebtorRecord>().Sum(el => el.Balance.Value);
                Records.Insert(0, HR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (RMS.Base.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckRMSFile(RMSBase.RMSList<RMSBase.Record> FileRecords)
            {
                DateTime? PlacementDate = null;
                int Records = 0;
                int RecordsFound = 0;
                decimal BalancePlaced = 0;
                decimal BalanceFound = 0;
                bool Error = false;
                StringBuilder sb = new StringBuilder();
                sb.Append("Record Errors:\r\n\r\n");

                for (int i = 0; i < FileRecords.Count; i++)
                {
                    if (FileRecords[i] is RMS.HeaderRecord)
                    {
                        if (PlacementDate != null)
                        {
                            if (Records > 0) // Last Header Had Records
                            {
                                if (Records != RecordsFound || BalancePlaced != BalanceFound)
                                {
                                    Error = true;
                                    sb.Append(string.Format("{4}\r\nExpected Placement: {0} Records, for {1:C}\r\nActual Placement: {2} Records, for {3:C}\r\n\r\n", Records, BalancePlaced, RecordsFound, BalanceFound, PlacementDate.Value.ToString("yyyy-MM-dd")));
                                }
                            }
                        }

                        PlacementDate = ((RMS.HeaderRecord)FileRecords[i]).Date.Value;
                        Records = ((RMS.HeaderRecord)FileRecords[i]).HeaderCount.Value.Value;
                        BalancePlaced = ((RMS.HeaderRecord)FileRecords[i]).TotalBalance.Value.Value;
                        RecordsFound = 0;
                        BalanceFound = 0;
                    }
                    else
                    {
                        RecordsFound++;
                        if (FileRecords[i] is RMS.DebtorRecord)
                            BalanceFound += ((RMS.DebtorRecord)FileRecords[i]).Balance.Value.Value;
                    }
                }
                if (Records > 0) // Last Header Had Records
                {
                    if (Records != RecordsFound || BalancePlaced != BalanceFound)
                    {
                        Error = true;
                        sb.Append(string.Format("Expected Placement: {0} Accounts, for {1:C}\r\nActual Placement: {2} Accounts, for {3:C}", Records, BalancePlaced, RecordsFound, BalanceFound));
                    }
                }

                if (!Error) return new RecordErrors(false, "Succeeded!");
                else return new RecordErrors(true, sb.ToString());
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class FordRMSFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public FordRMSFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public RMSBase.Record ReadRecord()
            {
                return FordRMSRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public RMSBase.RMSList<RMSBase.Record> ReadFile()
            {
                RMSBase.RMSList<RMSBase.Record> List = new RMSBase.RMSList<RMSBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<RMSBase.Record> GetAccountRecords(CLS.Account Account, List<RMSBase.Record> Records)
            {
                return RMSFileReader.GetAccountRecords(Account, Records);
            }

            public static void BasicFileMaintenance(List<RMSBase.Record> Records)
            {
                // Remove Empty Debtors and Property
                Records.RemoveAll(el1 => Records.OfType<Ford.CoMakerRecord>().Where(el => el.CompanyName.Value == "" || el.CompanyName.Value.ToUpper().Contains("PROPERTY")).OfType<RMSBase.Record>().Contains(el1));
            }

            public static void AddHeaders(List<RMSBase.Record> BaseList, List<RMSBase.Record> AddList)
            {
                AddList.AddRange(BaseList.FindAll(el => el is Ford.HeaderRecord));
            }

            public static void WriteNewFile(string FileName, List<RMSBase.Record> Records)
            {
                Ford.HeaderRecord OHead = Records.OfType<Ford.HeaderRecord>().FirstOrDefault();
                Records.RemoveAll(el => el is Ford.HeaderRecord);
                Ford.HeaderRecord HR = new Ford.HeaderRecord();
                HR.AgencyCode.Value = OHead.AgencyCode.Value;
                HR.Date.Value = DateTime.Now;
                HR.HeaderCount.Value = Records.Count;
                HR.TotalBalance.Value = Records.OfType<Ford.DebtorRecord>().Sum(el => el.Balance.Value);
                HR.CreditorID.Value = OHead.CreditorID.Value;
                HR.TotalPrincipal.Value = Records.OfType<Ford.DebtorRecord>().Sum(el => el.NetPrincipal.Value);
                HR.TotalInterest.Value = Records.OfType<Ford.DebtorRecord>().Sum(el => el.NetInterest.Value);
                HR.TotalCosts.Value = Records.OfType<Ford.DebtorRecord>().Sum(el => el.NetAssociatedCosts.Value);
                Records.Insert(0, HR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (RMS.Base.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckRMSFile(RMSBase.RMSList<RMSBase.Record> FileRecords)
            {
                DateTime? PlacementDate = null;
                int Records = 0;
                int RecordsFound = 0;
                decimal BalancePlaced = 0;
                decimal BalanceFound = 0;
                bool Error = false;
                StringBuilder sb = new StringBuilder();
                sb.Append("Record Errors:\r\n\r\n");

                for (int i = 0; i < FileRecords.Count; i++)
                {
                    if (FileRecords[i] is Ford.HeaderRecord)
                    {
                        if (PlacementDate != null)
                        {
                            if (Records > 0) // Last Header Had Records
                            {
                                if (Records != RecordsFound || BalancePlaced != BalanceFound)
                                {
                                    Error = true;
                                    sb.Append(string.Format("{4}\r\nExpected Placement: {0} Records, for {1:C}\r\nActual Placement: {2} Records, for {3:C}\r\n\r\n", Records, BalancePlaced, RecordsFound, BalanceFound, PlacementDate.Value.ToString("yyyy-MM-dd")));
                                }
                            }
                        }

                        PlacementDate = ((Ford.HeaderRecord)FileRecords[i]).Date.Value;
                        Records = ((Ford.HeaderRecord)FileRecords[i]).HeaderCount.Value.Value;
                        BalancePlaced = ((Ford.HeaderRecord)FileRecords[i]).TotalBalance.Value.Value;
                        RecordsFound = 0;
                        BalanceFound = 0;
                    }
                    else
                    {
                        RecordsFound++;
                        if (FileRecords[i] is Ford.DebtorRecord)
                            BalanceFound += ((Ford.DebtorRecord)FileRecords[i]).Balance.Value.Value;
                    }
                }
                if (Records > 0) // Last Header Had Records
                {
                    if (Records != RecordsFound || BalancePlaced != BalanceFound)
                    {
                        Error = true;
                        sb.Append(string.Format("Expected Placement: {0} Accounts, for {1:C}\r\nActual Placement: {2} Accounts, for {3:C}", Records, BalancePlaced, RecordsFound, BalanceFound));
                    }
                }

                if (!Error) return new RecordErrors(false, "Succeeded!");
                else return new RecordErrors(true, sb.ToString());
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class CitiRMSFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public CitiRMSFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public RMSBase.Record ReadRecord()
            {
                return CitiRMSRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public RMSBase.RMSList<RMSBase.Record> ReadFile()
            {
                RMSBase.RMSList<RMSBase.Record> List = new RMSBase.RMSList<RMSBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<RMSBase.Record> GetAccountRecords(CLS.Account Account, List<RMSBase.Record> Records)
            {
                return RMSFileReader.GetAccountRecords(Account, Records);
            }

            public static void BasicFileMaintenance(List<RMSBase.Record> Records)
            {
                // Remove Empty Debtors and Property
                IEnumerable<Citi.CoMakerRecord> RecordsToRemove = Records.OfType<Citi.CoMakerRecord>().Where(el => el.CompanyName.Value == "" || el.CompanyName.Value.ToUpper().Contains("PROPERTY"));
                foreach (Citi.CoMakerRecord CMR in Records.OfType<Citi.CoMakerRecord>())
                {
                    List<Citi.CoMakerRecord> OtherDebtors = Records.OfType<Citi.CoMakerRecord>().Where(el => el.AccountNumber.Value == CMR.AccountNumber.Value && !RecordsToRemove.Contains(el)).ToList();
                    int Seq = 1;
                    foreach (Citi.CoMakerRecord OtherD in OtherDebtors.OrderBy(el => el.SequenceNumber.Value))
                    {
                        OtherD.SequenceNumber.Value = Seq;
                        Seq++;
                    }
                }
                Records.RemoveAll(el1 => RecordsToRemove.OfType<RMSBase.Record>().Contains(el1));
            }

            public static void AddHeaders(List<RMSBase.Record> BaseList, List<RMSBase.Record> AddList)
            {
                AddList.AddRange(BaseList.FindAll(el => el is Citi.HeaderRecord));
            }

            public static void WriteNewFile(string FileName, List<RMSBase.Record> Records)
            {
                Citi.HeaderRecord OHead = Records.OfType<Citi.HeaderRecord>().FirstOrDefault();
                Records.RemoveAll(el => el is Citi.HeaderRecord);
                Citi.HeaderRecord HR = new Citi.HeaderRecord();
                HR.AgencyCode.Value = OHead.AgencyCode.Value;
                HR.Date.Value = DateTime.Now;
                HR.HeaderCount.Value = Records.Count;
                HR.TotalBalance.Value = Records.OfType<Citi.DebtorRecord>().Sum(el => el.Balance.Value);
                Records.Insert(0, HR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (RMS.Base.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckRMSFile(RMSBase.RMSList<RMSBase.Record> FileRecords)
            {
                DateTime? PlacementDate = null;
                int Records = 0;
                int RecordsFound = 0;
                decimal BalancePlaced = 0;
                decimal BalanceFound = 0;
                bool Error = false;
                StringBuilder sb = new StringBuilder();
                sb.Append("Record Errors:\r\n\r\n");

                for (int i = 0; i < FileRecords.Count; i++)
                {
                    if (FileRecords[i] is Ford.HeaderRecord)
                    {
                        if (PlacementDate != null)
                        {
                            if (Records > 0) // Last Header Had Records
                            {
                                if (Records != RecordsFound || BalancePlaced != BalanceFound)
                                {
                                    Error = true;
                                    sb.Append(string.Format("{4}\r\nExpected Placement: {0} Records, for {1:C}\r\nActual Placement: {2} Records, for {3:C}\r\n\r\n", Records, BalancePlaced, RecordsFound, BalanceFound, PlacementDate.Value.ToString("yyyy-MM-dd")));
                                }
                            }
                        }

                        PlacementDate = ((Citi.HeaderRecord)FileRecords[i]).Date.Value;
                        Records = ((Citi.HeaderRecord)FileRecords[i]).HeaderCount.Value.Value;
                        BalancePlaced = ((Citi.HeaderRecord)FileRecords[i]).TotalBalance.Value.Value;
                        RecordsFound = 0;
                        BalanceFound = 0;
                    }
                    else
                    {
                        RecordsFound++;
                        if (FileRecords[i] is Citi.DebtorRecord)
                            BalanceFound += ((Citi.DebtorRecord)FileRecords[i]).Balance.Value.Value;
                    }
                }
                if (Records > 0) // Last Header Had Records
                {
                    if (Records != RecordsFound || BalancePlaced != BalanceFound)
                    {
                        Error = true;
                        sb.Append(string.Format("Expected Placement: {0} Accounts, for {1:C}\r\nActual Placement: {2} Accounts, for {3:C}", Records, BalancePlaced, RecordsFound, BalanceFound));
                    }
                }

                if (!Error) return new RecordErrors(false, "Succeeded!");
                else return new RecordErrors(true, sb.ToString());
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class NANFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public NANFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public NANBase.Record ReadRecord()
            {
                return NANRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public NANBase.NANList<NANBase.Record> ReadFile()
            {
                NANBase.NANList<NANBase.Record> List = new NANBase.NANList<NANBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<NANBase.Record> GetAccountRecords(CLS.Account Account, List<NANBase.Record> Records)
            {
                List<NAN.NewBusinessRecord> AccountRecords = Records.OfType<NAN.NewBusinessRecord>().ToList();
                return AccountRecords.FindAll(el => el.NetworkAccountNumber.Value == Account.COCO_FileNo).OfType<NANBase.Record>().ToList();
            }

            public static void BasicFileMaintenance(List<NANBase.Record> Records) { }

            public static void AddHeaders(List<NANBase.Record> BaseList, List<NANBase.Record> AddList) { }

            public static void WriteNewFile(string FileName, List<NANBase.Record> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (NANBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckRMSFile(NANBase.NANList<NANBase.Record> FileRecords)
            {
                return null;
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class TrakFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public TrakFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public Trak.Base.RecordTypeBase ReadRecord()
            {
                return TrakRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public Trak.Base.TrakList<Trak.Base.RecordTypeBase> ReadFile()
            {
                Trak.Base.TrakList<Trak.Base.RecordTypeBase> List = new Trak.Base.TrakList<Trak.Base.RecordTypeBase>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public static List<Trak.Base.RecordTypeBase> GetAccountRecords(CLS.Account Account, List<Trak.Base.RecordTypeBase> Records)
            {
                List<Trak.TrakPlacement> AccountRecords = Records.OfType<Trak.TrakPlacement>().ToList();
                return AccountRecords.FindAll(el => el.OriginalAccountNumber.Value == Account.Forw_FileNo).OfType<Trak.Base.RecordTypeBase>().ToList();
            }

            public static void BasicFileMaintenance(List<Trak.Base.RecordTypeBase> Records)
            {
                foreach (Trak.TrakPlacement Placement in Records.OfType<Trak.TrakPlacement>())
                {
                    Placement.ForwarderAccountNumber.Value = Placement.ClientFILE.Value;
                }
            }

            public static void AddHeaders(List<Trak.Base.RecordTypeBase> BaseList, List<Trak.Base.RecordTypeBase> AddList) { }

            public static void WriteNewFile(string FileName, List<Trak.Base.RecordTypeBase> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                sw.WriteLine(@"Client Purchase Cost (if applicable)|Client Purchase Rate (if applicable)|placement rate (if applicable)|instruction|SCORE|Client FILE#|LAST NAME|FIRST NAME|MIDDLE|GENERATION|ALSO KNOWN AS|STREET|CITY|STATE|ZIP|DOB|DRIVERC LICENSE|PHONE|SSNO|MILITARY INFORMATION|APR|PLACE OF EMPLOYMENT|PLACE OF EMPLOYMENT ADDRESS|PLACE OF EMPLOYMENT CITY|PLACE OF EMPLOYMENT STATE|PLACE OF EMPLOYMENT ZIP|PLACE OF EMPLOYMENT PHONE|SELF EMPLOYED|WEEKLY SALARY|BANK NAME|BANK ADDRESS|BANK CITY|BANK STATE|BANK ZIP|BANK PHONE|BANK ACCT #|BANK ACCT TYPE|BANK FUNDS VERIFIED|ATTORNEY NAME|ATTORNEY FIRM|ATTORNEY ADDRESS|ATTORNEY CITY|ATTORNEY STATE|ATTORNEY ZIP|ATTORNEY PHONE|DATE OF DEATH|BR FILING DATE|BR FILING NUMBER|BR CHAPTER # |DEBT DISPUTED|OWNS AT|RENTS AT|3RD PARTY AUTHORIZED|OPEN DATE|CHARGE OFF DATE|CHARGE OFF AMOUNT|Current Balance|PURCHASE DATE|LAST CHARGE AMOUNT|INTERST START DATE - LAST INTEREST POSTING DATE|LAST CASH ADVANCE AMOUNT|ORIGINAL CREDITOR|CARD TYPE|ORIGINAL ACCT#|ORIGINAL CREDITOR LAST PAY DATE|ORIGINAL CREDITOR LAST PAY AMOUNT|SOL Date|Client LAST PAY DATE|Client LAST PAY AMOUNT|Client TOTAL PAYMENTS|PRE CHARGE OFF ACCOUNT NUMBER|JDG DATE|JDG AMT|JDG CST|JDG TOT|JURISDICTION|DOCKET.NO|CO APPLICANT LAST NAME|CO APPLICANT FIRST NAME|CO APPLICANT MIDDLE NAME|CO APPLICANT GENERATION|CO APPLICANT ALSO KNOWN AS|CO APPLICANT ADDRESS|CO APPLICANT CITY|CO APPLICANT STATE|CO APPLICANT ZIP|CO APPLICANT DOB|CO APPLICANT DRIVERC LICENSE|CO APPLICANT PHONE|CO APPLICANT SSNO|MILITARY INFORMATION|CO APPLICANT PLACE OF EMPLOYMENT|CO APPLICANT PLACE OF EMPLOYMENT ADDRESS|CO APPLICANT PLACE OF EMPLOYMENT CITY|CO APPLICANT PLACE OF EMPLOYMENT STATE|CO APPLICANT PLACE OF EMPLOYMENT ZIP|CO APPLICANT PLACE OF EMPLOYMENT PHONE|CO APPLICANT SELF EMPLOYED|CO APPLICANT WEEKLY SALARY|CO APPLICANT BANK NAME|CO APPLICANT BANK ADDRESS|CO APPLICANT BANK CITY|CO APPLICANT BANK STATE|CO APPLICANT BANK ZIP|CO APPLICANT BANK PHONE|CO APPLICANT BANK ACCT #|CO APPLICANT BANK ACCT TYPE|CO APPLICANT BANK FUNDS VERIFIED|CO APPLICANT ATTORNEY NAME|CO APPLICANT ATTORNEY FIRM|CO APPLICANT ATTORNEY ADDRESS|CO APPLICANT ATTORNEY CITY|CO APPLICANT ATTORNEY STATE|CO APPLICANT ATTORNEY ZIP|CO APPLICANT ATTORNEY PHONE|CO APPLICANT DATE OF DEATH|CO APPLICANT BR FILING DATE|CO APPLICANT BR FILING NUMBER|CO APPLICANT BR CHAPTER # |CO APPLICANT DEBT DISPUTED|CO APPLICANT OWNS AT|CO APPLICANT RENTS AT|CO APPLICANT 3RD PARTY AUTHORIZED|Lexis/Nexis Property|Lexis/Nexis Address|Lexis/Nexis Phone(s)|Lexis/Nexis Br Info|Lexis/Nexis Deceased Info|Accurint Property|Accurint Address|Accurint Phone(s)|Accurint Br Info|Accurint Deceased Info|Merchant|Seller|Forwarder/Account#|ForwardedAgency|ForwardedAttorney|Repo Date|TRAK Placed Date|Car Info|Contract State|Current Creditor");
                foreach (Trak.Base.RecordTypeBase R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public RecordErrors CheckRMSFile(Trak.Base.TrakList<Trak.Base.RecordTypeBase> FileRecords)
            {
                return null;
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class Dantom_NCOAFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public Dantom_NCOAFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public static Dantom.NCOA.Record GetRecordType(string record)
            {
                return new Dantom.NCOA.Record(record);
            }

            public Dantom.NCOA.Record ReadRecord()
            {
                return new Dantom.NCOA.Record(this.FileReader.ReadLine());
            }

            public List<Dantom.NCOA.Record> ReadFile()
            {
                List<Dantom.NCOA.Record> List = new List<Dantom.NCOA.Record>();
                this.FileReader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class CollectionTriggersFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public CollectionTriggersFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public RecordTypes2.Experian.CollectionTriggers.Base.Record ReadRecord()
            {
                return CollectionTriggersRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public List<RecordTypes2.Experian.CollectionTriggers.Base.Record> ReadFile()
            {
                List<RecordTypes2.Experian.CollectionTriggers.Base.Record> List = new List<RecordTypes2.Experian.CollectionTriggers.Base.Record>();
                this.FileReader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class WalzExtractFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public WalzExtractFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
                //try
                //{
                //    this.FileReader.ReadLine();
                //}
                //catch { }
            }

            public RecordTypes.Walz.ExtractRecord ReadRecord()
            {
                string Line = this.FileReader.ReadLine();
                if (Line.Split('\t').Length == 8)
                {
                    if (Line.Split('\t')[0].ToUpper().Trim() == "TRACKING NUMBER")
                        return ReadRecord();
                    else
                        return new Walz.ExtractRecord(Line);
                }
                else
                    return null;
            }

            public List<RecordTypes.Walz.ExtractRecord> ReadFile()
            {
                List<RecordTypes.Walz.ExtractRecord> List = new List<RecordTypes.Walz.ExtractRecord>();
                this.FileReader.BaseStream.Seek(0, SeekOrigin.Begin);
                //try
                //{
                //    this.FileReader.ReadLine();
                //}
                //catch { }
                while (!this.EOF)
                {
                    List.Add(this.ReadRecord());
                }
                return List;
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        #endregion

        #region Maintenance File Readers

        public abstract class MaintenanceFileReader
        {
            public delegate void FileReadProgressHandler(int Progress);
            public event FileReadProgressHandler FileReadProgress;
            protected void OnFileReadProgress(int Progress)
            {
                if (FileReadProgress != null) FileReadProgress(Progress);
            }
        }
        public class MaintenanceFileReader<T> : MaintenanceFileReader
        {
            private string MaintenanceFileLocation;
            private StreamReader FR;
            private SupportedEDITypes EDIType;

            public bool EOF { get { return this.FR.EndOfStream; } }

            public MaintenanceFileReader(SupportedEDITypes EDIType)
            {
                this.EDIType = EDIType;
            }
            public MaintenanceFileReader(string MaintenanaceFileLocation, SupportedEDITypes EDIType)
            {
                this.MaintenanceFileLocation = MaintenanaceFileLocation;
                this.EDIType = EDIType;
                this.FR = new StreamReader(this.MaintenanceFileLocation);
            }

            public object ReadMaintenanceRecord()
            {
                Type t = RecordTypeIDFinder.GetMaintenanceRecordTypeID(this.EDIType);
                if (t == null) return null;
                System.Reflection.MethodInfo mi = t.GetMethod("GetMaintenanceRecord", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                return mi.Invoke(null, new object[] { this.FR.ReadLine() });
            }

            private void AddRecord(List<T> List, T Record)
            {
                List.Add(Record);
            }

            public List<T> ReadMaintenanceFile()
            {
                if (RecordTypeIDFinder.GetMaintenanceRecordTypeID(this.EDIType) == null) return new List<T>();
                try
                {
                    List<T> file = new List<T>();
                    this.FR.BaseStream.Seek(0, SeekOrigin.Begin);
                    while (!this.EOF)
                    {
                        OnFileReadProgress((int)(this.FR.BaseStream.Position * 100 / this.FR.BaseStream.Length));
                        System.Reflection.MethodInfo mi = this.GetType().GetMethod("AddRecord", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        mi.Invoke(this, new object[] { file, ReadMaintenanceRecord() });
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
                Type t = RecordTypeIDFinder.GetMaintenanceFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("GetAccountRecords", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                return (List<T>)mi.Invoke(null, new object[] { Account, Records });
            }

            public void WriteNewFile(string FileName, List<T> Records)
            {
                Type t = RecordTypeIDFinder.GetMaintenanceFileReaderType(this.EDIType);
                System.Reflection.MethodInfo mi = t.GetMethod("WriteNewFile", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(null, new object[] { FileName, Records });
            }

            // Incomplete
            public IEnumerable<List<T>> UniqueAccountListing(List<T> Records)
            {
                switch (EDIType)
                {
                    case SupportedEDITypes.Ford:
                    case SupportedEDITypes.Citi:
                    case SupportedEDITypes.RMS:
                        foreach (RecordTypes.RMS.Maintenance.MaintenanceInfo R in Records.OfType<RecordTypes.RMS.Maintenance.MaintenanceInfo>().GroupBy(el => el.AccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.RMS.Maintenance.MaintenanceInfo>().ToList().FindAll(el => el.AccountNumber.Value == R.AccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.NAN:
                        foreach (RecordTypes.NAN.Maintenance.TransactionRecord R in Records.OfType<RecordTypes.NAN.Maintenance.TransactionRecord>().GroupBy(el => el.NetworkAccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.NAN.Maintenance.TransactionRecord>().ToList().FindAll(el => el.NetworkAccountNumber.Value == R.NetworkAccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.NCO:

                        yield return null;
                        break;
                    case SupportedEDITypes.PLX:
                        foreach (RecordTypes.PLX.Base.RecordBase R in Records.OfType<RecordTypes.PLX.Base.RecordBase>().GroupBy(el => el.AccountNumber).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.PLX.Base.RecordBase>().ToList().FindAll(el => el.AccountNumber == R.AccountNumber).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.YGC:
                        foreach (RecordTypes.YGC.Base.YGCBase R in Records.OfType<RecordTypes.YGC.Base.YGCBase>().GroupBy(el => el.FORW_FILE.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.YGC.Base.YGCBase>().ToList().FindAll(el => el.FORW_FILE.Value == R.FORW_FILE.Value).OfType<T>().ToList();
                        }
                        break;
                    case SupportedEDITypes.Trak:
                        foreach (RecordTypes.Trak.TrakPlacement R in Records.OfType<RecordTypes.Trak.TrakPlacement>().GroupBy(el => el.OriginalAccountNumber.Value).Select(el => el.First()))
                        {
                            yield return Records.OfType<RecordTypes.Trak.TrakPlacement>().ToList().FindAll(el => el.OriginalAccountNumber.Value == R.OriginalAccountNumber.Value).OfType<T>().ToList();
                        }
                        break;
                    default:
                        yield return null;
                        break;
                }
            }
        }

        public class NANMaintenanceFileReader : MaintenanceFileReader
        {
            private string MaintenanceFileLocation;
            private StreamReader MaintenanceFileReader;

            public bool MaintenanceEOF { get { return this.MaintenanceFileReader.EndOfStream; } }

            public NANMaintenanceFileReader(string FileLocation)
            {
                this.MaintenanceFileLocation = FileLocation;
                this.MaintenanceFileReader = new StreamReader(this.MaintenanceFileLocation);
            }

            public NANBase.Record ReadMaintenanceRecord()
            {
                return NANMaintenanceTypeIdentifier.GetMaintenanceRecord(this.MaintenanceFileReader.ReadLine());
            }

            public NANBase.NANList<NANBase.Record> ReadMaintenanceFile()
            {
                NANBase.NANList<NANBase.Record> List = new NANBase.NANList<NANBase.Record>();
                this.MaintenanceFileReader.BaseStream.Position = 0;
                while (!this.MaintenanceEOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                List.NANRecords.RemoveAll(el => el == null);
                return List;
            }

            public static List<NANBase.Record> GetAccountRecords(CLS.Account Account, List<NANBase.Record> Records)
            {
                List<NAN.Maintenance.TransactionRecord> AccountRecords = Records.OfType<NAN.Maintenance.TransactionRecord>().ToList();
                return AccountRecords.FindAll(el => el.NetworkAccountNumber.Value == Account.COCO_FileNo).OfType<NANBase.Record>().ToList();
            }

            public static void WriteNewFile(string FileName, List<NANBase.Record> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (NANBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.MaintenanceFileLocation;
            }
        }

        public class CitiMaintenanceFileReader : MaintenanceFileReader
        {
            private string MaintenanceFileLocation;
            private StreamReader MaintenanceFileReader;

            public bool MaintenanceEOF { get { return this.MaintenanceFileReader.EndOfStream; } }

            public CitiMaintenanceFileReader(string FileLocation)
            {
                this.MaintenanceFileLocation = FileLocation;
                this.MaintenanceFileReader = new StreamReader(this.MaintenanceFileLocation);
            }

            public RMSBase.Record ReadMaintenanceRecord()
            {
                return CitiMaintenanceTypeIdentifier.GetMaintenanceRecord(this.MaintenanceFileReader.ReadLine());
            }

            public RMSBase.RMSList<RMSBase.Record> ReadMaintenanceFile()
            {
                RMSBase.RMSList<RMSBase.Record> List = new RMSBase.RMSList<RMSBase.Record>();
                this.MaintenanceFileReader.BaseStream.Position = 0;
                while (!this.MaintenanceEOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<RMSBase.Record> GetAccountRecords(CLS.Account Account, List<RMSBase.Record> Records)
            {
                List<RMS.Maintenance.MaintenanceInfo> AccountRecords = Records.OfType<RMS.Maintenance.MaintenanceInfo>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_FileNo).OfType<RMSBase.Record>().ToList();
            }

            public static void WriteNewFile(string FileName, List<RMSBase.Record> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (RMSBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.MaintenanceFileLocation;
            }
        }

        public class FordMaintenanceFileReader : MaintenanceFileReader
        {
            private string MaintenanceFileLocation;
            private StreamReader MaintenanceFileReader;

            public bool MaintenanceEOF { get { return this.MaintenanceFileReader.EndOfStream; } }

            public FordMaintenanceFileReader(string FileLocation)
            {
                this.MaintenanceFileLocation = FileLocation;
                this.MaintenanceFileReader = new StreamReader(this.MaintenanceFileLocation);
            }

            public RMSBase.Record ReadMaintenanceRecord()
            {
                return FordMaintenanceTypeIdentifier.GetMaintenanceRecord(this.MaintenanceFileReader.ReadLine());
            }

            public RMSBase.RMSList<RMSBase.Record> ReadMaintenanceFile()
            {
                RMSBase.RMSList<RMSBase.Record> List = new RMSBase.RMSList<RMSBase.Record>();
                this.MaintenanceFileReader.BaseStream.Position = 0;
                while (!this.MaintenanceEOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<RMSBase.Record> GetAccountRecords(CLS.Account Account, List<RMSBase.Record> Records)
            {
                List<RMS.Maintenance.MaintenanceInfo> AccountRecords = Records.OfType<RMS.Maintenance.MaintenanceInfo>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_FileNo).OfType<RMSBase.Record>().ToList();
            }

            public static void WriteNewFile(string FileName, List<RMSBase.Record> Records)
            {
                //System.IO.StreamWriter sw = new StreamWriter(FileName);
                //foreach (RMSBase.Record R in Records)
                //{
                //    sw.WriteLine(R);
                //    sw.Flush();
                //}
                //sw.Close();
                RMS.Maintenance.HeaderRecord OHead = Records.OfType<RMS.Maintenance.HeaderRecord>().FirstOrDefault();
                Records.RemoveAll(el => el is RMS.Maintenance.HeaderRecord);
                RMS.Maintenance.HeaderRecord HR = new RMS.Maintenance.HeaderRecord();
                HR.TransactionDate.Value = OHead.TransactionDate.Value;
                HR.BatchTransactionCount.Value = Records.Count;
                HR.GrossBatchTotal.Value = Records.OfType<Ford.Maintenance.FinanacialRecord>().Sum(r => r.TransactionAmount.Value);
                HR.NetBatchTransaction.Value = Records.OfType<Ford.Maintenance.FinanacialRecord>().Sum(r => r.NetPaymentAmount.Value);
                HR.ParentOrgCode.Value = OHead.ParentOrgCode.Value;
                HR.PartnerCode.Value = OHead.PartnerCode.Value;
                HR.RecovererCode.Value = OHead.RecovererCode.Value;
                Records.Insert(0, HR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (RMS.Base.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }



            public override string ToString()
            {
                return this.MaintenanceFileLocation;
            }
        }

        public class PLXMaintenanceFileReader : MaintenanceFileReader
        {
            private string FileLocation;
            private StreamReader FileReader;

            public bool EOF { get { return this.FileReader.EndOfStream; } }

            public PLXMaintenanceFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public PLX2.Base.RecordTypeBase ReadMaintenanceRecord()
            {
                return PLXMaintenanceTypeIdentifier.GetMaintenanceRecord(this.FileReader.ReadLine());
            }

            public List<PLX2.Base.RecordTypeBase> ReadMaintenanceFile()
            {
                List<PLX2.Base.RecordTypeBase> List = new List<PLX2.Base.RecordTypeBase>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<PLX2.Base.RecordTypeBase> GetAccountRecords(CLS.Account Account, List<PLX2.Base.RecordTypeBase> Records)
            {
                List<PLX2.Base.RecordBase> AccountRecords = Records.OfType<PLX2.Base.RecordBase>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_FileNo).OfType<PLX2.Base.RecordTypeBase>().ToList();
            }

            public static void WriteNewFile(string FileName, List<PLX2.Base.RecordTypeBase> Records)
            {
                PLX2.FileHeader OHead = Records.OfType<PLX2.FileHeader>().FirstOrDefault();
                List<PLX2.RecordHeader> RHeads = Records.OfType<PLX2.RecordHeader>().GroupBy(el => el.HeaderRecordType.Value).Select(el => el.First()).ToList();
                Records.RemoveAll(el => el is PLX2.FileHeader);
                Records.RemoveAll(el => el is PLX2.RecordHeader);
                PLX2.FileHeader HR = new PLX2.FileHeader(OHead);
                foreach (PLX2.RecordHeader RH in RHeads)
                {
                    RH.NumOfRecords.Value = Records.Count(el => el.RecordType.Value == RH.HeaderRecordType.Value);
                }
                Records.Insert(0, HR);
                Records.InsertRange(1, RHeads.OfType<PLX2.Base.RecordTypeBase>().OrderBy(el => el.RecordType.Value));
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (PLX2.Base.RecordTypeBase R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class YGCMaintenanceFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public YGCMaintenanceFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public YGCBase.YGCBase ReadMaintenanceRecord()
            {
                return YGCRecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public YGCBase.YGCList<YGCBase.YGCBase> ReadMaintenanceFile()
            {
                YGCBase.YGCList<YGCBase.YGCBase> List = new YGCBase.YGCList<YGCBase.YGCBase>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<YGCBase.YGCBase> GetAccountRecords(CLS.Account Account, List<YGCBase.YGCBase> Records)
            {
                return Records.FindAll(el => el.FORW_FILE.Value == Account.Forw_FileNo).ToList();
            }

            public static void WriteNewFile(string FileName, List<YGCBase.YGCBase> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (YGCBase.YGCBase R in Records.OrderBy(el => el))
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class NCOMaintenanceFileReader : FileReader
        {
            private new string FileLocation;
            private StreamReader FileReader;

            public new bool EOF { get { return this.FileReader.EndOfStream; } }

            public NCOMaintenanceFileReader(string FileLocation)
            {
                this.FileLocation = FileLocation;
                this.FileReader = new StreamReader(this.FileLocation);
            }

            public NCOBase.Record ReadMaintenanceRecord()
            {
                return NCORecordTypeIdentifier.GetRecordType(this.FileReader.ReadLine());
            }

            public NCOBase.NCOList<NCOBase.Record> ReadMaintenanceFile()
            {
                NCOBase.NCOList<NCOBase.Record> List = new NCOBase.NCOList<NCOBase.Record>();
                this.FileReader.BaseStream.Position = 0;
                while (!this.EOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<NCOBase.Record> GetAccountRecords(CLS.Account Account, List<NCOBase.Record> Records)
            {
                List<NCOBase.RecordBase> AccountRecords = Records.OfType<NCOBase.RecordBase>().ToList();
                return AccountRecords.FindAll(el => el.AccountNumber.Value == Account.Forw_RefNo).OfType<NCOBase.Record>().ToList();
            }

            public static void WriteNewFile(string FileName, List<NCOBase.Record> Records)
            {
                NCO.RecordType00 OHead = Records.OfType<NCO.RecordType00>().FirstOrDefault();
                if (OHead == null) throw new NotImplementedException("No PLX Header (Record Type 00) Could Be Found in Placement File");
                Records.RemoveAll(el => el is NCO.RecordType00);
                Records.RemoveAll(el => el is NCO.RecordType99);
                NCO.RecordType00 HR = new NCO.RecordType00();
                NCO.RecordType99 TR = new NCO.RecordType99();
                HR.CreateDateTime.Value = DateTime.Now;
                HR.NCOID.Value = OHead.NCOID.Value;
                HR.ReceiverID.Value = OHead.ReceiverID.Value;
                Records.Insert(0, HR);
                try
                {
                    TR.TotalAmountOfAccounts.Value = Records.OfType<NCO.RecordType01>().Sum(el => el.TotalBalance.Value);
                }
                catch { TR.TotalAmountOfAccounts.Value = 0; }
                TR.TotalRecordType01.Value = Records.OfType<NCO.RecordType01>().Count();
                TR.TotalRecordType02.Value = Records.OfType<NCO.RecordType02>().Count();
                TR.TotalRecordType03.Value = Records.OfType<NCO.RecordType03>().Count();
                TR.TotalRecordType04.Value = Records.OfType<NCO.RecordType04>().Count();
                TR.TotalRecordType05.Value = Records.OfType<NCO.RecordType05>().Count();
                TR.TotalRecordType06.Value = Records.OfType<NCO.RecordType06>().Count();
                TR.TotalRecordType07.Value = Records.OfType<NCO.RecordType07>().Count();
                TR.TotalRecordType08.Value = Records.OfType<NCO.RecordType08>().Count();
                TR.TotalRecordType09.Value = Records.OfType<NCO.RecordType09>().Count();
                TR.TotalRecordType10.Value = Records.OfType<NCO.RecordType10>().Count();
                Records.Add(TR);
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (NCOBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.FileLocation;
            }
        }

        public class TrakMaintenanceFileReader : MaintenanceFileReader
        {
            private string MaintenanceFileLocation;
            private StreamReader MaintenanceFileReader;

            public bool MaintenanceEOF { get { return this.MaintenanceFileReader.EndOfStream; } }

            public TrakMaintenanceFileReader(string FileLocation)
            {
                this.MaintenanceFileLocation = FileLocation;
                this.MaintenanceFileReader = new StreamReader(this.MaintenanceFileLocation);
            }

            public Trak.Base.RecordTypeBase ReadMaintenanceRecord()
            {
                return TrakMaintenanceTypeIdentifier.GetMaintenanceRecord(this.MaintenanceFileReader.ReadLine());
            }

            public Trak.Base.TrakList<Trak.Base.RecordTypeBase> ReadMaintenanceFile()
            {
                Trak.Base.TrakList<Trak.Base.RecordTypeBase> List = new Trak.Base.TrakList<Trak.Base.RecordTypeBase>();
                this.MaintenanceFileReader.BaseStream.Position = 0;
                while (!this.MaintenanceEOF)
                {
                    List.Add(this.ReadMaintenanceRecord());
                }
                return List;
            }

            public static List<Trak.Base.RecordTypeBase> GetAccountRecords(CLS.Account Account, List<Trak.Base.RecordTypeBase> Records)
            {
                List<Trak.TrakPlacement> AccountRecords = Records.OfType<Trak.TrakPlacement>().ToList();
                return AccountRecords.FindAll(el => el.OriginalAccountNumber.Value == Account.Forw_FileNo).OfType<Trak.Base.RecordTypeBase>().ToList();
            }

            public static void WriteNewFile(string FileName, List<NANBase.Record> Records)
            {
                System.IO.StreamWriter sw = new StreamWriter(FileName);
                foreach (NANBase.Record R in Records)
                {
                    sw.WriteLine(R);
                    sw.Flush();
                }
                sw.Close();
            }

            public override string ToString()
            {
                return this.MaintenanceFileLocation;
            }
        }

        #endregion

        #region Record Identifiers

        public static class YGCRecordTypeIdentifier
        {
            public static YGCBase.YGCBase GetRecordType(string Record)
            {
                try
                {
                    string myType = "RecordTypes.YGC.RecordType" + (new YGCBaseRecord(Record).RECORD).ToString();
                    Type T = Type.GetType(myType);
                    return (YGCBase.YGCBase)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static class PLXRecordTypeIdentifier
        {
            public static PLX.Base.RecordTypeBase GetRecordType(string Record)
            {
                try
                {
                    string myType = "RecordTypes.PLX." + (new PLXBaseRecord(Record).RecordType).ToString();
                    Type T = Type.GetType(myType);
                    return (PLX.Base.RecordTypeBase)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static class NCORecordTypeIdentifier
        {
            public static NCO.Base.Record GetRecordType(string Record)
            {
                try
                {
                    string myType = "RecordTypes.NCO." + (new NCOBaseRecord(Record)).RecordType.Value.ToString();
                    Type T = Type.GetType(myType);
                    NCO.Base.Record R = (NCO.Base.Record)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                    if (R is RecordTypes.NCO.RecordType08)
                    {
                        try
                        {
                            T = typeof(RecordTypes.NCO.RecordType08<>).MakeGenericType(Type.GetType("RecordTypes.NCO.Transactions." + ((RecordTypes.NCO.RecordType08)R).TransactionCode.Value.ToString()));
                            return (NCO.Base.Record)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                        }
                        catch
                        {
                            return R;
                        }
                    }
                    else
                    {
                        return R;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static class RMSRecordTypeIdentifier
        {
            public static RMS.Base.Record GetRecordType(string Record)
            {
                try
                {
                    if (Record.Substring(0, 6) == "RMSHDR")
                    {
                        return new RMS.HeaderRecord(Record);
                    }
                    else
                    {
                        RMSAccount Acct = new RMSAccount(Record);
                        if (Acct.RecordType.Value == RMS.Enums.RecordTypes.Debtor)
                            return new RMS.DebtorRecord(Record);
                        else
                            return new RMS.CoMakerRecord(Record);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class FordRMSRecordTypeIdentifier
        {
            public static RMS.Base.Record GetRecordType(string Record)
            {
                try
                {
                    if (Record.Substring(0, 6) == "RMSHDR")
                    {
                        return new Ford.HeaderRecord(Record);
                    }
                    else
                    {
                        RMSAccount Acct = new RMSAccount(Record);
                        switch (Acct.RecordType.Value)
                        {
                            case RMS.Enums.RecordTypes.Debtor:
                                return new Ford.DebtorRecord(Record);
                            case RMS.Enums.RecordTypes.CoMaker:
                                return new Ford.CoMakerRecord(Record);
                            case RMS.Enums.RecordTypes.Notes:
                                return new Ford.NotesRecord(Record);
                            case RMS.Enums.RecordTypes.Collateral:
                                return new Ford.CollateralRecord(Record);
                            case RMS.Enums.RecordTypes.Legal:
                                return new Ford.LegalRecord(Record);
                            case RMS.Enums.RecordTypes.Credit:
                                return new Ford.CreditRecord(Record);
                            case RMS.Enums.RecordTypes.UserDefined:
                                return new Ford.UserRecord(Record);
                            default:
                                return null;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class CitiRMSRecordTypeIdentifier
        {
            public static RMS.Base.Record GetRecordType(string Record)
            {
                try
                {
                    if (Record.Substring(0, 6) == "RMSHDR")
                    {
                        return new Citi.HeaderRecord(Record);
                    }
                    else
                    {
                        RMSAccount Acct = new RMSAccount(Record);
                        switch (Acct.RecordType.Value)
                        {
                            case RMS.Enums.RecordTypes.Debtor:
                                return new Citi.DebtorRecord(Record);
                            case RMS.Enums.RecordTypes.CoMaker:
                                CitiAccount CA = new CitiAccount(Record);
                                if (CA.SequenceNumber.Value != null)
                                {
                                    if (CA.SequenceNumber.Value.Value == 7)
                                        return new Citi.CoMakerRecord07(Record);
                                    else if (CA.SequenceNumber.Value.Value == 8)
                                        return new Citi.CoMakerRecord08(Record);
                                    else
                                        return new Citi.CoMakerRecord(Record);
                                }
                                else
                                    return new Citi.CoMakerRecord(Record);
                            case RMS.Enums.RecordTypes.Notes:
                                return new Citi.NotesRecord(Record);
                            case RMS.Enums.RecordTypes.Collateral:
                                //return new Citi.CollateralRecord(Record);
                                return null;
                            case RMS.Enums.RecordTypes.Legal:
                                return new Citi.LegalRecord(Record);
                            case RMS.Enums.RecordTypes.Credit:
                                //return new Citi.CreditRecord(Record);
                                return null;
                            case RMS.Enums.RecordTypes.UserDefined:
                                //return new Citi.UserRecord(Record);
                                return null;
                            case RMS.Enums.RecordTypes.Extra:
                                return new Citi.ExtraRecord(Record);
                            default:
                                return null;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class NANRecordTypeIdentifier
        {
            public static NANBase.Record GetRecordType(string Record)
            {
                try
                {
                    if (Record.Length >= 5998)
                        return new NAN.NewBusinessRecord(Record);
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class TrakRecordTypeIdentifier
        {
            public static Trak.Base.RecordTypeBase GetRecordType(string Record)
            {
                if (Record.Split('|').GetUpperBound(0) >= 142)
                {
                    Trak.TrakPlacement Rec = new Trak.TrakPlacement(Record);
                    if (Rec.Instruction.Value.ToUpper().Trim() != "INSTRUCTION")
                        return Rec;
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static class CollectionTriggersRecordTypeIdentifier
        {
            public static RecordTypes2.Experian.CollectionTriggers.Base.Record GetRecordType(string Record)
            {
                try
                {
                    if (Record.StartsWith("HDR"))
                        return new RecordTypes2.Experian.CollectionTriggers.Header(Record);
                    else if (Record.StartsWith("NTC"))
                        return new RecordTypes2.Experian.CollectionTriggers.Trigger(Record);
                    else if (Record.StartsWith("T"))
                    {
                        if (Record.Substring(1, 2) == "99")
                            return new RecordTypes2.Experian.CollectionTriggers.Footer(Record);
                        else
                            return new RecordTypes2.Experian.CollectionTriggers.TriggerTotals(Record);
                    }
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Maintenance Record Identifiers

        public static class NANMaintenanceTypeIdentifier
        {
            public static NANBase.Record GetMaintenanceRecord(string Record)
            {
                try
                {
                    NAN.Maintenance.TransactionRecord TR = new NAN.Maintenance.TransactionRecord(Record);
                    Type SubT = Type.GetType("RecordTypes.NAN.Maintenance.Transactions." + TR.TransactionCode.Value.ToString() + "_RECORD");
                    if (SubT != null)
                    {
                        try
                        {
                            System.Reflection.ConstructorInfo CI = typeof(NAN.Maintenance.TransactionRecord<>).MakeGenericType(new Type[] { SubT }).GetConstructor(new Type[] { typeof(string) });
                            return (NANBase.Record)CI.Invoke(new object[] { Record });
                        }
                        catch
                        {
                            return TR;
                        }
                    }
                    else
                    {
                        return TR;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class RMSMaintenanceTypeIdentifier
        {
            public static RMSBase.Record GetMaintenanceRecord(string Record)
            {
                try
                {
                    RMS.Maintenance.MaintenanceInfo MI = new RMS.Maintenance.MaintenanceInfo(Record);
                    switch (MI.TransactionCode.Value)
                    {
                        case RMS.Maintenance.TransactionCode.Header:
                            return new RMS.Maintenance.HeaderRecord(Record);
                        case RMS.Maintenance.TransactionCode.GeneralComment:
                        case RMS.Maintenance.TransactionCode.LegalComment:
                            return new RMS.Maintenance.CommentRecord(Record);
                        default:
                            return new RMS.Maintenance.FinanacialRecord(Record);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static class FordMaintenanceTypeIdentifier
        {
            public static RMSBase.Record GetMaintenanceRecord(string Record)
            {
                try
                {
                    RMS.Maintenance.MaintenanceInfo MI = new RMS.Maintenance.MaintenanceInfo(Record);
                    switch (MI.TransactionCode.Value)
                    {
                        case RMS.Maintenance.TransactionCode.Header:
                            return new RMS.Maintenance.HeaderRecord(Record);
                        case RMS.Maintenance.TransactionCode.GeneralComment:
                        case RMS.Maintenance.TransactionCode.LegalComment:
                            return new RMS.Maintenance.CommentRecord(Record);
                        default:
                            if (Ford.Dictionaries.FinancialTransactionCodes.ContainsKey(MI.TransactionCode.DataString))
                                return new Ford.Maintenance.FinanacialRecord(Record);
                            else
                                return GetMaintenanceTransaction(Record, new Ford.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.Transaction>(Record).FieldCode.Value);
                    }
                }
                catch
                {
                    return null;
                }
            }

            public static Ford.Maintenance.MaintenanceRecord GetMaintenanceTransaction(string Record, RMS.Maintenance.MaintenanceCode code)
            {
                switch (code)
                {
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Amount:
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Amount2:
                    case RMS.Maintenance.MaintenanceCode.MASINT:
                    case RMS.Maintenance.MaintenanceCode.MASPRI:
                    case RMS.Maintenance.MaintenanceCode.Past_Due_Amount:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_1:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_2:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_3:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_4:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_5:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_7:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_8:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_1:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_2:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_3:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_4:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_5:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_6:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_7:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_8:
                        return new Ford.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.DecimalTransaction>(Record);
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Purchase_Date:
                    case RMS.Maintenance.MaintenanceCode.Date_Charged_Off:
                    case RMS.Maintenance.MaintenanceCode.AP_Transaction_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Billing_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Interest_Date:
                    case RMS.Maintenance.MaintenanceCode.Action_Code_Date:
                    case RMS.Maintenance.MaintenanceCode.Execution_Date:
                    case RMS.Maintenance.MaintenanceCode.Suit_Service_Date:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_1:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_2:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_3:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_4:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_5:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_1:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_2:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_3:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_4:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_5:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_7:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_8:
                        return new Ford.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.DateTransaction>(Record);
                    default:
                        return new Ford.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.Transaction>(Record);
                }
            }
        }

        public static class CitiMaintenanceTypeIdentifier
        {
            public static RMSBase.Record GetMaintenanceRecord(string Record)
            {
                try
                {
                    Citi.Maintenance.MaintenanceInfo MI = new Citi.Maintenance.MaintenanceInfo(Record);
                    switch (MI.TransactionCode.Value)
                    {
                        case RMS.Maintenance.TransactionCode.Header:
                            return new Citi.Maintenance.HeaderRecord(Record);
                        case RMS.Maintenance.TransactionCode.Maintenance:
                        case RMS.Maintenance.TransactionCode.Recall:
                            return GetMaintenanceTransaction(Record, new Citi.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.Transaction>(Record).FieldCode.Value);
                        case RMS.Maintenance.TransactionCode.GeneralComment:
                        case RMS.Maintenance.TransactionCode.LegalComment:
                            return new Citi.Maintenance.CommentRecord(Record);
                        default:
                            return new Citi.Maintenance.FinanacialRecord(Record);
                    }
                }
                catch
                {
                    return null;
                }
            }

            private static Citi.Maintenance.MaintenanceRecord GetMaintenanceTransaction(string Record, RMS.Maintenance.MaintenanceCode Code)
            {
                switch (Code)
                {
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Amount:
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Amount2:
                    case RMS.Maintenance.MaintenanceCode.MASINT:
                    case RMS.Maintenance.MaintenanceCode.MASPRI:
                    case RMS.Maintenance.MaintenanceCode.Past_Due_Amount:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_1:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_2:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_3:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_4:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_5:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_7:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Number_8:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_1:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_2:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_3:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_4:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_5:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_6:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_7:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Number_8:
                        return new Citi.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.DecimalTransaction>(Record);
                    case RMS.Maintenance.MaintenanceCode.Last_Payment_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Purchase_Date:
                    case RMS.Maintenance.MaintenanceCode.Date_Charged_Off:
                    case RMS.Maintenance.MaintenanceCode.AP_Transaction_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Billing_Date:
                    case RMS.Maintenance.MaintenanceCode.Last_Interest_Date:
                    case RMS.Maintenance.MaintenanceCode.Action_Code_Date:
                    case RMS.Maintenance.MaintenanceCode.Execution_Date:
                    case RMS.Maintenance.MaintenanceCode.Suit_Service_Date:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_1:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_2:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_3:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_4:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_5:
                    case RMS.Maintenance.MaintenanceCode.Legal_User_Defined_Date_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_1:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_2:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_3:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_4:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_5:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_6:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_7:
                    case RMS.Maintenance.MaintenanceCode.User_Defined_Field_Date_8:
                        return new Citi.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.DateTransaction>(Record);
                    default:
                        return new Citi.Maintenance.MaintenanceRecord<RMS.Maintenance.Transactions.Transaction>(Record);
                }
            }
        }

        public static class NCOMaintenanceTypeIdentifier
        {
            public static NCOBase.Record GetMaintenanceRecord(string Record) { return NCORecordTypeIdentifier.GetRecordType(Record); }
        }

        public static class PLXMaintenanceTypeIdentifier
        {
            public static PLX2.Base.RecordTypeBase GetMaintenanceRecord(string Record)
            {
                try
                {
                    string myType = "RecordTypes.PLX2." + (new PLX2BaseRecord(Record).RecordType.Value).ToString();
                    Type T = Type.GetType(myType);
                    return (PLX2.Base.RecordTypeBase)T.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { Record });
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static class YGCMaintenanceTypeIdentifier
        {
            public static YGCBase.YGCBase GetMaintenanceRecord(string Record) { return YGCRecordTypeIdentifier.GetRecordType(Record); }
        }

        public static class TrakMaintenanceTypeIdentifier
        {
            public static Trak.Base.RecordTypeBase GetMaintenanceRecord(string Record)
            {
                if (Record.Split('|').GetUpperBound(0) >= 142)
                    return new Trak.TrakPlacement(Record);
                else
                    return null;
            }
        }

        #endregion

        #region Error Trapping

        public class RecordErrors
        {
            public bool HadError { get; private set; }
            public string Info { get; private set; }

            public RecordErrors(bool HadError, string Info)
            {
                this.HadError = HadError;
                this.Info = Info;
            }
        }

        #endregion

        #region Disambiguate Abstract Classes

        internal class YGCBaseRecord : YGC.Base.YGCBase
        {
            public override YGC.DataTypes.YGCString FIRM_ID { get { throw new NotImplementedException(); } protected set { throw new NotImplementedException(); } }
            public override YGC.DataTypes.YGCString FORW_ID { get { throw new NotImplementedException(); } protected set { throw new NotImplementedException(); } }

            public YGCBaseRecord(string Record) : base(Record) { }

            public override Type GetType()
            {
                return YGCRecordTypeIdentifier.GetRecordType(this.RECORD).GetType();
            }
        }
        internal class PLXBaseRecord : PLX.Base.RecordTypeBase
        {
            public PLXBaseRecord(string Record) : base(Record) { }
        }
        internal class PLX2BaseRecord : PLX2.Base.RecordTypeBase
        {
            public PLX2BaseRecord(string Record) : base(Record) { }
        }
        internal class NCOBaseRecord : NCO.Base.Record
        {
            public NCOBaseRecord(string Record) : base(Record) { }
        }
        internal class RMSAccount : RMS.AccountInfo
        {
            public RMSAccount(string Record) : base(Record) { }
        }
        internal class CitiAccount : Citi.AccountRecord
        {
            public CitiAccount(string Record) : base(Record) { }
        }
        internal class FordAccount : Ford.AccountRecord
        {
            public FordAccount(string Record) : base(Record) { }
        }

        #endregion
    }
}
