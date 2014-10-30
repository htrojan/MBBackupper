using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.Models
{
    public class PathItem
    {
        private Observable<string> _path = new Observable<string>();

        public PathItem()
        {

        }

        public string Path
        {
            get { return _path; }
            set
            {
                // if (!File.Exists(value))
                //    throw new PathDoesNotExistException(string.Format("The given path \"{0}\" does not exist!", value));
                _path.Value = value;
            }
        }

    }
}
