using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Results
{
    public partial class ResultsForm : Form
    {
        Data.Enums.AssetType AssetType { get; set; }
        Data.Enums.Status Status { get; set; }
        Data.Requests.Results.ResultCodeHandler CodeHandler { get; set; }
        BindingList<Data.Requests.Results.Result> Results { get; set; }
        BindingList<EvaluationCriteria.CriteriaSets.State> States { get; set; }
        BindingList<byte> Debtors { get; set; } = new BindingList<byte>(new byte[] { 1, 2, 3 }.ToList());
        bool _Editing = false;

        public ResultsForm(Data.Enums.AssetType assetType, Data.Enums.Status status, Data.Requests.Results.ResultCodeHandler resultCodeHandler, IEnumerable<EvaluationCriteria.CriteriaSets.State> states)
        {
            this.AssetType = assetType;
            this.Status = status;
            this.CodeHandler = resultCodeHandler;
            this.Results = new BindingList<Data.Requests.Results.Result>(resultCodeHandler.Results?.OrderBy(res => res.Description).ToList() ?? new List<Data.Requests.Results.Result>());
            this.States = new BindingList<EvaluationCriteria.CriteriaSets.State>(states.OrderBy(s => s.Abbreviation).ToList());
            InitializeComponent();
        }

        private async void ResultsForm_Load(object sender, EventArgs e)
        {
            lock (this)
            {
                this._Editing = true;

                try
                {
                    this.cboResult.DataSource = this.Results;
                    this.cboState.DataSource = this.States;
                    this.cboState.DisplayMember = "Abbreviation";
                    this.cboState.ValueMember = "SalesNo";
                    this.cboDebtor.DataSource = this.Debtors;
                    SetSelectedResult(this.cboResult.SelectedItem as Data.Requests.Results.Result);
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    this._Editing = false;
                }
            }
        }

        private void SetSelectedResult(Data.Requests.Results.Result result)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    this.Invoke(new Action<Data.Requests.Results.Result>(SetSelectedResult), result);
                }
                return;
            }

            lock (this)
            {
                this.rdoGood.Checked = result.Good;
                this.rdoBad.Checked = !result.Good;
                this.chkUpdateAsset.Checked = result.Update;
                this.chkMoneyExpected.Checked = result.MoneyExpected;

                object val = this.cboState.SelectedValue;
                object val2 = this.cboDebtor.SelectedValue;
                if (val != null && val2 != null && (byte)val > 0 && (byte)val2 > 0)
                {
                    SetSelectedCodeSubset(result, (byte)val, (byte)val2);
                }
            }
        }

        private void SetSelectedCodeSubset(Data.Requests.Results.Result result, byte salesNo, byte debtor)
        {
            Controls.Settings.Results.ResultCodesControl codesPanel = new Controls.Settings.Results.ResultCodesControl(this.CodeHandler, result, salesNo, debtor);
            this.splitContainer1.Panel2.Controls.Clear();
            this.splitContainer1.Panel2.Controls.Add(codesPanel);
            codesPanel.Dock = DockStyle.Fill;
        }

        private void cboResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._Editing)
            {
                try
                {
                    this._Editing = true;
                    SetSelectedResult((sender as ComboBox).SelectedItem as Data.Requests.Results.Result);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    this._Editing = false;
                }
            }
        }

        private async void rdoGood_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._Editing)
            {
                // Update Good Status for Result
                Data.Requests.Results.Result result = this.cboResult.SelectedItem as Data.Requests.Results.Result;
                if (result != null)
                {
                    try
                    {
                        this._Editing = true;

                        var newResult = await Data.DataHandler.UpdateResultAsync(result, this.rdoGood.Checked, result.Update, result.MoneyExpected);

                        lock (this)
                        {
                            var index = this.Results.IndexOf(result);
                            this.Results.Remove(result);
                            this.Results.Insert(index, newResult);
                            this.cboResult.SelectedItem = newResult;
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                    finally
                    {
                        this._Editing = false;
                    }
                }
            }
        }

        private async void chkUpdateAsset_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._Editing)
            {
                // Update "Update Asset" Status for Result
                Data.Requests.Results.Result result = this.cboResult.SelectedItem as Data.Requests.Results.Result;
                if (result != null)
                {
                    try
                    {
                        this._Editing = true;

                        var newResult = await Data.DataHandler.UpdateResultAsync(result, result.Good, chkUpdateAsset.Checked, result.MoneyExpected);

                        lock (this)
                        {
                            var index = this.Results.IndexOf(result);
                            this.Results.Remove(result);
                            this.Results.Insert(index, newResult);
                            this.cboResult.SelectedItem = newResult;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        this._Editing = false;
                    }
                }
            }
        }

        private async void chkMoneyExpected_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._Editing)
            {
                // Update "Update Asset" Status for Result
                Data.Requests.Results.Result result = this.cboResult.SelectedItem as Data.Requests.Results.Result;
                if (result != null)
                {
                    try
                    {
                        this._Editing = true;

                        var newResult = await Data.DataHandler.UpdateResultAsync(result, result.Good, result.Update, chkMoneyExpected.Checked);

                        lock (this)
                        {
                            var index = this.Results.IndexOf(result);
                            this.Results.Remove(result);
                            this.Results.Insert(index, newResult);
                            this.cboResult.SelectedItem = newResult;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        this._Editing = false;
                    }
                }
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            EnterNameForm enf = new EnterNameForm("Enter Result Description");
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this._Editing = true;

                    var newResult = await Data.DataHandler.AddResultAsync(this.AssetType, this.Status, enf.EnteredText);
                    this.Results.Add(newResult);
                    this.cboResult.SelectedItem = newResult;
                    SetSelectedResult(newResult);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this._Editing = false;
                }
            }
        }

        private void CodeSelectionBoxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            object val = this.cboState.SelectedValue;
            object val2 = this.cboDebtor.SelectedValue;
            if (val != null && val2 != null && (byte)val > 0  && (byte)val2 > 0)
            {
                try
                {
                    SetSelectedCodeSubset(this.cboResult.SelectedItem as Data.Requests.Results.Result, (byte)val, (byte)val2);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
