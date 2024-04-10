using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ExcelInterface.Application.Workbooks.Worksheets
{
    public class Range : IDisposable
    {
        internal AutoCleanup<Microsoft.Office.Interop.Excel.Range> _Range;

        public object Value2 { get { return this._Range.Resource.Value2; } set { this._Range.Resource.Value2 = value; } }
        public object NumberFormat { get { return this._Range.Resource.NumberFormat; } set { this._Range.Resource.NumberFormat = value; } }
        public object Formula { get { return this._Range.Resource.Formula; }set { this._Range.Resource.Formula = value; } }
        public int ColumnWidth { get { return Convert.ToInt32(this._Range.Resource.ColumnWidth); } set { this._Range.Resource.ColumnWidth = value; } }
        public Color BackgroundColor { get { return ColorTranslator.FromOle(Convert.ToInt32(this._Range.Resource.Interior.Color)); } set { this._Range.Resource.Interior.Color = ColorTranslator.ToOle(value); } }
        public Borders Borders { get { return new Borders(this._Range.Resource.Borders); } }

        public Range Columns { get { return new Range(this._Range.Resource.Columns); } }
        public Range Rows { get { return new Range(this._Range.Resource.Rows); } }
        public Range EntireColumn { get { return new Range(this._Range.Resource.EntireColumn); } }
        public Range EntireRow { get { return new Range(this._Range.Resource.EntireRow); } }

        internal Range(Microsoft.Office.Interop.Excel.Range range)
        {
            this._Range = new Application.AutoCleanup<Microsoft.Office.Interop.Excel.Range>(range);
        }

        public Range GetOffset(object rowOffset, object columnOffset)
        {
            return new Range(this._Range.Resource.get_Offset(rowOffset, columnOffset));
        }

        public void Delete(Enums.DeleteShiftDirection direction)
        {
            this._Range.Resource.Delete(Dictionaries.XlDeleteShiftDirectionDictionary[direction]);
        }

        public void AutoFit()
        {
            this._Range.Resource.AutoFit();
        }

        public void Dispose()
        {
            this._Range.Dispose();
        }
    }
}
