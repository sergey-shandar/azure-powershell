﻿using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using System;
using System.Threading.Tasks;

namespace Azure.Experiments
{
    public sealed class PublicIpAddressObject :
        ResourceObject<PublicIPAddress>
    {
        public PublicIpAddressObject(
            INetworkManagementClient client,
            string name,
            ResourceGroupObject rg)
            : base(name, rg)
        {
            Client = client.PublicIPAddresses;
        }

        protected override Task<PublicIPAddress> CreateAsync()
            => Client.CreateOrUpdateAsync(
                ResourceGroupName,
                Name,
                new PublicIPAddress { Location = "eastus" });

        protected override Task<PublicIPAddress> GetOrThrowAsync()
            => Client.GetAsync(ResourceGroupName, Name);

        private IPublicIPAddressesOperations Client { get; }
    }
}
