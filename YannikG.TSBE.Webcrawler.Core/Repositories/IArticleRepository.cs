using YannikG.TSBE.Webcrawler.Core.Entities;

namespace YannikG.TSBE.Webcrawler.Core.Repositories
{
    public interface IArticleRepository
    {
        List<ArticleEntity> GetAll();

        List<ArticleEntity> GetEntitiesByArticleNumberAndManufacturer(string articleNumber, string articleManufacturer);

        void Create(ArticleEntity entity);
    }
}