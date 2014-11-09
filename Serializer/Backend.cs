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
        private readonly List<Type> _attributeHandlers;
        private readonly List<Type> _specialAttributeHandlers; 
        private readonly List<Type> _assemblyPartConverters;

        private Backend(IEnumerable<Type> attributeHandlers, IEnumerable<Type> assemblyPartConverters, Type assemblyType, Type assemblyGeneratorType, IEnumerable<Type> specialAttributeHandlers)
        {
            _assemblyType = assemblyType;
            _assemblyGeneratorType = assemblyGeneratorType;
            _specialAttributeHandlers = new List<Type>(specialAttributeHandlers);
            _attributeHandlers = new List<Type>(attributeHandlers);
            _assemblyPartConverters = new List<Type>(assemblyPartConverters);
        }

        public static Backend LoadBackend(Assembly assembly)
        {
            Type assemblyType = null;
            Type assemblyGeneratorType = null;
            var attributeHandlers = new List<Type>();
            var specialAttributeHandlers = new List<Type>();
            var assemblyPartConverters = new List<Type>();

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
            var supportedTypes = GetSupportedTypesWithin(_attributeHandlers);
            return supportedTypes;
        }

        public ISet<Type> GetSupportedSpecialTypes()
        {
            var specialTypes = GetSupportedTypesWithin(_specialAttributeHandlers);
            return specialTypes;
        }

        private ISet<Type> GetSupportedTypesWithin(IEnumerable<Type> types )
        {
            ISet<Type> supportedTypes = new HashSet<Type>();
            foreach (var mappingAttribute in types.Select(AttributeHelper.GetMappingAttribute))
            {
                supportedTypes.Add(mappingAttribute.GetTargetingType());
            }
            return supportedTypes;
        }

        public Dictionary<Type, IAttributeHandler> GetAttributeHandlerMap()
        {
            Dictionary<Type, IAttributeHandler> handlers =
                CreateTypeInstanceDictionaryFrom<IAttributeHandler>(_attributeHandlers);
            return handlers;
        }

        public Dictionary<Type, IAssemblyPartConverter> GetAssemblyPartConverterMap()
        {
            Dictionary<Type, IAssemblyPartConverter> converters =
                CreateTypeInstanceDictionaryFrom<IAssemblyPartConverter>(_assemblyPartConverters);
            return converters;
        }
         
        private Dictionary<Type, T> CreateTypeInstanceDictionaryFrom<T>(IEnumerable<Type> instanceTypes )
        {
            Dictionary<Type, T> instanceMap = new Dictionary<Type, T>();
            foreach (var instanceType in instanceTypes)
            {
                var mappingAttribute = (MappingAttribute)Attribute.GetCustomAttribute(instanceType, typeof(MappingAttribute));
                var targetingType = mappingAttribute.GetTargetingType();
                var ctor = instanceType.GetConstructor(new Type[0]);
                Debug.Assert(ctor != null, "Default constructor of targetingType != null");
                T instanceTypeObject = (T) ctor.Invoke(new object[0]);
                InjectBackendIdentifier(instanceTypeObject);
                instanceMap.Add(targetingType, instanceTypeObject);
            }
            return instanceMap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">An object that implements IIdentifiableBackend and has a BackendIdentifier attrbitute attached to it</param>
        private void InjectBackendIdentifier(object obj)
        {
            var objType = obj.GetType();
            if (!ImplementsInterface(objType, typeof (IIdentifiableBackend)))
            {
                throw new Exception(string.Format("The given Type ({0}) does not implement the IIdentifiableBackendInterface", obj.GetType().FullName));
            }
            //There is only one property with that name
            var backendIdentifier = objType.GetProperties().First(info => info.Name == "BackendIdentifier");
            backendIdentifier.SetValue(obj, AttributeHelper.GetBackendIdentifierAttribute(objType).GetBackendIdentifier());
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
