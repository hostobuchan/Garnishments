using RecordTypes.PLX.Enums;
using RecordTypes.PLX2.Base;
using RecordTypes.PLX2.DataTypes;
using System;

namespace RecordTypes.PLX2
{
    public class FileHeader : RecordTypeBase
    {
        #region Properties
        /// <summary>
        /// Version number of the File Header
        /// </summary>
        public PLXNumber HeaderVersion { get; private set; }
        /// <summary>
        /// This is the Version Number of the Complete File
        /// </summary>
        public PLXNumber FileVersion { get; private set; }
        /// <summary>
        /// Business Process Type
        /// </summary>
        public PLXString BusProcType { get; private set; }
        /// <summary>
        /// Servicer ID that file is being received from
        /// </summary>
        public PLXNumber Source { get; private set; }
        /// <summary>
        /// Servicer ID that file is being sent to
        /// </summary>
        public PLXNumber Destination { get; private set; }
        /// <summary>
        /// Date and time that the file was created
        /// </summary>
        public PLXDateTime CreationDate { get; private set; }
        /// <summary>
        /// Servicer provided unique file ID
        /// </summary>
        public PLXString FileID { get; private set; }
        #endregion

        public FileHeader(FileHeader OriginalHeader)
            : base(Types.FileHeader)
        {
            this.HeaderVersion = OriginalHeader.HeaderVersion;
            this.FileVersion = OriginalHeader.FileVersion;
            this.BusProcType = OriginalHeader.BusProcType;
            this.Source = OriginalHeader.Source;
            this.Destination = OriginalHeader.Destination;
            this.CreationDate = new PLXDateTime("");
            this.CreationDate.Value = DateTime.Now;
            this.FileID = OriginalHeader.FileID;
        }
        public FileHeader(string Record)
            : base(Record)
        {
            this.HeaderVersion = new PLXNumber(this.LineItems[1], 2, true);
            this.FileVersion = new PLXNumber(this.LineItems[2], 2, true);
            this.BusProcType = new PLXString(this.LineItems[3]);
            this.Source = new PLXNumber(this.LineItems[4], 6, true);
            this.Destination = new PLXNumber(this.LineItems[5], 6, true);
            this.CreationDate = new PLXDateTime(this.LineItems[6]);
            if (this.LineItems.GetUpperBound(0) > 6)
                this.FileID = new PLXString(this.LineItems[7]);
            else
                this.FileID = new PLXString("");
        }
    }

    public class RecordHeader : RecordTypeBase
    {
        #region Properties
        /// <summary>
        /// Version number of the Record Header
        /// </summary>
        public PLXNumber HeaderVersion { get; private set; }
        /// <summary>
        /// Which record type is this the header for
        /// </summary>
        public PLXEnum<Types, TypeValues> HeaderRecordType { get; private set; }
        /// <summary>
        /// Version of the Record Type
        /// </summary>
        public PLXNumber RecordVersion { get; private set; }
        /// <summary>
        /// Number of records included for the Record Type
        /// </summary>
        public PLXNumber NumOfRecords { get; set; }
        #endregion

        public RecordHeader(string Record)
            : base(Record)
        {
            this.HeaderVersion = new PLXNumber(this.LineItems[1], 2, true);
            this.HeaderRecordType = new PLXEnum<Types, TypeValues>(this.LineItems[2]);
            this.RecordVersion = new PLXNumber(this.LineItems[3], 2, true);
            this.NumOfRecords = new PLXNumber(this.LineItems[4], 7, true);
        }
    }
}
