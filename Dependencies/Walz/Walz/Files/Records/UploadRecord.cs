using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Records
{
    public class UploadRecord : BaseRecord
    {
        #region Private Properties
        private int _FileNo { get; set; }
        private int _Name1 { get; set; }
        private int _Name2 { get; set; }
        private int _Address1 { get; set; }
        private int _Address2 { get; set; }
        private int _City { get; set; }
        private int _State { get; set; }
        private int _Zip { get; set; }
        private int _BatchID { get; set; }
        private int _ReturnReceiptType { get; set; }
        private int _RestrictedDelivery { get; set; }
        private int _FormType { get; set; }
        private int _Sender { get; set; }
        private int _ChargeAmount { get; set; }
        private int _ChargeTo { get; set; }
        private int _ReturnName { get; set; }
        private int _ReturnAddress1 { get; set; }
        private int _ReturnAddress2 { get; set; }
        private int _ReturnCity { get; set; }
        private int _ReturnState { get; set; }
        private int _ReturnZip { get; set; }
        private int _Weight { get; set; }
        private int _Flat { get; set; }
        #endregion

        #region Public Properties
        public Enums.FileVersion Version { get; private set; }
        public string FileNo { get { return _FileNo == -1 ? "" : this.Segments[_FileNo]; } set { this.Segments[_FileNo] = value; } }
        public string Name1 { get { return _Name1 == -1 ? "" : this.Segments[_Name1]; } set { this.Segments[_Name1] = value; } }
        public string Name2 { get { return _Name2 == -1 ? "" : this.Segments[_Name2]; } set { this.Segments[_Name2] = value; } }
        public string Address1 { get { return _Address1 == -1 ? "" : this.Segments[_Address1]; } set { this.Segments[_Address1] = value; } }
        public string Address2 { get { return _Address2 == -1 ? "" : this.Segments[_Address2]; } set { this.Segments[_Address2] = value; } }
        public string City { get { return _City == -1 ? "" : this.Segments[_City]; } set { this.Segments[_City] = value; } }
        public string State { get { return _State == -1 ? "" : this.Segments[_State]; } set { this.Segments[_State] = value; } }
        public string Zip { get { return _Zip == -1 ? "" : this.Segments[_Zip]; } set { this.Segments[_Zip] = value; } }
        public int? BatchID { get { try { return _BatchID == -1 ? null : (int?)Convert.ToInt32(this.Segments[_BatchID]); } catch { return null; } } set { this.Segments[_BatchID] = value.HasValue ? value.Value.ToString() : ""; } }
        public Enums.ReturnReceiptType ReturnReceiptType { get { try { return _ReturnReceiptType == -1 ? Enums.ReturnReceiptType.Unknown : Dictionaries.ReturnReceiptStrings.FirstOrDefault(el => el.Value == this.Segments[_ReturnReceiptType]).Key; } catch { return Enums.ReturnReceiptType.Unknown; } } set { try { this.Segments[_ReturnReceiptType] = Dictionaries.ReturnReceiptStrings[value]; } catch { this.Segments[_ReturnReceiptType] = ""; } } }
        public bool RestrictedDelivery { get { try { return _RestrictedDelivery == -1 ? false : Convert.ToBoolean(this.Segments[_RestrictedDelivery]); } catch { return false; } } set { this.Segments[_RestrictedDelivery] = value ? "1" : "0"; } }
        public Enums.FormType FormType { get { try { return _FormType == -1 ? Enums.FormType.Unknown : Dictionaries.FormTypeStrings.FirstOrDefault(el => el.Value == this.Segments[_FormType]).Key; } catch { return Enums.FormType.Unknown; } } set { try { this.Segments[_FormType] = Dictionaries.FormTypeStrings[value]; } catch { this.Segments[_FormType] = ""; } } }
        public string Sender { get { return _Sender == -1 ? "" : this.Segments[_Sender]; } set { this.Segments[_Sender] = value; } }
        public string ChargeAmount { get { return _ChargeAmount == -1 ? "" : this.Segments[_ChargeAmount]; } set { this.Segments[_ChargeAmount] = value; } }
        public string ChargeTo { get { return _ChargeTo == -1 ? "" : this.Segments[_ChargeTo]; } set { this.Segments[_ChargeTo] = value; } }
        public string ReturnName { get { return _ReturnName == -1 ? "" : this.Segments[_ReturnName]; } set { this.Segments[_ReturnName] = value; } }
        public string ReturnAddress1 { get { return _ReturnAddress1 == -1 ? "" : this.Segments[_ReturnAddress1]; } set { this.Segments[_ReturnAddress1] = value; } }
        public string ReturnAddress2 { get { return _ReturnAddress2 == -1 ? "" : this.Segments[_ReturnAddress2]; } set { this.Segments[_ReturnAddress2] = value; } }
        public string ReturnCity { get { return _ReturnCity == -1 ? "" : this.Segments[_ReturnCity]; } set { this.Segments[_ReturnCity] = value; } }
        public string ReturnState { get { return _ReturnState == -1 ? "" : this.Segments[_ReturnState]; } set { this.Segments[_ReturnState] = value; } }
        public string ReturnZip { get { return _ReturnZip == -1 ? "" : this.Segments[_ReturnZip]; } set { this.Segments[_ReturnZip] = value; } }
        public decimal? Weight { get { try { return _Weight == -1 ? null : (decimal?)Convert.ToDecimal(this.Segments[_Weight]); } catch { return null; } } set { this.Segments[_Weight] = value.HasValue ? value.Value.ToString("F1") : ""; } }
        public bool Flat { get { try { return _Flat == -1 ? false : Convert.ToBoolean(this.Segments[_Flat]); } catch { return false; } } set { this.Segments[_Flat] = value ? "1" : "0"; } }
        #endregion

        public UploadRecord(Recipient recipient, Batches.BatchInfo batch, decimal weight, Enums.FileVersion fileVersion) : base(Dictionaries.FileVersionSegments[fileVersion])
        {
            this.Version = fileVersion;
            SetSegments(fileVersion);
            this.FileNo = recipient.FileNo;
            this.Name1 = recipient.Name;
            this.Name2 = recipient.Name2;
            this.Address1 = recipient.Address1;
            this.Address2 = recipient.Address2;
            this.City = recipient.City;
            this.State = recipient.State;
            this.Zip = recipient.Zip;
            this.BatchID = batch.ID;
            this.ReturnReceiptType = recipient.ReturnReceipt;
            this.RestrictedDelivery = recipient.RestrictedDelivery;
            this.FormType = recipient.EnvelopeSize.Size;
            this.Weight = weight;
            this.Flat = recipient.Flat;
        }
        public UploadRecord(string Record, Enums.FileVersion fileVersion) : base(Record)
        {
            this.Version = fileVersion;
            SetSegments(fileVersion);
        }

        private void SetSegments(Enums.FileVersion fileVersion)
        {
            this._FileNo = 0;
            this._Name1 = 1;
            this._Name2 = 2;
            this._Address1 = 3;
            this._Address2 = 4;
            this._City = 5;
            this._State = 6;
            this._Zip = 7;
            switch (fileVersion)
            {
                case Enums.FileVersion.FileVersion3:
                case Enums.FileVersion.FileVersion3m:
                    this._BatchID = 8;
                    this._ReturnReceiptType = 9;
                    this._RestrictedDelivery = 10;
                    this._FormType = 11;
                    this._Sender = 12;
                    this._ChargeAmount = 13;
                    this._ChargeTo = 14;
                    this._ReturnName = -1;
                    this._ReturnAddress1 = -1;
                    this._ReturnAddress2 = -1;
                    this._ReturnCity = -1;
                    this._ReturnState = -1;
                    this._ReturnZip = -1;
                    this._Weight = 15;
                    this._Flat = 16;
                    break;
                case Enums.FileVersion.FileVersion3r:
                case Enums.FileVersion.FileVersion3rm:
                    this._BatchID = 8;
                    this._ReturnReceiptType = 9;
                    this._RestrictedDelivery = 10;
                    this._FormType = 11;
                    this._Sender = 12;
                    this._ChargeAmount = 13;
                    this._ChargeTo = 14;
                    this._ReturnName = 15;
                    this._ReturnAddress1 = 16;
                    this._ReturnAddress2 = 17;
                    this._ReturnCity = 18;
                    this._ReturnState = 19;
                    this._ReturnZip = 20;
                    this._Weight = 21;
                    this._Flat = 22;
                    break;
                case Enums.FileVersion.FileVersion1:
                case Enums.FileVersion.FileVersion2:
                case Enums.FileVersion.FileVersion2m:
                case Enums.FileVersion.FileVersion2r:
                case Enums.FileVersion.FileVersion2rm:
                default:
                    throw new NotImplementedException("The Selected Format is Not Implemented");
            }
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "3":
                case "3m":
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}",
                        this.FileNo,
                        this.Name1,
                        this.Name2,
                        this.Address1,
                        this.Address2,
                        this.City,
                        this.State,
                        this.Zip,
                        this.BatchID,
                        Dictionaries.ReturnReceiptStrings[this.ReturnReceiptType],
                        this.RestrictedDelivery ? "1" : "0",
                        Dictionaries.FormTypeStrings[this.FormType],
                        this.Sender,
                        this.ChargeAmount,
                        this.ChargeTo,
                        this.Weight.Value.ToString("F0"),
                        this.Flat ? "1" : "0"
                        );
                case "3r":
                case "3rm":
                    return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}",
                        this.FileNo,
                        this.Name1,
                        this.Name2,
                        this.Address1,
                        this.Address2,
                        this.City,
                        this.State,
                        this.Zip,
                        this.BatchID,
                        Dictionaries.ReturnReceiptStrings[this.ReturnReceiptType],
                        this.RestrictedDelivery ? "1" : "0",
                        Dictionaries.FormTypeStrings[this.FormType],
                        this.Sender,
                        this.ChargeAmount,
                        this.ChargeTo,
                        this.ReturnName,
                        this.ReturnAddress1,
                        this.ReturnAddress2,
                        this.ReturnCity,
                        this.ReturnState,
                        this.ReturnZip,
                        this.Weight.Value.ToString("F0"),
                        this.Flat ? "1" : "0"
                        );
                case "1":
                case "2":
                case "2r":
                case "2m":
                case "2rm":
                default:
                    throw new NotImplementedException(string.Format("Selected Format Not Supported \"{0}\"", format));
            }
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public string ToString(Enums.FileVersion fileVersion)
        {
            return ToString(Dictionaries.FileVersionStrings[fileVersion]);
        }

        public override string ToString()
        {
            return ToString("3m");
        }

        #endregion
    }
}
