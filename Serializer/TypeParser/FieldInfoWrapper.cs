using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.TypeParser
{
    public class FieldInfoWrapper : IField
    {
        private FieldInfo _field;

        public FieldInfoWrapper(FieldInfo field)
        {
            _field = field;
        }

        public Type GetValueType()
        {
            return _field.GetType();
        }

        public object GetValue(object obj)
        {
            return _field.GetValue(obj);
        }

        public void SetValue(object obj, object value)
        {
            _field.SetValue(obj, value);
        }

        public string GetName()
        {
            return _field.Name;
        }
    }
}
