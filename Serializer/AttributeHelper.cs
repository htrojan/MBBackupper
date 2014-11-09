using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Attributes;

namespace Serializer
{
    public static class AttributeHelper
    {
        public static BackendIdentifierAttribute GetBackendIdentifierAttribute(Type type)
        {
            BackendIdentifierAttribute backendIdentifier = (BackendIdentifierAttribute)
                   Attribute.GetCustomAttribute(type, typeof(BackendIdentifierAttribute));
            if (backendIdentifier == null)
            {
                throw new Exception(string.Format("The Type {0} has no attached backendIdentifier attribute", type.FullName));
            }
            return backendIdentifier;
        }
    }
}
