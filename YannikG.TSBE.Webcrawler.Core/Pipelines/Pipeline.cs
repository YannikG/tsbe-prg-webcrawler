using Microsoft.Extensions.Logging;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
    public class Pipeline<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        private readonly ICollector<TInput, TPipelineSettings>? _collector;
        private readonly List<IProcessor<TInput, TPipelineSettings>> _processors;
        private TPipelineSettings? _pipelineSettings;
        private readonly ILogger _logger;

        public Pipeline(ICollector<TInput, TPipelineSettings>? collector, List<IProcessor<TInput, TPipelineSettings>> processors, ILoggerFactory loggerFactory)
        {
            _collector = collector;
            _processors = processors;
            _logger = loggerFactory.CreateLogger<Pipeline<TInput, TPipelineSettings>>();
        }

        public async Task StartPipelineAsync(TPipelineSettings pipelineSettings)
        {
            if (pipelineSettings is null)
                throw new ArgumentNullException("Pipeline Settings must be provided!");

            _pipelineSettings = pipelineSettings;

            if (_collector is null)
                // When no collector was found, start processors with null.
                await runProcessors(null);
            else
            {
                // Otherwise start collector.
                var collectorResult = await _collector.CollectAsync(pipelineSettings);

                string? currentCollectorName = _collector.GetType().Name;
                _logger.LogInformation($"[{currentCollectorName}] Collector done with total {collectorResult.Count} items");

                collectorResult.ToList().ForEach(async result => await runProcessors(result));
            }
        }

        private async Task runProcessors(TInput? input)
        {
            foreach (var processor in _processors)
            {
                var processorResult = await processor.ProcessAsync(input, _pipelineSettings!);

                string? currentProcessorName = processor.GetType().Name;
                string message = $"[{currentProcessorName}:{processorResult.Result}] " + processorResult.Message;
                _logger.LogInformation(message);

                if (processorResult.Result == ProcessorResultType.ABORT_ITEM)
                    break;
            };
        }
    }
}