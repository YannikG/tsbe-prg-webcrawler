using static Dapper.SqlMapper;
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

        public async Task<ProcessorResult> ProcessAsync(ImageFromDatabaseModel? input, ImageDownloadPipelineSettings pipelineSettings)
        {
            if (input == null || input.Entity == null)
                return new ProcessorResult(ProcessorResultType.SKIPPED, "skipped, no entity");

            if (_imageFileRepository.DoesImageAlreadyExists(input.Entity.Id))
                return new ProcessorResult(ProcessorResultType.ABORT_ITEM, "already downloaded. abort for this item");
            else
                return new ProcessorResult(ProcessorResultType.SUCCESS, "not existing, go ahead");
        }
    }
}