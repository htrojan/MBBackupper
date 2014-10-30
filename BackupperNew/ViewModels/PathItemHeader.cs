using BackupperNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.ViewModels
{
    public class PathItemHeader
    {
        private readonly PathItem _pathItem;

        public PathItemHeader(PathItem pathItem)
        {
            _pathItem = pathItem;
        }

        public PathItem PathItem
        {
            get { return _pathItem; }
        }

        public string Name
        {
            get { return _pathItem.Path ?? "ERROR"; }
        }
    }
}
