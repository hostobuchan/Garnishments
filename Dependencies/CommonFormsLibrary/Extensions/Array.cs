using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static partial class Extensions
    {
        public static IEnumerable<T> Where<T>(this Array array, Func<T, bool> func)
        {
            foreach (T el in array.OfType<T>())
            {
                if (func(el))
                    yield return el;
            }
        }
        public static T[] Split<T>(this T[] array, int StartIndex, int Length)
        {
            T[] newArray = new T[Length];
            for (int i = StartIndex; i <= array.GetUpperBound(0) && (i - StartIndex) < Length; i++)
            {
                newArray[i - StartIndex] = array[i];
            }
            return newArray;
        }
    }
}
