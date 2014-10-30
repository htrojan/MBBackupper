using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple=false, Inherited=true)]
    public class NonSerializableAttribute : Attribute
    {
        public NonSerializableAttribute()
        {

        }
    }
}
