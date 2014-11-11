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
        public void ParseContainsCorrectValueTest()
        {
            var tree = Substitute.For<SerializationTree>();
            var atomicType = Substitute.For<AtomicType>(new object[]{null});
            atomicType.Name.Returns("Dummy");
            atomicType.GetValue(Arg.Any<Object>()).Returns(5);

            var atomicTypes = new List<AtomicType> {atomicType};

            tree.AtomicTypes.Returns(atomicTypes);
            

            ValuePool result = ObjectParser.Parse(tree, new DummyObj(){Dummy = 5});
            Assert.That(result.Values, Contains.Item(5));
        }

        [Test]
        public void ParseContainsCorrectMappingTset()
        {
            var tree = Substitute.For<SerializationTree>();
            var atomicType = Substitute.For<AtomicType>(new object[] { null });
            atomicType.Name.Returns("Dummy");
            atomicType.GetValue(Arg.Any<Object>()).Returns(5);

            var atomicTypes = new List<AtomicType> { atomicType };

            tree.AtomicTypes.Returns(atomicTypes);
            ValuePool result = ObjectParser.Parse(tree, new DummyObj() { Dummy = 5 });
            Assert.That(result.GetValue("Dummy"), Is.EqualTo(5));
        }
    }

    class DummyObj
    {
        public int Dummy { get; set; }
    }
}
