using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;


namespace ExcelInterface.Application.Workbooks
{
    public class PivotTable : IDisposable
    {
        private Microsoft.Office.Interop.Excel.Application xlApp { get; set; }
        private AutoCleanup<Microsoft.Office.Interop.Excel.PivotTable> _PTable { get; set; }
        private Microsoft.Office.Interop.Excel.PivotTable PTable { get { return _PTable.Resource; }set { _PTable = new AutoCleanup<Microsoft.Office.Interop.Excel.PivotTable>(value); } }

        public bool EnableFieldList { get { return this.PTable.EnableFieldList; } set { this.PTable.EnableFieldList = value; } }
        public string CompactLayoutRowHeader { get { return this.PTable.CompactLayoutRowHeader; } set { this.PTable.CompactLayoutRowHeader = value; } }
        public bool DisplayErrorString { get { return this.PTable.DisplayErrorString; } set { this.PTable.DisplayErrorString = value; } }
        public string ErrorString { get { return this.PTable.ErrorString; } set { this.PTable.ErrorString = value; } }
        public bool ColumnStripes { get { return this.PTable.ShowTableStyleColumnStripes; } set { this.PTable.ShowTableStyleColumnStripes = value; } }
        public bool RowStripes { get { return this.PTable.ShowTableStyleRowStripes; } set { this.PTable.ShowTableStyleRowStripes = value; } }

        internal protected PivotTable(Microsoft.Office.Interop.Excel.Application xlApp, Microsoft.Office.Interop.Excel.PivotTable PTable)
        {
            this.xlApp = xlApp;
            this.PTable = PTable;
        }

        internal static PivotTable CreatePivotTable(Microsoft.Office.Interop.Excel.Application xlApp, string SheetName, string PivotName, string SourceName, bool ColumnStripes = false, bool RowStripes = false, string Style = "")
        {
            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlApp.Worksheets.Add();
            xlSheet.Name = SheetName;
            Microsoft.Office.Interop.Excel.PivotTable PT = xlApp.ActiveWorkbook.PivotCaches().Create(Microsoft.Office.Interop.Excel.XlPivotTableSourceType.xlDatabase, SourceName).CreatePivotTable(string.Format("{0}!R6C1", SheetName), PivotName);
            PT.ShowTableStyleColumnStripes = ColumnStripes;
            PT.ShowTableStyleRowStripes = RowStripes;
            if (!string.IsNullOrEmpty(Style))
                PT.TableStyle2 = Style;
            return new PivotTable(xlApp, PT);
        }

        public void AddCalculatedField(string Field, string Definition, string Format)
        {
            this.PTable.CalculatedFields().Add(Field, Definition).NumberFormat = Format;
        }

        public void SetPivotFilter(string Field, int Position, string NumberFormat = "")
        {
            Microsoft.Office.Interop.Excel.PivotField pf = (Microsoft.Office.Interop.Excel.PivotField)this.PTable.PivotFields(Field);
            pf.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlPageField;
            pf.Position = Position;
            try
            {
                if (!string.IsNullOrEmpty(NumberFormat))
                    pf.NumberFormat = NumberFormat;
            }
            catch { }
        }

        public void SetPivotRow(string Field, int Position)
        {
            Microsoft.Office.Interop.Excel.PivotField pf = (Microsoft.Office.Interop.Excel.PivotField)this.PTable.PivotFields(Field);
            pf.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlRowField;
            pf.Position = Position;
        }

        public void SetPivotColumnField(string Field, string Caption)
        {
            Microsoft.Office.Interop.Excel.PivotField pf = (Microsoft.Office.Interop.Excel.PivotField)this.PTable.PivotFields(Field);
            pf.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlColumnField;
            pf.Caption = Caption;
        }

        public void SetPivotColumn(string Field, string Caption, Enums.ConsolidationFunction Function, string NumberFormat = "0", ConditionalFormat Formatting = default(ConditionalFormat))
        {
            Microsoft.Office.Interop.Excel.PivotField pf = (Microsoft.Office.Interop.Excel.PivotField)this.PTable.PivotFields(Field);
            pf.Orientation = Microsoft.Office.Interop.Excel.XlPivotFieldOrientation.xlDataField;
            try
            {
                pf.Caption = Caption;
            }
            catch { }
            pf.Function = Dictionaries.XlConsolidationFunctionDictionary[Function];
            try
            {
                pf.NumberFormat = NumberFormat;
            }
            catch { }
            ConditionalFormatter.Format(pf.DataRange, Formatting);
        }

        public void CollapseField(string Field, bool Collapse = true)
        {
            ((Microsoft.Office.Interop.Excel.PivotField)this.PTable.PivotFields(Field)).ShowDetail = !Collapse;
        }

        public void DisableValuesRow()
        {
            //Microsoft.Vbe.Interop.VBProject p = this.xlApp.ActiveWorkbook.VBProject; // Disabled by individual's trust center settings
            //Microsoft.Vbe.Interop.VBComponent c = p.VBComponents.Add(Microsoft.Vbe.Interop.vbext_ComponentType.vbext_ct_StdModule);
            //c.CodeModule.DeleteLines(0, c.CodeModule.CountOfLines);
            //c.CodeModule.AddFromString("Public Sub RemoveValuesRow()\nActiveSheet.PivotTables(\"" + this.PTable.Name + "\").ShowValuesRow = False\nEnd Sub");
            //this.xlApp.Run("RemoveValuesRow");
            //p.VBComponents.Remove(c);
        }

        public void Dispose()
        {
            this._PTable.Dispose();
        }
    }
}
