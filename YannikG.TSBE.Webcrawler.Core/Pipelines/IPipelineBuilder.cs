using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;

namespace YannikG.TSBE.Webcrawler.Core.Pipelines
{
    public interface IPipelineBuilder<TInput, TPipelineSettings> where TInput : class where TPipelineSettings : IPipelineSettings
    {
        public IPipelineBuilder<TInput, TPipelineSettings> UseCollector<TCollector>() where TCollector : ICollector<TInput, TPipelineSettings>;
        public IPipelineBuilder<TInput, TPipelineSettings> WithoutCollector();
        public IPipelineBuilder<TInput, TPipelineSettings> AddProcessor<TProcessor>() where TProcessor : IProcessor<TInput, TPipelineSettings>;
        public IPipelineBuilder<TInput, TPipelineSettings> WithoutProcessors();
        public Pipeline<TInput, TPipelineSettings> Build();


    }
}
