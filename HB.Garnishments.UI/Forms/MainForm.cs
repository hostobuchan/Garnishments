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

namespace HB.Garnishments.UI.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm(Task.Run(async () =>
            {
                UI.Settings.Properties.RegisteredAgents = await Data.DataHandler.GetRegisteredAgentsAsync();
                UI.Settings.Properties.Venues = await Data.DataHandler.GetVenuesAsync();
                UI.Settings.Properties.Counsels = await Data.DataHandler.GetCounselAsync();
            }));
            wf.ShowDialog(this);
            while (string.IsNullOrEmpty(Data.Settings.Properties.FaxNumber))
            {
                // Capture User's Fax Number
                EnterNameForm enf = new EnterNameForm("Please Enter Your Fax Number", 12, "501.375.3670");
                if (enf.ShowDialog(this) == DialogResult.OK)
                {
                    Data.Settings.Properties.FaxNumber = enf.EnteredText;
                }
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            SelectComboForm<string, Data.Enums.Status> scf = new SelectComboForm<string, Data.Enums.Status>("Select Processing Step", new Dictionary<string, Data.Enums.Status>()
            {
                { "New Requests", Data.Enums.Status.Processed },
                { "Signed by Attorney", Data.Enums.Status.Signed },
                { "File with Court", Data.Enums.Status.Filed },
                { "Hearing Request", Data.Enums.Status.HearingRequested },
                { "Court Response", Data.Enums.Status.CourtResponse },
                { "Garnishee Response", Data.Enums.Status.GarnisheeResponse }
            });

            if (scf.ShowDialog(this) == DialogResult.OK)
            {
                bool genericForm = true;
                Data.Processing.IProcessor processor = null;
                SelectComboForm<string, byte> selectState = new SelectComboForm<string, byte>("Select Legal State", UI.Settings.Properties.States.ToDictionary(s => s.Abbreviation, s => s.SalesNo));
                if (selectState.ShowDialog(this) == DialogResult.OK)
                {
                    switch (scf.SelectedItem)
                    {
                        case Data.Enums.Status.Processed:
                            genericForm = false;

                            processor = new Data.Processing.RequestProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            Processing.CountyProcessingForm countyProcessingForm = new Processing.CountyProcessingForm(processor);
                            countyProcessingForm.ShowDialog(this);
                            try
                            {
                                await processor.ProcessResultsAsync();
                            }
                            catch (AggregateException aex)
                            {
                                // Handle Multiple Failures
                                bool cont = true;
                                foreach (var ex in aex.Flatten().InnerExceptions)
                                {
                                    cont = MessageBox.Show(ex.Message + "\n\nShow More?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes;
                                    if (!cont) break;
                                }
                                this.DialogResult = DialogResult.Abort;
                            }
                            catch (Exception ex)
                            {
                                // Handle Single Failure
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case Data.Enums.Status.Signed:
                            processor = new Data.Processing.SignedProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.Venues, UI.Settings.Properties.RegisteredAgents, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            break;
                        case Data.Enums.Status.Filed:
                            processor = new Data.Processing.FilingProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            break;
                        case Data.Enums.Status.HearingRequested:
                            processor = new Data.Processing.HearingRequestProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            break;
                        case Data.Enums.Status.CourtResponse:
                            processor = new Data.Processing.CourtAnswerProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            break;
                        case Data.Enums.Status.GarnisheeResponse:
                            processor = new Data.Processing.GarnisheeAnswerProcessor(SynchronizationContext.Current, UI.Settings.Communications.HubConnection, UI.Settings.Properties.User.SamAccountName, selectState.SelectedItem);
                            break;
                    }
                    if (genericForm)
                    {
                        Processing.ProcessingForm pf = new Processing.ProcessingForm(processor);
                        pf.ShowDialog(this);
                    }
                }
            }

        }

        private void btnReleaseOfGarn_Click(object sender, EventArgs e)
        {
            Processing.ReleaseOfGarnForm rogf = new Processing.ReleaseOfGarnForm();
            rogf.ShowDialog(this);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reporting.ReportingForm reportingForm = new Reporting.ReportingForm();
            reportingForm.ShowDialog(this);
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            Settings.SettingsForm sf = new Settings.SettingsForm();
            sf.ShowDialog(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
