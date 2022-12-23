using Microsoft.Extensions.Options;
using Dapper;
using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Repositories.Implementations
{
    public class ArticleSqliteRepository : BaseSqliteRepository, IArticleRepository
    {
        public ArticleSqliteRepository(IOptions<SqliteConfig> config) : base(config)
        {
        }

        public void Create(ArticleEntity entity)
        {
            using (var connection = GetConnection())
            {
                connection.Execute($"INSERT INTO {Config.ArticleEntityTableName} (Name,ArticleNumber,ArticleManufacturer,Url,ImageId)" +
                    $" VALUES (@Name, @ArticleNumber, @ArticleManufacturer, @Url, @ImageId)", entity);
            }
        }

        public List<ArticleEntity> GetAll()
        {
            using (var connection = GetConnection())
            {
                var result = connection.Query<ArticleEntity>($"SELECT * FROM {Config.ArticleEntityTableName}");

                return result.ToList();
            }
        }

        public List<ArticleEntity> GetEntitiesByArticleNumberAndManufacturer(string articleNumber, string articleManufacturer)
        {
            using (var connection = GetConnection())
            {
                var result = connection.Query<ArticleEntity>($"SELECT * FROM {Config.ArticleEntityTableName} WHERE ArticleNumber = @ArticleNumber AND ArticleManufacturer = @ArticleManufacturer", new { ArticleNumber = articleNumber, ArticleManufacturer = articleManufacturer });

                return result.ToList();
            }
        }
    }
}