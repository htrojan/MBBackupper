using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.TypeParser
{
    public class PropertyInfoWrapper : IField
    {
        private PropertyInfo _property;

        public PropertyInfoWrapper(PropertyInfo property)
        {
            _property = property;
        }

        public Type GetValueType()
        {
            return _property.PropertyType;
        }

        public object GetValue(object obj)
        {
            return _property.GetValue(obj);
        }

        public void SetValue(object obj, object value)
        {
            _property.SetValue(obj, value);
        }

        public string GetName()
        {
            return _property.Name;
        }
    }
}
