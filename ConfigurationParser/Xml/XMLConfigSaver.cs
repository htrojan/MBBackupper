using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace ConfigurationParser.Xml
{
    public class XMLConfigSaver : IConfigSaver
    {
        private XElement _document;

        public XMLConfigSaver()
        {
            _document = BuildDocumentStructure();
        }

        public XMLConfigSaver(string existingXml) 
        {
            _document = XElement.Parse(existingXml);
        }

        public void SaveSavePaths(IEnumerable<string> paths)
        {
            if (_document.Element("SavePaths") == null)
                _document.Add(new XElement("SavePaths"));
            XElement currentPaths = _document.Element("SavePaths");
            currentPaths.RemoveNodes();
            foreach (var path in paths)
            {
                currentPaths.Add(new XElement("Path", path));
            }
        }

        public void SaveName(string name)
        {
            AddAttributeIfExists(_document, "Name", name);
        }

        public void SaveDateCreated(DateTime date)
        {
            AddAttributeIfExists(_document, "Created", date.ToString("o"));
        }

        private void AddAttributeIfExists(XElement element, string attributeName, string value)
        {
            if (element.Attribute(attributeName) != null)
            {
                element.Attribute(attributeName).Value = value;
            }
            else
            {
                element.Add(new XAttribute(attributeName, value));
            }
        }

        private XElement BuildDocumentStructure()
        {
            XElement structure = 
                new XElement("Profile",
                    new XAttribute("Name", "unnamed"),
                    new XAttribute("Created", DateTime.UtcNow),
                    new XElement("SavePaths")
                );
            return structure;
        }

        public override string ToString()
        {
            return _document.ToString();
        }

        public string ToString(SaveOptions options)
        {
            return _document.ToString(options);
        }

        public void WriteToFile(FileStream stream)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ToString());

            using (StreamWriter outfile = new StreamWriter(stream))
            {
                outfile.Write(builder.ToString());
            } 
        }
    }
}
