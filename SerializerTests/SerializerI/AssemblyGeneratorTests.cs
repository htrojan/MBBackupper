using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DummyBackend;
using NSubstitute;
using Serializer.SerializerI;
using NUnit.Framework;
using Serializer.Attributes;
using Serializer.ObjectParser;
using Serializer.TypeParser;

// ReSharper disable once CheckNamespace
namespace Serializer.SerializerI.Tests
{
    [TestFixture()]
    public class AssemblyGeneratorTests
    {
        private const string AssemblyPath = @"DummyBackend.dll";
        private Backend _backend;

        [TestFixtureSetUp]
        public void Setup()
        {
            _backend = Backend.LoadBackend(System.Reflection.Assembly.LoadFrom(AssemblyPath));
        }

        [Test]
        public void CreateAssemblyTest()
        {
            var tree = TypeParser.TypeParser.ParseType(typeof (DummyType), _backend.GetSupportedAtomicTypes());
            var values = ObjectParser.ObjectParser.Parse(tree, new DummyType(){Test = 5});
            string backendId = _backend.BackendIdentifier;
            var generatorParams = new AssemblyGeneratorParams(tree, backendId, _backend.GetAttributeHandlerMap(),
                _backend.GetAssemblyPartConverterMap(), _backend.GetAssemblyType());
            AssemblyGenerator generator = Substitute.For<AssemblyGenerator>(new object[]{generatorParams});
            generator.BackendIdentifier.Returns("dummy");
            Assembly actualAssembly = generator.CreateAssembly(values);

            Assert.That(actualAssembly.Parts.Count(), Is.EqualTo(1));
            Assert.That(List.Map(new List<AssemblyPart>(actualAssembly.Parts)).Property("Value"), Contains.Item(5));
        }

    }

    [Attributes.Serializable]
    class DummyType
    {
        public int Test { get; set; }
    }
}
