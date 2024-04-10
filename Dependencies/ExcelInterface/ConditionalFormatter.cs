using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using ExcelInterface.Enums;

namespace ExcelInterface
{
    static class ConditionalFormatter
    {
        public static void Format(Range rng, ConditionalFormat format)
        {
            Microsoft.Office.Interop.Excel.FormatConditions Conditions = rng.FormatConditions;
            Microsoft.Office.Interop.Excel.ColorScale Scale;
            FormatCondition condition;
            switch (format.FormatType)
            {
                case ConditionalFormattingType.None:
                    break;
                case ConditionalFormattingType.TwoColor:
                    condition = (FormatCondition)Conditions.Add(Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlLess, format.MinValue);
                    condition.Interior.PatternColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    condition.Interior.Color = format.MinColor_Ole;
                    condition.Interior.TintAndShade = 0;

                    condition = (FormatCondition)Conditions.Add(Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreaterEqual, format.MaxValue);
                    condition.Interior.PatternColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    condition.Interior.Color = format.MaxColor_Ole;
                    condition.Interior.TintAndShade = 0;
                    break;
                case ConditionalFormattingType.ThreeColor:
                    condition = (FormatCondition)Conditions.Add(Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlLess, format.MinValue);
                    condition.Interior.PatternColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    condition.Interior.Color = format.MinColor_Ole;
                    condition.Interior.TintAndShade = 0;

                    condition = (FormatCondition)Conditions.Add(Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween, format.MinValue, format.MaxValue);
                    condition.Interior.PatternColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    condition.Interior.Color = format.MidColor_Ole;
                    condition.Interior.TintAndShade = 0;

                    condition = (FormatCondition)Conditions.Add(Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue, Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlGreaterEqual, format.MaxValue);
                    condition.Interior.PatternColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    condition.Interior.Color = format.MaxColor_Ole;
                    condition.Interior.TintAndShade = 0;
                    break;
                case ConditionalFormattingType.TwoColor_Scale:
                    Scale = (Microsoft.Office.Interop.Excel.ColorScale)Conditions.AddColorScale(2);

                    Scale.ColorScaleCriteria[1].Type = format.DefinedMinMax ? Dictionaries.XlConditionValueTypeDictionary[format.ValueType] : Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueLowestValue;
                    Scale.ColorScaleCriteria[1].Value = format.MinValue;
                    Scale.ColorScaleCriteria[1].FormatColor.Color = format.MinColor_Ole;
                    Scale.ColorScaleCriteria[1].FormatColor.TintAndShade = 0;
                    Scale.ColorScaleCriteria[2].Type = format.DefinedMinMax ? Dictionaries.XlConditionValueTypeDictionary[format.ValueType] : Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueHighestValue;
                    Scale.ColorScaleCriteria[2].Value = format.MaxValue;
                    Scale.ColorScaleCriteria[2].FormatColor.Color = format.MaxColor_Ole;
                    Scale.ColorScaleCriteria[2].FormatColor.TintAndShade = 0;
                    break;
                case ConditionalFormattingType.ThreeColor_Scale:
                    Scale = (Microsoft.Office.Interop.Excel.ColorScale)Conditions.AddColorScale(3);

                    Scale.ColorScaleCriteria[1].Type = format.DefinedMinMax ? Dictionaries.XlConditionValueTypeDictionary[format.ValueType] : Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueLowestValue;
                    Scale.ColorScaleCriteria[1].Value = format.MinValue;
                    Scale.ColorScaleCriteria[1].FormatColor.Color = format.MinColor_Ole;
                    Scale.ColorScaleCriteria[1].FormatColor.TintAndShade = 0;
                    Scale.ColorScaleCriteria[2].Type = Dictionaries.XlConditionValueTypeDictionary[format.ValueType];
                    Scale.ColorScaleCriteria[2].Value = format.MidValue;
                    Scale.ColorScaleCriteria[2].FormatColor.Color = format.MidColor_Ole;
                    Scale.ColorScaleCriteria[2].FormatColor.TintAndShade = 0;
                    Scale.ColorScaleCriteria[3].Type = format.DefinedMinMax ? Dictionaries.XlConditionValueTypeDictionary[format.ValueType] : Microsoft.Office.Interop.Excel.XlConditionValueTypes.xlConditionValueHighestValue;
                    Scale.ColorScaleCriteria[3].Value = format.MaxValue;
                    Scale.ColorScaleCriteria[3].FormatColor.Color = format.MaxColor_Ole;
                    Scale.ColorScaleCriteria[3].FormatColor.TintAndShade = 0;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
