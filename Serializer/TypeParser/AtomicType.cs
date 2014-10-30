using System.Reflection;

namespace Serializer.TypeParser
{
    public class AtomicType : SerializationType
    {
        public AtomicType(FieldInfo field) : base(field)
        {
        }

        public AtomicType(PropertyInfo property) : base(property)
        {
        }
    }
}
