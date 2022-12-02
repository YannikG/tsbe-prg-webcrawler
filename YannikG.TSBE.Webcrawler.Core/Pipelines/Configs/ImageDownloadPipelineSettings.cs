using System;
namespace YannikG.TSBE.Webcrawler.Core.Pipelines.Configs
{
	public class ImageDownloadPipelineSettings : BasePipelineSettings, IPipelineSettings
	{
		public string StoragePath { get; set; } = string.Empty;
	}
}

