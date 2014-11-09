using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Serializer.Annotations;
using Serializer.Attributes;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

namespace Serializer
{
    [UsedImplicitly]
    public class Backend : IIdentifiableBackend
    {
        private readonly Type _assemblyType;
        private readonly Type _assemblyGeneratorType;
        private readonly IEnumerable<Type> _attributeHandlers;
        private readonly IEnumerable<Type> _specialAttributeHandlers; 
        private readonly IEnumerable<Type> _assemblyPartConverters;

        private Backend(IEnumerable<Type> attributeHandlers, IEnumerable<Type> assemblyPartConverters, Type assemblyType, Type assemblyGeneratorType, IEnumerable<Type> specialAttributeHandlers)
        {
            _assemblyType = assemblyType;
            _assemblyGeneratorType = assemblyGeneratorType;
            _specialAttributeHandlers = specialAttributeHandlers;
            _attributeHandlers = attributeHandlers;
            _assemblyPartConverters = assemblyPartConverters;
        }

        public static Backend LoadBackend(Assembly assembly)
        {
            Type assemblyType = null;
            Type assemblyGeneratorType = null;
            List<Type> attributeHandlers = new List<Type>();
            List<Type> specialAttributeHandlers = new List<Type>();
            List<Type> assemblyPartConverters = new List<Type>();

            foreach (var type in assembly.DefinedTypes)
            {
                if (ImplementsInterface(type, typeof(IAttributeHandler)))  
                {
                    attributeHandlers.Add(type);
                }
                else if (ImplementsInterface(type, typeof(IAssemblyPartConverter)))
                {
                    assemblyPartConverters.Add(type);
                }
                else if (type.BaseType == typeof(SerializerI.Assembly))
                {
                    assemblyType = type;
                }
                else if (type.BaseType == typeof(AssemblyGenerator))
                {
                    assemblyGeneratorType = type;
                }
                else if (ImplementsInterface(type, typeof(ISpecialAttributeHandler))) 
                {
                    specialAttributeHandlers.Add(type);
                }
            }
            if (assemblyType == null || assemblyGeneratorType == null)
            {
                throw new Exception(string.Format("The given Backend-Assembly: {0} has either no AssemblyType or no AssemblyGeneratorType", assembly.FullName));
            }
            var backend = new Backend(attributeHandlers, assemblyPartConverters, assemblyType, assemblyGeneratorType, specialAttributeHandlers);
            return backend;
        }

        private static bool ImplementsInterface(Type type, Type interfaceType)
        {
            return type.GetTypeInfo().ImplementedInterfaces.Contains(interfaceType);
        }

        public ISet<Type> GetSupportedAtomicTypes()
        {
            var supportedTypes = GetSupportedTypes(_attributeHandlers);
            return supportedTypes;
        }

        public ISet<Type> GetSupportedSpecialTypes()
        {
            var specialTypes = GetSupportedTypes(_specialAttributeHandlers);
            return specialTypes;
        }

        private ISet<Type> GetSupportedTypes(IEnumerable<Type> types )
        {
            ISet<Type> supportedTypes = new HashSet<Type>();
            foreach (Type handler in types)
            {
                //allowed as only one mappingAttribute per class is allowed
                var mappingAttribute =
                    (MappingAttribute) Attribute.GetCustomAttribute(handler, typeof (MappingAttribute));
                supportedTypes.Add(mappingAttribute.GetTargetingType());
            }
            return supportedTypes;
        }

        public Dictionary<Type, IAttributeHandler> GetAttributeHandlerMap()
        {
            Dictionary<Type, IAttributeHandler> handlers =
                ExtractTargetingTypeInstanceDictionary<IAttributeHandler>(_attributeHandlers);
            return handlers;
        }

        public Dictionary<Type, IAssemblyPartConverter> GetAssemblyPartConverterMap()
        {
            Dictionary<Type, IAssemblyPartConverter> converters =
                ExtractTargetingTypeInstanceDictionary<IAssemblyPartConverter>(_assemblyPartConverters);
            return converters;
        }

        private Dictionary<Type, T> ExtractTargetingTypeInstanceDictionary<T>(IEnumerable<Type> instanceTypes )
        {
            Dictionary<Type, T> instanceMap = new Dictionary<Type, T>();
            foreach (var instanceType in instanceTypes)
            {
                MappingAttribute attribute = (MappingAttribute)Attribute.GetCustomAttribute(instanceType, typeof(MappingAttribute));
                Type targetingType = attribute.GetTargetingType();
                var ctor = instanceType.GetConstructor(new Type[0]);
                T instanceTypeObject = (T) ctor.Invoke(new object[0]);
                instanceMap.Add(targetingType, instanceTypeObject);
            }
            return instanceMap;
        }

        public string BackendIdentifier {
            get
            {
                //throw new NotImplementedException("All Methods for obtaining BackendIdentifiers are obsolete and should be removed");
                var backendIdentifier =
                    AttributeHelper.GetBackendIdentifierAttribute(_assemblyType);

                return backendIdentifier.GetBackendIdentifier();
            }
        }

        public AssemblyGenerator GetAssemblyGeneratorInstance(AssemblyGeneratorParams generatorParams)
        {
            var ctor = _assemblyGeneratorType.GetConstructor(new[] {typeof (AssemblyGeneratorParams)});
// ReSharper disable once PossibleNullReferenceException
            return (AssemblyGenerator) ctor.Invoke(new object[] {generatorParams});
        }

        public Type GetAssemblyType()
        {
            return _assemblyType;
        }

        public Type GetAssemblyGeneratorType()
        {
            return _assemblyGeneratorType;
        }
    }
}
