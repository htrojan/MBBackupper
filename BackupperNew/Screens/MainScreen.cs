using Assisticant;
using BackupperNew.Models;
using BackupperNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupperNew.Screens
{
    public class MainScreen : ViewModelLocatorBase
    {
        private Profile _profile;
        private ProfilePathSelection _selection;

        public MainScreen()
        {
            _profile = new Profile();
            _selection = new ProfilePathSelection();
            if (DesignMode)
            {
                CreateDesignModeObjects();
            }
            else
            {
                CreateRealObjects();
            }
        }

        public object Main
        {
            get { return ViewModel(() => new MainViewModel(_profile, _selection)); }
        }

        public object PathDetails
        {
            get
            {
                return ViewModel(() => _selection.Selection == null
                    ? new PathDetailsViewModel(new PathItem())
                    : new PathDetailsViewModel(_selection.Selection));
            }
        }

        public object Controls
        {
            get { return ViewModel(() => new ControlsViewModel(_profile)); }
        }

        public object Toolbar
        {
            get { return ViewModel(() => new ToolbarViewModel(_profile)); }
        }

        [Obsolete("Only for UI designing purposes")]
        public object PathDetailsDesign
        {
            get 
            {
                PathItem item = new PathItem();
                item.Path = @"E:\Design";
                return ViewModel(
                    () => new PathDetailsViewModel(item)); 
            }
        }

        private void CreateDesignModeObjects() 
        {
            _profile.AddSavePath().Path = @"C:\Design";
            _profile.AddSavePath().Path = @"D:\Design";
            _profile.Name = "I'm a profile";
        }

        private void CreateRealObjects()
        {
            _profile.AddSavePath().Path = @"C:\Real";
            _profile.AddSavePath().Path = @"D:\Real";
            _profile.AddSavePath().Path = @"E:\Real";
        }
    }
}
