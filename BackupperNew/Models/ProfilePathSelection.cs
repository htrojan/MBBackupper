using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.Models
{
    public class ProfilePathSelection
    {
        private Observable<PathItem> _selection = new Observable<PathItem>();

        public PathItem Selection
        {
            get { return _selection; }
            set { _selection.Value = value; }
        }
    }
}
