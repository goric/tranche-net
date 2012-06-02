/*  
 * tranche.NET - a DSL for modeling structured finance products.
 * Copyright (C) 2012 Timothy Goric
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
*/
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
