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
    public class Rule<T1, T2, T3>
    {
        public Predicate<T1> Predicate { get; set; }
        public Func<T2, T3> Action { get; set; }

        public Rule(Predicate<T1> pred, Func<T2,T3> action = null)
        {
            Predicate = pred;
            Action = action;
        }

        public void SetAction(Func<T2,T3> f)
        {
            Action = f;
        }

        public bool InvokePredicate(T1 item)
        {
            return Predicate.Invoke(item);
        }

        public T3 InvokeAction(T2 item)
        {
            if (Action == null)
                throw new ApplicationException("Action is unititialized");

            return Action(item);
        }
    }
}
