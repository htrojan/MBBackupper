using Assisticant.Collections;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.Models
{
    public class Profile
    {
        Observable<string> _name = new Observable<string>("unnamed");
        ObservableList<PathItem> _pathItems = new ObservableList<PathItem>();
        Observable<DateTime> _dateCreated = new Observable<DateTime>();

        public string Name
        {
            get { return _name; }
            set { _name.Value = value; }
        }

        public IEnumerable<PathItem> PathItems
        {
            get { return _pathItems; }
        }

        public PathItem AddSavePath()
        {
            PathItem pathItem = new PathItem();
            _pathItems.Add(pathItem);
            return pathItem;
        }

        public void DeleteSavePath(PathItem pathItem)
        {
            _pathItems.Remove(pathItem);
        }
    }
}
