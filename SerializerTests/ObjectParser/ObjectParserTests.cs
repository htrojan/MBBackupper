using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Serializer.ObjectParser;
using Serializer.TypeParser;

// ReSharper disable once CheckNamespace
namespace Serializer.ObjectParser.Tests
{
    [TestFixture()]
    public class ObjectParserTests
    {
     
        [Test()]
        public void ParseTest()
        {
            var tree = Substitute.For<SerializationTree>();
            FieldInfo info = null;
            var atomicType = Substitute.For<AtomicType>();

            atomicType.GetValue(Arg.Any<Object>()).Returns(5);
            atomicType.Name.Returns("Dummy");

            tree.AtomicTypes.Returns(new List<AtomicType>() {atomicType});

            ValuePool result = ObjectParser.Parse(tree, new DummyObj(){Dummy = 5});
            Assert.That(result.Values, Contains.Item(5));
        }
    }

    class DummyObj
    {
        public int Dummy { get; set; }
    }
}
