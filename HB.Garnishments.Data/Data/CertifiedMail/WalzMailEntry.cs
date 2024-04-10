using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.CertifiedMail
{
    public class WalzMailEntry : RecordTypes.Delimited.Base.TabDelimitedBaseRecord
    {
        #region Public Variables
        public string FileNo { get { return this.LineItems[0]; } }
        public string Name1 { get { return this.LineItems[1]; } }
        public string Name2 { get { return this.LineItems[2]; } }
        public string Address1 { get { return this.LineItems[3]; } }
        public string Address2 { get { return this.LineItems[4]; } }
        public string City { get { return this.LineItems[5]; } }
        public string State { get { return this.LineItems[6]; } }
        public string Zip { get { return this.LineItems[7]; } }
        public string BatchID { get { return this.LineItems[8]; } }
        public string ReturnReceiptType { get { return this.LineItems[9]; } }
        public string RestrictedDelivery { get { return this.LineItems[10]; } }
        public string FormType { get { return this.LineItems[11]; } }
        public string Sender { get { return this.LineItems[12]; } }
        public string ChargeAmount { get { return this.LineItems[13]; } }
        public string ChargeTo { get { return this.LineItems[14]; } }
        public string Weight { get { return this.LineItems[15]; } }
        public string Flat { get { return this.LineItems[16]; } }
        public bool ReceivesSSILetter { get; private set; }
        public bool NeedsCertifiedMail { get; private set; }
        public bool NeedsLabel { get; private set; }
        #endregion

        public WalzMailEntry(int batchId, string fileNo, Interfaces.IAddressable recipient, bool ssiLetter, bool label, bool certMail) : base(17)
        {
            this.LineItems[0].Value = fileNo;
            this.LineItems[1].Value = recipient.Name?.ToUpper();
            this.LineItems[2].Value = recipient.Attention?.ToUpper();
            this.LineItems[3].Value = recipient.Address1?.ToUpper();
            this.LineItems[4].Value = recipient.Address2?.ToUpper();
            this.LineItems[5].Value = recipient.City?.ToUpper();
            this.LineItems[6].Value = recipient.State?.ToUpper();
            this.LineItems[7].Value = recipient.Zip;
            this.LineItems[8].Value = batchId.ToString();
            this.LineItems[9].Value = "RRE";
            this.LineItems[10].Value = "0";
            this.LineItems[11].Value = "6x9SSMeter";
            this.LineItems[12].Value = "";
            this.LineItems[13].Value = "";
            this.LineItems[14].Value = "";
            this.LineItems[15].Value = "2";
            this.LineItems[16].Value = "";
            this.ReceivesSSILetter = ssiLetter;
            this.NeedsLabel = label;
            this.NeedsCertifiedMail = certMail;
        }

        public static string HeaderLine
        {
            get
            {
                return "ReferenceNumber	Name1	Name2	Address1	Address2	City	State	Zip	BatchID	ReturnReceiptType	RestrictedDelivery	FormType	Sender	ChargeAmount	ChargeTo	Weight	Flat";
            }
        }
    }
}
