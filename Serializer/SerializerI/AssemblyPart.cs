using Serializer.Annotations;

namespace Serializer.SerializerI
{
    public abstract class AssemblyPart
    {
        [UsedImplicitly]
        protected object _value;

        public object Value
        {
            get { return _value; }
        }
    }
}
