using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ExcelInterface
{
    static partial class Dictionaries
    {
        private static Dictionary<Enums.ListObjectSourceType, XlListObjectSourceType> _XlListObjectSourceTypeDictionary;
        public static Dictionary<Enums.ListObjectSourceType, XlListObjectSourceType> XlListObjectSourceTypeDictionary { get { if (_XlListObjectSourceTypeDictionary == null) LoadXlListObjectSourceTypeDictionary(); return _XlListObjectSourceTypeDictionary; } }

        private static void LoadXlListObjectSourceTypeDictionary()
        {
            _XlListObjectSourceTypeDictionary = new Dictionary<Enums.ListObjectSourceType, XlListObjectSourceType>()
            {
                { Enums.ListObjectSourceType.External,  XlListObjectSourceType.xlSrcExternal },
                { Enums.ListObjectSourceType.Query,  XlListObjectSourceType.xlSrcQuery },
                { Enums.ListObjectSourceType.Range,  XlListObjectSourceType.xlSrcRange },
                { Enums.ListObjectSourceType.Xml,  XlListObjectSourceType.xlSrcXml }
            };
        }
    }
}
