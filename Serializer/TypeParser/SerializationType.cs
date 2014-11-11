using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serializer.TypeParser
{
    public abstract class SerializationType : ISerializationType
    {
        protected List<SerializerAttribute> _attributes;
        protected readonly IField _field;

        protected SerializationType(IField field)
        {
            _field = field;
            _attributes = new List<SerializerAttribute>();
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
            get { return _field.GetValueType(); }
        }

        public string Name
        {
            get { return _field.GetName(); }
        }


        public void SetValue(object obj, object value)
        {
            _field.SetValue(obj, value);
        }

        public object GetValue(object instance)
        {
            return _field.GetValue(instance);
        }
    }
}
