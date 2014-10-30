using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.Exceptions
{
    public class PathDoesNotExistException : Exception
    {
        public PathDoesNotExistException(string message) : base(message) { }

        public PathDoesNotExistException() : base() { }
    }
}
