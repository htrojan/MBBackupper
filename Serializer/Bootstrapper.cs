using System;
using System.Collections.Generic;
using Serializer.Annotations;
using Serializer.ObjectParser;
using Serializer.SerializerI;
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

        private readonly Backend _backend;
         
        #region Constructors
        public Bootstrapper(Backend operatingBackend) : this(new Dictionary<string, SerializationTree>(), operatingBackend)
        {
            
        }

        public Bootstrapper(Dictionary<string, SerializationTree> serializationTreeMap, Backend backend)
        {
            _serializationTreeMap = serializationTreeMap;
            _valuePoolMap = new Dictionary<int, ValuePool>();
            _backend = backend;
        }
        #endregion

        #region Public methods

        [UsedImplicitly]
        public void CacheType([NotNull] Type type)
        {
            var tree = CreateSerializationTree(type);
            AddTypeToSerializationTreeMap(type, tree);
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

        [UsedImplicitly]
        public void Serialize(object obj, object destination)
        {
            SerializationTree tree = GetOrCreateSerializationTree(obj.GetType());
            ValuePool pool = _valuePoolMap[obj.GetHashCode()] ?? ObjectParser.ObjectParser.Parse(tree, obj);
            AssemblyGeneratorParams pParams = new AssemblyGeneratorParams(tree, _backend.BackendIdentifier,
                _backend.GetAttributeHandlerMap(), _backend.GetAssemblyPartConverterMap(), _backend.GetAssemblyType());
            AssemblyGenerator generator = _backend.GetAssemblyGeneratorInstance(pParams);
            Assembly asm = generator.CreateAssembly(pool);
            asm.Serialize(destination);
        }

        #endregion



        private SerializationTree GetOrCreateSerializationTree(Type type)
        {
            var tree = GetSerializationTreeOf(type);
            if (tree == null)
            {
                tree = CreateSerializationTree(type);
                AddTypeToSerializationTreeMap(type, tree);
            }
            return tree;
        }

        private SerializationTree CreateSerializationTree(Type type)
        {
            ISet<Type> atomicTypes = _backend.GetSupportedAtomicTypes();

            var serializationTree = TypeParser.TypeParser.ParseType(type, atomicTypes);
            return serializationTree;
        }

        private void AddTypeToSerializationTreeMap(Type type, SerializationTree tree)
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
        private SerializationTree GetSerializationTreeOf(Type type)
        {
            SerializationTree tree = _serializationTreeMap[type.FullName];
            return tree;
        }
    }
}
