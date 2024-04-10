using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;

namespace ExcelInterface.Application.Workbooks.Worksheets
{ 
    public class ListObjects : IDisposable
    {
        private AutoCleanup<Microsoft.Office.Interop.Excel.ListObjects> _ListObjects;

        internal ListObjects(Microsoft.Office.Interop.Excel.ListObjects listObjects)
        {
            this._ListObjects = new AutoCleanup<Microsoft.Office.Interop.Excel.ListObjects>(listObjects);
        }

        public ListObject Add(Enums.ListObjectSourceType srcType, Range src, object linkSrc, Enums.YesNoGuess hasHeaders, object destination)
        {
            return new ListObject(this._ListObjects.Resource.Add(Dictionaries.XlListObjectSourceTypeDictionary[srcType], src._Range.Resource, linkSrc ?? Type.Missing, Dictionaries.XlYesNoGuessDictionary[hasHeaders], destination));
        }

        public void Dispose()
        {
            this._ListObjects.Dispose();
        }
    }

    public class ListObject : IDisposable
    {
        private AutoCleanup<Microsoft.Office.Interop.Excel.ListObject> _ListObject;

        public string Name { get { return this._ListObject.Resource.Name; } set { this._ListObject.Resource.Name = value; } }

        internal ListObject(Microsoft.Office.Interop.Excel.ListObject listObject)
        {
            this._ListObject = new AutoCleanup<Microsoft.Office.Interop.Excel.ListObject>(listObject);
        }

        public void Dispose()
        {
            this._ListObject.Dispose();
        }
    }
}
