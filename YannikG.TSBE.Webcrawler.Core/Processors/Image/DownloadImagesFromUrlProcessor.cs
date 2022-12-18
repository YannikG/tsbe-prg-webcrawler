using YannikG.TSBE.Webcrawler.Core.Entities;
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
        /// <summary>
        /// Download Image from stored URL in <paramref name="imageEntity"/>
        /// </summary>
        /// <param name="imageEntity"></param>
        /// <returns></returns>
        private async Task downloadImageEntity(ImageEntity imageEntity)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string extension = findImageExtension(imageEntity.ImageUrl);
                var result = await httpClient.GetByteArrayAsync(imageEntity.ImageUrl);

                _imageFileRepository.SaveImage(result, imageEntity.Id, extension);
            }
        }
        /// <summary>
        /// Find file extension from URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private string findImageExtension(string url)
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
                    throw new ArgumentException($"unknown image file extension was found: {lastPartOfUrl}");
            }
        }
    }
}