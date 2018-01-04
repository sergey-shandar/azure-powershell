using Newtonsoft.Json;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-authoring-templates
    /// </summary>
    public class Template
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; }
            = "http://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";

        public string contentVersion { get; set; }

        public Resource[] resources { get; set; }
    }
}
