using System;
using System.Collections.Concurrent;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public class TemplateEngine : IEngine
    {
        public string GetId(IEntityConfig config)
            => "[concat(resourceGroup().id, '" + config.GetProvidersId().IdToString() + "')]";

        public string GetSecureString(string name, string secret)
        {
            SecureStrings.AddOrUpdate(
                name,
                secret,
                (n, s) => 
                {
                    throw new Exception("The template parameter '" + name + "' already exists.");
                });
            return secret;
        }

        public ConcurrentDictionary<string, string> SecureStrings { get; }
            = new ConcurrentDictionary<string, string>();

        public TemplateEngine() { }
    }
}
