using System;
namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
	public interface IPipeline
	{
        Task Execute(string jobName, string startUrl);
    }
}

