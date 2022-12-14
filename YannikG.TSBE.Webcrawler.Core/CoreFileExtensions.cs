using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Repositories;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories.Implementations;

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