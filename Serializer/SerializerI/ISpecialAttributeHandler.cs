using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.TypeParser;

namespace Serializer.SerializerI
{
    interface ISpecialAttributeHandler
    {
        void Execute(ref AssemblyPart part, SerializerAttribute attribute);
        bool CanExecute(SerializerAttribute attribute);
    }
}
