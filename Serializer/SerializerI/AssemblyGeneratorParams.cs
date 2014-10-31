using System;
using System.Collections.Generic;
using Serializer.TypeParser;

namespace Serializer.SerializerI
{
    public class AssemblyGeneratorParams
    {
        private SerializationTree _tree;
        private IEnumerable<IAttributeHandler> _attributeHandlers;
        private Type _serializableElementType;
        private string _backendIdentifier;
        private Dictionary<Type, IAttributeHandler> _attributeMap;
        private Dictionary<Type, IAssemblyPartConverter> _converterMap;
        private Type _assemblyType;

        public AssemblyGeneratorParams(SerializationTree tree, IEnumerable<IAttributeHandler> attributeHandlers, Type serializableElementType, string backendIdentifier, Dictionary<Type, IAttributeHandler> attributeMap, Dictionary<Type, IAssemblyPartConverter> converterMap, Type assemblyType)
        {
            _tree = tree;
            _attributeHandlers = attributeHandlers;
            _serializableElementType = serializableElementType;
            _backendIdentifier = backendIdentifier;
            _attributeMap = attributeMap;
            _converterMap = converterMap;
            _assemblyType = assemblyType;
        }

        public SerializationTree Tree
        {
            get { return _tree; }
        }

        public IEnumerable<IAttributeHandler> AttributeHandlers
        {
            get { return _attributeHandlers; }
        }

        public Type SerializableElementType
        {
            get { return _serializableElementType; }
        }

        public string BackendIdentifier
        {
            get { return _backendIdentifier; }
        }

        public Dictionary<Type, IAttributeHandler> AttributeMap
        {
            get { return _attributeMap; }
        }

        public Dictionary<Type, IAssemblyPartConverter> ConverterMap
        {
            get { return _converterMap; }
        }

        public Type AssemblyType
        {
            get { return _assemblyType; }
        }
    }
}