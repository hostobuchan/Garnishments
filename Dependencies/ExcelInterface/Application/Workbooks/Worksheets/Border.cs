using ExcelInterface.Enums;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ExcelInterface.Application.Workbooks.Worksheets
{
    public class Border : IDisposable
    {
        internal AutoCleanup<Microsoft.Office.Interop.Excel.Border> _Border;

        public LineStyle LineStyle { get { return Dictionaries.XlLineStyleDictionary.FirstOrDefault(kp => kp.Value == (Microsoft.Office.Interop.Excel.XlLineStyle)Enum.ToObject(typeof(Microsoft.Office.Interop.Excel.XlLineStyle), this._Border.Resource.LineStyle)).Key; } set { this._Border.Resource.LineStyle = Dictionaries.XlLineStyleDictionary[value]; } }
        public int Weight { get { return Convert.ToInt32(this._Border.Resource.Weight); } set { this._Border.Resource.Weight = value; } }
        public Color Color { get { return ColorTranslator.FromOle(Convert.ToInt32(this._Border.Resource.Color)); } set { this._Border.Resource.Color = ColorTranslator.ToOle(value); } }
        public ColorIndex ColorIndex { get { return Dictionaries.XlColorIndexDictionary.FirstOrDefault(kp => kp.Value == (Microsoft.Office.Interop.Excel.XlColorIndex)Enum.ToObject(typeof(Microsoft.Office.Interop.Excel.XlColorIndex), this._Border.Resource.ColorIndex)).Key; } set { this._Border.Resource.LineStyle = Dictionaries.XlColorIndexDictionary[value]; } }


        internal Border(Microsoft.Office.Interop.Excel.Border border)
        {
            this._Border = new Application.AutoCleanup<Microsoft.Office.Interop.Excel.Border>(border);
        }



        public void Dispose()
        {
            this._Border.Dispose();
        }
    }
}
