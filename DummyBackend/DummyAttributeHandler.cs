using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Attributes;
using Serializer.SerializerI;
using Serializer.TypeParser;

namespace DummyBackend
{
    [Mapping(typeof(int))]
    [BackendIdentifier("dummy")]
    public class DummyAttributeHandler : IAttributeHandler
    {
        //The field is initialized with reflection
#pragma warning disable 649
        private string _backendIdentifier;
#pragma warning restore 649

        public string BackendIdentifier
        {
            get { return _backendIdentifier; }
        }

        public void Execute(ref AssemblyPart element, SerializerAttribute attribute)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(SerializerAttribute attribute)
        {
            throw new NotImplementedException();
        }
    }
}
