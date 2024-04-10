using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HB.Garnishments.UI.Forms.Reporting
{
    public partial class ReportingForm : Form
    {
        public ReportingForm()
        {
            InitializeComponent();
        }

        private void ReportingForm_Load(object sender, EventArgs e)
        {
            this.cboStatus.DataSource = Enum.GetValues(typeof(Data.Enums.Status));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var status = (Data.Enums.Status)this.cboStatus.SelectedItem;
            //System.Windows.Forms.Delegates.EventArgs.TextMultiProgressUpdatedEventHandler updatedEventHandler = null;
          //  MultiProgressTextForm multiProgressTextForm = new MultiProgressTextForm(() =>
           // {
             //   Task.Run(async () => await //Garnishments.Reporting.ReportGenerator.CreateUnprocessedReportAsync(status)).Wait();
           // }, "Loading...");
           // updatedEventHandler += multiProgressTextForm.UpdateProgress;
           // multiProgressTextForm.ShowDialog(this);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
