using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.Windows.Forms.Objects
{
    public class ObjectWithProperties<TValue> : ICustomTypeDescriptor
    {
        public class MyDescriptor : PropertyDescriptor
        {
            public MyDescriptor(string name) : base(name, null) { }
            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override Type ComponentType
            {
                get { return typeof(ObjectWithProperties<TValue>); }
            }

            public override object GetValue(object component)
            {
                return (component as ObjectWithProperties<TValue>)[Name];
            }

            public override bool IsReadOnly
            {
                get { return false; }
            }

            public override Type PropertyType
            {
                get { return typeof(object); }
            }

            public override void ResetValue(object component)
            {
                (component as ObjectWithProperties<TValue>).properties.Remove(Name);
            }

            public override void SetValue(object component, object value)
            {
                (component as ObjectWithProperties<TValue>)[Name] = (TValue)value;
            }

            public override bool ShouldSerializeValue(object component)
            {
                return (component as ObjectWithProperties<TValue>).properties.ContainsKey(Name);
            }
        }

        Dictionary<string, TValue> properties = new Dictionary<string, TValue>();
        public TValue this[string name]
        {
            get
            {
                if (properties.ContainsKey(name))
                {
                    return properties[name];
                }
                return default(TValue);
            }
            set
            {
                properties[name] = value;
            }
        }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return null;
        }

        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(properties.Keys.Select(key => new MyDescriptor(key)).ToArray());
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }
}
