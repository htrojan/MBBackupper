using BackupperNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.ViewModels
{
    public class MainViewModel
    {
        private Profile _profile;
        private ProfilePathSelection _pathSelection;

        public MainViewModel(Profile profile, ProfilePathSelection pathSelection)
        {
            this._profile = profile;
            this._pathSelection = pathSelection;
        }

        public IEnumerable<PathItemHeader> Paths
        {
            get
            {
                return
                    from item in _profile.PathItems
                    select new PathItemHeader(item);
            }
        }

        public PathItemHeader SelectedItem
        {
            get
            {
                return _pathSelection.Selection == null
                    ? null
                    : new PathItemHeader(_pathSelection.Selection);
            }
            set
            {
                if (value != null)
                {
                    _pathSelection.Selection = value.PathItem;
                }
            }
        }
    }
}
