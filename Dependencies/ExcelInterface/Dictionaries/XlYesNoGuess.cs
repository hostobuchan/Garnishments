using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace ExcelInterface
{
    static partial class Dictionaries
    {
        private static Dictionary<Enums.YesNoGuess, XlYesNoGuess> _XlYesNoGuessDictionary;
        public static Dictionary<Enums.YesNoGuess, XlYesNoGuess> XlYesNoGuessDictionary { get { if (_XlYesNoGuessDictionary == null) LoadXlYesNoGuessDictionary(); return _XlYesNoGuessDictionary; } }

        private static void LoadXlYesNoGuessDictionary()
        {
            _XlYesNoGuessDictionary = new Dictionary<Enums.YesNoGuess, XlYesNoGuess>()
            {
                { Enums.YesNoGuess.Yes, XlYesNoGuess.xlYes },
                { Enums.YesNoGuess.No, XlYesNoGuess.xlNo },
                { Enums.YesNoGuess.Guess, XlYesNoGuess.xlGuess }
            };
        }
    }
}
