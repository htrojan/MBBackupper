using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializerTests.Dummy
{
    [Serializer.Attributes.Serializable]
    public class SerializableClass
    {
        private byte _backingField;

        public int _foo;
        public string _bar;

        public int Test
        {
            get;
            set;
        }

// ReSharper disable once ConvertToAutoProperty
        public byte OwnBackingField
        {
            get { return _backingField; }
            set { _backingField = value; }
        }
    }
}
