using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface
{
    public static class Utilities
    {
        public static string GetColumnName(int column)
        {
            string ColName = "";
            if (column >= 26)
            {
                ColName = GetColumnName((int)Math.Floor((float)column / (float)26) - 1);
            }
            column = column % 26;
            ColName += Convert.ToChar(column + 65);
            return ColName;
        }
    }
}
