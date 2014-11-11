using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Serializer.TypeParser.Tests
{
    [TestFixture()]
    public class TypeParserTests
    {
        [Test()]
        public void ParseTypeCorrectTypesParsedTest()
        {
            var supportedTypes = new HashSet<Type>
            {
                typeof (int),
                typeof (string),
                typeof (byte)
            };

            var expected = new List<Type>
            {
                typeof (int),
                typeof (string),
                typeof (int),
                typeof (byte)
            };

            SerializationTree actual = TypeParser.ParseType(typeof (DummyType), supportedTypes);
            Assert.That(actual.AtomicTypes.Select(at => at.ValueType), Is.EquivalentTo(expected));
        }
    }

    [Attributes.Serializable]
    class DummyType
    {
        private byte _backingField;

#pragma warning disable 169
        public int _foo;
        public string _bar;
#pragma warning restore 169

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
