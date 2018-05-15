﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Rest;
using Xunit;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using System.Threading;
using Microsoft.Azure.Commands.Common.Strategies.Cmdlets;
using Microsoft.Azure.Commands.Common.Strategies.Entities;
using Microsoft.Azure.Commands.Common.Strategies.Config;

namespace Microsoft.Azure.Commands.Common.Strategies.UnitTest
{
    public class AsyncCmdletExtensionsTest
    {
        class Client : IClient
        {
            public string SubscriptionId => "subscription";

            public T GetClient<T>() where T : ServiceClient<T>
                => null;
        }

        class RG { }

        class Parameters : ICmdletParameters<RG, RG>
        {
            public string DefaultLocation => "eastus";

            public string Location
            {
                get
                {
                    return "somelocation";
                }

                set
                {
                }
            }

            public string OutputTemplateFile
                => null;

            public async Task<ResourceConfig<RG>> CreateConfigAsync(ResourceConfig<RG> resourceGroupConfig)
                => new ResourceConfig<RG>(
                    new ResourceStrategy<RG>(
                        null,
                        null,
                        async (c, p) => new RG(),
                        null,
                        null,
                        null),
                    null,
                    null,
                    null,
                    null);

            public ResourceConfig<RG> CreateResourceGroup()
                => null;
        }

        class AsyncCmdlet : IAsyncCmdlet
        {
            public CancellationToken CancellationToken { get; } 
                = new CancellationToken();

            public IEnumerable<KeyValuePair<string, object>> Parameters
            {
                get
                {
                    yield return new KeyValuePair<string, object>("Str", "str");
                }
            }

            public string VerbsNew
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void ReportTaskProgress(ITaskProgress taskProgress)
            {
                throw new NotImplementedException();
            }

            public Task<bool> ShouldProcessAsync(string target, string action)
            {
                throw new NotImplementedException();
            }

            public void WriteObject(object value)
            {
                throw new NotImplementedException();
            }

            public void WriteVerbose(string message)
            {
                Assert.Equal("Str = \"str\"", message);
            }

            public void WriteWarning(string message)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public async Task TestVerboseStream()
        {
            var client = new Client();
            var parameters = new Parameters();
            var asyncCmdlet = new AsyncCmdlet();

            await client.RunAsync(parameters, asyncCmdlet);
        }
    }
}
