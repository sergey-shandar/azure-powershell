using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public static class TemplateExtensions
    {
        public static Template CreateTemplate<TModel>(
            this ResourceConfig<TModel> config,
            IState target,
            string subscriptionId,
            string location)
            where TModel : class
        {
            var context = new Context(target, subscriptionId, location);
            context.CreateResource(config);
            return new Template
            {
                resources = context.Map.Values.ToArray()
            };
        }

        sealed class Context
        {
            public IState Target { get; }

            public string SubscriptionId { get; }

            public string Location { get; }

            public ConcurrentDictionary<string, Resource> Map { get; } 
                = new ConcurrentDictionary<string, Resource>();

            public Context(IState target, string subscriptionId, string location)
            {
                Target = target;
                SubscriptionId = subscriptionId;
                Location = location;
            }

            public void CreateResource<TModel>(ResourceConfig<TModel> config)
                where TModel : class
                => Map.GetOrAdd(
                    config.DefaultIdStr(),
                    _ => new Resource
                    {
                        name = config.Name,
                        location = Location,
                        properties = Target.Get(config),
                        dependsOn = config
                            .GetTargetDependencies(Target)
                            .Select(d => d.GetId(SubscriptionId).IdToString())
                            .ToArray()
                    });
        }

        sealed class Visitor : IResourceConfigVisitor<Context, Void>
        {
            public Void Visit<TModel>(ResourceConfig<TModel> config, Context context)
                where TModel : class
            {
                context.CreateResource(config);
                return new Void();
            }
        }
    }
}
