using Assisticant;
using BackupperNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace BackupperNew.ViewModels
{
    public class ToolbarViewModel
    {
        private Profile _profile;

        public ToolbarViewModel(Profile profile)
        {
            _profile = profile;
        }

        public ICommand SaveCommand
        {
            get
            {
                return MakeCommand.Do(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Filter = "Text documents (.txt)|*.txt"
                    };
                    Nullable<bool> result = dialog.ShowDialog();
                });
            }
        }
    }
}
