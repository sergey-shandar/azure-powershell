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

using Microsoft.Rest;
using System;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Common.Strategies
{
    public sealed class ResourceStrategy<TModel> : IResourceStrategy
    {
        public ResourceType Type { get; }

        public Func<IClient, string> GetApiVersion { get; }

        public Func<IClient, GetAsyncParams, Task<TModel>> GetAsync { get; }

        public Func<IClient, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> CreateOrUpdateAsync
        { get; }

        public Func<TModel, string> GetLocation { get; }

        public Action<TModel, string> SetLocation { get; }

        public Func<TModel, int> CreateTime { get; }

        public bool CompulsoryLocation { get; }

        public ResourceStrategy(
            ResourceType type,
            Func<IClient, string> getApiVersion,
            Func<IClient, GetAsyncParams, Task<TModel>> getAsync,
            Func<IClient, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> createOrUpdateAsync,
            Func<TModel, string> getLocation,
            Action<TModel, string> setLocation,
            Func<TModel, int> createTime,
            bool compulsoryLocation)
        {
            Type = type;
            GetApiVersion = getApiVersion;
            GetAsync = getAsync;
            CreateOrUpdateAsync = createOrUpdateAsync;
            GetLocation = getLocation;
            SetLocation = setLocation;
            CreateTime = createTime;
            CompulsoryLocation = compulsoryLocation;
        }
    }

    public static class ResourceStrategy
    {
        public static ResourceStrategy<TModel> Create<TModel, TClient, TOperation>(
            ResourceType type,
            Func<TClient, string> getApiVersion,
            Func<TClient, TOperation> getOperations,
            Func<TOperation, GetAsyncParams, Task<TModel>> getAsync,
            Func<TOperation, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> createOrUpdateAsync,
            Func<TModel, string> getLocation,
            Action<TModel, string> setLocation,
            Func<TModel, int> createTime,
            bool compulsoryLocation)
            where TClient : ServiceClient<TClient>
        {
            Func<IClient, TOperation> toOperations = client => getOperations(client.GetClient<TClient>());
            return new ResourceStrategy<TModel>(
                type,
                client => getApiVersion(client.GetClient<TClient>()),
                (client, p) => getAsync(toOperations(client), p),
                (client, p) => createOrUpdateAsync(toOperations(client), p),
                getLocation,
                setLocation,
                createTime,
                compulsoryLocation);
        }

        public static string GetResourceType(this IResourceStrategy strategy)
            => strategy.Type == null ? null : strategy.Type.Namespace + "/" + strategy.Type.Provider;
    }
}
