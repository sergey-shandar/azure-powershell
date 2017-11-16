﻿using Microsoft.Azure.Management.ResourceManager.Models;
using System.Linq;

namespace Microsoft.Azure.Commands.Common.Strategies
{
    public static class StateLocation
    {
        public static string GetLocation(this IState state, IResourceConfig config)
            => config.Apply(new Visitor(state))?.Location;

        class DependencyLocation
        {
            public string Location { get; }

            public bool IsCompulsory { get; }
            
            public DependencyLocation(string location, bool isCompulsory)
            {
                Location = location;
                IsCompulsory = isCompulsory;
            }
        }

        static DependencyLocation Merge(this DependencyLocation a, DependencyLocation b)
        {
            if (a == null)
            {
                return b;
            }
            if (b == null)
            {
                return a;
            }

            if (a.IsCompulsory != b.IsCompulsory)
            {
                return a.IsCompulsory ? b : a;
            }

            // a.IsCompulsory == b.IsCompulsory
            return a.Location == b.Location ? a : new DependencyLocation(null, a.IsCompulsory);
        }

        sealed class Visitor : IResourceConfigVisitor<DependencyLocation>
        {
            public DependencyLocation Visit<Model>(ResourceConfig<Model> config) 
                where Model : class
            {
                var info = State.GetOrNull(config);
                return info != null
                    ? new DependencyLocation(
                        config.Strategy.GetLocation(info),
                        typeof(Model) != typeof(ResourceGroup))
                    : config
                        .Dependencies
                        .Select(c => c.Apply(this))
                        .Aggregate((DependencyLocation)null, Merge);
            }

            public DependencyLocation Visit<Model, ParentModel>(
                NestedResourceConfig<Model, ParentModel> config)
                where Model : class
                where ParentModel : class
                => config.Parent.Apply(this);

            public Visitor(IState state)
            {
                State = state;
            }

            IState State { get; }
        }
    }
}