﻿using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Core.Processors.Image
{
    public class DownloadImagesFromUrlProcessor : IProcessor<ImageFromDatabaseModel, ImageDownloadPipelineSettings>
    {
        private readonly IImageFileRepository _imageFileRepository;

        public DownloadImagesFromUrlProcessor(IImageFileRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public void Process(ImageFromDatabaseModel? input, ImageDownloadPipelineSettings pipelineSettings, ProcessorNextCallback<ImageFromDatabaseModel> next)
        {
            if (input == null || input.ImageEntities.Count <= 0)
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED));

            input!.ImageEntities.ForEach(i =>
            {
                // Currently there is no async implementation for Processors and Pipelines planned ;)
                var task = Task.Run(() => downloadImageEntity(i));
                task.Wait();
            });
        }

        private async Task downloadImageEntity(ImageEntity imageEntity)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string extention = findImageExtention(imageEntity.ImageUrl);
                var result = await httpClient.GetByteArrayAsync(imageEntity.ImageUrl);

                _imageFileRepository.SaveImage(result, imageEntity.Id, extention);
            }
        }

        private string findImageExtention(string url)
        {
            string lastPartOfUrl = url.Split(".").Last();

            switch (lastPartOfUrl)
            {
                case "jpg":
                    return "jpg";

                case "jpeg":
                    return "jpeg";

                case "png":
                    return "png";

                default:
                    throw new ArgumentException($"unknown image file extention was found: {lastPartOfUrl}");
            }
        }
    }
}