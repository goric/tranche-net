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
    public class RuleSet<T1,T2,T3> : Set<Rule<T1,T2,T3>>
    {
        public Func<T2,T3> First(T1 item)
        {
            foreach (var element in _values)
                if (element.Predicate(item))
                    return element.Action;
            return x => default(T3);
        }

        public Func<T2,T3> Last(T1 item)
        {
            for (var i = _values.Count; i >= 0; i--)
                if (_values[i].Predicate(item))
                    return _values[i].Action;
            return x => default(T3);
        }
    }
}
