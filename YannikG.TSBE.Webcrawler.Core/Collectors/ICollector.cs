using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public interface ICollector<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        public Task CollectAsync(TPipelineSettings pipelineSettings, ProcessorNextCallback<TInput> next);
    }
}
