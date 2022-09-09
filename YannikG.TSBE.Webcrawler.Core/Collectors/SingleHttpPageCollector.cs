using System;
using Flurl;
using Flurl.Http;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
	public class SingleHttpPageCollector
	{
		private FlurlClient _cli;

		public SingleHttpPageCollector()
		{
			_cli = new FlurlClient();
		}

		public async Task<string> CollectAsync(string urlString)
		{
			return await _cli.Request(urlString)
				.GetStringAsync();
        }
    }
}

