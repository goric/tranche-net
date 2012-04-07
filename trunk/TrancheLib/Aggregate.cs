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
