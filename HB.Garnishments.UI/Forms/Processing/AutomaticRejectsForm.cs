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
    public partial class AutomaticRejectsForm : Form
    {
        Data.Processing.IProcessor Processor { get; set; }
        DataTable Table { get; set; }

        public AutomaticRejectsForm(Data.Processing.IProcessor processor)
        {
            this.Processor = processor;
            InitializeComponent();
        }

        private void AutomaticRejectsForm_Load(object sender, EventArgs e)
        {
            //this.Processor.FailedItemsPending.First().Item1.Asset.Account.FileNo
            this.Table = Data.Processing.Rejects.ReportGenerator.CreateRejectTable();
        }

        private void AutomaticRejectsForm_Shown(object sender, EventArgs e)
        {
            WaitForm waitForm = new WaitForm(() =>
            {
                foreach (var reject in this.Processor.FailedItemsPending)
                {
                    Data.Processing.Rejects.ReportGenerator.AddRejectToTable(this.Table, reject);
                }
            });
            waitForm.ShowDialog(this);
            this.dgvRejects.DataSource = this.Table;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WaitForm waitForm = new WaitForm(() =>
            {
                ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();
                xlApp.xlBook.AddWorksheetFromTable(this.Table);
                xlApp.ShowWorkBook();
            });
            waitForm.ShowDialog(this);
        }
    }
}
