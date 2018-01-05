using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public class Resource
    {
        public string apiVersion { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public Dictionary<string, object> properties { get; set; }

        public string[] dependsOn { get; set; }
    }
}
