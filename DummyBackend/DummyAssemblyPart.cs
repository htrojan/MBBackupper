using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;
using Serializer.Attributes;
using Serializer.SerializerI;

namespace DummyBackend
{
    [BackendIdentifier("dummy")]
    public class DummyAssemblyPart : AssemblyPart, IIdentifiableBackend
    {
        public string BackendIdentifier { get; [UsedImplicitly] private set; }

        public int IntValue
        {
            get { return (int) _value; }
            set { _value = value; }
        }
    }
}
