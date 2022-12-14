using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    public interface IProcessor<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        public void Process(TInput? input, TPipelineSettings pipelineSettings, ProcessorNextCallback<TInput> next);
    }
}