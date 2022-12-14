using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;
using YannikG.TSBE.Webcrawler.Core.Collectors.Requesters;
using YannikG.TSBE.Webcrawler.Core.Pipelines;
using YannikG.TSBE.Webcrawler.Core.Processors.Image;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;
using YannikG.TSBE.Webcrawler.Core.Services;

namespace YannikG.TSBE.Webcrawler.Core
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            // Processors.
            services.AddScoped<RocoModelToArticleEntityProcessor>();
            services.AddScoped<RocoModelToImageEntityProcessor>();
            services.AddScoped<SortAlreadyDownloadedImageProcessor>();
            services.AddScoped<DownloadImagesFromUrlProcessor>();

            return services;
        }

        public static IServiceCollection AddCollectors(this IServiceCollection services)
        {
            // Collector Handlers.
            services.AddScoped<RocoHtmlNextUrlHandler>();
            services.AddScoped<RocoHtmlArticleParserHandler>();

            // Collector Requesters.
            services.AddSingleton<SingleHttpPageRequester>();

            // Collectors.
            services.AddScoped<RocoHtmlCollector>();
            services.AddScoped<ImageFromDatabaseCollector>();

            return services;
        }

        public static IServiceCollection AddPipelineBuilder(this IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();

            var pipelineServiceProvider = new PipelineServiceProvider(provider);
            services.AddSingleton<PipelineServiceProvider>(pipelineServiceProvider);

            // Inject generic interface and class.
            services.AddScoped(typeof(IPipelineBuilder<,>), typeof(PipelineBuilder<,>));

            return services;
        }
    }
}