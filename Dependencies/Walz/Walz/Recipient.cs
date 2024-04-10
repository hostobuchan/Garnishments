using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data
{
    public class Recipient
    {
        public string FileNo { get; set; }
        public Letter LetterSize { get; set; }
        public Envelope EnvelopeSize { get; set; }
        public int Pages { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set;}
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Enums.ReturnReceiptType ReturnReceipt { get; set; }
        public bool RestrictedDelivery { get; set; }
        public bool Flat { get; set; }
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Name)
                    && !string.IsNullOrWhiteSpace(this.Address1)
                    && !string.IsNullOrWhiteSpace(this.City)
                    && !string.IsNullOrWhiteSpace(this.State)
                    && !string.IsNullOrWhiteSpace(this.Zip);
            }
        }

        public Recipient(string FileNo, Letter LetterSize, Envelope EnvelopeSize, int Pages, Enums.ReturnReceiptType ReturnReceipt = Enums.ReturnReceiptType.ReturnReceiptElectronic, bool RestrictedDelivery = false, bool Flat = false)
        {
            this.FileNo = FileNo;
            this.LetterSize = LetterSize;
            this.EnvelopeSize = EnvelopeSize;
            this.Pages = Pages;
            this.ReturnReceipt = ReturnReceipt;
            this.RestrictedDelivery = RestrictedDelivery;
            this.Flat = Flat;
        }
    }
}
