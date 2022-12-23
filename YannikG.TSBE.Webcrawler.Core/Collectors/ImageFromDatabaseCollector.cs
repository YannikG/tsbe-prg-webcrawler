using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public class ImageFromDatabaseCollector : ICollector<ImageFromDatabaseModel, ImageDownloadPipelineSettings>
    {
        private readonly IImageRepository _imageRepository;

        public ImageFromDatabaseCollector(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        public async Task<ICollection<ImageFromDatabaseModel>> CollectAsync(ImageDownloadPipelineSettings pipelineSettings)
        {
            var result = _imageRepository
                .GetAllImages()
                .Select(entity => new ImageFromDatabaseModel()
                {
                    Entity = entity
                }).ToList();

            return result;
        }
    }
}