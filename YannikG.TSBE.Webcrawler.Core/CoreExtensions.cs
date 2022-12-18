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
    /// <summary>
    /// Extension for pipelines, processors and collectors.
    /// </summary>
    public static class CoreExtensions
    {
        /// <summary>
        /// Add all processors.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            // Processors.
            services.AddScoped<RocoModelToArticleEntityProcessor>();
            services.AddScoped<RocoModelToImageEntityProcessor>();
            services.AddScoped<SortAlreadyDownloadedImageProcessor>();
            services.AddScoped<DownloadImagesFromUrlProcessor>();

            return services;
        }

        /// <summary>
        /// Add collectors and their handlers / helpers.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add Pipeline builder.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipelineBuilder(this IServiceCollection services)
        {
            // This workaround is required to allow access to the service provider within the pipeline builder.
            IServiceProvider provider = services.BuildServiceProvider();

            var pipelineServiceProvider = new PipelineServiceProvider(provider);
            services.AddSingleton<PipelineServiceProvider>(pipelineServiceProvider);

            // Inject generic interface and class.
            services.AddScoped(typeof(IPipelineBuilder<,>), typeof(PipelineBuilder<,>));

            return services;
        }
    }
}