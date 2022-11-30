﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
    public class Pipeline<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        private readonly ICollector<TInput, TPipelineSettings>? _collector;
        private readonly List<ProcessorCallback<TInput, TPipelineSettings>> _processors;
        private int _processorCalled = -1;
        private TPipelineSettings? _pipelineSettings;
        public Pipeline(ICollector<TInput, TPipelineSettings>? collector, List<ProcessorCallback<TInput, TPipelineSettings>> processors)
        {
            _collector = collector;
            _processors = processors;
        }

        public async Task StartPipeline(TPipelineSettings pipelineSettings)
        {
            if (pipelineSettings is null)
                throw new ArgumentNullException("Pipeline Settings must be provided!");

            _pipelineSettings = pipelineSettings;

            if (_collector is null)
                // When no collector was found, start processors with null.
                _handleNext(null);
            else
                // Otherwise start collector.
                await _collector.CollectAsync(pipelineSettings, _handleNext);
        }

        private void _handleNext(TInput? input)
        {
            _processorCalled++;

            if (_processorCalled < _processors.Count)
                // pipelineSettings are always provided. See StartPipeline.
                _processors[_processorCalled].Invoke(input, _pipelineSettings!, _handleNext);
            else
                // now we are at the end of a chain. let's reset the counter for the next chain.
                _processorCalled = -1;
        }
    }
}