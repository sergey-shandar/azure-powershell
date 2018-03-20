using System;
using System.Collections.Concurrent;
using System.Security;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public class TemplateEngine : IEngine
    {
        public string GetId(IEntityConfig config)
            => "[concat(resourceGroup().id, '" + config.GetProvidersId().IdToString() + "')]";

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

        public TemplateEngine() { }
    }
}
