﻿using Microsoft.Azure.Experiments.Network;
using Microsoft.Azure.Experiments.ResourceManager;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Azure.Experiments.Tests
{
    public class PublicIPAddressTest
    {
        [Fact]
        public async Task CreateAsyncTest()
        {
            var rg = ResourceGroupPolicy.CreateResourceGroupConfig("piparg");
            var vn = rg.CreatePublicIPAddressConfig("pipa");
            var client = new Client(Credentials.Get());
            var state = await vn.GetAsync(client, new CancellationToken());
            var location = state.GetLocation(rg);
            var parameters = vn.GetParameters(client.Context.SubscriptionId, "eastus");
            var vnc = parameters.GetOrNull(vn);
            var createState = await vn.CreateOrUpdateAsync(
                client, state, parameters, new CancellationToken());
            var vncc = createState.GetOrNull(vn);
            Assert.Equal("eastus", vncc.Location);
            Assert.Equal("pipa", vncc.Name);
            Assert.Equal(vn.GetId(client.Context.SubscriptionId).IdToString(), vncc.Id);
        }
    }
}
