using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YannikG.TSBE.Webcrawler.Core.Repositories;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;
using YannikG.TSBE.Webcrawler.Core.Repositories.Implementations;
using YannikG.TSBE.Webcrawler.Core.Services;

namespace YannikG.TSBE.Webcrawler.Core
{
    /// <summary>
    /// Extension for sqlite.
    /// </summary>
    public static class CoreSqliteExtensions
    {
        /// <summary>
        /// Configure sqlite and inject setup service for local database.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqliteConfig>(
                configuration.GetSection("Sqlite")
                                            );

            services.AddTransient<SqliteSetupService>();

            return services;
        }
        /// <summary>
        /// Add sqlite based implementation for entities repositories.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqliteRepositories(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<IArticleRepository, ArticleSqliteRepository>();
            services.AddScoped<IImageRepository, ImageSqliteRepository>();

            return services;
        }

        /// <summary>
        /// Run setup to ensure a sqlite database has been created.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder SetupSqlite(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<SqliteSetupService>();
                service.Setup();
            }

            return app;
        }
    }
}