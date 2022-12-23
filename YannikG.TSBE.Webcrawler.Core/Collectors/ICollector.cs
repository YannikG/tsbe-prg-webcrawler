using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public interface ICollector<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        public Task<ICollection<TInput>> CollectAsync(TPipelineSettings pipelineSettings);
    }
}