using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YannikG.TSBE.Webcrawler.Core.Entities;

namespace YannikG.TSBE.Webcrawler.Core.Repositories
{
    public interface IImageRepository
    {
        void Create(ImageEntity entity);
        List<ImageEntity> GetImagesByImageUrl(string imageUrl);
    }
}
