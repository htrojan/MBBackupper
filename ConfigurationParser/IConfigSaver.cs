using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    public interface IConfigSaver
    {
        void SaveSavePaths(IEnumerable<string> paths);
        void SaveName(string name);
        void SaveDateCreated(DateTime date);
    }
}
