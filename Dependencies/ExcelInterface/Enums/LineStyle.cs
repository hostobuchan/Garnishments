using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelInterface.Enums
{
    public enum LineStyle
    {
        /// <summary>
        ///     No line.
        /// </summary>
        LineStyleNone = -4142,
        /// <summary>
        ///     Double line.
        /// </summary>
        Double = -4119,
        /// <summary>
        ///     Dotted line.
        /// </summary>
        Dot = -4118,
        /// <summary>
        ///     Dashed line.
        /// </summary>
        Dash = -4115,
        /// <summary>
        ///     Continuous line.
        /// </summary>
        Continuous = 1,
        /// <summary>
        ///     Alternating dashes and dots.
        /// </summary>
        DashDot = 4,
        /// <summary>
        ///     Dash followed by two dots.
        /// </summary>
        DashDotDot = 5,
        /// <summary>
        ///     Slanted dashes.
        /// </summary>
        SlantDashDot = 13
    }
}
