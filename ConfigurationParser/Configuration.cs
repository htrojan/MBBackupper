using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    public class Configuration : ICloneable
    {
        public IEnumerable<string> SavePaths
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }


        public object Clone()
        {
            Configuration cfg = new Configuration();
            cfg.Name = Name;
            cfg.Created = Created;
            cfg.SavePaths =
                from path in SavePaths
                select path;

            return cfg;
        }
    }
}
