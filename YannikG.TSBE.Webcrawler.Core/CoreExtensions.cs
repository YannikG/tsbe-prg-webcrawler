using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Collectors.Handlers.Roco;
using YannikG.TSBE.Webcrawler.Core.Collectors.Requesters;
using YannikG.TSBE.Webcrawler.Core.Pipelines;
using YannikG.TSBE.Webcrawler.Core.Pipelines.Configs;
using YannikG.TSBE.Webcrawler.Core.Processors.Roco;
using YannikG.TSBE.Webcrawler.Core.Repositories;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories.Implementations;
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

            return services;
        }

        public static IServiceCollection AddPipelineServiceProvider(this IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();

            var pipelineServiceProvider = new PipelineServiceProvider(provider);
            services.AddSingleton<PipelineServiceProvider>(pipelineServiceProvider);

            return services;
        }
    }
}

