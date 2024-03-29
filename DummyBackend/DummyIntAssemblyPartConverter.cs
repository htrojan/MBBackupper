﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.Annotations;
using Serializer.Attributes;
using Serializer.SerializerI;

namespace DummyBackend
{
    [BackendIdentifier("dummy")]
    [Mapping(typeof(int))]
    public class DummyIntAssemblyPartConverter : IAssemblyPartConverter, IIdentifiableBackend
    {
        public AssemblyPart Convert(object value)
        {
            var part = new DummyAssemblyPart {IntValue = (int)value};
            return part;
        }

        public string BackendIdentifier { get; [UsedImplicitly] private set; }
    }
}
