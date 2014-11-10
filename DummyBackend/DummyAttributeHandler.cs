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
    [Mapping(typeof(DummyAttribute))]
    [BackendIdentifier("dummy")]
    public class DummyAttributeHandler : IAttributeHandler
    {

        public string BackendIdentifier { get; private set; }

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
