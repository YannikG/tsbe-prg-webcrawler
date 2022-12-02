using System;
namespace YannikG.TSBE.Webcrawler.Core.Processors.Models
{
	public class UrlToFileExecutionContext
	{
		public int StopAfterRound { get; set; } = default(int);
		public string StartUrl { get; set; } = string.Empty;
	}
}

