﻿using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace Microsoft.Azure.Experiments.Network
{
    public static class NetworkInterfacePolicy
    {
        public static ResourcePolicy<NetworkInterface> Policy { get; }
            = NetworkPolicy.Create(
                "networkInterfaces",
                client => client.NetworkInterfaces,
                p => p.Operations.GetAsync(
                    p.ResourceGroupName, p.Name, null, p.CancellationToken),
                p => p.Operations.CreateOrUpdateAsync(
                    p.ResourceGroupName, p.Name, p.Config, p.CancellationToken));

        public static ResourceConfig<NetworkInterface> CreateNetworkInterfaceConfig(
            this ResourceConfig<ResourceGroup> resourceGroup,
            string name,
            NestedResourceConfig<Subnet, VirtualNetwork> subnet,
            ResourceConfig<PublicIPAddress> publicIPAddress,
            ResourceConfig<NetworkSecurityGroup> networkSecurityGroup)
            => Policy.CreateConfig(
                resourceGroup,
                name,
                subscription => new NetworkInterface
                {
                    IpConfigurations = new []
                    {
                        new NetworkInterfaceIPConfiguration
                        {
                            Name = name,
                            Subnet = new Subnet { Id = subnet.GetId(subscription).IdToString() },
                            PublicIPAddress = new PublicIPAddress
                            {
                                Id = publicIPAddress.GetId(subscription).IdToString()
                            }
                        }
                    },
                    NetworkSecurityGroup = new NetworkSecurityGroup
                    {
                        Id = networkSecurityGroup.GetId(subscription).IdToString()
                    }
                },
                new IResourceConfig[] { subnet, publicIPAddress, networkSecurityGroup });
    }
}
