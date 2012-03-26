using System;

namespace TrancheLib
{
    public class Rule<T1, T2, T3>
    {
        public Predicate<T1> Predicate { get; set; }
        public Func<T2, T3> Action { get; set; }

        public Rule(Predicate<T1> pred, Func<T2,T3> action)
        {
            Predicate = pred;
            Action = action;
        }

        public bool InvokePredicate(T1 item)
        {
            return Predicate.Invoke(item);
        }

        public T3 InvokeAction(T2 item)
        {
            return Action(item);
        }
    }
}
