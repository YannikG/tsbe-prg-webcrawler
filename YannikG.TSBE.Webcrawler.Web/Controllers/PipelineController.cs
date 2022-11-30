using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;
using YannikG.TSBE.Webcrawler.Core.Services;

namespace YannikG.TSBE.Webcrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {
        private readonly Pipeline<BasicArticleModel, RocoPipelineSettings> _rocoBasicArticlePipeline;

        public PipelineController(PipelineServiceProvider pipelineServiceProvider)
        {
            _rocoBasicArticlePipeline = new PipelineBuilder<BasicArticleModel, RocoPipelineSettings>(pipelineServiceProvider)
                   .UseCollector<RocoHtmlCollector>()
                   .AddProcessor<RocoModelToImageEntityProcessor>()
                   .AddProcessor<RocoModelToArticleEntityProcessor>()
                   .Build();
        }

        [HttpPost]
        public async Task<IActionResult> StartRocoPipeline(string startUrl)
        {
            await _rocoBasicArticlePipeline.StartPipeline(new RocoPipelineSettings()
            {
                ManufacturerDefaultValue = "Roco",
                StopAfterRounds = 2,
                StartUrl = startUrl
            });

            return NoContent();
        }
    }
}
