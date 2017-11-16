﻿using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace Microsoft.Azure.Commands.Common.Strategies.Network
{
    public static class PublicIPAddressStrategy
    {
        public static ResourceStrategy<PublicIPAddress> Strategy { get; }
            = NetworkStrategy.Create(
                "publicIPAddresses",
                client => client.PublicIPAddresses,
                p => p.Operations.GetAsync(
                    p.ResourceGroupName, p.Name, null, p.CancellationToken),
                p => p.Operations.CreateOrUpdateAsync(
                    p.ResourceGroupName, p.Name, p.Model, p.CancellationToken));

        public static ResourceConfig<PublicIPAddress> CreatePublicIPAddressConfig(
            this ResourceConfig<ResourceGroup> resourceGroup, string name)
            => Strategy.CreateConfig(resourceGroup, name);
    }
}
