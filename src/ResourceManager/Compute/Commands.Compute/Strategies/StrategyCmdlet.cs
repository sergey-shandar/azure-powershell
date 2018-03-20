using Microsoft.Azure.Commands.Common.Strategies;
using Microsoft.Azure.Commands.Common.Strategies.Templates;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Compute.Strategies
{
    static class StrategyCmdlet
    {
        public static async Task<TModel> RunAsync<TModel>(
            IClient client,
            IParameters<TModel> parameters,
            IAsyncCmdlet asyncCmdlet,
            CancellationToken cancellationToken)
            where TModel : class
        {
            // create a DAG of configs.
            var config = await parameters.CreateConfigAsync();

            // reade current Azure state.
            var current = await config.GetStateAsync(client, cancellationToken);

            // update location.
            parameters.Location = current.UpdateLocation(parameters.Location, config);

            // update a DAG of configs.
            config = await parameters.CreateConfigAsync();

            if (parameters.AsArmTemplate)
            {
                // create target state
                var target = config.GetTargetState(current, TemplateEngine.Instance, parameters.Location);

                var template = config.CreateTemplate(client, target, client.SubscriptionId);
                var templateResult = JsonConvert.SerializeObject(template);
                asyncCmdlet.WriteObject(templateResult);

                return null;
            }
            else
            {
                var engine = new SdkEngine(client.SubscriptionId);
                var target = config.GetTargetState(current, engine, parameters.Location);

                // apply target state
                var newState = await config.UpdateStateAsync(
                    client,
                    target,
                    cancellationToken,
                    new ShouldProcess(asyncCmdlet),
                    asyncCmdlet.ReportTaskProgress);

                return newState.Get(config) ?? current.Get(config);
            }
        }
    }
}
