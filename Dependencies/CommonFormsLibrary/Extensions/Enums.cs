using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        public static IEnumerable<int> GetAllSetFlags<T>(T flags) where T : struct
        {
            int value = (int)Convert.ChangeType(flags, typeof(int));
            return Enum.GetValues(flags.GetType()).Cast<int>().Where(f => (f & value) == f && f != 0);
        }
        public static int GetMaxSetFlagValue<T>(T flags) where T : struct
        {
            IEnumerable<int> setValues = GetAllSetFlags(flags);
            return setValues.Any() ? setValues.Max() : 0;
        }
        public static int GetMinSetFlagValue<T>(T flags) where T : struct
        {
            IEnumerable<int> setValues = GetAllSetFlags(flags);
            return setValues.Any() ? setValues.Min() : 0;
        }

        /// <summary>
        /// Get Highest Order Flag
        /// <para>**MUST BE INT TYPE ENUM**</para>
        /// </summary>
        /// <typeparam name="T">Type of Enum</typeparam>
        /// <param name="flags">Flagged Enum</param>
        /// <returns>Highest Order Flag</returns>
        public static T GetMaxFlag<T>(this T flags) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), GetMaxSetFlagValue(flags));
        }
        /// <summary>
        /// Get Lowest Order Flag
        /// <para>**MUST BE INT TYPE ENUM**</para>
        /// </summary>
        /// <typeparam name="T">Type of Enum</typeparam>
        /// <param name="flags">Flagged Enum</param>
        /// <returns>Lowest Order Flag</returns>
        public static T GetMinFlag<T>(this T flags) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), GetMinSetFlagValue(flags));
        }
        /// <summary>
        /// Get Enumerable Set Flags
        /// </summary>
        /// <typeparam name="T">Type of Enum</typeparam>
        /// <param name="flags">Flagged Enum</param>
        /// <returns>Enumerable Flags</returns>
        public static IEnumerable<T> GetAllFlags<T>(this T flags) where T : struct
        {
            return GetAllSetFlags(flags).Cast<T>();
        }
    }
}
