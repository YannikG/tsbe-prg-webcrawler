using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Entities;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Repositories.Implementations
{
    public class ImageSqliteRepository : BaseSqliteRepository, IImageRepository
    {
        public ImageSqliteRepository(IOptions<SqliteConfig> config) : base(config)
        {
        }

        public void Create(ImageEntity entity)
        {
            using (var connection = GetConnection())
            {
                connection.Execute($"INSERT INTO {Config.ImageEntityTableName} (ImageUrl)" +
                    $" VALUES (@ImageUrl)", entity);
            }
        }

        public List<ImageEntity> GetImagesByImageUrl(string imageUrl)
        {
            using (var connction = GetConnection())
            {
                var result = connction.Query<ImageEntity>($"SELECT * FROM {Config.ImageEntityTableName} WHERE ImageUrl = @ImageUrl", new { ImageUrl = imageUrl });

                return result.ToList();
            }
        }
    }
}
