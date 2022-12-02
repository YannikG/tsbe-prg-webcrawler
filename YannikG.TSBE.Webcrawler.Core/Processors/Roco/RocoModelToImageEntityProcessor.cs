﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Core.Processors.Roco
{
    public class RocoModelToImageEntityProcessor : IProcessor<BasicArticleModel, RocoPipelineSettings>
    {
        private readonly IImageRepository _imageRepository;

        public RocoModelToImageEntityProcessor(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public void Process(BasicArticleModel? input, RocoPipelineSettings pipelineSettings, ProcessorNextCallback<BasicArticleModel> next)
        {
            if (input is null || string.IsNullOrEmpty(input.ImageUrl))
            {
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED, "ImageUrl is null"));
                return;
            }
                
            var imagesWithSameUrl = _imageRepository.GetImagesByImageUrl(input.ImageUrl);

            if (imagesWithSameUrl.Count <= 0)
            {
                var newImage = new ImageEntity()
                {
                    ImageUrl = input.ImageUrl
                };

                _imageRepository.Create(newImage);
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SUCCESS, $"Import for {input.ImageUrl} finished"));
            }
            else
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED, $"Import for {input.ImageUrl} skipped"));
        }
    }
}
