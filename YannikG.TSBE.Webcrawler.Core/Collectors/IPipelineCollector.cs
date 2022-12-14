namespace YannikG.TSBE.Webcrawler.Core.Collectors
{
    public interface IPipelineCollector
    {
        public Task<string> CollectAsync(string urlString);
    }
}