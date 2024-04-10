using System.Collections.Generic;
using System.Dynamic;

namespace System.Windows.Forms.Objects
{
    public class DynamicObject
    {
        public dynamic Instance = new ExpandoObject();

        public void AddProperty(string name, object value)
        {
            ((IDictionary<string, object>)this.Instance).Add(name, value);
        }

        public dynamic GetProperty(string name)
        {
            if (((IDictionary<string, object>)this.Instance).ContainsKey(name))
                return ((IDictionary<string, object>)this.Instance)[name];
            else
                return null;
        }

        public void AddMethod(string methodName, Action methodBody)
        {
            this.AddProperty(methodName, methodBody);
        }
    }
}
