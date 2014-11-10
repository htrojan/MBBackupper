using System;
using System.Collections.Generic;
using Serializer.TypeParser;

namespace Serializer.SerializerI
{
    public class AssemblyGeneratorParams
    {
        private readonly SerializationTree _tree;
        private readonly string _backendIdentifier;
        private readonly Dictionary<Type, IAttributeHandler> _attributeMap;
        private readonly Dictionary<Type, IAssemblyPartConverter> _converterMap;
        private readonly Type _assemblyType;

        public AssemblyGeneratorParams(SerializationTree tree, string backendIdentifier, Dictionary<Type, IAttributeHandler> attributeMap, Dictionary<Type, IAssemblyPartConverter> converterMap, Type assemblyType)
        { 
            _tree = tree;
            _backendIdentifier = backendIdentifier;
            _attributeMap = attributeMap;
            _converterMap = converterMap;
            _assemblyType = assemblyType;
        }

        public SerializationTree Tree
        {
            get { return _tree; }
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