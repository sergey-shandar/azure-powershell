using Microsoft.Azure.Commands.Common.Strategies;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Compute.Strategies
{
    /// <summary>
    /// Parameters for creating a config.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    interface IParameters<TModel>
        where TModel : class
    {
        string Location { get; set; }

        Task<ResourceConfig<TModel>> CreateConfigAsync();
    }
}
