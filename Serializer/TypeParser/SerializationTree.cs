using System;
using System.Collections.Generic;
using Serializer.Annotations;

namespace Serializer.TypeParser
{
    public class SerializationTree : IAttributeContainer
    {
        private List<SerializerAttribute> _attributes;
        private List<AtomicType> _atomicTypes;
        private List<SpecialType> _specialTypes;

        public SerializationTree()
        {
            _atomicTypes = new List<AtomicType>();
            _attributes = new List<SerializerAttribute>();
            _specialTypes = new List<SpecialType>();
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

        public IEnumerable<AtomicType> AtomicTypes
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

        public IEnumerable<SpecialType> SpecialTypes
        {
            get
            {
                return _specialTypes;
            }
            [UsedImplicitly] set
            {
                _specialTypes = new List<SpecialType>(value);
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

        public void AddSpecialType(SpecialType type)
        {
            _specialTypes.Add(type);
        }

        public void AddAtomicType(AtomicType type)
        {
            _atomicTypes.Add(type);
        }
    }
}
