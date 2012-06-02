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
using System.Linq;

namespace TrancheLib
{
    public enum AggregateMethods
    {
        Sum,
        Avg,
        Product,
        Min,
        Max
    }

    public class Aggregate<T> where T : struct
    {
        private readonly Set<T> _input;
        private readonly AggregateMethods _method;

        public Aggregate(Set<T> input, AggregateMethods m)
        {
            _input = input;
            _method = m;
        }

        public double Apply()
        {
            switch(_method)
            {
                case AggregateMethods.Sum:
                    return _input.Values.Cast<double>().Aggregate(0d, (current, item) => current + item);
                case AggregateMethods.Avg:
                    return _input.Values.Cast<double>().Average();
                case AggregateMethods.Product:
                    return _input.Values.Cast<double>().Aggregate(1d, (current, item) => current*item);
                case AggregateMethods.Max:
                    return _input.Values.Cast<double>().Max();
                case AggregateMethods.Min:
                    return _input.Values.Cast<double>().Min();
                default:
                    throw new ApplicationException("Unhandled aggregation type: " + _method);
            }
        }
    }
}
