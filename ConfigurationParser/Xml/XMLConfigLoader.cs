using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace ConfigurationParser.Xml
{
    public class XMLConfigLoader : IConfigLoader
    {
        private XElement _document;

        public XMLConfigLoader(string xml) 
        {
            _document = XElement.Parse(xml);
        }

        public XMLConfigLoader(XmlReader reader)
        {
            _document = XElement.ReadFrom(reader).Document.Root;
        }

        public XMLConfigLoader(FileStream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                string xml = sr.ReadToEnd();
                _document = XElement.Parse(xml);
            }
        }

        public IEnumerable<string> GetSavePaths()
        {
            XElement xSavePaths = _document.Element("SavePaths");
            var SavePaths =
                from path in xSavePaths.Elements("Path")
                select path.Value;
            return SavePaths;
        }

        public string GetProfileName()
        {
            return _document.Attribute("Name").Value;
        }

        public DateTime GetDateCreated()
        {
            string time = _document.Attribute("Created").Value;
            return DateTime.Parse(time);
        }


        public Configuration GetFullConfig()
        {
            return new Configuration 
            { 
                SavePaths = GetSavePaths(),
                Name = GetProfileName(),
                Created = GetDateCreated()
            };
        }
    }
}
