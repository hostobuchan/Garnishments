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
        private static Dictionary<ConsolidationFunction, Microsoft.Office.Interop.Excel.XlConsolidationFunction> _XlConsolidationFunctionDictionary;
        public static Dictionary<ConsolidationFunction, Microsoft.Office.Interop.Excel.XlConsolidationFunction> XlConsolidationFunctionDictionary { get { if (_XlConsolidationFunctionDictionary == null) LoadXlConsolidationFunctionDictionary(); return _XlConsolidationFunctionDictionary; } }

        private static void LoadXlConsolidationFunctionDictionary()
        {
            _XlConsolidationFunctionDictionary = new Dictionary<ConsolidationFunction, Microsoft.Office.Interop.Excel.XlConsolidationFunction>()
            {
                { ConsolidationFunction.Average , Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlAverage},
                { ConsolidationFunction.Count, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlCount},
                { ConsolidationFunction.CountNums, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlCountNums},
                { ConsolidationFunction.Max, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlMax},
                { ConsolidationFunction.Min, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlMin},
                { ConsolidationFunction.Product, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlProduct},
                { ConsolidationFunction.StDev, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlStDev},
                { ConsolidationFunction.StDevP, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlStDevP},
                { ConsolidationFunction.Sum, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlSum},
                { ConsolidationFunction.Unkown, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlUnknown},
                { ConsolidationFunction.Var, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlVar},
                { ConsolidationFunction.VarP, Microsoft.Office.Interop.Excel.XlConsolidationFunction.xlVarP}
            };
        }
    }
}
