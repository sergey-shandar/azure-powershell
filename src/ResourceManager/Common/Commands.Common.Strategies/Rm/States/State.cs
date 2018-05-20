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

using Microsoft.Azure.Commands.Common.Strategies.Rm.Config;
using System;
using System.Collections.Concurrent;

namespace Microsoft.Azure.Commands.Common.Strategies.Rm.States
{
    /// <summary>
    /// Azure State. It contains information (models) of Azure resources.
    /// </summary>
    sealed class State : IState
    {
        readonly ConcurrentDictionary<string, object> _Map
            = new ConcurrentDictionary<string, object>();

        public TModel Get<TModel>(IResourceConfig<TModel> config)
            where TModel : class
            => _Map.GetOrNull(config?.DefaultIdStr()) as TModel;

        public TModel GetOrAdd<TModel>(IResourceConfig<TModel> config, Func<TModel> f)
            where TModel : class
            => _Map.GetOrAddWithCast(config.DefaultIdStr(), f);

        public bool Contains(IResourceConfig config)
            => _Map.ContainsKey(config.DefaultIdStr());
    }
}
