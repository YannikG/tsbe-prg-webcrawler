using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    public interface IProcessor<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        /// <summary>
        /// Generic method for to process an object.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pipelineSettings"></param>
        /// <param name="next"></param>
        public Task<ProcessorResult> ProcessAsync(TInput? input, TPipelineSettings pipelineSettings);
    }
}