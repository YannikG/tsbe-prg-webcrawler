using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Services
{
    public class SqliteSetupService
    {
        private readonly SqliteConfig _config;

        public SqliteSetupService(IOptions<SqliteConfig> options)
        {
            if (options.Value is null)
                throw new ArgumentNullException("SqliteConfig must be provided!");
            _config = options.Value;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_config.DatabaseName);

            string sql = "SELECT name FROM sqlite_master WHERE type='table' AND (";
            sql += $"name = @ArticleTableName OR ";
            sql += $"name = @ImageTableName ";
            sql += ")";

            var table = connection.Query<string>(sql, new { ArticleTableName = _config.ArticleEntityTableName, ImageTableName = _config.ImageEntityTableName });

            if (!table.Contains(_config.ImageEntityTableName))
                setupImageEntity(connection);

            if (!table.Contains(_config.ArticleEntityTableName))
                setupArticleEntity(connection);
        }

        private void setupArticleEntity(SqliteConnection connection)
        {
            string sql = $"CREATE TABLE {_config.ArticleEntityTableName} (";
            sql += "Id INTEGER NOT NULL PRIMARY KEY,";
            sql += "Name VARCHAR(200) NOT NULL,";
            sql += "ArticleNumber VARCHAR(50) NOT NULL,";
            sql += "ArticleManufacturer VARCHAR(200) NOT NULL,";
            sql += "Url VARCHAR(600) NULL,";
            sql += "ImageId INTEGER NULL,";
            sql += $"FOREIGN KEY (ImageId) REFERENCES {_config.ImageEntityTableName} (Id) ON DELETE CASCADE ON UPDATE NO ACTION";
            sql += ")";

            connection.Execute(sql);
        }

        private void setupImageEntity(SqliteConnection connection)
        {
            string sql = $"CREATE TABLE {_config.ImageEntityTableName} (";
            sql += "Id INTEGER NOT NULL PRIMARY KEY,";
            sql += "ImageUrl VARCHAR(600) NULL";
            sql += ")";

            connection.Execute(sql);
        }
    }
}