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
    public abstract class AssemblyGenerator : IIdentifiableBackend
    { 
        private readonly SerializationTree _tree;
        private readonly Dictionary<Type, IAttributeHandler> _attributeMap;
        private readonly Dictionary<Type, IAssemblyPartConverter> _converterMap;
        private readonly Type _assemblyType;

        protected AssemblyGenerator(AssemblyGeneratorParams assemblyGeneratorParams)
        {
            _tree = assemblyGeneratorParams.Tree;
            BackendIdentifier = assemblyGeneratorParams.BackendIdentifier;
            _attributeMap = assemblyGeneratorParams.AttributeMap;
            _converterMap = assemblyGeneratorParams.ConverterMap;
            _assemblyType = assemblyGeneratorParams.AssemblyType;
        }

        public SerializationTree SerializationTree
        {
            get { return _tree; }
        }

// ReSharper disable once MemberCanBePrivate.Global
        public virtual string BackendIdentifier { [UsedImplicitly] get; private set; }

        public Assembly CreateAssembly(ValuePool values)
        {
            IEnumerable<AssemblyPart> parts = ApplyAttributeHandlersToValues(values);
            return InstantiateAssembly(parts);
        }

        private IEnumerable<AssemblyPart> ApplyAttributeHandlersToValues(ValuePool values)
        {
            List<AssemblyPart> elements = new List<AssemblyPart>();
            foreach (var atomicType in _tree.AtomicTypes)
            {
                object value = values.GetValue(atomicType.Name);
                var assemblyPart = _converterMap[value.GetType()].Convert(value);
                var attributes = atomicType.Attributes;
                ApplyAttributeHandlersToValue(assemblyPart, attributes);
                elements.Add(assemblyPart);
            }

            return elements;
        }

        private void ApplyAttributeHandlersToValue(AssemblyPart element, IEnumerable<SerializerAttribute> attributes)
        {

            foreach (var attribute in attributes)
            {
                IAttributeHandler handler = _attributeMap[attribute.GetType()];
                if (handler.CanExecute(attribute))
                {
                    handler.Execute(ref element, attribute);
                }
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
