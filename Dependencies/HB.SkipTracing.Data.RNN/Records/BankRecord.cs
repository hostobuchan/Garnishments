using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.RNN.Records
{
    public class BankRecord : DownloadRecord
    {
        #region Spreadsheet Values
        public object rnn_bank_name { get; set; }
        public object rnn_bank_address { get; set; }
        public object rnn_bank_city { get; set; }
        public object rnn_bank_state { get; set; }
        public object rnn_bank_zip { get; set; }
        public object rnn_bank_phone { get; set; }
        public object rnn_rounting_number { get; set; }
        #endregion

        public BankRecord(Dictionary<string, object> values) : base(values)
        {
            try { this.rnn_bank_name = values["rnn_bank_name"]; } catch { }
            try { this.rnn_bank_address = values["rnn_bank_address"]; } catch { }
            try { this.rnn_bank_city = values["rnn_bank_city"]; } catch { }
            try { this.rnn_bank_state = values["rnn_bank_state"]; } catch { }
            try { this.rnn_bank_zip = values["rnn_bank_zip"]; } catch { }
            try { this.rnn_bank_phone = values["rnn_bank_phone"]; } catch { }
            try { this.rnn_rounting_number = values["rnn_routing_number"]; } catch { }


            this.AssetType = Enums.AssetType.Bank;
            this.Name = $"{this.rnn_bank_name}";
            this.Address1 = $"{this.rnn_bank_address}";
            this.Address2 = "";
            this.City = $"{this.rnn_bank_city}";
            this.State = $"{this.rnn_bank_state}";
            this.Zip = $"{this.rnn_bank_zip}";
            this.Phone = $"{this.rnn_bank_phone}";
        }
    }
}
