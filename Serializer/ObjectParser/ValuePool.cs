

using System.Collections.Generic;

namespace Serializer.ObjectParser
{
    /// <summary>
    /// Represents the state of an object as a deep copy
    /// </summary>
    public class ValuePool
    {
        private readonly Dictionary<string, object> _values;
 
        public ValuePool()
        {
            _values = new Dictionary<string, object>();
        }

        public IEnumerable<object> Values
        {
            get { return _values.Values; }
        } 

        public void AddValue(string fieldName, object value)
        {
            _values.Add(fieldName, value);
        }
            
        public object GetValue(string fieldName)
        {
            object obj;
            _values.TryGetValue(fieldName, out obj);
            return obj;
        }

        public void DeleteValue(string fieldName)
        {
            _values.Remove(fieldName);
        }

    }
}
