using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Repositories.Implementations;
using YannikG.TSBE.Webcrawler.Core.Repositories;
using Microsoft.Extensions.Configuration;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;
using YannikG.TSBE.Webcrawler.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace YannikG.TSBE.Webcrawler.Core
{
    public static class CoreFileExtensions
    {
        public static IServiceCollection ConfigureFileExport(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileExportConfig>(
                configuration.GetSection("FileExport")
            );

            services.AddTransient<FileExportConfig>();

            return services;
        }

        public static IServiceCollection AddFileRepositories(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<IImageFileRepository, ImageFilesystemRepository>();

            return services;
        }
    }
}
