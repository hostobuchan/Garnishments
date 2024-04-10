using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface.Application.Workbooks.Worksheets
{
    public class Borders : IDisposable
    {
        internal AutoCleanup<Microsoft.Office.Interop.Excel.Borders> _Borders;

        public Border this[Enums.BordersIndex index] { get { return new Border(this._Borders.Resource.Item[Dictionaries.XlBordersIndexDictionary[index]]); } }
        
        internal Borders(Microsoft.Office.Interop.Excel.Borders borders)
        {
            this._Borders = new Application.AutoCleanup<Microsoft.Office.Interop.Excel.Borders>(borders);
        }

        public void Dispose()
        {
            this._Borders.Dispose();
        }
    }
}
