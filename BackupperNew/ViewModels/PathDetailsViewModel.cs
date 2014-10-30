using BackupperNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.ViewModels
{
    public class PathDetailsViewModel
    {
        private PathItem _pathItem;

        public PathDetailsViewModel(PathItem pathItem)
        {
            this._pathItem = pathItem;
        }

        public string Path
        {
            get { return _pathItem.Path; }
            set { _pathItem.Path = value; }
        }
    }
}
