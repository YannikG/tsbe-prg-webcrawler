using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
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

        public async Task CollectAsync(ImageDownloadPipelineSettings pipelineSettings, ProcessorNextCallback<ImageFromDatabaseModel> next)
        {
            var images = _imageRepository.GetAllImages();

            next.Invoke(new ImageFromDatabaseModel()
            {
                ImageEntities = images
            }, new ProcessorResult(ProcessorResultType.SUCCESS));
        }
    }
}