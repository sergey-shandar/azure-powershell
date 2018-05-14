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

using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.Azure.Commands.Common.Strategies.Json
{
    class JTokenConverter : IConverter
    {
        public object Deserialize(Converters converters, Type type, JToken token)
            => token;

        public bool Match(Type type)
            => new[] { type }.Concat(type.GetInterfaces()).Any(v => v == typeof(JToken));

        public JToken Serialize(Converters converters, Type type, object value)
            => value as JToken;
    }
}