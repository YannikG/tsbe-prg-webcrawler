using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Pipelines;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors;
using YannikG.TSBE.Webcrawler.Core.Processors.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;

namespace YannikG.TSBE.Webcrawler.Core
{
	public static class CoreExtensions
	{
        public static IServiceCollection ConfigurePipelines(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RocoPipelineSettings>(
                configuration.GetSection("Pipeline:Roco")
            );

            return services;
        }
        public static IServiceCollection ConfigureProcessors(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileProcessorSettings>(
                configuration.GetSection("Processor:File")
            );

            return services;
        }

        public static IServiceCollection AddProcessors(this IServiceCollection services)
		{
			services.AddScoped<ObjectToCSVProcessor>();
			services.AddScoped<ObjectToJsonProcessor>();
            services.AddScoped<RocoHtmlNextUrlProcessor>();
            services.AddScoped<RocoHtmlParserProcessor>();
            services.AddScoped<StringToFileProcessor>();

            return services;
		}

        public static IServiceCollection AddCollectors(this IServiceCollection services)
        {
            services.AddScoped<SingleHttpPageCollector>();

            return services;
        }

        public static IServiceCollection AddPipelines(this IServiceCollection services)
        {
            services.AddScoped<RocoBasicArticlePipeline>();

            return services;
        }
    }
}

