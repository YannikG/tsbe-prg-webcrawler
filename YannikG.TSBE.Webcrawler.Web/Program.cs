


// TESTING

using YannikG.TSBE.Webcrawler.Core.Collectors;
using YannikG.TSBE.Webcrawler.Core.Models;
using YannikG.TSBE.Webcrawler.Core.Processors;

var collector = new SingleHttpPageCollector();
var processor = new RocoHtmlParserProcessor();

var result = await collector.CollectAsync("https://www.roco.cc/rde/produkte/wagen/guterwagen.html?verfuegbarkeit_status=41%2C42%2C43%2C45");

var processedResult = processor.Process(result);

Console.WriteLine($"Found Results ({processedResult.Count}):");

processedResult.ToList().ForEach(r =>
{
    Console.WriteLine(r.Name);
    Console.WriteLine(r.Url);
});

var fileName = "test";

var fileProcessor = new ObjectToFileProcessor();

fileProcessor.Process<List<BasicArticleModel>>(processedResult.ToList(), fileName);


Console.ReadLine();

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();

