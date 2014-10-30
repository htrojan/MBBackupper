using Serializer.Annotations;

namespace Serializer.SerializerI
{
    public abstract class SerializableElement : IIdentifiableBackend
    {
        protected SerializableElement(object value)
        {
            Value = value;
        }

        [UsedImplicitly]
        public object Value { get; private set; }
        public string BackendIdentifier { get; private set; }
    }
}
