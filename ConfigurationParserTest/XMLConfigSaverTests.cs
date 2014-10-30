using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationParser;
using ConfigurationParser.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace ConfigurationParser.Tests
{
    [TestClass()]
    public class XMLConfigSaverTests
    {

        [TestMethod()]
        public void SaveSavePathsTest()
        {
            string existingXml = 
                "<Profile>" +
                    "<SavePaths>" +
                        @"<Path>No1:\</Path>" +
                        @"<Path>No2:\</Path>" +
                        @"<Path>No3:\</Path>" +
                    "</SavePaths>" +
                "</Profile>";
            string expected = 
                "<Profile>"+
                    "<SavePaths>"+
                        @"<Path>C:\</Path>"+
                        @"<Path>D:\</Path>"+
                    "</SavePaths>"+
                "</Profile>";
            var toSave = new List<string> { @"C:\", @"D:\" };

            var saver = new XMLConfigSaver(existingXml);
            saver.SaveSavePaths(toSave);
            string actual = saver.ToString(SaveOptions.DisableFormatting);

            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod()]
        public void SaveNameTest()
        {
            string existingXml =
                "<Profile>" +
                    "<SavePaths>" +
                    "</SavePaths>" +
                "</Profile>";
            string expected = 
                "<Profile Name=\"Test\">" +
                    "<SavePaths>" +
                    "</SavePaths>" +
                "</Profile>";

            var saver = new XMLConfigSaver(existingXml);
            saver.SaveName("Test");

            string actual = saver.ToString(SaveOptions.DisableFormatting);
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod()]
        public void SaveDateCreatedTest()
        {
            string existingXml =
                "<Profile>" +
                    "<SavePaths>" +
                    "</SavePaths>" +
                "</Profile>";
            string expected =
                "<Profile Created=\"2014-12-07T12:00:00.0000000Z\">" +
                    "<SavePaths>" +
                    "</SavePaths>" +
                "</Profile>";

            var saver = new XMLConfigSaver(existingXml);
            saver.SaveDateCreated(new DateTime(2014, 12, 7, 12, 0, 0, DateTimeKind.Utc));

            string actual = saver.ToString(SaveOptions.DisableFormatting);
            Assert.AreEqual<string>(expected, actual);
        }
    }
}
