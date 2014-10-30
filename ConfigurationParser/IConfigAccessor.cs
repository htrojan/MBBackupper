using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    /// <summary>
    /// This Interface defines a wrapper for the 
    /// IConfigLoader and IConfigSaver interfaces into a 
    /// single class with handy properties.\n
    /// 
    /// All changes are maintained in memory and have to 
    /// be submitted through the SubmitChanges() method
    /// </summary>
    public interface IConfigAccessor
    {
        IEnumerable<string> SavePaths
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        DateTime Created
        {
            get;
            set;
        }

        /// <summary>
        /// Submits the changes made in memory to the real world
        /// and throws errors if something went wrong
        /// </summary>
        /// <returns></returns>
        void SubmitChanges();
    }
}
