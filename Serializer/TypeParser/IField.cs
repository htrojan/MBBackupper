using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;

namespace Serializer.TypeParser
{
    public interface IField
    {
        Type GetValueType();
        Object GetValue(Object obj);
        void SetValue(Object obj, Object value);
        String GetName();
    }
}
