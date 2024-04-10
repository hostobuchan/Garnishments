using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface
{
    static partial class Dictionaries
    {
        private static Dictionary<Enums.FileFormat, XlFileFormat> _XlFileFormatDictionary;
        public static Dictionary<Enums.FileFormat, XlFileFormat> XlFileFormatDictionary { get { if (_XlFileFormatDictionary == null) LoadXlFileFormatDictionary(); return _XlFileFormatDictionary; } }

        private static void LoadXlFileFormatDictionary()
        {
            _XlFileFormatDictionary = new Dictionary<Enums.FileFormat, XlFileFormat>()
            {
                { Enums.FileFormat.xls, XlFileFormat.xlExcel9795 },
                { Enums.FileFormat.xlsx, XlFileFormat.xlExcel8 },
                { Enums.FileFormat.txt_tab, XlFileFormat.xlTextMSDOS },
                { Enums.FileFormat.txt_csv, XlFileFormat.xlCSV }
            };
        }
    }
}
