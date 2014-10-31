﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MappingAttribute : Attribute
    {

        private Type _targetType;

        public MappingAttribute(Type targetType)
        {
            _targetType = targetType;
        }
    }
}