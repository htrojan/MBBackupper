using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Serializer.Annotations;
using Serializer.TypeParser;
using Serializer.ObjectParser;

namespace Serializer.SerializerI
{
    [UsedImplicitly]
    public class AssemblyGenerator : IIdentifiableBackend
    {
        private readonly SerializationTree _tree;
        private readonly List<IAttributeHandler> _attributeHandlers;
        private readonly Dictionary<Type, IAttributeHandler> _attributeMap;
        private readonly Dictionary<Type, IAssemblyPartConverter> _converterMap;
        private readonly Type _serializableElementType;
        private readonly Type _assemblyType;
        private List<SerializableElement> _serializableElements; 

        public AssemblyGenerator(SerializationTree tree, IEnumerable<IAttributeHandler> attributeHandlers, Type serializableElementType, 
            string backendIdentifier, Dictionary<Type, IAttributeHandler> attributeMap, Dictionary<Type, IAssemblyPartConverter> converterMap, Type assemblyType)
        {
            _tree = tree;
            _serializableElementType = serializableElementType;
            _attributeHandlers = new List<IAttributeHandler>(attributeHandlers);
            BackendIdentifier = backendIdentifier;
            _attributeMap = attributeMap;
            _converterMap = converterMap;
            _assemblyType = assemblyType;
            _serializableElements = new List<SerializableElement>();
        }

        public SerializationTree SerializationTree
        {
            get { return _tree; }
        }

// ReSharper disable once MemberCanBePrivate.Global
        public string BackendIdentifier { [UsedImplicitly] get; private set; }

        public Assembly CreateAssembly(object destination, ValuePool values)
        {
            IEnumerable<SerializableElement> elements = ApplyAttributeHandlersToValues(values);
            IEnumerable<AssemblyPart> parts = ConvertSerializableElementsToAssemblyParts(elements);
            return InstantiateAssembly(parts);
        }

        private IEnumerable<AssemblyPart> ConvertSerializableElementsToAssemblyParts(IEnumerable<SerializableElement> elements)
        {
            return elements.Select(ConvertSerializableElementToAssemblyPart).ToList();
        }

        private AssemblyPart ConvertSerializableElementToAssemblyPart(SerializableElement element)
        {
            IAssemblyPartConverter converter = _converterMap[element.GetType()];
            if (converter == null)
            {
                throw new Exception(string.Format("No converter for the Type: {0} has been found", element.GetType().Name));
            }
            return converter.Convert(element);
        }

        private IEnumerable<SerializableElement> ApplyAttributeHandlersToValues(ValuePool values)
        {
            List<SerializableElement> elements = new List<SerializableElement>();
            foreach (var atomicType in _tree.AtomicTypes)
            {
                object value = values.GetValue(atomicType.Name);
                var serializableElement = InstantiateElement(value);
                var attributes = atomicType.Attributes;
                ApplyAttributeHandlersToValue(serializableElement, attributes);
                elements.Add(serializableElement);
            }

            return elements;
        }

        private void ApplyAttributeHandlersToValue(SerializableElement element, IEnumerable<SerializerAttribute> attributes)
        {
            //Select only attributes with the same BackendItentifier
            var matchingAttributes = from attribute in attributes
                where attribute.BackendIdentifier == BackendIdentifier
                select attribute;

            foreach (var attribute in matchingAttributes)
            {
                IAttributeHandler handler = _attributeMap[attribute.GetType()];
                if (handler.CanExecute(attribute))
                {
                    handler.Execute(ref element, attribute);
                }
            }
        }

        private SerializableElement InstantiateElement(object value)
        {
            var ctor = _serializableElementType.GetConstructor(new[] {typeof (object)});
            if (ctor != null)
            {
                var element = ctor.Invoke(new[] {value});
                return (SerializableElement) element;
            }
            else
            {
                throw new Exception("An Error occured while trying to instantiate a SerializableElement");
            }
        }

        private Assembly InstantiateAssembly(IEnumerable<AssemblyPart> parts )
        {
            var ctor = _assemblyType.GetConstructor(new[] {typeof (IEnumerable<AssemblyPart>)});
            if (ctor != null)
            {
                var assembly = ctor.Invoke(new object[] {parts});
                return (Assembly) assembly;
            }
            else
            {
                throw new Exception("An Error occurred while trying to instantiate a new Assembly");
            }
        }
    }
}
