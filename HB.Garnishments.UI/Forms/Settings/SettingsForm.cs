using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private async void btnRestrictions_Click(object sender, EventArgs e)
        {
            SelectComboForm<string, byte> scf = new SelectComboForm<string, byte>("Select Legal State", UI.Settings.Properties.States.ToDictionary(s => s.Abbreviation, s => s.SalesNo));
            if (scf.ShowDialog(this) == DialogResult.OK)
            {
                SelectComboFormStruct<Data.Enums.AssetType> scf2 = new SelectComboFormStruct<Data.Enums.AssetType>("Select Asset Type", Enum.GetValues(typeof(Data.Enums.AssetType)).Cast<Data.Enums.AssetType>());
                if (scf2.ShowDialog(this) == DialogResult.OK)
                {
                    SelectComboFormStruct<Data.Enums.Status> scf3 = new SelectComboFormStruct<Data.Enums.Status>("Select Processing Status", Enum.GetValues(typeof(Data.Enums.Status)).Cast<Data.Enums.Status>());
                    if (scf3.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            Data.CriteriaSets.CriteriaDataHandler dataHandler = await Data.CriteriaSets.CriteriaDataHandler.CreateCriteriaDataHandlerAsync();
                            Data.CriteriaSets.Routine routine = dataHandler.Routines.FirstOrDefault(r => r.SalesNo == scf.SelectedItem && r.AssetType == scf2.SelectedItem && r.Status == scf3.SelectedItem);
                            if (routine == null)
                            {
                                routine = new Data.CriteriaSets.Routine(dataHandler, scf.SelectedItem, scf2.SelectedItem.Value, scf3.SelectedItem.Value);
                            }
                            Criteria.RoutineForm rf = new Criteria.RoutineForm(dataHandler, routine);
                            rf.ShowDialog(this);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void btnResults_Click(object sender, EventArgs e)
        {
            SelectComboFormStruct<Data.Enums.AssetType> scf = new SelectComboFormStruct<Data.Enums.AssetType>("Select Asset Type", Enum.GetValues(typeof(Data.Enums.AssetType)).Cast<Data.Enums.AssetType>());
            if (scf.ShowDialog(this) == DialogResult.OK)
            {
                SelectComboFormStruct<Data.Enums.Status> scf2 = new SelectComboFormStruct<Data.Enums.Status>("Select Processing Step", Enum.GetValues(typeof(Data.Enums.Status)).Cast<Data.Enums.Status>());
                if (scf2.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var states = await Data.DataHandler.GetStatesAsync();
                        var resultHandler = await Data.DataHandler.GetResultCodeHandlerAsync(scf2.SelectedItem.Value, scf.SelectedItem.Value);
                        Results.ResultsForm rf = new Results.ResultsForm(scf.SelectedItem.Value, scf2.SelectedItem.Value, resultHandler, states);
                        rf.ShowDialog(this);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            try
            {
                Fees.FeesForm feesForm = new Fees.FeesForm();
                feesForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLetterhead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Select Firm Letterhead",
                Filter = "PDF|*.pdf",
                FilterIndex = 1
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(ofd.FileName, System.IO.FileMode.Open))
                    {
                        var newLetterhead = await Data.DataHandler.AddNewFirmLetterhead(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An Unknown Error Occurred\r\n\r\n{ex.Message}", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnOverride_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm("Enter File No.", 8);
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var assets = await Data.DataHandler.GetAccountAssetsAsync(enf.EnteredText);
                    var infos = assets.Assets?.SelectMany(aai => aai.History);
                    var requests = infos.SelectMany(info => Task.Run(info.GetRequestsAsync).Result).ToArray();
                    foreach (var request in requests)
                    {
                        await request.GetAssetInfoAsync();
                    }
                    if (requests.Length > 0)
                    {
                        Overrides.AccountForm accountForm = new Overrides.AccountForm(assets.Assets, requests);
                        accountForm.ShowDialog(this);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnWalz_Click(object sender, EventArgs e)
        {
            using (Walz.Data.UI.SettingsForm settings = new Walz.Data.UI.SettingsForm())
            {
                if (settings.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        await settings.DataHandler.SaveSettings();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
