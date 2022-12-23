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

        public async Task<ProcessorResult> ProcessAsync(BasicArticleModel? input, RocoPipelineSettings pipelineSettings)
        {
            if (input is null || string.IsNullOrEmpty(input.ImageUrl))
            {
                return new ProcessorResult(ProcessorResultType.SKIPPED, $"processing of {pipelineSettings.ManufacturerDefaultValue} {input?.ArticleNumber}: {input?.ImageUrl}");
            }

            var imagesWithSameUrl = _imageRepository.GetImagesByImageUrl(input.ImageUrl);

            if (imagesWithSameUrl.Count <= 0)
            {
                var newImage = new ImageEntity()
                {
                    ImageUrl = input.ImageUrl
                };

                _imageRepository.Create(newImage);
            }

            return new ProcessorResult(ProcessorResultType.SUCCESS, $"processing of {pipelineSettings.ManufacturerDefaultValue} {input?.ArticleNumber}: {input?.ImageUrl}");
        }
    }
}