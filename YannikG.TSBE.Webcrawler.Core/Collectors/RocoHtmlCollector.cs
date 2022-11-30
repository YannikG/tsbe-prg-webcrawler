using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;
using YannikG.TSBE.Webcrawler.Core.Collectors.Requesters;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public class RocoHtmlCollector : ICollector<BasicArticleModel, RocoPipelineSettings>
    {
        private readonly SingleHttpPageRequester _requester;
        private readonly RocoHtmlArticleParserHandler _parserHandler;
        private readonly RocoHtmlNextUrlHandler _nextUrlHandler;

        public RocoHtmlCollector(SingleHttpPageRequester requester, RocoHtmlArticleParserHandler parserHandler, RocoHtmlNextUrlHandler nextUrlHandler)
        {
            _requester = requester;
            _parserHandler = parserHandler;
            _nextUrlHandler = nextUrlHandler;
        }

        public async Task CollectAsync(RocoPipelineSettings pipelineSettings, ProcessorNextCallback<BasicArticleModel> next)
        {
            string url = pipelineSettings.StartUrl;
            bool shouldContinue = true;

            int roundCounter = 0;
            do
            {
                string htmlResult = await _requester.RequestAsync(url);

                _parserHandler.Handle(htmlResult, next);

                url = _nextUrlHandler.Handle(htmlResult);

                if (string.IsNullOrEmpty(url))
                    shouldContinue = false;

                roundCounter++;

            } while (
                shouldContinue && 
                (
                    pipelineSettings.StopAfterRounds == null ||
                    roundCounter < pipelineSettings.StopAfterRounds!
                )
            );
        }
    }
}
