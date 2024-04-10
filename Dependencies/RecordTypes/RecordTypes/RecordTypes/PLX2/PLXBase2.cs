using RecordTypes.EDI.EDIDataTypes;
using RecordTypes.PLX.Enums;
using System.Collections.Generic;

namespace RecordTypes.PLX2
{
    namespace Base
    {
        public abstract class RecordTypeBase : Delimited.Base.TabDelimitedBaseRecord
        {
            public DataTypes.PLXEnum<Types, TypeValues> RecordType { get; private set; }

            public RecordTypeBase(Types RecordType) : base(Dictionaries.RecordLineItems[RecordType])
            {
                this.RecordType = new DataTypes.PLXEnum<Types, TypeValues>("");
                this.RecordType.Value = RecordType;
            }
            public RecordTypeBase(string Record) : base(Record)
            {
                this.RecordType = new DataTypes.PLXEnum<Types, TypeValues>(this.LineItems[0]);
            }
        }

        public abstract class RecordBase : RecordTypeBase
        {
            public DataTypes.PLXNumber AccountID { get; private set; }
            public DataTypes.PLXString AccountNumber { get; private set; }

            public RecordBase(string Record)
                : base(Record)
            {
                this.AccountID = new DataTypes.PLXNumber(this.LineItems[1]);
                this.AccountNumber = new DataTypes.PLXString(this.LineItems[2]);
            }
        }
    }

    namespace DataTypes
    {
        public class PLXString : Delimited.DataTypes.DelimitedString
        {
            public PLXString(StringHolder DataString) : base(DataString) { }
        }

        public class PLXNumber : Delimited.DataTypes.DelimitedNumber
        {
            public PLXNumber(StringHolder DataString, int Length = 0, bool Padded = false) : base(DataString, Length, Padded, '0') { }
        }

        public class PLXDecimal : Delimited.DataTypes.DelimitedDecimal
        {
            public PLXDecimal(StringHolder DataString, int Precision = 0, int Scale = 2) : base(DataString, Precision, Scale) { }
        }

        public class PLXDate : Delimited.DataTypes.DelimitedDate
        {
            public PLXDate(StringHolder DataString) : base(DataString, "MM/dd/yyyy") { }
        }

        public class PLXDateTime : Delimited.DataTypes.DelimitedDate
        {
            public PLXDateTime(StringHolder DataString) : base(DataString, "MM/dd/yyyy HH.mm.ss") { }
        }

        public class PLXEnum<T> : Delimited.DataTypes.DelimitedEnum<T>
        {
            public PLXEnum(StringHolder DataString, int DataLength = 2) : base(DataString, DataLength, true) { }
        }

        public class PLXEnum<T, Q> : Delimited.DataTypes.DelimitedEnum<T, Q>
        {
            public PLXEnum(StringHolder DataString) : base(DataString) { }
        }

        public class PLXDictionary<T> : Delimited.DataTypes.DelimitedDictionary<T> where T : struct
        {
            public PLXDictionary(StringHolder DataString, Dictionary<string, T> Lookup) : base(DataString, Lookup) { }
        }

        public class PLXBool : Delimited.DataTypes.DelimitedBool
        {
            public PLXBool(StringHolder DataString) : base(DataString, "Y", "N") { }
        }
    }

    static class Dictionaries
    {
        private static Dictionary<Types, int> _RecordLineItems { get; set; }
        public static Dictionary<Types, int> RecordLineItems { get { if (_RecordLineItems == null) LoadRecordLineItems(); return _RecordLineItems; } }

        private static void LoadRecordLineItems()
        {
            _RecordLineItems = new Dictionary<Types, int>()
            {
                { Types.Nothing, 1},
                { Types.FileHeader, 8},
                { Types.RecordHeader, 5},
                { Types.BorrowerRecord, 1},
                { Types.AccountBalanceRecord, 1},
                { Types.PlacementRecord, 1},
                { Types.PlaceBatchSummary, 1},
                { Types.AdditionalInfoRecord, 1},
                { Types.AlternateContactInfo, 1},
                { Types.DeceasedInfo, 1},
                { Types.LegalInfo, 1},
                { Types.RelatedAsset_Bank, 1},
                { Types.RelatedAsset_Employment, 1},
                { Types.RelatedAsset_Other, 1},
                { Types.RelatedAsset_RealEstate, 1},
                { Types.RelatedAsset_Vehicle, 1},
                { Types.AttorneyBondingInfo, 1},
                { Types.BankruptcyInfo, 1},
                { Types.JudgmentInfo, 1},
                { Types.LawsuitInfo, 1},
                { Types.InsufficientFundsInfo, 1},
                { Types.StatusCodeRecordInfo, 1},
                { Types.DormantJudgmentInfo, 1},
                { Types.AccountScoreInfo, 1},
                { Types.AdditionalInformationUpdateRecord, 30},
                { Types.AccountNumberUpdateRecord, 3},
                { Types.BankruptcyUpdateRecord, 8},
                { Types.BorrowerAddressUpdateRecord, 9},
                { Types.BorrowerPhoneUpdateRecord, 6 },
                { Types.BorrowerAdditionalInfoUpdateRecord, 11},
                { Types.BorrowerDeceasedUpdateRecord, 5 },
                { Types.PlacementUpdateRecord, 8 },
                { Types.ScoreUpdateRecord, 6 },
                { Types.RecallRecord, 8 },
                { Types.BalanceItemizationRecord, 20 },
                { Types.BalanceItemizationUpdateRecord, 20 }
            };
        }
    }
}
