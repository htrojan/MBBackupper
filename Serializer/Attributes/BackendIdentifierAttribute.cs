using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;

namespace Serializer.Attributes
{
    [UsedImplicitly]
    public class BackendIdentifierAttribute : Attribute
    {
        private readonly string _backendIdentifier;

        public BackendIdentifierAttribute(string backendIdentifier)
        {
            _backendIdentifier = backendIdentifier;
        }

        public String GetBackendIdentifier()
        {
            return _backendIdentifier;
        }
    }
}
