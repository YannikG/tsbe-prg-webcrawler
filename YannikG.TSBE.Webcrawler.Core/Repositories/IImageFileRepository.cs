using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YannikG.TSBE.Webcrawler.Core.Repositories
{
    public interface IImageFileRepository
    {
        public bool DoesImageAlreadyExists(long imageId);
        public void SaveImage(byte[] imageData, long imageId, string imageFormat);

    }
}
