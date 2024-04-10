using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ExcelInterface.Application.Workbooks
{
    public class Worksheet : IDisposable
    {
        public event Action<int> ProgressUpdated;
        protected void OnProgressUpdated(int progress) { this.ProgressUpdated?.Invoke(progress); }

        private AutoCleanup<Microsoft.Office.Interop.Excel.Worksheet> _Worksheet;
        private object missing { get { return Type.Missing; } }
        
        public string Name { get { return this._Worksheet.Resource.Name; } set { this._Worksheet.Resource.Name = value; } }
        public Color TabColor { get { return ColorTranslator.FromOle(Convert.ToInt32(this._Worksheet.Resource.Tab.Color)); } set { this._Worksheet.Resource.Tab.Color = ColorTranslator.ToOle(value); } }
        public Worksheets.Range Columns { get { return new Worksheets.Range(this._Worksheet.Resource.Columns); } }
        public Worksheets.Range Rows { get { return new Worksheets.Range(this._Worksheet.Resource.Rows); } }
        public Worksheets.ListObjects ListObjects { get { return new Worksheets.ListObjects(this._Worksheet.Resource.ListObjects); } }

        internal Worksheet(Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            this._Worksheet = new AutoCleanup<Microsoft.Office.Interop.Excel.Worksheet>(worksheet);
        }


        public void AddHeatMapToTable(string TableName, string ColumnName)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<ListObjects> xlObjs = new AutoCleanup<Microsoft.Office.Interop.Excel.ListObjects>(xlSheet.ListObjects);
            AutoCleanup<ListObject> xlObj = new AutoCleanup<ListObject>(xlObjs.Resource[TableName]);
            AutoCleanup<ListColumns> xlColumns = new AutoCleanup<ListColumns>(xlObj.Resource.ListColumns);
            AutoCleanup<ListColumn> xlColumn = new AutoCleanup<ListColumn>(xlColumns.Resource[ColumnName]);
            AutoCleanup<Range> xlRange = new AutoCleanup<Range>(xlColumn.Resource.Range);

            FormatConditions Conditions = xlRange.Resource.FormatConditions;

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
            
            xlObjs.Dispose();
            xlObj.Dispose();
            xlColumns.Dispose();
            xlColumn.Dispose();
            xlRange.Dispose();
        }

        public void AddDataToWorkSheet(object Data, string DataFormat, string Cell, bool Bold = false)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<Range> rng = new AutoCleanup<Range>(xlSheet.get_Range(Cell, missing));
            
            rng.Resource.Value2 = Data;

            try
            {
                rng.Resource.Font.Bold = Bold;
            }
            catch { }

            try
            {
                rng.Resource.NumberFormat = DataFormat;

                xlSheet.Columns.WithComCleanup().Resource.AutoFit();
            }
            catch { }

            try
            {
                rng.Dispose();
            }
            catch { }
        }

        public void AddDataToWorkSheet(object[] Data, string[] DataFormat, int StartColumn, int StartRow)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;

            string StartCell = string.Format("{0}{1}", getColumnName(StartColumn), StartRow);
            string EndCell = string.Format("{0}{1}", getColumnName(StartColumn + Data.GetUpperBound(0)), StartRow);
            AutoCleanup<Range> rng = new AutoCleanup<Range>(xlSheet.get_Range(StartCell, EndCell));

            if (DataFormat == null)
            {
                DataFormat = new string[Data.Length];
                for (int i = 0; i < Data.Length; i++)
                {
                    string Format = "";
                    if (Data[i].GetType().Name == "DateTime") Format = "MM/dd/yyyy";
                    if (Data[i].GetType().Name == "String") Format = "@";
                    if (Data[i].GetType().Name == "Double") Format = "0.00";
                    DataFormat[i] = Format;
                }
            }

            rng.Resource.NumberFormat = DataFormat;

            rng.Resource.Value2 = Data;

            try
            {
                xlSheet.Columns.WithComCleanup().Resource.AutoFit();
            }
            catch { }

            try
            {
                rng.Dispose();
            }
            catch { }
        }

        public void AddTableColumn(string TableName, string ColumnName, string Formula, string Format = "General")
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<ListObjects> xlObjs = new AutoCleanup<Microsoft.Office.Interop.Excel.ListObjects>(xlSheet.ListObjects);
            AutoCleanup<ListObject> xlTable = null;
            try
            {
                xlTable = new AutoCleanup<ListObject>(xlObjs.Resource[TableName]);
            }
            catch
            {
                xlTable = new AutoCleanup<ListObject>(xlObjs.Resource[TableName.Replace(' ', '_')]);
            }

            AutoCleanup<ListColumns> xlColumns = new AutoCleanup<ListColumns>(xlTable.Resource.ListColumns);
            AutoCleanup<ListColumn> xlColumn = new AutoCleanup<ListColumn>(xlColumns.Resource.Add());

            xlColumn.Resource.Name = ColumnName;
            xlColumn.Resource.DataBodyRange.NumberFormat = "General";
            xlColumn.Resource.DataBodyRange.Formula = Formula;
            xlColumn.Resource.Range.EntireColumn.NumberFormat = Format;
            xlColumn.Resource.Range.Rows[1] = ColumnName;
            xlColumn.Resource.Range.EntireColumn.AutoFit();

            xlObjs.Dispose();
            xlTable.Dispose();
            xlColumns.Dispose();
            xlColumn.Dispose();
        }
        /// <summary>
        /// Insert a Row Above Desired Row #
        /// </summary>
        /// <param name="Sheetname">Name of WorkSheet</param>
        /// <param name="Row">1-Based Index of Row to Insert Above</param>
        public void InsertRowAbove(int Row)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<Range> row = new AutoCleanup<Range>(xlSheet.Rows[Row] as Range);

            row.Resource.Insert(XlInsertShiftDirection.xlShiftDown);

            row.Dispose();
        }

        public void HideWorkSheet(XlSheetVisibility Visibility)
        {
            try
            {
                this._Worksheet.Resource.Visible = Visibility;
            }
            catch { }
        }

        public object[,] ReadData()
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<Range> range = new AutoCleanup<Range>(xlSheet.UsedRange);
            return (object[,])range.Resource.Value2;
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
                        string entry = values[i + 1, v + 1]?.ToString();
                        string baseEntry = entry;
                        int cnt = 0;
                        while (Dict.Contains(entry))
                        {
                            cnt++;
                            entry = $"{baseEntry}_{cnt}";
                        }
                        Dict[v] = entry;
                    }
                    continue;
                }

                Dictionary<string, object> temp = new Dictionary<string, object>();
                for (int v = 0; v < values.GetUpperBound(1); v++)
                {
                    if (Dict[v] != null)
                        temp.Add(Dict[v], values[i + 1, v + 1]);
                }
                yield return temp;
            }
        }

        public object[,] ReadData(string StartCell, string EndCell)
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<Range> range = new AutoCleanup<Range>(xlSheet.get_Range(StartCell, EndCell));
            return (object[,])range.Resource.Value2;
        }

        public IEnumerable<object[]> ReadRows()
        {
            object[,] values = ReadData();

            OnProgressUpdated(0);
            int ttl = values?.GetUpperBound(0) ?? 0;
            for (int i = 0; i < ttl; i++)
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

        public IEnumerable<object[]> ReadRows(string StartCell, string EndCell)
        {
            object[,] values = ReadData(StartCell, EndCell);

            OnProgressUpdated(0);
            int ttl = values?.GetUpperBound(0) ?? 0;
            for (int i = 0; i < ttl; i++)
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
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = this._Worksheet.Resource;
            AutoCleanup<Range> rng = xlSheet.get_Range(Cell).WithComCleanup();
            object clr = rng.Resource.Interior.WithComCleanup().Resource.Color;
            int col = 0;
            if (!int.TryParse(clr.ToString(), out col))
                return Color.White;
            return ColorTranslator.FromOle(col);
        }

        public static string getColumnName(int Col)
        {
            string ColName = "";
            if (Col >= 26)
            {
                ColName = getColumnName((int)Math.Floor((float)Col / (float)26) - 1);
            }
            Col = Col % 26;
            ColName += Convert.ToChar(Col + 65);
            return ColName;
        }

        public Worksheets.Range GetRange(object startCell, object endCell)
        {
            return new Worksheets.Range(this._Worksheet.Resource.get_Range(startCell, endCell));
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this._Worksheet.Dispose();
                this.Columns.Dispose();
                this.Rows.Dispose();
                this.ListObjects.Dispose();

                this.ProgressUpdated = null;
                this._Worksheet = null;
            }
            catch { }
        }

        #endregion

    }
}
