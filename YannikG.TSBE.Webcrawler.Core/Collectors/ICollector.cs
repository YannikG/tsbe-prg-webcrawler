using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    /// <summary>
    /// generic interface for a new collector that should be used in a <see cref="Pipelines.Pipeline{TInput, TPipelineSettings}"/>
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TPipelineSettings"></typeparam>
    public interface ICollector<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        /// <summary>
        /// generic method that returns a <see cref="ICollection{TInput}"/> and takes <paramref name="pipelineSettings"/> with settings.
        /// </summary>
        /// <param name="pipelineSettings"></param>
        /// <returns></returns>
        public Task<ICollection<TInput>> CollectAsync(TPipelineSettings pipelineSettings);
    }
}