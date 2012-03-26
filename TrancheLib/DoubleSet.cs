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
