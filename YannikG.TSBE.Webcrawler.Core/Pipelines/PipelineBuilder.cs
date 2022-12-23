using Microsoft.Extensions.Logging;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
using YannikG.TSBE.Webcrawler.Core.Services;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
    /// <summary>
    /// generic implementation of <see cref="IPipelineBuilder{TInput, TPipelineSettings}"/> that builds a new pipeline of type <see cref="Pipeline{TInput, TPipelineSettings}"/>.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TPipelineSettings"></typeparam>
    public class PipelineBuilder<TInput, TPipelineSettings> : IPipelineBuilder<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        private ICollector<TInput, TPipelineSettings>? _collector;
        private List<IProcessor<TInput, TPipelineSettings>> _processors = new List<IProcessor<TInput, TPipelineSettings>>();

        private readonly PipelineServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// </summary>
        /// <param name="serviceProvider"></param>
        public PipelineBuilder(PipelineServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
        }

        /// <summary>
        /// let the builder know what collector you want to use for this pipeline.
        /// </summary>
        /// <typeparam name="TCollector"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IPipelineBuilder<TInput, TPipelineSettings> UseCollector<TCollector>() where TCollector : ICollector<TInput, TPipelineSettings>
        {
            var collector = _serviceProvider.GetService<TCollector>();

            if (collector is null)
                throw new ArgumentException("Collector must be in dependency injection pipeline!");

            _collector = collector;

            return this;
        }

        /// <summary>
        /// let the builder know that you don't want to use a collector for this pipeline.
        /// </summary>
        /// <returns></returns>
        public IPipelineBuilder<TInput, TPipelineSettings> WithoutCollector()
        {
            _collector = null;
            return this;
        }

        /// <summary>
        /// add a processor to this pipeline.
        /// </summary>
        /// <typeparam name="TProcessor"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IPipelineBuilder<TInput, TPipelineSettings> AddProcessor<TProcessor>() where TProcessor : IProcessor<TInput, TPipelineSettings>
        {
            var processor = _serviceProvider.GetService<TProcessor>();

            if (processor is null)
                throw new ArgumentException("Processor must be in dependency injection pipeline!");

            _processors.Add(processor);
            return this;
        }

        /// <summary>
        /// let the pipeline know you don't want to use any processors.
        /// </summary>
        /// <returns></returns>
        public IPipelineBuilder<TInput, TPipelineSettings> WithoutProcessors()
        {
            this._processors = new List<IProcessor<TInput, TPipelineSettings>>();
            return this;
        }

        /// <summary>
        /// return a new pipeline of type <see cref="Pipeline{TInput, TPipelineSettings}"/> based on the previous called methods.
        /// </summary>
        /// <returns></returns>
        public Pipeline<TInput, TPipelineSettings> Build()
        {
            return new Pipeline<TInput, TPipelineSettings>(this._collector, this._processors, this._loggerFactory);
        }
    }
}