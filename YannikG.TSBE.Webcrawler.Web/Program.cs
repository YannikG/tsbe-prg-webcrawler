﻿using Serilog;
using YannikG.TSBE.Webcrawler.Core;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

// configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile($"appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config
builder.Services.ConfigureSqlite(builder.Configuration);

builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddCollectors();
builder.Services.AddProcessors();
builder.Services.AddSqliteRepositories();
builder.Services.AddPipelineServiceProvider();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI();

app.SetupSqlite();

app.MapControllers();

app.Run();