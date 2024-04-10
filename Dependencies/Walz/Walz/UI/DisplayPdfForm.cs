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
    public partial class DisplayPdfForm : Form
    {
        public DisplayPdfForm(Enums.FileType type, System.IO.Stream pdfStream)
        {
            InitializeComponent();
            System.IO.FileInfo file = new System.IO.FileInfo(System.IO.Path.GetTempFileName());

            using (System.IO.Stream fs = file.OpenWrite())
            {
                pdfStream.Seek(0, System.IO.SeekOrigin.Begin);
                pdfStream.CopyTo(fs);
            }
            this.wbDocument.Navigate(file.FullName);
            this.Text = type == Enums.FileType.BarcodeFile ? "Barcodes" : "Receipt";
        }
    }
}
