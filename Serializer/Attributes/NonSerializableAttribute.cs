using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;

namespace Serializer.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple=false, Inherited=true), UsedImplicitly]
    public class NonSerializableAttribute : Attribute
    {
        public NonSerializableAttribute()
        {

        }
    }
}
