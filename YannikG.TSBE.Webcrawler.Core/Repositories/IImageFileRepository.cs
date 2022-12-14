namespace YannikG.TSBE.Webcrawler.Core.Repositories
{
    public interface IImageFileRepository
    {
        public bool DoesImageAlreadyExists(long imageId);

        public void SaveImage(byte[] imageData, long imageId, string imageFormat);
    }
}