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

using Microsoft.Azure.Commands.Common.Strategies.Rm.Entities;
using System;

namespace Microsoft.Azure.Commands.Common.Strategies.Rm.Config
{
    public interface INestedResourceConfig : IEntityConfig
    {
        new INestedResourceStrategy Strategy { get; }

        IEntityConfig Parent { get; }
    }

    public interface INestedResourceConfig<TParentModel> : INestedResourceConfig
        where TParentModel : class
    {
        new IEntityConfig<TParentModel> Parent { get; }

        TResult Accept<TContext, TResult>(
            INestedResourceConfigVisitor<TParentModel, TContext, TResult> visitor, TContext context);
    }

    /// <summary>
    /// Nested resource configuration. Fro example, Subnet.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TParentModel"></typeparam>
    public interface INestedResourceConfig<TModel, TParentModel> :
        INestedResourceConfig<TParentModel>,
        IEntityConfig<TModel>
        where TParentModel : class
        where TModel : class
    {
        Func<IEngine, TModel> CreateModel { get; }

        new INestedResourceStrategy<TModel, TParentModel> Strategy { get; }
    }
}
