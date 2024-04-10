using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.RNN.Records
{
    public class DownloadRecord : Data.Records.DownloadRecord
    {
        public override string Vendor { get { return "RNN"; } }
        public override Data.Enums.ImportSettings ImportSettings { get { return Data.Enums.ImportSettings.ImportAndGarn; } }

        #region SpreadSheet Values
        public object social_security_number { get; set; }
        public object first_name { get; set; }
        public object last_name { get; set; }
        public object street_address_1 { get; set; }
        public object street_address_2 { get; set; }
        public object city { get; set; }
        public object state { get; set; }
        public object zip_code { get; set; }
        public object date_of_birth { get; set; }
        public object phone_1 { get; set; }
        public object phone_2 { get; set; }
        public object phone_3 { get; set; }
        public object client_field_1 { get; set; }
        public object client_field_2 { get; set; }
        public object client_field_3 { get; set; }
        public object client_field_4 { get; set; }
        public object client_field_5 { get; set; }
        public object account_open_date { get; set; }
        public object rnn_id { get; set; }
        public object source_code { get; set; }
        public object tier_verified { get; set; }
        #endregion

        public DownloadRecord(Dictionary<string, object> values)
        {
            try { this.social_security_number = values.ContainsKey("social_security_number") ? values["social_security_number"] : null; } catch { }
            try { this.first_name = values.ContainsKey("first_name") ? values["first_name"] : null; } catch { }
            try { this.last_name = values.ContainsKey("last_name") ? values["last_name"] : null; } catch { }
            try { this.street_address_1 = values.ContainsKey("street_address_1") ? values["street_address_1"] : null; } catch { }
            try { this.street_address_2 = values.ContainsKey("street_address_2") ? values["street_address_2"] : null; } catch { }
            try { this.city = values.ContainsKey("city") ? values["city"] : null; } catch { }
            try { this.state = values.ContainsKey("state") ? values["state"] : null; } catch { }
            try { this.zip_code = values.ContainsKey("zip_code") ? values["zip_code"] : null; } catch { }
            try { this.date_of_birth = values.ContainsKey("date_of_birth") ? values["date_of_birth"] : null; } catch { }
            try { this.phone_1 = values.ContainsKey("phone_1") ? values["phone_1"] : null; } catch { }
            try { this.phone_2 = values.ContainsKey("phone_2") ? values["phone_2"] : null; } catch { }
            try { this.phone_3 = values.ContainsKey("phone_3") ? values["phone_3"] : null; } catch { }
            try { this.client_field_1 = values.ContainsKey("client_field_1") ? values["client_field_1"] : null; } catch { }
            try { this.client_field_2 = values.ContainsKey("client_field_2") ? values["client_field_2"] : null; } catch { }
            try { this.client_field_3 = values.ContainsKey("client_field_3") ? values["client_field_3"] : null; } catch { }
            try { this.client_field_4 = values.ContainsKey("client_field_4") ? values["client_field_4"] : null; } catch { }
            try { this.client_field_5 = values.ContainsKey("client_field_5") ? values["client_field_5"] : null; } catch { }
            try { this.account_open_date = values.ContainsKey("account_open_date") ? values["account_open_date"] : null; } catch { }
            try { this.rnn_id = values.ContainsKey("rnn_id") ? values["rnn_id"] : null; } catch { }
            try { this.source_code = values.ContainsKey("source_code") ? values["source_code"] : null; } catch { }
            try { this.tier_verified = values.ContainsKey("tier verified") ? values["tier verified"] : null; } catch { }


            this.UploadAccountNo = this.client_field_1?.ToString() ?? "";
        }
    }
}
