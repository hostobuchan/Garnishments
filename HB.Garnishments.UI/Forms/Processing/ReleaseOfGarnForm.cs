using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Processing
{
    public partial class ReleaseOfGarnForm : Form
    {
        BindingList<Tuple<string, DateTime, int?>> Releases = new BindingList<Tuple<string, DateTime, int?>>();

        public ReleaseOfGarnForm()
        {
            InitializeComponent();
        }

        private void ReleaseOfGarnForm_Load(object sender, EventArgs e)
        {
            this.dgvReleases.AutoGenerateColumns = false;
            this.dgvReleases.DataSource = Releases;
            this.dgvReleases.Columns["FileNo"].DataPropertyName = "Item1";
            this.dgvReleases.Columns["ReleaseDate"].DataPropertyName = "Item2";
            this.dgvReleases.Columns["RequestID"].DataPropertyName = "Item3";
        }

        private void txtFileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFileNo.Text))
            {
                try
                {
                    if (await Data.DataHandler.CheckFileNoAsync(txtFileNo.Text))
                    {
                        var result = await Data.Processing.ReleaseOfGarnProcessor.GetGarnishmentRequestAsync(txtFileNo.Text, SelectRequest);
                        if (result?.Item1 ?? false)
                        {
                            this.Releases.Add(
                                new Tuple<string, DateTime, int?>(
                                    txtFileNo.Text,
                                    this.dtReleaseDate.Value,
                                    result.Item2
                                )
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid File No. \"{txtFileNo.Text}\"", "Bad Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An Exception Occurred While Verifying File No.\r\n\r\n{ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txtFileNo.Text = string.Empty;
            }
            txtFileNo.Focus();
        }

        private Tuple<bool, int?> SelectRequest(IEnumerable<Data.Requests.AssetRequest> requests)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    return this.Invoke(new Func<IEnumerable<Data.Requests.AssetRequest>, Tuple<bool, int?>>(SelectRequest), requests) as Tuple<bool, int?>;
                }
            }

            SelectRequestForm srf = new SelectRequestForm(requests);
            if (srf.ShowDialog(this) == DialogResult.OK)
            {
                return new Tuple<bool, int?>(true, srf.SelectedRequest?.ID);
            }
            else
            {
                return new Tuple<bool, int?>(false, null);
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string saveLocation = null;
                await Data.Processing.ReleaseOfGarnProcessor.ProcessReleasesAsync(this.Releases, UI.Settings.Properties.User.SamAccountName, new Func<string>(() =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog()
                    {
                        Title = "Select Where to Save Merge File",
                        Filter = "Text Files|*.txt",
                        FilterIndex = 1
                    };
                    while (saveFileDialog.ShowDialog() != DialogResult.OK) { }
                    saveLocation = saveFileDialog.FileName;
                    return saveFileDialog.FileName;
                }));
                this.Releases.Clear();
                MessageBox.Show($"Please Open CLS and Press 1-3-6-5-4\n\nThen Press F5 to Search For File\nLocate File: {saveLocation}\nClick Open to Select File\nPress Enter to Add Documents\n\nThen Escape Back To Main Menu\nPress 3-8-3 to Merge All Documents", "CLS Document Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An Exception Occurred During The Operation\r\n\r\n{ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
