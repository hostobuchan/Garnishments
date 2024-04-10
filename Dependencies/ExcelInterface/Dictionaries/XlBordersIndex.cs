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
        private static Dictionary<BordersIndex, Microsoft.Office.Interop.Excel.XlBordersIndex> _XlBordersIndexDictionary;
        public static Dictionary<BordersIndex, Microsoft.Office.Interop.Excel.XlBordersIndex> XlBordersIndexDictionary { get { if (_XlBordersIndexDictionary == null) LoadXlBordersIndexDictionary(); return _XlBordersIndexDictionary; } }

        private static void LoadXlBordersIndexDictionary()
        {
            _XlBordersIndexDictionary = new Dictionary<BordersIndex, Microsoft.Office.Interop.Excel.XlBordersIndex>()
            {
                { BordersIndex.DiagonalDown, Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown },
                { BordersIndex.DiagonalUp, Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp },
                { BordersIndex.EdgeBottom, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom },
                { BordersIndex.EdgeLeft, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft },
                { BordersIndex.EdgeRight, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight },
                { BordersIndex.EdgeTop, Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop },
                { BordersIndex.InsideHorizontal, Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal },
                { BordersIndex.InsideVertical, Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical }
            };
        }
    }
}
