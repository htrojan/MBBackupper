using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    public abstract class ConfigAccessor : IConfigAccessor
    {
        protected IConfigLoader _loader;
        protected IConfigSaver _saver;

        protected History<Configuration> _history;
        protected Configuration _currentConfig;

        public ConfigAccessor()
        {
            LoadDocument();
            _history = new History<Configuration>(_currentConfig);
        }

        private void LoadDocument()
        {
            _currentConfig = _loader.GetFullConfig();
        }

        public IEnumerable<string> SavePaths
        {
            get
            {
                return _currentConfig.SavePaths;
            }
            set
            {
                _saver.SaveSavePaths(value);
            }
        }

        public string Name
        {
            get
            {
                return _currentConfig.Name;
            }
            set
            {
                _saver.SaveName(value);
            }
        }

        public DateTime Created
        {
            get
            {
                return _currentConfig.Created;
            }
            set
            {
                _saver.SaveDateCreated(value);
            }
        }

        public virtual void SubmitChanges()
        {
            _history.AddHistory(_currentConfig);
        }
    }
}
