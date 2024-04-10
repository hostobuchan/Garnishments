using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface
{
    public struct ConditionalFormat
    {
        public Enums.ConditionalFormattingType FormatType;
        public Enums.ConditionValueType ValueType;
        public bool DefinedMinMax;
        public double MinValue;
        public double MidValue;
        public double MaxValue;
        public System.Drawing.Color MinColor;
        public System.Drawing.Color MidColor;
        public System.Drawing.Color MaxColor;
        public int MinColor_Ole { get { return System.Drawing.ColorTranslator.ToOle(this.MinColor); } }
        public int MidColor_Ole { get { return System.Drawing.ColorTranslator.ToOle(this.MidColor); } }
        public int MaxColor_Ole { get { return System.Drawing.ColorTranslator.ToOle(this.MaxColor); } }

        public ConditionalFormat(Enums.ConditionalFormattingType FormatType, Enums.ConditionValueType ValueType = Enums.ConditionValueType.Percentile, bool DefinedMinMaxValue = false)
        {
            this.FormatType = FormatType;
            this.ValueType = ValueType;
            this.DefinedMinMax = DefinedMinMaxValue;
            this.MinValue = 50;
            this.MidValue = 75;
            this.MaxValue = 90;
            this.MinColor = System.Drawing.Color.Red;
            this.MidColor = System.Drawing.Color.Yellow;
            this.MaxColor = System.Drawing.ColorTranslator.FromOle(5296274);// System.Drawing.Color.Green;
        }
    }
}
