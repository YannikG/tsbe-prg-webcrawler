using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public interface ICollector<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        public Task CollectAsync(TPipelineSettings pipelineSettings, ProcessorNextCallback<TInput> next);
    }
}