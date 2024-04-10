using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Collections;

using static ExcelInterface.Utilities;

namespace ExcelInterface.Application
{
    public class Workbook : IEnumerable<Workbooks.Worksheet>, IDisposable
    {
        public event Action<int> ProgressUpdated;
        protected void OnProgressUpdated(int progress) { this.ProgressUpdated?.Invoke(progress); }

        private AutoCleanup<Microsoft.Office.Interop.Excel.Workbook> _Workbook;
        private AutoCleanup<Microsoft.Office.Interop.Excel.Sheets> _Worksheets;
        private object missing = Type.Missing;


        public List<Workbooks.Worksheet> Worksheets { get; private set; }
        public Workbooks.Worksheet this[string Name] { get { return this.Worksheets.Find(el => el.Name == Name); } }

        public Workbooks.Worksheet ActiveSheet { get { return new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)_Workbook.Resource.ActiveSheet); } }

        internal Workbook(Microsoft.Office.Interop.Excel.Workbook workbook)
        {
            this._Workbook = new AutoCleanup<Microsoft.Office.Interop.Excel.Workbook>(workbook);
            this._Worksheets = new AutoCleanup<Microsoft.Office.Interop.Excel.Sheets>(workbook.Worksheets);
            this.Worksheets = new List<Workbooks.Worksheet>();
        }

        public Workbooks.Worksheet AddWorksheetBlank(string SheetName, Color TabColor)
        {
            Workbooks.Worksheet xlSheet = new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource.Add(missing, this._Worksheets.Resource[this._Worksheets.Resource.Count], missing, missing));
            this.Worksheets.Add(xlSheet);

            try
            {
                xlSheet.Name = SheetName;
            }
            catch
            {
                bool t = false;
                int a = 1;
                while (t == false)
                {
                    try
                    {
                        xlSheet.Name = $"{SheetName} {a}";
                        t = true;
                    }
                    catch { }
                    a++;
                }
            }
            if (!TabColor.IsEmpty)
                xlSheet.TabColor = TabColor;
            return xlSheet;
        }

        public Workbooks.Worksheet AddWorksheetFromTable(System.Data.DataTable dt, bool formatAsTable = true)
        {
            Workbooks.Worksheet xlSheet = new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource.Add(missing, this._Worksheets.Resource[this._Worksheets.Resource.Count], missing, missing));
            this.Worksheets.Add(xlSheet);
            try
            {
                xlSheet.Name = dt.TableName;
            }
            catch
            {
                bool t = false;
                int a = 1;
                while (t == false)
                {
                    try
                    {
                        xlSheet.Name = dt.TableName + " " + a.ToString();
                        t = true;
                    }
                    catch { }
                    a++;
                }
            }

            Workbooks.Worksheets.Range rng = xlSheet.GetRange("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (DataColumn dc in dt.Columns)
            {
                rng = xlSheet.GetRange("A1", missing).GetOffset(missing, i);
                rng.Value2 = dc.ColumnName;
                string Format = "";
                if (dc.DataType.Name == "DateTime") Format = "MM/dd/yyyy";
                if (dc.DataType.Name == "String") Format = "@";
                if (dc.DataType.Name == "Double") Format = "0.00";
                rng.Columns.EntireColumn.NumberFormat = Format;

                i++;
            }

            i = 0;
            int ttl = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                OnProgressUpdated(i * 100 / ttl);
                rng = xlSheet.GetRange("A2", GetColumnName(dt.Columns.Count - 1) + "2").GetOffset(i, missing);
                rng.Value2 = dr.ItemArray;
                i++;
            }
            rng = xlSheet.GetRange("A1", GetColumnName(dt.Columns.Count - 1) + (dt.Rows.Count + 1).ToString());
            if (formatAsTable) xlSheet.ListObjects.Add(Enums.ListObjectSourceType.Range, rng, null, Enums.YesNoGuess.Yes, "A1").Name = dt.TableName + " Table";
            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public Workbooks.Worksheet AddWorksheetFromTable(System.Data.DataTable dt, string[] Formats, bool formatAsTable = true)
        {
            Workbooks.Worksheet xlSheet = new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource.Add(missing, this._Worksheets.Resource[this._Worksheets.Resource.Count], missing, missing));
            try
            {
                xlSheet.Name = dt.TableName;
            }
            catch
            {
                bool t = false;
                int a = 1;
                while (t == false)
                {
                    try
                    {
                        xlSheet.Name = dt.TableName + " " + a.ToString();
                        t = true;
                    }
                    catch { }
                    a++;
                }
            }

            Workbooks.Worksheets.Range rng = xlSheet.GetRange("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (DataColumn dc in dt.Columns)
            {
                rng = xlSheet.GetRange("A1", missing).GetOffset(missing, i);
                rng.Value2 = dc.ColumnName;
                i++;
            }

            for (int x = 0; x < Formats.Length; x++)
            {
                rng = xlSheet.GetRange(GetColumnName(x) + "1", missing);
                rng.Columns.EntireColumn.NumberFormat = Formats[x];
            }


            rng = xlSheet.GetRange("A1", GetColumnName(dt.Columns.Count - 1) + (dt.Rows.Count + 1).ToString());
            if (formatAsTable) xlSheet.ListObjects.Add(Enums.ListObjectSourceType.Range, rng, null, Enums.YesNoGuess.Yes, "A1").Name = dt.TableName + " Table";

            i = 0;
            int ttl = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                OnProgressUpdated(i * 100 / ttl);
                rng = xlSheet.GetRange("A2", GetColumnName(dt.Columns.Count - 1) + "2").GetOffset(i, missing);
                rng.Value2 = dr.ItemArray;
                i++;
            }

            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public Workbooks.Worksheet AddWorksheetFromList<T>(string SheetName, List<T> objList, string[] Formats, bool formatAsTable = true)
        {
            Workbooks.Worksheet xlSheet = new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource.Add(missing, this._Worksheets.Resource[this._Worksheets.Resource.Count], missing, missing));
            try
            {
                xlSheet.Name = SheetName;
            }
            catch
            {
                bool t = false;
                int a = 1;
                while (t == false)
                {
                    try
                    {
                        xlSheet.Name = SheetName + " " + a.ToString();
                        t = true;
                    }
                    catch { }
                    a++;
                }
            }

            Workbooks.Worksheets.Range rng = xlSheet.GetRange("A1", missing);

            IEnumerable<System.Reflection.PropertyInfo> Properties = typeof(T).GetProperties().Where(el => !el.PropertyType.IsArray && !(el.PropertyType.IsGenericType && (el.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || el.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))));

            int i = 0;
            OnProgressUpdated(0);
            foreach (System.Reflection.PropertyInfo PI in Properties)
            {
                rng = xlSheet.GetRange("A1", missing).GetOffset(missing, i);
                rng.Value2 = PI.Name;
                rng.Columns.EntireColumn.NumberFormat = i <= Formats.GetUpperBound(0) ? Formats[i] : "@";

                i++;
            }

            int row = 0;
            int ttl = objList.Count;
            foreach (T obj in objList)
            {
                OnProgressUpdated(row * 100 / ttl);
                rng = xlSheet.GetRange("A2", GetColumnName(Properties.Count() - 1) + "2").GetOffset(row, missing);
                object[] Arry = new object[Properties.Count()];

                i = 0;
                foreach (System.Reflection.PropertyInfo PI in Properties)
                {
                    try
                    {
                        if (PI.PropertyType == typeof(DateTime))
                        {
                            Arry[i] = ((DateTime?)PI.GetValue(obj, null))?.ToOADate();
                        }
                        else if (PI.PropertyType.IsPrimitive)
                        {
                            Arry[i] = PI.GetValue(obj, null);
                        }
                        else if (PI.PropertyType.IsGenericType && PI.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            if (PI.PropertyType == typeof(DateTime?))
                            {
                                DateTime? Value = (DateTime?)PI.GetValue(obj, null);
                                Arry[i] = Value.HasValue ? (double?)Value.Value.ToOADate() : null;
                            }
                            else
                            {
                                Arry[i] = PI.GetValue(obj, null);
                            }
                        }
                        else if (PI.PropertyType.GetInterfaces().Contains(typeof(IFormattable)))
                        {
                            Arry[i] = (PI.GetValue(obj, null) as IFormattable)?.ToString("G", null);
                        }
                        else
                            Arry[i] = PI.GetValue(obj, null)?.ToString();
                    }
                    catch { }
                    i++;
                }
                rng.Value2 = Arry;
                row++;
            }
            rng = xlSheet.GetRange("A1", GetColumnName(Properties.Count() - 1) + (objList.Count + 1).ToString());
            if (formatAsTable) xlSheet.ListObjects.Add(Enums.ListObjectSourceType.Range, rng, null, Enums.YesNoGuess.Yes, "A1").Name = SheetName + " Table";

            xlSheet.Columns.ColumnWidth = 255;
            xlSheet.Columns.AutoFit();
            xlSheet.Rows.AutoFit();
            return xlSheet;
        }

        public Workbooks.Worksheet AddWorksheetFromList<T>(string SheetName, List<T> objList, bool formatAsTable = true)
        {
            IEnumerable<System.Reflection.PropertyInfo> Properties = typeof(T).GetProperties().Where(el => !el.PropertyType.IsArray && !(el.PropertyType.IsGenericType && (el.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || el.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))));
            List<string> Formats = new List<string>();
            foreach (System.Reflection.PropertyInfo PI in Properties)
            {
                string Format = "";
                if (PI.PropertyType == typeof(DateTime) || PI.PropertyType == typeof(DateTime?)) Format = "MM/dd/yyyy";
                else if (PI.PropertyType == typeof(double) || PI.PropertyType == typeof(decimal) || PI.PropertyType == typeof(double?) || PI.PropertyType == typeof(decimal?)) Format = "#,##0.00";
                else if (PI.PropertyType == typeof(long) || PI.PropertyType == typeof(int) || PI.PropertyType == typeof(short) || PI.PropertyType == typeof(byte)) Format = "0";
                else Format = "@";
                Formats.Add(Format);
            }
            return AddWorksheetFromList<T>(SheetName, objList, Formats.ToArray(), formatAsTable);
        }

        public Workbooks.Worksheet AddWorksheetFromArray(string SheetName, List<string[]> Elements, string[] Headers, bool formatAsTable)
        {
            Workbooks.Worksheet xlSheet = new Workbooks.Worksheet((Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource.Add(missing, this._Worksheets.Resource[this._Worksheets.Resource.Count], missing, missing));
            try
            {
                xlSheet.Name = SheetName;
            }
            catch
            {
                bool t = false;
                int a = 1;
                while (t == false)
                {
                    try
                    {
                        xlSheet.Name = SheetName + " " + a.ToString();
                        t = true;
                    }
                    catch { }
                    a++;
                }
            }

            Workbooks.Worksheets.Range rng = xlSheet.GetRange("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (string header in Headers)
            {
                rng = xlSheet.GetRange("A1", missing).GetOffset(missing, i);
                rng.Value2 = header;
                i++;
            }

            for (int x = 0; x < Headers.Length; x++)
            {
                rng = xlSheet.GetRange(GetColumnName(x) + "1", missing);
                rng.Columns.EntireColumn.NumberFormat = "@";
            }


            rng = xlSheet.GetRange("A1", GetColumnName(Headers.Length - 1) + (Headers.Length + 1).ToString());
            if (formatAsTable) xlSheet.ListObjects.Add(Enums.ListObjectSourceType.Range, rng, null, Enums.YesNoGuess.Yes, "A1").Name = SheetName + " Table";

            i = 0;
            int ttl = Headers.Length;
            foreach (string[] row in Elements)
            {
                OnProgressUpdated(i * 100 / ttl);
                rng = xlSheet.GetRange("A2", GetColumnName(Headers.Length - 1) + "2").GetOffset(i, missing);
                rng.Value2 = row;
                i++;
            }

            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public void DeleteWorksheet(string name)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)this._Worksheets.Resource[name];
                
                // Should Check & Remove from Collection



                ws.Delete();
            }
            catch { }
        }

        public Application.Workbooks.PivotTable CreatePivotTable(Workbooks.Worksheet worksheet, string pivotName, string sourceName, bool columnStripes = false, bool rowStripes = false, string style = "")
        {
            Microsoft.Office.Interop.Excel.PivotTable PT = this._Workbook.Resource.PivotCaches().Create(XlPivotTableSourceType.xlDatabase, sourceName).CreatePivotTable(string.Format("'{0}'!R6C1", worksheet.Name), pivotName);
            PT.ShowTableStyleColumnStripes = columnStripes;
            PT.ShowTableStyleRowStripes = rowStripes;
            if (!string.IsNullOrEmpty(style))
                PT.TableStyle2 = style;

            return new Application.Workbooks.PivotTable(this._Workbook.Resource.Application, PT);
        }

        public void SaveAs(string fileName, Enums.FileFormat format)
        {
            this._Workbook.Resource.SaveAs(fileName, Dictionaries.XlFileFormatDictionary[format]);
        }

        public void Close(bool saveChanges, object fileName = null, bool? routeWorkbook = null)
        {
            this._Workbook.Resource?.Close(saveChanges, fileName, routeWorkbook);
        }

        #region IEnumerable<Worksheet>

        public IEnumerator<Workbooks.Worksheet> GetEnumerator()
        {
            return new WorksheetEnumerator(this.Worksheets);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                this._Workbook.Dispose();
                this._Worksheets.Dispose();
                foreach (var ws in Worksheets)
                {
                    ws.Dispose();
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WorksheetEnumerator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class WorksheetEnumerator : IEnumerator<Workbooks.Worksheet>
    {
        List<Workbooks.Worksheet> _Worksheets { get; set; }
        int position { get; set; } = -1;

        public Workbooks.Worksheet Current { get { return this._Worksheets[this.position]; } }
        object IEnumerator.Current { get { return this.Current; } }

        public WorksheetEnumerator(List<Workbooks.Worksheet> worksheets)
        {
            this._Worksheets = worksheets;
        }

        public bool MoveNext()
        {
            this.position++;
            if (this.position < this._Worksheets.Count)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            this.position = -1;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WorksheetEnumerator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
