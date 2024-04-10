using System;
using System.Collections.Generic;

namespace RecordTypes
{
    namespace EDI
    {
        namespace EDIRecords
        {
            public interface RecordType<T>
            {
                T GetRecordType(string Record);
                List<T> GetAccountRecords(CLS.Account Account, List<T> Records);
                void BasicFileMaintenance(List<T> Records);
                void AddHeaders(List<T> BaseList, List<T> AddList);
                T PlacementRecord(List<T> Records, string AccountNumber);
                int NewAccounts(List<T> Records);
                IEnumerable<List<T>> UniqueAccountListing(List<T> Records);
            }
        }

        namespace EDIDataTypes
        {
            /// <summary>
            /// Base Data Type for Flat Files
            /// </summary>
            public abstract class DataType : IFormattable
            {
                private string _Data;
                private bool _Justify;
                private byte _Padded;
                public int DataLength { get; private set; }

                public virtual string DataString
                {
                    get
                    {
                        if (!string.IsNullOrEmpty(this._Data))
                            return this._Data;
                        else
                            return "".PadRight(this.DataLength);
                    }
                    set
                    {
                        if (_Justify)
                            this._Data = value.Length < DataLength ? value.PadRight(DataLength, ' ') : value.Substring(0, DataLength);
                        else
                            this._Data = value.Length < DataLength ? value.PadLeft(DataLength, ' ') : value.Substring(0, DataLength);
                    }
                }
                protected virtual string Value
                {
                    get { return this._Data.Trim(); }
                    set
                    {
                        if (string.IsNullOrEmpty(value))
                            value = "";
                        string Clean = value.Replace("\r\n", " ").Replace("\n", " ").Trim();
                        if (_Justify)
                            this._Data = Clean.Length + (2 * _Padded) < DataLength ? ("".PadLeft(_Padded, ' ') + Clean + "".PadRight(_Padded, ' ')).PadRight(DataLength, ' ') : ("".PadLeft(_Padded, ' ') + Clean).Substring(0, DataLength - _Padded).PadRight(DataLength, ' ');
                        else
                            this._Data = Clean.Length + (2 * _Padded) < DataLength ? ("".PadLeft(_Padded, ' ') + Clean + "".PadRight(_Padded, ' ')).PadLeft(DataLength, ' ') : ("".PadLeft(_Padded, ' ') + Clean).Substring(0, DataLength - _Padded).PadRight(DataLength, ' ');
                    }
                }

                public DataType(int DataLength, bool LeftJustified = true, byte PaddingCharacters = 0)
                {
                    this._Justify = LeftJustified;
                    this._Padded = PaddingCharacters;
                    this.DataLength = DataLength;
                    this.DataString = "";
                }

                public override string ToString()
                {
                    return ToString(string.Empty, null);
                }

                public virtual string ToString(string format, IFormatProvider formatProvider)
                {
                    if (format == "G")
                    {
                        return $"{this.Value}";
                    }
                    else
                    {
                        return this.DataString;
                    }
                }
            }

            public abstract class EncapsulatedDataType
            {
                private StringHolder _Data;
                private bool _Padded;
                private char _PadChar;
                public int DataLength { get; private set; }

                protected virtual string DataString
                {
                    get
                    {
                        return this._Data.Value;
                    }
                    set
                    {
                        this._Data.Value = value.Length < DataLength || DataLength == 0 ? value : value.Substring(0, DataLength);
                    }
                }
                protected virtual string Value
                {
                    get { return this._Data.Value.Trim(); }
                    set
                    {
                        string Clean = string.IsNullOrEmpty(value) ? "" : value.Replace("\r\n", " ").Replace("\n", " ").Trim();
                        this.DataString = Clean.Length < DataLength && _Padded ? Clean.PadLeft(this.DataLength, _PadChar) : Clean;
                    }
                }

                public EncapsulatedDataType(StringHolder Data, int DataLength = 0, bool PaddingCharacters = false, char PaddingCharacter = ' ')
                {
                    this._Data = Data;
                    this.DataLength = DataLength;
                    this._Padded = PaddingCharacters;
                    this._PadChar = PaddingCharacter;
                }

                public override string ToString()
                {
                    return this.DataString;
                }
            }

            public class StringHolder
            {
                public string Value { get; set; }

                public StringHolder(string Value)
                {
                    this.Value = Value;
                }

                public override string ToString()
                {
                    return this.Value;
                }

                public static implicit operator StringHolder(string s)
                {
                    return new StringHolder(s);
                }
                public static implicit operator string(StringHolder sh)
                {
                    return sh.Value;
                }
            }
        }
    }
}
