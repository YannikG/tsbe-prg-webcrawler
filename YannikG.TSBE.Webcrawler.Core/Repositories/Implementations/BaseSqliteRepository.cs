using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
