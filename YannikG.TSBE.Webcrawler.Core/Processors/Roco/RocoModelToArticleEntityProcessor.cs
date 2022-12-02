using System;
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
    public class RocoModelToArticleEntityProcessor : IProcessor<BasicArticleModel, RocoPipelineSettings>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IImageRepository _imageRepository;

        public RocoModelToArticleEntityProcessor(IArticleRepository articleRepository, IImageRepository imageRepository)
        {
            _articleRepository = articleRepository;
            _imageRepository = imageRepository;
        }

        public void Process(BasicArticleModel? input, RocoPipelineSettings pipelineSettings, ProcessorNextCallback<BasicArticleModel> next)
        {

            if (input is null || string.IsNullOrEmpty(input.ArticleNumber) || string.IsNullOrEmpty(input.Name))
            {
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED, "Article Number or Name is empty"));
                return;
            }

            if (string.IsNullOrEmpty(pipelineSettings.ManufacturerDefaultValue))
                throw new ArgumentNullException("ManufacturerDefaultValue cannot be null!");

            // Set Manufacturer.
            var articleManufacturer = pipelineSettings.ManufacturerDefaultValue;

            var existingArticles = _articleRepository.GetEntitiesByArticleNumberAndManufacturer(input.ArticleNumber, articleManufacturer);

            if (existingArticles.Count <= 0)
            {              
                var newArticle = new ArticleEntity()
                {
                    ArticleManufacturer = articleManufacturer,
                    ArticleNumber = input.ArticleNumber,
                    Name = input.Name,
                    Url = input.Url
                };

                if (!string.IsNullOrEmpty(input.ImageUrl))
                {
                    var imagesWithImageUrl = _imageRepository.GetImagesByImageUrl(input.ImageUrl);
                    var image = imagesWithImageUrl.FirstOrDefault();
                    if (image is not null)
                        newArticle.ImageId = image.Id;
                }

                _articleRepository.Create(newArticle);
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SUCCESS, $"Import for {input.ArticleNumber} finished."));

            }
            else
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED, $"Import for {input.ArticleNumber} skipped."));

        }
    }
}
