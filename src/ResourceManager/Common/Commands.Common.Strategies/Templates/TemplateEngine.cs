using System;
using System.Collections.Concurrent;
using System.Security;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public class TemplateEngine : IEngine
    {
        IClient _client { get; }

        public TemplateEngine(IClient client)
        {
            _client = client;
        }

        public string GetId(IEntityConfig config)
        //    => "[concat(resourceGroup().id, '" + config.GetProvidersId().IdToString() + "')]";        
        {
            var res = config.Resource;
            return "[reference('/" 
                + res.Strategy.Type.Namespace 
                + "/"
                + res.Strategy.Type.Provider
                + "/"
                + res.Name
                + "', '"
                + res.Strategy.GetApiVersion(_client) + 
                "').id]";
        }

        sealed class Visitor : IEntityConfigVisitor<TemplateEngine, string>
        {
            public string Visit<TModel>(ResourceConfig<TModel> config, TemplateEngine context)
                where TModel : class
                => "reference('/"
                    + config.Strategy.Type.Namespace
                    + "/"
                    + config.Strategy.Type.Provider
                    + "/"
                    + config.Name
                    + "', '"
                    + config.Strategy.GetApiVersion(context._client) +
                    "')";

            public string Visit<TModel, TParentModel>(
                NestedResourceConfig<TModel, TParentModel> config, TemplateEngine context)
                where TModel : class
                where TParentModel : class
                => config.Parent.Accept(this, context) 
                    + "." 
                    + config.Strategy.Provider 
                    + "[" + config.Name + "]";
        }

        public string GetSecureString(string name, SecureString secret)
        {
            SecureStrings.AddOrUpdate(
                name,
                secret,
                (n, s) => 
                {
                    throw new Exception("The template parameter '" + name + "' already exists.");
                });
            return "[parameters('" + name + "')]";
        }

        public ConcurrentDictionary<string, SecureString> SecureStrings { get; }
            = new ConcurrentDictionary<string, SecureString>();
    }
}
