using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.Windows.Forms.DataSources
{
    public class DictionaryLimitedDataSource<TKey, TValue> : IList<TValue>, IList, IListSource where TValue : struct
    {
        Bindings.DictionaryBindingList<TKey, TValue> Bindings { get; set; }

        public DictionaryLimitedDataSource(Bindings.DictionaryBindingList<TKey, TValue> bindings)
        {
            this.Bindings = bindings;
        }

        protected TValue[] TotalSet { get { return Enum.GetValues(typeof(TValue)).OfType<TValue>().ToArray(); } }
        protected IEnumerable<TValue> CurrentSet { get { return Enum.GetValues(typeof(TValue)).Where<TValue>(en => !this.Bindings.Select(el => el.Value).Where(el => Convert.ToInt64(el) != 0).Contains(en)); } }

        object IList.this[int index] { get { return this[index]; } set { } }
        public TValue this[int index] { get { return this.CurrentSet.ToArray()[index]; } set { } }

        public int Count { get { return this.CurrentSet.Count(); } }

        public bool IsReadOnly { get { return true; } }

        public bool ContainsListCollection { get { return false; } }

        public bool IsFixedSize { get { return false; } }

        public object SyncRoot { get { return this; } }

        public bool IsSynchronized { get { return false; } }


        public int Add(object value) { return -1; }
        public void Add(TValue item) { return; }
        public void Clear() { return; }

        public bool Contains(object value) { return this.Contains((TValue)value); }
        public bool Contains(TValue item) { return this.CurrentSet.Contains(item); }

        public void CopyTo(Array array, int index) { return; }
        public void CopyTo(TValue[] array, int arrayIndex) { return; }

        public int IndexOf(object value) { return this.IndexOf((TValue)value); }
        public int IndexOf(TValue item)
        {
            int itemIndex = -1;
            for (int i = 0; i < this.TotalSet.Length; i++)
            {
                if (Convert.ToInt64(this.TotalSet[i]) == Convert.ToInt64(item))
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }

        public void Insert(int index, object value) { return; }
        public void Insert(int index, TValue item) { return; }

        public void Remove(object value) { return; }
        public bool Remove(TValue item) { return false; }

        public void RemoveAt(int index) { return; }

        public IEnumerator<TValue> GetEnumerator()
        {
            return this.CurrentSet.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.CurrentSet.GetEnumerator();
        }

        public IList GetList()
        {
            return this;
        }
    }
}
