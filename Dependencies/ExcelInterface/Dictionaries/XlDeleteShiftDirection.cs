using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;

namespace ExcelInterface
{
    public static partial class Dictionaries
    {
        private static Dictionary<Enums.DeleteShiftDirection, Microsoft.Office.Interop.Excel.XlDeleteShiftDirection> _XlDeleteShiftDirectionDictionary;
        public static Dictionary<Enums.DeleteShiftDirection, Microsoft.Office.Interop.Excel.XlDeleteShiftDirection> XlDeleteShiftDirectionDictionary { get { if (_XlDeleteShiftDirectionDictionary == null) LoadXlDeleteShiftDirectionDictionary(); return _XlDeleteShiftDirectionDictionary; } }

        private static void LoadXlDeleteShiftDirectionDictionary()
        {
            _XlDeleteShiftDirectionDictionary = new Dictionary<Enums.DeleteShiftDirection, Microsoft.Office.Interop.Excel.XlDeleteShiftDirection>()
            {
                { Enums.DeleteShiftDirection.ShiftLeft, Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftToLeft },
                { Enums.DeleteShiftDirection.ShiftUp, Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp }
            };
        }
    }
}
