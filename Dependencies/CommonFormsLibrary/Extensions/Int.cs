using System.Collections.Generic;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        public static IEnumerable<int> GetBitwiseValues(this int value)
        {
            for (int i = 0; i < 32; i++)
            {
                if ((value & (1 << i)) == (1 << i)) yield return 1 << i;
            }
        }
    }
}
