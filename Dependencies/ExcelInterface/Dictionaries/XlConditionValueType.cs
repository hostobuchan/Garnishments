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
        private static Dictionary<ConditionValueType, Microsoft.Office.Interop.Excel.XlConditionValueTypes> _XlConditionValueTypeDictionary;
        public static Dictionary<ConditionValueType, Microsoft.Office.Interop.Excel.XlConditionValueTypes> XlConditionValueTypeDictionary { get { if (_XlConditionValueTypeDictionary == null) LoadXlConditionValueTypeDictionary(); return _XlConditionValueTypeDictionary; } }

        private static void LoadXlConditionValueTypeDictionary()
        {
            _XlConditionValueTypeDictionary = new Dictionary<ConditionValueType, Microsoft.Office.Interop.Excel.XlConditionValueTypes>()
            {
                { ConditionValueType.Formula, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueFormula },
                { ConditionValueType.HighestValue, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueHighestValue },
                { ConditionValueType.LowestValue, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueLowestValue },
                { ConditionValueType.None, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueNone },
                { ConditionValueType. Number, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueNumber },
                { ConditionValueType.Percent, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValuePercent },
                { ConditionValueType.Percentile, Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValuePercentile }
            };
        }
    }
}
