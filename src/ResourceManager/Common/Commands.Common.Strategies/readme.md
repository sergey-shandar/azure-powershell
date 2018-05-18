# Strategies

- [X] partly done, public extensions vs private extensions
- [X] partly done, public extensions in a simple common namespace
- [X] no Utilities namespace
- [X] move UniqueId to Compute and make private.
- [X] move Compute to Compute.
- ResourceConfig => IResourceConfig
- NestedResourceConfig => INestedResourceConfig
- ResourceStrategy => IResourceStrategy
- NestedResourceStrategy => IResourceStrategy
- [X] partly done (removed), template abstractions / dependency injection.

## Future

- move Cmdlets to a separeate PS common library. (Garrett)

## Design

- Strategies for resources. It's a meta information about Azure resources which is missed in Azure SDK.
      - Generic strategy interfaces.
      - Specific implementations for different Azure resources. Each resource type should have only one implementation.
- Configuration graph (DAG)
      - Generic configuration interfaces.
      - Specific implementations for different Azure resources. Each resource type may have different configuration implementations.

- Utilities
  - Azure State
  - Generic JSON serializer/deserializer from/to JObject.
  - Task Progress which works with graphs.
  - Async support for PS
  - Template support (WIP)

```C#
interface IParameters<TModel, TResourceGroup>
{

}
```

### Changes

`IParameters` => `INewCmdlet`.

### Public Types

- [Compute/](Compute/)
- [Extensions.cs](Extensions.cs)
- [UniqueId.cs](UniqueId.cs)

### Private Types

- [Json/](Json/)
- [Templates/](Templates/)
- [Progress/](Progress/)

## Wish List

### AutoRest

1. An SDK serializer
   1. should be generic and belong to a runtime library
   1. should have a function to serialize to `JToken` instead of `string`.
1. API version
   1. should be discoverable from meta information, for example from a model, an operation, and/or
      a client.
1. Resource model should implement an `IResourceModel` interface, for example
   ```cs
   interface IResourceModel
   {
         [JsonProperty("name")]
         string Name { get; set; }

         [JsonProperty("location")]
         string? Location { get; set; }

         IDictionary<string, object> GetProperties();
   }
   ```
1. Resource operations classes should implement an `IResourceOperations<TModel>` interface, for example
   ```cs
   interface IResourceOperations<TModel>
       where TModel : IResourceModel
   {
         Task<TModel> BeginCreateOrUpdate(
            string name, string resourceGroupName, TModel model);
   }
   ```
1. `secureString` should be Swagger type/format.
1. Another type for Swagger `id[ResourceType]`.
