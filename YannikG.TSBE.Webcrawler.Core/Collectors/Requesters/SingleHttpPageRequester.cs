using System;
using Flurl;
using Flurl.Http;

namespace YannikG.TSBE.Webcrawler.Core.Collectors.Requesters
{
    public class SingleHttpPageRequester
    {
        private FlurlClient _cli;

        public SingleHttpPageRequester()
        {
            _cli = new FlurlClient();
        }

        public async Task<string> RequestAsync(string urlString)
        {
            return await _cli.Request(urlString)
                .GetStringAsync();
        }
    }
}

