using System;
using System.Diagnostics;
using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
	public class RocoBasicArticlePipeline : IPipeline
    {
		private readonly PipelineSettings _settings;
        private readonly SingleHttpPageCollector _collector;
        private readonly RocoHtmlParserProcessor _processor;
        private readonly RocoHtmlNextUrlProcessor _nextPageProcessor;
        private readonly ObjectToCSVProcessor _csvProcessor;
        private readonly StringToFileProcessor _fileProcessor;
        private readonly ILogger _logger;

        public RocoBasicArticlePipeline(
            IOptions<PipelineSettings> settings,
            SingleHttpPageCollector collector,
            RocoHtmlParserProcessor processor,
            RocoHtmlNextUrlProcessor nextPageProcessor,
            ObjectToCSVProcessor cSVProcessor,
            StringToFileProcessor fileProcessor,
            ILoggerFactory loggerFactory
            )
		{
			_settings = settings.Value;
            _collector = collector;
            _processor = processor;
            _nextPageProcessor = nextPageProcessor;
            _csvProcessor = cSVProcessor;
            _fileProcessor = fileProcessor;
            _logger = loggerFactory.CreateLogger<RocoBasicArticlePipeline>();
		}

		public async Task Execute(string jobName, string startUrl)
		{
            jobName = $"{_settings.BaseJobName}_{jobName}";

            _logger.LogInformation($"starting with job {jobName}");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var processedResult = new List<BasicArticleModel>();
            string url = startUrl;

            bool shouldContinue = true;
            int roundCounter = 1;

            do
            {
                _logger.LogDebug($"Starting with Round {roundCounter} of {_settings.StopAfterRounds}");
                _logger.LogDebug($"Using url {url}");

                var result = await _collector.CollectAsync(url);
                processedResult.AddRange(_processor.Process(result));

                url = _nextPageProcessor.Process(result);

                if (string.IsNullOrEmpty(url))
                    shouldContinue = false;

                if (roundCounter >= _settings.StopAfterRounds)
                    shouldContinue = false;

                roundCounter++;

                _logger.LogDebug($"Delay for {_settings.Deloay} ms");
                System.Threading.Thread.Sleep(_settings.Deloay);

            } while (shouldContinue);

            _logger.LogInformation("collecting and processing done...");

            processedResult = processedResult.DistinctBy(q => q.ArticleNumber).ToList();

            string csvContent = await _csvProcessor.Process<BasicArticleModel>(processedResult.ToList());

            _fileProcessor.Process(jobName, "csv", csvContent);

            stopWatch.Stop();

            _logger.LogInformation($" job done after {stopWatch.ElapsedMilliseconds / 1000} s");
        }
    }
}

