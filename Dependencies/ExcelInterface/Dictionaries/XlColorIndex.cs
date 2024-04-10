using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using ExcelInterface.Enums;

namespace ExcelInterface
{
    static partial class Dictionaries
    {
        private static Dictionary<ColorIndex, Microsoft.Office.Interop.Excel.XlColorIndex> _XlColorIndexDictionary;
        public static Dictionary<ColorIndex, Microsoft.Office.Interop.Excel.XlColorIndex> XlColorIndexDictionary { get { if (_XlColorIndexDictionary == null) LoadXlColorIndexDictionary(); return _XlColorIndexDictionary; } }

        private static void LoadXlColorIndexDictionary()
        {
            _XlColorIndexDictionary = new Dictionary<ColorIndex, Microsoft.Office.Interop.Excel.XlColorIndex>()
            {
                { ColorIndex.ColorIndexNone, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone },
                { ColorIndex.ColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic }
            };
        }
    }
}
