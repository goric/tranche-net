using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TrancheLib
{
    public class Set<T>
    {
        public ReadOnlyCollection<T> Values { get { return _values.AsReadOnly(); } }
        protected readonly List<T> _values;
        public T this[int pos] { get { return _values[pos]; } }

        public Set()
        {
            _values = new List<T>();
        }

        public Set(IEnumerable<T> initialValues)
        {
            _values = new List<T>(initialValues);
        }

        public Set<T> Cons(Set<T> set, T value)
        {
            _values.Add(value);
            return this;
        }

        public Set<T> Remove(T item)
        {
            _values.Remove(item);
            return this;
        }

        public Set<T> RemoveAt(int index)
        {
            _values.RemoveAt(index);
            return this;
        }
    }
}
