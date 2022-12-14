using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Repositories.Implementations
{
    public class BaseSqliteRepository
    {
        protected readonly SqliteConfig Config;

        public BaseSqliteRepository(IOptions<SqliteConfig> config)
        {
            if (config.Value is null)
                throw new ArgumentException("config cannot be null!");

            Config = config.Value;
        }

        protected SqliteConnection GetConnection()
        {
            return new SqliteConnection(Config.DatabaseName);
        }
    }
}