using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.SerializerI;

namespace XmlBackend
{
    class XmlAssembly : Assembly
    {
        public XmlAssembly(List<AssemblyPart> parts) : base(parts)
        {
        }

        public override void Serialize(object destination)
        {
            
        }
    }
}
