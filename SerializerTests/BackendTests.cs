using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serializer.SerializerI;
using Assembly = System.Reflection.Assembly;

namespace Serializer.Tests
{
    [TestClass()]
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

        [TestMethod()]
        public void LoadBackendAssemblyTypeTest()
        {
            Backend backend = Backend.LoadBackend(_assembly);   
            Assert.AreEqual(typeof(DummyBackend.DummyAssembly), backend.GetAssemblyType());
        }

        [TestMethod()]
        public void LoadBackendAssemblyPartConvertersTest() 
        {
            var backend = Backend.LoadBackend(_assembly);
            List<Type> expectedTypes = new List<Type>()
            {
                typeof(DummyBackend.DummyIntAssemblyPartConverter)
            };
            CollectionAssert.AreEqual(expectedTypes, new List<Type>(backend.GetAssemblyPartConverters()));
        }

        [TestMethod()]
        public void LoadBackendAttributeHandlersTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            List<Type> expectedTypes = new List<Type>()
            {
                typeof(DummyBackend.DummyAttributeHandler)
            };
            CollectionAssert.AreEqual(expectedTypes, new List<Type>(backend.GetAttributeHandlers()));
        }

        [TestMethod()]
        public void LoadBackendAssemblyGeneratorTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            Assert.AreEqual(typeof(DummyBackend.DummyAssemblyGenerator), backend.GetAssemblyGeneratorType());
        }

        [TestMethod()]
        public void GetSupportedAtomicTypesTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            ISet<Type> expectedSupportedAtomicTypes = new HashSet<Type>()
            {
                typeof(int)
            };
            CollectionAssert.AreEqual(expectedSupportedAtomicTypes.ToList(), backend.GetSupportedAtomicTypes().ToList());
        }

        [TestMethod()]
        public void GetSupportedSpecialTypesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAttributeHandlerMapAllTypesContainedTest()
        {
            var backend = Backend.LoadBackend(_assembly);
            var expected = new List<Type>()
            {
                typeof(int)
            };
            CollectionAssert.AreEqual(expected, backend.GetAttributeHandlerMap().Keys);
        }

        [TestMethod()]
        public void GetAssemblyPartConverterMapTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAssemblyGeneratorInstanceTest()
        {
            Assert.Fail();
        }


    }
}
