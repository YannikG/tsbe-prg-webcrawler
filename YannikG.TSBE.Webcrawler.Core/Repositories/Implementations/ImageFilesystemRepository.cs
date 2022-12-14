using Microsoft.Extensions.Options;
using YannikG.TSBE.Webcrawler.Core.Repositories.Configs;

namespace YannikG.TSBE.Webcrawler.Core.Repositories.Implementations
{
    public class ImageFilesystemRepository : IImageFileRepository
    {
        private FileExportConfig _config;

        public ImageFilesystemRepository(IOptions<FileExportConfig> config)
        {
            _config = config.Value;

            ensureFoldersExists();
        }

        public bool DoesImageAlreadyExists(long imageId)
        {
            string path = calculateFullPath("");
            return Directory.GetFiles(path, $"{imageId}.*").Length > 0;
        }

        public void SaveImage(byte[] imageData, long imageId, string imageFormat)
        {
            string path = calculateFullPath($"{imageId}.{imageFormat}");
            File.WriteAllBytes(path, imageData);
        }

        private string calculateFullPath(string lastPath)
        {
            string basePath = Environment.CurrentDirectory;

            return Path.Combine(basePath, _config.ImageExportPath, lastPath);
        }

        private void ensureFoldersExists()
        {
            string path = calculateFullPath("");

            Directory.CreateDirectory(path);
        }
    }
}