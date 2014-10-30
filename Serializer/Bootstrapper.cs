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
        private readonly Dictionary<string, SerializationTree> _serializationTreeMap;
        /// <summary>
        /// The int is for the hashcode of the object the ValuePool is mapped to
        /// </summary>
        private readonly Dictionary<int, ValuePool> _valuePoolMap;

        private readonly Dictionary<string, Backend> _backendMap;

        #region Constructors
        public Bootstrapper() : this(new Dictionary<string, SerializationTree>())
        {
            
        }

        public Bootstrapper(Dictionary<string, SerializationTree> serializationTreeMap)
        {
            _serializationTreeMap = serializationTreeMap;
            _valuePoolMap = new Dictionary<int, ValuePool>();
            _backendMap = new Dictionary<string, Backend>();
        }
        #endregion

        #region Public methods

        [UsedImplicitly]
        public void AddBackend(Backend backend)
        {
            _backendMap.Add(backend.BackendIdentifier, backend);
        }

        [UsedImplicitly]
        public void CacheType([NotNull] Type type)
        {
            var tree = CreateSerializationTree(type);
            AddTypeToSerializationTreePool(type, tree);
        }

        [UsedImplicitly]
        public void CacheObject([NotNull] object obj)
        {
            //obtain SerializationTree
            var tree = GetOrCreateSerializationTree(obj.GetType());
            //create ObjectValuePool
            var objectCache = ObjectParser.ObjectParser.Parse(tree, obj);
            //Register ObjectPool with HashCode in the cache
            _valuePoolMap.Add(obj.GetHashCode(), objectCache);
        }

        public void Serialize(object obj, string backendIdentifier)
        {
            Backend backend = _backendMap[backendIdentifier];
            if (backend == null)
            {
                throw new Exception("The given backendIdentifier has no corresponding backend");
            }
        }

        #endregion

        private SerializationTree GetOrCreateSerializationTree(Type type)
        {
            var tree = GetSerializationTree(type);
            if (tree == null)
            {
                tree = CreateSerializationTree(type);
                AddTypeToSerializationTreePool(type, tree);
            }
            return tree;
        }

        private SerializationTree CreateSerializationTree(Type type)
        {
            var serializationTree = TypeParser.TypeParser.ParseType(type, _atomicTypes, _specialTypes);
            return serializationTree;
        }

        private void AddTypeToSerializationTreePool(Type type, SerializationTree tree)
        {
            if (type.AssemblyQualifiedName != null) _serializationTreeMap.Add(type.AssemblyQualifiedName, tree);
            else
            {
                throw new Exception(string.Format("The type \"{0}\" can not be registered since the AssemblyQualifiedName is null", type));
            }
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
                _serializationTreeMap.TryGetValue(type.AssemblyQualifiedName, out tree);
            return tree;
        }
    }
}
