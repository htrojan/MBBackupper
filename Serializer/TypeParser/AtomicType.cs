using System.Reflection;

namespace Serializer.TypeParser
{
    public class AtomicType : SerializationType
    {
        public AtomicType(IField field) : base(field)
        {
        }

    }
}
