using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationParser
{
    /// <summary>
    /// Provides methods for creating histories of
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class History<T>
    {
        private LinkedList<T> _history;
        private int _maxEntries;

        public History()
        {
            _history = new LinkedList<T>();   
        }

        public History(T currentState) : this()  
        {
            AddHistory(currentState);
        }

        public int MaxEntries
        {
            get { return _maxEntries; }
            set { _maxEntries = value; }
        }

        public void AddHistory(T history)
        {
            _history.AddFirst(history);
        }

        public void RemoveLatestHistory()
        {
            _history.RemoveFirst();
        }

        public T GetLatestHistory()
        {
            return _history.First();
        }
    }
}
