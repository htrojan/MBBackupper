﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

namespace Serializer
{
    public class Backend : IIdentifiableBackend
    {
        private Backend()
        {
            
        }

        public static Backend LoadBackend(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        ISet<Type> GetSupportedAtomicTypes()
        {
            throw new NotImplementedException();
        }

        ISet<Type> GetSupportedSpecialTypes()
        {
            throw new NotImplementedException();
        }

        public string BackendIdentifier {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}