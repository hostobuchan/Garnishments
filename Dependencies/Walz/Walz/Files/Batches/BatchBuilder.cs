using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Batches
{
    public class BatchBuilder
    {
        public event Action<Enums.BatchStatus, string> StatusUpdated;
        public event Func<string, string, System.Windows.Forms.MessageBoxButtons, System.Windows.Forms.MessageBoxIcon, System.Windows.Forms.DialogResult> ActionRequested;
        protected void OnStatusUpdated(string StatusUpdate) { if (this.StatusUpdated != null) this.StatusUpdated(this.Status, StatusUpdate); }

        private BatchManager Manager { get; set; }
        private List<Recipient> _LetterRecipients { get; set; }
        private List<Records.BarcodesRecord> _BarcodesRecords { get; set; }

        public Enums.BatchStatus Status { get; private set; }
        public bool Finalized { get { return this.Status != Enums.BatchStatus.Unknown; } }
        public List<Recipient> LetterRecipients { get { return this._LetterRecipients.ToList(); } }
        public List<Records.BarcodesRecord> BarcodesRecords { get { if (this._BarcodesRecords == null) return null; else return this._BarcodesRecords.ToList(); } }
        public System.IO.MemoryStream BarcodeImage { get; private set; }
        public System.IO.MemoryStream ReceiptImage { get; private set; }
        public System.IO.MemoryStream CheckSpreadsheet { get; private set; }
        public BatchInfo Batch { get; private set; }
        public bool AutoRecheck { get; set; }
        public bool InformationUpdated { get; private set; }

        public BatchBuilder(BatchManager Manager)
        {
            this.Manager = Manager;
            this.Status = Enums.BatchStatus.Unknown;
            this._LetterRecipients = new List<Recipient>();
            this.CheckSpreadsheet = new System.IO.MemoryStream();
        }
        public BatchBuilder(BatchManager Manager, int BatchID, bool NoHistory = false) : this(Manager)
        {
            this.Batch = NoHistory ? new BatchInfo(BatchID, DateTime.Today, 0, 0) : Manager.GetBatch(BatchID);
            this.Status = Enums.BatchStatus.BatchUploaded;
        }

        public void AddRecipient(Recipient recipient)
        {
            lock (this)
            {
                if (Finalized) throw new InvalidOperationException("The Batch Has Already Been Built\n\nAdding Additional Records is Prohibited");
                this._LetterRecipients.Add(recipient);
            }
        }
        public void RemoveRecipient(Recipient recipient)
        {
            lock (this)
            {
                if (Finalized) throw new InvalidOperationException("The Batch Has Already Been Built\n\nRemoving Records is Prohibited");
                this._LetterRecipients.Remove(recipient);
            }
        }

        private List<Records.UploadRecord> BuildUploadRecords(IEnumerable<Recipient> Recipients, Enums.FileVersion fileVersion)
        {
            List<Records.UploadRecord> Records = new List<Records.UploadRecord>();
            foreach (Recipient rec in Recipients)
            {
                if (rec.IsValid)
                {
                    Records.Add(new Records.UploadRecord(rec, this.Batch, this.Manager.EstimateRecipientWeight(rec), fileVersion));
                }
                else
                {
                    throw new InvalidOperationException($"Recipient \"{rec.Name}\" Is Invalid.  Please Check the Address.");
                }
            }
            return Records;
        }

        public void UpdateInformation()
        {
            lock (this)
            {
                if (this.Status < Enums.BatchStatus.BatchReceived)
                {
                    this.InformationUpdated = true;
                    this.Status = Enums.BatchStatus.BatchCreated;
                }
            }
        }

        public void Upload()
        {
            lock (this)
            {
                if (this.Status < Enums.BatchStatus.BatchCreated)
                {
                    this.Status = Enums.BatchStatus.BatchCreated;
                    OnStatusUpdated("Creating Batch...");

                    #region Store Batch Information in SQL
                    try
                    {
                        this.Batch = this.Manager.CreateBatch(this._LetterRecipients);
                    }
                    catch (Exception ex)
                    {
                        this.Status = Enums.BatchStatus.Unknown;
                        OnStatusUpdated("Batch Creation Failed!\n\n" + ex.Message);
                        return;
                    }
                    #endregion
                }

                if (this.Status < Enums.BatchStatus.BatchUploaded)
                {
                    this.InformationUpdated = false;
                    System.IO.MemoryStream FileStream = null;
                    string DirectoryName = System.IO.Path.Combine(Settings.Locations.SaveLocation, this.Batch.ID.ToString());
                    string UploadFileName = FileManager.GetUploadFileName("1172", this.Batch, Enums.FileVersion.FileVersion3m);
                    string SaveFileName = System.IO.Path.Combine(DirectoryName, string.Format("Upload File ({0}).txt", UploadFileName));
                    OnStatusUpdated("Creating File...");
                    try
                    {
                        // Get File Stream for Upload File
                        FileStream = FileManager.CreateUploadFile(BuildUploadRecords(this._LetterRecipients, Enums.FileVersion.FileVersion3m), this.Batch);

                        #region Generate Directory For Batch
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(DirectoryName);
                        if (!dir.Exists)
                            dir.Create();
                        #endregion

                        #region Generate Physical File
                        using (System.IO.FileStream file = new System.IO.FileStream(SaveFileName, System.IO.FileMode.Create))
                        {
                            if (FileStream != null)
                            {
                                FileStream.Seek(0, System.IO.SeekOrigin.Begin);
                                FileStream.CopyTo(file);
                                file.Flush();
                            }
                            else
                            {
                                throw new Exception("Unknown File Creation Error");
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        OnStatusUpdated("File Creation Failed!\n\n" + ex.Message);
                        return;
                    }

                    OnStatusUpdated("Uploading File...");
                    try
                    {
                        #region Connect & Upload File
                        RemoteFile.Connections.SiteLogin login = RemoteFile.Connections.SiteConnectionManager.FindLoginByNameAsync("Walz").Result;
                        using (var sftp = login.GetClient())
                        {
                            sftp.Connect();
                            sftp.ChangeDirectory(Settings.Locations.SFTP_TO_Location);

                            using (System.IO.FileStream fs = new System.IO.FileStream(SaveFileName, System.IO.FileMode.Open))
                            {
                                sftp.PutFile(fs, UploadFileName);
                            }
                            this.Status = Enums.BatchStatus.BatchUploaded;
                            OnStatusUpdated("File Uploaded.");
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        OnStatusUpdated("File Upload Failed!" + ex.Message);
                    }
                }
                else
                {
                    if (this.ActionRequested?.Invoke("Batch Appears to Have Been Uploaded\r\n\r\nDo You Want to Re-Upload?", "Uploaded", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (this.Batch?.ID != null)
                        {
                            this.Status = Enums.BatchStatus.BatchCreated;
                            Upload();
                        }
                        else
                        {
                            this.Status = Enums.BatchStatus.Unknown;
                            Upload();
                        }
                    }
                }
            }
        }

        public void Download()
        {
            lock (this)
            {
                if (this.Status < Enums.BatchStatus.BatchUploaded)
                {
                    OnStatusUpdated("Batch Not Uploaded.");
                    return;
                }
                else if (this.Status == Enums.BatchStatus.BatchReceived)
                {
                    OnStatusUpdated("Batch Already Received.");
                    return;
                }

                try
                {
                    OnStatusUpdated("Connecting...");
                    #region Connect and Get Output File Listing
                    RemoteFile.Connections.SiteLogin login = RemoteFile.Connections.SiteConnectionManager.FindLoginByNameAsync("Walz").Result;
                    using (var sftp = login.GetClient())
                    {

                        sftp.Connect();
                        List<BaseFile> files = FileManager.GetFiles(sftp.FileListing(Settings.Locations.SFTP_FROM_Location).Select(el => el.FullPath));
                        #endregion

                        // Check That Output Files Have Been Created
                        if (files.Count(el => el.BatchID.HasValue && el.BatchID.Value == this.Batch.ID) > 1)
                        {
                            #region Process Batch Output Files
                            string DirectoryName = System.IO.Path.Combine(Settings.Locations.SaveLocation, this.Batch.ID.ToString());
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(DirectoryName);
                            if (!dir.Exists)
                                dir.Create();

                            foreach (BaseFile file in files.Where(el => el.BatchID.HasValue && el.BatchID.Value == this.Batch.ID))
                            {
                                using (System.IO.MemoryStream fileStream = new System.IO.MemoryStream())
                                {
                                    sftp.GetFile(file.FileName, fileStream);
                                    file.OpenArchiveOnStream(fileStream);
                                    if (file.Type == Enums.FileType.BarcodeFile)
                                    {
                                        #region Process Barcode File
                                        BarcodesFile bFile = file as BarcodesFile;
                                        foreach (Zip.ZipArchive.ZipFileInfo Image in bFile.ImageFiles)
                                        {
                                            #region Copy Barcode Streams
                                            using (System.IO.Stream img = Image.GetStream())
                                            {
                                                // Copy Barcodes PDF to Image Stream
                                                this.BarcodeImage = new System.IO.MemoryStream();
                                                img.CopyTo(this.BarcodeImage);
                                                img.Seek(0, System.IO.SeekOrigin.Begin);
                                                // Copy Barcodes PDF to Output Folder
                                                using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(dir.FullName, string.Format("Barcodes ({0}).pdf", Image.Name)), System.IO.FileMode.Create))
                                                {
                                                    img.CopyTo(fs);
                                                    fs.Flush();
                                                }
                                            }
                                            #endregion
                                        }
                                        // Copy Barcodes Records to Batch Builder
                                        this._BarcodesRecords = bFile.BarcodeRecords;
                                        // Copy Barcodes Record to Output Folder
                                        using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(dir.FullName, string.Format("Barcodes ({0}).txt", bFile.RecordFile.Name)), System.IO.FileMode.Create))
                                        {
                                            bFile.RecordFile.GetStream().CopyTo(fs);
                                            fs.Flush();
                                        }
                                        #endregion
                                    }
                                    if (file.Type == Enums.FileType.ReceiptFile)
                                    {
                                        #region Process Receipt File
                                        ReceiptFile rFile = file as ReceiptFile;
                                        foreach (Zip.ZipArchive.ZipFileInfo Image in rFile.ImageFiles)
                                        {
                                            #region Copy Receipt Streams
                                            using (System.IO.Stream img = Image.GetStream())
                                            {
                                                // Copy Receipt PDF to Image Stream
                                                this.ReceiptImage = new System.IO.MemoryStream();
                                                img.CopyTo(this.ReceiptImage);
                                                img.Seek(0, System.IO.SeekOrigin.Begin);
                                                // Copy Reciept PDF to Output Folder
                                                using (System.IO.FileStream fs = new System.IO.FileStream(System.IO.Path.Combine(dir.FullName, string.Format("Receipt ({0}).pdf", Image.Name)), System.IO.FileMode.Create))
                                                {
                                                    img.CopyTo(fs);
                                                    fs.Flush();
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Create Check Spreadsheet
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                            {
                                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ms))
                                {
                                    sw.WriteLine("FILENO,CHECK_AMT,COMMENT,RET_CODE");
                                    foreach (Records.BarcodesRecord record in this.BarcodesRecords)
                                    {
                                        sw.WriteLine(string.Format("{0},{1},{2},{3}",
                                            record.FileNo,
                                            this.Manager.EstimateRecipientCost(record),
                                            "",
                                            "56"));
                                    }
                                    sw.Flush();

                                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                                    ms.CopyTo(this.CheckSpreadsheet);
                                }
                            }
                            #endregion

                            this.Status = Enums.BatchStatus.BatchReceived;
                            OnStatusUpdated("Download Completed.");
                        }
                        else
                        {
                            if (this.AutoRecheck)
                            {
                                OnStatusUpdated("No File.  Checking Again in 30 sec.");
                                Action wait30 = new Action(() => { System.Threading.Thread.Sleep(30000); if (this.AutoRecheck) Download(); else OnStatusUpdated("AutoCheck Disabled."); });
                                wait30.BeginInvoke((callback) => { }, wait30);
                            }
                            else
                            {
                                OnStatusUpdated("No File.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnStatusUpdated("Problem Receiving File!\n\n" + ex.Message);
                    return;
                }
            }
        }
    }
}
