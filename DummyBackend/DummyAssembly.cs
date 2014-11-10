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
    public class DummyAssembly : Assembly, IIdentifiableBackend
    {
        public DummyAssembly(List<AssemblyPart> parts) : base(parts)
        {
        }

        public override void Serialize(object destination)
        {
            throw new NotImplementedException();
        }

        public string BackendIdentifier { get; [UsedImplicitly] private set; }
    }
}
