using System.Collections.Generic;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        public static Dictionary<TValue, TKey> Inverse<TKey, TValue>(this Dictionary<TKey, TValue> input)
        {
            Dictionary<TValue, TKey> dict = new Dictionary<TValue, TKey>();
            foreach (var kv in input)
            {
                dict.Add(kv.Value, kv.Key);
            }
            return dict;
        }
    }
}
