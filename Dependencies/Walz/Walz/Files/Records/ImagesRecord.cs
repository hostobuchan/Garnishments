using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Records
{
    public class ImagesRecord : BaseRecord
    {
        #region Properties
        public string TrackingNumber { get { return this.Segments[0]; } set { this.Segments[0] = value; } }
        public string FileNo { get { return this.Segments[1]; } set { this.Segments[1] = value; } }
        public int? BatchID { get { try { return Convert.ToInt32(this.Segments[2]); } catch { return null; } } set { this.Segments[2] = value.HasValue ? value.Value.ToString() : ""; } }
        public string Recipient { get { return this.Segments[3]; } set { this.Segments[3] = value; } }
        public Enums.LastStatus LastStatus { get { try { return Dictionaries.LastStatusStrings.FirstOrDefault(el => el.Value == this.Segments[4]).Key; } catch { return Enums.LastStatus.Unknown; } } set { try { this.Segments[4] = Dictionaries.LastStatusStrings[value]; } catch { this.Segments[4] = ""; } } }
        public DateTime? LastStatusDate { get { try { return DateTime.ParseExact(this.Segments[5], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { this.Segments[9] = value.HasValue ? value.Value.ToString("M/d/yyyy") : ""; } }
        public string LastKeyWord { get { return this.Segments[6]; } set { this.Segments[6] = value; } }
        public string DeliveredText{ get { return this.Segments[7]; } set { this.Segments[7] = value; } }
        #endregion

        public ImagesRecord(string Record) : base(Record) { }
    }
}
