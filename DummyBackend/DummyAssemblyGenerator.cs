using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.SerializerI;

namespace DummyBackend
{
    public class DummyAssemblyGenerator : AssemblyGenerator
    {
        public DummyAssemblyGenerator(AssemblyGeneratorParams assemblyGeneratorParams) : base(assemblyGeneratorParams)
        {
        }
    }
}
