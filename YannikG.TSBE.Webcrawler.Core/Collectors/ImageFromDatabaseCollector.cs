using System;
using System.Collections.Generic;
using YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.FileSystem;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
	public class ImageFromDatabaseCollector : ICollector<ImageFromDatabaseModel, ImageDownloadPipelineSettings>
	{
		private readonly IImageRepository _imageRepository;
		private readonly FileSystemHandler _fileSystemHandler;

		public ImageFromDatabaseCollector(IImageRepository imageRepository, FileSystemHandler fileSystemHandler)
		{
			this._imageRepository = imageRepository;
			this._fileSystemHandler = fileSystemHandler;
		}

        public async Task CollectAsync(ImageDownloadPipelineSettings pipelineSettings, ProcessorNextCallback<ImageFromDatabaseModel> next)
        {
			var images = _imageRepository.GetAllImages();


        }
    }
}

