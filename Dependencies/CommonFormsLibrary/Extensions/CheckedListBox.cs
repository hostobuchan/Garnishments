using System.Collections.Generic;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this CheckedListBox.CheckedItemCollection coll)
        {
            return coll.ToList<T>();
        }
        public static List<T> ToList<T>(this CheckedListBox.CheckedItemCollection coll)
        {
            List<T> list = new List<T>();
            foreach (T V in coll)
            {
                list.Add(V);
            }
            return list;
        }

        public static IEnumerable<T> AsEnumerable<T>(this CheckedListBox.SelectedObjectCollection coll)
        {
            return coll.ToList<T>();
        }
        public static List<T> ToList<T>(this CheckedListBox.SelectedObjectCollection coll)
        {
            List<T> list = new List<T>();
            foreach (T V in coll)
            {
                list.Add(V);
            }
            return list;
        }
    }
}
