using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
    /// <summary>
    /// generic interface for a new processor that should be used in a <see cref="Pipelines.Pipeline{TInput, TPipelineSettings}"/>
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TPipelineSettings"></typeparam>
    public interface IProcessor<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        /// <summary>
        /// method that processes an object of the type <typeparamref name="TInput"/> with settings provided trough <paramref name="pipelineSettings"/> and returns a valid <see cref="ProcessorResult"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pipelineSettings"></param>
        /// <returns></returns>
        public Task<ProcessorResult> ProcessAsync(TInput? input, TPipelineSettings pipelineSettings);
    }
}