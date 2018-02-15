namespace Microsoft.Azure.Commands.Common.Strategies.Templates
{
    public sealed class TemplateContext : IEngine
    {
        public string GetId<TModel>(IEntityConfig<TModel> config)
            where TModel : class
            => "[concat(resourceGroup().id, '" + config.GetProvidersId().IdToString() + "')]";
    }
}
