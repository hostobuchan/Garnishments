using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ExcelInterface
{
    public class Excel : IDisposable
    {
        public event Action<int> ProgressUpdated;
        protected void OnProgressUpdated(int Progress) { this.ProgressUpdated?.Invoke(Progress); }

        private object missing = Type.Missing;
        private Microsoft.Office.Interop.Excel.Application xlApp;
        private bool CanClose;
        public Workbook xlBook { get; private set; }

        public Excel()
        {
            CanClose = true;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.StandardFont = "Arial";
            xlApp.StandardFontSize = 10;
            xlApp.WorkbookBeforeClose += new AppEvents_WorkbookBeforeCloseEventHandler(xlApp_WorkbookBeforeClose);
            xlBook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }
        public Excel(string FileLocation, bool StayOpen = false)
        {
            CanClose = !StayOpen;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.WorkbookBeforeClose += new AppEvents_WorkbookBeforeCloseEventHandler(xlApp_WorkbookBeforeClose);
            xlBook = xlApp.Workbooks.Open(FileLocation);
        }
        public Excel(System.IO.Stream FileStream, Enums.ExcelFormat Format, bool StayOpen = false)
        {
            CanClose = !StayOpen;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.WorkbookBeforeClose += new AppEvents_WorkbookBeforeCloseEventHandler(xlApp_WorkbookBeforeClose);
            string Path = System.IO.Path.GetTempFileName() + "." + Format.ToString();
            System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.Create);
            byte[] buffer = new byte[FileStream.Length];
            FileStream.Read(buffer, 0, buffer.Length);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
            xlBook = xlApp.Workbooks.Open(Path);
        }

        void xlApp_WorkbookBeforeClose(Microsoft.Office.Interop.Excel.Workbook Wb, ref bool Cancel)
        {
            if (!CanClose) Cancel = true;
        }

        public void RunMacro(string Macro)
        {
            xlApp.Run(Macro);
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetBlank(string SheetName, Color TabColor)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.Add(missing, xlBook.Worksheets[xlBook.Worksheets.Count], missing, missing);
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
            if (!TabColor.IsEmpty)
                xlSheet.Tab.Color = TabColor;
            return xlSheet;
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetFromTable(System.Data.DataTable dt)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.Add(missing, xlBook.Worksheets[xlBook.Worksheets.Count], missing, missing);
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

            Range rng = xlSheet.get_Range("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (DataColumn dc in dt.Columns)
            {
                rng = xlSheet.get_Range("A1", missing).get_Offset(missing, i);
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
                rng = xlSheet.get_Range("A2", getColumnName(dt.Columns.Count-1) + "2").get_Offset(i, missing);
                rng.Value2 = dr.ItemArray;
                i++;
            }
            rng = xlSheet.get_Range("A1", getColumnName(dt.Columns.Count - 1) + (dt.Rows.Count+1).ToString());
            xlSheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange, rng, null, XlYesNoGuess.xlYes, "A1").Name = dt.TableName + " Table";
            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetFromTable(System.Data.DataTable dt, string[] Formats)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.Add(missing, xlBook.Worksheets[xlBook.Worksheets.Count], missing, missing);
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

            Range rng = xlSheet.get_Range("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (DataColumn dc in dt.Columns)
            {
                rng = xlSheet.get_Range("A1", missing).get_Offset(missing, i);
                rng.Value2 = dc.ColumnName;
                i++;
            }

            for (int x = 0; x < Formats.Length; x++)
            {
                rng = xlSheet.get_Range(getColumnName(x) + "1", missing);
                rng.Columns.EntireColumn.NumberFormat = Formats[x];
            }


            rng = xlSheet.get_Range("A1", getColumnName(dt.Columns.Count - 1) + (dt.Rows.Count + 1).ToString());
            xlSheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange, rng, null, XlYesNoGuess.xlYes, "A1").Name = dt.TableName + " Table";

            i = 0;
            int ttl = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                OnProgressUpdated(i * 100 / ttl);
                rng = xlSheet.get_Range("A2", getColumnName(dt.Columns.Count - 1) + "2").get_Offset(i, missing);
                rng.Value2 = dr.ItemArray;
                i++;
            }

            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetFromList<T>(string SheetName, List<T> objList, string[] Formats)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.Add(missing, xlBook.Worksheets[xlBook.Worksheets.Count], missing, missing);
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

            Range rng = xlSheet.get_Range("A1", missing);

            IEnumerable<System.Reflection.PropertyInfo> Properties = typeof(T).GetProperties().Where(el => !el.PropertyType.IsArray && !(el.PropertyType.IsGenericType && (el.PropertyType.GetGenericTypeDefinition() == typeof(List<>) || el.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))));

            int i = 0;
            OnProgressUpdated(0);
            foreach (System.Reflection.PropertyInfo PI in Properties)
            {
                rng = xlSheet.get_Range("A1", missing).get_Offset(missing, i);
                rng.Value2 = PI.Name;
                rng.Columns.EntireColumn.NumberFormat = i <= Formats.GetUpperBound(0) ? Formats[i] : "@";

                i++;
            }

            int row = 0;
            int ttl = objList.Count;
            foreach (T obj in objList)
            {
                OnProgressUpdated(row * 100 / ttl);
                rng = xlSheet.get_Range("A2", getColumnName(Properties.Count() - 1) + "2").get_Offset(row, missing);
                object[] Arry = new object[Properties.Count()];

                i = 0;
                foreach (System.Reflection.PropertyInfo PI in Properties)
                {
                    try
                    {
                        if (PI.PropertyType == typeof(DateTime))
                        {
                            Arry[i] = ((DateTime)PI.GetValue(obj, null)).ToOADate();
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
                        else
                        {
                            Arry[i] = PI.GetValue(obj, null)?.ToString();
                        }
                    }
                    catch { }
                    i++;
                }
                rng.Value2 = Arry;
                row++;
            }
            rng = xlSheet.get_Range("A1", getColumnName(Properties.Count() - 1) + (objList.Count + 1).ToString());
            xlSheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange, rng, null, XlYesNoGuess.xlYes, "A1").Name = SheetName + " Table";

            xlSheet.Columns.ColumnWidth = 255;
            xlSheet.Columns.AutoFit();
            xlSheet.Rows.AutoFit();
            return xlSheet;
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetFromList<T>(string SheetName, List<T> objList)
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
            return AddWorksheetFromList<T>(SheetName, objList, Formats.ToArray());
        }

        public Microsoft.Office.Interop.Excel.Worksheet AddWorksheetFromArray(string SheetName, List<string[]> Elements, string[] Headers)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.Add(missing, xlBook.Worksheets[xlBook.Worksheets.Count], missing, missing);
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

            Range rng = xlSheet.get_Range("A1", missing);

            int i = 0;
            OnProgressUpdated(0);
            foreach (string header in Headers)
            {
                rng = xlSheet.get_Range("A1", missing).get_Offset(missing, i);
                rng.Value2 = header;
                i++;
            }

            for (int x = 0; x < Headers.Length; x++)
            {
                rng = xlSheet.get_Range(getColumnName(x) + "1", missing);
                rng.Columns.EntireColumn.NumberFormat = "@";
            }


            rng = xlSheet.get_Range("A1", getColumnName(Headers.Length - 1) + (Headers.Length + 1).ToString());
            xlSheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange, rng, null, XlYesNoGuess.xlYes, "A1").Name = SheetName + " Table";

            i = 0;
            int ttl = Headers.Length;
            foreach (string[] row in Elements)
            {
                OnProgressUpdated(i * 100 / ttl);
                rng = xlSheet.get_Range("A2", getColumnName(Headers.Length - 1) + "2").get_Offset(i, missing);
                rng.Value2 = row;
                i++;
            }

            xlSheet.Columns.AutoFit();
            return xlSheet;
        }

        public void AddHeatMapToTable(string SheetName, string TableName, string ColumnName)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[SheetName];
            ListObject xlObj = xlSheet.ListObjects[TableName];

            FormatConditions Conditions = xlObj.ListColumns[ColumnName].Range.FormatConditions;

            //FormatConditions Conditions = pf.DataRange.FormatConditions;
            ColorScale Scale = (ColorScale)Conditions.AddColorScale(3);

            Scale.ColorScaleCriteria[1].Type = XlConditionValueTypes.xlConditionValueLowestValue;
            Scale.ColorScaleCriteria[1].FormatColor.Color = 7039480;
            Scale.ColorScaleCriteria[1].FormatColor.TintAndShade = 0;
            Scale.ColorScaleCriteria[2].Type = XlConditionValueTypes.xlConditionValuePercentile;
            Scale.ColorScaleCriteria[2].Value = 50;
            Scale.ColorScaleCriteria[2].FormatColor.Color = 8711167;
            Scale.ColorScaleCriteria[2].FormatColor.TintAndShade = 0;
            Scale.ColorScaleCriteria[3].Type = XlConditionValueTypes.xlConditionValueHighestValue;
            Scale.ColorScaleCriteria[3].FormatColor.Color = 8109667;
            Scale.ColorScaleCriteria[3].FormatColor.TintAndShade = 0;
        }

        public void AddDataToWorkSheet(string WorkSheet, object Data, string DataFormat, string Cell, bool Bold = false)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[WorkSheet];

            Range rng = xlSheet.get_Range(Cell, missing);

            rng.Value2 = Data;

            try
            {
                rng.Font.Bold = Bold;
            }
            catch { }

            try
            {
                rng.NumberFormat = DataFormat;

                xlSheet.Columns.AutoFit();
            }
            catch { }
        }

        public void AddDataToWorkSheet(string WorkSheet, object[] Data, int StartColumn, int StartRow)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[WorkSheet];

            string StartCell = string.Format("{0}{1}", getColumnName(StartColumn), StartRow);
            string EndCell = string.Format("{0}{1}", getColumnName(StartColumn + Data.GetUpperBound(0)), StartRow);
            Range rng = xlSheet.get_Range(StartCell, EndCell);

            rng.Value2 = Data;

            try
            {
                xlSheet.Columns.AutoFit();
            }
            catch { }
        }

        public void AddTableColumn(string Sheetname, string TableName, string ColumnName, string Formula, string Format = "General")
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[Sheetname];

            ListObject xlTable = null;
            try
            {
                xlTable = xlSheet.ListObjects[TableName];
            }
            catch
            {
                xlTable = xlSheet.ListObjects[TableName.Replace(' ', '_')];
            }

            ListColumn xlColumn = xlTable.ListColumns.Add();

            xlColumn.Name = ColumnName;
            xlColumn.DataBodyRange.NumberFormat = "General";
            xlColumn.DataBodyRange.Formula = Formula;
            xlColumn.Range.EntireColumn.NumberFormat = Format;
            xlColumn.Range.Rows[1] = ColumnName;
            xlColumn.Range.EntireColumn.AutoFit();
        }
        /// <summary>
        /// Insert a Row Above Desired Row #
        /// </summary>
        /// <param name="Sheetname">Name of WorkSheet</param>
        /// <param name="Row">1-Based Index of Row to Insert Above</param>
        public void InsertRowAbove(string Sheetname, int Row)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[Sheetname];

            (xlSheet.Rows[Row] as Range).Insert(XlInsertShiftDirection.xlShiftDown);
        }

        public void HideWorkSheet(string Name, XlSheetVisibility Visibility)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Worksheet HideMe = (Microsoft.Office.Interop.Excel.Worksheet)this.xlApp.Worksheets[Name];
                HideMe.Visible = Visibility;
            }
            catch { }
        }

        public void SelectWorkSheet(string Name)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Sheets[Name];
            xlSheet.Select();
        }

        public object[,] ReadData()
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = ((Microsoft.Office.Interop.Excel.Worksheet)xlBook.ActiveSheet);
            Range range = xlSheet.UsedRange;
            return (object[,])range.Value2;
        }

        public IEnumerable<Dictionary<string, object>> ReadDictionary()
        {
            object[,] values = ReadData();
            string[] Dict = new string[values.GetUpperBound(1)];
            for (int i = 0; i < values.GetUpperBound(0); i++)
            {
                if (i == 0)
                {
                    for (int v = 0; v < values.GetUpperBound(1); v++)
                    {
                        Dict[v] = values[i + 1, v + 1]?.ToString();
                    }
                    continue;
                }

                Dictionary<string, object> temp = new Dictionary<string, object>();
                for (int v = 0; v < values.GetUpperBound(1); v++)
                {
                    if (Dict[v] != null)
                        temp.Add(Dict[v], values[i+1, v+1]);
                }
                yield return temp;
            }
        }

        public object[,] ReadData(string StartCell, string EndCell)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = ((Microsoft.Office.Interop.Excel.Worksheet)xlBook.ActiveSheet);
            Range range = xlSheet.get_Range(StartCell, EndCell);
            return (object[,])range.Value2;
        }

        public IEnumerable<object[]> ReadRows()
        {
            object[,] values = ReadData();

            OnProgressUpdated(0);
            int ttl = values.GetUpperBound(0);
            for (int i = 0; i < ttl; i++)
            {
                OnProgressUpdated(i * 100 / ttl);
                object[] Result = new object[values.GetUpperBound(1)];
                for (int o = 0; o < values.GetUpperBound(1); o++)
                {
                    Result[o] = values[i+1,o+1];
                }
                yield return Result;
            }
        }

        public IEnumerable<object[]> ReadRows(string StartCell, string EndCell)
        {
            object[,] values = ReadData(StartCell, EndCell);
            
            OnProgressUpdated(0);
            int ttl = values.GetUpperBound(0);
            for (int i = 0; i < values.GetUpperBound(0); i++)
            {
                OnProgressUpdated(i * 100 / ttl);
                object[] Result = new object[values.GetUpperBound(1)];
                for (int o = 0; o < values.GetUpperBound(1); o++)
                {
                    Result[o] = values[i + 1, o + 1];
                }
                yield return Result;
            }
        }

        public Color BackgroundColor(string Cell)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)this.xlApp.ActiveSheet;
            Range rng = xlSheet.get_Range(Cell);
            object clr = rng.Interior.Color;
            int col = 0;
            if (!int.TryParse(clr.ToString(), out col))
                return Color.White;
            return ColorTranslator.FromOle(col);
        }

        public void ShowWorkBook()
        {
            try
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets["Sheet1"];
                ws.Delete();
            }
            catch { }
            xlApp.Visible = true;
        }

        public void SaveWorkBook(string FileLocation)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets["Sheet1"];
                ws.Delete();
            }
            catch { }
            try
            {
                xlBook.SaveAs(FileLocation);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public void CloseExcel()
        {
            try
            {
                CanClose = true;
                xlBook.Close(false);
                xlApp.Quit();
                xlApp = null;
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
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets["Sheet1"];
                ws.Delete();
            }
            catch { }
            try
            {
                xlBook.Close(true, FileLocation);
                xlApp.Quit();
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

        public static string getColumnName(int Col)
        {
            string ColName = "";
            if (Col >= 26)
            {
                ColName = getColumnName((int)Math.Floor((float)Col / (float)26)-1);
            }
            Col = Col % 26;
            ColName += Convert.ToChar(Col + 65);
            return ColName;
        }
        /// <summary>
        /// Creates New Pivot Table
        /// </summary>
        /// <param name="SheetName">Name of Sheet Tab Created</param>
        /// <param name="PivotName">Name of Pivot Table</param>
        /// <param name="SourceName">Name of Source Data Table</param>
        /// <param name="ColumnStripes">Show Column Stripes</param>
        /// <param name="RowStripes">Show Row Stripes</param>
        /// <param name="Style">Pivot Table Style</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.PivotTable CreatePivotTable(string SheetName, string PivotName, string SourceName, bool ColumnStripes = false, bool RowStripes = false, string Style = "")
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)this.xlApp.Worksheets.Add();
            xlSheet.Name = SheetName;
            Microsoft.Office.Interop.Excel.PivotTable PT = this.xlApp.ActiveWorkbook.PivotCaches().Create(XlPivotTableSourceType.xlDatabase, SourceName).CreatePivotTable(string.Format("'{0}'!R6C1", SheetName), PivotName);
            PT.ShowTableStyleColumnStripes = ColumnStripes;
            PT.ShowTableStyleRowStripes = RowStripes;
            if (!string.IsNullOrEmpty(Style))
                PT.TableStyle2 = Style;
            return PT;
        }

        public Application.Workbooks.PivotTable CreatePivotTable2(string SheetName, string PivotName, string SourceName, bool ColumnStripes = false, bool RowStripes = false, string Style = "")
        {
            return new Application.Workbooks.PivotTable(this.xlApp, CreatePivotTable(SheetName, PivotName, SourceName, ColumnStripes, RowStripes, Style));
        }

        public void AddCalculatedField(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, string Definition, string Format)
        {
            PT.CalculatedFields().Add(Field, Definition).NumberFormat = Format;
        }

        public void SetPivotFilter(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, int Position, string NumberFormat = "")
        {
            PivotField pf = (PivotField)PT.PivotFields(Field);
            pf.Orientation = XlPivotFieldOrientation.xlPageField;
            pf.Position = Position;
            try
            {
                if (!string.IsNullOrEmpty(NumberFormat))
                    pf.NumberFormat = NumberFormat;
            }
            catch { }
        }

        public void SetPivotRow(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, int Position)
        {
            PivotField pf = (PivotField)PT.PivotFields(Field);
            pf.Orientation = XlPivotFieldOrientation.xlRowField;
            pf.Position = Position;
        }

        public void SetPivotColumnField(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, string Caption)
        {
            PivotField pf = (PivotField)PT.PivotFields(Field);
            pf.Orientation = XlPivotFieldOrientation.xlColumnField;
            pf.Caption = Caption;
        }

        public void SetPivotColumn(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, string Caption, XlConsolidationFunction Function, string NumberFormat = "0", bool HeatMap = false)
        {
            PivotField pf = (PivotField)PT.PivotFields(Field);
            pf.Orientation = XlPivotFieldOrientation.xlDataField;
            try
            {
                pf.Caption = Caption;
            }
            catch { }
            pf.Function = Function;
            try
            {
                pf.NumberFormat = NumberFormat;
            }
            catch { }
            if (HeatMap)
            {
                FormatConditions Conditions = pf.DataRange.FormatConditions;
                ColorScale Scale = (ColorScale)Conditions.AddColorScale(3);

                Scale.ColorScaleCriteria[1].Type = XlConditionValueTypes.xlConditionValueLowestValue;
                Scale.ColorScaleCriteria[1].FormatColor.Color = 7039480;
                Scale.ColorScaleCriteria[1].FormatColor.TintAndShade = 0;
                Scale.ColorScaleCriteria[2].Type = XlConditionValueTypes.xlConditionValuePercentile;
                Scale.ColorScaleCriteria[2].Value = 50;
                Scale.ColorScaleCriteria[2].FormatColor.Color = 8711167;
                Scale.ColorScaleCriteria[2].FormatColor.TintAndShade = 0;
                Scale.ColorScaleCriteria[3].Type = XlConditionValueTypes.xlConditionValueHighestValue;
                Scale.ColorScaleCriteria[3].FormatColor.Color = 8109667;
                Scale.ColorScaleCriteria[3].FormatColor.TintAndShade = 0;
            }
        }

        public void CollapseField(Microsoft.Office.Interop.Excel.PivotTable PT, string Field, bool Collapse = true)
        {
            ((PivotField)PT.PivotFields(Field)).ShowDetail = !Collapse;
        }

        public void Zoom(int Percent)
        {
            xlApp.ActiveWindow.Zoom = Percent;
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                CanClose = true;
                xlBook.Close(false);
            }
            catch { }
            try
            {
                xlApp?.Quit();
            }
            catch { }
            this.xlApp = null;
        }

        #endregion
    }
}
