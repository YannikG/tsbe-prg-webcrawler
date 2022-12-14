namespace YannikG.TSBE.Webcrawler.Core.Services
{
    public class PipelineServiceProvider
    {
        private readonly IServiceProvider _provider;

        public PipelineServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

        public T? GetService<T>()
        {
            if (_provider == null)
            {
                throw new ArgumentNullException(nameof(_provider));
            }
            return (T?)_provider.GetService(typeof(T));
        }
    }
}