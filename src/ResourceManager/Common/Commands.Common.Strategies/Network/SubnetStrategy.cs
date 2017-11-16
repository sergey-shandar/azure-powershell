﻿using Microsoft.Azure.Management.Network.Models;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Common.Strategies.Network
{
    public static class SubnetPolicy
    {
        public static NestedResourceStrategy<Subnet, VirtualNetwork> Strategy { get; }
            = NestedResourceStrategy.Create<Subnet, VirtualNetwork>(
                "subnets",
                (vn, name) => vn.Subnets?.FirstOrDefault(s => s.Name == name),
                (vn, name, subnet) =>
                {
                    subnet.Name = name;
                    if (vn.Subnets == null)
                    {
                        vn.Subnets = new List<Subnet> { subnet };
                        return;
                    }
                    var s = vn
                        .Subnets
                        .Select((sn, i) => new { sn, i })
                        .FirstOrDefault(p => p.sn.Name == name);
                    if (s != null)
                    {
                        vn.Subnets[s.i] = subnet;
                        return;
                    }
                    vn.Subnets.Add(subnet);
                });

        public static NestedResourceConfig<Subnet, VirtualNetwork> CreateSubnet(
            this ResourceConfig<VirtualNetwork> virtualNetwork, string name, string addressPrefix)
            => Strategy.CreateConfig(
                virtualNetwork,
                name,
                () => new Subnet { Name = name, AddressPrefix = addressPrefix });
    }
}
