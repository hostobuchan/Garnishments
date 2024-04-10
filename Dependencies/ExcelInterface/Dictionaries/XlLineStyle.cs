using ExcelInterface.Enums;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;

namespace ExcelInterface
{
    static partial class Dictionaries
    {
        private static Dictionary<LineStyle, Microsoft.Office.Interop.Excel.XlLineStyle> _XlLineStyleDictionary;
        public static Dictionary<LineStyle, Microsoft.Office.Interop.Excel.XlLineStyle> XlLineStyleDictionary { get { if (_XlLineStyleDictionary == null) LoadXlLineStyleDictionary(); return _XlLineStyleDictionary; } }

        private static void LoadXlLineStyleDictionary()
        {
            _XlLineStyleDictionary = new Dictionary<LineStyle, Microsoft.Office.Interop.Excel.XlLineStyle>()
            {
                { LineStyle.Continuous, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous },
                { LineStyle.Dash, Microsoft.Office.Interop.Excel.XlLineStyle.xlDash },
                { LineStyle.DashDot, Microsoft.Office.Interop.Excel.XlLineStyle.xlDashDot },
                { LineStyle.DashDotDot, Microsoft.Office.Interop.Excel.XlLineStyle.xlDashDotDot },
                { LineStyle.Dot, Microsoft.Office.Interop.Excel.XlLineStyle.xlDot },
                { LineStyle.Double, Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble },
                { LineStyle.LineStyleNone, Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone },
                { LineStyle.SlantDashDot, Microsoft.Office.Interop.Excel.XlLineStyle.xlSlantDashDot }
            };
        }
    }
}
