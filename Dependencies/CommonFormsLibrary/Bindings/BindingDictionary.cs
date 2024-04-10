using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms.Bindings
{
    public sealed class Pair<TKey, TValue> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property) { this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)); }

        private readonly TKey key;
        private readonly IDictionary<TKey, TValue> data;
        public Pair(TKey key, IDictionary<TKey, TValue> data)
        {
            this.key = key;
            this.data = data;
        }
        public TKey Key { get { return key; } }
        public TValue Value
        {
            get
            {
                TValue value;
                data.TryGetValue(key, out value);
                return value;
            }
            set { data[key] = value; OnPropertyChanged("Value"); }
        }

        public override string ToString()
        {
            return $"{this.key} [{this.Value}]";
        }
    }
    public class DictionaryBindingList<TKey, TValue> : BindingList<Pair<TKey, TValue>>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) { this.PropertyChanged?.Invoke(sender, e); }

        private readonly IDictionary<TKey, TValue> data;
        public DictionaryBindingList(IDictionary<TKey, TValue> data)
        {
            this.data = data;
            Reset();
        }
        public void Reset()
        {
            bool oldRaise = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            try
            {
                Clear();
                foreach (TKey key in data.Keys)
                {
                    var newPair = new Pair<TKey, TValue>(key, data);
                    Add(newPair);
                    newPair.PropertyChanged += OnPropertyChanged;
                }
            }
            finally
            {
                RaiseListChangedEvents = oldRaise;
                ResetBindings();
            }
        }

        public Pair<TKey, TValue> this[string key]
        {
            get
            {
                foreach (var pair in this)
                {
                    if (pair.Key.Equals(key)) return pair;
                }
                return null;
            }
        }

        public void Add(TKey key, TValue value)
        {
            this.data.Add(new KeyValuePair<TKey, TValue>(key, value));
            var newPair = new Pair<TKey, TValue>(key, this.data);
            Add(newPair);
            newPair.PropertyChanged += OnPropertyChanged;
        }
    }
}
