using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class SerializableAttribute : Attribute
    {
        public SerializableAttribute()
        {

        }
    }
}
