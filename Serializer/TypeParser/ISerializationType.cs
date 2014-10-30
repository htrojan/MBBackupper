using System;

namespace Serializer.TypeParser
{
    public interface ISerializationType : IAttributeContainer
    {
        Type ValueType
        {
            get;
        }

        string Name
        {
            get;
        }

        void SetValue(object obj, object value);

        object GetValue(object instance);
    }
}
