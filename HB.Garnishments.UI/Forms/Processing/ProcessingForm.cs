using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Processing
{
    public partial class ProcessingForm : Form
    {
        bool CommunicationFailed = false;
        Data.Processing.IProcessor Processor { get; set; }

        public ProcessingForm(Data.Processing.IProcessor processor)
        {
            SynchronizationContext.SetSynchronizationContext(processor.Sync);
            this.Processor = processor;
            this.Processor.GarnishmentProblem += Processor_GarnishmentProblem;
            this.Processor.CommunicationProblem += Processor_CommunicationProblem;
            InitializeComponent();
        }

        #region Processor Handling
        private void Processor_CommunicationProblem(object sender, Exception e)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated && !IsDisposed)
                {
                    this.Invoke(new EventHandler<Exception>(Processor_CommunicationProblem), sender, e);
                }
                return;
            }

            try
            {
                if (!this.CommunicationFailed)
                {
                    this.CommunicationFailed = true;
                    this.lblCommunicationStatus.Image = HB.Garnishments.UI.Properties.Resources.alert;
                    this.lblStatus.Text += " - No Communication!";
                }
                this.lblCommunicationStatus.AutoToolTip = true;
                this.lblCommunicationStatus.ToolTipText = e.Message;
            }
            catch { }
        }

        private void Processor_GarnishmentProblem(Exception ex, Data.Requests.Request request)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Data.Processing.Processor.GarnProblem(Processor_GarnishmentProblem), ex, request);
                }
                return;
            }

            try
            {
                MessageBox.Show($"A Work Item Has Changed Status\r\n\r\n{ex.Message}");
            }
            catch { }
        }
        #endregion

        private void ProcessingForm_Load(object sender, EventArgs e)
        {
            this.lstPending.DataSource = this.Processor.ItemsPending;
            this.lstGood.DataSource = this.Processor.PositiveItemsPending;
            this.lstBad.DataSource = this.Processor.NegativeItemsPending;
            UpdateStatus("Loading...", false);
        }

        private async void ProcessingForm_Shown(object sender, EventArgs e)
        {
            await this.Processor.LoadAsync().ContinueWith(async (task) =>
            {
                if (task.IsFaulted)
                {
                    UpdateStatus(this.CommunicationFailed ? "Failure!!! - Unable to Load Data! - No Communication!" : "Failure!!! - Unable to Load Data!", true);
                }
                else
                {
                    await this.Processor.EvaluateExclusionsAsync().ContinueWith((task2) =>
                    {
                        if (task2.IsFaulted)
                        {
                            UpdateStatus(this.CommunicationFailed ? "Failure!!! - Unable to Evaluate Accounts! - No Communication!" : "Failure!!! - Unable to Evaluate Accounts!", true);
                        }
                        else
                        {
                            UpdateStatus(this.CommunicationFailed ? "Ready! - No Communication!" : "Ready!", true);
                        }
                    });
                }
            });
        }

        private void lstPending_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox)?.SelectedItem is Data.Requests.AssetRequest)
            {
                var request = (sender as ListBox).SelectedItem as Data.Requests.AssetRequest;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(this.lstPending,
                    $"{request.Asset.Account.FileNo}\r\nDebtor: {request.Asset.Debtor}\r\n\r\n{request.Asset.Name}\r\n{(!string.IsNullOrWhiteSpace(request.Asset.Contact) ? $"Contact: {request.Asset.Contact}\r\n" : "")}{request.Asset.Address1}\r\n{(!string.IsNullOrWhiteSpace(request.Asset.Address2) ? $"{request.Asset.Address2}\r\n" : "")}{request.Asset.City}, {request.Asset.State} {request.Asset.Zip}");
            }
        }

        #region Menu

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Will Be Implemented In A Future Release", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportWorklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm(() =>
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FILENO", typeof(string));
                dt.Columns.Add("TYPE", typeof(string));
                dt.Columns.Add("STATUS", typeof(string));
                dt.Columns.Add("DAYS IN STATUS", typeof(int));
                dt.Columns.Add("COUNTY", typeof(string));
                foreach (var request in this.Processor.ItemsPending)
                {
                    DataRow dr = dt.NewRow();
                    dr["FILENO"] = request.Asset.Account.FileNo;
                    dr["TYPE"] = request.Asset.Type;
                    dr["STATUS"] = Data.Dictionaries.StatusAwaitingDescription[request.CurrentStatus.Type];
                    dr["DAYS IN STATUS"] = (DateTime.Today - request.CurrentStatus.Date.Date).Days;
                    dr["COUNTY"] = UI.Settings.Properties.Venues.FirstOrDefault(ven => ven.VenueNo == (request as Data.Requests.AssetRequestAndAccount)?.Account.Venue)?.County ?? "";
                    dt.Rows.Add(dr);
                }
                ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();
                xlApp.xlBook.AddWorksheetFromTable(dt);
                xlApp.ShowWorkBook();
            });
            wf.ShowDialog(this);
        }

        private void rejectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutomaticRejectsForm arf = new AutomaticRejectsForm(this.Processor);
            arf.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new Forms.AboutBox();
            ab.ShowDialog(this);
        }

        #endregion

        #region Buttons

        #region Add / Remove

        private async void btnAdd_Good_Click(object sender, EventArgs e)
        {
            if (this.lstPending.SelectedItem is Data.Requests.AssetRequest)
            {
                try
                {
                    if (this.lstPending.SelectedItem is Data.Requests.AssetRequestAndAccount)
                    {
                        await this.Processor.AddPositiveResultAsync(this.lstPending.SelectedItem as Data.Requests.AssetRequestAndAccount, SelectResult);
                        this.lstPending.SelectedIndex = -1;
                    }
                }
                catch (InvalidOperationException iex)
                {
                    MessageBox.Show(iex.Message, "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Went Wrong\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.txtSearch.Focus();
        }

        private async void btnRemove_Good_Click(object sender, EventArgs e)
        {
            if (this.lstGood.SelectedItem is Data.Requests.Results.AssetRequestProcessingResult)
            {
                try
                {
                    await this.Processor.RemovePositiveResultAsync(this.lstGood.SelectedItem as Data.Requests.Results.AssetRequestProcessingResult);
                }
                catch (InvalidOperationException iex)
                {
                    MessageBox.Show(iex.Message, "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Went Wrong\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.txtSearch.Focus();
        }

        private async void btnAdd_Bad_Click(object sender, EventArgs e)
        {
            if (this.lstPending.SelectedItem is Data.Requests.AssetRequest)
            {
                try
                {
                    if (this.lstPending.SelectedItem is Data.Requests.AssetRequestAndAccount)
                    {
                        await this.Processor.AddNegativeResultAsync(this.lstPending.SelectedItem as Data.Requests.AssetRequestAndAccount, SelectResult);
                        this.lstPending.SelectedIndex = -1;
                    }
                }
                catch (InvalidOperationException iex)
                {
                    MessageBox.Show(iex.Message, "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Went Wrong\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.txtSearch.Focus();
        }

        private async void btnRemove_Bad_Click(object sender, EventArgs e)
        {
            if (this.lstBad.SelectedItem is Data.Requests.Results.AssetRequestProcessingResult)
            {
                try
                {
                    await this.Processor.RemoveNegativeResultAsync(this.lstBad.SelectedItem as Data.Requests.Results.AssetRequestProcessingResult);
                }
                catch (InvalidOperationException iex)
                {
                    MessageBox.Show(iex.Message, "Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something Went Wrong\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.txtSearch.Focus();
        }

        #endregion

        #region Search
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = this.txtSearch.Text;
            var item = this.Processor.ItemsPending.FirstOrDefault(i => i.Asset.Account.FileNo.Equals(searchString, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                this.lstPending.SelectedIndex = this.lstPending.Items.IndexOf(item);
            }
            else
            {
                this.lstPending.SelectedIndex = -1;
            }
            this.txtSearch.Text = string.Empty;
            this.txtSearch.Focus();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSearch_Click(sender, e);
            }
        }

        #endregion

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateStatus("Processing...", false);
                bool success = false;
                try
                {
                    await this.Processor.ProcessResultsAsync();
                    success = true;
                }
                catch (AggregateException aex)
                {
                    bool cont = true;
                    foreach (var ex in aex.Flatten().InnerExceptions)
                    {
                        cont = MessageBox.Show(ex.Message + "\r\n\r\nShow More?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes;
                        if (!cont) break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (success)
                    this.Close();
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.Flatten().InnerExceptions)
                {
                    bool cont = true;
                    cont = MessageBox.Show(ex.Message + "\r\n\r\nShow More?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes;
                    if (!cont) break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Unknown Error Occurred\r\n\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateStatus("Ready!", true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose(true);
        }

        #endregion

        #region Processing Helpers

        private Tuple<Data.Requests.Results.Result, Dictionary<string, object>> SelectResult(IEnumerable<Data.Requests.Results.Result> options, IEnumerable<Data.Requests.Results.Codes.MergeCodeInfo> additions, Dictionary<string, object> previousValues)
        {
            if (options == null) return null;

            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    return this.Invoke(new Func<IEnumerable<Data.Requests.Results.Result>, IEnumerable<Data.Requests.Results.Codes.MergeCodeInfo>, Dictionary<string, object>, Tuple<Data.Requests.Results.Result, Dictionary<string, object>>>(SelectResult), new object[] { options, additions, previousValues }) as Tuple<Data.Requests.Results.Result, Dictionary<string, object>>;
                }
                throw new NotImplementedException();
            }

            SelectResultForm srf = new Forms.Processing.SelectResultForm(options);
            if (srf.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, object> enteredValues = new Dictionary<string, object>();
                var fieldValues = srf.SelectedResult.MergeCodes.SelectMany(merge => merge.DebtorCodes.SelectMany(debtor => debtor.Codes.SelectMany(code => code.Values))).Union(additions?.Select(add => new Data.Requests.Results.Codes.MergeCodeFieldValue(new Data.Requests.Results.Codes.MergeCodeField(), add)) ?? new Data.Requests.Results.Codes.MergeCodeFieldValue[0]).GroupBy(value => value.Info.Value);
                foreach (var fv in fieldValues)
                {
                    var fieldValue = fv.FirstOrDefault();
                    if (previousValues?.ContainsKey(fieldValue.Info.Value) ?? false)
                    {
                        enteredValues.Add(fieldValue.Info.Value, previousValues[fieldValue.Info.Value]);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(fieldValue.Info.ReferenceObject) || string.IsNullOrEmpty(fieldValue.Info.ReferenceObject))
                        {
                            switch (fieldValue.Info.InfoType)
                            {
                                case Data.Enums.ResultInfoType.String:
                                    EnterTextForm etf = new EnterTextForm(fieldValue.Info.Value);
                                    while (etf.ShowDialog(this) != DialogResult.OK) { }
                                    enteredValues.Add(fieldValue.Info.Value, etf.EnteredText);
                                    break;
                                case Data.Enums.ResultInfoType.Date:
                                    SelectDateForm sdf = new SelectDateForm(fieldValue.Info.Value);
                                    while (sdf.ShowDialog(this) != DialogResult.OK) { }
                                    enteredValues.Add(fieldValue.Info.Value, sdf.SelectedDate);
                                    break;
                                case Data.Enums.ResultInfoType.Money:
                                    EnterDecimalForm edf = new EnterDecimalForm(fieldValue.Info.Value);
                                    while (edf.ShowDialog(this) != DialogResult.OK) { }
                                    enteredValues.Add(fieldValue.Info.Value, edf.EnteredNumber);
                                    break;
                            }

                        }
                    }
                }

                return new Tuple<Data.Requests.Results.Result, Dictionary<string, object>>(srf.SelectedResult, enteredValues);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private void UpdateStatus(string status, bool enable)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<string, bool>(UpdateStatus), status, enable);
                }
                return;
            }

            this.lblStatus.Text = status;
            this.Enabled = enable;
        }

        #region Drawing
        private void lstPending_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;

            e.ItemHeight = lb.Font.Height;
        }

        private void lstPending_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {
                ListBox lb = sender as ListBox;
                e.DrawBackground();
                Brush myBrush = SystemBrushes.ControlText;
                Data.Requests.Request toDraw = (Data.Requests.Request)lb.Items[e.Index];
                Brush myHighlight = SystemBrushes.Highlight;// toDraw.JudgmentExpiringSoon ? new SolidBrush(Color.LawnGreen) : new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
                int Diff = (DateTime.Now - toDraw.CurrentStatus.Date).Days;
                if (Diff > Data.Dictionaries.StatusDaysColor[this.Processor.ProcessingStatus][2])
                {
                    myBrush = Brushes.Red;
                    myHighlight = Brushes.Yellow;
                }
                else if (Diff > Data.Dictionaries.StatusDaysColor[this.Processor.ProcessingStatus][1])
                {
                    myBrush = Brushes.Orange;
                    myHighlight = Brushes.Purple;
                }
                else if (Diff < Data.Dictionaries.StatusDaysColor[this.Processor.ProcessingStatus][0])
                {
                    myBrush = Brushes.DarkGreen;
                    myHighlight = Brushes.LightGreen;
                }
                else
                {
                    if (e.State.HasFlag(DrawItemState.Selected))
                        myBrush = SystemBrushes.HighlightText;
                }

                //if (toDraw.JudgmentExpiringSoon) myBrush = new SolidBrush(Color.ForestGreen);

                if (e.State.HasFlag(DrawItemState.Selected))
                {
                    e.Graphics.FillRectangle(myHighlight, e.Bounds);
                }
                e.Graphics.DrawString(toDraw.ToString(lb.FormatString), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }
        }
        #endregion

        private void ProcessingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }

        #region IDisposable
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    this.Processor.Dispose();
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                catch { }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
