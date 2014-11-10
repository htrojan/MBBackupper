using System;
using Serializer.TypeParser;

namespace Serializer.ObjectParser
{
    public class ObjectParser
    {
        private readonly Object _instance;
        private readonly SerializationTree _tree;
        private readonly ValuePool _pool;

        private ObjectParser(SerializationTree tree, object obj)
        {
            _instance = obj;
            _tree = tree;
            _pool = new ValuePool();
        }

        public static ValuePool Parse(SerializationTree tree, object obj)
        {
            ObjectParser objectParser = new ObjectParser(tree, obj);
            return objectParser.ParseObject();
        }

        private ValuePool ParseObject()
        {
            foreach (var atomicType in _tree.AtomicTypes)
            {
                ParseField(atomicType);
            }

            return _pool;
        }

        private void ParseField(ISerializationType field)
        {
            object value = field.GetValue(_instance);
            string name = field.Name;
            _pool.AddValue(name, value);
        }
    }
}
