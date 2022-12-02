using System;
using YannikG.TSBE.Webcrawler.Core.Models;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public interface IPipelineProcessor<TInput, TOutput>
	{
        public Task<TOutput> ProcessAsync(TInput input);
    }
}

