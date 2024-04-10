using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.RNN.Records
{
    public class MilitaryRecord : DownloadRecord
    {
        #region Spreadsheet Values
        public object DATEOFINTEREST { get; set; }
        public object ACTIVEDUTYSTATUS { get; set; }
        public object ACTIVEDUTYSTARTDATE { get; set; }
        public object ACTIVEDUTYENDDATE { get; set; }
        public object ORDERNOTIFICATIONSTARTDATE { get; set; }
        public object ORDERNOTIFICATIONENDDATE { get; set; }
        public object SERVICEAGENCY { get; set; }
        public object REPORTID { get; set; }
        public object DOCUMENTNAME { get; set; }
        #endregion

        #region Public Values
        public DateTime? DateofInterest { get; private set; }
        public bool ActiveDutyStatus { get; private set; }
        public DateTime? ActiveDutyStartDate { get; private set; }
        public DateTime? ActiveDutyEndDate { get; private set; }
        public DateTime? OrderNotificationStartDate { get; private set; }
        public DateTime? OrderNotificationEndDate { get; private set; }
        public string ServiceAgency { get; private set; }
        public string ReportID { get; private set; }
        public string DocumentName { get; private set; }
        #endregion

        public MilitaryRecord(Dictionary<string, object> values) : base(values)
        {
            try { this.DATEOFINTEREST = values["DATEOFINTEREST"]; } catch { }
            try { this.ACTIVEDUTYSTATUS = values["ACTIVEDUTYSTATUS"]; } catch { }
            try { this.ACTIVEDUTYSTARTDATE = values["ACTIVEDUTYSTARTDATE"]; } catch { }
            try { this.ACTIVEDUTYENDDATE = values["ACTIVEDUTYENDDATE"]; } catch { }
            try { this.ORDERNOTIFICATIONSTARTDATE = values["ORDERNOTIFICATIONSTARTDATE"]; } catch { }
            try { this.ORDERNOTIFICATIONENDDATE = values["ORDERNOTIFICATIONENDDATE"]; } catch { }
            try { this.SERVICEAGENCY = values["SERVICEAGENCY"]; } catch { }
            try { this.REPORTID = values["REPORTID"]; } catch { }
            try { this.DOCUMENTNAME = values["DOCUMENTNAME"]; } catch { }

            try { this.DateofInterest = this.DATEOFINTEREST == null ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(this.DATEOFINTEREST), "yyyyMMdd", CultureInfo.CurrentCulture); } catch { }
            try { this.ActiveDutyStatus = Convert.ToString(this.ACTIVEDUTYSTATUS).Contains("On Active Duty"); } catch { }
            try { this.ActiveDutyStartDate = this.ACTIVEDUTYSTARTDATE == null ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(this.ACTIVEDUTYSTARTDATE), "yyyyMMdd", CultureInfo.CurrentCulture); } catch { }
            try { this.ActiveDutyEndDate = this.ACTIVEDUTYENDDATE == null ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(this.ACTIVEDUTYENDDATE), "yyyyMMdd", CultureInfo.CurrentCulture); } catch { }
            try { this.OrderNotificationStartDate = this.ORDERNOTIFICATIONSTARTDATE == null ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(this.ORDERNOTIFICATIONSTARTDATE), "yyyyMMdd", CultureInfo.CurrentCulture); } catch { }
            try { this.OrderNotificationEndDate = this.ORDERNOTIFICATIONENDDATE == null ? (DateTime?)null : DateTime.ParseExact(Convert.ToString(this.ORDERNOTIFICATIONENDDATE), "yyyyMMdd", CultureInfo.CurrentCulture); } catch { }
            try { this.ServiceAgency = Convert.ToString(this.SERVICEAGENCY); } catch { }
            try { this.ReportID = Convert.ToString(this.REPORTID); } catch { }
            try { this.DocumentName = Convert.ToString(this.DOCUMENTNAME); } catch { }

            this.AssetType = Enums.AssetType.SCRA;
        }
    }
}
