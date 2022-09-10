using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YannikG.TSBE.Webcrawler.Core.Pipelines;

namespace YannikG.TSBE.Webcrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {
        private readonly RocoBasicArticlePipeline _rocoBasicArticlePipeline;

        public PipelineController(RocoBasicArticlePipeline rocoBasicArticlePipeline)
        {
            _rocoBasicArticlePipeline = rocoBasicArticlePipeline;
        }

        [HttpPost]
        public async Task<IActionResult> StartRocoPipeline(string jobName, string startUrl)
        {
            await _rocoBasicArticlePipeline.Execute(jobName, startUrl);

            return NoContent();
        }
    }
}
