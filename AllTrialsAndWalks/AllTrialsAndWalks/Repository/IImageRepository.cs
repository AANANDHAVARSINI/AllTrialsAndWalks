using AllTrialsAndWalks.Models.Domain;

namespace AllTrialsAndWalks.Repository
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
