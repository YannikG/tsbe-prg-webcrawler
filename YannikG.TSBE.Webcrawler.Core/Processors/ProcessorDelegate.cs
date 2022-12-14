using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    public delegate void ProcessorCallback<TInput, TPipelineSettings>(TInput? input, TPipelineSettings pipelineSettings, ProcessorNextCallback<TInput> next) where TInput : class where TPipelineSettings : IPipelineSettings;

    public delegate void ProcessorNextCallback<TInput>(TInput? input, ProcessorResult result) where TInput : class;
}