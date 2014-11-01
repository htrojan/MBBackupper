using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

namespace Serializer
{
    [UsedImplicitly]
    public class Backend : IIdentifiableBackend
    {
        private Type _assembly;
        private IEnumerable<Type> _attributeHandlers;
        private IEnumerable<Type> _assemblyPartConverters;
        private Backend(IEnumerable<Type> attributeHandlers, IEnumerable<Type> assemblyPartConverters, Type assembly   )
        {
            _assembly = assembly;
            _attributeHandlers = attributeHandlers;
            _assemblyPartConverters = assemblyPartConverters;
        }

        public static Backend LoadBackend(Assembly assembly)
        {
            Type assemblyType = null;
            List<Type> attributeHandlers = new List<Type>();
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
            }

            Backend backend = new Backend(attributeHandlers, assemblyPartConverters, assemblyType);
            return backend;
        }

        public ISet<Type> GetSupportedAtomicTypes()
        {
            return null;
        }

        public ISet<Type> GetSupportedSpecialTypes()
        {
            throw new NotImplementedException();
        }

        public string BackendIdentifier {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AssemblyGenerator GetAssemblyGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
