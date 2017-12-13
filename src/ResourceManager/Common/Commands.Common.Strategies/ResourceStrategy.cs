// ----------------------------------------------------------------------------------
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Common.Strategies
{
    public sealed class ResourceStrategy<TModel> : IResourceStrategy, IEntityStrategy<TModel>
        where TModel : class
    {
        public string Type { get; }

        public Func<string, IEnumerable<string>> CreateId { get; }

        public Func<IClient, GetAsyncParams, Task<TModel>> GetAsync { get; }

        public Func<IClient, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> CreateOrUpdateAsync
        { get; }

        public Property<TModel, string> Location { get; }

        public Func<TModel, string> GetId { get; }

        public Func<string, TModel> IdToRef { get; }

        public Func<TModel, int> CreateTime { get; }

        public bool CompulsoryLocation { get; }

        public ResourceStrategy(
            string type,
            Func<string, IEnumerable<string>> createId,
            Func<IClient, GetAsyncParams, Task<TModel>> getAsync,
            Func<IClient, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> createOrUpdateAsync,
            Property<TModel, string> location,
            Func<TModel, string> getId,
            Func<string, TModel> idToRef,
            Func<TModel, int> createTime,
            bool compulsoryLocation)
        {
            Type = type;
            CreateId = createId;
            GetAsync = getAsync;
            CreateOrUpdateAsync = createOrUpdateAsync;
            Location = location;
            GetId = getId;
            IdToRef = idToRef;
            CreateTime = createTime;
            CompulsoryLocation = compulsoryLocation;
        }
    }

    public static class ResourceStrategy
    {
        public static ResourceStrategy<TModel> Create<TModel, TClient, TOperation>(
            string type,
            Func<string, IEnumerable<string>> createId,
            Func<TClient, TOperation> getOperations,
            Func<TOperation, GetAsyncParams, Task<TModel>> getAsync,
            Func<TOperation, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> createOrUpdateAsync,
            Func<TModel, string> getLocation,
            Action<TModel, string> setLocation,
            Func<TModel, string> getId,
            Func<string, TModel> idToRef,
            Func<TModel, int> createTime,
            bool compulsoryLocation)
            where TModel : class
            where TClient : ServiceClient<TClient>
        {
            Func<IClient, TOperation> toOperations = client => getOperations(client.GetClient<TClient>());
            return new ResourceStrategy<TModel>(
                type: type,
                createId: createId,
                getAsync: (client, p) => getAsync(toOperations(client), p),
                createOrUpdateAsync: (client, p) => createOrUpdateAsync(toOperations(client), p),
                location: Property.Create(getLocation, setLocation),
                getId: getId,
                idToRef: idToRef,
                createTime: createTime,
                compulsoryLocation: compulsoryLocation);
        }

        public static ResourceStrategy<TModel> Create<TModel, TClient, TOperation>(
            string type,
            IEnumerable<string> providers,
            Func<TClient, TOperation> getOperations,
            Func<TOperation, GetAsyncParams, Task<TModel>> getAsync,
            Func<TOperation, CreateOrUpdateAsyncParams<TModel>, Task<TModel>> createOrUpdateAsync,
            Func<TModel, string> getLocation,
            Action<TModel, string> setLocation,
            Func<TModel, string> getId,
            Func<string, TModel> idToRef,
            Func<TModel, int> createTime,
            bool compulsoryLocation)
            where TModel: class
            where TClient : ServiceClient<TClient>
            => Create(
                type: type,
                createId: name => new[] { "providers" }.Concat(providers).Concat(new[] { name }),
                getOperations: getOperations,
                getAsync: getAsync,
                createOrUpdateAsync: createOrUpdateAsync,
                getLocation: getLocation,
                setLocation: setLocation,
                getId: getId,
                idToRef: idToRef,
                createTime: createTime,
                compulsoryLocation: compulsoryLocation);
    }
}
