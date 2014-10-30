using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections;
using ConfigurationParser.Xml;

namespace ConfigurationParser.Tests
{
    [TestClass()]
    public class XMLConfigLoaderTests
    {
        private XMLConfigLoader _loader;

        [TestInitialize]
        public void Setup()
        {
            _loader = new XMLConfigLoader
                ("<Profile Name=\"unnamed\"" +
                    " Created=\"2008-01-02T07:00:00\"> "+
                     @"<SavePaths>
                        <Path>C:\</Path>
                        <Path>D:\</Path>
                    </SavePaths>
                </Profile>"
                );
        }

        [TestMethod()]
        public void GetSavePathsTest()
        {
            var expected = (ICollection) new List<string> {@"C:\", @"D:\" };
            var actual = (ICollection) _loader.GetSavePaths().ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void GetProfileNameTest()
        {
            string expected = "unnamed";
            string actual = _loader.GetProfileName();

            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod()]
        public void GetDateCreatedTest()
        {
            DateTime expected = new DateTime(2008, 1, 2, 7, 0, 0);
            DateTime actual = _loader.GetDateCreated();

            Assert.AreEqual<DateTime>(expected, actual);
        }
    }
}
