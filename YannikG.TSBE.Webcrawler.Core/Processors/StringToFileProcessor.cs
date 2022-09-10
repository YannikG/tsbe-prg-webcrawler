using System;
using Microsoft.Extensions.Options;
using YannikG.TSBE.Webcrawler.Core.Processors.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Processors
{
	public class StringToFileProcessor
	{
		private FileProcessorSettings _settings;

		public StringToFileProcessor(IOptions<FileProcessorSettings> settings)
		{
			_settings = settings.Value;
		}

		public void Process(string jobName, string fileExtenstion, string content)
		{
			string fqFileName = $"{_settings.BaseExportPath}/{jobName}.{fileExtenstion}";
			File.WriteAllText(fqFileName, content);
		}
	}
}

