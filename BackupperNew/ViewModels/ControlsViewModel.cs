using BackupperNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Fields;
using Assisticant.Descriptors;
using Assisticant;
using System.Windows.Input;

namespace BackupperNew.ViewModels
{
    public class ControlsViewModel
    {
        private Profile _profile;
        private Observable<string> _path = new Observable<string>(@"C:\");

        public ControlsViewModel(Profile profile) 
        {
            this._profile = profile;
        }

        public string Path
        {
            get { return _path; }
            set { _path.Value = value; }
        }

        public ICommand AddCommand
        {
            get { return MakeCommand.Do(() => _profile.AddSavePath().Path = Path); }
        }
    }
}
