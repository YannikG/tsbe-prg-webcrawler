﻿using Microsoft.Extensions.DependencyInjection;
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
    public static class CoreSqliteExtensions
    {
        public static IServiceCollection ConfigureSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqliteConfig>(
                configuration.GetSection("Sqlite")
            );

            services.AddTransient<SqliteSetupService>();

            return services;
        }
        public static IServiceCollection AddSqliteRepositories(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<IArticleRepository, ArticleSqliteRepository>();
            services.AddScoped<IImageRepository, ImageSqliteRepository>();

            return services;
        }

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