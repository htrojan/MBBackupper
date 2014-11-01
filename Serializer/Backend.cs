using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Serializer.Annotations;
using Serializer.Attributes;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

namespace Serializer
{
    [UsedImplicitly]
    public class Backend : IIdentifiableBackend
    {
        private Type _assemblyType;
        private Type _assemblyGeneratorType;
        private IEnumerable<Type> _attributeHandlers;
        private IEnumerable<Type> _specialAttributeHandlers; 
        private IEnumerable<Type> _assemblyPartConverters;
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
                if (type.ImplementedInterfaces.Contains(typeof(IAttributeHandler)))  
                {
                    attributeHandlers.Add(type);
                }
                else if (type.ImplementedInterfaces.Contains(typeof(IAssemblyPartConverter)))
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
                else if (type.ImplementedInterfaces.Contains(typeof(ISpecialAttributeHandler))) 
                {
                    specialAttributeHandlers.Add(type);
                }
            }
            if (assemblyType == null || assemblyGeneratorType == null)
            {
                throw new Exception(string.Format("The given Backend-Assembly: {0} has either no AssemblyType or no AssemblyGeneratorType", assembly.FullName));
            }
            Backend backend = new Backend(attributeHandlers, assemblyPartConverters, assemblyType, assemblyGeneratorType, specialAttributeHandlers);
            return backend;
        }

        public ISet<Type> GetSupportedAtomicTypes()
        {
            ISet<Type> supportedTypes = new HashSet<Type>();
            foreach (Type handler in _attributeHandlers)
            {
                //allowed as only one mappingAttribute per class is allowed
                MappingAttribute mappingAttribute = (MappingAttribute) Attribute.GetCustomAttribute(handler, typeof(MappingAttribute));
                supportedTypes.Add(mappingAttribute.GetTargetingType());
            }
            return supportedTypes;
        }

        public ISet<Type> GetSupportedSpecialTypes()
        {
            ISet<Type> supportedTypes = new HashSet<Type>();
            foreach (Type handler in _specialAttributeHandlers)
            {
                //allowed as only one mappingAttribute per class is allowed
                MappingAttribute mappingAttribute = (MappingAttribute) Attribute.GetCustomAttribute(handler, typeof(MappingAttribute));
                supportedTypes.Add(mappingAttribute.GetTargetingType());
            }
            return supportedTypes;
        }

        public Dictionary<Type, IAttributeHandler> GetAttributeHandlerMap()
        {
            Dictionary<Type, IAttributeHandler> handlers = new Dictionary<Type, IAttributeHandler>();
            foreach (var handler in _attributeHandlers)
            {
                MappingAttribute attribute =(MappingAttribute) Attribute.GetCustomAttribute(handler, typeof (MappingAttribute));
                Type targetingType = attribute.GetTargetingType();
                var ctor = handler.GetConstructor(new Type[0]);
                IAttributeHandler handlerObj =(IAttributeHandler) ctor.Invoke(new object[0]);
                handlers.Add(targetingType, handlerObj);
            }
            return handlers;
        }

        public Dictionary<Type, IAssemblyPartConverter> GetAssemblyPartConverterMap()
        {
            Dictionary<Type, IAssemblyPartConverter> converters = new Dictionary<Type, IAssemblyPartConverter>();
            foreach (var converter in _assemblyPartConverters)
            {
                MappingAttribute attribute =(MappingAttribute) Attribute.GetCustomAttribute(converter, typeof (MappingAttribute));
                Type targetingType = attribute.GetTargetingType();
                var ctor = converter.GetConstructor(new Type[0]);
                IAssemblyPartConverter handlerObj =(IAssemblyPartConverter) ctor.Invoke(new object[0]);
                converters.Add(targetingType, handlerObj);
            }
            return converters;
        } 

        public string BackendIdentifier {
            get
            {
                throw new NotImplementedException("All Methods for obtaining BackendIdentifiers are obsolete and should be removed");
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
