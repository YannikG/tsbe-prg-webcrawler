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

        public async Task<ProcessorResult> ProcessAsync(BasicArticleModel? input, RocoPipelineSettings pipelineSettings)
        {
            if (input is null || string.IsNullOrEmpty(input.ArticleNumber) || string.IsNullOrEmpty(input.Name))
            {
                return new ProcessorResult(ProcessorResultType.SKIPPED, $"processing of {pipelineSettings.ManufacturerDefaultValue} {input?.ArticleNumber}");
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
            }

            return new ProcessorResult(ProcessorResultType.SUCCESS, $"processing of {pipelineSettings.ManufacturerDefaultValue} {input?.ArticleNumber}");
        }
    }
}