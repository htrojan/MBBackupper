using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    /// <summary>
    /// This interface defines all methods needed to retain the 
    /// needed information out of the given configuration file.
    /// </summary>
    public interface IConfigLoader
    {
        IEnumerable<string> GetSavePaths();
        string GetProfileName();
        DateTime GetDateCreated();

        Configuration GetFullConfig();
    }
}
