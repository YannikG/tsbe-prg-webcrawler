﻿using Microsoft.Extensions.Logging;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
using YannikG.TSBE.Webcrawler.Core.Services;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
    public class PipelineBuilder<TInput, TPipelineSettings> : IPipelineBuilder<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        private ICollector<TInput, TPipelineSettings>? _collector;
        private List<IProcessor<TInput, TPipelineSettings>> _processors = new List<IProcessor<TInput, TPipelineSettings>>();

        private readonly PipelineServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;

        public PipelineBuilder(PipelineServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
        }

        public IPipelineBuilder<TInput, TPipelineSettings> UseCollector<TCollector>() where TCollector : ICollector<TInput, TPipelineSettings>
        {
            var collector = _serviceProvider.GetService<TCollector>();

            if (collector is null)
                throw new ArgumentException("Collector must be in dependency injection pipeline!");

            _collector = collector;

            return this;
        }

        public IPipelineBuilder<TInput, TPipelineSettings> WithoutCollector()
        {
            _collector = null;
            return this;
        }

        public IPipelineBuilder<TInput, TPipelineSettings> AddProcessor<TProcessor>() where TProcessor : IProcessor<TInput, TPipelineSettings>
        {
            var processor = _serviceProvider.GetService<TProcessor>();

            if (processor is null)
                throw new ArgumentException("Processor must be in dependency injection pipeline!");

            _processors.Add(processor);
            return this;
        }

        public IPipelineBuilder<TInput, TPipelineSettings> WithoutProcessors()
        {
            this._processors = new List<IProcessor<TInput, TPipelineSettings>>();
            return this;
        }

        public Pipeline<TInput, TPipelineSettings> Build()
        {
            return new Pipeline<TInput, TPipelineSettings>(this._collector, this._processors, this._loggerFactory);
        }
    }
}