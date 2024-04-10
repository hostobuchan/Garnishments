using System;

namespace RecordTypes.Walz
{
    public class ExtractRecord
    {
        #region Properties
        public string TrackingNumber { get; set; }
        public string FileNo { get; set; }
        public int BatchID { get; set; }
        public string Recipient { get; set; }
        public Enums.LastStatus LastStatus { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public string LastKeyWord { get; set; }
        public string DeliveredText { get; set; }
        #endregion

        public ExtractRecord(string Record)
        {
            string[] Elements = Record.Split('\t');
            this.TrackingNumber = Elements[0];
            this.FileNo = Elements[1];
            this.BatchID = Convert.ToInt32(Elements[2]);
            this.Recipient = Elements[3];
            try
            {
                this.LastStatus = (Enums.LastStatus)Enum.Parse(typeof(Enums.LastStatus), Elements[4].Replace(' ', '_'), true);
            }
            catch
            {
                this.LastStatus = Enums.LastStatus.Unknown;
            }
            this.LastStatusDate = string.IsNullOrEmpty(Elements[5]) ? null : (DateTime?)DateTime.ParseExact(Elements[5], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            this.LastKeyWord = Elements[6];
            this.DeliveredText = Elements[7];
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                this.TrackingNumber,
                this.FileNo,
                this.BatchID,
                this.Recipient,
                this.LastStatus.ToString().Replace('_', ' '),
                this.LastStatusDate.HasValue ? this.LastStatusDate.Value.ToString("M/d/yyyy") : "",
                this.LastKeyWord,
                this.DeliveredText
                );
        }
    }

    namespace Enums
    {
        public enum LastStatus
        {
            Unknown = 0,
            Delivered = 1,
            Returned_to_Sender = 2,
            Mailed = 3
        }
    }
}
