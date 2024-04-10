using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface.Application
{
    public class Excel : IDisposable
    {
        public event Action<int> ProgressUpdated;
        protected void OnProgressUpdated(int Progress) { this.ProgressUpdated?.Invoke(Progress); }

        private object missing = Type.Missing;
        private AutoCleanup<Microsoft.Office.Interop.Excel.Application> xlApp;
        private AutoCleanup<Microsoft.Office.Interop.Excel.Workbooks> xlBooks;
        private bool CanClose;
        public Workbook xlBook { get; private set; }

        private Excel(bool stayOpen)
        {
            CanClose = !stayOpen;
            xlApp = new Application.AutoCleanup<Microsoft.Office.Interop.Excel.Application>(new Microsoft.Office.Interop.Excel.Application());
            xlApp.Resource.StandardFont = "Arial";
            xlApp.Resource.StandardFontSize = 10;
            xlApp.Resource.WorkbookBeforeClose += new Microsoft.Office.Interop.Excel.AppEvents_WorkbookBeforeCloseEventHandler(xlApp_WorkbookBeforeClose);
            xlBooks = new Application.AutoCleanup<Microsoft.Office.Interop.Excel.Workbooks>(xlApp.Resource.Workbooks);
        }
        public Excel() : this(false)
        {
            xlBook = new Workbook(xlBooks.Resource.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet));
        }
        public Excel(string fileLocation, bool stayOpen = false) : this(stayOpen)
        {
            xlBook = new Workbook(xlBooks.Resource.Open(fileLocation));
        }
        public Excel(System.IO.Stream fileStream, Enums.ExcelFormat format, bool stayOpen = false) : this(stayOpen)
        {
            string Path = System.IO.Path.GetTempFileName() + "." + format.ToString();
            System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Create);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
            xlBook = new Workbook(xlBooks.Resource.Open(Path));
        }

        private void xlApp_WorkbookBeforeClose(Microsoft.Office.Interop.Excel.Workbook Wb, ref bool Cancel)
        {
            if (!CanClose) Cancel = true;
        }

        public void ShowWorkBook()
        {
            try
            {
                this.xlBook.DeleteWorksheet("Sheet1");
            }
            catch { }
            xlApp.Resource.Visible = true;
        }

        public void CloseExcel()
        {
            try
            {
                CanClose = true;
                xlBook.Close(false);
                xlApp.Resource.Quit();
                Dispose();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public void CloseAndSave(string FileLocation)
        {
            try
            {
                CanClose = true;
                this.xlBook.DeleteWorksheet("Sheet1");
            }
            catch { }
            try
            {
                xlBook.Close(true, FileLocation);
                xlApp.Resource.Quit();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            finally
            {
                try
                {
                    this.Dispose();
                }
                catch { }
            }
        }

        public void CloseAndSaveAs(string FileLocation, Enums.FileFormat Format)
        {
            try
            {
                CanClose = true;
                this.xlBook.DeleteWorksheet("Sheet1");
            }
            catch { }
            try
            {
                xlBook.SaveAs(FileLocation, Format);
                xlBook.Close(false);
                xlApp.Resource.Quit();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            finally
            {
                try
                {
                    this.Dispose();
                }
                catch { }
            }
        }

        public void Zoom(int Percent)
        {
            xlApp.Resource.ActiveWindow.Zoom = Percent;
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.ProgressUpdated = null;
                this.CanClose = true;
                this.xlBook?.Close(false);
            }
            catch { }
            try
            {
                xlApp?.Resource?.Quit();
            }
            catch { }

            try
            {
                this.xlApp?.Dispose();
                this.xlBooks?.Dispose();
                this.xlBook?.Dispose();
            }
            catch { }

            this.xlApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion
    }
}
