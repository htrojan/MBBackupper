using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConfigurationParser.Xml
{
    public class XMLConfigAccessor : ConfigAccessor
    {
        private FileStream _stream;
        private XMLConfigSaver _xmlSaver;
        private XMLConfigLoader _xmlLoader;

        public XMLConfigAccessor(FileStream stream)
        {
            _xmlLoader = new XMLConfigLoader(stream);
            _xmlSaver = new XMLConfigSaver();
            _stream = stream;

            //Cast xmlConfig-types into generic config-types
            //for letting the abstract base class taking care
            //of the properties
            _saver = _xmlSaver;
            _loader = _xmlLoader;
        }

        public override void SubmitChanges()
        {
            base.SubmitChanges();
            _xmlSaver.WriteToFile(_stream);
        }
    }
}
