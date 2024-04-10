using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Walz.Data.UI
{
    public partial class BatchManagementForm : Form
    {
        private Files.Batches.BatchBuilder Builder { get; set; }

        public BatchManagementForm() : this(new Files.Batches.BatchBuilder(new Files.Batches.BatchManager(new DataHandler()))) { }
        public BatchManagementForm(Files.Batches.BatchBuilder builder)
        {
            this.Builder = builder;
            this.Builder.StatusUpdated += UpdateStatus;
            this.Builder.ActionRequested += Builder_ActionRequested;
            InitializeComponent();
            this.chkAutoDownload.DataBindings.Add("Checked", this.Builder, "AutoRecheck");
            UpdateDisplay();
        }

        private DialogResult Builder_ActionRequested(string arg1, string arg2, MessageBoxButtons arg3, MessageBoxIcon arg4)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Func<string, string, MessageBoxButtons, MessageBoxIcon, DialogResult>(Builder_ActionRequested), arg1, arg2, arg3, arg4);
                }
                return DialogResult.Abort;
            }

            return MessageBox.Show(arg1, arg2, arg3, arg4);
        }

        #region Form Interface
        private void btnViewItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecipientListForm rlf = new RecipientListForm(this.Builder.LetterRecipients);
            rlf.RecipientModified += RecipientsModified;
            rlf.ShowDialog(this);
        }
        
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (this.Builder.InformationUpdated)
            {
                if (MessageBox.Show("Are You Sure You Want to Re-Upload the File?", "Re-Upload?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            Action aUpload = new Action(this.Builder.Upload);
            aUpload.BeginInvoke((callback) =>
            {

                try
                {
                    (callback.AsyncState as Action).EndInvoke(callback);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }, aUpload);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Action aDownload = new Action(this.Builder.Download);
            aDownload.BeginInvoke((callback) =>
            {

                try
                {
                    (callback.AsyncState as Action).EndInvoke(callback);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }, aDownload);
        }

        private void BatchManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Builder.Status != Enums.BatchStatus.BatchReceived && this.Builder.Status != Enums.BatchStatus.Unknown && this.Builder.LetterRecipients.Count > 0)
            {
                if (MessageBox.Show("Process Is Not Completed!\r\n\r\nAre You Sure You Want to Exit?", "Incomplete!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Form Functions
        private void RecipientsModified()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action(RecipientsModified));
                return;
            }

            if (this.Builder.Status == Enums.BatchStatus.BatchUploaded)
            {
                this.Builder.UpdateInformation();
                SetButtonsByStatus(this.Builder.Status);
            }
        }

        private void UpdateDisplay()
        {
            this.txtBatchID.Text = this.Builder.Batch == null ? "Not Yet Attributed" : this.Builder.Batch.ID.ToString();
            this.txtItems.Text = this.Builder.LetterRecipients.Count().ToString();
        }

        private void UpdateStatus(Enums.BatchStatus status, string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Enums.BatchStatus, string>(UpdateStatus), new object[]{ status, message });
                return;
            }

            this.txtStatus.Text = message;
            SetButtonsByStatus(status);

            if (status == Enums.BatchStatus.BatchReceived)
            {
                // Open PDFs for Printing
                DisplayPdfForm dpf1 = new DisplayPdfForm(Enums.FileType.BarcodeFile, this.Builder.BarcodeImage);
                DisplayPdfForm dpf2 = new DisplayPdfForm(Enums.FileType.ReceiptFile, this.Builder.ReceiptImage);
                dpf1.Show(this);
                dpf2.Show(this);
                // Send Spreadsheet to Accounting
                try
                {
                    using (ExchangeInterface.Exchange xcApp = new ExchangeInterface.Exchange())
                    {
                        xcApp.UnAuthorizedException += XcApp_UnAuthorizedException;
                        this.Builder.CheckSpreadsheet.Seek(0, System.IO.SeekOrigin.Begin);
                        xcApp.SendEmail("Certified Mail", "", new string[] { "teresat@hosto.com" }, new string[] { }, new ExchangeInterface.Exchange.Attachment[] { new ExchangeInterface.Exchange.Attachment(string.Format("AR CertMail ({0}).csv", this.Builder.Batch.ID), this.Builder.CheckSpreadsheet) }, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Create Record Type 09 for CLS Output
                try
                {
                    List<RecordTypes2.YGC.RecordType09> Notes = new List<RecordTypes2.YGC.RecordType09>();
                    foreach (Files.Records.BarcodesRecord record in this.Builder.BarcodesRecords)
                    {
                        Notes.Add(new RecordTypes2.YGC.RecordType09()
                        {
                            MASCO_FILE = record.FileNo,
                            PDATE = DateTime.Today,
                            PCMT = "Walz Certified Mail Tracking",
                            NOTE02 = string.Format("Name: {0}", record.Name2),
                            NOTE03 = string.Format("BatchID: {0}", record.BatchID),
                            NOTE04 = string.Format("TrackingNumber: {0}", record.TrackingNumber)
                        });
                    }
                    RecordTypes.Output.Send_YGC_Imp(Notes);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            UpdateDisplay();
        }

        private System.Net.NetworkCredential XcApp_UnAuthorizedException(Exception arg)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    return this.Invoke(new Func<Exception, System.Net.NetworkCredential>(XcApp_UnAuthorizedException), arg) as System.Net.NetworkCredential;
                }
                return null;
            }

            ExchangeInterface.EnterCredentialsForm ecf = new ExchangeInterface.EnterCredentialsForm();
            if (ecf.ShowDialog(this) == DialogResult.OK)
            {
                return new System.Net.NetworkCredential(ecf.EnteredUser, ecf.EnteredPassword);
            }
            else
            {
                return null;
            }
        }

        private void SetButtonsByStatus(Enums.BatchStatus status)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<Enums.BatchStatus>(SetButtonsByStatus), status);
                }
                return;
            }

            switch (status)
            {
                case Enums.BatchStatus.Unknown:
                case Enums.BatchStatus.BatchCreated:
                    this.btnUpload.Enabled = true;
                    this.btnDownload.Enabled = true;
                    break;
                case Enums.BatchStatus.BatchUploaded:
                    this.btnUpload.Enabled = false;
                    this.btnDownload.Enabled = true;
                    break;
                case Enums.BatchStatus.BatchReceived:
                    this.btnUpload.Enabled = false;
                    this.btnDownload.Enabled = false;
                    break;
            }
        }
        #endregion

        #region Public Functions
        public void AddRecipient(Recipient recipient)
        {
            this.Builder.AddRecipient(recipient);
            UpdateDisplay();
        }

        public void RemoveRecipient(Recipient recipient)
        {
            this.Builder.RemoveRecipient(recipient);
            UpdateDisplay();
        }
        #endregion
    }
}
