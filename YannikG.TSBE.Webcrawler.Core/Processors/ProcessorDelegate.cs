using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    public delegate Task<ProcessorResult> ProcessorCallback<TInput, TPipelineSettings>(TInput? input, TPipelineSettings pipelineSettings) where TInput : class where TPipelineSettings : IPipelineSettings;

    public delegate Task<ProcessorResult> ProcessorNextCallback<TInput>(TInput? input) where TInput : class;
}