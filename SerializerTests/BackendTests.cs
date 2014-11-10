using System;
using System.Collections.Generic;
using System.Linq;
using DummyBackend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

// ReSharper disable once CheckNamespace
namespace Serializer.Tests
{
    [TestClass]
    public class BackendTests
    {
        private const string AssemblyPath = @"DummyBackend.dll";
        private Assembly _assembly;

        [TestInitialize]
        public void Setup()
        {
           // _assembly = Assembly.LoadFile(AssemblyPath);
            _assembly = Assembly.LoadFrom(AssemblyPath);
           // Console.Out.WriteLine("{0}", _assembly.FullName);
        }

        [TestMethod]
        public void LoadBackendAssemblyTypeTest()
        {
            Backend backend = Backend.LoadBackend(_assembly);   
            Assert.AreEqual(typeof(DummyAssembly), backend.GetAssemblyType());
        }

        [TestMethod]
        public void LoadBackendAssemblyPartConvertersTest() 
        {
            var backend = Backend.LoadBackend(_assembly);
            List<Type> expectedTypes = new List<Type>
            {
                typeof(DummyIntAssemblyPartConverter)
            };
            CollectionAssert.AreEqual(expectedTypes, new List<Type>(backend.GetAssemblyPartConverters()));
        }

        [TestMethod]
        public void LoadBackendAttributeHandlersTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            List<Type> expectedTypes = new List<Type>
            {
                typeof(DummyAttributeHandler)
            };
            CollectionAssert.AreEqual(expectedTypes, new List<Type>(backend.GetAttributeHandlers()));
        }

        [TestMethod]
        public void LoadBackendAssemblyGeneratorTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            Assert.AreEqual(typeof(DummyAssemblyGenerator), backend.GetAssemblyGeneratorType());
        }

        [TestMethod]
        public void GetSupportedAtomicTypesTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            ISet<Type> expectedSupportedAtomicTypes = new HashSet<Type>
            {
                typeof(int)
            };
            var supTypes = backend.GetSupportedAtomicTypes();
            CollectionAssert.AreEqual(expectedSupportedAtomicTypes.ToList(), backend.GetSupportedAtomicTypes().ToList());
        }

        [TestMethod]
        public void GetAttributeHandlerMapAllKeysContainedTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = new List<Type>
            {
                typeof(DummyAttribute)
            };
            CollectionAssert.AreEqual(expected, backend.GetAttributeHandlerMap().Keys);
        }

        [TestMethod]
        public void GetAttributeHandlerMapAllValuesContainedTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = new List<Type>
            {
                typeof(DummyAttributeHandler)
            };
            var actual = from attributeHandler in backend.GetAttributeHandlerMap()
                select attributeHandler.Value.GetType();
            
            CollectionAssert.AreEqual(expected, new List<Type>(actual));
        }

        [TestMethod]
        public void GetAttributeHandlerMapCorrectMapping()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = typeof (DummyAttributeHandler);
            var map = backend.GetAttributeHandlerMap();
            var actual = map[typeof (DummyAttribute)].GetType();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAssemblyPartConverterMapAllKeysContainedTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = new List<Type>
            {
                typeof(int)
            };

            CollectionAssert.AreEqual(expected, backend.GetAssemblyPartConverterMap().Keys);
        }

        [TestMethod]
        public void GetAssemblyPartConverterMapAllValuesContainedTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = new List<Type>
            {
                typeof(DummyIntAssemblyPartConverter)
            };

            var actual = from converter in backend.GetAssemblyPartConverterMap().Values 
                    select converter.GetType();

            CollectionAssert.AreEqual(expected, new List<Type>(actual));
        }

        [TestMethod]
        public void GetAssemblyPartConverterMapCorrectMappingTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            Assert.AreEqual(typeof(DummyIntAssemblyPartConverter), backend.GetAssemblyPartConverterMap()[typeof(int)].GetType());
        }

        [TestMethod]
        public void GetAssemblyGeneratorInstanceTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            AssemblyGeneratorParams mockParams = new AssemblyGeneratorParams(null, null, null, null, null);
            Assert.IsInstanceOfType(backend.GetAssemblyGeneratorInstance(mockParams), typeof(AssemblyGenerator));
        }


    }
}
