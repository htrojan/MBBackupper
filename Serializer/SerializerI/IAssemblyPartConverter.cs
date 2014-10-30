namespace Serializer.SerializerI
{
    public interface IAssemblyPartConverter
    {
        AssemblyPart Convert(SerializableElement element);
    }
}