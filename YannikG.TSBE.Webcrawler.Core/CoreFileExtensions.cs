using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Repositories;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories.Implementations;

namespace YannikG.TSBE.Webcrawler.Core
{
    /// <summary>
    /// Extension for file access and handling.
    /// </summary>
    public static class CoreFileExtensions
    {
        /// <summary>
        /// Configure file export settings.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureFileExport(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileExportConfig>(
                configuration.GetSection("FileExport")
                                                );

            services.AddTransient<FileExportConfig>();

            return services;
        }

        /// <summary>
        /// Add file access based repositories for entities.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileRepositories(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<IImageFileRepository, ImageFilesystemRepository>();

            return services;
        }
    }
}