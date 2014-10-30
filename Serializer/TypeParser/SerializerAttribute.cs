using System;
using Serializer.SerializerI;

namespace Serializer.TypeParser
{
    public class SerializerAttribute : Attribute, IIdentifiableBackend
    {
        public string BackendIdentifier { get; private set; }
    }
}
