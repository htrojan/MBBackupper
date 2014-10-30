using System;
using System.Collections.Generic;
using Serializer.Annotations;
using Serializer.ObjectParser;
using Serializer.TypeParser;

namespace Serializer
{
    public class Bootstrapper
    {
        /// <summary>
        /// The string is the name of the type, the actual type isn't used since memory concerns
        /// </summary>
        [NotNull] private readonly Dictionary<string, SerializationTree> _serializationTreePool;
        /// <summary>
        /// The int is for the hashcode of the object the ValuePool is mapped to
        /// </summary>
        [NotNull] private readonly Dictionary<int, ValuePool> _valuePoolMapping;

        [NotNull] private readonly HashSet<Type> _atomicTypes= new HashSet<Type>
        {
            typeof(string),
            typeof(int),
            typeof(byte),
            typeof(short),
            typeof(long)
        };
        [NotNull] private readonly HashSet<Type> _specialTypes = new HashSet<Type>
        {
            typeof(IEnumerable<>)
        };  

        public Bootstrapper() : this(new Dictionary<string, SerializationTree>())
        {
            
        }

        public Bootstrapper(Dictionary<string, SerializationTree> serializationTreePool)
        {
            _serializationTreePool = serializationTreePool;
            _valuePoolMapping = new Dictionary<int, ValuePool>();
        }

        [UsedImplicitly]
        public void RegisterType([NotNull] Type type)
        {
            var tree = CreateSerializationTree(type);
            AddTypeToSerializationTreePool(type, tree);
        }

        private void AddTypeToSerializationTreePool(Type type, SerializationTree tree)
        {
            if (type.AssemblyQualifiedName != null) _serializationTreePool.Add(type.AssemblyQualifiedName, tree);
            else
            {
                throw new Exception(string.Format("The type \"{0}\" can not be registered since the AssemblyQualifiedName is null", type));
            }
        }

        private SerializationTree CreateSerializationTree(Type type)
        {
            var serializationTree = TypeParser.TypeParser.ParseType(type, _atomicTypes, _specialTypes);
            return serializationTree;
        }

        [UsedImplicitly]
        public void RegisterObjectValuePool([NotNull] object obj)
        {
            //obtain SerializationTree
            var tree = GetSerializationTree(obj.GetType());
            if (tree == null)
            {
                tree = CreateSerializationTree(obj.GetType());
                AddTypeToSerializationTreePool(obj.GetType(), tree);
            }
            //create ObjectValuePool
            var pool = ObjectParser.ObjectParser.Parse(tree, obj);
            //Register ObjectPool with HashCode in the cache
            _valuePoolMapping.Add(obj.GetHashCode(), pool);
        }

        /// <summary>
        /// returns null if the type is not contained within the local pool
        /// </summary>
        /// <param name="type">The type the requested SerializationTree is mapped to</param>
        /// <returns></returns>
        private SerializationTree GetSerializationTree(Type type)
        {
            SerializationTree tree = null;
            if (type.AssemblyQualifiedName != null)
                _serializationTreePool.TryGetValue(type.AssemblyQualifiedName, out tree);
            return tree;
        }
    }
}
