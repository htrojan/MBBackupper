using System.Reflection;

namespace Serializer.TypeParser
{
    public class SpecialType : SerializationType
    {
        public SpecialType(FieldInfo field) : base(field)
        {
        }

        public SpecialType(PropertyInfo property) : base(property)
        {
        }
    }
}
