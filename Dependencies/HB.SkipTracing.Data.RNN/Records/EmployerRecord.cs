using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.RNN.Records
{
    public class EmployerRecord : DownloadRecord
    {
        #region Spreadsheet Values
        public object rnn_employer_name { get; set; }
        public object rnn_employer_address_1 { get; set; }
        public object rnn_employer_address_2 { get; set; }
        public object rnn_employer_city { get; set; }
        public object rnn_employer_state { get; set; }
        public object rnn_employer_zip { get; set; }
        public object rnn_employer_phone { get; set; }
        #endregion

        public EmployerRecord (Dictionary<string, object> values) : base(values)
        {
            try { this.rnn_employer_name = values["rnn_employer_name"]; } catch { }
            try { this.rnn_employer_address_1 = values["rnn_employer_address_1"]; } catch { }
            try { this.rnn_employer_address_2 = values["rnn_employer_address_2"]; } catch { }
            try { this.rnn_employer_city = values["rnn_employer_city"]; } catch { }
            try { this.rnn_employer_state = values["rnn_employer_state"]; } catch { }
            try { this.rnn_employer_zip = values["rnn_employer_zip"]; } catch { }
            try { this.rnn_employer_phone = values["rnn_employer_phone"]; } catch { }


            this.AssetType = Enums.AssetType.Employer;
            this.Name = $"{this.rnn_employer_name}";
            this.Address1 = $"{this.rnn_employer_address_1}";
            this.Address2 = $"{this.rnn_employer_address_2}";
            this.City = $"{this.rnn_employer_city}";
            this.State = $"{this.rnn_employer_state}";
            this.Zip = $"{this.rnn_employer_zip}";
            this.Phone = $"{this.rnn_employer_phone}";
        }
    }
}
