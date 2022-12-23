using YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;
using YannikG.TSBE.Webcrawler.Core.Collectors.Requesters;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;

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

        public async Task<ICollection<BasicArticleModel>> CollectAsync(RocoPipelineSettings pipelineSettings)
        {
            string url = pipelineSettings.StartUrl;
            bool shouldContinue = true;

            var result = new List<BasicArticleModel>();

            int roundCounter = 0;
            do
            {
                string htmlResult = await _requester.RequestAsync(url);

                result.AddRange(_parserHandler.Handle(htmlResult));

                url = _nextUrlHandler.Handle(htmlResult);

                if (string.IsNullOrEmpty(url))
                    shouldContinue = false;

                // A Delay can be defined so Roco.cc doesn't get hit in a too short interval to trigger some sort of DDoS protection...
                Thread.Sleep(pipelineSettings.Deloay);

                roundCounter++;
            } while (
                shouldContinue &&
                (
                    pipelineSettings.StopAfterRounds == null ||
                    roundCounter < pipelineSettings.StopAfterRounds!
                )
                    );

            return result;
        }
    }
}