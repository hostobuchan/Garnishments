using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WalzDebugging
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int batch;
            if (int.TryParse(txtID.Text, out batch))
            {
                Walz.Data.Files.Batches.BatchManager manager = new Walz.Data.Files.Batches.BatchManager(new Walz.Data.DataHandler());
                Walz.Data.Files.Batches.BatchBuilder builder = new Walz.Data.Files.Batches.BatchBuilder(manager, batch, true);
                Walz.Data.UI.BatchManagementForm bmf = new Walz.Data.UI.BatchManagementForm(builder);
                bmf.ShowDialog(this);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Walz.Data.UI.SettingsForm settingsForm = new Walz.Data.UI.SettingsForm();
            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Task.Run(async () => await settingsForm.DataHandler.SaveSettings()).Wait();
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
