﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Common.Strategies
{
    public sealed class ResourceType
    {       
        public static ResourceType ResourceGroup { get; }
            = new ResourceType(null, ResourceId.ResourceGroups);

        /// <summary>
        /// A resource type namespace, for example 'Microsoft.Network'.
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// A resource type provider, for example 'virtualNetworks'.
        /// </summary>
        public string Provider { get; } 

        public ResourceType(string namespace_, string provider)
        {
            Namespace = namespace_;
            Provider = provider;
        }
    }
}
