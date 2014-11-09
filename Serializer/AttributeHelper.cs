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
            return GetAttribute<BackendIdentifierAttribute>(type) as BackendIdentifierAttribute;
        }

        public static MappingAttribute GetMappingAttribute(Type type)
        {
            //allowed as only one mappingAttribute per class is allowed
            return GetAttribute<MappingAttribute> (type) as MappingAttribute;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Always has to be of type Attribute</typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Attribute GetAttribute<T>(Type type)
        {
            Attribute attribute = Attribute.GetCustomAttribute(type, typeof (T));
            if (attribute == null)
            {
                throw new Exception(string.Format("The type {0} does not contain the attribute {1}", type.FullName, T));
            }
            return attribute;
        }
    }
}
