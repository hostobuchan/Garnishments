using System.Collections.Generic;
using System.Windows.Forms.Bindings;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        public static DictionaryBindingList<TKey, TValue>
            ToBindingList<TKey, TValue>(this IDictionary<TKey, TValue> data)
        {
            return new DictionaryBindingList<TKey, TValue>(data);
        }
    }
}
