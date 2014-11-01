using Serializer.Annotations;
using Serializer.TypeParser;

namespace Serializer.SerializerI
{
    [UsedImplicitly]
    public interface IAttributeHandler : IIdentifiableBackend
    {
        void Execute(ref AssemblyPart element, SerializerAttribute attribute);
        bool CanExecute(SerializerAttribute attribute);  
    }
}
   