using System;

namespace Microsoft.Azure.Commands.Common.Strategies
{
    public sealed class Property<TClass, TProperty>
        where TClass : class
    {
        public Func<TClass, TProperty> Get { get; }

        public Action<TClass, TProperty> Set { get; }

        public Property(Func<TClass, TProperty> get, Action<TClass, TProperty> set)
        {
            Get = get;
            Set = set;
        }
    }

    public static class Property
    {
        public static Property<TClass, TProperty> Create<TClass, TProperty>(
            Func<TClass, TProperty> get, Action<TClass, TProperty> set)
            where TClass : class
            => new Property<TClass, TProperty>(get, set);
    }
}
