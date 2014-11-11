using System;
using System.Collections.Generic;
using Serializer.Annotations;

namespace Serializer.TypeParser
{
    public class SerializationTree : IAttributeContainer
    {
        private List<SerializerAttribute> _attributes;
        private List<AtomicType> _atomicTypes;

        public SerializationTree()
        {
            _atomicTypes = new List<AtomicType>();
            _attributes = new List<SerializerAttribute>();
        }

        public virtual IEnumerable<SerializerAttribute> Attributes
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

        public virtual IEnumerable<AtomicType> AtomicTypes
        {
            get
            {
                return _atomicTypes;
            }
            [UsedImplicitly] set
            {
                _atomicTypes = new List<AtomicType>(value);
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

        public void AddAtomicType(AtomicType type)
        {
            _atomicTypes.Add(type);
        }
    }
}
