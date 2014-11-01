using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Attributes
{
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
