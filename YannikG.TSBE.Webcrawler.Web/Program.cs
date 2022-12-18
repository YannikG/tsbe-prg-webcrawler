using Serilog;
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

// HTTP context
builder.Services.AddHttpContextAccessor();

// configure custom services
builder.Services.ConfigureSqlite(builder.Configuration);
builder.Services.ConfigureFileExport(builder.Configuration);

// add custom services
builder.Services.AddCollectors();
builder.Services.AddProcessors();
builder.Services.AddSqliteRepositories();
builder.Services.AddFileRepositories();
builder.Services.AddPipelineBuilder();

var app = builder.Build();

// use logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI();

// setup custom sqlite database
app.SetupSqlite();

// Controllers
app.MapControllers();

app.Run();