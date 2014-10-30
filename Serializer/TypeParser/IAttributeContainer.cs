using System;
using System.Collections.Generic;

namespace Serializer.TypeParser
{
    public interface IAttributeContainer
    {
        IEnumerable<SerializerAttribute> Attributes
        {
            get;
            set;
        }

        void AddAttribute(SerializerAttribute attribute);

        void AddAttributes(IEnumerable<SerializerAttribute> attributes);
    }
}
