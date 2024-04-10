using System;

namespace RecordTypes
{
    public enum SupportedEDITypes
    {
        None = 0,
        Citi,
        Ford,
        NAN,
        NCO,
        PLX,
        RMS,
        YGC,
        Trak,
        DantomNCOA
    }

    public static class EDITypeFinder
    {
        public static string GetEDINamespace(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case RecordTypes.SupportedEDITypes.Citi:
                    return "RecordTypes.Citi";
                case RecordTypes.SupportedEDITypes.Ford:
                    return "RecordTypes.Ford";
                case RecordTypes.SupportedEDITypes.NAN:
                    return "RecordTypes.NAN";
                case RecordTypes.SupportedEDITypes.NCO:
                    return "RecordTypes.NCO";
                case RecordTypes.SupportedEDITypes.PLX:
                    return "RecordTypes.PLX";
                case RecordTypes.SupportedEDITypes.RMS:
                    return "RecordTypes.RMS";
                case RecordTypes.SupportedEDITypes.YGC:
                    return "RecordTypes.YGC";
                case SupportedEDITypes.Trak:
                    return "RecordTypes.Trak";
                case SupportedEDITypes.DantomNCOA:
                    return "RecordTypes.Dantom.NCOA";
                default:
                    return "";
            }
        }
        public static string[] GetEDINamespaces(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case RecordTypes.SupportedEDITypes.Citi:
                    return new string[] { "RecordTypes.Citi", "RecordTypes.RMS" };
                case RecordTypes.SupportedEDITypes.Ford:
                    return new string[] { "RecordTypes.Ford", "RecordTypes.RMS" };
                case RecordTypes.SupportedEDITypes.NAN:
                    return new string[] { "RecordTypes.NAN" };
                case RecordTypes.SupportedEDITypes.NCO:
                    return new string[] { "RecordTypes.NCO" };
                case RecordTypes.SupportedEDITypes.PLX:
                    return new string[] { "RecordTypes.PLX" };
                case RecordTypes.SupportedEDITypes.RMS:
                    return new string[] { "RecordTypes.RMS" };
                case RecordTypes.SupportedEDITypes.YGC:
                    return new string[] { "RecordTypes.YGC" };
                case SupportedEDITypes.Trak:
                    return new string[] { "RecordTypes.Trak" };
                case SupportedEDITypes.DantomNCOA:
                    return new string[] { "RecordTypes.Dantom.NCOA" };
                default:
                    return new string[] { "" };
            }
        }
        public static Type GetMainRecord(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(Citi.DebtorRecord);
                case SupportedEDITypes.Ford:
                    return typeof(Ford.DebtorRecord);
                case SupportedEDITypes.NAN:
                    return typeof(NAN.NewBusinessRecord);
                case SupportedEDITypes.NCO:
                    return typeof(NCO.RecordType01);
                case SupportedEDITypes.PLX:
                    return typeof(PLX.PlacementRecord);
                case SupportedEDITypes.RMS:
                    return typeof(RMS.DebtorRecord);
                case SupportedEDITypes.YGC:
                    return typeof(YGC.RecordType01);
                case SupportedEDITypes.Trak:
                    return typeof(Trak.TrakPlacement);
                case SupportedEDITypes.DantomNCOA:
                    return typeof(Dantom.NCOA.Record);
                default:
                    return typeof(CLS.Account);
            }
        }
        public static Type GetBaseRecord(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(RMS.Base.Record);
                case SupportedEDITypes.Ford:
                    return typeof(RMS.Base.Record);
                case SupportedEDITypes.NAN:
                    return typeof(NAN.Base.Record);
                case SupportedEDITypes.NCO:
                    return typeof(NCO.Base.Record);
                case SupportedEDITypes.PLX:
                    return typeof(PLX.Base.RecordTypeBase);
                case SupportedEDITypes.RMS:
                    return typeof(RMS.Base.Record);
                case SupportedEDITypes.YGC:
                    return typeof(YGC.Base.YGCBase);
                case SupportedEDITypes.Trak:
                    return typeof(Trak.Base.RecordTypeBase);
                case SupportedEDITypes.DantomNCOA:
                    return typeof(Dantom.NCOA.Record);
                default:
                    return typeof(CLS.Account);
            }
        }
        public static Type GetAnonTypeDeriv(Type RecordType)
        {
            if (RecordType == typeof(NCO.RecordType08<>))
            {
                return typeof(NCO.Base.TransactionCode);
            }
            else if (RecordType == typeof(RMS.Maintenance.MaintenanceRecord<>))
            {
                return typeof(RMS.Maintenance.Transactions.Transaction);
            }
            else if (RecordType == typeof(Citi.Maintenance.MaintenanceRecord<>))
            {
                return typeof(RMS.Maintenance.Transactions.Transaction);
            }
            else if (RecordType == typeof(Ford.Maintenance.MaintenanceRecord<>))
            {
                return typeof(RMS.Maintenance.Transactions.Transaction);
            }
            else if (RecordType == typeof(NAN.Maintenance.TransactionRecord<>))
            {
                return typeof(NAN.Maintenance.Transactions.Transaction);
            }
            else
                return null;
        }
        public static FileReaders.FileReader GetFileReader(SupportedEDITypes EDIType, string FileLocation)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return new FileReaders.CitiRMSFileReader(FileLocation);
                case SupportedEDITypes.Ford:
                    return new FileReaders.FordRMSFileReader(FileLocation);
                case SupportedEDITypes.NAN:
                    return new FileReaders.NANFileReader(FileLocation);
                case SupportedEDITypes.NCO:
                    return new FileReaders.NCOFileReader(FileLocation);
                case SupportedEDITypes.PLX:
                    return new FileReaders.PLXFileReader(FileLocation);
                case SupportedEDITypes.RMS:
                    return new FileReaders.RMSFileReader(FileLocation);
                case SupportedEDITypes.YGC:
                    return new FileReaders.YGCFileReader(FileLocation);
                case SupportedEDITypes.Trak:
                    return new FileReaders.TrakFileReader(FileLocation);
                case SupportedEDITypes.DantomNCOA:
                    return new FileReaders.Dantom_NCOAFileReader(FileLocation);
                default:
                    return null;
            }
        }
    }

    internal static class RecordTypeIDFinder
    {
        internal static Type GetRecordTypeID(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(FileReaders.CitiRMSRecordTypeIdentifier);
                case SupportedEDITypes.Ford:
                    return typeof(FileReaders.FordRMSRecordTypeIdentifier);
                case SupportedEDITypes.NAN:
                    return typeof(FileReaders.NANRecordTypeIdentifier);
                case SupportedEDITypes.NCO:
                    return typeof(FileReaders.NCORecordTypeIdentifier);
                case SupportedEDITypes.PLX:
                    return typeof(FileReaders.PLXRecordTypeIdentifier);
                case SupportedEDITypes.RMS:
                    return typeof(FileReaders.RMSRecordTypeIdentifier);
                case SupportedEDITypes.YGC:
                    return typeof(FileReaders.YGCRecordTypeIdentifier);
                case SupportedEDITypes.Trak:
                    return typeof(FileReaders.TrakRecordTypeIdentifier);
                case SupportedEDITypes.DantomNCOA:
                    return typeof(FileReaders.Dantom_NCOAFileReader);
                default:
                    return null;
            }
        }
        internal static Type GetMaintenanceRecordTypeID(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(FileReaders.CitiMaintenanceTypeIdentifier);
                case SupportedEDITypes.Ford:
                    return typeof(FileReaders.FordMaintenanceTypeIdentifier);
                case SupportedEDITypes.NAN:
                    return typeof(FileReaders.NANMaintenanceTypeIdentifier);
                case SupportedEDITypes.NCO:
                    return typeof(FileReaders.NCOMaintenanceTypeIdentifier);
                case SupportedEDITypes.PLX:
                    return typeof(FileReaders.PLXMaintenanceTypeIdentifier);
                case SupportedEDITypes.RMS:
                    return typeof(FileReaders.RMSMaintenanceTypeIdentifier);
                case SupportedEDITypes.YGC:
                    return typeof(FileReaders.YGCMaintenanceTypeIdentifier);
                case SupportedEDITypes.Trak:
                    return typeof(FileReaders.TrakMaintenanceTypeIdentifier);
                default:
                    return null;
            }
        }
        internal static Type GetFileReaderType(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(FileReaders.CitiRMSFileReader);
                case SupportedEDITypes.Ford:
                    return typeof(FileReaders.FordRMSFileReader);
                case SupportedEDITypes.NAN:
                    return typeof(FileReaders.NANFileReader);
                case SupportedEDITypes.NCO:
                    return typeof(FileReaders.NCOFileReader);
                case SupportedEDITypes.PLX:
                    return typeof(FileReaders.PLXFileReader);
                case SupportedEDITypes.RMS:
                    return typeof(FileReaders.RMSFileReader);
                case SupportedEDITypes.YGC:
                    return typeof(FileReaders.YGCFileReader);
                case SupportedEDITypes.Trak:
                    return typeof(FileReaders.TrakFileReader);
                case SupportedEDITypes.DantomNCOA:
                    return typeof(FileReaders.Dantom_NCOAFileReader);
                default:
                    return null;
            }
        }
        internal static Type GetMaintenanceFileReaderType(SupportedEDITypes EDIType)
        {
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    return typeof(FileReaders.CitiMaintenanceFileReader);
                case SupportedEDITypes.Ford:
                    return typeof(FileReaders.FordMaintenanceFileReader);
                case SupportedEDITypes.NAN:
                    return typeof(FileReaders.NANMaintenanceFileReader);
                case SupportedEDITypes.NCO:
                    return typeof(FileReaders.NCOMaintenanceFileReader);
                case SupportedEDITypes.PLX:
                    return typeof(FileReaders.PLXMaintenanceFileReader);
                case SupportedEDITypes.RMS:
                    return null;
                case SupportedEDITypes.YGC:
                    return typeof(FileReaders.YGCMaintenanceFileReader);
                case SupportedEDITypes.Trak:
                    return typeof(FileReaders.TrakMaintenanceFileReader);
                case SupportedEDITypes.DantomNCOA:
                    return null;
                default:
                    return null;
            }
        }
    }
}