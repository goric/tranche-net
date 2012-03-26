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
