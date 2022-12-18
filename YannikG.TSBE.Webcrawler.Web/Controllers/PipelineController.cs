using Microsoft.AspNetCore.Mvc;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors.Image;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;

namespace YannikG.TSBE.Webcrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {
        private readonly Pipeline<BasicArticleModel, RocoPipelineSettings> _rocoBasicArticlePipeline;
        private readonly Pipeline<ImageFromDatabaseModel, ImageDownloadPipelineSettings> _imageDownloadPipeline;

        public PipelineController(
            IPipelineBuilder<BasicArticleModel, RocoPipelineSettings> rocoPipelineBuilder,
            IPipelineBuilder<ImageFromDatabaseModel, ImageDownloadPipelineSettings> imageDownloadPipelineBuilder)
        {
            // Configure Pipeline to extract articles from https://roco.cc
            _rocoBasicArticlePipeline = rocoPipelineBuilder
                .UseCollector<RocoHtmlCollector>()
                    .AddProcessor<RocoModelToImageEntityProcessor>()
                    .AddProcessor<RocoModelToArticleEntityProcessor>()
                    .Build();

            // Configure Pipeline to download images from any url-based source.
            _imageDownloadPipeline = imageDownloadPipelineBuilder
                .UseCollector<ImageFromDatabaseCollector>()
                    .AddProcessor<SortAlreadyDownloadedImageProcessor>()
                    .AddProcessor<DownloadImagesFromUrlProcessor>()
                    .Build();
        }

        [HttpPost("roco")]
        public async Task<IActionResult> StartRocoPipeline([FromBody] RocoPipelineSettings settings)
        {
            await _rocoBasicArticlePipeline.StartPipeline(settings);

            return NoContent();
        }

        [HttpPost("imagedownload")]
        public async Task<IActionResult> StartImageDownloadPipeline([FromBody] ImageDownloadPipelineSettings settings)
        {
            await _imageDownloadPipeline.StartPipeline(settings);

            return NoContent();
        }
    }
}