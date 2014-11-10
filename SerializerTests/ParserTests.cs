using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer;
using Serializer.Annotations;
using Serializer.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializer.TypeParser;
using SerializerTests.Dummy;

// ReSharper disable once CheckNamespace
namespace Serializer.Tests
{
    [TestClass()]
    public class ParserTests
    {
        [NotNull] private readonly HashSet<Type> _atomicTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(string),
            typeof(short),
            typeof(byte)
        };


        [TestMethod()]
        public void ParseTypeTest()
        {
            var tree = TypeParser.TypeParser.ParseType(typeof(SerializableClass), _atomicTypes);
            Assert.AreEqual<int>(4, tree.AtomicTypes.Count());
            Console.WriteLine("expected: {0}", 4);
            Console.WriteLine("actual: {0}", tree.AtomicTypes.Count());
          //  Assert.Fail();
        }
    }
}
