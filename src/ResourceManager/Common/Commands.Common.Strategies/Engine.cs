using System.Linq;

namespace Microsoft.Azure.Commands.Common.Strategies
{
    /// <summary>
    /// Context for creating resources using Azure SDK for .Net.
    /// </summary>
    public sealed class Engine : IEngine
    {
        string _Subscription { get; }

        public Engine(string subscription)
        {
            _Subscription = subscription;
        }

        public string GetId<TModel>(IEntityConfig<TModel> config)
            where TModel : class
            => new[] { "subscriptions", _Subscription }
                .Concat(config.GetIdFromSubscription())
                .IdToString();
    }
}
