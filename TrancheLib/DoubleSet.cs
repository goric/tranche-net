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
    public class DoubleSet : Set<double>
    {
        public Set<double> SetMultiply(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] * val;

            return this;
        }
        public Set<double> SetDivide(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] / val;

            return this;
        }
        public Set<double> SetAdd(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] + val;

            return this;
        }
        public Set<double> SetSubtract(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] - val;

            return this;
        }
        public Set<double> SetPow(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = Math.Pow(_values[i], val);

            return this;
        }

        public Set<double> SetMod(double val)
        {
            if (_values.Count == 0)
                return this;

            for (var i = 0; i < _values.Count; i++)
                _values[i] = _values[i] % val;

            return this;
        }
    }
}
