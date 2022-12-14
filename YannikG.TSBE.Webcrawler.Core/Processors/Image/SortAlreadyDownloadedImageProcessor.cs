using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories;

namespace YannikG.TSBE.Webcrawler.Core.Processors.Image
{
    public class SortAlreadyDownloadedImageProcessor : IProcessor<ImageFromDatabaseModel, ImageDownloadPipelineSettings>
    {
        private readonly IImageFileRepository _imageFileRepository;

        public SortAlreadyDownloadedImageProcessor(IImageFileRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public void Process(ImageFromDatabaseModel? input, ImageDownloadPipelineSettings pipelineSettings, ProcessorNextCallback<ImageFromDatabaseModel> next)
        {
            if (input == null || input.ImageEntities.Count <= 0)
                next.Invoke(input, new ProcessorResult(ProcessorResultType.SKIPPED));

            var notYetDownloadedImages = new List<ImageEntity>();

            input!.ImageEntities.ForEach(entity =>
            {
                if (!_imageFileRepository.DoesImageAlreadyExists(entity.Id))
                    notYetDownloadedImages.Add(entity);
            });

            input!.ImageEntities = notYetDownloadedImages;

            next.Invoke(input, new ProcessorResult(ProcessorResultType.SUCCESS));
        }
    }
}