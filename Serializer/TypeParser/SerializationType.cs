using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serializer.TypeParser
{
    public abstract class SerializationType : ISerializationType
    {
        protected List<SerializerAttribute> _attributes;
        protected readonly FieldInfo _field;
        protected readonly PropertyInfo _property;

        protected SerializationType(FieldInfo field)
        {
            _attributes = new List<SerializerAttribute>();
            _field = field;
        }

        protected SerializationType(PropertyInfo property)
        {
            _attributes = new List<SerializerAttribute>();
            _property = property;
        }

        public IEnumerable<SerializerAttribute> Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = new List<SerializerAttribute>(value);
            }
        }


        public void AddAttribute(SerializerAttribute attribute)
        {
            _attributes.Add(attribute);
        }

        public void AddAttributes(IEnumerable<SerializerAttribute> attributes)
        {
            _attributes.AddRange(attributes);
        }

        public Type ValueType
        {
            get
            {
                if (_field != null)
                {
                    return _field.FieldType;
                }
                else if (_property != null)
                {
                    return _property.PropertyType;
                }
                return null;
            }
        }

        public string Name
        {
            get
            {
                if (_field != null)
                {
                    return _field.Name;
                }
                else if (_property != null)
                {
                    return _property.Name;
                }
                return null;
            }
        }


        public void SetValue(object obj, object value)
        {
            if (_field != null)
            {
                _field.SetValue(obj, value);
            }
            else if (_property != null)
            {
                _property.SetValue(obj, value);
            }
        }

        public object GetValue(object instance)
        {
            if (_field != null)
            {
                return _field.GetValue(instance);
            }
            else if (_property != null)
            {
                return _property.GetValue(instance);
            }
            return null;
        }
    }
}
