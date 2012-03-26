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
