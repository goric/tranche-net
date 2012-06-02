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
using System;
namespace TrancheLib
{
    public class IntSet : Set<int>
    {
        public Set<int> SetMultiply(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] * val;

            return this;
        }
        public Set<int> SetDivide(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] / val;

            return this;
        }
        public Set<int> SetAdd(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] + val;

            return this;
        }
        public Set<int> SetSubtract(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] - val;

            return this;
        }
        public Set<int> SetPow(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = (int)Math.Pow(_values[i], val);

            return this;
        }

        public Set<int> SetMod(int val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] % val;

            return this;
        }
    }
}
